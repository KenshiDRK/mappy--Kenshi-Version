namespace mappy {
   partial class Controller {
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

      #region Component Designer generated code

      private void InitializeLanguage() {
         miActiveOnTop.Text = Program.GetLang("menu_toggle_topmost");
         miActiveDocked.Text = Program.GetLang("menu_toggle_docked");
         miActiveDraggable.Text = Program.GetLang("menu_toggle_draggable");
         miActiveResizable.Text = Program.GetLang("menu_toggle_resizable");
         miActiveClickthru.Text = Program.GetLang("menu_toggle_clickthrough");
         //----------------
         miActiveShowSpawns.Text = Program.GetLang("menu_show_spawns");
         miActiveShowHidden.Text = Program.GetLang("menu_show_hidden");
         miActiveShowLines.Text = Program.GetLang("menu_show_lines");
         miActiveShowLabels.Text = Program.GetLang("menu_show_labels");
         miActiveShowMapImage.Text = Program.GetLang("menu_show_map_image");
         //----------------
         miActiveCenter.Text = Program.GetLang("menu_center_view");
         miActiveSnapRange.Text = Program.GetLang("menu_snap_range");
         //----------------
         miRefresh.Text = Program.GetLang("menu_refresh_games");
         //----------------
         miOptions.Text = Program.GetLang("menu_preferences");
         //----------------
         miAbout.Text = Program.GetLang("menu_about");
         miTrayExit.Text = Program.GetLang("menu_exit");
      }

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miActiveOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveDocked = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveDraggable = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveResizable = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveClickthru = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.miActiveShowSpawns = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveShowHidden = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveShowLines = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveShowLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveShowMapImage = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.miActiveCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveSnapRange = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.miInstance = new System.Windows.Forms.ToolStripComboBox();
            this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.miTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayMenu.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayMenu;
            this.TrayIcon.Text = "Mappy";
            this.TrayIcon.Visible = true;
            // 
            // TrayMenu
            // 
            this.TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miActiveOnTop,
            this.miActiveDocked,
            this.miActiveDraggable,
            this.miActiveResizable,
            this.miActiveClickthru,
            this.miSep4,
            this.miActiveShowSpawns,
            this.miActiveShowHidden,
            this.miActiveShowLines,
            this.miActiveShowLabels,
            this.miActiveShowMapImage,
            this.miSep2,
            this.miActiveCenter,
            this.miActiveSnapRange,
            this.miSep5,
            this.miInstance,
            this.miRefresh,
            this.miSaveDefault,
            this.miClearDefault,
            this.miSep1,
            this.miOptions,
            this.miSep3,
            this.miAbout,
            this.miTrayExit});
            this.TrayMenu.Name = "TrayMenu";
            this.TrayMenu.Size = new System.Drawing.Size(214, 457);
            // 
            // miActiveOnTop
            // 
            this.miActiveOnTop.CheckOnClick = true;
            this.miActiveOnTop.Name = "miActiveOnTop";
            this.miActiveOnTop.Size = new System.Drawing.Size(213, 22);
            this.miActiveOnTop.Text = "Always On Top";
            // 
            // miActiveDocked
            // 
            this.miActiveDocked.CheckOnClick = true;
            this.miActiveDocked.Name = "miActiveDocked";
            this.miActiveDocked.Size = new System.Drawing.Size(213, 22);
            this.miActiveDocked.Text = "Docked to FFXI";
            // 
            // miActiveDraggable
            // 
            this.miActiveDraggable.CheckOnClick = true;
            this.miActiveDraggable.Name = "miActiveDraggable";
            this.miActiveDraggable.Size = new System.Drawing.Size(213, 22);
            this.miActiveDraggable.Text = "Draggable";
            // 
            // miActiveResizable
            // 
            this.miActiveResizable.CheckOnClick = true;
            this.miActiveResizable.Name = "miActiveResizable";
            this.miActiveResizable.Size = new System.Drawing.Size(213, 22);
            this.miActiveResizable.Text = "Resizable";
            // 
            // miActiveClickthru
            // 
            this.miActiveClickthru.CheckOnClick = true;
            this.miActiveClickthru.Name = "miActiveClickthru";
            this.miActiveClickthru.Size = new System.Drawing.Size(213, 22);
            this.miActiveClickthru.Text = "Click Through";
            // 
            // miSep4
            // 
            this.miSep4.Name = "miSep4";
            this.miSep4.Size = new System.Drawing.Size(210, 6);
            // 
            // miActiveShowSpawns
            // 
            this.miActiveShowSpawns.CheckOnClick = true;
            this.miActiveShowSpawns.Name = "miActiveShowSpawns";
            this.miActiveShowSpawns.Size = new System.Drawing.Size(213, 22);
            this.miActiveShowSpawns.Text = "Show Spawns";
            // 
            // miActiveShowHidden
            // 
            this.miActiveShowHidden.CheckOnClick = true;
            this.miActiveShowHidden.Name = "miActiveShowHidden";
            this.miActiveShowHidden.Size = new System.Drawing.Size(213, 22);
            this.miActiveShowHidden.Text = "Show Hidden Spawns";
            // 
            // miActiveShowLines
            // 
            this.miActiveShowLines.CheckOnClick = true;
            this.miActiveShowLines.Name = "miActiveShowLines";
            this.miActiveShowLines.Size = new System.Drawing.Size(213, 22);
            this.miActiveShowLines.Text = "Show Map Lines";
            // 
            // miActiveShowLabels
            // 
            this.miActiveShowLabels.CheckOnClick = true;
            this.miActiveShowLabels.Name = "miActiveShowLabels";
            this.miActiveShowLabels.Size = new System.Drawing.Size(213, 22);
            this.miActiveShowLabels.Text = "Show Labels";
            // 
            // miActiveShowMapImage
            // 
            this.miActiveShowMapImage.CheckOnClick = true;
            this.miActiveShowMapImage.Name = "miActiveShowMapImage";
            this.miActiveShowMapImage.Size = new System.Drawing.Size(213, 22);
            this.miActiveShowMapImage.Text = "{menu_show_map_image}";
            // 
            // miSep2
            // 
            this.miSep2.Name = "miSep2";
            this.miSep2.Size = new System.Drawing.Size(210, 6);
            // 
            // miActiveCenter
            // 
            this.miActiveCenter.Name = "miActiveCenter";
            this.miActiveCenter.Size = new System.Drawing.Size(213, 22);
            this.miActiveCenter.Text = "{menu_center_view}";
            // 
            // miActiveSnapRange
            // 
            this.miActiveSnapRange.Name = "miActiveSnapRange";
            this.miActiveSnapRange.Size = new System.Drawing.Size(213, 22);
            this.miActiveSnapRange.Text = "{menu_snap_range}";
            // 
            // miSep5
            // 
            this.miSep5.Name = "miSep5";
            this.miSep5.Size = new System.Drawing.Size(210, 6);
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
            this.miRefresh.Size = new System.Drawing.Size(213, 22);
            this.miRefresh.Text = "Refresh";
            // 
            // miSaveDefault
            // 
            this.miSaveDefault.Name = "miSaveDefault";
            this.miSaveDefault.Size = new System.Drawing.Size(213, 22);
            this.miSaveDefault.Text = "Set as Default";
            // 
            // miClearDefault
            // 
            this.miClearDefault.Name = "miClearDefault";
            this.miClearDefault.Size = new System.Drawing.Size(213, 22);
            this.miClearDefault.Text = "Clear Default";
            // 
            // miSep1
            // 
            this.miSep1.Name = "miSep1";
            this.miSep1.Size = new System.Drawing.Size(210, 6);
            // 
            // miOptions
            // 
            this.miOptions.Image = global::mappy.Properties.Resources.icon_properties;
            this.miOptions.Name = "miOptions";
            this.miOptions.Size = new System.Drawing.Size(213, 22);
            this.miOptions.Text = "Preferences...";
            // 
            // miSep3
            // 
            this.miSep3.Name = "miSep3";
            this.miSep3.Size = new System.Drawing.Size(210, 6);
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(213, 22);
            this.miAbout.Text = "{menu_about}";
            // 
            // miTrayExit
            // 
            this.miTrayExit.Image = global::mappy.Properties.Resources.icon_delete;
            this.miTrayExit.Name = "miTrayExit";
            this.miTrayExit.Size = new System.Drawing.Size(213, 22);
            this.miTrayExit.Text = "Exit";
            this.TrayMenu.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.NotifyIcon TrayIcon;
      private System.Windows.Forms.ContextMenuStrip TrayMenu;
      private System.Windows.Forms.ToolStripMenuItem miTrayExit;
      private System.Windows.Forms.ToolStripSeparator miSep1;
      private System.Windows.Forms.ToolStripMenuItem miRefresh;
      private System.Windows.Forms.ToolStripComboBox miInstance;
      private System.Windows.Forms.ToolStripMenuItem miActiveOnTop;
      private System.Windows.Forms.ToolStripMenuItem miActiveClickthru;
      private System.Windows.Forms.ToolStripMenuItem miActiveDocked;
      private System.Windows.Forms.ToolStripMenuItem miActiveDraggable;
      private System.Windows.Forms.ToolStripMenuItem miActiveResizable;
      private System.Windows.Forms.ToolStripSeparator miSep2;
      private System.Windows.Forms.ToolStripMenuItem miOptions;
      private System.Windows.Forms.ToolStripSeparator miSep3;
      private System.Windows.Forms.ToolStripMenuItem miActiveShowHidden;
      private System.Windows.Forms.ToolStripSeparator miSep4;
      private System.Windows.Forms.ToolStripMenuItem miActiveShowSpawns;
      private System.Windows.Forms.ToolStripMenuItem miActiveShowLines;
      private System.Windows.Forms.ToolStripMenuItem miActiveShowLabels;
      private System.Windows.Forms.ToolStripMenuItem miAbout;
      private System.Windows.Forms.ToolStripMenuItem miActiveCenter;
      private System.Windows.Forms.ToolStripMenuItem miActiveSnapRange;
      private System.Windows.Forms.ToolStripSeparator miSep5;
      private System.Windows.Forms.ToolStripMenuItem miActiveShowMapImage;
      private System.Windows.Forms.ToolStripMenuItem miSaveDefault;
      private System.Windows.Forms.ToolStripMenuItem miClearDefault;
   }
}
