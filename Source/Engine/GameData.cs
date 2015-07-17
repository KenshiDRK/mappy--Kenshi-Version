using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace MapEngine
{
    /// <summary>A container for the current game state.</summary>
    public class GameData
    {
        /// <summary>Fires when the game state has changed.</summary>
        public event GenericEvent Updated;

        public GameData(Engine engine)
        {
            Spawns = new GameSpawns(this);
            Player = null;
            Target = null;
            Engine = engine;
        }

        /// <summary>Gets the array of spawns currently associated with the process.</summary>
        public GameSpawns Spawns { get; private set; }

        /// <summary>Gets the engine bound to this data pool.</summary>
        public Engine Engine { get; private set; }

        /// <summary>Gets or sets the spawn that is recognized to be the logged in player.</summary>
        public GameSpawn Player { get; set; }

        /// <summary>Gets or sets the spawn that is recognized to be the current target.</summary>
        public GameSpawn Target { get; set; }
        /// <summary>Gets or sets the spawn that is selected.</summary>
        public GameSpawn Selected { get; set; }
        /// <summary>Gets or sets the spawn that is highlighed.</summary>
        public GameSpawn Highlighted { get; set; }

        /// <summary>Clears all spawn information</summary>
        public void Clear()
        {
            Spawns.Clear();
            Target = null;
            Player = null;
            Selected = null;
            Highlighted = null;
            Update();
        }

        /// <summary>Forces each spawn to update itself and then causes the map to redraw.</summary>
        public void Update()
        {
            foreach (KeyValuePair<uint, GameSpawn> spawn in Spawns)
            {
                //force the spawn to update itself
                spawn.Value.Update();

                //check to make sure the spawn is within the map boundary. if not, then expand it
                if (!spawn.Value.Hidden || Engine.ShowHiddenSpawns)
                    Engine.Data.CheckBatch(spawn.Value.Location.X, spawn.Value.Location.Y);
            }
            //force the boundary to recalculate now that were done checking them all
            Engine.Data.CheckBatchEnd();

            //notify the parent control that we need to refresh the map
            if (Updated != null)
                Updated();
        }

        /// <summary>
        /// Sets the player spawn by its ID
        /// </summary>
        /// <param name="ID">The ID of the spawn</param>
        /// <param name="batch">If true, the map will not automatically update</param>
        public void setPlayer(uint ID, bool batch)
        {
            if (Player != null && Player.ID == ID)
                return;
            if (Spawns.ContainsIndex(ID))
                Player = Spawns[ID];
            if (!batch && Updated != null)
                Updated();
        }

        /// <summary>
        /// Sets the target spawn by its ID
        /// </summary>
        /// <param name="ID">The ID of the spawn</param>
        /// <param name="batch">If true, the map will not automatically update</param>
        public void setTarget(uint ID, bool batch)
        {
            if (Target != null && Target.ID == ID)
                return;

            if (ID == 0)
            {
                Target = null;
            }
            else if (Spawns.ContainsIndex(ID))
            {
                Target = Spawns[ID];
            }

            if (!batch && Updated != null)
                Updated();
        }
    }

    /// <summary>A collection of game spawns.</summary>
    public class GameSpawns
    {
        private Dictionary<uint, GameSpawn> Spawns;
        private GameData m_data;

        public GameSpawns(GameData data)
        {
            Spawns = new Dictionary<uint, GameSpawn>();
            m_data = data;
        }

        /// <summary>Gets the spawn based on its ID.</summary>
        public GameSpawn this[uint idx]
        {
            get { return Spawns[idx]; }
        }

        /// <summary>Determines if a spawn exists in the collection with the given ID.</summary>
        public bool ContainsIndex(uint ID)
        {
            return Spawns.ContainsKey(ID);
        }

        public Dictionary<uint, GameSpawn>.Enumerator GetEnumerator()
        {
            return Spawns.GetEnumerator();
        }

        /// <summary>Clears all spawn data.</summary>
        public void Clear()
        {
            Spawns.Clear();
        }

        /// <summary>Gets the number of spawns in the collection.</summary>
        public int Count
        {
            get { return Spawns.Count; }
        }

        /// <summary>Adds the spawn to the collection.</summary>
        public void Add(GameSpawn spawn)
        {
            if (!Spawns.ContainsKey(spawn.ID))
            {
                Spawns.Add(spawn.ID, spawn);
                m_data.Engine.Data.Hunts.Bind(spawn);
                m_data.Engine.Data.Replacements.Bind(spawn);
            }
        }

        /// <summary>
        /// Attempts to find the spawn at the specified MAP coordinate.
        /// </summary>
        /// <param name="x">The X MAP coordinate</param>
        /// <param name="y">The Y MAP coordinate</param>
        /// <param name="threshhold">Search tolerance</param>
        /// <returns>The closest spawn to the specified map coordinates, or null if there are no spawns within the search tolerance.</returns>
        public GameSpawn FindSpawn(float x, float y, float threshhold)
        {
            float closestDistance = -1;
            GameSpawn closestSpawn = null;

            foreach (KeyValuePair<uint, GameSpawn> pair in Spawns)
            {
                if (!pair.Value.Hidden || m_data.Engine.ShowHiddenSpawns)
                {
                    //calculate the distance between where the mouse cursor is and the center of the spawn
                    float distance = CalcDistance(pair.Value, x, y);

                    //if this is currently the closest then cache the spawn
                    if (closestDistance < 0 || distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestSpawn = pair.Value;
                    }
                }
            }

            //if closest spawn is within the requested threshold then return it
            if (closestDistance > 0 && closestDistance < threshhold)
                return closestSpawn;
            return null;
        }

        /// <summary>Calculate the distance between a spawn and an arbitrary set of MAP coordinates.</summary>
        public float CalcDistance(GameSpawn source, float x, float y)
        {
            return CalcDistance(source.Location.X, source.Location.Y, x, y);
        }

        /// <summary>Calculate the distance between two spawns</summary>
        public float CalcDistance(GameSpawn source, GameSpawn dest)
        {
            return CalcDistance(source.Location.X, source.Location.Y, dest.Location.X, dest.Location.Y);
        }

        /// <summary>Calculate the distance between two MAP points</summary>
        public float CalcDistance(float x1, float y1, float x2, float y2)
        {
            float s1 = x1 - x2;
            float s2 = y1 - y2;
            return (float)Math.Sqrt(s1 * s1 + s2 * s2); //go go pythagorean theorem
        }
    }

    public enum SpawnType : byte
    {
        Player = 0x01,
        NPC = 0x02,
        MOB = 0x10,
        Hidden = 0x03
    }

    /// <summary>This is a base prototype intended to be extended and used by game specific containers</summary>
    public class GameSpawn
    {
        protected GameSpawn() : this(0) { }
        public GameSpawn(uint ID)
        {
            Name = "";
            Heading = 0;
            Distance = 0;
            Speed = 0;
            Type = SpawnType.Hidden;
            HealthPercent = 0;
            Level = 0;
            Dead = false;
            Hidden = true;
            InCombat = false;
            Alert = false;
            Hunt = false;
            Replacement = false;
            RepName = "";
            Icon = null;
            FillColor = Color.Black;
            ClaimID = 0;
			TargetIndex = 0;
            PetIndex = 0;
            GroupMember = false;
            RaidMember = false;
            DEBUG = "";
            DEBUGHOVER = "";
            Attackable = false;
            this.ID = ID;
            Location = new MapPoint();
        }
        public uint ID { get; protected set; }
        public string Name { get; protected set; }
        public MapPoint Location { get; private set; }
        public float Heading { get; protected set; }
        public float Distance { get; protected set; }
        public float Speed { get; protected set; }
        public bool Dead { get; protected set; }
        public bool InCombat { get; protected set; }
        public bool Alert { get; set; }
        public bool Hunt { get; set; }
        public bool Replacement { get; set; }
        public string RepName { get; set; }
        public bool Hidden { get; protected set; }
        public SpawnType Type { get; protected set; }
        public int Level { get; protected set; }
        public int HealthPercent { get; protected set; }
        public Image Icon { get; protected set; }
        public Color FillColor { get; protected set; }
        public uint ClaimID { get; set; }
		public uint TargetIndex { get; set; }
        public uint PetIndex { get; set; }
        public bool RaidMember { get; set; }
        public bool GroupMember { get; set; }
        public bool Attackable { get; protected set; }
        public string DEBUG { get; set; }
        public string DEBUGHOVER { get; set; }
        //prototype for derived classes
        public virtual void Update() { }
    }
}