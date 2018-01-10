using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using MapEngine;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

using EntityEnums;

////////////////////////////////////////////////////////////////////////////////////
// All image-based map data is stored in FFAssist format.
// Formulas, map packs, and format information derived from:
// http://forums.windower.net/topic/7563-map-pack-nov-2-and-mapini-dec-15/
////////////////////////////////////////////////////////////////////////////////////
namespace mappy
{
    public class FFXIGameInstance : GameInstance, IFFXIGameContainer, IFFXIMapImageContainer
    {
        //assembler code to scan for in order to locate pointers to data
        //public static readonly string DEFAULT_SIG_ZONE_ID     = ">>0fbfc88bc1894e??a3";
        public static readonly string DEFAULT_SIG_ZONE_ID = "7CE18B4E088B15";
        public static readonly string DEFAULT_SIG_ZONE_SHORT = "5f5ec38b0cf5";
        public static readonly string DEFAULT_SIG_SPAWN_START = "<<8b3e3bfb74128bcfe8";
        public static readonly string DEFAULT_SIG_SPAWN_END = "891e83c60481fe";
        public static readonly string DEFAULT_SIG_MY_ID = ">>8B8E??00000051B9";
        //public static readonly string DEFAULT_SIG_MY_ID = "8B8EA800000051B9"; No Longer Valid
        public static readonly string DEFAULT_SIG_MY_TARGET = "8946188b0d????????85c9";
        //public static readonly string DEFAULT_SIG_MY_TARGET = "3ac3a1????????8a4838"; //target was the only one to break last patch. including this backup just in case it happens again.

        public static string SIG_ZONE_ID = DEFAULT_SIG_ZONE_ID;
        public static string SIG_ZONE_SHORT = DEFAULT_SIG_ZONE_SHORT;
        public static string SIG_SPAWN_START = DEFAULT_SIG_SPAWN_START;
        public static string SIG_SPAWN_END = DEFAULT_SIG_SPAWN_END;
        public static string SIG_MY_ID = DEFAULT_SIG_MY_ID;
        public static string SIG_MY_TARGET = DEFAULT_SIG_MY_TARGET;
        private static bool sigloaded = false;

        private List<string> m_zoneNameShort;
        private FFXIImageMaps m_imagemaps;

        private IntPtr pSpawnStart;
        private IntPtr pSpawnEnd;
        private IntPtr pMyID;
        private IntPtr pMyTarget;
        private IntPtr pZoneID;
        private IntPtr pZoneShortNames;
        private int listSize;
        private int listMax;
        private int lastZone;
        private int lastMapID = -1;
        private bool zoneFinished = false;
        private Dictionary<UInt32, FFXISpawn> m_ServerIDLookup;
        private MapPoint lastPlayerLocation;
        FFXIImageMap curMap = null;

        public event EventHandler ZoneChanged;
        public event EventHandler MapChanged;

        public FFXIGameInstance(Engine engine, Config config, string FilePath, string ModuleName)
            : base(engine, config, ModuleName)
        {
            m_ServerIDLookup = new Dictionary<uint, FFXISpawn>();
            m_zoneNameShort = new List<string>();
            lastZone = 0;

            FFXIGameInstance.LoadSignatures(config);
            m_imagemaps = new FFXIImageMaps(Program.MapFileExt, FilePath);
        }

        public FFXIGameInstance(Process process, Engine engine, Config config, string FilePath, string ModuleName)
            : base(engine, config, ModuleName)
        {
            m_ServerIDLookup = new Dictionary<uint, FFXISpawn>();
            m_zoneNameShort = new List<string>();
            lastZone = 0;

            FFXIGameInstance.LoadSignatures(config);
            m_imagemaps = new FFXIImageMaps(Program.MapFileExt, FilePath);
            Process = process;
        }

        private static void LoadSignatures(Config config)
        {
            if (sigloaded)
                return;

            //Determine if there are manually configured signatures
            if (config.Exists("sigversion"))
            {
                //If so, first determine if there are new internally built ones and clobber the manual ones if so.
                if (config.Get("sigversion", "") != Application.ProductVersion)
                {
                    config.Remove("sigversion");
                    config.Remove("SIG_MY_ID");
                    config.Remove("SIG_MY_TARGET");
                    config.Remove("SIG_SPAWN_END");
                    config.Remove("SIG_SPAWN_START");
                    config.Remove("SIG_ZONE_ID");
                    config.Remove("SIG_ZONE_SHORT");
                }

                //push the manually configured signatures
                SIG_MY_ID = config.Get("SIG_MY_ID", DEFAULT_SIG_MY_ID);
                SIG_MY_TARGET = config.Get("SIG_MY_TARGET", DEFAULT_SIG_MY_TARGET);
                SIG_SPAWN_END = config.Get("SIG_SPAWN_END", DEFAULT_SIG_SPAWN_END);
                SIG_SPAWN_START = config.Get("SIG_SPAWN_START", DEFAULT_SIG_SPAWN_START);
                SIG_ZONE_ID = config.Get("SIG_ZONE_ID", DEFAULT_SIG_ZONE_ID);
                SIG_ZONE_SHORT = config.Get("SIG_ZONE_SHORT", DEFAULT_SIG_ZONE_SHORT);
            }
            sigloaded = true;
        }

        public FFXIImageMaps Maps
        {
            get { return m_imagemaps; }
        }

        /// <summary>Find the zone id from the specified server id.</summary>
        public uint lookupZoneIDFromServerID(UInt32 ServerID)
        {
            if (m_ServerIDLookup.ContainsKey(ServerID))
                return m_ServerIDLookup[ServerID].ID;
            return 0;
        }

        /// <summary>Reload the map image alternative deck.</summary>
        public void ReloadImageMaps()
        {
            ResetImageMap();
            m_imagemaps.Reload();
        }

        public void ResetImageMap()
        {
            engine.MapAlternativeImage = null;
            lastPlayerLocation = null;
            lastMapID = -1;
        }

        /// <summary>Extracts the abbreviated zone text using the specified pointer and builds the lookup table.</summary>
        /// <param name="pTable">A pointer to the pointer table. derived from asm code.</param>
        private void ProcessZoneNames(IntPtr pTable)
        {
            //How things are stored:
            //  there is a pointer table that contains the pointers to the offset table and string table.
            //  the offset table is a serial list of paired integers: [offset][length] x 256
            //  the offset is a hard offset from the string table base address; the length the number of bytes to read.
            //  However, the actual zone text is inset by 40 bytes (for reasons unknown), so that must be accounted for.

            //Read in the pointer table
            m_zoneNameShort.Clear();
            Int32[] pList = reader.ReadStructArray<Int32>(pTable, 4); //this pointer is derived from ASM code. why it includes
            Int32 offsetTableBase = pList[2];                         // the first 2 garbage entries and not the "full text"
            Int32 stringTableBase = pList[3];                         // table instead, i dont know.

            //prime ze pump
            Int32 index = 0;
            Int32 currentOffset = reader.ReadStruct<Int32>((IntPtr)offsetTableBase);
            Int32 currentLength = reader.ReadStruct<Int32>((IntPtr)(offsetTableBase + 4));
            Int32 lastOffset = -1;
            int stringOffset = 0x28; //string is always inset by 40 bytes
            string zoneText = "";

            //Loop through each offset and only stop if the next offset is less than the last...
            //  zone information is stored in a byte, and thus there are only 256 maximum zone entries. period.
            //  If a later expansion exceeds this amount, it is quite possible SE will convert this to a short.
            //  so this way of pulling the data out is an attempt to be future proof.
            while (currentOffset > lastOffset)
            {
                //read the string at the specified table offset
                zoneText = reader.ReadString((IntPtr)(stringTableBase + currentOffset + stringOffset), currentLength - stringOffset);
                //add it to the pile.
                m_zoneNameShort.Add(zoneText);
                //read the next pair
                index++;
                lastOffset = currentOffset;
                currentOffset = reader.ReadStruct<Int32>((IntPtr)(offsetTableBase + (index * 8)));
                currentLength = reader.ReadStruct<Int32>((IntPtr)(offsetTableBase + (index * 8) + 4));
            }
        }

