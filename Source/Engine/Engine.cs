using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

////////////////////////////////////////////////////////////////////////////////////
// Portions of this code is based on the ShowEQ & MySEQ project
// http://sourceforge.net/projects/seq/
////////////////////////////////////////////////////////////////////////////////////
namespace MapEngine
{
    public delegate void GenericEvent();

    public enum MapSpawnShowInfo
    {
        Names,
        HealthPercent,
        Distance
    }

    public partial class Engine : Component
    {
        //private storage
        private Rectangle clientRect = new Rectangle(0, 0, 100, 100);
        private PointF clientCenter = new PointF(300, 300);
        private PointF mapCenter = new PointF(0, 0);
        private PointF mapViewOffset = new PointF(0, 0);
        private float[] zoomTable = { 1, .6666f, .5f, .3333f, .25f, .1666f, .125f, .0833f, .0625f, .05f, .04f, .03f, .02f, .01f };

        //propery storage
        private MapData m_data = null;
        private GameData m_game = null;
        private float m_zoom = 1.0f;
        private float m_ratio = 1.0f;
        private Color m_gridColorTick = Color.FromArgb(225, 200, 75);
        private bool m_showPlayerPosition = true;
        private Font m_gridFontTick = SystemFonts.DefaultFont;
        private Color m_gridColorLine = Color.FromArgb(75, 200, 75);
        private int m_gridResolution = 100;
        private bool m_showGridTicks = true;
        private bool m_showGridLines = true;
        private Font m_labelFont = SystemFonts.DefaultFont;
        private bool m_showLines = true;
        private bool m_showSpawns = true;
        private bool m_showHiddenSpawn = false;
        private bool m_showLabels = true;
        private bool m_showRadarRange = true;
        private bool m_scaleThickness = false;
        private bool m_showAltMap = true;
        private bool m_showAltAlways = false;
        private bool m_autoRangeSnap = true;
        private bool m_depthFilterSpawn = false;
        private bool m_depthFilterLines = false;
        private float m_depthDistance = 2f;
        private float m_depthCutoff = 5f;
        private int m_depthMinAlpha = 10;
        private int m_depthMaxAlpha = 255;
        private bool m_depthUseAlpha = true;
        private bool m_drawYOUFill = true;
        private bool m_drawTextOutline = true;
        private bool m_drawTextShadow = false;
        private bool m_drawAlertRanges = true;
        private bool m_drawClaimLines = true;
        private bool m_drawPetLines = true;
        private bool m_drawHuntLines = true;
        private bool m_usePlayerColor = false;
        private bool m_drawHeadings = true;

        private Color m_playerColor = Color.Purple;
        private Color m_playerTextColor = Color.Purple;
        private Font m_playerTextFont = SystemFonts.DefaultFont;
        private float m_playerSize = 4.0f;
        private Color m_NPCColor = Color.Green;
        private Color m_NPCTextColor = Color.Green;
        private Font m_NPCTextFont = SystemFonts.DefaultFont;
        private float m_NPCSize = 4.0f;
        private Color m_MOBColor = Color.Red;
        private Color m_MOBTextColor = Color.Red;
        private Font m_MOBTextFont = SystemFonts.DefaultFont;
        private float m_MOBSize = 4.0f;

        private float m_spawnMemberSize = 2.0f; //added to current size when player is a group or raid member
        private float m_spawnSelectSize = 4.0f; //added to current size when selected
        private float m_huntSize = 4.0f; //added to current size when in alert state
        private float m_alertSize = 8.0f; //added to current size when in alert state
        private float m_radarRange = 50f;  //fixed value representing the max distance from player to outer update range (RADIUS)

        private Color m_SelectedOutColor = Color.FromArgb(15, Color.Red);
        private Color m_GroupMemColor = Color.Cyan;
        private Color m_RaidMemColor = Color.DarkCyan;
        private Color m_YOUFill = Color.Black;

        private string m_infoTemplate = "{name} {hpp}%{br}{distance}";
        private bool m_infoAllPlayers = false;
        private bool m_infoAllNPCs = false;
        private bool m_infoAllEnemies = false;
        private Regex rxInfoName = new Regex("{name}");
        private Regex rxInfoHpp = new Regex("{hpp}");
        private Regex rxInfoDistance = new Regex("{distance}");
        private Regex rxNewLine = new Regex("\\\\n|{br}");
        private Regex rxInfoID = new Regex("{id}");

        //cached pens of various user settings
        private Pen pRangeFinder = new Pen(Color.White);
        private Pen pAlertRing = new Pen(Color.Red);
        private Pen pHuntChain = new Pen(Color.Green);
        private Pen pHuntLockedChain = new Pen(Color.DarkGreen);
        private Pen pClaimChain = new Pen(Color.Red);
        private Pen pPetChain = new Pen(Color.Cyan);
        private Pen pSelected = new Pen(Color.White);
        private Pen pYOU = new Pen(Color.White);
        private Pen pHeading = new Pen(Color.White);

        //cached pens used only internally
        private Pen pCachePlayer = new Pen(Color.Black);
        private Pen pCacheMapLine;
        private Pen pCacheSpawnBorder = new Pen(Color.White);

        //cached brushes
        private Brush bPlayer = new SolidBrush(Color.Purple);
        private Brush bNPC = new SolidBrush(Color.Green);
        private Brush bMOB = new SolidBrush(Color.Red);
        private Brush bHidden = new SolidBrush(Color.Gray);
        private Brush bSelectedOutline = new SolidBrush(Color.FromArgb(15, Color.Red));
        private Brush bSelected = new SolidBrush(Color.White);
        private Brush bYOUFill = new SolidBrush(Color.Black);
        private Brush bTextPlayer = new SolidBrush(Color.Purple);
        private Brush bTextNPC = new SolidBrush(Color.Green);
        private Brush bTextMOB = new SolidBrush(Color.Red);

        private Image m_MapAltImage = null;
        private RectangleF m_MapAltBounds;
        private float m_MapAltOpacity = 0.4f;
        private ColorMatrix m_MapAltMatrix;
        private ImageAttributes m_MapAltAttr;
        private PointF m_LocInImage; /// IHM EDIT

        private const int WM_NCHITTEST = 0x0084;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int HTTRANSPARENT = -1;
        private const int WS_EX_TRANSPARENT = 0x020;
        private const int WS_EX_COMPOSITED = 0x02000000;

        /// <summary>Fires when the map has been updated and needs to be rendered.</summary>
        public event GenericEvent Updated;

        /// <summary>Creates a new engine.</summary>
        public Engine()
        {
            CommonInitialization();
        }
        /// <summary>Creates a new engine adds it to another container.</summary>
        public Engine(IContainer container)
        {
            container.Add(this);
            CommonInitialization();
        }

        private void CommonInitialization()
        {
            InitializeComponent();
            m_data = new MapData(this);
            m_data.DataChanged += new GenericEvent(m_data_DataChanged);
            m_game = new GameData(this);
            m_game.Updated += new GenericEvent(m_game_Updated);
            m_MapAltMatrix = new ColorMatrix();
            m_MapAltMatrix.Matrix33 = m_MapAltOpacity;
            m_MapAltAttr = new ImageAttributes();
            m_MapAltAttr.SetColorMatrix(m_MapAltMatrix);
            pCacheMapLine = new Pen(Color.White, m_scaleThickness ? 1 : 0);
        }

