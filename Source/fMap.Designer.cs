namespace mappy {
   partial class fMap {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      private void InitializeLanguage() {
         miOnTop.Text = Program.GetLang("menu_toggle_topmost");
         miDockedToFFXI.Text = Program.GetLang("menu_toggle_docked");
         miDraggable.Text = Program.GetLang("menu_toggle_draggable");
         miResizable.Text = Program.GetLang("menu_toggle_resizable");
         miClickThru.Text = Program.GetLang("menu_toggle_clickthrough");
         //----------------
         miModeMenu.Text = Program.GetLang("menu_mode");
         miShowSpawns.Text = Program.GetLang("menu_show_spawns");
         miShowHiddenSpawns.Text = Program.GetLang("menu_show_hidden");
         miShowLines.Text = Program.GetLang("menu_show_lines");
         miShowLabels.Text = Program.GetLang("menu_show_labels");
         miShowMapImage.Text = Program.GetLang("menu_show_map_image");
         //----------------
         miSpawnAddAsHunt.Text = "";
         //----------------
         miCenterView.Text = Program.GetLang("menu_center_view");
         miSnapToRange.Text = Program.GetLang("menu_snap_range");
         //----------------
         miAddLabel.Text = Program.GetLang("menu_new_label");
         miEditHunts.Text = Program.GetLang("menu_edit_hunts");
         miEditReplacements.Text = Program.GetLang("menu_edit_replacements");
         miShowEditor.Text = Program.GetLang("menu_edit_map");
         //----------------
         miReloadMap.Text = Program.GetLang("menu_reload_map");
         miSaveMap.Text = Program.GetLang("menu_save_map");
         //----------------
         miRefresh.Text = Program.GetLang("menu_refresh_games");
         //----------------
         miQuickSettings.Text = Program.GetLang("menu_preferences");
         miExit.Text = Program.GetLang("menu_exit");
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMap));
            this.MapEngine = new MapEngine.Engine(this.components);
            this.MapMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.miDockedToFFXI = new System.Windows.Forms.ToolStripMenuItem();
            this.miDraggable = new System.Windows.Forms.ToolStripMenuItem();
            this.miResizable = new System.Windows.Forms.ToolStripMenuItem();
            this.miClickThru = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.miModeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miShowSpawns = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowHiddenSpawns = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowLines = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowMapImage = new System.Windows.Forms.ToolStripMenuItem();
            this.miSepSpawn = new System.Windows.Forms.ToolStripSeparator();
            this.miSpawnAddAsHunt = new System.Windows.Forms.ToolStripMenuItem();
            this.miSpawnAddAsHuntId = new System.Windows.Forms.ToolStripMenuItem();
            this.miSpawnAddAsHuntName = new System.Windows.Forms.ToolStripMenuItem();
            this.miSpawnAddAsReplacement = new System.Windows.Forms.ToolStripMenuItem();
            this.miSpawnAddAsReplacementId = new System.Windows.Forms.ToolStripMenuItem();
            this.miSpawnAddAsReplacementName = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miCenterView = new System.Windows.Forms.ToolStripMenuItem();
            this.miSnapToRange = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.miAddLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditHunts = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditReplacements = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.miReloadMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.miInstance = new System.Windows.Forms.ToolStripComboBox();
            this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep6 = new System.Windows.Forms.ToolStripSeparator();
            this.miQuickSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MapTimer = new System.Windows.Forms.Timer(this.components);
            this.MapInstance = new System.Windows.Forms.Timer(this.components);
            this.MapMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapEngine
            // 
            this.MapEngine.AlertRangeColor = System.Drawing.Color.Red;
            this.MapEngine.ClaimChainColor = System.Drawing.Color.Red;
            this.MapEngine.ClientRectangle = new System.Drawing.Rectangle(0, 0, 100, 100);
            this.MapEngine.DepthAlphaMax = 255;
            this.MapEngine.DepthAlphaMin = 10;
            this.MapEngine.DepthCutoff = 5F;
            this.MapEngine.DepthDistance = 2F;
            this.MapEngine.DrawHeadings = true;
            this.MapEngine.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(200)))), ((int)(((byte)(75)))));
            this.MapEngine.GridTickColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(200)))), ((int)(((byte)(75)))));
            this.MapEngine.GridTickFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapEngine.GroupMemberColor = System.Drawing.Color.Cyan;
            this.MapEngine.HeadingColor = System.Drawing.Color.White;
            this.MapEngine.HuntChainColor = System.Drawing.Color.Green;
            this.MapEngine.HuntLockedChainColor = System.Drawing.Color.DarkGreen;
            this.MapEngine.InfoTemplate = "{name} {hpp}%{br}{distance}";
            this.MapEngine.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapEngine.LocInImage = ((System.Drawing.PointF)(resources.GetObject("MapEngine.LocInImage")));
            this.MapEngine.MainPlayerColor = System.Drawing.Color.White;
            this.MapEngine.MainPlayerFillColor = System.Drawing.Color.Black;
            this.MapEngine.MapAlternativeBounds = ((System.Drawing.RectangleF)(resources.GetObject("MapEngine.MapAlternativeBounds")));
            this.MapEngine.MapAlternativeImage = null;
            this.MapEngine.MOBColor = System.Drawing.Color.Red;
            this.MapEngine.MOBInfoColor = System.Drawing.Color.Red;
            this.MapEngine.MOBInfoFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapEngine.NPCColor = System.Drawing.Color.Green;
            this.MapEngine.NPCInfoColor = System.Drawing.Color.Green;
            this.MapEngine.NPCInfoFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapEngine.PetChainColor = System.Drawing.Color.Cyan;
            this.MapEngine.PlayerColor = System.Drawing.Color.Purple;
            this.MapEngine.PlayerInfoColor = System.Drawing.Color.Purple;
            this.MapEngine.PlayerInfoFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapEngine.RadarRangeColor = System.Drawing.Color.White;
            this.MapEngine.RaidMemberColor = System.Drawing.Color.DarkCyan;
            this.MapEngine.SelectedColor = System.Drawing.Color.White;
            this.MapEngine.SpawnGroupMemberSize = 2F;
            this.MapEngine.TextOutlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MapEngine.TextShadowEnabled = false;
            this.MapEngine.ViewLocation = ((System.Drawing.PointF)(resources.GetObject("MapEngine.ViewLocation")));
            // 
            // MapMenu
            // 
            this.MapMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOnTop,
            this.miDockedToFFXI,
            this.miDraggable,
            this.miResizable,
            this.miClickThru,
            this.miSep5,
            this.miModeMenu,
            this.miShowSpawns,
            this.miShowHiddenSpawns,
            this.miShowLines,
            this.miShowLabels,
            this.miShowMapImage,
            this.miSepSpawn,
            this.miSpawnAddAsHunt,
            this.miSpawnAddAsReplacement,
            this.miSep1,
            this.miCenterView,
            this.miSnapToRange,
            this.miSep4,
            this.miAddLabel,
            this.miEditHunts,
            this.miEditReplacements,
            this.miShowEditor,
            this.miSep3,
            this.miReloadMap,
            this.miSaveMap,
            this.miSep2,
            this.miInstance,
            this.miRefresh,
            this.miSaveDefault,
            this.miClearDefault,
            this.miSep6,
            this.miQuickSettings,
            this.miExit});
            this.MapMenu.Name = "MapMenu";
            this.MapMenu.Size = new System.Drawing.Size(225, 667);
            // 
            // miOnTop
            // 
            this.miOnTop.CheckOnClick = true;
            this.miOnTop.Name = "miOnTop";
            this.miOnTop.Size = new System.Drawing.Size(224, 22);
            this.miOnTop.Text = "{menu_toggle_topmost}";
            // 
            // miDockedToFFXI
            // 
            this.miDockedToFFXI.CheckOnClick = true;
            this.miDockedToFFXI.Name = "miDockedToFFXI";
            this.miDockedToFFXI.Size = new System.Drawing.Size(224, 22);
            this.miDockedToFFXI.Text = "{menu_toggle_docked}";
            // 
            // miDraggable
            // 
            this.miDraggable.CheckOnClick = true;
            this.miDraggable.Name = "miDraggable";
            this.miDraggable.Size = new System.Drawing.Size(224, 22);
            this.miDraggable.Text = "{menu_toggle_draggable}";
            this.miDraggable.ToolTipText = "Warning: you cannot pan when drag mode is enabled!";
            // 
            // miResizable
            // 
            this.miResizable.CheckOnClick = true;
            this.miResizable.Name = "miResizable";
            this.miResizable.Size = new System.Drawing.Size(224, 22);
            this.miResizable.Text = "{menu_toggle_resizable}";
            // 
            // miClickThru
            // 
            this.miClickThru.CheckOnClick = true;
            this.miClickThru.Name = "miClickThru";
            this.miClickThru.Size = new System.Drawing.Size(224, 22);
            this.miClickThru.Text = "{menu_toggle_clickthrough}";
            // 
            // miSep5
            // 
            this.miSep5.Name = "miSep5";
            this.miSep5.Size = new System.Drawing.Size(221, 6);
            // 
            // miModeMenu
            // 
            this.miModeMenu.DropDown = this.ModeMenu;
            this.miModeMenu.Name = "miModeMenu";
            this.miModeMenu.Size = new System.Drawing.Size(224, 22);
            this.miModeMenu.Text = "{menu_mode}";
            // 
            // ModeMenu
            // 
            this.ModeMenu.Name = "ModeMenu";
            this.ModeMenu.OwnerItem = this.miModeMenu;
            this.ModeMenu.Size = new System.Drawing.Size(61, 4);
            this.ModeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ModeMenu_Opening);
            this.ModeMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ModeMenu_ItemClicked);
            // 
            // miShowSpawns
            // 
            this.miShowSpawns.CheckOnClick = true;
            this.miShowSpawns.Name = "miShowSpawns";
            this.miShowSpawns.Size = new System.Drawing.Size(224, 22);
            this.miShowSpawns.Text = "{menu_show_spawns}";
            this.miShowSpawns.Click += new System.EventHandler(this.miShowSpawns_Click);
            // 
            // miShowHiddenSpawns
            // 
            this.miShowHiddenSpawns.CheckOnClick = true;
            this.miShowHiddenSpawns.Name = "miShowHiddenSpawns";
            this.miShowHiddenSpawns.Size = new System.Drawing.Size(224, 22);
            this.miShowHiddenSpawns.Text = "{menu_show_hidden}";
            this.miShowHiddenSpawns.Click += new System.EventHandler(this.miShowHiddenSpawns_Click);
            // 
            // miShowLines
            // 
            this.miShowLines.CheckOnClick = true;
            this.miShowLines.Name = "miShowLines";
            this.miShowLines.Size = new System.Drawing.Size(224, 22);
            this.miShowLines.Text = "{menu_show_lines}";
            this.miShowLines.Click += new System.EventHandler(this.miShowLines_Click);
            // 
            // miShowLabels
            // 
            this.miShowLabels.CheckOnClick = true;
            this.miShowLabels.Name = "miShowLabels";
            this.miShowLabels.Size = new System.Drawing.Size(224, 22);
            this.miShowLabels.Text = "{menu_show_labels}";
            this.miShowLabels.Click += new System.EventHandler(this.miShowLabels_Click);
            // 
            // miShowMapImage
            // 
            this.miShowMapImage.CheckOnClick = true;
            this.miShowMapImage.Name = "miShowMapImage";
            this.miShowMapImage.Size = new System.Drawing.Size(224, 22);
            this.miShowMapImage.Text = "{menu_show_map_image}";
            this.miShowMapImage.Click += new System.EventHandler(this.miShowMapImage_Click);
            // 
            // miSepSpawn
            // 
            this.miSepSpawn.Name = "miSepSpawn";
            this.miSepSpawn.Size = new System.Drawing.Size(221, 6);
            this.miSepSpawn.Visible = false;
            // 
            // miSpawnAddAsHunt
            // 
            this.miSpawnAddAsHunt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSpawnAddAsHuntId,
            this.miSpawnAddAsHuntName});
            this.miSpawnAddAsHunt.Name = "miSpawnAddAsHunt";
            this.miSpawnAddAsHunt.Size = new System.Drawing.Size(224, 22);
            this.miSpawnAddAsHunt.Text = "{menu_add_ashunt}";
            this.miSpawnAddAsHunt.Click += new System.EventHandler(this.miSpawnAddAsHuntName_Click);
            // 
            // miSpawnAddAsHuntId
            // 
            this.miSpawnAddAsHuntId.Name = "miSpawnAddAsHuntId";
            this.miSpawnAddAsHuntId.Size = new System.Drawing.Size(112, 22);
            this.miSpawnAddAsHuntId.Text = "{id}";
            this.miSpawnAddAsHuntId.Click += new System.EventHandler(this.miSpawnAddAsHuntId_Click);
            // 
            // miSpawnAddAsHuntName
            // 
            this.miSpawnAddAsHuntName.Name = "miSpawnAddAsHuntName";
            this.miSpawnAddAsHuntName.Size = new System.Drawing.Size(112, 22);
            this.miSpawnAddAsHuntName.Text = "{name}";
            this.miSpawnAddAsHuntName.Click += new System.EventHandler(this.miSpawnAddAsHuntName_Click);
            // 
            // miSpawnAddAsReplacement
            // 
            this.miSpawnAddAsReplacement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSpawnAddAsReplacementId,
            this.miSpawnAddAsReplacementName});
            this.miSpawnAddAsReplacement.Name = "miSpawnAddAsReplacement";
            this.miSpawnAddAsReplacement.Size = new System.Drawing.Size(224, 22);
            this.miSpawnAddAsReplacement.Text = "{menu_add_asreplacement}";
            this.miSpawnAddAsReplacement.Click += new System.EventHandler(this.miSpawnAddAsReplacementName_Click);
            // 
            // miSpawnAddAsReplacementId
            // 
            this.miSpawnAddAsReplacementId.Name = "miSpawnAddAsReplacementId";
            this.miSpawnAddAsReplacementId.Size = new System.Drawing.Size(112, 22);
            this.miSpawnAddAsReplacementId.Text = "{id}";
            this.miSpawnAddAsReplacementId.Click += new System.EventHandler(this.miSpawnAddAsReplacementId_Click);
            // 
            // miSpawnAddAsReplacementName
            // 
            this.miSpawnAddAsReplacementName.Name = "miSpawnAddAsReplacementName";
            this.miSpawnAddAsReplacementName.Size = new System.Drawing.Size(112, 22);
            this.miSpawnAddAsReplacementName.Text = "{name}";
            this.miSpawnAddAsReplacementName.Click += new System.EventHandler(this.miSpawnAddAsReplacementName_Click);
            // 
            // miSep1
            // 
            this.miSep1.Name = "miSep1";
            this.miSep1.Size = new System.Drawing.Size(221, 6);
            // 
            // miCenterView
            // 
            this.miCenterView.Name = "miCenterView";
            this.miCenterView.Size = new System.Drawing.Size(224, 22);
            this.miCenterView.Text = "{menu_center_view}";
            this.miCenterView.Click += new System.EventHandler(this.miCenterView_Click);
            // 
            // miSnapToRange
            // 
            this.miSnapToRange.Name = "miSnapToRange";
            this.miSnapToRange.Size = new System.Drawing.Size(224, 22);
            this.miSnapToRange.Text = "{menu_snap_range}";
            this.miSnapToRange.Click += new System.EventHandler(this.miSnapToRange_Click);
            // 
            // miSep4
            // 
            this.miSep4.Name = "miSep4";
            this.miSep4.Size = new System.Drawing.Size(221, 6);
            // 
            // miAddLabel
            // 
            this.miAddLabel.Name = "miAddLabel";
            this.miAddLabel.Size = new System.Drawing.Size(224, 22);
            this.miAddLabel.Text = "{menu_new_label}";
            this.miAddLabel.Click += new System.EventHandler(this.miAddLabel_Click);
            // 
            // miEditHunts
            // 
            this.miEditHunts.Name = "miEditHunts";
            this.miEditHunts.Size = new System.Drawing.Size(224, 22);
            this.miEditHunts.Text = "{menu_edit_hunts}";
            this.miEditHunts.Click += new System.EventHandler(this.miEditHunts_Click);
            // 
            // miEditReplacements
            // 
            this.miEditReplacements.Name = "miEditReplacements";
            this.miEditReplacements.Size = new System.Drawing.Size(224, 22);
            this.miEditReplacements.Text = "{menu_edit_replacements}";
            this.miEditReplacements.Click += new System.EventHandler(this.miEditReplacements_Click);
            // 
            // miShowEditor
            // 
            this.miShowEditor.Name = "miShowEditor";
            this.miShowEditor.Size = new System.Drawing.Size(224, 22);
            this.miShowEditor.Text = "{menu_edit_map}";
            this.miShowEditor.Click += new System.EventHandler(this.miShowEditor_Click);
            // 
            // miSep3
            // 
            this.miSep3.Name = "miSep3";
            this.miSep3.Size = new System.Drawing.Size(221, 6);
            // 
            // miReloadMap
            // 
            this.miReloadMap.Name = "miReloadMap";
            this.miReloadMap.Size = new System.Drawing.Size(224, 22);
            this.miReloadMap.Text = "{menu_reload_map}";
            this.miReloadMap.Click += new System.EventHandler(this.miReloadMap_Click);
            // 
            // miSaveMap
            // 
            this.miSaveMap.Image = global::mappy.Properties.Resources.icon_save;
            this.miSaveMap.Name = "miSaveMap";
            this.miSaveMap.Size = new System.Drawing.Size(224, 22);
            this.miSaveMap.Text = "{menu_save_map}";
            this.miSaveMap.Click += new System.EventHandler(this.miSaveMap_Click);
            // 
            // miSep2
            // 
            this.miSep2.Name = "miSep2";
            this.miSep2.Size = new System.Drawing.Size(221, 6);
            // 
            // miInstance
            // 
            this.miInstance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.miInstance.Enabled = false;
            this.miInstance.Name = "miInstance";
            this.miInstance.Size = new System.Drawing.Size(121, 23);
            // 
            // miRefresh
            // 
            this.miRefresh.Image = global::mappy.Properties.Resources.icon_refresh;
            this.miRefresh.Name = "miRefresh";
            this.miRefresh.Size = new System.Drawing.Size(224, 22);
            this.miRefresh.Text = "Refresh";
            // 
            // miSaveDefault
            // 
            this.miSaveDefault.Name = "miSaveDefault";
            this.miSaveDefault.Size = new System.Drawing.Size(224, 22);
            this.miSaveDefault.Text = "Set as Default";
            // 
            // miClearDefault
            // 
            this.miClearDefault.Enabled = false;
            this.miClearDefault.Name = "miClearDefault";
            this.miClearDefault.Size = new System.Drawing.Size(224, 22);
            this.miClearDefault.Text = "Clear Default";
            // 
            // miSep6
            // 
            this.miSep6.Name = "miSep6";
            this.miSep6.Size = new System.Drawing.Size(221, 6);
            // 
            // miQuickSettings
            // 
            this.miQuickSettings.Image = global::mappy.Properties.Resources.icon_properties;
            this.miQuickSettings.Name = "miQuickSettings";
            this.miQuickSettings.Size = new System.Drawing.Size(224, 22);
            this.miQuickSettings.Text = "{menu_preferences}";
            this.miQuickSettings.Click += new System.EventHandler(this.miQuickSettings_Click);
            // 
            // miExit
            // 
            this.miExit.Image = global::mappy.Properties.Resources.icon_delete;
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(224, 22);
            this.miExit.Text = "{menu_exit}";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // MapInstance
            // 
            this.MapInstance.Interval = 10;
            // 
            // fMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Layered = true;
            this.LayerOpacity = 0.4D;
            this.Name = "fMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mappy";
            this.MapMenu.ResumeLayout(false);
            this.ResumeLayout(false);

      }

      #endregion

      private MapEngine.Engine MapEngine;
      private System.Windows.Forms.ContextMenuStrip MapMenu;
      private System.Windows.Forms.ToolStripMenuItem miOnTop;
      private System.Windows.Forms.ToolStripMenuItem miDockedToFFXI;
      private System.Windows.Forms.ToolStripMenuItem miDraggable;
      private System.Windows.Forms.ToolStripMenuItem miQuickSettings;
      private System.Windows.Forms.ToolStripMenuItem miResizable;
      private System.Windows.Forms.ToolStripSeparator miSep1;
      private System.Windows.Forms.ToolStripMenuItem miClickThru;
      private System.Windows.Forms.ToolStripMenuItem miShowEditor;
      private System.Windows.Forms.ToolStripSeparator miSep2;
      private System.Windows.Forms.ToolStripMenuItem miCenterView;
      private System.Windows.Forms.ToolStripMenuItem miAddLabel;
      private System.Windows.Forms.ToolStripMenuItem miEditHunts;
      private System.Windows.Forms.ToolStripSeparator miSepSpawn;
      private System.Windows.Forms.ToolStripMenuItem miSpawnAddAsHunt;
      private System.Windows.Forms.ToolStripSeparator miSep3;
      private System.Windows.Forms.ToolStripMenuItem miSaveMap;
      private System.Windows.Forms.ToolStripMenuItem miReloadMap;
      private System.Windows.Forms.ToolStripMenuItem miShowHiddenSpawns;
      private System.Windows.Forms.ToolStripMenuItem miSnapToRange;
      private System.Windows.Forms.ToolStripSeparator miSep4;
      private System.Windows.Forms.ToolStripSeparator miSep5;
      private System.Windows.Forms.ToolStripMenuItem miShowSpawns;
      private System.Windows.Forms.ToolStripMenuItem miShowLines;
      private System.Windows.Forms.ToolStripMenuItem miShowLabels;
      private System.Windows.Forms.ToolStripMenuItem miExit;
      private System.Windows.Forms.ToolStripMenuItem miShowMapImage;
      private System.Windows.Forms.Timer MapTimer;
      private System.Windows.Forms.Timer MapInstance;
      private System.Windows.Forms.ContextMenuStrip ModeMenu;
      private System.Windows.Forms.ToolStripMenuItem miModeMenu;
      private System.Windows.Forms.ToolStripMenuItem miSpawnAddAsReplacement;
      private System.Windows.Forms.ToolStripMenuItem miEditReplacements;
      private System.Windows.Forms.ToolStripMenuItem miSpawnAddAsHuntId;
      private System.Windows.Forms.ToolStripMenuItem miSpawnAddAsHuntName;
      private System.Windows.Forms.ToolStripMenuItem miSpawnAddAsReplacementId;
      private System.Windows.Forms.ToolStripMenuItem miSpawnAddAsReplacementName;
      private System.Windows.Forms.ToolStripSeparator miSep6;
      public System.Windows.Forms.ToolStripComboBox miInstance;
      private System.Windows.Forms.ToolStripMenuItem miRefresh;
      public System.Windows.Forms.ToolStripMenuItem miSaveDefault;
      public System.Windows.Forms.ToolStripMenuItem miClearDefault;
    }
}