        //This is a pair of functions to allow the user to save changes to the map even after zoning.
        //  Becuase the zone change is performed during polling, this must be offloaded into another thread.
        //  Otherwise the next interval will raise the zoning code again while it waits for the user to answer.
        private void DirtyZone(MapData data)
        {
            //start the thread
            Thread thread = new Thread(DirtyZoneThread);
            thread.Start(data);
        }
        private void DirtyZoneThread(object data)
        {
            MapData copy = (MapData)data;
            if (System.Windows.Forms.MessageBox.Show(Program.GetLang("msg_unsaved_text"), Program.GetLang("msg_unsaved_title"), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                copy.Save();
            }
        }

        protected override void OnInitializeProcess()
        {
            try
            {
                //These are direct addresses to things used by the instance manager
                pSpawnStart = reader.FindSignature(SIG_SPAWN_START, Program.ModuleName);
                pSpawnEnd = reader.FindSignature(SIG_SPAWN_END, Program.ModuleName);
                pMyID = reader.FindSignature(SIG_MY_ID, Program.ModuleName, 4);
                pZoneID = reader.FindSignature(SIG_ZONE_ID, Program.ModuleName);

                //This is a double pointer to the target table. the table is laid out by:
                // 0x00 Zone ID
                // 0x04 Server ID (null if not a player)
                // 0x08 p->Spawn Record
                IntPtr ppMyTarget = reader.FindSignature(SIG_MY_TARGET, Program.ModuleName);

                //This is a double pointer to the zone name table
                IntPtr ppZoneShortNames = reader.FindSignature(SIG_ZONE_SHORT, Program.ModuleName, 0xA0); //offset by 0xA0
                pMyTarget = IntPtr.Zero;
                pZoneShortNames = IntPtr.Zero;

                if (ppMyTarget != IntPtr.Zero)
                    pMyTarget = (IntPtr)reader.ReadStruct<Int32>(ppMyTarget);
                if (ppZoneShortNames != IntPtr.Zero)
                    pZoneShortNames = (IntPtr)reader.ReadStruct<Int32>(ppZoneShortNames);

#if DEBUG
            //Quick grabbag of pointers for memory digging
            Debug.WriteLine("Signature Addressess:");
            Debug.WriteLine("Spawn Start: " + pSpawnStart.ToString("X"));
            Debug.WriteLine("Spawn End: " + pSpawnEnd.ToString("X"));
            Debug.WriteLine("My ID: " + pMyID.ToString("X"));
            Debug.WriteLine("*My Target: " + ppMyTarget.ToString("X"));
            Debug.WriteLine("My Target: " + pMyTarget.ToString("X"));
            Debug.WriteLine("Zone ID: " + pZoneID.ToString("X"));
            Debug.WriteLine("*Zone Names (short): " + ppZoneShortNames.ToString("X"));
            Debug.WriteLine("Zone Names (short): " + pZoneShortNames.ToString("X"));
#endif

                if (pSpawnStart == IntPtr.Zero || pSpawnEnd == IntPtr.Zero || pMyID == IntPtr.Zero ||
                   pZoneID == IntPtr.Zero || pMyTarget == IntPtr.Zero || pZoneShortNames == IntPtr.Zero)
                {
                    string failtext = "[FAILED]";
                    string varlist = "Spawn List Start: " + (pSpawnStart == IntPtr.Zero ? failtext : pSpawnStart.ToString("X2"));
                    varlist += "\nSpawn List End: " + (pSpawnEnd == IntPtr.Zero ? failtext : pSpawnEnd.ToString("X2"));
                    varlist += "\nMy ID: " + (pMyID == IntPtr.Zero ? failtext : pMyID.ToString("X2"));
                    varlist += "\nMy Target: " + (pMyTarget == IntPtr.Zero ? failtext : pMyTarget.ToString("X2"));
                    varlist += "\nZone ID: " + (pZoneID == IntPtr.Zero ? failtext : pZoneID.ToString("X2"));
                    varlist += "\nZone Names (short): " + (pZoneShortNames == IntPtr.Zero ? failtext : pZoneShortNames.ToString("X2"));
                    throw new InstanceException(string.Format(Program.GetLang("msg_invalid_sig_text"), Process.MainWindowTitle, varlist), InstanceExceptionType.SigFailure);
                }

                //cache the size and index count of the array
                listSize = (int)pSpawnEnd - (int)pSpawnStart;
                if (listSize % 4 != 0)
                    return;
                listMax = listSize / 4; //size of a 32bit pointer

                //Build the zone name table
                ProcessZoneNames(pZoneShortNames);
                Valid = true;
            }
            catch (InstanceException fex)
            {
                throw fex; //cascade the exception to the caller
            }
            catch (MemoryReaderException fex)
            {
                throw fex; //cascade the exception to the caller
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while initializing the process: " + ex.Message);
            }
        }

        public override bool Poll()
        {
            if (Switching)
                return true;
            if (reader == null)
                return false;
            if (reader.HasExited)
                return false;
            if (!Valid)
                throw new InstanceException("FFXI could not be polled becuase the context is invalid.", InstanceExceptionType.InvalidContext);

            try
            {
                //grab the current zone id
                Int32 ZoneID = reader.ReadStruct<Int32>(pZoneID);
                if (ZoneID > 0xFF)
                {
                    if (ZoneID == 0x122) //bastok mog house floor 1
                    {
                        ZoneID = 0xE5;
                    }
                    else if (ZoneID == 0x268) //bastok mog house floor 2
                    {
                        ZoneID = 0x268;
                    }
                    else if (ZoneID == 0x101) //sandoria mog house floor 1
                    {
                        ZoneID = 0xE5;
                    }
                    else if (ZoneID == 0x267) //sandoria mog house floor 2
                    {
                        ZoneID = 0x267;
                    }
                    else if (ZoneID == 0x120) //windurst mog house floor 1
                    {
                        ZoneID = 0xE5;
                    }
                    else if (ZoneID == 0x269) //windurst mog house floor 2
                    {
                        ZoneID = 0x269;
                    }
                    else if (ZoneID == 0x26A) //Mog Patio
                    {
                        ZoneID = 0x26A;
                    }
                    else if (ZoneID == 0x100) //jeuno mog house
                    {
                        ZoneID = 0x116;
                    }
                    else if (ZoneID == 0x124) //adoulin mog house
                    {
                        ZoneID = 0x11E;
                    }
                    else
                    {
                        ZoneID = ZoneID - 0x1BC;
                    }
                }
                //stop all reading while the zone is in flux
                if (ZoneID == 0)
                {
                    lastZone = -1; //make sure the data gets properly reset in case the player zones into the same zone (tractor, warp, etc)
                    lastMapID = -1;
                    return true;
                }

                //If the zone has changed, then clear out any old spawns and load the zone map
                if (ZoneID != lastZone || engine.Data.Empty)
                {
                    //An edit to the map has been made. Inform the user they are about to lose thier changes
                    //  and give them a final opportunity to save them.
                    if (engine.Data.Dirty)
                    {
                        //clone the map data
                        MapData mapcopy = engine.Data.Clone();
                        //pass the closed data to another thread so the current zone can continue processing.
                        DirtyZone(mapcopy);
                    }

                    zoneFinished = false;

                    //get the zone name
                    string shortName = "";
                    if (ZoneID < m_zoneNameShort.Count)
                        shortName = m_zoneNameShort[ZoneID];    //grab the new zone name
                    if (shortName == "")
                        shortName = "Zone" + ZoneID.ToString(); //support unnamed zones, like the mog house

                    //release zone resources consumed by the image map processor
                    if (engine.ShowMapAlternative)
                    {
                        engine.MapAlternativeImage = null;
                        m_imagemaps.ClearCache(lastZone);
                    }

                    //clear the old zone data and load in the new
                    engine.Clear(); //clear both the spawn and map data
                    engine.Data.ZoneName = shortName;
                    engine.Data.LoadZone(shortName); //load the zone map
                    lastZone = ZoneID;
                    lastMapID = -1;
                }

                //read the pointer array in one big lump, and create spawns for any new id's detected
                Int32[] spawnList = reader.ReadStructArray<Int32>(pSpawnStart, listMax); //cant use intptr since its machine dependant
                for (uint i = 0; i < listMax; i++)
                {
                    if (spawnList[i] > 0)
                    {
                        //only add new id's. each spawn is responsible for updating itself.
                        if (!engine.Game.Spawns.ContainsIndex(i))
                        {
                            //create the spawn and add it to the game data
                            FFXISpawn spawn = new FFXISpawn(i, (IntPtr)spawnList[i], this);

                            engine.Game.Spawns.Add(spawn);

                            //spawn.DEBUGHOVER = "pointer: " + spawnList[i].ToString("X") + " index: " + i;

                            //add the spawn to the server lookup table. this is used later to convert the claim id
                            if (spawn.Type == SpawnType.Player)
                            {
                                if (m_ServerIDLookup.ContainsKey(spawn.ServerID))
                                    m_ServerIDLookup[spawn.ServerID] = spawn;
                                else
                                    m_ServerIDLookup.Add(spawn.ServerID, spawn); //add the server id to the lookup table
                            }
                        }
                    }
                }

                //fill in the player and target
                UInt32 myID = reader.ReadStruct<UInt32>(pMyID);
                engine.Game.setPlayer(myID, true);
                UInt32 myTarget = reader.ReadStruct<UInt32>(pMyTarget);
                engine.Game.setTarget(myTarget, true);

                //force each spawn to self update
                engine.Game.Update();

                //determine if the map image alternative requires processing
                if (engine.ShowMapAlternative && (lastPlayerLocation == null || (engine.Game.Player != null && !engine.Game.Player.Location.isEqual(lastPlayerLocation))))
                {
                    //Since the player has moved, determine if the map id has changed
                    lastPlayerLocation = engine.Game.Player.Location.Clone();
                    curMap = m_imagemaps.GetCurrentMap(ZoneID, lastPlayerLocation);

                    //only process if there is a map to display
                    if (curMap != null)
                    {
                        /// IHM EDIT
                        //Send the location in image coordinates to the engine. (I don't like doing it this way.)
                        engine.LocInImage = curMap.Translate(engine.Game.Player.Location);

                        //only process if the map has actually changed
                        if (curMap.MapID != lastMapID)
                        {
                            engine.MapAlternativeImage = curMap.GetImage(); //set the background image
                            RectangleF bounds = curMap.Bounds;                //retrieve the map coodinate boundaries
                            engine.MapAlternativeBounds = bounds;           //set the origin/scale of the background image
                            engine.Data.CheckBounds(bounds);                //expand the map bounds (if necessary) to allow the map to be zoomed all the way out
                            lastMapID = curMap.MapID;                         //set the map id so that the map isnt processed again until a change is made
                            if (MapChanged != null)
                                MapChanged(curMap, new EventArgs());
                        }
                    }
                    else if (engine.MapAlternativeImage != null)
                    {
                        //inform the engine that there is no map to display for the current location
                        engine.MapAlternativeImage = null;
                        lastMapID = -1;
                    }
                }

                if (engine.Game.Spawns.Count > 0 && !zoneFinished)
                {
                    zoneFinished = true;

                    //automatically snap the range into view (if enabled)
                    if (engine.AutoRangeSnap)
                        engine.SnapToRange();
                    if (ZoneChanged != null)
                        ZoneChanged(curMap, new EventArgs());
                }
                return true;
#if DEBUG
         } catch(Exception ex) {
            Debug.WriteLine("Error while polling the process: " + ex.Message);
#else
            }
            catch
            {
#endif
                return false;
            }
        }

        public FFXIImageMap CurrentMap
        {
            get { return curMap; }
        }
        public FFXIZoneMaps CurrentZone
        {
            get
            {
                if (curMap != null)
                    return curMap.Zone;
                return null;
            }
        }

        public void Save()
        {
            m_imagemaps.SaveMapData();
        }
    }

