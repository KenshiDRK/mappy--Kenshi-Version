using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapEngine;
using System.Diagnostics;
using System.IO;
using static mappy.Controller;

namespace mappy {
    public partial class fMap : LayeredForm {
        //properties
      private IController  m_controller = null;
      private GameInstance m_game       = null;
      private Config       m_config     = null;

      //storage
      private GameSpawn  m_contextspawn = null;
      private string     m_mapPackPath  = "";

      private double m_original_opacity;
      private bool m_original_clickthru;
      private bool m_actionState = false;
      private IMapEditor currentEditor;

      public Dictionary<int, ProcessItem> gamelist;
        //====================================================================================================
        // Constructor
        //====================================================================================================
        public fMap(Controller controller, Process process, Config config) {
         m_controller = controller;
         m_config = config;
         
         //Initialize form
         m_config.Suspend();
         Editor = controller.Editors.Default;
         InitializeComponent();
         InitializeLanguage();

         gamelist = new Dictionary<int, ProcessItem>();

         //Add the supported editors to the mode list
         if (controller.Editors != null && controller.Editors.Count > 0) {
            //add supported editors to the mode selector
            foreach (IMapEditor editor in controller.Editors) {
               ToolStripItem item = new ToolStripMenuItem(editor.ToString());
               item.Tag = editor;
               ModeMenu.Items.Add(item);
            }
            miModeMenu.Visible = true;
         } else {
            currentEditor = new MapNavigator();
            miModeMenu.Visible = false;
         }

         //apply the configuration settings from the current profile
         ApplyConfig();
         m_config.Resume();
         m_controller.RefreshInstances(gamelist, miInstance, miSaveDefault, miClearDefault);

         //set the intial check state of the context menu toggles
         miOnTop.Checked = this.OnTop;
         miDockedToFFXI.Checked = this.Docked;
         miDraggable.Checked = this.Draggable;
         miResizable.Checked = this.Resizable;
         miClickThru.Checked = this.ClickThrough;

         //bind event handlers
         this.Paint += new PaintEventHandler(fMap_Paint);
         MapEngine.Updated += new GenericEvent(MapEngine_Updated);
         MapEngine.ClientRectangle = Bounds;

         MapMenu.Opening += new CancelEventHandler(MapMenu_Opening);
         miOnTop.CheckedChanged += new EventHandler(miOnTop_CheckedChanged);
         miDockedToFFXI.CheckedChanged += new EventHandler(miDockedToFFXI_CheckedChanged);
         miDraggable.CheckedChanged += new EventHandler(miDraggable_CheckedChanged);
         miResizable.CheckedChanged += new EventHandler(miResizable_CheckedChanged);
         miClickThru.CheckedChanged += new EventHandler(miClickThru_CheckedChanged);
         miInstance.SelectedIndexChanged += new EventHandler(m_controller.miInstance_SelectedIndexChanged);
         miRefresh.Click += new EventHandler(m_controller.miRefresh_Click);
         miSaveDefault.Click += new EventHandler(m_controller.miSaveDefault_Click);
         miClearDefault.Click += new EventHandler(m_controller.miClearDefault_Click);

         //check the data path and inform the user if invalid
         bool dns = m_config.Get("DNS_MapPath", false);
         if(!dns && !Directory.Exists(MapEngine.Data.FilePath)) {
            int button = MessageBoxEx.Show(string.Format(Program.GetLang("msg_mappath_invalid_text"), MapEngine.Data.FilePath), Program.GetLang("msg_mappath_invalid_title"), new string[] { Program.GetLang("button_browse"), Program.GetLang("button_ignore") }, MessageBoxIcon.Warning, out dns);
            m_config["DNS_MapPath"] = dns;
            if(button == 0) {
               FolderBrowserDialog browse = new FolderBrowserDialog();
               if(browse.ShowDialog(this) == DialogResult.OK) {
                  MapEngine.Data.FilePath = browse.SelectedPath;
                  m_config["MapPath"] = MapEngine.Data.FilePath;
               }
            }
         }

         //check the map pack path and inform the user if invalid
         dns = m_config.Get("DNS_MapPackPath", false);
         if(!dns && !File.Exists(MapPackPath + Program.MapIniFile)) {
            int button = MessageBoxEx.Show(string.Format(Program.GetLang("msg_mappack_invalid_text"), MapPackPath), Program.GetLang("msg_mappack_invalid_title"), new string[] { Program.GetLang("button_download"), Program.GetLang("button_browse"), Program.GetLang("button_ignore") }, MessageBoxIcon.Warning, out dns);
            m_config["DNS_MapPackPath"] = dns;
            if(button == 0) {
               System.Diagnostics.Process.Start(Program.ResGlobal.GetString("config_mappack_url"));
               MessageBox.Show(Program.GetLang("msg_pack_afterdownload_text"), Program.GetLang("msg_pack_afterdownload_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if(button == 1) {
               while(true) {
                  FolderBrowserDialog browse = new FolderBrowserDialog();
                  if(browse.ShowDialog(this) == DialogResult.OK) {
                     //if the user selected a path that does not contain the map.ini file then reject it and inform the user
                     if(!File.Exists(browse.SelectedPath + "\\" + Program.MapIniFile)) {
                        if(MessageBox.Show(string.Format(Program.GetLang("msg_mappack_badsel_text_alt"), browse.SelectedPath, Program.MapIniFile), Program.GetLang("msg_mappack_badsel_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                           continue;
                        break;
                     }

                     //lock the new map pack in
                     MapPackPath = browse.SelectedPath;
                     m_config["MapPackPath"] = MapPackPath;
                  }
                  break;
               }
            }
         }

         try {
            //initialize the game instance with the map engine
            m_game = controller.CreateInstance(process, this);

            if (m_game.Valid)
            {
               MapTimer.Tick += new EventHandler(MapTimer_Tick);
               MapTimer.Enabled = true;
               MapInstance.Tick += new EventHandler(MapInstance_Tick);
               MapInstance.Enabled = true;
            }
         } catch (InstanceException fex) {
            MessageBox.Show(fex.Message, "Unable to Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      //====================================================================================================
      // Properties
      //====================================================================================================
      public IMapEditor Editor {
         get { return currentEditor; }
         set {
            //always revert to the default editor if an empty editor is passed in
            IMapEditor neweditor = value;
            if (neweditor == null && m_controller.Editors != null)
               neweditor = m_controller.Editors.Default;
            if (neweditor == null || neweditor.Engaged) //ignore the new editor if it is already engaged
               return;

            //only disengage/engage if the editor is actually changing
            if (currentEditor != neweditor) {
               if (currentEditor != null)
                  currentEditor.OnDisengage();
               currentEditor = neweditor;
               currentEditor.OnEngage(this);
            }
         }
      }
      
      public Process Process {
         get {
            if (m_game == null)
               return null;
            return m_game.Process; 
         }
         set {
            if (m_game != null && value != null)
               m_game.Process = value; 
         }
      }

      public GameInstance Instance {
         get { return m_game; }
      }

      public Config Config {
         get { return m_config; }
         set {
            if(m_config != null)
               m_config.Save();

            m_config = value;
            ApplyConfig();
         }
      }

      public Engine Engine {
         get { return MapEngine; }
      }

      public int PollFrequency {
         get { return MapTimer.Interval; }
         set { MapTimer.Interval = value; }
      }

      //track changes to embedded functionality for configuration storage
      new public bool OnTop {
         get { return base.OnTop; }
         set {
            base.OnTop = value;
            m_config["OnTop"] = value;
         }
      }

      new public bool Docked {
         get { return base.Docked; }
         set {
            base.Docked = value;
            m_config["Docked"] = value;
         }
      }

      new public bool Draggable {
         get { return base.Draggable; }
         set {
            base.Draggable = value;
            m_config["Draggable"] = value;
         }
      }

      new public bool Resizable {
         get { return base.Resizable; }
         set {
            base.Resizable = value;
            m_config["Resizable"] = value;
         }
      }

      new public bool ClickThrough {
         get { return base.ClickThrough; }
         set {
            base.ClickThrough = value;
            m_config["ClickThrough"] = value;
            m_original_clickthru = value;
         }
      }

      public string MapPackPath {
         get { return m_mapPackPath; }
         set {
            if(value != "" && value.Substring(value.Length - 1, 1) != "\\")
               value += "\\";
            m_mapPackPath = value;
            if(m_game != null && m_game is IFFXIMapImageContainer)
               ((IFFXIMapImageContainer)m_game).Maps.FilePath = value;
         }
      }

      public float Zoom {
         get { return MapEngine.Zoom; }
         set {
            MapEngine.Zoom = value;
            m_config["Zoom"] = MapEngine.Zoom;
         }
      }

      //====================================================================================================
      // Private Functions
      //====================================================================================================
      private void ApplyConfig() {
         try {
            //window configuration
            this.OnTop = m_config.Get("OnTop", false);
            this.Docked = m_config.Get("Docked", false);
            this.Draggable = m_config.Get("Draggable", false);
            this.Resizable = m_config.Get("Resizable", true);
            this.ClickThrough = m_config.Get("ClickThrough", false);
            this.Left = m_config.Get("Left", Bounds.Left);
            this.Top = m_config.Get("Top", Bounds.Top);
            this.Width = m_config.Get("Width", Bounds.Width);
            this.Height = m_config.Get("Height", Bounds.Height);
            this.BackColor = Color.FromArgb(m_config.Get("BackColor", Color.Black.ToArgb()));
            this.LayerOpacity = m_config.Get("LayerOpacity", 0.4);
            this.PollFrequency = m_config.Get("PollFreq", PollFrequency);
            this.MapPackPath = m_config.Get("MapPackPath", "maps\\");

            //engine configuration
            MapEngine.Zoom = m_config.Get("Zoom", 1.0f);
            MapEngine.GridSize = m_config.Get("GridSize", MapEngine.GridSize);
            MapEngine.ShowGridLines = m_config.Get("ShowGridLines", true);
            MapEngine.ShowGridTicks = m_config.Get("ShowGridTicks", true);
            MapEngine.GridLineColor = Color.FromArgb(m_config.Get("GridLineColor", MapEngine.GridLineColor.ToArgb()));
            MapEngine.GridTickColor = Color.FromArgb(m_config.Get("GridTickColor", MapEngine.GridTickColor.ToArgb()));
            MapEngine.ShowPlayerPosition = m_config.Get("ShowPlayerPosition", true);
            MapEngine.PlayerColor = Color.FromArgb(m_config.Get("PlayerColor", MapEngine.PlayerColor.ToArgb()));
            MapEngine.NPCColor = Color.FromArgb(m_config.Get("NPCColor", MapEngine.NPCColor.ToArgb()));
            MapEngine.MOBColor = Color.FromArgb(m_config.Get("MOBColor", MapEngine.MOBColor.ToArgb()));
            MapEngine.UsePlayerColor = m_config.Get("UsePlayerColor", false);
            MapEngine.Data.FilePath = m_config.Get("MapPath", "maps\\");
            MapEngine.MapAlternativeOpacity = m_config.Get("MapImgOpacity", MapEngine.MapAlternativeOpacity);
            MapEngine.ShowMapAlternative = m_config.Get("ShowMapAlt", true);
            MapEngine.ShowMapAlternativeAlways = m_config.Get("ShowMapAltAlways", true);
            MapEngine.ShowHiddenSpawns = m_config.Get("ShowHiddenSpawns", false);
            MapEngine.ShowSpawns = m_config.Get("ShowSpawns", true);
            MapEngine.ShowLines = m_config.Get("ShowLines", true);
            MapEngine.ShowLabels = m_config.Get("ShowLabels", true);
            MapEngine.AutoRangeSnap = m_config.Get("AutoRangeSnap", true);
            MapEngine.DepthFilterLines = m_config.Get("DepthFilterLines", true);
            MapEngine.DepthFilterSpawns = m_config.Get("DepthFilterSpawns", false);
            MapEngine.DepthFilterAlpha = m_config.Get("DepthFilterAlpha", true);
            MapEngine.DepthAlphaMin = m_config.Get("DepthAlphaMin", MapEngine.DepthAlphaMin);
            MapEngine.DepthAlphaMax = m_config.Get("DepthAlphaMax", MapEngine.DepthAlphaMax);
            MapEngine.DepthDistance = m_config.Get("DepthDistance", MapEngine.DepthDistance);
            MapEngine.DepthCutoff = m_config.Get("DepthCutoff", MapEngine.DepthCutoff);
            MapEngine.SelectedColor = Color.FromArgb(m_config.Get("SelectedColor", MapEngine.SelectedColor.ToArgb()));
            MapEngine.TextOutlineEnabled = m_config.Get("TextOutlineEnabled", true);
            MapEngine.TextShadowEnabled = m_config.Get("TextShadowEnabled", false);
            MapEngine.TextOutlineColor = Color.FromArgb(m_config.Get("TextOutlineColor", MapEngine.TextOutlineColor.ToArgb()));
            MapEngine.ShowRadarRange = m_config.Get("ShowRadarRange", true);
            MapEngine.RadarRangeColor = Color.FromArgb(m_config.Get("RadarRangeColor", MapEngine.RadarRangeColor.ToArgb()));
            MapEngine.HuntChainColor = Color.FromArgb(m_config.Get("HuntChainColor", MapEngine.HuntChainColor.ToArgb()));
            MapEngine.HuntLockedChainColor = Color.FromArgb(m_config.Get("HuntLockedChainColor", MapEngine.HuntLockedChainColor.ToArgb()));
            MapEngine.ClaimChainColor = Color.FromArgb(m_config.Get("ClaimChainColor", MapEngine.ClaimChainColor.ToArgb()));
            MapEngine.PetChainColor = Color.FromArgb(m_config.Get("PetChainColor", MapEngine.PetChainColor.ToArgb()));
            MapEngine.GroupMemberColor = Color.FromArgb(m_config.Get("GroupMemberColor", MapEngine.GroupMemberColor.ToArgb()));
            MapEngine.RaidMemberColor = Color.FromArgb(m_config.Get("RaidMemberColor", MapEngine.RaidMemberColor.ToArgb()));
            MapEngine.InfoTemplate = m_config.Get("InfoTemplate", MapEngine.InfoTemplate);
            MapEngine.ShowAllPlayerInfo = m_config.Get("ShowAllPlayerInfo", false);
            MapEngine.ShowAllNPCInfo = m_config.Get("ShowAllNPCInfo", false);
            MapEngine.ShowAllEnemyInfo = m_config.Get("ShowAllEnemyInfo", false);
            MapEngine.ScaleLines = m_config.Get("ScaleLines", false);
            MapEngine.MainPlayerColor = Color.FromArgb(m_config.Get("MainPlayerColor", MapEngine.MainPlayerColor.ToArgb()));
            MapEngine.MainPlayerFillColor = Color.FromArgb(m_config.Get("MainPlayerFillColor", MapEngine.MainPlayerFillColor.ToArgb()));
            MapEngine.MainPlayerFillEnabled = m_config.Get("MainPlayerFillEnabled", true);
            MapEngine.ShowClaimLines = m_config.Get("ShowClaimLines", true);
            MapEngine.ShowHuntLines = m_config.Get("ShowHuntLines", true);
            MapEngine.ShowPetLines = m_config.Get("ShowPetLines", true);
            MapEngine.PlayerSize = m_config.Get("PlayerSize", MapEngine.PlayerSize);
            MapEngine.NPCSize = m_config.Get("NPCSize", MapEngine.NPCSize);
            MapEngine.MOBSize = m_config.Get("MOBSize", MapEngine.MOBSize);
            MapEngine.SpawnSelectSize = m_config.Get("SpawnSelectSize", MapEngine.SpawnSelectSize);
            MapEngine.SpawnGroupMemberSize = m_config.Get("SpawnGroupMemberSize", MapEngine.SpawnGroupMemberSize);
            MapEngine.SpawnHuntSize = m_config.Get("SpawnHuntSize", MapEngine.SpawnHuntSize);
            MapEngine.PlayerInfoColor = Color.FromArgb(m_config.Get("PlayerInfoColor", MapEngine.PlayerInfoColor.ToArgb()));
            MapEngine.NPCInfoColor = Color.FromArgb(m_config.Get("NPCInfoColor", MapEngine.NPCInfoColor.ToArgb()));
            MapEngine.MOBInfoColor = Color.FromArgb(m_config.Get("MOBInfoColor", MapEngine.MOBInfoColor.ToArgb()));
            MapEngine.PlayerInfoFont = m_config.Get<Font>("PlayerInfoFont", MapEngine.PlayerInfoFont);
            MapEngine.NPCInfoFont = m_config.Get<Font>("NPCInfoFont", MapEngine.NPCInfoFont);
            MapEngine.MOBInfoFont = m_config.Get<Font>("MOBInfoFont", MapEngine.MOBInfoFont);
            MapEngine.DrawHeadings = m_config.Get("DrawHeadingLines", MapEngine.DrawHeadings);
            MapEngine.HeadingColor = Color.FromArgb(m_config.Get("HeadingLineColor", MapEngine.HeadingColor.ToArgb()));

            //save initial modifier settings
            m_original_opacity = LayerOpacity;
            m_original_clickthru = ClickThrough;
         } catch(Exception ex) {
            Debug.WriteLine("WARNING: an error occured processing a config value: " + ex.Message);
         }
      }

      public void SetActionState(bool state) {
         if(m_actionState == state)
            return;
         m_actionState = state;
         
         if(state) {
            m_original_opacity = LayerOpacity;
            m_original_clickthru = ClickThrough;
            base.LayerOpacity = 1;     //skip past the config hooks
            base.ClickThrough = false; //skip past the config hooks
         } else {
            base.LayerOpacity = m_original_opacity;
            base.ClickThrough = m_original_clickthru;
         }
      }

      private Font GetFont(string font) {
         TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
         return (Font)tc.ConvertFromString(font);
      }

      public void ShowContextMenu(GameSpawn context, Point location) {
         m_contextspawn = context;
         MapMenu.Show(this, location);
      }

      //====================================================================================================
      // Editor Events
      //====================================================================================================
      protected override void OnMouseDown(MouseEventArgs e) {
         //base.OnMouseDown(e);
         currentEditor.OnMouseDown(e);
      }
      protected override void OnMouseMove(MouseEventArgs e) {
         //base.OnMouseMove(e);
         currentEditor.OnMouseMove(e);
      }
      protected override void OnMouseUp(MouseEventArgs e) {
         //base.OnMouseUp(e);
         currentEditor.OnMouseUp(e);
      }
      protected override void OnMouseEnter(EventArgs e) {
         //base.OnMouseEnter(e);
         currentEditor.OnMouseEnter(e);
      }
      protected override void OnMouseLeave(EventArgs e) {
         //base.OnMouseLeave(e);
         currentEditor.OnMouseLeave(e);
      }
      protected override void OnMouseWheel(MouseEventArgs e) {
         //base.OnMouseWheel(e);
         currentEditor.OnMouseWheel(e);
      }
      protected override void OnKeyDown(KeyEventArgs e) {
         //base.OnKeyDown(e);
         currentEditor.OnKeyDown(e);
      }
      protected override void OnKeyPress(KeyPressEventArgs e) {
         //base.OnKeyPress(e);
         currentEditor.OnKeyPress(e);
      }
      protected override void OnKeyUp(KeyEventArgs e) {
         //base.OnKeyUp(e);
         currentEditor.OnKeyUp(e);
      }
      protected override void OnMove(EventArgs e) {
         base.OnMove(e);
         currentEditor.OnMove(e);

         //Capture the current location so that it may be restored next time the app runs.
         // This behavior cannot be redefined by an editor.
         m_config["Top"] = Bounds.Top;
         m_config["Left"] = Bounds.Left;
      }
      protected override void OnResize(EventArgs e) {
         base.OnResize(e);
         currentEditor.OnResize(e);

         //The engine must be notified whenever the drawing surface changes to handle buffer allocations
         // This behavior cannot be redefined by an editor.
         MapEngine.ClientRectangle = Bounds;
         m_config["Width"] = Bounds.Width;
         m_config["Height"] = Bounds.Height;
      }
      //Cannot use override in this case since we need the propagated graphic object
      private void fMap_Paint(object sender, PaintEventArgs e) {
         //Render the map directly to the window surface.
         currentEditor.OnBeforePaint(e);
         MapEngine.Render(e.Graphics);
         currentEditor.OnAfterPaint(e);
      }

        //====================================================================================================
        // Events
        //====================================================================================================
        protected override void OnClosed(EventArgs e) {
         if (currentEditor != null)
            currentEditor.OnDisengage();
         SetActionState(false);
         m_config["ShowSpawns"] = MapEngine.ShowSpawns;
         m_config["ShowHiddenSpawns"] = MapEngine.ShowHiddenSpawns;
         m_config["ShowLines"] = MapEngine.ShowLines;
         m_config["ShowLabels"] = MapEngine.ShowLabels;
         m_config["ShowMapAlt"] = MapEngine.ShowMapAlternative;
         m_config.Save();
      }
      
      private void MapMenu_Opening(object sender, CancelEventArgs e) {
         //since there are two menus that set these values in the engine directly, we must update the
         //  state of the menu items when opening it.
         SetActionState(false);
         miOnTop.Checked = this.OnTop;
         miDockedToFFXI.Checked = this.Docked;
         miDraggable.Checked = this.Draggable;
         miResizable.Checked = this.Resizable;
         miClickThru.Checked = this.ClickThrough;
         miShowLabels.Checked = MapEngine.ShowLabels;
         miShowLines.Checked = MapEngine.ShowLines;
         miShowSpawns.Checked = MapEngine.ShowSpawns;
         miShowHiddenSpawns.Checked = MapEngine.ShowHiddenSpawns;
         miShowHiddenSpawns.Enabled = miShowSpawns.Checked;
         miShowMapImage.Checked = MapEngine.ShowMapAlternative;

         miSaveMap.Enabled = MapEngine.Data.Dirty; //disable the save option if there have been no edits

         //some context menu options are only available if right clicking a spawn
         if(m_contextspawn != null && m_contextspawn.Name != "") {
            miSepSpawn.Visible = true;
            miSpawnAddAsHunt.Visible = true;
            miSpawnAddAsHunt.Text = string.Format(Program.GetLang("menu_add_ashunt"), m_contextspawn.Name);
            miSpawnAddAsHuntId.Text = Program.GetLang("id");
            miSpawnAddAsHuntName.Text = Program.GetLang("name");
            miSpawnAddAsReplacement.Visible = true;
            miSpawnAddAsReplacement.Text = string.Format(Program.GetLang("menu_add_asreplacement"), m_contextspawn.Name);
            miSpawnAddAsReplacementId.Text = Program.GetLang("id");
            miSpawnAddAsReplacementName.Text = Program.GetLang("name");
         } else {
            miSepSpawn.Visible = false;
            miSpawnAddAsHunt.Visible = false;
            miSpawnAddAsReplacement.Visible = false;
         }
      }

      private void miQuickSettings_Click(object sender, EventArgs e) {
         fCustomize.BeginCustomization(m_controller, this);
      }

      private void miClickThru_CheckedChanged(object sender, EventArgs e) {
         this.ClickThrough = miClickThru.Checked;
         if (ClickThrough) {
            m_controller.ShowBalloonTip(1500, Program.GetLang("bubble_warn_clickthru_title"), Program.GetLang("bubble_warn_clickthru_text"), ToolTipIcon.Info);
         }
      }

      private void miResizable_CheckedChanged(object sender, EventArgs e) {
         this.Resizable = miResizable.Checked;
      }

      private void miDockedToFFXI_CheckedChanged(object sender, EventArgs e) {
         this.Docked = miDockedToFFXI.Checked;
      }

      private void miDraggable_CheckedChanged(object sender, EventArgs e) {
         this.Draggable = miDraggable.Checked;
      }

      private void miOnTop_CheckedChanged(object sender, EventArgs e) {
         this.OnTop = miOnTop.Checked;
      }

      private void miShowHiddenSpawns_Click(object sender, EventArgs e) {
         MapEngine.ShowHiddenSpawns = miShowHiddenSpawns.Checked;
      }

      private void miShowSpawns_Click(object sender, EventArgs e) {
         MapEngine.ShowSpawns = miShowSpawns.Checked;
         miShowHiddenSpawns.Enabled = miShowSpawns.Checked;
      }

      private void miShowLines_Click(object sender, EventArgs e) {
         MapEngine.ShowLines = miShowLines.Checked;
      }

      private void miShowLabels_Click(object sender, EventArgs e) {
         MapEngine.ShowLabels = miShowLabels.Checked;
      }

      private void MapTimer_Tick(object sender, EventArgs e) {
         try {
            m_game.Poll();
         } catch(Exception ex) {
            Debug.WriteLine("Shutting down the poll timer...");
            MapTimer.Enabled = false;
            MessageBox.Show("An error occurred while polling the game:\n" + ex.Message);
         }
      }
      
      private void MapInstance_Tick(object sender, EventArgs e) {
         try {
            var currentHandle = GetForegroundWindow();
            if (currentHandle == this.Handle) {
            }
            else if (OnTop) {
                if (!TopMost)
                    this.TopMost = true;
            }
            else if ((currentHandle == Process.MainWindowHandle) && Docked) {
                if (!TopMost)
                    this.TopMost = true;
            }
            else if ((currentHandle != Process.MainWindowHandle) && TopMost) {
                this.TopMost = false;
            }
         } catch(Exception ex) {
            Debug.WriteLine("Shutting down the instance timer...");
            MapInstance.Enabled = false;
            MessageBox.Show("An error occurred while getting the instance handle:\n" + ex.Message);
         }
      }

      private void MapEngine_Updated() {
         //Something on the map has been updated, so invalidate the surface to prepare it for rendering
         this.Invalidate();
      }

      private void miShowEditor_Click(object sender, EventArgs e) {
         fEditMap.BeginEditor(this);
      }

      private void miCenterView_Click(object sender, EventArgs e) {
         MapEngine.CenterView();
      }

      private void miAddLabel_Click(object sender, EventArgs e) {
         fEditLabel.BeginAdd(this);
      }

      private void miEditHunts_Click(object sender, EventArgs e) {
         fEditHunts.BeginEdit(this);
      }

      private void miEditReplacements_Click(object sender, EventArgs e)
      {
         fEditReplacements.BeginEdit(this);
      }

      private void miSpawnAddAsHuntId_Click(object sender, EventArgs e)
      {
          if (m_contextspawn == null)
              return;
          MapEngine.Data.Hunts.Add(m_contextspawn.ID.ToString("X"), true);
      }

      private void miSpawnAddAsHuntName_Click(object sender, EventArgs e)
      {
          if (m_contextspawn == null)
              return;
          MapEngine.Data.Hunts.Add(m_contextspawn.Name, true);
      }

      private void miSpawnAddAsReplacementId_Click(object sender, EventArgs e)
      {
          if (m_contextspawn == null)
              return;
          MapEngine.Data.Replacements.Add(m_contextspawn.ID.ToString("X"), m_contextspawn.Name, true);
      }

      private void miSpawnAddAsReplacementName_Click(object sender, EventArgs e)
      {
          if (m_contextspawn == null)
              return;
          MapEngine.Data.Replacements.Add(m_contextspawn.Name, m_contextspawn.Name, true);
      }

      private void miSaveMap_Click(object sender, EventArgs e) {
         MapEngine.Data.Save();
      }

      private void miReloadMap_Click(object sender, EventArgs e) {
         if(MapEngine.Data.Dirty && MessageBox.Show(Program.GetLang("msg_unsaved_reload_text"), Program.GetLang("msg_unsaved_reload_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            return;
         MapEngine.Data.Reload();
      }

      private void miSnapToRange_Click(object sender, EventArgs e) {
         MapEngine.SnapToRange();
         m_config["Zoom"] = MapEngine.Zoom;
      }

      private void miExit_Click(object sender, EventArgs e) {
         m_controller.Terminate();
      }

      private void miShowMapImage_Click(object sender, EventArgs e) {
         MapEngine.ShowMapAlternative = miShowMapImage.Checked;
      }

      private void ModeMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
         if (e.ClickedItem == null)
            return;
         IMapEditor editor = (IMapEditor)e.ClickedItem.Tag;
         if (editor == null)
            return;
         Editor = editor;
      }

      private void ModeMenu_Opening(object sender, CancelEventArgs e) {
         foreach(ToolStripMenuItem item in ModeMenu.Items) {
            IMapEditor editor = (IMapEditor)item.Tag;
            item.Checked = (editor == currentEditor);
         }
      }
   }
}