        //====================================================================================================
        // Designable Properties
        //====================================================================================================
        [Category("Map")]
        [Description("Gets or sets the current zoom factor.")]
        [DefaultValue(1f)]
        public float Zoom
        {
            get { return m_zoom; }
            set
            {
                m_zoom = value;
                if (m_zoom < .01)
                    m_zoom = .01f;
                if (m_zoom > 32)
                    m_zoom = 32;
                UpdateMap();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether the map lines are scaled when zooming.")]
        [DisplayName("Scale Lines")]
        [DefaultValue(false)]
        public bool ScaleLines
        {
            get { return m_scaleThickness; }
            set
            {
                m_scaleThickness = value;
                pCacheMapLine = new Pen(pCacheMapLine.Color, m_scaleThickness ? 1 : 0);
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether the vector map is rendered.")]
        [DisplayName("Show Lines")]
        [DefaultValue(true)]
        public bool ShowLines
        {
            get { return m_showLines; }
            set
            {
                m_showLines = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether to spawns are rendered at all.")]
        [DisplayName("Show Spawns")]
        [DefaultValue(true)]
        public bool ShowSpawns
        {
            get { return m_showSpawns; }
            set
            {
                m_showSpawns = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether to display spawns that would not otherwise be rendered.")]
        [DisplayName("Show Hidden Spawns")]
        [DefaultValue(false)]
        public bool ShowHiddenSpawns
        {
            get { return m_showHiddenSpawn; }
            set
            {
                m_showHiddenSpawn = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or Sets whether to display the spawn update range. Modern online games limit spawn updates to only a certain circumference around the player. When true, this limit is displayed as a ring around the player. Use RadarRange to set the distance for your game.")]
        [DisplayName("Show Radar Range")]
        [DefaultValue(true)]
        public bool ShowRadarRange
        {
            get { return m_showRadarRange; }
            set
            {
                m_showRadarRange = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color used to display the radar range.")]
        [DisplayName("Radar Range Color")]
        public Color RadarRangeColor
        {
            get { return pRangeFinder.Color; }
            set
            {
                pRangeFinder.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether to enable alert rings for spawns that enable the alert status.")]
        [DisplayName("Show Alert Ranges")]
        [DefaultValue(true)]
        public bool ShowAlertRanges
        {
            get { return m_drawAlertRanges; }
            set
            {
                m_drawAlertRanges = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color used to display the alert ranges.")]
        [DisplayName("Alert Range Color")]
        public Color AlertRangeColor
        {
            get { return pAlertRing.Color; }
            set
            {
                pAlertRing.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or Sets the spawn update distance. Modern online games limit spawn updates to only a certain circumference around the player. Use ShowRadarRange to enable or disable the display of this value.")]
        [DisplayName("Radar Range")]
        [DefaultValue(50f)]
        public float RadarRange
        {
            get { return m_radarRange; }
            set
            {
                m_radarRange = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or Sets the amount added to an entity's diameter when it is selected by the user.")]
        [DisplayName("Spawn Select Size")]
        [DefaultValue(4.0f)]
        public float SpawnSelectSize
        {
            get { return m_spawnSelectSize; }
            set
            {
                m_spawnSelectSize = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or Sets the amount added to an players's diameter when in the same group or raid as the user.")]
        [DisplayName("Spawn Group Member Size")]
        [DefaultValue(4.0f)]
        public float SpawnGroupMemberSize
        {
            get { return m_spawnMemberSize; }
            set
            {
                m_spawnMemberSize = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or Sets the amount added to an entity's diameter when it matches a hunt filter.")]
        [DisplayName("Spawn Hunt Size")]
        [DefaultValue(4.0f)]
        public float SpawnHuntSize
        {
            get { return m_huntSize; }
            set
            {
                m_huntSize = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Grid")]
        [Description("Gets or sets whether the grid lines are shown.")]
        [DisplayName("Show Lines")]
        [DefaultValue(true)]
        public bool ShowGridLines
        {
            get { return m_showGridLines; }
            set
            {
                m_showGridLines = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Grid")]
        [Description("Gets or sets whether the grid tick marks are shown.")]
        [DisplayName("Show Ticks")]
        [DefaultValue(true)]
        public bool ShowGridTicks
        {
            get { return m_showGridTicks; }
            set
            {
                m_showGridTicks = value;
                if (Updated != null)
                    Updated();
            }
        }


        [Category("Grid")]
        [Description("Gets or sets the font used to render the grid tick marks.")]
        [DisplayName("Tick Font")]
        public Font GridTickFont
        {
            get { return m_gridFontTick; }
            set
            {
                m_gridFontTick = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Grid")]
        [Description("Gets or sets the color of the grid lines.")]
        [DisplayName("Line Color")]
        public Color GridLineColor
        {
            get { return m_gridColorLine; }
            set
            {
                m_gridColorLine = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Grid")]
        [Description("Gets or sets the color of the grid tick marks.")]
        [DisplayName("Tick Color")]
        public Color GridTickColor
        {
            get { return m_gridColorTick; }
            set
            {
                m_gridColorTick = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Grid")]
        [Description("Gets or sets whether the players position should be shown.")]
        [DisplayName("Show Player Position")]
        [DefaultValue(true)]
        public bool ShowPlayerPosition
        {
            get { return m_showPlayerPosition; }
            set
            {
                m_showPlayerPosition = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Grid")]
        [Description("Gets or sets the spacing between grid lines.")]
        [DisplayName("Grid Size")]
        [DefaultValue(100)]
        public int GridSize
        {
            get { return m_gridResolution; }
            set
            {
                m_gridResolution = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets if map labels are rendered to the grpahics buffer.")]
        [DisplayName("Show Labels")]
        [DefaultValue(true)]
        public bool ShowLabels
        {
            get { return m_showLabels; }
            set { m_showLabels = value; }
        }

        [Category("Map")]
        [Description("Gets or sets the color of NPC spawns.")]
        [DisplayName("NPC Color")]
        public Color NPCColor
        {
            get { return m_NPCColor; }
            set
            {
                m_NPCColor = value;
                bNPC = new SolidBrush(value);
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color of Enemy spawns.")]
        [DisplayName("MOB Color")]
        public Color MOBColor
        {
            get { return m_MOBColor; }
            set
            {
                m_MOBColor = value;
                bMOB = new SolidBrush(value);
            }
        }

        [Category("Map")]
        [Description("Gets or sets the font used to display player spawn information.")]
        [DisplayName("Player Font")]
        public Font PlayerInfoFont
        {
            get { return m_playerTextFont; }
            set
            {
                m_playerTextFont = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color used to display player spawn information.")]
        [DisplayName("Player Info Color")]
        public Color PlayerInfoColor
        {
            get { return m_playerTextColor; }
            set
            {
                m_playerTextColor = value;
                bTextPlayer = new SolidBrush(value);
            }
        }

        [Category("Map")]
        [Description("Gets or Sets the base rendering size of a player spawn.")]
        [DisplayName("Player Size")]
        [DefaultValue(4.0f)]
        public float PlayerSize
        {
            get { return m_playerSize; }
            set
            {
                m_playerSize = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the font used to display NPC spawn information.")]
        [DisplayName("NPC Font")]
        public Font NPCInfoFont
        {
            get { return m_NPCTextFont; }
            set
            {
                m_NPCTextFont = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color used to display NPC spawn information.")]
        [DisplayName("NPC Info Color")]
        public Color NPCInfoColor
        {
            get { return m_NPCTextColor; }
            set
            {
                m_NPCTextColor = value;
                bTextNPC = new SolidBrush(value);
            }
        }

        [Category("Map")]
        [Description("Gets or Sets the base rendering size of a NPC spawn.")]
        [DisplayName("NPC Size")]
        [DefaultValue(4.0f)]
        public float NPCSize
        {
            get { return m_NPCSize; }
            set
            {
                m_NPCSize = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the font used to display enemy spawn information.")]
        [DisplayName("MOB Font")]
        public Font MOBInfoFont
        {
            get { return m_MOBTextFont; }
            set
            {
                m_MOBTextFont = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color used to display enemy spawn information.")]
        [DisplayName("MOB Info Color")]
        public Color MOBInfoColor
        {
            get { return m_MOBTextColor; }
            set
            {
                m_MOBTextColor = value;
                bTextMOB = new SolidBrush(value);
            }
        }

        [Category("Map")]
        [Description("Gets or Sets the base rendering size of a MOB spawn.")]
        [DisplayName("MOB Size")]
        [DefaultValue(4.0f)]
        public float MOBSize
        {
            get { return m_MOBSize; }
            set
            {
                m_MOBSize = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the font used to display the map labels.")]
        [DisplayName("Label Font")]
        public Font LabelFont
        {
            get { return m_labelFont; }
            set
            {
                m_labelFont = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the outline color of selected spawn and its chain.")]
        [DisplayName("Selected Color")]
        public Color SelectedColor
        {
            get { return pSelected.Color; }
            set
            {
                pSelected.Color = value;
                bSelected = new SolidBrush(value);
            }
        }

        [Category("Map")]
        [Description("Gets or sets the base color of the glow effect for selected spawns.")]
        [DisplayName("Text Outline Color")]
        public Color TextOutlineColor
        {
            get { return m_SelectedOutColor; }
            set
            {
                m_SelectedOutColor = Color.FromArgb(15, value);
                bSelectedOutline = new SolidBrush(m_SelectedOutColor);
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether a glow effect is applied to selected spawns.")]
        [DisplayName("Text Outline Enabled")]
        [DefaultValue(true)]
        public bool TextOutlineEnabled
        {
            get { return m_drawTextOutline; }
            set
            {
                m_drawTextOutline = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether a text drop shadow is added to improve readability.")]
        [DisplayName("Text Shadow Enabled")]
        [DefaultValue(true)]
        public bool TextShadowEnabled
        {
            get { return m_drawTextShadow; }
            set
            {
                m_drawTextShadow = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the outline color of players that are a part of your current group.")]
        [DisplayName("Group Member Color")]
        public Color GroupMemberColor
        {
            get { return m_GroupMemColor; }
            set
            {
                m_GroupMemColor = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the outline color of players that are a part of your current raid.")]
        [DisplayName("Raid Member Color")]
        public Color RaidMemberColor
        {
            get { return m_RaidMemColor; }
            set
            {
                m_RaidMemColor = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color of the line that is drawn to spawns matching a hunt pattern.")]
        [DisplayName("Hunt Chain Color")]
        public Color HuntChainColor
        {
            get { return pHuntChain.Color; }
            set
            {
                pHuntChain.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color that overrides the hunt chain color when the spawn is unavailable to the player.")]
        [DisplayName("Hunt Locked Chain Color")]
        public Color HuntLockedChainColor
        {
            get { return pHuntLockedChain.Color; }
            set
            {
                pHuntLockedChain.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color of the line that is drawn between a claimed mob and its claimee.")]
        [DisplayName("Claim Chain Color")]
        public Color ClaimChainColor
        {
            get { return pClaimChain.Color; }
            set
            {
                pClaimChain.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color of the line that is drawn between a pet and its owner.")]
        [DisplayName("Pet Chain Color")]
        public Color PetChainColor
        {
            get { return pPetChain.Color; }
            set
            {
                pPetChain.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether info text is displayed at all times for all players in range.")]
        [DisplayName("Show All Player Info")]
        [DefaultValue(false)]
        public bool ShowAllPlayerInfo
        {
            get { return m_infoAllPlayers; }
            set
            {
                m_infoAllPlayers = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether info text is displayed at all times for all NPCs in range.")]
        [DisplayName("Show All NPC Info")]
        [DefaultValue(false)]
        public bool ShowAllNPCInfo
        {
            get { return m_infoAllNPCs; }
            set
            {
                m_infoAllNPCs = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets whether info text is displayed at all times for all Enemies in range.")]
        [DisplayName("Show All Enemy Info")]
        [DefaultValue(false)]
        public bool ShowAllEnemyInfo
        {
            get { return m_infoAllEnemies; }
            set
            {
                m_infoAllEnemies = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the template used to display the info text on the screen for select spawns.")]
        [DisplayName("Info Template")]
        public string InfoTemplate
        {
            get { return m_infoTemplate; }
            set
            {
                m_infoTemplate = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets the image that serves as an alternative to the vector map.</summary>
        [Browsable(false)]
        [Description("Gets or sets the image that serves as an alternative to the vector map.")]
        public Image MapAlternativeImage
        {
            get { return m_MapAltImage; }
            set
            {
                m_MapAltImage = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets the boundaries of the map alternative in MAP coordinates.</summary>
        [Browsable(false)]
        [Description("Gets or sets the boundaries of the map alternative in MAP coordinates.")]
        public RectangleF MapAlternativeBounds
        {
            get { return m_MapAltBounds; }
            set
            {
                m_MapAltBounds = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets the opacity level of the map alternative.</summary>
        [Category("Map")]
        [Description("Gets or sets the opacity level of the map alternative.")]
        [DisplayName("Map Alt Opacity")]
        [DefaultValue(0.4f)]
        public float MapAlternativeOpacity
        {
            get { return m_MapAltOpacity; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 1)
                    value = 1;
                m_MapAltOpacity = value;
                m_MapAltMatrix.Matrix33 = value;
                m_MapAltAttr.SetColorMatrix(m_MapAltMatrix);

                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets whether the map alternative is enabled or not.</summary>
        [Category("Map")]
        [Description("Gets or sets whether the map alternative is enabled or not.")]
        [DisplayName("Show Alternative")]
        [DefaultValue(true)]
        public bool ShowMapAlternative
        {
            get { return m_showAltMap; }
            set
            {
                m_showAltMap = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets whether the map alternative is always shown when enabled or only shown when there is no vector map available.</summary>
        [Category("Map")]
        [Description("Gets or sets whether the map alternative is always shown when enabled or only shown when there is no vector map available.")]
        [DisplayName("Show Alternative Always")]
        [DefaultValue(false)]
        public bool ShowMapAlternativeAlways
        {
            get { return m_showAltAlways; }
            set
            {
                m_showAltAlways = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets whether the player range is autofit to the window after zoning.</summary>
        [Category("Map")]
        [Description("Gets or sets whether the player range is autofit to the window after zoning.")]
        [DisplayName("Auto Range Snap")]
        [DefaultValue(true)]
        public bool AutoRangeSnap
        {
            get { return m_autoRangeSnap; }
            set { m_autoRangeSnap = value; }
        }

        /// <summary>Gets or sets whether the vector map lines are depth filtered.</summary>
        [Category("Map")]
        [Description("Gets or sets whether the vector map lines are depth filtered.")]
        [DisplayName("Depth Filter Lines")]
        [DefaultValue(false)]
        public bool DepthFilterLines
        {
            get { return m_depthFilterLines; }
            set
            {
                m_depthFilterLines = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets whether the vector map spawns are depth filtered.</summary>
        [Category("Map")]
        [Description("Gets or sets whether the vector map spawns are depth filtered.")]
        [DisplayName("Depth Filter Spawns")]
        [DefaultValue(false)]
        public bool DepthFilterSpawns
        {
            get { return m_depthFilterSpawn; }
            set
            {
                m_depthFilterSpawn = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>Gets or sets whether depth filtered objects fade after crossing the distance threshold.</summary>
        [Category("Map")]
        [Description("Gets or sets whether depth filtered objects fade after crossing the distance threshold.")]
        [DisplayName("Depth Filter Alpha")]
        [DefaultValue(true)]
        public bool DepthFilterAlpha
        {
            get { return m_depthUseAlpha; }
            set
            {
                m_depthUseAlpha = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>When depth filtering with alpha, defines how far a map object must stray before it begins to fade.</summary>
        [Category("Map")]
        [Description("When depth filtering with alpha, defines how far a map object must stray before it begins to fade.")]
        [DisplayName("Depth Distance")]
        public float DepthDistance
        {
            get { return m_depthDistance; }
            set
            {
                m_depthDistance = value;
                if (Updated != null)
                    Updated();
            }
        }

        /// <summary>When depth filtering, defines how far a map object must stray before it is no longer displayed.</summary>
        [Category("Map")]
        [Description("When depth filtering, defines how far a map object must stray before it is no longer displayed.")]
        [DisplayName("Depth Cutoff")]
        public float DepthCutoff
        {
            get { return m_depthCutoff; }
            set
            {
                m_depthCutoff = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("When depth filtering with alpha turned on, defines absolute minimum alpha value to render lines in.")]
        [DisplayName("Depth Alpha Minimum")]
        public int DepthAlphaMin
        {
            get { return m_depthMinAlpha; }
            set
            {
                m_depthMinAlpha = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("When depth filtering with alpha turned on, defines absolute maximum alpha value to render lines in.")]
        [DisplayName("Depth Alpha Maximum")]
        public int DepthAlphaMax
        {
            get { return m_depthMaxAlpha; }
            set
            {
                m_depthMaxAlpha = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("When enabled, a background is filled in behind the main player spawn.")]
        [DisplayName("Main Player Fill Enabled")]
        [DefaultValue(true)]
        public bool MainPlayerFillEnabled
        {
            get { return m_drawYOUFill; }
            set
            {
                m_drawYOUFill = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the color of the main player spawn.")]
        [DisplayName("Main Player Color")]
        public Color MainPlayerColor
        {
            get { return pYOU.Color; }
            set
            {
                pYOU.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("Gets or sets the background fill color of the main player spawn.")]
        [DisplayName("Main Player Fill Color")]
        public Color MainPlayerFillColor
        {
            get { return m_YOUFill; }
            set
            {
                m_YOUFill = value;
                bYOUFill = new SolidBrush(value);
            }
        }

        [Category("Map")]
        [Description("When enabled lines connecting a claimed enemy and its owner is displayed.")]
        [DisplayName("Show Claim Lines")]
        [DefaultValue(true)]
        public bool ShowClaimLines
        {
            get { return m_drawClaimLines; }
            set
            {
                m_drawClaimLines = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("When enabled lines connecting a pet and its owner is displayed.")]
        [DisplayName("Show Pet Lines")]
        [DefaultValue(true)]
        public bool ShowPetLines
        {
            get { return m_drawPetLines; }
            set
            {
                m_drawPetLines = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [Description("When enabled lines connecting the main player to a matching hunt is displayed.")]
        [DisplayName("Show Hunt Lines")]
        [DefaultValue(true)]
        public bool ShowHuntLines
        {
            get { return m_drawHuntLines; }
            set
            {
                m_drawHuntLines = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [DisplayName("Player Color")]
        [Description("Gets or sets the color of player spawns.")]
        public Color PlayerColor
        {
            get { return m_playerColor; }
            set
            {
                if (m_playerColor != value)
                {
                    m_playerColor = value;
                    if (m_usePlayerColor)
                    {
                        pCachePlayer.Color = value;
                        bPlayer = pCachePlayer.Brush;
                    }
                }
            }
        }

        [Category("Map")]
        [DisplayName("Use Player Color")]
        [Description("Gets or sets whether the player is drawn with the user specified color or if it is managed automatically by the game.")]
        [DefaultValue(false)]
        public bool UsePlayerColor
        {
            get { return m_usePlayerColor; }
            set
            {
                if (m_usePlayerColor != value)
                {
                    m_usePlayerColor = value;
                    if (m_usePlayerColor)
                    {
                        pCachePlayer.Color = m_playerColor;
                        bPlayer = pCachePlayer.Brush;
                    }
                }
            }
        }

        [Category("Map")]
        [DisplayName("Draw Headings")]
        [Description("Gets or sets whether heading lines are drawn for all spawns.")]
        [DefaultValue(false)]
        public bool DrawHeadings
        {
            get { return m_drawHeadings; }
            set
            {
                m_drawHeadings = value;
                if (Updated != null)
                    Updated();
            }
        }

        [Category("Map")]
        [DisplayName("Heading Color")]
        [Description("Gets or sets the color of the spawn heading line.")]
        public Color HeadingColor
        {
            get { return pHeading.Color; }
            set
            {
                pHeading.Color = value;
                if (Updated != null)
                    Updated();
            }
        }

        //====================================================================================================
        // Runtime Properties
        //====================================================================================================
        /// <summary>Gets or sets the center of the current viewing area.</summary>
        [Browsable(false)]
        [Description("Gets or sets the center of the current viewing area.")]
        public PointF ViewLocation
        {
            get { return mapViewOffset; }
            set
            {
                mapViewOffset = value;
                UpdateMap();
            }
        }

        /// <summary>Gets the data container the map is rendered from.</summary>
        [Browsable(false)]
        [Description("Gets the data container the map is rendered from.")]
        public MapData Data
        {
            get { return m_data; }
        }

        /// <summary>Gets the data container the spawn information is rendered from.</summary>
        [Browsable(false)]
        [Description("Gets the data container the spawn information is rendered from.")]
        public GameData Game
        {
            get { return m_game; }
        }

        /// <summary>Gets or Sets the current client dimensions of the rendering surface.</summary>
        [Browsable(false)]
        [Description("Gets or Sets the current client dimensions of the rendering surface.")]
        public Rectangle ClientRectangle
        {
            get { return clientRect; }
            set
            {
                clientRect = value;
                UpdateMap();
            }
        }

        [Browsable(false)]
        [Description("Gets the current map scale that may be used with drawing operations")]
        public float Scale
        {
            get { return m_ratio; }
        }

        /// IHM EDIT
        /// <summary>Gets or sets the current location in IMAGE coordinates.</summary>
        [Browsable(false)]
        [Description("Gets or sets the current location in IMAGE coordinates.")]
        public PointF LocInImage
        {
            get { return m_LocInImage; }
            set
            {
                m_LocInImage = value;
            }
        }

        //====================================================================================================
        // Methods
        //====================================================================================================
        /// <summary>Reset the viewport to the center of the map</summary>
        public void CenterView()
        {
            mapViewOffset.X = 0;
            mapViewOffset.Y = 0;
            UpdateMap();
        }

        /// <summary>Zoom in one logical level.</summary>
        public void ZoomIn()
        {
            if (m_zoom > 1)
            {
                if (m_zoom % 1 > 0)
                {
                    Zoom = (float)Math.Floor(m_zoom);
                }
                else
                {
                    Zoom = Zoom - 1f;
                }
            }
            else
            {
                for (int i = 0; i < zoomTable.Length; i++)
                {
                    if (zoomTable[i] < m_zoom)
                    {
                        Zoom = zoomTable[i];
                        break;
                    }
                }
            }
        }

        /// <summary>Zoom out one logical level.</summary>
        public void ZoomOut()
        {
            if (m_zoom < 1)
            {
                for (int i = zoomTable.Length - 1; i > -1; i--)
                {
                    if (zoomTable[i] > m_zoom)
                    {
                        Zoom = zoomTable[i];
                        break;
                    }
                }
            }
            else
            {
                if (m_zoom % 1 > 0)
                {
                    Zoom = (float)Math.Ceiling(m_zoom);
                }
                else
                {
                    Zoom = Zoom + 1f;
                }
            }
        }

        /// <summary>Renders the map onto the given grpahic surface.</summary>
        /// <param name="surface"></param>
        public void Render(Graphics surface)
        {
            surface.SmoothingMode = SmoothingMode.HighQuality;

            //try to prevent the "Big Red X" by catching errors ourself
            try
            {
                DrawBackgroundImage(surface);
                DrawGrid(surface);
                if (m_depthFilterLines && m_game.Player != null)
                    DrawLinesFiltered(surface);
                else
                    DrawLines(surface);
                    DrawLabels(surface);
                    DrawSpawns(surface);
                    DrawLoc(surface); /// IHM EDIT
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WARNING: Exception thrown during render: " + ex.Message);
            }
        }

        /// <summary>Clears the map and game state.</summary>
        public void Clear()
        {
            mapViewOffset.X = 0;
            mapViewOffset.Y = 0;
            m_data.Clear();
            m_game.Clear();
            UpdateMap();
        }

        /// <summary>Updates the map state and prepares the data for rendering</summary>
        public void UpdateMap()
        {
            RectangleF mapBounds = m_data.Bounds;
            RectangleF clientBounds = ClientRectangle;

            //update the ratio based on the current size of the map and the viewport
            clientCenter.X = clientBounds.Width / 2;
            clientCenter.Y = clientBounds.Height / 2;

            float xratio = clientBounds.Width / mapBounds.Width;
            float yratio = clientBounds.Height / mapBounds.Height;

            if (xratio < yratio)
            {
                m_ratio = xratio / m_zoom;
            }
            else
            {
                m_ratio = yratio / m_zoom;
            }

            //follow map center
            if (m_game.Player != null)
            {
                mapCenter.X = m_game.Player.Location.X;
                mapCenter.Y = m_game.Player.Location.Y;
            }
            else
            {
                mapCenter.X = mapBounds.Left + (mapBounds.Width / 2);
                mapCenter.Y = mapBounds.Top + (mapBounds.Height / 2);
            }

            //Inform the host container that it needs to render the map
            if (Updated != null)
                Updated();
        }

        /// <summary>If a radar range has been defined for this game, then set the zoom factor so that the range auto fits within the window.</summary>
        public void SnapToRange()
        {
            if (m_radarRange <= 0)
                return;
            RectangleF clientBounds = ClientRectangle;

            float xratio = clientBounds.Width / (m_radarRange * 2 + 1); //range is a radius, not a diameter
            float yratio = clientBounds.Height / (m_radarRange * 2 + 1); //extra pixel so that the circle lies fully inside
            float snapbase = (xratio < yratio) ? xratio : yratio;
            float curbase = m_ratio * m_zoom;
            Zoom = (curbase / snapbase);
        }

        /// <summary>Find a spawn from CLIENT coordinates with a default search radius</summary>
        public GameSpawn FindSpawn(PointF client)
        {
            return FindSpawn(client.X, client.Y);
        }
        /// <summary>Find a spawn from CLIENT coordinates with a default search radius</summary>
        public GameSpawn FindSpawn(float clientX, float clientY)
        {
            return FindSpawn(clientX, clientY, (m_playerSize / m_ratio) * 3);
        }
        /// <summary>Find a spawn from CLIENT coordinates within the specified search radius</summary>
        public GameSpawn FindSpawn(float clientX, float clientY, float threshold)
        {
            return Game.Spawns.FindSpawn(ClientToMapCoordX(clientX), ClientToMapCoordY(clientY), threshold);
        }

        //====================================================================================================
        // Private Functions
        //====================================================================================================
        /// <summary>Translates a MAP X coordinate to a CLIENT X coordinate.</summary>
        public float CalcClientCoordX(float X)
        {
            return mapViewOffset.X + clientCenter.X + ((X - mapCenter.X) * m_ratio);
        }
        /// <summary>Translates a MAP Y coordinate to a CLIENT Y coordinate.</summary>
        public float CalcClientCoordY(float Y)
        {
            return mapViewOffset.Y + clientCenter.Y - ((Y - mapCenter.Y) * m_ratio);
        }
        /// <summary>Translates a CLIENT X coordinate to a MAP X coordinate.</summary>
        public float ClientToMapCoordX(float X)
        {
            return mapCenter.X - ((mapViewOffset.X + clientCenter.X - X) / m_ratio);
        }
        /// <summary>Translates a CLIENT Y coordinate to a MAP Y coordinate.</summary>
        public float ClientToMapCoordY(float Y)
        {
            return mapCenter.Y + ((mapViewOffset.Y + clientCenter.Y - Y) / m_ratio);
        }

        //renders the map alternative to the buffer
        private void DrawBackgroundImage(Graphics g)
        {
            if (!m_showAltMap || m_MapAltImage == null)
                return;
            if (!m_showAltAlways && m_data.Lines.Count > 0)
                return;
            RectangleF dest = RectangleF.FromLTRB(
               CalcClientCoordX(m_MapAltBounds.Left),
               CalcClientCoordY(m_MapAltBounds.Top),
               CalcClientCoordX(m_MapAltBounds.Right),
               CalcClientCoordY(m_MapAltBounds.Bottom)
            );
            g.DrawImage(m_MapAltImage, Rectangle.Round(dest), 0, 0, m_MapAltImage.Width, m_MapAltImage.Height, GraphicsUnit.Pixel, m_MapAltAttr);
        }

        //renders the grid to the buffer
        private void DrawGrid(Graphics g)
        {
            if (!m_showGridTicks && !m_showGridLines)
                return;

            int gx, gy, label;
            float sx, sy;

            Pen pLine = new Pen(m_gridColorLine);
            Pen pTick = new Pen(m_gridColorTick);
            Brush bTick = pTick.Brush;

            // Draw Horizontal Grid Lines
            for (gx = ((int)(m_data.Bounds.Left / (float)m_gridResolution)) - 1; (float)gx < (m_data.Bounds.Right / m_gridResolution) + 1; gx++)
            {
                label = (gx * m_gridResolution);
                sx = (float)Math.Round(CalcClientCoordX((float)label), 0);
                if (m_showGridLines)
                    g.DrawLine(pLine, sx, 0, sx, ClientRectangle.Height);
                if (m_showGridTicks)
                    g.DrawString(label.ToString(), m_gridFontTick, bTick, sx, ClientRectangle.Height - m_gridFontTick.Height);
            }

            // Draw Vertical Grid Lines
            for (gy = ((int)(m_data.Bounds.Top / m_gridResolution)) - 1; gy < (int)(m_data.Bounds.Bottom / m_gridResolution) + 1; gy++)
            {
                label = (gy * m_gridResolution);
                sy = (float)Math.Round(CalcClientCoordY((float)label), 0);
                if (m_showGridLines)
                    g.DrawLine(pLine, 0, sy, ClientRectangle.Width, sy);
                if (m_showGridTicks)
                    g.DrawString(label.ToString(), m_gridFontTick, bTick, 0, sy);
            }
        }

        //renders the map labels (points of interest) to the buffer
        private void DrawLabels(Graphics g)
        {
            if (!m_showLabels)
                return;

            SolidBrush b = null;
            foreach (MapLabel label in m_data.Labels)
            {
                if (b == null || b.Color != label.Color)
                {
                    b = new SolidBrush(label.Color);
                }
                float x = CalcClientCoordX(label.Location.X);
                float y = CalcClientCoordY(label.Location.Y);
                float playerZ = m_game.Player.Location.Z;

                if (m_depthFilterSpawn)
                    {
                        //calculate simple z offset and filter based on user cutoff settings.
                        // note: depth filter NEVER filters out the player, target, or current selection.
                        if (Math.Max(playerZ, label.Location.Z) - Math.Min(playerZ, label.Location.Z) > m_depthCutoff)
                            continue;
                    }
                    
                g.DrawString(label.Caption, m_labelFont, b, x, y);
            }
        }

        //renders the vector map with no depth filtering
        private void DrawLines(Graphics g)
        {
            if (!m_showLines)
                return;

            float dx = (mapViewOffset.X + clientCenter.X) / -m_ratio + mapCenter.X;
            float dy = (mapViewOffset.Y + clientCenter.Y) / -m_ratio - mapCenter.Y;
            GraphicsState tState = g.Save();
            g.ScaleTransform(m_ratio, -m_ratio);
            g.TranslateTransform(-dx, dy);

            foreach (MapLine line in m_data.Lines)
            {
                if (line.Count > 1)
                {
                    pCacheMapLine.Color = line.Color;
                    g.DrawLines(pCacheMapLine, line.getPoints());
                }
            }

            g.Restore(tState);
        }

        //renders the vector map with depth filtering
        private void DrawLinesFiltered(Graphics g)
        {
            if (!m_showLines)
                return;

            float dx = (mapViewOffset.X + clientCenter.X) / -m_ratio + mapCenter.X;
            float dy = (mapViewOffset.Y + clientCenter.Y) / -m_ratio - mapCenter.Y;
            GraphicsState tState = g.Save();
            g.ScaleTransform(m_ratio, -m_ratio);
            g.TranslateTransform(-dx, dy);

            float playerZ = m_game.Player.Location.Z;

            foreach (MapLine line in m_data.Lines)
            {
                if (line.Count > 1)
                {
                    float curX, curY, curZ, lastX, lastY, lastZ;
                    int alpha;
                    lastX = line[0].X;
                    lastY = line[0].Y;
                    lastZ = line[0].Z;

                    float lastZDist = Math.Max(playerZ, line[0].Z) - Math.Min(playerZ, line[0].Z);
                    float curZDist = 0;
                    float useZDist = 0;

                    pCacheMapLine.Color = line.Color;

                    for (int d = 1; d < line.Count; d++)
                    {
                        curX = line[d].X;
                        curY = line[d].Y;
                        curZ = line[d].Z;

                        curZDist = Math.Max(playerZ, line[d].Z) - Math.Min(playerZ, line[d].Z);
                        useZDist = Math.Min(lastZDist, curZDist);
                        lastZDist = curZDist;

                        if (useZDist > m_depthCutoff)
                        {
                            lastX = curX;
                            lastY = curY;
                            lastZ = curZ;
                            continue;
                        }
                        else if (useZDist > m_depthDistance)
                        {
                            if (m_depthUseAlpha)
                            {
                                float aratio = m_depthCutoff / m_depthMaxAlpha;
                                alpha = (int)((m_depthCutoff - useZDist) / aratio);

                                if (alpha < m_depthMinAlpha)
                                    alpha = m_depthMinAlpha;
                                pCacheMapLine.Color = Color.FromArgb(alpha, pCacheMapLine.Color);
                            }
                            else
                            {
                                pCacheMapLine.Color = Color.FromArgb(64, pCacheMapLine.Color);
                            }
                        }

                        g.DrawLine(pCacheMapLine, lastX, lastY, curX, curY);

                        lastX = curX;
                        lastY = curY;
                        lastZ = curZ;
                    }
                }
            }

            g.Restore(tState);
        }

        /// IHM EDIT
        //render FFXI-style location.
        private void DrawLoc(Graphics g)
        {
            if (ShowPlayerPosition)
            {
                char tlxc;
                decimal tlx = (decimal)(m_LocInImage.X * 2 - 17) / 32;
                decimal tly = (decimal)(m_LocInImage.Y * 2 - 17) / 32;
                tlx = Decimal.Ceiling(tlx);
                tly = Decimal.Ceiling(tly);
                if (tlx < 1 || tlx > 15)
                    tlxc = '?';
                else
                    tlxc = (char)(tlx + 64);

                g.DrawString(String.Format("({0}-{1})", tlxc, tly), m_labelFont, bYOUFill, 10, 10);
            }

        }

        //renders all valid spawn data to the buffer
        private void DrawSpawns(Graphics g)
        {
            float playerZ = 0;
            if (m_game.Player != null)
                playerZ = m_game.Player.Location.Z;

            //draw the radar radius guide around the players location
            if (m_showRadarRange && m_game.Player != null)
            {
                RectangleF area = RectangleF.FromLTRB(
                   CalcClientCoordX(m_game.Player.Location.X - m_radarRange),
                   CalcClientCoordY(m_game.Player.Location.Y + m_radarRange),
                   CalcClientCoordX(m_game.Player.Location.X + m_radarRange),
                   CalcClientCoordY(m_game.Player.Location.Y - m_radarRange)
                );
                g.DrawEllipse(pRangeFinder, area);
            }

            //draw the claim and hunt lines. rendered prior to spawns so that they are always underneath
            foreach (KeyValuePair<uint, GameSpawn> pair in m_game.Spawns)
            {
                GameSpawn spawn = pair.Value;
                if (!spawn.Hidden)
                {
                    if (m_drawPetLines && spawn.PetIndex > 0 && m_game.Spawns.ContainsIndex(spawn.PetIndex))
                    {
                        //draw the line with an arrowhead to the thing
                        GameSpawn pet = m_game.Spawns[spawn.PetIndex];
                        float headX = CalcClientCoordX(pet.Location.X);
                        float headY = CalcClientCoordY(pet.Location.Y);
                        float tailX = CalcClientCoordX(spawn.Location.X);
                        float tailY = CalcClientCoordY(spawn.Location.Y);
                        float rads = (float)Math.Atan2(headY - tailY, headX - tailX); //calculate the line angle
                        float size = 0;
                        switch (spawn.Type)
                        {
                            case SpawnType.Player:
                                size = m_playerSize;
                                break;
                            case SpawnType.NPC:
                                size = m_NPCSize;
                                break;
                            case SpawnType.MOB:
                                size = m_MOBSize;
                                break;
                            default:
                                size = 4.0f;
                                break;
                        }

                        float offX = (size * (float)Math.Cos(rads));           //move the line and arrow out a bit so its not in the dead center
                        float offY = (size * (float)Math.Sin(rads));

                        //draw ze lines
                        g.DrawLine(pPetChain, headX - offX, headY - offY, tailX, tailY);
                        DrawArrowHead(g, pPetChain, headX - offX, headY - offY, rads, 10f, 5f);
                    }
                    if (m_drawPetLines && spawn.TargetIndex > 0 && m_game.Spawns.ContainsIndex(spawn.TargetIndex))
                    {
                        if (spawn != m_game.Player)
                        {
                            GameSpawn target = m_game.Spawns[spawn.TargetIndex];
                            float headX = CalcClientCoordX(target.Location.X);
                            float headY = CalcClientCoordY(target.Location.Y);
                            float tailX = CalcClientCoordX(spawn.Location.X);
                            float tailY = CalcClientCoordY(spawn.Location.Y);
                            float rads = (float)Math.Atan2(headY - tailY, headX - tailX); //calculate the line angle
                            float size = 0;
                            switch (spawn.Type)
                        {
                            case SpawnType.Player:
                                size = m_playerSize;
                                break;
                            case SpawnType.NPC:
                                size = m_NPCSize;
                                break;
                            case SpawnType.MOB:
                                size = m_MOBSize;
                                break;
                            default:
                                size = 4.0f;
                                break;
                        }

                            float offX = (size * (float)Math.Cos(rads));           //move the line and arrow out a bit so its not in the dead center
                            float offY = (size * (float)Math.Sin(rads));
                            
                            //draw ze lines
                            g.DrawLine(pPetChain, headX - offX, headY - offY, tailX, tailY);
                            DrawArrowHead(g, pPetChain, headX - offX, headY - offY, rads, 10f, 5f);
                        }
                    }
                    if (m_drawClaimLines && spawn.ClaimID > 0 && m_game.Spawns.ContainsIndex(spawn.ClaimID))
                    {
                        GameSpawn claimee = m_game.Spawns[spawn.ClaimID];
                        g.DrawLine(pClaimChain,
                           CalcClientCoordX(claimee.Location.X),
                           CalcClientCoordY(claimee.Location.Y),
                           CalcClientCoordX(spawn.Location.X),
                           CalcClientCoordY(spawn.Location.Y)
                        );
                    }
                }
                if (!spawn.Hidden || m_showHiddenSpawn)
                {
                    if (m_drawHuntLines && spawn.Hunt && m_game.Player != null)
                    {
                        g.DrawLine(spawn.Attackable ? pHuntChain : pHuntLockedChain,
                           CalcClientCoordX(m_game.Player.Location.X),
                           CalcClientCoordY(m_game.Player.Location.Y),
                           CalcClientCoordX(spawn.Location.X),
                           CalcClientCoordY(spawn.Location.Y)
                        );
                    }
                }
            }

            if (m_game.Player != null)                                       //YOU the player is placed above all other normal spawns
                DrawPlayer(g, m_game.Player);
            
            //draw each spawn
            foreach (KeyValuePair<uint, GameSpawn> pair in m_game.Spawns)
            {
                
                GameSpawn spawn = pair.Value;
                //do not draw the target/player/selected spawns. these will be drawn later to give them higher viewing priority
                if (spawn != m_game.Player && (spawn != m_game.Selected || spawn != m_game.Target))
                {
                    //if spawn depth filtering is turned on then then determine if the spawn should be displayed
                    if (m_depthFilterSpawn && m_game.Player != null)
                    {
                        //calculate simple z offset and filter based on user cutoff settings.
                        // note: depth filter NEVER filters out the player, target, or current selection.
                        if (Math.Max(playerZ, spawn.Location.Z) - Math.Min(playerZ, spawn.Location.Z) > m_depthCutoff)
                            continue;
                    }
                    DrawSpawn(g, spawn);
                }
                
            }
            
            //draw target and selection lines
            if (m_game.Player != null && m_game.Target != null)
            {
                g.DrawLine(pSelected,
                   CalcClientCoordX(m_game.Player.Location.X),
                   CalcClientCoordY(m_game.Player.Location.Y),
                   CalcClientCoordX(m_game.Target.Location.X),
                   CalcClientCoordY(m_game.Target.Location.Y)
                );
            }
            if (m_game.Player != null && m_game.Selected != null && (!m_game.Selected.Hidden || m_showHiddenSpawn))
            {
                g.DrawLine(pSelected,
                   CalcClientCoordX(m_game.Player.Location.X),
                   CalcClientCoordY(m_game.Player.Location.Y),
                   CalcClientCoordX(m_game.Selected.Location.X),
                   CalcClientCoordY(m_game.Selected.Location.Y)
                );
            }

            //draw the player/target/selected spawns after everything else so that they are always visible above anything else
            if (m_game.Player != null)                                       //YOU the player is placed above all other normal spawns
                DrawPlayer(g, m_game.Player);
            if (m_game.Selected != null && m_game.Selected != m_game.Player) //selection has a higher precidence than YOU
                DrawSpawn(g, m_game.Selected);
            if (m_game.Target != null && m_game.Target != m_game.Player)     //current in-game target has highest precidence of anything
                DrawSpawn(g, m_game.Target);

        }

        //renders the special player graphic to the buffer
        //  this spawn represents YOU the user which displays heading information and drawn in a manner different from the other spawns
        private void DrawPlayer(Graphics g, GameSpawn spawn)
        {
            float ox = CalcClientCoordX(spawn.Location.X);
            float oy = CalcClientCoordY(spawn.Location.Y);
            float x, y;

            float size = (m_playerSize + m_spawnSelectSize);
            float arrowSize = size * 0.7f;
            float arrowHeadSize = arrowSize * 0.8f;

            //draw bounding circle
            if (m_drawYOUFill)
            {
                g.FillEllipse(bYOUFill, ox - size, oy - size, size * 2, size * 2);
                g.DrawEllipse(pYOU, ox - size, oy - size, size * 2, size * 2);
            }

            //draw arrow shaft
            y = (float)(Math.Sin(spawn.Heading) * arrowSize);
            x = (float)(Math.Cos(spawn.Heading) * arrowSize);
            g.DrawLine(pYOU, ox - x, oy - y, ox + x, oy + y);

            //draw the arrow head
            DrawArrowHead(g, pYOU, ox + x, oy + y, spawn.Heading, 10f, arrowHeadSize);
            
        }

        private void DrawArrowHead(Graphics g, Pen pen, float x, float y, float Heading, float Angle, float Size)
        {
            float x2, y2;
            //draw arrow head
            y2 = (float)(Math.Sin(Heading + Angle) * Size);
            x2 = (float)(Math.Cos(Heading + Angle) * Size);
            g.DrawLine(pen, x, y, x + x2, y + y2);
            y2 = (float)(Math.Sin(Heading - Angle) * Size);
            x2 = (float)(Math.Cos(Heading - Angle) * Size);
            g.DrawLine(pen, x, y, x + x2, y + y2);
        }

        //renders an individual spawn to the buffer
        private void DrawSpawn(Graphics g, GameSpawn spawn)
        {
            //cache certain applicability checks
            bool STH = spawn == m_game.Target || spawn == m_game.Highlighted || spawn == m_game.Selected;
            bool STHAH = STH || spawn.Alert || spawn.Hunt;
            bool STHP = STH;

            if ((m_showSpawns || STHAH) && (!spawn.Hidden || m_showHiddenSpawn))
            {
                //calc the screen coords. result is rounded to help deal with anti-aliasing issues.
                float x = (float)Math.Round(CalcClientCoordX(spawn.Location.X));
                float y = (float)Math.Round(CalcClientCoordY(spawn.Location.Y));
                float ox, oy;
                float size = 0;
                float radius = 0;

                //calculate the size to render the spawn
                if (spawn.Icon != null)
                {
                    size = Math.Max(spawn.Icon.Width, spawn.Icon.Height);
                }
                else
                {
                    switch (spawn.Type)
                    {
                        case SpawnType.Player:
                            size = m_playerSize;
                            break;
                        case SpawnType.NPC:
                            size = m_NPCSize;
                            break;
                        case SpawnType.MOB:
                            size = m_MOBSize;
                            break;
                        default:
                            size = 4.0f;
                            break;
                    }

                    if (STH)
                        size += m_spawnSelectSize;
                    if (spawn.RaidMember || spawn.GroupMember)
                        size += m_spawnMemberSize;
                    if (spawn.Alert)
                        size += m_alertSize;
                    if (spawn.Hunt)
                        size += m_huntSize;
                }
                radius = size / 2;

                if (spawn.Icon != null)
                {
                    //since an icon is set, draw it instead of a standard shape
                    g.DrawImage(spawn.Icon, x - radius, y - radius);
                    if (STHP)
                        g.DrawRectangle(pSelected, x - radius - 1, y - radius - 1, size + 1, size + 1); //prevent fuzzy edges in hq mode
                }
                else
                {
                    //draw the spawn shape based on its type
                    switch (spawn.Type)
                    {
                        case SpawnType.Player:
                            //draw the player box. if the current target draw an outline around it.
                            if (!m_usePlayerColor)
                            {
                                pCachePlayer.Color = spawn.FillColor;
                                bPlayer = pCachePlayer.Brush;
                            }
                            g.FillRectangle(bPlayer, x - radius, y - radius, size, size);

                            //alter the border to reflect various player states
                            if (STHP)
                                pCacheSpawnBorder.Color = pSelected.Color;
                            else if (spawn.GroupMember)
                                pCacheSpawnBorder.Color = m_GroupMemColor;
                            else if (spawn.RaidMember)
                                pCacheSpawnBorder.Color = m_RaidMemColor;
                            else
                                pCacheSpawnBorder.Color = pCachePlayer.Color; //prevent fuzzy edges

                            g.DrawRectangle(pCacheSpawnBorder, x - radius, y - radius, size, size);
                            break;
                        case SpawnType.NPC:
                            g.FillEllipse(bNPC, x - radius, y - radius, size, size);
                            if (STH)
                                g.DrawEllipse(pSelected, x - radius, y - radius, size, size); //prevent fuzzy edges in hq mode
                            if (m_drawAlertRanges && spawn.Alert)
                                g.DrawEllipse(pAlertRing, x - (size + m_alertSize) / 2, y - (size + m_alertSize) / 2, (size + m_alertSize), (size + m_alertSize));
                            break;
                        case SpawnType.MOB:
                            g.FillEllipse(bMOB, x - radius, y - radius, size, size);
                            if (STH)
                                g.DrawEllipse(pSelected, x - radius, y - radius, size, size); //prevent fuzzy edges in hq mode
                            if (m_drawAlertRanges && spawn.Alert)
                                g.DrawEllipse(pAlertRing, x - (size + m_alertSize) / 2, y - (size + m_alertSize) / 2, (size + m_alertSize), (size + m_alertSize));
                            break;
                    }
                }

                //draw heading line if required
                if ((m_drawHeadings || STHAH) && spawn.Type != SpawnType.Hidden)
                {
                    oy = (float)(Math.Sin(spawn.Heading) * size);
                    ox = (float)(Math.Cos(spawn.Heading) * size);
                    g.DrawLine(pHeading, x, y, x + ox, y + oy);
                }

                //Should the quick info be displayed?
                bool showInfo = STHAH || spawn.DEBUG != "";
                if (!showInfo)
                {
                    showInfo = showInfo || (m_infoAllPlayers && spawn.Type == SpawnType.Player);
                    showInfo = showInfo || (m_infoAllNPCs && spawn.Type == SpawnType.NPC);
                    showInfo = showInfo || (m_infoAllEnemies && spawn.Type == SpawnType.MOB);
                }

                //If so then build the template and write it out
                if (showInfo)
                {
                    Font fInfoText;
                    Brush bInfoText;

                    switch (spawn.Type)
                    {
                        case SpawnType.Player:
                            fInfoText = m_playerTextFont;
                            bInfoText = bTextPlayer;
                            break;
                        case SpawnType.NPC:
                            fInfoText = m_NPCTextFont;
                            bInfoText = bTextNPC;
                            break;
                        case SpawnType.MOB:
                            fInfoText = m_MOBTextFont;
                            bInfoText = bTextMOB;
                            break;
                        default:
                            fInfoText = SystemFonts.DefaultFont;
                            bInfoText = bSelected;
                            break;
                    }

                    if (STH)
                        bInfoText = bSelected;

                    //build the output string using the custom template
                    string output = m_infoTemplate;
                    //output = rxInfoName.Replace(output, spawn.Name);
                    if (spawn.Replacement)
                        output = rxInfoName.Replace(output, spawn.RepName);
                    else
                        output = rxInfoName.Replace(output, spawn.Name);
                        output = rxInfoHpp.Replace(output, spawn.HealthPercent.ToString());
                        output = rxInfoDistance.Replace(output, spawn.Distance.ToString("0.##"));
                        output = rxNewLine.Replace(output, "\n");
                        output = rxInfoID.Replace(output, spawn.ID.ToString("X")); /// IHM EDIT

                    //if debugging and a debug string is set on the spawn, then append it to the info text regardless of the template
#if DEBUG
                    if (spawn.DEBUG != "")
                        output += (output == "" ? "" : "\n") + spawn.DEBUG;
#endif

                    //measure the string and draw it on to the buffer
                    if (output != "")
                    {
                        SizeF infoSize = g.MeasureString(output, fInfoText);
                        ox = x + radius + 3;
                        oy = y - (infoSize.Height / 2);

                        if (STHAH && m_drawTextOutline)
                            DrawOutlineString(g, output, fInfoText, bSelectedOutline, ox, oy, 4);

                        if (m_drawTextShadow)
                            g.DrawString(output, fInfoText, new SolidBrush(Color.Black), ox + 1, oy + 1);

                        g.DrawString(output, fInfoText, bInfoText, ox, oy);
                    }
                }
            }
        }

        //renders text with a faded outline effect to the buffer
        private void DrawOutlineString(Graphics g, string s, Font font, Brush brush, float x, float y, int size)
        {
            float tan = size / 2;
            for (int bx = 0; bx <= size; bx++)
            {
                for (int by = 0; by <= size; by++)
                {
                    g.DrawString(s, font, brush, x - tan + bx, y - tan + by);
                }
            }
        }

        //NOTE: This is no longer being used now that clipping is handled by GDI+,
        //      but leaving this here for prosterity's sake
        //
        ////Liang-Barsky clipping algorithim (simplified)
        ////http://en.wikipedia.org/wiki/Liang-Barsky
        ////http://www.skytopia.com/project/articles/compsci/clipping.html
        //private bool LiangBarskyDetect(float x0, float y0, float x1, float y1, RectangleF bounds) {
        //   float tMin = 0;
        //   float tMax = 1;
        //   float xDelta = x1 - x0;
        //   float yDelta = y1 - y0;
        //   float[] projection = { -xDelta, xDelta, -yDelta, yDelta };
        //   float[] distance = { x0 - bounds.Left, bounds.Right - x0, y0 - bounds.Top, bounds.Bottom - y0 };
        //
        //   for(int i = 0; i < 4; i++) {
        //      if(projection[i] == 0) {
        //         if(distance[i] < 0)
        //            return false;
        //      } else {
        //         float amount = distance[i] / projection[i];
        //         if(projection[i] < 0) {
        //            if(amount > tMax)
        //               return false;
        //         } else {
        //            if(amount < tMin)
        //               return false;
        //         }
        //      }
        //   }
        //   return true;
        //}

        //====================================================================================================
        // Events
        //====================================================================================================
        //Update the map since either game state or map data has changed
        private void m_data_DataChanged()
        {
            UpdateMap();
        }
        private void m_game_Updated()
        {
            UpdateMap();
        }
    }
}