    //Biggest. Struct. Ever.
    [StructLayout(LayoutKind.Sequential, Pack = 1)] //im sure some of these fields could be removed with the proper packing. but im lazy.
    public struct SpawnInfo
    {
        public UInt32 EntityVTablePtr;      // CYyObject
        public float locX;                  // Client side positions?
        public float locZ;
        public float locY;
        public float locUnk;
        public float locRoll;
        public float locYaw;
        public float locPitch;
        public float Unk1;
        public float lastX;                 // Last known positions?
        public float lastZ;
        public float lastY;
        public float lastUnk;
        public float lastRoll;
        public float lastYaw;
        public float lastPitch;
        public UInt32 Unk2;
        public float moveX;                 // Server given positions?
        public float moveZ;
        public float moveY;
        public float moveUnk;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] Unk00;
        public UInt32 UnknownVTablePtr;
        public UInt32 ZoneID;               //TargetID
        public UInt32 ServerID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
        public string DisplayName;          //player is 16 max, but npc's can be up to 24.
        public float RunSpeed;
        public float AnimationSpeed;
        public UInt32 WarpPtr;
        public UInt32 Unk01;
        public UInt32 Unk02;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public UInt32[] Unk03;
        public float Distance;
        public UInt32 Unk04;                // 0x64
        public UInt32 Unk05;                // 0x64
        public float Heading;               // Yaw
        public UInt32 PetOwnerID;          //only for permanent pets. charmed mobs do not fill this.
        public byte HealthPercent;
        public byte Unk06;
        public byte ModelType;
        public byte Race;
        public byte Unk07;
        public UInt16 Unk08;                // Some type of timer..
        public byte Unk09;                // Deals with model update..
        public byte ModelFade;              // Updates the entity model. (Blinking)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public byte[] Unk13;
        public UInt16 ModelFace;
        public UInt16 ModelHead;
        public UInt16 ModelBody;
        public UInt16 ModelHands;
        public UInt16 ModelLegs;
        public UInt16 ModelFeet;
        public UInt16 ModelMain;
        public UInt16 ModelSub;
        public UInt16 ModelRanged;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public byte[] Unk14;
        public UInt16 ActionWaitTimer1;
        public UInt16 ActionWaitTimer2;
        public UInt32 Flags1;               // Main Entity Rendering Flag..
        public UInt32 Flags2;               // Name Flags.. (Party, Away, Anon, etc.)
        public UInt32 Flags3;               // Name flags.. (Bazaar, GM icon, etc.)
        public UInt32 Flags4;               // Entity shadow..
        public UInt32 Flags5;               // Name visibility..
        public float Unk15;                 // Deals with fishing..
        public UInt32 Unk16;                // Fade-in effect (Valid values: 3, 6)
        public UInt16 Unk17;                // Fade-in misc (-1 gets reset to 0)
        public UInt32 Unk18;
        public UInt16 NPCSpeechLoop;
        public UInt16 NPCSpeechFrame;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public byte[] Unk19;
        public float RunSpeed2;
        public UInt16 NPCWalkPos1;
        public UInt16 NPCWalkPos2;
        public UInt16 NPCWalkMode;
        public UInt16 CostumeID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string mou4;                 // Always 'mou4'..
        public UInt32 Status;
        public UInt32 StatusServer;
        public UInt32 StatusNpcChat;        // Only used while talking with npc..
        public UInt32 Unk20;
        public UInt32 Unk21;
        public UInt32 Unk22;
        public UInt32 Unk23;
        public UInt32 ClaimID;              // The ID of the last person to perform an action on the mob
        public UInt32 Unk25;                // Has something to do with inventory..
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation1;           // Animation strings.. idl, sil, wlk, etc..
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation3;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation4;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation5;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation6;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation7;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation8;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation9;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Animation10;
        public UInt16 AnimationTick;        // Current ticks of the animation..
        public UInt16 AnimationStep;        // Current step of the animation..
        public byte AnimationPlay;          // 6 stand/sit, 12 play current emote..
        public byte Unk26;                  // Something with animations..
        public UInt16 Unk27;                // Something with animations..
        public UInt16 Unk28;                // Something with animations..
        public UInt16 Unk29;                // Does nothing..
        public UInt32 EmoteID;              // Emote string..
        public UInt32 Unk30;
        public UInt32 Unk31;
        public UInt32 SpawnType;            // 0x0001 PC, 0x0002 NPC, 0x0010 Mob, 0x000D Self
        public byte LSColorRed;
        public byte LSColorGreen;
        public byte LSColorBlue;
        public byte LSUnk;
        public UInt16 NameColor;            // Sets the players name color..
        [MarshalAs(UnmanagedType.I1)]
        public bool CampaignMode;
        public byte MountID;                // ID+1 Except for Chocobo mount
        public UInt16 FishingTimer;         // Counts down from when you click 'fish' to either catch or real in..
        public UInt16 FishingCastTimer;     // Counts down fromw when you click 'fish' til your bait hits the water..
        public UInt32 FishingUnknown0001;   // Gets set to 1800 when you hook a fish.. then unknown afterward..
        public UInt32 FishingUnknown0002;   // Gets read when you first cast your rod..
        public UInt16 FishingUnknown0003;   // Gets set when you first cast your rod..
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public byte[] Unk33;
        public UInt16 TargetIndex;          // The players target's index.
        public UInt16 PetIndex;
        public UInt16 Unk34;                // Countdown after talking with an npc.
        public byte Unk35;                  // Flag after talkign with an npc.
        public byte BallistaScoreFlag;      // Deals with Ballista / PvP, shows game information..
        public byte PankrationEnabled;      // Displays the Pankration score flags.
        public byte PankrationFlagFlip;     // Determines which side each flag is on.
        public UInt16 Unk36;                // Deals with current action..
        public float ModelSize;
        public UInt32 Unk37;    
        public UInt16 Unk38;
        public UInt16 Unk39;
        public UInt16 MonstrosityFlag;      // 01 Sets the entity name to a status icon of a black cat..
        public UInt16 Unk40;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string MonstrosityName;
    }

    /// <summary>A FFXI-specific spawn that updates itself from game memory.</summary>
    public class FFXISpawn : GameSpawn
    {
        private IntPtr m_pointer;
        private MemoryReader m_reader;
        private FFXIGameInstance m_instance;
        private UInt32 m_serverID;
        private UInt32 m_serverClaimID = 0;

        public FFXISpawn(uint ID, IntPtr pointer, FFXIGameInstance instance)
        {
            base.ID = ID;
            m_pointer = pointer;
            m_instance = instance;
            m_reader = instance.Reader;
            Update(); //read the memory straight away so that the server id is available
        }

        /// <summary>The memory address to retrieve the current spawn state from.</summary>
        public IntPtr Pointer
        {
            get { return m_pointer; }
        }

        /// <summary>The server id assigned to this spawn. This is FFXI specific data.</summary>
        public uint ServerID
        {
            get { return m_serverID; }
        }

        /// <summary>Updates the spawn state from game memory.</summary>
        public override void Update()
        {
            try
            {
                //read the memory for the this spawn
                SpawnInfo info = m_reader.ReadStruct<SpawnInfo>(m_pointer);

                //The Zone ID is always known upfront in FFXI. This is a sanity check.
                if (info.ZoneID != base.ID)
                    return;

                m_serverID = info.ServerID;
                base.Location.X = info.moveX;
                base.Location.Y = info.moveY;
                base.Location.Z = info.moveZ;
                base.Heading = info.Heading;
                base.HealthPercent = info.HealthPercent;
                base.Speed = info.RunSpeed;
                base.Hidden = false;
                base.Dead = false;
                base.InCombat = false;
                base.GroupMember = false;
                base.RaidMember = false;
                base.Name = info.DisplayName;
                base.Level = -1; //level is never known in FFXI
                base.Distance = 0;
				base.TargetIndex = info.TargetIndex;
                base.PetIndex = info.PetIndex;
                base.Attackable = true;

                //calculate the distance between the player and this spawn.
                if (m_instance.Engine.Game.Player != null && m_instance.Engine.Game.Player != this)
                    base.Distance = (float)m_instance.Engine.Game.Player.Location.calcDist2D(base.Location);

                //set linkshell color for players
                if (base.Type == SpawnType.Player)
                    base.FillColor = Color.FromArgb(info.LSColorRed, info.LSColorGreen, info.LSColorBlue);

                //set the spawn type
                if ((info.SpawnType & (int)EntityType.PC) != 0)
                {
                    base.Type = SpawnType.Player;
                }
                else if ((info.SpawnType & (int)EntityType.NPC) != 0)
                {
                    base.Type = SpawnType.NPC;
                }
                else if ((info.SpawnType & (int)EntityType.MOB) != 0)
                {
                    base.Type = SpawnType.MOB;
                }

                //only process the claim if the id has changed
                if (m_serverClaimID != info.ClaimID)
                {
                    if (info.ClaimID > 0)
                    {
                        //Claims use server IDs not zone IDs. Since the engine does not know what a server id is, a lookup must be done ahead of time
                        UInt32 lookup = m_instance.lookupZoneIDFromServerID(info.ClaimID);

                        //it is possible that the lookup will fail if the claimee is added after the claimed mob (can happen on zone).
                        if (lookup > 0)
                        {
                            base.ClaimID = lookup;
                            m_serverClaimID = info.ClaimID; //only lock things in if the lookup is successful.
                        }
                    }
                    else
                    {
                        //the mob is no longer claimed, so zero things out
                        base.ClaimID = 0;
                        m_serverClaimID = 0;
                    }
                }

                //set party/alliance flags
                if ((info.SpawnType & (int)EntityType.GroupMember) != 0)
                    base.GroupMember = true;
                if ((info.SpawnType & (int)EntityType.AllianceMember) != 0)
                    base.RaidMember = true;

                //set combat/visibility modes
                if ((info.Flags1 & (int)RenderFlags1.Hidden) != 0 || (base.Type == SpawnType.NPC && info.Flags1 == 0x40530000))
                    base.Hidden = true;
                if ((info.Status & (int)FFXICombatFlags.Dead) != 0 && info.Status < 4) //prevent sit and heal messing with us
                    base.Dead = true;
                if ((info.Status & (int)FFXICombatFlags.InCombat) != 0 && info.Status < 4) //prevent sit and heal messing with us
                    base.InCombat = true;

                base.Attackable = base.Type == SpawnType.MOB && !base.Dead && info.ClaimID > 0 && (info.Flags4 & (int)RenderFlags4.Attackable) != 0;

                //set icon if any of these situations are met
                if (base.Type == SpawnType.MOB && base.InCombat && !base.Dead)
                {
                    if (info.ClaimID > 0)
                    {
                        if ((info.Flags4 & (int)RenderFlags4.Attackable) != 0)
                        {
                            base.Icon = MapRes.StatusBattleTarget;
                        }
                        else
                        {
                            base.Icon = MapRes.StatusClaimed;
                        }
                    }
                    else
                    {
                        base.Icon = MapRes.StatusAggro;
                    }
                }
                else if ((info.Flags2 & (int)RenderFlags2.ConnectionLost) != 0)
                {
                    base.Icon = MapRes.StatusDisconnected;
                }
                else if (base.Dead)
                {
                    base.Icon = MapRes.StatusDead;
                }
                else if (base.Alert)
                {
                    base.Icon = MapRes.StatusAlert;
                }
                else if ((info.Flags3 & (int)RenderFlags3.GM) != 0) //Adjusted flag location
                {
                    base.Icon = MapRes.StatusGM;
                }
                else if (base.Type == SpawnType.Player && (info.Flags4 & (int)RenderFlags4.Charmed) != 0)
                {
                    base.Icon = MapRes.StatusCharmed;
                }
                else if (base.Type == SpawnType.MOB && info.CampaignMode)
                {
                    base.Icon = MapRes.StatusCampaign;
                }
                else if ((info.Flags3 & (int)RenderFlags3.InvisibleEffect) != 0)
                {
                    base.Icon = MapRes.StatusInvisible;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Moogle")
                {
                    base.Icon = MapRes.StatusMoogle;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName.Contains(" Moogle"))
                {
                    base.Icon = MapRes.StatusMoogle;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName.Contains("Home Point"))
                {
                    base.Icon = MapRes.HomePoint;
                }
                else if (base.Type == SpawnType.Player && (info.Status & (int)FFXICombatFlags.Chocobo) == 5 && info.Status < 6)
                {
                    base.Icon = MapRes.StatusChocobo;
                }
                else if (base.Type == SpawnType.Player && (info.Status & (int)FFXICombatFlags.Mount) == 85)
                {
                    if (info.MountID == 2)
                    {
                        base.Icon = MapRes.StatusRaptor;
                    }
                    else if (info.MountID == 3)
                    {
                        base.Icon = MapRes.StatusTiger;
                    }
                    else if (info.MountID == 4)
                    {
                        base.Icon = MapRes.StatusCrab;
                    }
                    else if (info.MountID == 5)
                    {
                        base.Icon = MapRes.StatusRedCrab;
                    }
                    else if (info.MountID == 6)
                    {
                        base.Icon = MapRes.StatusBomb;
                    }
                    else if (info.MountID == 7)
                    {
                        base.Icon = MapRes.StatusRam;
                    }
                    else if (info.MountID == 8)
                    {
                        base.Icon = MapRes.StatusMorbol;
                    }
                    else if (info.MountID == 9)
                    {
                        base.Icon = MapRes.StatusCrawler;
                    }
                    else if (info.MountID == 10)
                    {
                        base.Icon = MapRes.Fenrir;
                    }
                    else if (info.MountID == 11)
                    {
                        base.Icon = MapRes.StatusBeetle;
                    }
                    else if (info.MountID == 12)
                    {
                        base.Icon = MapRes.MountMoogle;
                    }
                    else if (info.MountID == 13)
                    {
                        base.Icon = MapRes.StatusPot;
                    }
                    else if (info.MountID == 14)
                    {
                        base.Icon = MapRes.StatusTulfaire;
                    }
                    else if (info.MountID == 15)
                    {
                        base.Icon = MapRes.StatusWarmachine;
                    }
                    else if (info.MountID == 16)
                    {
                        base.Icon = MapRes.StatusXzomit;
                    }
                    else if (info.MountID == 17)
                    {
                        base.Icon = MapRes.StatusHippogryph;
                    }
                    else if (info.MountID == 18)
                    {
                        base.Icon = MapRes.StatusDverg;
                    }
                    else if (info.MountID == 19)
                    {
                        base.Icon = MapRes.StatusSpheroid;
                    }
                    else
                    {
                        base.Icon = MapRes.StatusMount;
                    }
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Waypoint")
                {
                    base.Icon = MapRes.Waypoint;
                }
                else if (base.Type == SpawnType.NPC && (info.DisplayName == "Augural Conveyor" || info.DisplayName == "Living Cairn"))
                {
                    base.Icon = MapRes.Conveyor;
                }
                else if (base.Type == SpawnType.NPC && (info.DisplayName == "Treasure Chest" || info.DisplayName == "Treasure Coffer" || info.DisplayName == "Treasure Casket" || info.DisplayName == "Ancient Lockbox" || info.DisplayName == "Armoury Crate" || info.DisplayName == "Riftworn Pyxis" || info.DisplayName == "Sturdy Pyxis" || info.DisplayName == "Emblazoned Reliquary"))
                {
                    base.Icon = MapRes.StatusTreasure;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Cavernous Maw")
                {
                    base.Icon = MapRes.Maw;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Survival Guide")
                {
                    base.Icon = MapRes.SurvivalGuide;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Telepoint")
                {
                    base.Icon = MapRes.Telepoint;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Shattered Telepoint")
                {
                    base.Icon = MapRes.ShatteredTelepoint;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Planar Rift")
                {
                    base.Icon = MapRes.PlanarRift;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Ethereal Junction")
                {
                    base.Icon = MapRes.EtherealJunction;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Carbuncle")
                {
                    base.Icon = MapRes.Carbuncle;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Alexander")
                {
                    base.Icon = MapRes.Alexander;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Diabolos")
                {
                    base.Icon = MapRes.Diabolos;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Garuda")
                {
                    base.Icon = MapRes.Garuda;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Fenrir")
                {
                    base.Icon = MapRes.Fenrir;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Ifrit")
                {
                    base.Icon = MapRes.Ifrit;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Leviathan")
                {
                    base.Icon = MapRes.Leviathan;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Odin")
                {
                    base.Icon = MapRes.Odin;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Ramuh")
                {
                    base.Icon = MapRes.Ramuh;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Shiva")
                {
                    base.Icon = MapRes.Shiva;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Titan")
                {
                    base.Icon = MapRes.Titan;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Cait Sith")
                {
                    base.Icon = MapRes.CaitSith;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Atomos")
                {
                    base.Icon = MapRes.Maw;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Mining Point")
                {
                    base.Icon = MapRes.Pickaxe;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Harventing Point")
                {
                    base.Icon = MapRes.Sickle;
                }
                else if (base.Type == SpawnType.NPC && info.DisplayName == "Logging Point")
                {
                    base.Icon = MapRes.Hatchet;
                }
                else
                {
                    base.Icon = null;
                }
            }
            catch { }
        }
    }

    public interface IFFXIGameContainer
    {
        event EventHandler ZoneChanged;
        event EventHandler MapChanged;
    }

    public interface IFFXIMapImageContainer
    {
        FFXIImageMaps Maps { get; }
        FFXIImageMap CurrentMap { get; }
        FFXIZoneMaps CurrentZone { get; }
        void ResetImageMap();
        void Save();
    }

    /// <summary>A collection of image maps keyed by its zone</summary>
    public class FFXIImageMaps
    {
        private Dictionary<int, FFXIZoneMaps> m_zones;
        private string m_filePath;
        private string m_lastLoad;
        private string[] m_extentionList;

        public FFXIImageMaps(string ImageExtList, string FilePath)
        {
            m_extentionList = ImageExtList.Split('|');
            m_filePath = FilePath;
            m_lastLoad = "";
            m_zones = new Dictionary<int, FFXIZoneMaps>();
            LoadMapData();
        }

        /// <summary>Gets the file extention to be appended to the generated map file.</summary>
        public string[] FileExtList
        {
            get { return m_extentionList; }
        }

        /// <summary>Gets or sets the file path where the map pack is located at.</summary>
        public string FilePath
        {
            get { return m_filePath; }
            set
            {
                m_filePath = value;
                Reload();
            }
        }

        /// <summary>Gets whether and zone data has been loaded or not.</summary>
        public bool Empty
        {
            get { return m_zones.Count == 0; }
        }

        /// <summary>Determines if the given zone id exists within the collection.</summary>
        public bool ContainsKey(int key)
        {
            return m_zones.ContainsKey(key);
        }

        /// <summary>Gets the specified zone collection.</summary>
        public FFXIZoneMaps this[int key]
        {
            get { return m_zones[key]; }
        }

        /// <summary>Gets the currently applicable map.</summary>
        public FFXIImageMap GetCurrentMap(int ZoneID, MapPoint point)
        {
            if (m_zones.ContainsKey(ZoneID))
                return m_zones[ZoneID].GetCurrentMap(point);
            return null;
        }

        /// <summary>Reloads the map data.</summary>
        public void Reload()
        {
            m_zones.Clear();
            LoadMapData();
        }

        /// <summary>Releases cached image data across all zones.</summary>
        public void ClearCache()
        {
            foreach (KeyValuePair<int, FFXIZoneMaps> pair in m_zones)
                pair.Value.ClearCache();
        }

        /// <summary>Releases cached image data for the specified zone.</summary>
        public void ClearCache(int ZoneID)
        {
            if (m_zones.ContainsKey(ZoneID))
                m_zones[ZoneID].ClearCache();
        }


        /// <summary>Writes map data to the currently loaded file.</summary>
        public void SaveMapData()
        {
            if (m_lastLoad == "")
                return;
            SaveMapData(m_lastLoad);
        }

        /// <summary>Writes map data to the specified file.</summary>
        public void SaveMapData(string FilePath)
        {
            string tempFile = Path.GetTempFileName();
            string output;
            int zoneID;
            FFXIImageMap map;

            try
            {
                //Save the data to a temp file first, so that if an error occurs the ini file isnt left empty
                FileStream stream = File.Open(tempFile, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);

                //hey it is an ini file afterall
                writer.WriteLine("[Map]");

                //sort zones by id
                SortedList<int, FFXIZoneMaps> zones = new SortedList<int, FFXIZoneMaps>(m_zones);
                foreach (KeyValuePair<int, FFXIZoneMaps> zonepair in zones)
                {
                    zoneID = zonepair.Value.ZoneID;

                    foreach (KeyValuePair<int, FFXIImageMap> mappair in zonepair.Value)
                    {
                        map = mappair.Value;

                        output = string.Format(CultureInfo.InvariantCulture, "{0:X2}_{1:#0}={2:0.###},{3:0.###},{4:0.###},{5:0.###}", zoneID, map.MapID, map.XScale, map.XOffset, map.YScale, map.YOffset);
                        foreach (FFXIImageMapRange range in map)
                        {
                            output += string.Format(CultureInfo.InvariantCulture, ",{0:0.###},{1:0.###},{2:0.###},{3:0.###},{4:0.###},{5:0.###}", range.Left, range.Floor, range.Top, range.Right, range.Ceiling, range.Bottom); //XZY ordering
                        }
                        writer.WriteLine(output);
                    }
                }
                writer.Close();

                //overwrite the old file with the new one
                if (File.Exists(FilePath))
                    File.Delete(FilePath);
                File.Move(tempFile, FilePath);
            }
            catch (Exception ex)
            {
                if (File.Exists(FilePath)) //if the temp file still exists, remove it
                    File.Delete(tempFile);
                throw ex; //escalate error to caller
            }
        }

        /// <summary>Loads and parses the map.ini from the current map path.</summary>
        private void LoadMapData()
        {
            if (m_filePath == "")
                return;

            if (!File.Exists(m_filePath + Program.MapIniFile))
            {
                Debug.WriteLine("WARNING: map ini does not exist");
                return;
            }

            try
            {
                //Why have the overhead of getprivateprofile when we can just do it ourself?
                m_lastLoad = m_filePath + Program.MapIniFile;
                FileStream stream = File.OpenRead(m_lastLoad);
                StreamReader reader = new StreamReader(stream);
                string line = "";
                string subline = "";
                string[] parts;
                string[] subparts;
                int zoneid;
                int mapid;
                int linenumber = 0;
                int idx = 0;
                FFXIZoneMaps zonemaps = null;
                FFXIImageMap map = null;

                while ((line = reader.ReadLine()) != null)
                {
                    linenumber++;
                    try
                    {
                        if (line.Length == 0 || line[0] == ';' || line[0] == '[') //only care about the data
                            continue;

                        //parse the key/value pair of the ini line
                        parts = line.Split('=');
                        if (parts.Length != 2)
                            continue;

                        //parse the zone/map pair
                        subparts = parts[0].Split('_');
                        if (subparts.Length != 2)
                            continue;

                        zoneid = System.Convert.ToInt16(subparts[0], 16);
                        if (!int.TryParse(subparts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out mapid))
                            continue;

                        //parse the calc and range data
                        subline = parts[1];
                        idx = subline.IndexOf(';');
                        if (idx > -1)
                            subline = subline.Substring(0, idx); //kill comments following the data area
                        subline = subline.Trim();
                        subparts = subline.Split(',');
                        if (subparts.Length == 0 || ((subparts.Length - 4) % 6 != 0)) //ranges are specified in vector pairs
                            continue;

                        //get the zone object
                        if (zonemaps == null || zonemaps.ZoneID != zoneid)
                        {
                            if (!m_zones.ContainsKey(zoneid))
                            {
                                zonemaps = new FFXIZoneMaps(this, zoneid);
                                m_zones.Add(zoneid, zonemaps);
                            }
                            else
                            {
                                zonemaps = m_zones[zoneid];
                            }
                        }

                        if (zonemaps.ContainsKey(mapid)) //only process the same zone/map combo once
                            continue;

                        //create the map object
                        map = new FFXIImageMap(zonemaps, mapid,
                           float.Parse(subparts[0], CultureInfo.InvariantCulture),
                           float.Parse(subparts[1], CultureInfo.InvariantCulture),
                           float.Parse(subparts[2], CultureInfo.InvariantCulture),
                           float.Parse(subparts[3], CultureInfo.InvariantCulture)
                        );

                        //add each range of values
                        int i = 4;
                        while (i < subparts.Length)
                        {
                            map.addRange(
                               float.Parse(subparts[i], CultureInfo.InvariantCulture),      //X1 
                               float.Parse(subparts[i + 2], CultureInfo.InvariantCulture),  //Y1 Y/Z swapped to standard vertex order.
                               float.Parse(subparts[i + 1], CultureInfo.InvariantCulture),  //Z1 Why must we perpetuate bad coordinate ordering?
                               float.Parse(subparts[i + 3], CultureInfo.InvariantCulture),  //X2 
                               float.Parse(subparts[i + 5], CultureInfo.InvariantCulture),  //Y2 Y/Z swapped here too.
                               float.Parse(subparts[i + 4], CultureInfo.InvariantCulture)   //Z2
                            );
                            i += 6;
                        }
                        zonemaps.Add(map);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("error reading line " + linenumber + ": " + ex.Message);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoadMapData ERROR: " + ex.Message);
            }
        }
    }

    /// <summary>A collection of image maps that belong to a given zone.</summary>
    public class FFXIZoneMaps
    {
        FFXIImageMaps m_parent;
        Dictionary<int, FFXIImageMap> m_maps;
        int zoneid;

        /// <summary>Gets the zone id of this collection.</summary>
        public int ZoneID
        {
            get { return zoneid; }
        }

        /// <summary>Gets the parent map collection of this zone.</summary>
        public FFXIImageMaps Maps
        {
            get { return m_parent; }
        }

        public FFXIZoneMaps(FFXIImageMaps parent, int zoneid)
        {
            m_maps = new Dictionary<int, FFXIImageMap>();
            m_parent = parent;
            this.zoneid = zoneid;
        }

        /// <summary>Adds the image map to the zone.</summary>
        public void Add(FFXIImageMap map)
        {
            m_maps.Add(map.MapID, map);
        }

        /// <summary>Determines if the zone id is contained in the collection.</summary>
        public bool ContainsKey(int key)
        {
            return m_maps.ContainsKey(key);
        }

        /// <summary>Determines if the given map is bound to the zone.</summary>
        public bool ContainsValue(FFXIImageMap map)
        {
            return m_maps.ContainsValue(map);
        }

        public Dictionary<int, FFXIImageMap>.Enumerator GetEnumerator()
        {
            return m_maps.GetEnumerator();
        }

        /// <summary>Gets the specified map from the zone collection.</summary>
        public FFXIImageMap this[int key]
        {
            get { return m_maps[key]; }
        }

        /// <summary>Gets the currently applicable zone map.</summary>
        public FFXIImageMap GetCurrentMap(MapPoint point)
        {
            foreach (KeyValuePair<int, FFXIImageMap> pair in m_maps)
            {
                if (pair.Value.Contains(point))
                    return (pair.Value);
            }
            return null;
        }

        /// <summary>Gets a list of map id's registered in the zone</summary>
        public List<FFXIImageMap> MapList
        {
            get { return new List<FFXIImageMap>(m_maps.Values); }
        }

        /// <summary>Releases cached image data for the zone.</summary>
        public void ClearCache()
        {
            foreach (KeyValuePair<int, FFXIImageMap> pair in m_maps)
                pair.Value.ClearCache();
        }

        /// <summary>Creates a new map with the given id and scale</summary>
        public FFXIImageMap Create(int ID, float scale)
        {
            return Create(ID, scale, 0, 0);
        }
        /// <summary>Creates a new map with the given id, scale, and offset</summary>
        public FFXIImageMap Create(int ID, float scale, float x, float y)
        {
            if (m_maps.ContainsKey(ID))
                return m_maps[ID];
            FFXIImageMap map = new FFXIImageMap(this, ID, scale, x, -scale, y);
            Add(map);
            return map;
        }

        /// <summary>Remove the map with the given id from the zone collection.</summary>
        public void Remove(int ID)
        {
            if (m_maps.ContainsKey(ID))
                m_maps.Remove(ID);
        }

        /// <summary>Returns the next available map id not currently in use within the zone.</summary>
        public int GetFreeID()
        {
            List<int> list = new List<int>(m_maps.Keys);
            if (list.Count > 0)
            {
                list.Sort();
                int max = list[list.Count - 1];

                for (int i = 0; i <= max; i++)
                {
                    if (!m_maps.ContainsKey(i))
                        return i;
                }
                return max + 1;
            }
            return 0;
        }
    }

    /// <summary>Defines a single axis-aligned bounding box (AABB).</summary>
    public class FFXIImageMapRange
    {
        private float minX;
        private float maxX;
        private float minY;
        private float maxY;
        private float minZ;
        private float maxZ;
        private FFXIImageMap m_map;

        public FFXIImageMapRange(FFXIImageMap map, float x1, float y1, float z1, float x2, float y2, float z2)
        {
            //cache the mins/maxs
            m_map = map;
            SetRange(x1, y1, z1, x2, y2, z2);
        }

        public float Floor
        {
            get { return minZ; }
            set { minZ = value; }
        }
        public float Ceiling
        {
            get { return maxZ; }
            set { maxZ = value; }
        }
        public float Left
        {
            get { return minX; }
            set { minX = value; }
        }
        public float Top
        {
            get { return minY; }
            set { minY = value; }
        }
        public float Right
        {
            get { return maxX; }
            set { maxX = value; }
        }
        public float Bottom
        {
            get { return maxY; }
            set { maxY = value; }
        }
        public RectangleF Bounds
        {
            get { return RectangleF.FromLTRB(minX, minY, maxX, maxY); }
            set
            {
                minX = value.Left;
                minY = value.Top;
                maxX = value.Right;
                maxY = value.Bottom;
            }
        }

        public void SetRange(MapPoint p1, MapPoint p2)
        {
            SetRange(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z);
        }
        public void SetRange(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            minX = Math.Min(x1, x2);
            maxX = Math.Max(x1, x2);
            minY = Math.Min(y1, y2);
            maxY = Math.Max(y1, y2);
            minZ = Math.Min(z1, z2);
            maxZ = Math.Max(z1, z2);
        }

        /// <summary>Check the 3D point against the AABB</summary>
        public bool Contains(MapPoint point)
        {
            return Contains(point.X, point.Y, point.Z);
        }
        /// <summary>Check the 3D point against the AABB</summary>
        public bool Contains(float x, float y, float z)
        {
            return x <= maxX && x >= minX &&
                   y <= maxY && y >= minY &&
                   z <= maxZ && z >= minZ;
        }

        /// <summary>Check the 2D point against the AABB</summary>
        public bool Contains(PointF point)
        {
            return Contains(point.X, point.Y);
        }
        /// <summary>Check the 2D point against the AABB</summary>
        public bool Contains(float x, float y)
        {
            return x <= maxX && x >= minX &&
                   y <= maxY && y >= minY;
        }
    }

    /// <summary>Defines information about a given image map that is displayed in the client as a vector alternative.</summary>
    public class FFXIImageMap
    {
        FFXIZoneMaps m_parent;
        int m_mapid;
        float m_xScale;
        float m_xOffset;
        float m_yScale;
        float m_yOffset;
        List<FFXIImageMapRange> ranges;
        Image mapImage;
        bool loadAttempted = false;
        RectangleF bounds = RectangleF.Empty;

        public FFXIImageMap(FFXIZoneMaps parent, int mapid, float xScale, float xOffset, float yScale, float yOffset)
        {
            m_parent = parent;
            ranges = new List<FFXIImageMapRange>();
            m_mapid = mapid;
            m_xScale = xScale;
            m_xOffset = xOffset;
            m_yScale = yScale;
            m_yOffset = yOffset;
        }

        /// <summary>Adds a detection range. Ranges are axis aligned bounding boxes (AABB) that define the applicability of a given map.</summary>
        public void addRange(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            FFXIImageMapRange range = new FFXIImageMapRange(this, x1, y1, z1, x2, y2, z2);
            ranges.Add(range);
        }
        /// <summary>Adds a detection range. Ranges are axis aligned bounding boxes (AABB) that define the applicability of a given map.</summary>
        public void addRange(MapPoint p1, MapPoint p2)
        {
            addRange(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z);
        }

        /// <summary>Determines whether any of the defined ranges are applicable to the given MAP coordinate.</summary>
        public bool Contains(MapPoint point)
        {
            foreach (FFXIImageMapRange range in ranges)
            {
                if (range.Contains(point))
                    return true;
            }
            return false;
        }

        /// <summary>Gets the X zone scale.</summary>
        public float XScale
        {
            get { return m_xScale; }
        }
        /// <summary>Gets the X offset to place map from zone center.</summary>
        public float XOffset
        {
            get { return m_xOffset; }
        }
        /// <summary>Gets the Y zone scale.</summary>
        public float YScale
        {
            get { return m_yScale; }
        }
        /// <summary>Gets the Y offset to place map from zone center.</summary>
        public float YOffset
        {
            get { return m_yOffset; }
        }

        public List<FFXIImageMapRange>.Enumerator GetEnumerator()
        {
            return ranges.GetEnumerator();
        }

        public FFXIImageMapRange this[int index]
        {
            get { return ranges[index]; }
        }
        public int Count
        {
            get { return ranges.Count; }
        }

        public void SetMapLocation(float X, float Y)
        {
            m_xOffset = X;
            m_yOffset = Y;
            bounds = RectangleF.Empty; //force recalculation
        }
        public void SetMapLocation(float scale, float X, float Y)
        {
            SetMapLocation(scale, -scale, X, Y);
        }
        public void SetMapLocation(float Xscale, float Yscale, float X, float Y)
        {
            m_xScale = Xscale;
            m_yScale = Yscale;
            SetMapLocation(X, Y);
        }

        /// <summary>Gets the map id of this map image.</summary>
        public int MapID
        {
            get { return m_mapid; }
        }

        /// <summary>Translate a MAP coordinate into an IMAGE coordinate.</summary>
        public PointF Translate(MapPoint point)
        {
            return new PointF(
               (m_xOffset + (m_xScale * point.X)),
               (m_yOffset + (m_yScale * point.Y))
            );
        }

        /// <summary>Translate an IMAGE coordinate into a MAP coordinate.</summary>
        public MapPoint Translate(PointF point)
        {
            return new MapPoint(
               ((point.X - m_xOffset) / m_xScale),
               ((point.Y - m_yOffset) / m_yScale),
               0
            );
        }

        /// <summary>Retrieve the image boundaries in MAP coordinates.</summary>
        public RectangleF Bounds
        {
            get
            {
                //The bounds will never change, so only calculate it once (upon demand) and then cache it
                if (bounds == RectangleF.Empty)
                {
                    bounds = new RectangleF(
                       (-m_xOffset / m_xScale),
                       (-m_yOffset / m_yScale),
                       (512 / m_xScale) * 0.5f, //map is scaled by a factor of 2, so reduce the value by half
                       (512 / m_yScale) * 0.5f
                    );
                }
                return bounds;
            }
        }

        /// <summary>Gets the zone collection this map is part of.</summary>
        public FFXIZoneMaps Zone
        {
            get { return m_parent; }
        }

        /// <summary>Releases cached image data for the map.</summary>
        public void ClearCache()
        {
            mapImage = null;
            loadAttempted = false;
        }

        /// <summary>Loads and retrieves the map image.</summary>
        public Image GetImage()
        {
            if (mapImage == null && !loadAttempted)
            {
                try
                {
                    //search the map path for the zone image, using the list of supported extentions. take the first one found.
                    Bitmap tempImage = null;
                    for (int i = 0; i < m_parent.Maps.FileExtList.Length; i++)
                    {
                        //load the image file
                        string fullfilepath = m_parent.Maps.FilePath + m_parent.ZoneID.ToString("X2") + "_" + m_mapid + m_parent.Maps.FileExtList[i];
                        if (File.Exists(fullfilepath))
                        {
                            Debug.WriteLine("GetImage: loading " + fullfilepath);
                            tempImage = new Bitmap(fullfilepath);
                            break;
                        }
                    }
                    if (tempImage != null)
                    {
                        //if the image is not already at 32 alpha, then convert it. This fixes alpha issues when running on win 7
                        if (tempImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                        {
                            Bitmap newImage = new Bitmap(tempImage.Width, tempImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                            newImage.SetResolution(tempImage.HorizontalResolution, tempImage.VerticalResolution);
                            Graphics g = Graphics.FromImage(newImage);
                            g.DrawImage(tempImage, 0, 0);
                            g.Dispose();
                            mapImage = newImage;
                        }
                        else
                        {
                            mapImage = tempImage;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("GetImage: no suitable image could be found for zone " + m_parent.ZoneID.ToString("X2"));
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("GetImage ERROR: " + ex.Message);
                }
                //only attempt to load once in case the image file is missing.
                loadAttempted = true;
            }
            return mapImage;
        }

        public override string ToString()
        {
            return m_mapid.ToString();
        }
    }
}
