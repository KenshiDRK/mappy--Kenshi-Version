namespace mappy {
   partial class fCustomize {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if(disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }


      private void InitializeLanguage() {
         //dialog
         this.Text = Program.GetLang("dialog_config");
         cmdOK.Text = Program.GetLang("button_ok");
         cmdCancel.Text = Program.GetLang("button_cancel");
         //tabs
         tpMap.Text = Program.GetLang("tab_map");
         tpAppearance.Text = Program.GetLang("tab_appearance");
         tpDepthFilter.Text = Program.GetLang("tab_depthfilter");
         tpSignatures.Text = Program.GetLang("tab_sig");
         tpHotkeys.Text = Program.GetLang("tab_hotkeys");
         //map tab
         gBehavior.Text = Program.GetLang("config_group_behavior");
         lblFillColor.Text = Program.GetLang("config_map_fill_color");
         lblFillOpacity.Text = Program.GetLang("config_map_fill_opacity");
         chkAutoRangeSnap.Text = Program.GetLang("config_map_autosnap");
         chkScaleLines.Text = Program.GetLang("config_map_scale_lines");
         lblMapPathTitle.Text = Program.GetLang("config_map_mappath");
         lblPollFreq.Text = Program.GetLang("config_map_pollfreq");
         gMapPack.Text = Program.GetLang("config_group_mappack");
         chkShowMapAlternative.Text = Program.GetLang("config_map_alt_show");
         lblMapPackImagePathTitle.Text = Program.GetLang("config_map_mappackpath");
         chkShowMapAlternativeAlways.Text = Program.GetLang("config_map_alt_show_always");
         lblMapAltOpacityTitle.Text = Program.GetLang("config_map_alt_opacity");
         gSpawnInfo.Text = Program.GetLang("config_group_spawninfo");
         lblInfoTemplateTitle.Text = Program.GetLang("config_map_info_template");
         lblInfoTemplateHelp.Text = Program.GetLang("config_map_info_template_help");
         chkInfoAllPlayers.Text = Program.GetLang("config_map_info_all_players");
         chkInfoAllNPCs.Text = Program.GetLang("config_map_info_all_npcs");
         chkInfoAllEnemies.Text = Program.GetLang("config_map_info_all_enemies");
         //appearance tab
         lblAppearSectionGrid.Text = Program.GetLang("config_appear_section_grid");
         chkShowGridLines.Text = Program.GetLang("config_appear_show_grid_lines");
         chkShowGridTicks.Text = Program.GetLang("config_appear_show_grid_ticks");
         lblGridSize.Text = Program.GetLang("config_appear_grid_size");
         lblGridLineColor.Text = Program.GetLang("config_appear_grid_line_color");
         lblGridTickColor.Text = Program.GetLang("config_appear_grid_tick_color");
         chkShowPlayerPosition.Text = Program.GetLang("config_appear_show_player_position");
         lblAppearSectionSpawn.Text = Program.GetLang("config_appear_section_spawn");
         lblPlayerSize.Text = Program.GetLang("config_appear_player_size");
         lblPlayerInfoFont.Text = Program.GetLang("config_appear_player_info_font");
         lblPlayerInfoColor.Text = Program.GetLang("config_appear_player_info_color");
         lblNPCInfoFont.Text = Program.GetLang("config_appear_npc_info_font");
         lblNPCInfoColor.Text = Program.GetLang("config_appear_npc_info_color");
         lblMOBInfoFont.Text = Program.GetLang("config_appear_enemy_info_font");
         lblMOBInfoColor.Text = Program.GetLang("config_appear_enemy_info_color");
         lblNPCSize.Text = Program.GetLang("config_appear_npc_size");
         lblMOBSize.Text = Program.GetLang("config_appear_enemy_size");
         lblSpawnSelectSize.Text = Program.GetLang("config_appear_spawn_select_size");
         lblSpawnGroupSize.Text = Program.GetLang("config_appear_spawn_group_size");
         lblSpawnHuntSize.Text = Program.GetLang("config_appear_spawn_hunt_size");
         lblYouColor.Text = Program.GetLang("config_appear_you_color");
         chkUseMainPlayerFill.Text = Program.GetLang("config_appear_you_fill_enabled");
         lblYouFillColor.Text = Program.GetLang("config_appear_you_fill_color");
         chkUsePlayerColor.Text = Program.GetLang("config_appear_use_player_color");
         lblNPCColor.Text = Program.GetLang("config_appear_npc_color");
         lblEnemyColor.Text = Program.GetLang("config_appear_enemy_color");
         lblSelectedColor.Text = Program.GetLang("config_appear_selected_color");
         chkShowTextOutline.Text = Program.GetLang("config_appear_use_glow");
         chkShowTextShadow.Text = Program.GetLang("config_appear_use_shadow");
         lblTextGlowColor.Text = Program.GetLang("config_appear_glow_color");
         chkShowRadarRange.Text = Program.GetLang("config_appear_show_range");
         lblRadarRangeColor.Text = Program.GetLang("config_appear_range_color");
         lblGroupColor.Text = Program.GetLang("config_appear_group_color");
         lblRaidColor.Text = Program.GetLang("config_appear_raid_color");
         lblAppearSectionLines.Text = Program.GetLang("config_appear_section_lines");
         chkShowHuntLines.Text = Program.GetLang("config_appear_show_hunt_lines");
         lblHuntLineColor.Text = Program.GetLang("config_appear_hunt_line_color");
         lblHuntLockedLineColor.Text = Program.GetLang("config_appear_hunt_locked_line_color");
         chkShowClaimLines.Text = Program.GetLang("config_appear_show_claim_lines");
         lblClaimLineColor.Text = Program.GetLang("config_appear_claim_line_color");
         chkShowPetLines.Text = Program.GetLang("config_appear_show_pet_lines");
         lblPetLineColor.Text = Program.GetLang("config_appear_pet_line_color");
         chkDrawHeadingLines.Text = Program.GetLang("config_appear_draw_heading_lines");
         lblHeadingLineColor.Text = Program.GetLang("config_appear_heading_line_color");

         //Depth Filter tab
         chkEnableLineDepthFilter.Text = Program.GetLang("config_depth_enable_line_filter");
         chkEnableSpawnDepthFilter.Text = Program.GetLang("config_depth_enable_spawn_filter");
         lblDepthDistance.Text = Program.GetLang("config_depth_distance");
         lblDepthCutoff.Text = Program.GetLang("config_depth_cutoff");
         chkDepthFilterUseAlpha.Text = Program.GetLang("config_depth_use_alpha");
         lblDepthAlphaMin.Text = Program.GetLang("config_depth_alpha_min");
         lblDepthAlphaMax.Text = Program.GetLang("config_depth_alpha_max");
         //hotkeys tab
         lblActionKey.Text = Program.GetLang("config_hk_action_key");
         lblActionKeyHelp.Text = Program.GetLang("config_hk_action_key_help");
         chkEnableInputFiltering.Text = Program.GetLang("config_hk_enable_filtering");
         chkEnableHotkeyCancel.Text = Program.GetLang("config_hk_allow_cancel");
         lblHotkeyHelp.Text = Program.GetLang("config_hk_help");
         cHotKey.Text = Program.GetLang("config_hk_col_action_name");
         cBinding.Text = Program.GetLang("config_hk_col_action_binding");
         //sig tab
         lblSigWarning.Text = Program.GetLang("config_general_sig_warning");
         lblSigHelp.Text = Program.GetLang("config_general_sig_help");
         lblSIG_ZONE_ID.Text = Program.GetLang("config_general_SIG_ZONE_ID");
         lblSIG_ZONE_SHORT.Text = Program.GetLang("config_general_SIG_ZONE_SHORT");
         lblSIG_SPAWN_START.Text = Program.GetLang("config_general_SIG_SPAWN_START");
         lblSIG_SPAWN_END.Text = Program.GetLang("config_general_SIG_SPAWN_END");
         lblSIG_MY_ID.Text = Program.GetLang("config_general_SIG_MY_ID");
         lblSIG_MY_TARGET.Text = Program.GetLang("config_general_SIG_MY_TARGET");
         lblSIG_INSTANCE_ID.Text = Program.GetLang("config_general_SIG_INSTANCE_ID");
         lblSigNoActivePID.Text = Program.GetLang("config_general_sig_no_active_pid");
         lblSigRestartWarning.Text = Program.GetLang("config_general_sig_restart_warning");
         cmdResetSigs.Text = Program.GetLang("button_reset");
         cmdSigDefaults.Text = Program.GetLang("button_defaults");
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCustomize));
            this.dColorChanger = new System.Windows.Forms.ColorDialog();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblProductName = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tpMap = new System.Windows.Forms.TabPage();
            this.gSpawnInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblInfoTemplateTitle = new System.Windows.Forms.Label();
            this.txtInfoTemplate = new System.Windows.Forms.TextBox();
            this.lblInfoTemplateHelp = new System.Windows.Forms.Label();
            this.chkInfoAllPlayers = new System.Windows.Forms.CheckBox();
            this.chkInfoAllNPCs = new System.Windows.Forms.CheckBox();
            this.chkInfoAllEnemies = new System.Windows.Forms.CheckBox();
            this.gMapPack = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pbMapPackWarning = new System.Windows.Forms.PictureBox();
            this.lblMapPackImagePathTitle = new System.Windows.Forms.Label();
            this.chkShowMapAlternative = new System.Windows.Forms.CheckBox();
            this.chkShowMapAlternativeAlways = new System.Windows.Forms.CheckBox();
            this.lblMapAltOpacityTitle = new System.Windows.Forms.Label();
            this.lblMapImageOpacityPercent = new System.Windows.Forms.Label();
            this.tbMapImageOpacity = new System.Windows.Forms.TrackBar();
            this.lblMapPackImagePath = new System.Windows.Forms.Label();
            this.cmdBrowseMapPackImagePath = new System.Windows.Forms.Button();
            this.gBehavior = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pbFilePathWarning = new System.Windows.Forms.PictureBox();
            this.lblMapPathTitle = new System.Windows.Forms.Label();
            this.lblFillColor = new System.Windows.Forms.Label();
            this.cmdChangeBackColor = new System.Windows.Forms.Button();
            this.lblFillOpacity = new System.Windows.Forms.Label();
            this.tbOpacity = new System.Windows.Forms.TrackBar();
            this.lblOpacityPercent = new System.Windows.Forms.Label();
            this.chkAutoRangeSnap = new System.Windows.Forms.CheckBox();
            this.lblMapPath = new System.Windows.Forms.Label();
            this.cmdBrowseMapPath = new System.Windows.Forms.Button();
            this.lblPollFreq = new System.Windows.Forms.Label();
            this.udPollFrequency = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.chkScaleLines = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.chkDepthFilterUseAlpha = new System.Windows.Forms.CheckBox();
            this.chkEnableSpawnDepthFilter = new System.Windows.Forms.CheckBox();
            this.chkEnableLineDepthFilter = new System.Windows.Forms.CheckBox();
            this.lblDepthDistance = new System.Windows.Forms.Label();
            this.txtDepthDistance = new System.Windows.Forms.TextBox();
            this.lblDepthCutoff = new System.Windows.Forms.Label();
            this.txtDepthCutoff = new System.Windows.Forms.TextBox();
            this.lblDepthAlphaMin = new System.Windows.Forms.Label();
            this.udDepthAlphaMin = new System.Windows.Forms.NumericUpDown();
            this.lblDepthAlphaMax = new System.Windows.Forms.Label();
            this.udDepthAlphaMax = new System.Windows.Forms.NumericUpDown();
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpAppearance = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.chkShowPlayerPosition = new System.Windows.Forms.CheckBox();
            this.lblAppearSectionGrid = new System.Windows.Forms.Label();
            this.chkShowGridLines = new System.Windows.Forms.CheckBox();
            this.chkShowGridTicks = new System.Windows.Forms.CheckBox();
            this.lblGridSize = new System.Windows.Forms.Label();
            this.udGridSize = new System.Windows.Forms.NumericUpDown();
            this.lblGridLineColor = new System.Windows.Forms.Label();
            this.cmdGridLineColor = new System.Windows.Forms.Button();
            this.lblGridTickColor = new System.Windows.Forms.Label();
            this.cmdGridTickColor = new System.Windows.Forms.Button();
            this.lblAppearSectionSpawn = new System.Windows.Forms.Label();
            this.lblPlayerSize = new System.Windows.Forms.Label();
            this.udPlayerSize = new System.Windows.Forms.NumericUpDown();
            this.chkUsePlayerColor = new System.Windows.Forms.CheckBox();
            this.cmdPlayerColor = new System.Windows.Forms.Button();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPlayerInfoFont = new System.Windows.Forms.Label();
            this.lblPlayerInfoFontDisplay = new System.Windows.Forms.Label();
            this.cmdPlayerInfoFont = new System.Windows.Forms.Button();
            this.lblPlayerInfoColor = new System.Windows.Forms.Label();
            this.cmdPlayerInfoColor = new System.Windows.Forms.Button();
            this.lblYouColor = new System.Windows.Forms.Label();
            this.cmdMainPlayerColor = new System.Windows.Forms.Button();
            this.chkUseMainPlayerFill = new System.Windows.Forms.CheckBox();
            this.lblYouFillColor = new System.Windows.Forms.Label();
            this.cmdMainPlayerFillColor = new System.Windows.Forms.Button();
            this.lblGroupColor = new System.Windows.Forms.Label();
            this.cmdGroupMemberColor = new System.Windows.Forms.Button();
            this.lblRaidColor = new System.Windows.Forms.Label();
            this.cmdRaidMemberColor = new System.Windows.Forms.Button();
            this.lblNPCSize = new System.Windows.Forms.Label();
            this.udNPCSize = new System.Windows.Forms.NumericUpDown();
            this.lblNPCColor = new System.Windows.Forms.Label();
            this.cmdNPCColor = new System.Windows.Forms.Button();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNPCInfoFont = new System.Windows.Forms.Label();
            this.lblNPCInfoFontDisplay = new System.Windows.Forms.Label();
            this.cmdNPCInfoFont = new System.Windows.Forms.Button();
            this.lblNPCInfoColor = new System.Windows.Forms.Label();
            this.cmdNPCInfoColor = new System.Windows.Forms.Button();
            this.lblMOBSize = new System.Windows.Forms.Label();
            this.udMOBSize = new System.Windows.Forms.NumericUpDown();
            this.lblEnemyColor = new System.Windows.Forms.Label();
            this.cmdEnemyColor = new System.Windows.Forms.Button();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMOBInfoFont = new System.Windows.Forms.Label();
            this.lblMOBInfoFontDisplay = new System.Windows.Forms.Label();
            this.cmdMOBInfoFont = new System.Windows.Forms.Button();
            this.lblMOBInfoColor = new System.Windows.Forms.Label();
            this.cmdMOBInfoColor = new System.Windows.Forms.Button();
            this.lblSpawnSelectSize = new System.Windows.Forms.Label();
            this.udSpawnSelectSize = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedColor = new System.Windows.Forms.Label();
            this.cmdSelectedColor = new System.Windows.Forms.Button();
            this.lblTextGlowColor = new System.Windows.Forms.Label();
            this.cmdTextGlowColor = new System.Windows.Forms.Button();
            this.udSpawnGroupSize = new System.Windows.Forms.NumericUpDown();
            this.lblSpawnGroupSize = new System.Windows.Forms.Label();
            this.lblSpawnHuntSize = new System.Windows.Forms.Label();
            this.udSpawnHuntSize = new System.Windows.Forms.NumericUpDown();
            this.chkDrawHeadingLines = new System.Windows.Forms.CheckBox();
            this.lblHeadingLineColor = new System.Windows.Forms.Label();
            this.cmdHeadingLineColor = new System.Windows.Forms.Button();
            this.chkShowRadarRange = new System.Windows.Forms.CheckBox();
            this.lblRadarRangeColor = new System.Windows.Forms.Label();
            this.cmdRadarRangeColor = new System.Windows.Forms.Button();
            this.lblAppearSectionLines = new System.Windows.Forms.Label();
            this.chkShowHuntLines = new System.Windows.Forms.CheckBox();
            this.lblHuntLineColor = new System.Windows.Forms.Label();
            this.cmdHuntLineColor = new System.Windows.Forms.Button();
            this.lblHuntLockedLineColor = new System.Windows.Forms.Label();
            this.cmdHuntLockedLineColor = new System.Windows.Forms.Button();
            this.chkShowClaimLines = new System.Windows.Forms.CheckBox();
            this.lblClaimLineColor = new System.Windows.Forms.Label();
            this.cmdClaimLineColor = new System.Windows.Forms.Button();
            this.chkShowPetLines = new System.Windows.Forms.CheckBox();
            this.lblPetLineColor = new System.Windows.Forms.Label();
            this.cmdPetLineColor = new System.Windows.Forms.Button();
            this.chkShowTextOutline = new System.Windows.Forms.CheckBox();
            this.chkShowTextShadow = new System.Windows.Forms.CheckBox();
            this.tpDepthFilter = new System.Windows.Forms.TabPage();
            this.tpHotkeys = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.chkEnableInputFiltering = new System.Windows.Forms.CheckBox();
            this.lblActionKey = new System.Windows.Forms.Label();
            this.lvHotKeyBindings = new System.Windows.Forms.ListView();
            this.cHotKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cBinding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmHotkeyBindings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miClearBinding = new System.Windows.Forms.ToolStripMenuItem();
            this.miUseDefaultBinding = new System.Windows.Forms.ToolStripMenuItem();
            this.miResetBinding = new System.Windows.Forms.ToolStripMenuItem();
            this.lblActionKeyHelp = new System.Windows.Forms.Label();
            this.chkEnableHotkeyCancel = new System.Windows.Forms.CheckBox();
            this.txtActionKey = new System.Windows.Forms.TextBox();
            this.lblHotkeyHelp = new System.Windows.Forms.Label();
            this.tpSignatures = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdResetSigs = new System.Windows.Forms.Button();
            this.cmdSigDefaults = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.pbStatus_SIG_INSTANCE_ID = new System.Windows.Forms.PictureBox();
            this.lblSIG_INSTANCE_ID = new System.Windows.Forms.Label();
            this.lblSigWarning = new System.Windows.Forms.Label();
            this.lblSigHelp = new System.Windows.Forms.Label();
            this.txtSIG_ZONE_ID = new System.Windows.Forms.TextBox();
            this.lblSIG_MY_TARGET = new System.Windows.Forms.Label();
            this.txtSIG_ZONE_SHORT = new System.Windows.Forms.TextBox();
            this.lblSIG_ZONE_SHORT = new System.Windows.Forms.Label();
            this.lblSIG_ZONE_ID = new System.Windows.Forms.Label();
            this.lblSIG_SPAWN_START = new System.Windows.Forms.Label();
            this.lblSIG_SPAWN_END = new System.Windows.Forms.Label();
            this.lblSIG_MY_ID = new System.Windows.Forms.Label();
            this.txtSIG_SPAWN_START = new System.Windows.Forms.TextBox();
            this.txtSIG_SPAWN_END = new System.Windows.Forms.TextBox();
            this.txtSIG_MY_ID = new System.Windows.Forms.TextBox();
            this.txtSIG_MY_TARGET = new System.Windows.Forms.TextBox();
            this.txtSIG_INSTANCE_ID = new System.Windows.Forms.TextBox();
            this.lblSigNoActivePID = new System.Windows.Forms.Label();
            this.lblSigRestartWarning = new System.Windows.Forms.Label();
            this.pbStatus_SIG_MY_TARGET = new System.Windows.Forms.PictureBox();
            this.pbStatus_SIG_MY_ID = new System.Windows.Forms.PictureBox();
            this.pbStatus_SIG_SPAWN_END = new System.Windows.Forms.PictureBox();
            this.pbStatus_SIG_SPAWN_START = new System.Windows.Forms.PictureBox();
            this.pbStatus_SIG_ZONE_SHORT = new System.Windows.Forms.PictureBox();
            this.pbStatus_SIG_ZONE_ID = new System.Windows.Forms.PictureBox();
            this.dBrowseFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dFontChanger = new System.Windows.Forms.FontDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tpMap.SuspendLayout();
            this.gSpawnInfo.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.gMapPack.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapPackWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMapImageOpacity)).BeginInit();
            this.gBehavior.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilePathWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPollFrequency)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDepthAlphaMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDepthAlphaMax)).BeginInit();
            this.tcOptions.SuspendLayout();
            this.tpAppearance.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPlayerSize)).BeginInit();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udNPCSize)).BeginInit();
            this.flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMOBSize)).BeginInit();
            this.flowLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpawnSelectSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpawnGroupSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpawnHuntSize)).BeginInit();
            this.tpDepthFilter.SuspendLayout();
            this.tpHotkeys.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.cmHotkeyBindings.SuspendLayout();
            this.tpSignatures.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_INSTANCE_ID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_MY_TARGET)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_MY_ID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_SPAWN_END)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_SPAWN_START)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_ZONE_SHORT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_ZONE_ID)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdOK.AutoSize = true;
            this.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(296, 17);
            this.cmdOK.MinimumSize = new System.Drawing.Size(70, 0);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(73, 23);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "{button_ok}";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(197, 17);
            this.cmdCancel.MinimumSize = new System.Drawing.Size(70, 0);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(93, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "{button_cancel}";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdOK, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdCancel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 432);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 57);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblProductName);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 51);
            this.panel1.TabIndex = 9;
            // 
            // lblProductName
            // 
            this.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(45, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblProductName.Size = new System.Drawing.Size(143, 51);
            this.lblProductName.TabIndex = 9;
            this.lblProductName.TabStop = true;
            this.lblProductName.Text = "MappyXI 0.0.00";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProductName.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lblProductName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblProductName_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::mappy.Properties.Resources.Mappy;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // tpMap
            // 
            this.tpMap.AutoScroll = true;
            this.tpMap.Controls.Add(this.gSpawnInfo);
            this.tpMap.Controls.Add(this.gMapPack);
            this.tpMap.Controls.Add(this.gBehavior);
            this.tpMap.Location = new System.Drawing.Point(4, 22);
            this.tpMap.Name = "tpMap";
            this.tpMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpMap.Size = new System.Drawing.Size(364, 406);
            this.tpMap.TabIndex = 0;
            this.tpMap.Text = "{tab_map}";
            this.tpMap.UseVisualStyleBackColor = true;
            // 
            // gSpawnInfo
            // 
            this.gSpawnInfo.AutoSize = true;
            this.gSpawnInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gSpawnInfo.Controls.Add(this.tableLayoutPanel6);
            this.gSpawnInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gSpawnInfo.Location = new System.Drawing.Point(3, 314);
            this.gSpawnInfo.Name = "gSpawnInfo";
            this.gSpawnInfo.Size = new System.Drawing.Size(341, 143);
            this.gSpawnInfo.TabIndex = 4;
            this.gSpawnInfo.TabStop = false;
            this.gSpawnInfo.Text = "{config_group_spawninfo}";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.lblInfoTemplateTitle, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtInfoTemplate, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblInfoTemplateHelp, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.chkInfoAllPlayers, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.chkInfoAllNPCs, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.chkInfoAllEnemies, 0, 4);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(335, 108);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // lblInfoTemplateTitle
            // 
            this.lblInfoTemplateTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoTemplateTitle.AutoSize = true;
            this.lblInfoTemplateTitle.Location = new System.Drawing.Point(3, 6);
            this.lblInfoTemplateTitle.Name = "lblInfoTemplateTitle";
            this.lblInfoTemplateTitle.Size = new System.Drawing.Size(139, 13);
            this.lblInfoTemplateTitle.TabIndex = 23;
            this.lblInfoTemplateTitle.Text = "{config_map_info_template}";
            // 
            // txtInfoTemplate
            // 
            this.txtInfoTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfoTemplate.Location = new System.Drawing.Point(148, 3);
            this.txtInfoTemplate.Name = "txtInfoTemplate";
            this.txtInfoTemplate.Size = new System.Drawing.Size(184, 20);
            this.txtInfoTemplate.TabIndex = 28;
            this.txtInfoTemplate.TextChanged += new System.EventHandler(this.txtInfoTemplate_TextChanged);
            // 
            // lblInfoTemplateHelp
            // 
            this.lblInfoTemplateHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoTemplateHelp.AutoSize = true;
            this.lblInfoTemplateHelp.Location = new System.Drawing.Point(148, 26);
            this.lblInfoTemplateHelp.Name = "lblInfoTemplateHelp";
            this.lblInfoTemplateHelp.Size = new System.Drawing.Size(184, 13);
            this.lblInfoTemplateHelp.TabIndex = 27;
            this.lblInfoTemplateHelp.Text = "{config_map_info_template_help}";
            // 
            // chkInfoAllPlayers
            // 
            this.chkInfoAllPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInfoAllPlayers.AutoEllipsis = true;
            this.tableLayoutPanel6.SetColumnSpan(this.chkInfoAllPlayers, 2);
            this.chkInfoAllPlayers.Location = new System.Drawing.Point(3, 42);
            this.chkInfoAllPlayers.Name = "chkInfoAllPlayers";
            this.chkInfoAllPlayers.Size = new System.Drawing.Size(329, 17);
            this.chkInfoAllPlayers.TabIndex = 24;
            this.chkInfoAllPlayers.Text = "{config_map_info_all_players}";
            this.chkInfoAllPlayers.UseVisualStyleBackColor = true;
            this.chkInfoAllPlayers.CheckedChanged += new System.EventHandler(this.chkInfoAllPlayers_CheckedChanged);
            // 
            // chkInfoAllNPCs
            // 
            this.chkInfoAllNPCs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInfoAllNPCs.AutoEllipsis = true;
            this.tableLayoutPanel6.SetColumnSpan(this.chkInfoAllNPCs, 2);
            this.chkInfoAllNPCs.Location = new System.Drawing.Point(3, 65);
            this.chkInfoAllNPCs.Name = "chkInfoAllNPCs";
            this.chkInfoAllNPCs.Size = new System.Drawing.Size(329, 17);
            this.chkInfoAllNPCs.TabIndex = 25;
            this.chkInfoAllNPCs.Text = "{config_map_info_all_npcs}";
            this.chkInfoAllNPCs.UseVisualStyleBackColor = true;
            this.chkInfoAllNPCs.CheckedChanged += new System.EventHandler(this.chkInfoAllNPCs_CheckedChanged);
            // 
            // chkInfoAllEnemies
            // 
            this.chkInfoAllEnemies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInfoAllEnemies.AutoEllipsis = true;
            this.tableLayoutPanel6.SetColumnSpan(this.chkInfoAllEnemies, 2);
            this.chkInfoAllEnemies.Location = new System.Drawing.Point(3, 88);
            this.chkInfoAllEnemies.Name = "chkInfoAllEnemies";
            this.chkInfoAllEnemies.Size = new System.Drawing.Size(329, 17);
            this.chkInfoAllEnemies.TabIndex = 26;
            this.chkInfoAllEnemies.Text = "{config_map_info_all_enemies}";
            this.chkInfoAllEnemies.UseVisualStyleBackColor = true;
            this.chkInfoAllEnemies.CheckedChanged += new System.EventHandler(this.chkInfoAllEnemies_CheckedChanged);
            // 
            // gMapPack
            // 
            this.gMapPack.AutoSize = true;
            this.gMapPack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gMapPack.Controls.Add(this.tableLayoutPanel3);
            this.gMapPack.Dock = System.Windows.Forms.DockStyle.Top;
            this.gMapPack.Location = new System.Drawing.Point(3, 183);
            this.gMapPack.Name = "gMapPack";
            this.gMapPack.Size = new System.Drawing.Size(341, 131);
            this.gMapPack.TabIndex = 3;
            this.gMapPack.TabStop = false;
            this.gMapPack.Text = "{config_group_mappack}";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.chkShowMapAlternative, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkShowMapAlternativeAlways, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblMapAltOpacityTitle, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblMapImageOpacityPercent, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.tbMapImageOpacity, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblMapPackImagePath, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cmdBrowseMapPackImagePath, 2, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(335, 95);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.pbMapPackWarning);
            this.flowLayoutPanel1.Controls.Add(this.lblMapPackImagePathTitle);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(169, 22);
            this.flowLayoutPanel1.TabIndex = 6;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pbMapPackWarning
            // 
            this.pbMapPackWarning.Image = global::mappy.Properties.Resources.icon_warn;
            this.pbMapPackWarning.Location = new System.Drawing.Point(3, 3);
            this.pbMapPackWarning.Name = "pbMapPackWarning";
            this.pbMapPackWarning.Size = new System.Drawing.Size(16, 16);
            this.pbMapPackWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMapPackWarning.TabIndex = 0;
            this.pbMapPackWarning.TabStop = false;
            this.pbMapPackWarning.Visible = false;
            // 
            // lblMapPackImagePathTitle
            // 
            this.lblMapPackImagePathTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMapPackImagePathTitle.AutoSize = true;
            this.lblMapPackImagePathTitle.Location = new System.Drawing.Point(25, 4);
            this.lblMapPackImagePathTitle.Name = "lblMapPackImagePathTitle";
            this.lblMapPackImagePathTitle.Size = new System.Drawing.Size(141, 13);
            this.lblMapPackImagePathTitle.TabIndex = 19;
            this.lblMapPackImagePathTitle.Text = "{config_map_mappackpath}";
            this.lblMapPackImagePathTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkShowMapAlternative
            // 
            this.chkShowMapAlternative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowMapAlternative.AutoEllipsis = true;
            this.tableLayoutPanel3.SetColumnSpan(this.chkShowMapAlternative, 3);
            this.chkShowMapAlternative.Location = new System.Drawing.Point(3, 3);
            this.chkShowMapAlternative.Name = "chkShowMapAlternative";
            this.chkShowMapAlternative.Size = new System.Drawing.Size(329, 17);
            this.chkShowMapAlternative.TabIndex = 14;
            this.chkShowMapAlternative.Text = "{config_map_alt_show}";
            this.chkShowMapAlternative.UseVisualStyleBackColor = true;
            this.chkShowMapAlternative.CheckedChanged += new System.EventHandler(this.chkShowMapAlternative_CheckedChanged);
            // 
            // chkShowMapAlternativeAlways
            // 
            this.chkShowMapAlternativeAlways.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowMapAlternativeAlways.AutoEllipsis = true;
            this.tableLayoutPanel3.SetColumnSpan(this.chkShowMapAlternativeAlways, 3);
            this.chkShowMapAlternativeAlways.Location = new System.Drawing.Point(3, 51);
            this.chkShowMapAlternativeAlways.Name = "chkShowMapAlternativeAlways";
            this.chkShowMapAlternativeAlways.Size = new System.Drawing.Size(329, 17);
            this.chkShowMapAlternativeAlways.TabIndex = 18;
            this.chkShowMapAlternativeAlways.Text = "{config_map_alt_show_always}";
            this.chkShowMapAlternativeAlways.UseVisualStyleBackColor = true;
            this.chkShowMapAlternativeAlways.CheckedChanged += new System.EventHandler(this.chkShowMapAlternativeAlways_CheckedChanged);
            // 
            // lblMapAltOpacityTitle
            // 
            this.lblMapAltOpacityTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMapAltOpacityTitle.AutoSize = true;
            this.lblMapAltOpacityTitle.Location = new System.Drawing.Point(3, 76);
            this.lblMapAltOpacityTitle.Name = "lblMapAltOpacityTitle";
            this.lblMapAltOpacityTitle.Size = new System.Drawing.Size(127, 13);
            this.lblMapAltOpacityTitle.TabIndex = 10;
            this.lblMapAltOpacityTitle.Text = "{config_map_alt_opacity}";
            // 
            // lblMapImageOpacityPercent
            // 
            this.lblMapImageOpacityPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMapImageOpacityPercent.Location = new System.Drawing.Point(284, 71);
            this.lblMapImageOpacityPercent.Name = "lblMapImageOpacityPercent";
            this.lblMapImageOpacityPercent.Size = new System.Drawing.Size(48, 24);
            this.lblMapImageOpacityPercent.TabIndex = 12;
            this.lblMapImageOpacityPercent.Text = "100%";
            this.lblMapImageOpacityPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMapImageOpacity
            // 
            this.tbMapImageOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMapImageOpacity.AutoSize = false;
            this.tbMapImageOpacity.Location = new System.Drawing.Point(170, 72);
            this.tbMapImageOpacity.Margin = new System.Windows.Forms.Padding(1);
            this.tbMapImageOpacity.Maximum = 100;
            this.tbMapImageOpacity.Name = "tbMapImageOpacity";
            this.tbMapImageOpacity.Size = new System.Drawing.Size(110, 22);
            this.tbMapImageOpacity.TabIndex = 11;
            this.tbMapImageOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMapImageOpacity.Value = 100;
            this.tbMapImageOpacity.Scroll += new System.EventHandler(this.tbMapImageOpacity_Scroll);
            // 
            // lblMapPackImagePath
            // 
            this.lblMapPackImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMapPackImagePath.AutoEllipsis = true;
            this.lblMapPackImagePath.Location = new System.Drawing.Point(172, 29);
            this.lblMapPackImagePath.Name = "lblMapPackImagePath";
            this.lblMapPackImagePath.Size = new System.Drawing.Size(106, 13);
            this.lblMapPackImagePath.TabIndex = 20;
            this.lblMapPackImagePath.Text = "(not set)";
            // 
            // cmdBrowseMapPackImagePath
            // 
            this.cmdBrowseMapPackImagePath.Location = new System.Drawing.Point(282, 24);
            this.cmdBrowseMapPackImagePath.Margin = new System.Windows.Forms.Padding(1);
            this.cmdBrowseMapPackImagePath.Name = "cmdBrowseMapPackImagePath";
            this.cmdBrowseMapPackImagePath.Size = new System.Drawing.Size(52, 23);
            this.cmdBrowseMapPackImagePath.TabIndex = 21;
            this.cmdBrowseMapPackImagePath.Text = "...";
            this.cmdBrowseMapPackImagePath.UseVisualStyleBackColor = true;
            this.cmdBrowseMapPackImagePath.Click += new System.EventHandler(this.cmdBrowseMapPackImagePath_Click);
            // 
            // gBehavior
            // 
            this.gBehavior.AutoSize = true;
            this.gBehavior.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gBehavior.Controls.Add(this.tableLayoutPanel2);
            this.gBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBehavior.Location = new System.Drawing.Point(3, 3);
            this.gBehavior.Name = "gBehavior";
            this.gBehavior.Size = new System.Drawing.Size(341, 180);
            this.gBehavior.TabIndex = 5;
            this.gBehavior.TabStop = false;
            this.gBehavior.Text = "{config_group_behavior}";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblFillColor, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmdChangeBackColor, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblFillOpacity, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbOpacity, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblOpacityPercent, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkAutoRangeSnap, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblMapPath, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.cmdBrowseMapPath, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblPollFreq, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.udPollFrequency, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label8, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.chkScaleLines, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 15);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(335, 146);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.pbFilePathWarning);
            this.flowLayoutPanel2.Controls.Add(this.lblMapPathTitle);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 96);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(145, 22);
            this.flowLayoutPanel2.TabIndex = 6;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // pbFilePathWarning
            // 
            this.pbFilePathWarning.Image = global::mappy.Properties.Resources.icon_warn;
            this.pbFilePathWarning.Location = new System.Drawing.Point(3, 3);
            this.pbFilePathWarning.Name = "pbFilePathWarning";
            this.pbFilePathWarning.Size = new System.Drawing.Size(16, 16);
            this.pbFilePathWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFilePathWarning.TabIndex = 1;
            this.pbFilePathWarning.TabStop = false;
            this.pbFilePathWarning.Visible = false;
            // 
            // lblMapPathTitle
            // 
            this.lblMapPathTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMapPathTitle.AutoSize = true;
            this.lblMapPathTitle.Location = new System.Drawing.Point(25, 4);
            this.lblMapPathTitle.Name = "lblMapPathTitle";
            this.lblMapPathTitle.Size = new System.Drawing.Size(117, 13);
            this.lblMapPathTitle.TabIndex = 15;
            this.lblMapPathTitle.Text = "{config_map_mappath}";
            this.lblMapPathTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFillColor
            // 
            this.lblFillColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFillColor.AutoSize = true;
            this.lblFillColor.Location = new System.Drawing.Point(3, 6);
            this.lblFillColor.Name = "lblFillColor";
            this.lblFillColor.Size = new System.Drawing.Size(139, 13);
            this.lblFillColor.TabIndex = 1;
            this.lblFillColor.Text = "{config_map_fill_color}";
            // 
            // cmdChangeBackColor
            // 
            this.cmdChangeBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdChangeBackColor.BackColor = System.Drawing.Color.Black;
            this.cmdChangeBackColor.Location = new System.Drawing.Point(282, 1);
            this.cmdChangeBackColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdChangeBackColor.Name = "cmdChangeBackColor";
            this.cmdChangeBackColor.Size = new System.Drawing.Size(52, 23);
            this.cmdChangeBackColor.TabIndex = 6;
            this.cmdChangeBackColor.UseVisualStyleBackColor = false;
            this.cmdChangeBackColor.Click += new System.EventHandler(this.cmdChangeBackColor_Click);
            // 
            // lblFillOpacity
            // 
            this.lblFillOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFillOpacity.AutoSize = true;
            this.lblFillOpacity.Location = new System.Drawing.Point(3, 30);
            this.lblFillOpacity.Name = "lblFillOpacity";
            this.lblFillOpacity.Size = new System.Drawing.Size(139, 13);
            this.lblFillOpacity.TabIndex = 8;
            this.lblFillOpacity.Text = "{config_map_fill_opacity}";
            // 
            // tbOpacity
            // 
            this.tbOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOpacity.AutoSize = false;
            this.tbOpacity.Location = new System.Drawing.Point(146, 26);
            this.tbOpacity.Margin = new System.Windows.Forms.Padding(1);
            this.tbOpacity.Maximum = 100;
            this.tbOpacity.Minimum = 1;
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.Size = new System.Drawing.Size(134, 22);
            this.tbOpacity.TabIndex = 8;
            this.tbOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbOpacity.Value = 100;
            this.tbOpacity.Scroll += new System.EventHandler(this.tbOpacity_Scroll);
            // 
            // lblOpacityPercent
            // 
            this.lblOpacityPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOpacityPercent.AutoSize = true;
            this.lblOpacityPercent.Location = new System.Drawing.Point(281, 25);
            this.lblOpacityPercent.Margin = new System.Windows.Forms.Padding(0);
            this.lblOpacityPercent.Name = "lblOpacityPercent";
            this.lblOpacityPercent.Size = new System.Drawing.Size(54, 24);
            this.lblOpacityPercent.TabIndex = 9;
            this.lblOpacityPercent.Text = "100%";
            this.lblOpacityPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkAutoRangeSnap
            // 
            this.chkAutoRangeSnap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoRangeSnap.AutoEllipsis = true;
            this.tableLayoutPanel2.SetColumnSpan(this.chkAutoRangeSnap, 3);
            this.chkAutoRangeSnap.Location = new System.Drawing.Point(3, 52);
            this.chkAutoRangeSnap.Name = "chkAutoRangeSnap";
            this.chkAutoRangeSnap.Size = new System.Drawing.Size(329, 17);
            this.chkAutoRangeSnap.TabIndex = 19;
            this.chkAutoRangeSnap.Text = "{config_map_autosnap}";
            this.chkAutoRangeSnap.UseVisualStyleBackColor = true;
            this.chkAutoRangeSnap.CheckedChanged += new System.EventHandler(this.chkAutoRangeSnap_CheckedChanged);
            // 
            // lblMapPath
            // 
            this.lblMapPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMapPath.AutoEllipsis = true;
            this.lblMapPath.Location = new System.Drawing.Point(148, 101);
            this.lblMapPath.Name = "lblMapPath";
            this.lblMapPath.Size = new System.Drawing.Size(130, 13);
            this.lblMapPath.TabIndex = 16;
            this.lblMapPath.Text = "(not set)";
            // 
            // cmdBrowseMapPath
            // 
            this.cmdBrowseMapPath.Location = new System.Drawing.Point(282, 96);
            this.cmdBrowseMapPath.Margin = new System.Windows.Forms.Padding(1);
            this.cmdBrowseMapPath.Name = "cmdBrowseMapPath";
            this.cmdBrowseMapPath.Size = new System.Drawing.Size(52, 23);
            this.cmdBrowseMapPath.TabIndex = 17;
            this.cmdBrowseMapPath.Text = "...";
            this.cmdBrowseMapPath.UseVisualStyleBackColor = true;
            this.cmdBrowseMapPath.Click += new System.EventHandler(this.cmdBrowseMapPath_Click);
            // 
            // lblPollFreq
            // 
            this.lblPollFreq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPollFreq.AutoSize = true;
            this.lblPollFreq.Location = new System.Drawing.Point(3, 126);
            this.lblPollFreq.Name = "lblPollFreq";
            this.lblPollFreq.Size = new System.Drawing.Size(139, 13);
            this.lblPollFreq.TabIndex = 20;
            this.lblPollFreq.Text = "{config_map_pollfreq}";
            // 
            // udPollFrequency
            // 
            this.udPollFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.udPollFrequency.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udPollFrequency.Location = new System.Drawing.Point(148, 123);
            this.udPollFrequency.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udPollFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPollFrequency.Name = "udPollFrequency";
            this.udPollFrequency.Size = new System.Drawing.Size(130, 20);
            this.udPollFrequency.TabIndex = 21;
            this.udPollFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPollFrequency.ValueChanged += new System.EventHandler(this.udPollFrequency_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(284, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "(ms)";
            // 
            // chkScaleLines
            // 
            this.chkScaleLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkScaleLines.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.chkScaleLines, 3);
            this.chkScaleLines.Location = new System.Drawing.Point(3, 75);
            this.chkScaleLines.Name = "chkScaleLines";
            this.chkScaleLines.Size = new System.Drawing.Size(329, 17);
            this.chkScaleLines.TabIndex = 31;
            this.chkScaleLines.Text = "{config_map_scale_lines}";
            this.chkScaleLines.UseVisualStyleBackColor = true;
            this.chkScaleLines.CheckedChanged += new System.EventHandler(this.chkScaleLines_CheckedChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.chkDepthFilterUseAlpha, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.chkEnableSpawnDepthFilter, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.chkEnableLineDepthFilter, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblDepthDistance, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtDepthDistance, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblDepthCutoff, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtDepthCutoff, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.lblDepthAlphaMin, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.udDepthAlphaMin, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.lblDepthAlphaMax, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.udDepthAlphaMax, 1, 6);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(358, 173);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // chkDepthFilterUseAlpha
            // 
            this.chkDepthFilterUseAlpha.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.chkDepthFilterUseAlpha, 2);
            this.chkDepthFilterUseAlpha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDepthFilterUseAlpha.Location = new System.Drawing.Point(3, 101);
            this.chkDepthFilterUseAlpha.Name = "chkDepthFilterUseAlpha";
            this.chkDepthFilterUseAlpha.Size = new System.Drawing.Size(352, 17);
            this.chkDepthFilterUseAlpha.TabIndex = 20;
            this.chkDepthFilterUseAlpha.Text = "{config_depth_use_alpha}";
            this.chkDepthFilterUseAlpha.UseVisualStyleBackColor = true;
            this.chkDepthFilterUseAlpha.CheckedChanged += new System.EventHandler(this.chkDepthFilterUseAlpha_CheckedChanged);
            // 
            // chkEnableSpawnDepthFilter
            // 
            this.chkEnableSpawnDepthFilter.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.chkEnableSpawnDepthFilter, 2);
            this.chkEnableSpawnDepthFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkEnableSpawnDepthFilter.Location = new System.Drawing.Point(3, 26);
            this.chkEnableSpawnDepthFilter.Name = "chkEnableSpawnDepthFilter";
            this.chkEnableSpawnDepthFilter.Size = new System.Drawing.Size(352, 17);
            this.chkEnableSpawnDepthFilter.TabIndex = 18;
            this.chkEnableSpawnDepthFilter.Text = "{config_depth_enable_spawn_filter}";
            this.chkEnableSpawnDepthFilter.UseVisualStyleBackColor = true;
            this.chkEnableSpawnDepthFilter.CheckedChanged += new System.EventHandler(this.chkEnableSpawnDepthFilter_CheckedChanged);
            // 
            // chkEnableLineDepthFilter
            // 
            this.chkEnableLineDepthFilter.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.chkEnableLineDepthFilter, 2);
            this.chkEnableLineDepthFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkEnableLineDepthFilter.Location = new System.Drawing.Point(3, 3);
            this.chkEnableLineDepthFilter.Name = "chkEnableLineDepthFilter";
            this.chkEnableLineDepthFilter.Size = new System.Drawing.Size(352, 17);
            this.chkEnableLineDepthFilter.TabIndex = 14;
            this.chkEnableLineDepthFilter.Text = "{config_depth_enable_line_filter}";
            this.chkEnableLineDepthFilter.UseVisualStyleBackColor = true;
            this.chkEnableLineDepthFilter.CheckedChanged += new System.EventHandler(this.chkEnableLineDepthFilter_CheckedChanged);
            // 
            // lblDepthDistance
            // 
            this.lblDepthDistance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepthDistance.AutoSize = true;
            this.lblDepthDistance.Location = new System.Drawing.Point(3, 52);
            this.lblDepthDistance.Name = "lblDepthDistance";
            this.lblDepthDistance.Size = new System.Drawing.Size(134, 13);
            this.lblDepthDistance.TabIndex = 22;
            this.lblDepthDistance.Text = "{config_depth_distance}";
            // 
            // txtDepthDistance
            // 
            this.txtDepthDistance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepthDistance.Location = new System.Drawing.Point(143, 49);
            this.txtDepthDistance.Name = "txtDepthDistance";
            this.txtDepthDistance.Size = new System.Drawing.Size(212, 20);
            this.txtDepthDistance.TabIndex = 26;
            this.txtDepthDistance.Validating += new System.ComponentModel.CancelEventHandler(this.txtDepthDistance_Validating);
            // 
            // lblDepthCutoff
            // 
            this.lblDepthCutoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepthCutoff.AutoSize = true;
            this.lblDepthCutoff.Location = new System.Drawing.Point(3, 78);
            this.lblDepthCutoff.Name = "lblDepthCutoff";
            this.lblDepthCutoff.Size = new System.Drawing.Size(134, 13);
            this.lblDepthCutoff.TabIndex = 23;
            this.lblDepthCutoff.Text = "{config_depth_cutoff}";
            // 
            // txtDepthCutoff
            // 
            this.txtDepthCutoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepthCutoff.Location = new System.Drawing.Point(143, 75);
            this.txtDepthCutoff.Name = "txtDepthCutoff";
            this.txtDepthCutoff.Size = new System.Drawing.Size(212, 20);
            this.txtDepthCutoff.TabIndex = 27;
            this.txtDepthCutoff.Validating += new System.ComponentModel.CancelEventHandler(this.txtDepthCutoff_Validating);
            // 
            // lblDepthAlphaMin
            // 
            this.lblDepthAlphaMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepthAlphaMin.AutoSize = true;
            this.lblDepthAlphaMin.Location = new System.Drawing.Point(3, 127);
            this.lblDepthAlphaMin.Name = "lblDepthAlphaMin";
            this.lblDepthAlphaMin.Size = new System.Drawing.Size(134, 13);
            this.lblDepthAlphaMin.TabIndex = 10;
            this.lblDepthAlphaMin.Text = "{config_depth_alpha_min}";
            // 
            // udDepthAlphaMin
            // 
            this.udDepthAlphaMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.udDepthAlphaMin.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udDepthAlphaMin.Location = new System.Drawing.Point(143, 124);
            this.udDepthAlphaMin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udDepthAlphaMin.Name = "udDepthAlphaMin";
            this.udDepthAlphaMin.Size = new System.Drawing.Size(212, 20);
            this.udDepthAlphaMin.TabIndex = 24;
            this.udDepthAlphaMin.ValueChanged += new System.EventHandler(this.udDepthAlphaMin_ValueChanged);
            // 
            // lblDepthAlphaMax
            // 
            this.lblDepthAlphaMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepthAlphaMax.AutoSize = true;
            this.lblDepthAlphaMax.Location = new System.Drawing.Point(3, 153);
            this.lblDepthAlphaMax.Name = "lblDepthAlphaMax";
            this.lblDepthAlphaMax.Size = new System.Drawing.Size(134, 13);
            this.lblDepthAlphaMax.TabIndex = 21;
            this.lblDepthAlphaMax.Text = "{config_depth_alpha_max}";
            // 
            // udDepthAlphaMax
            // 
            this.udDepthAlphaMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.udDepthAlphaMax.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udDepthAlphaMax.Location = new System.Drawing.Point(143, 150);
            this.udDepthAlphaMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udDepthAlphaMax.Name = "udDepthAlphaMax";
            this.udDepthAlphaMax.Size = new System.Drawing.Size(212, 20);
            this.udDepthAlphaMax.TabIndex = 25;
            this.udDepthAlphaMax.ValueChanged += new System.EventHandler(this.udDepthAlphaMax_ValueChanged);
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpMap);
            this.tcOptions.Controls.Add(this.tpAppearance);
            this.tcOptions.Controls.Add(this.tpDepthFilter);
            this.tcOptions.Controls.Add(this.tpHotkeys);
            this.tcOptions.Controls.Add(this.tpSignatures);
            this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOptions.Location = new System.Drawing.Point(0, 0);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(372, 432);
            this.tcOptions.TabIndex = 8;
            this.tcOptions.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcOptions_Selected);
            // 
            // tpAppearance
            // 
            this.tpAppearance.AutoScroll = true;
            this.tpAppearance.Controls.Add(this.tableLayoutPanel4);
            this.tpAppearance.Location = new System.Drawing.Point(4, 22);
            this.tpAppearance.Name = "tpAppearance";
            this.tpAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tpAppearance.Size = new System.Drawing.Size(364, 406);
            this.tpAppearance.TabIndex = 1;
            this.tpAppearance.Text = "{tab_appearance}";
            this.tpAppearance.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel4.Controls.Add(this.chkShowPlayerPosition, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.lblAppearSectionGrid, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.chkShowGridLines, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.chkShowGridTicks, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.lblGridSize, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.udGridSize, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.lblGridLineColor, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.cmdGridLineColor, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.lblGridTickColor, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.cmdGridTickColor, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.lblAppearSectionSpawn, 0, 8);
            this.tableLayoutPanel4.Controls.Add(this.lblPlayerSize, 0, 9);
            this.tableLayoutPanel4.Controls.Add(this.udPlayerSize, 1, 9);
            this.tableLayoutPanel4.Controls.Add(this.chkUsePlayerColor, 0, 10);
            this.tableLayoutPanel4.Controls.Add(this.cmdPlayerColor, 1, 10);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel4, 0, 11);
            this.tableLayoutPanel4.Controls.Add(this.cmdPlayerInfoFont, 1, 11);
            this.tableLayoutPanel4.Controls.Add(this.lblPlayerInfoColor, 0, 12);
            this.tableLayoutPanel4.Controls.Add(this.cmdPlayerInfoColor, 1, 12);
            this.tableLayoutPanel4.Controls.Add(this.lblYouColor, 0, 13);
            this.tableLayoutPanel4.Controls.Add(this.cmdMainPlayerColor, 1, 13);
            this.tableLayoutPanel4.Controls.Add(this.chkUseMainPlayerFill, 0, 14);
            this.tableLayoutPanel4.Controls.Add(this.lblYouFillColor, 0, 15);
            this.tableLayoutPanel4.Controls.Add(this.cmdMainPlayerFillColor, 1, 15);
            this.tableLayoutPanel4.Controls.Add(this.lblGroupColor, 0, 16);
            this.tableLayoutPanel4.Controls.Add(this.cmdGroupMemberColor, 1, 16);
            this.tableLayoutPanel4.Controls.Add(this.lblRaidColor, 0, 17);
            this.tableLayoutPanel4.Controls.Add(this.cmdRaidMemberColor, 1, 17);
            this.tableLayoutPanel4.Controls.Add(this.lblNPCSize, 0, 18);
            this.tableLayoutPanel4.Controls.Add(this.udNPCSize, 1, 18);
            this.tableLayoutPanel4.Controls.Add(this.lblNPCColor, 0, 19);
            this.tableLayoutPanel4.Controls.Add(this.cmdNPCColor, 1, 19);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel5, 0, 20);
            this.tableLayoutPanel4.Controls.Add(this.cmdNPCInfoFont, 1, 20);
            this.tableLayoutPanel4.Controls.Add(this.lblNPCInfoColor, 0, 21);
            this.tableLayoutPanel4.Controls.Add(this.cmdNPCInfoColor, 1, 21);
            this.tableLayoutPanel4.Controls.Add(this.lblMOBSize, 0, 22);
            this.tableLayoutPanel4.Controls.Add(this.udMOBSize, 1, 22);
            this.tableLayoutPanel4.Controls.Add(this.lblEnemyColor, 0, 23);
            this.tableLayoutPanel4.Controls.Add(this.cmdEnemyColor, 1, 23);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel6, 0, 24);
            this.tableLayoutPanel4.Controls.Add(this.cmdMOBInfoFont, 1, 24);
            this.tableLayoutPanel4.Controls.Add(this.lblMOBInfoColor, 0, 25);
            this.tableLayoutPanel4.Controls.Add(this.cmdMOBInfoColor, 1, 25);
            this.tableLayoutPanel4.Controls.Add(this.lblSpawnSelectSize, 0, 26);
            this.tableLayoutPanel4.Controls.Add(this.udSpawnSelectSize, 1, 26);
            this.tableLayoutPanel4.Controls.Add(this.lblSelectedColor, 0, 27);
            this.tableLayoutPanel4.Controls.Add(this.cmdSelectedColor, 1, 27);
            this.tableLayoutPanel4.Controls.Add(this.lblTextGlowColor, 0, 29);
            this.tableLayoutPanel4.Controls.Add(this.cmdTextGlowColor, 1, 29);
            this.tableLayoutPanel4.Controls.Add(this.udSpawnGroupSize, 1, 30);
            this.tableLayoutPanel4.Controls.Add(this.lblSpawnGroupSize, 0, 30);
            this.tableLayoutPanel4.Controls.Add(this.lblSpawnHuntSize, 0, 31);
            this.tableLayoutPanel4.Controls.Add(this.udSpawnHuntSize, 1, 31);
            this.tableLayoutPanel4.Controls.Add(this.chkDrawHeadingLines, 0, 32);
            this.tableLayoutPanel4.Controls.Add(this.lblHeadingLineColor, 0, 33);
            this.tableLayoutPanel4.Controls.Add(this.cmdHeadingLineColor, 1, 33);
            this.tableLayoutPanel4.Controls.Add(this.chkShowRadarRange, 0, 34);
            this.tableLayoutPanel4.Controls.Add(this.lblRadarRangeColor, 0, 35);
            this.tableLayoutPanel4.Controls.Add(this.cmdRadarRangeColor, 1, 35);
            this.tableLayoutPanel4.Controls.Add(this.lblAppearSectionLines, 0, 36);
            this.tableLayoutPanel4.Controls.Add(this.chkShowHuntLines, 0, 37);
            this.tableLayoutPanel4.Controls.Add(this.lblHuntLineColor, 0, 38);
            this.tableLayoutPanel4.Controls.Add(this.cmdHuntLineColor, 1, 38);
            this.tableLayoutPanel4.Controls.Add(this.lblHuntLockedLineColor, 0, 39);
            this.tableLayoutPanel4.Controls.Add(this.cmdHuntLockedLineColor, 1, 39);
            this.tableLayoutPanel4.Controls.Add(this.chkShowClaimLines, 0, 40);
            this.tableLayoutPanel4.Controls.Add(this.lblClaimLineColor, 0, 41);
            this.tableLayoutPanel4.Controls.Add(this.cmdClaimLineColor, 1, 41);
            this.tableLayoutPanel4.Controls.Add(this.chkShowPetLines, 0, 42);
            this.tableLayoutPanel4.Controls.Add(this.lblPetLineColor, 0, 43);
            this.tableLayoutPanel4.Controls.Add(this.cmdPetLineColor, 1, 43);
            this.tableLayoutPanel4.Controls.Add(this.chkShowTextOutline, 0, 29);
            this.tableLayoutPanel4.Controls.Add(this.chkShowTextShadow, 0, 28);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 45;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(341, 955);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // chkShowPlayerPosition
            // 
            this.chkShowPlayerPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowPlayerPosition.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowPlayerPosition, 2);
            this.chkShowPlayerPosition.Location = new System.Drawing.Point(3, 130);
            this.chkShowPlayerPosition.Name = "chkShowPlayerPosition";
            this.chkShowPlayerPosition.Size = new System.Drawing.Size(335, 17);
            this.chkShowPlayerPosition.TabIndex = 81;
            this.chkShowPlayerPosition.Text = "{config_appear_show_player_position}";
            this.chkShowPlayerPosition.UseVisualStyleBackColor = true;
            this.chkShowPlayerPosition.CheckedChanged += new System.EventHandler(this.chkShowPlayerPosition_CheckedChanged);
            // 
            // lblAppearSectionGrid
            // 
            this.lblAppearSectionGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAppearSectionGrid.AutoSize = true;
            this.lblAppearSectionGrid.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableLayoutPanel4.SetColumnSpan(this.lblAppearSectionGrid, 2);
            this.lblAppearSectionGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppearSectionGrid.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblAppearSectionGrid.Location = new System.Drawing.Point(3, 0);
            this.lblAppearSectionGrid.Name = "lblAppearSectionGrid";
            this.lblAppearSectionGrid.Padding = new System.Windows.Forms.Padding(1);
            this.lblAppearSectionGrid.Size = new System.Drawing.Size(335, 15);
            this.lblAppearSectionGrid.TabIndex = 52;
            this.lblAppearSectionGrid.Text = "{config_appear_section_grid}";
            this.lblAppearSectionGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkShowGridLines
            // 
            this.chkShowGridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowGridLines.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowGridLines, 2);
            this.chkShowGridLines.Location = new System.Drawing.Point(3, 18);
            this.chkShowGridLines.Name = "chkShowGridLines";
            this.chkShowGridLines.Size = new System.Drawing.Size(335, 17);
            this.chkShowGridLines.TabIndex = 8;
            this.chkShowGridLines.Text = "{config_appear_show_grid_lines}";
            this.chkShowGridLines.UseVisualStyleBackColor = true;
            this.chkShowGridLines.CheckedChanged += new System.EventHandler(this.chkShowGridLines_CheckedChanged);
            // 
            // chkShowGridTicks
            // 
            this.chkShowGridTicks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowGridTicks.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowGridTicks, 2);
            this.chkShowGridTicks.Location = new System.Drawing.Point(3, 41);
            this.chkShowGridTicks.Name = "chkShowGridTicks";
            this.chkShowGridTicks.Size = new System.Drawing.Size(335, 17);
            this.chkShowGridTicks.TabIndex = 9;
            this.chkShowGridTicks.Text = "{config_appear_show_grid_ticks}";
            this.chkShowGridTicks.UseVisualStyleBackColor = true;
            this.chkShowGridTicks.CheckedChanged += new System.EventHandler(this.chkShowGridTicks_CheckedChanged);
            // 
            // lblGridSize
            // 
            this.lblGridSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Location = new System.Drawing.Point(3, 67);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(279, 13);
            this.lblGridSize.TabIndex = 28;
            this.lblGridSize.Text = "{config_appear_grid_size}";
            // 
            // udGridSize
            // 
            this.udGridSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udGridSize.Location = new System.Drawing.Point(288, 64);
            this.udGridSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udGridSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udGridSize.Name = "udGridSize";
            this.udGridSize.Size = new System.Drawing.Size(50, 20);
            this.udGridSize.TabIndex = 29;
            this.udGridSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udGridSize.ValueChanged += new System.EventHandler(this.udGridSize_ValueChanged);
            // 
            // lblGridLineColor
            // 
            this.lblGridLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGridLineColor.AutoSize = true;
            this.lblGridLineColor.Location = new System.Drawing.Point(3, 90);
            this.lblGridLineColor.Name = "lblGridLineColor";
            this.lblGridLineColor.Size = new System.Drawing.Size(279, 13);
            this.lblGridLineColor.TabIndex = 1;
            this.lblGridLineColor.Text = "{config_appear_grid_line_color}";
            // 
            // cmdGridLineColor
            // 
            this.cmdGridLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGridLineColor.BackColor = System.Drawing.Color.Black;
            this.cmdGridLineColor.Location = new System.Drawing.Point(286, 88);
            this.cmdGridLineColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdGridLineColor.Name = "cmdGridLineColor";
            this.cmdGridLineColor.Size = new System.Drawing.Size(54, 18);
            this.cmdGridLineColor.TabIndex = 0;
            this.cmdGridLineColor.UseVisualStyleBackColor = false;
            this.cmdGridLineColor.Click += new System.EventHandler(this.cmdGridLineColor_Click);
            // 
            // lblGridTickColor
            // 
            this.lblGridTickColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGridTickColor.AutoSize = true;
            this.lblGridTickColor.Location = new System.Drawing.Point(3, 110);
            this.lblGridTickColor.Name = "lblGridTickColor";
            this.lblGridTickColor.Size = new System.Drawing.Size(279, 13);
            this.lblGridTickColor.TabIndex = 2;
            this.lblGridTickColor.Text = "{config_appear_grid_tick_color}";
            // 
            // cmdGridTickColor
            // 
            this.cmdGridTickColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGridTickColor.BackColor = System.Drawing.Color.Black;
            this.cmdGridTickColor.Location = new System.Drawing.Point(286, 108);
            this.cmdGridTickColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdGridTickColor.Name = "cmdGridTickColor";
            this.cmdGridTickColor.Size = new System.Drawing.Size(54, 18);
            this.cmdGridTickColor.TabIndex = 5;
            this.cmdGridTickColor.UseVisualStyleBackColor = false;
            this.cmdGridTickColor.Click += new System.EventHandler(this.cmdGridTickColor_Click);
            // 
            // lblAppearSectionSpawn
            // 
            this.lblAppearSectionSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAppearSectionSpawn.AutoSize = true;
            this.lblAppearSectionSpawn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableLayoutPanel4.SetColumnSpan(this.lblAppearSectionSpawn, 2);
            this.lblAppearSectionSpawn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppearSectionSpawn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblAppearSectionSpawn.Location = new System.Drawing.Point(3, 150);
            this.lblAppearSectionSpawn.Name = "lblAppearSectionSpawn";
            this.lblAppearSectionSpawn.Padding = new System.Windows.Forms.Padding(1);
            this.lblAppearSectionSpawn.Size = new System.Drawing.Size(335, 15);
            this.lblAppearSectionSpawn.TabIndex = 50;
            this.lblAppearSectionSpawn.Text = "{config_appear_section_spawn}";
            this.lblAppearSectionSpawn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlayerSize
            // 
            this.lblPlayerSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerSize.AutoSize = true;
            this.lblPlayerSize.Location = new System.Drawing.Point(3, 171);
            this.lblPlayerSize.Name = "lblPlayerSize";
            this.lblPlayerSize.Size = new System.Drawing.Size(279, 13);
            this.lblPlayerSize.TabIndex = 42;
            this.lblPlayerSize.Text = "{config_appear_player_size}";
            // 
            // udPlayerSize
            // 
            this.udPlayerSize.DecimalPlaces = 1;
            this.udPlayerSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udPlayerSize.Location = new System.Drawing.Point(288, 168);
            this.udPlayerSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPlayerSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPlayerSize.Name = "udPlayerSize";
            this.udPlayerSize.Size = new System.Drawing.Size(50, 20);
            this.udPlayerSize.TabIndex = 46;
            this.udPlayerSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udPlayerSize.ValueChanged += new System.EventHandler(this.udPlayerSize_ValueChanged);
            // 
            // chkUsePlayerColor
            // 
            this.chkUsePlayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUsePlayerColor.AutoEllipsis = true;
            this.chkUsePlayerColor.Location = new System.Drawing.Point(3, 194);
            this.chkUsePlayerColor.Name = "chkUsePlayerColor";
            this.chkUsePlayerColor.Size = new System.Drawing.Size(279, 17);
            this.chkUsePlayerColor.TabIndex = 39;
            this.chkUsePlayerColor.Text = "{config_appear_use_player_color}";
            this.chkUsePlayerColor.UseVisualStyleBackColor = true;
            this.chkUsePlayerColor.CheckedChanged += new System.EventHandler(this.chkUsePlayerColor_CheckedChanged);
            // 
            // cmdPlayerColor
            // 
            this.cmdPlayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPlayerColor.BackColor = System.Drawing.Color.Black;
            this.cmdPlayerColor.Location = new System.Drawing.Point(286, 193);
            this.cmdPlayerColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdPlayerColor.Name = "cmdPlayerColor";
            this.cmdPlayerColor.Size = new System.Drawing.Size(54, 18);
            this.cmdPlayerColor.TabIndex = 41;
            this.cmdPlayerColor.UseVisualStyleBackColor = false;
            this.cmdPlayerColor.Click += new System.EventHandler(this.cmdPlayerColor_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.lblPlayerInfoFont);
            this.flowLayoutPanel4.Controls.Add(this.lblPlayerInfoFontDisplay);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 220);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(215, 13);
            this.flowLayoutPanel4.TabIndex = 70;
            // 
            // lblPlayerInfoFont
            // 
            this.lblPlayerInfoFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerInfoFont.AutoSize = true;
            this.lblPlayerInfoFont.Location = new System.Drawing.Point(3, 0);
            this.lblPlayerInfoFont.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblPlayerInfoFont.Name = "lblPlayerInfoFont";
            this.lblPlayerInfoFont.Size = new System.Drawing.Size(164, 13);
            this.lblPlayerInfoFont.TabIndex = 54;
            this.lblPlayerInfoFont.Text = "{config_appear_player_info_font}";
            this.lblPlayerInfoFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlayerInfoFontDisplay
            // 
            this.lblPlayerInfoFontDisplay.AutoSize = true;
            this.lblPlayerInfoFontDisplay.Location = new System.Drawing.Point(167, 0);
            this.lblPlayerInfoFontDisplay.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblPlayerInfoFontDisplay.Name = "lblPlayerInfoFontDisplay";
            this.lblPlayerInfoFontDisplay.Size = new System.Drawing.Size(45, 13);
            this.lblPlayerInfoFontDisplay.TabIndex = 55;
            this.lblPlayerInfoFontDisplay.Text = "(not set)";
            this.lblPlayerInfoFontDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdPlayerInfoFont
            // 
            this.cmdPlayerInfoFont.AutoSize = true;
            this.cmdPlayerInfoFont.Location = new System.Drawing.Point(286, 215);
            this.cmdPlayerInfoFont.Margin = new System.Windows.Forms.Padding(1);
            this.cmdPlayerInfoFont.Name = "cmdPlayerInfoFont";
            this.cmdPlayerInfoFont.Size = new System.Drawing.Size(54, 23);
            this.cmdPlayerInfoFont.TabIndex = 55;
            this.cmdPlayerInfoFont.Text = "...";
            this.cmdPlayerInfoFont.UseVisualStyleBackColor = true;
            this.cmdPlayerInfoFont.Click += new System.EventHandler(this.cmdPlayerInfoFont_Click);
            // 
            // lblPlayerInfoColor
            // 
            this.lblPlayerInfoColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerInfoColor.AutoSize = true;
            this.lblPlayerInfoColor.Location = new System.Drawing.Point(3, 242);
            this.lblPlayerInfoColor.Name = "lblPlayerInfoColor";
            this.lblPlayerInfoColor.Size = new System.Drawing.Size(279, 13);
            this.lblPlayerInfoColor.TabIndex = 56;
            this.lblPlayerInfoColor.Text = "{config_appear_player_info_color}";
            // 
            // cmdPlayerInfoColor
            // 
            this.cmdPlayerInfoColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPlayerInfoColor.BackColor = System.Drawing.Color.Black;
            this.cmdPlayerInfoColor.Location = new System.Drawing.Point(286, 240);
            this.cmdPlayerInfoColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdPlayerInfoColor.Name = "cmdPlayerInfoColor";
            this.cmdPlayerInfoColor.Size = new System.Drawing.Size(54, 18);
            this.cmdPlayerInfoColor.TabIndex = 57;
            this.cmdPlayerInfoColor.UseVisualStyleBackColor = false;
            this.cmdPlayerInfoColor.Click += new System.EventHandler(this.cmdPlayerInfoColor_Click);
            // 
            // lblYouColor
            // 
            this.lblYouColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblYouColor.AutoSize = true;
            this.lblYouColor.Location = new System.Drawing.Point(3, 262);
            this.lblYouColor.Name = "lblYouColor";
            this.lblYouColor.Size = new System.Drawing.Size(279, 13);
            this.lblYouColor.TabIndex = 31;
            this.lblYouColor.Text = "{config_appear_you_color}";
            // 
            // cmdMainPlayerColor
            // 
            this.cmdMainPlayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMainPlayerColor.BackColor = System.Drawing.Color.Black;
            this.cmdMainPlayerColor.Location = new System.Drawing.Point(286, 260);
            this.cmdMainPlayerColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdMainPlayerColor.Name = "cmdMainPlayerColor";
            this.cmdMainPlayerColor.Size = new System.Drawing.Size(54, 18);
            this.cmdMainPlayerColor.TabIndex = 32;
            this.cmdMainPlayerColor.UseVisualStyleBackColor = false;
            this.cmdMainPlayerColor.Click += new System.EventHandler(this.cmdMainPlayerColor_Click);
            // 
            // chkUseMainPlayerFill
            // 
            this.chkUseMainPlayerFill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseMainPlayerFill.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkUseMainPlayerFill, 2);
            this.chkUseMainPlayerFill.Location = new System.Drawing.Point(3, 282);
            this.chkUseMainPlayerFill.Name = "chkUseMainPlayerFill";
            this.chkUseMainPlayerFill.Size = new System.Drawing.Size(335, 17);
            this.chkUseMainPlayerFill.TabIndex = 33;
            this.chkUseMainPlayerFill.Text = "{config_appear_you_fill_enabled}";
            this.chkUseMainPlayerFill.UseVisualStyleBackColor = true;
            this.chkUseMainPlayerFill.CheckedChanged += new System.EventHandler(this.chkUseMainPlayerFill_CheckedChanged);
            // 
            // lblYouFillColor
            // 
            this.lblYouFillColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblYouFillColor.AutoSize = true;
            this.lblYouFillColor.Location = new System.Drawing.Point(3, 305);
            this.lblYouFillColor.Name = "lblYouFillColor";
            this.lblYouFillColor.Size = new System.Drawing.Size(279, 13);
            this.lblYouFillColor.TabIndex = 34;
            this.lblYouFillColor.Text = "{config_appear_you_fill_color}";
            // 
            // cmdMainPlayerFillColor
            // 
            this.cmdMainPlayerFillColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMainPlayerFillColor.BackColor = System.Drawing.Color.Black;
            this.cmdMainPlayerFillColor.Location = new System.Drawing.Point(286, 303);
            this.cmdMainPlayerFillColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdMainPlayerFillColor.Name = "cmdMainPlayerFillColor";
            this.cmdMainPlayerFillColor.Size = new System.Drawing.Size(54, 18);
            this.cmdMainPlayerFillColor.TabIndex = 35;
            this.cmdMainPlayerFillColor.UseVisualStyleBackColor = false;
            this.cmdMainPlayerFillColor.Click += new System.EventHandler(this.cmdMainPlayerFillColor_Click);
            // 
            // lblGroupColor
            // 
            this.lblGroupColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGroupColor.AutoSize = true;
            this.lblGroupColor.Location = new System.Drawing.Point(3, 325);
            this.lblGroupColor.Name = "lblGroupColor";
            this.lblGroupColor.Size = new System.Drawing.Size(279, 13);
            this.lblGroupColor.TabIndex = 24;
            this.lblGroupColor.Text = "{config_appear_group_color}";
            // 
            // cmdGroupMemberColor
            // 
            this.cmdGroupMemberColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGroupMemberColor.BackColor = System.Drawing.Color.Black;
            this.cmdGroupMemberColor.Location = new System.Drawing.Point(286, 323);
            this.cmdGroupMemberColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdGroupMemberColor.Name = "cmdGroupMemberColor";
            this.cmdGroupMemberColor.Size = new System.Drawing.Size(54, 18);
            this.cmdGroupMemberColor.TabIndex = 25;
            this.cmdGroupMemberColor.UseVisualStyleBackColor = false;
            this.cmdGroupMemberColor.Click += new System.EventHandler(this.cmdGroupMemberColor_Click);
            // 
            // lblRaidColor
            // 
            this.lblRaidColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRaidColor.AutoSize = true;
            this.lblRaidColor.Location = new System.Drawing.Point(3, 345);
            this.lblRaidColor.Name = "lblRaidColor";
            this.lblRaidColor.Size = new System.Drawing.Size(279, 13);
            this.lblRaidColor.TabIndex = 26;
            this.lblRaidColor.Text = "{config_appear_raid_color}";
            // 
            // cmdRaidMemberColor
            // 
            this.cmdRaidMemberColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRaidMemberColor.BackColor = System.Drawing.Color.Black;
            this.cmdRaidMemberColor.Location = new System.Drawing.Point(286, 343);
            this.cmdRaidMemberColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdRaidMemberColor.Name = "cmdRaidMemberColor";
            this.cmdRaidMemberColor.Size = new System.Drawing.Size(54, 18);
            this.cmdRaidMemberColor.TabIndex = 27;
            this.cmdRaidMemberColor.UseVisualStyleBackColor = false;
            this.cmdRaidMemberColor.Click += new System.EventHandler(this.cmdRaidMemberColor_Click);
            // 
            // lblNPCSize
            // 
            this.lblNPCSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNPCSize.AutoSize = true;
            this.lblNPCSize.Location = new System.Drawing.Point(3, 368);
            this.lblNPCSize.Name = "lblNPCSize";
            this.lblNPCSize.Size = new System.Drawing.Size(279, 13);
            this.lblNPCSize.TabIndex = 66;
            this.lblNPCSize.Text = "{config_appear_npc_size}";
            // 
            // udNPCSize
            // 
            this.udNPCSize.DecimalPlaces = 1;
            this.udNPCSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udNPCSize.Location = new System.Drawing.Point(288, 365);
            this.udNPCSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udNPCSize.Name = "udNPCSize";
            this.udNPCSize.Size = new System.Drawing.Size(50, 20);
            this.udNPCSize.TabIndex = 67;
            this.udNPCSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udNPCSize.ValueChanged += new System.EventHandler(this.udNPCSize_ValueChanged);
            // 
            // lblNPCColor
            // 
            this.lblNPCColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNPCColor.AutoSize = true;
            this.lblNPCColor.Location = new System.Drawing.Point(3, 391);
            this.lblNPCColor.Name = "lblNPCColor";
            this.lblNPCColor.Size = new System.Drawing.Size(279, 13);
            this.lblNPCColor.TabIndex = 3;
            this.lblNPCColor.Text = "{config_appear_npc_color}";
            // 
            // cmdNPCColor
            // 
            this.cmdNPCColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNPCColor.BackColor = System.Drawing.Color.Black;
            this.cmdNPCColor.Location = new System.Drawing.Point(286, 389);
            this.cmdNPCColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdNPCColor.Name = "cmdNPCColor";
            this.cmdNPCColor.Size = new System.Drawing.Size(54, 18);
            this.cmdNPCColor.TabIndex = 6;
            this.cmdNPCColor.UseVisualStyleBackColor = false;
            this.cmdNPCColor.Click += new System.EventHandler(this.cmdNPCColor_Click);
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.lblNPCInfoFont);
            this.flowLayoutPanel5.Controls.Add(this.lblNPCInfoFontDisplay);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(0, 414);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(205, 13);
            this.flowLayoutPanel5.TabIndex = 65;
            // 
            // lblNPCInfoFont
            // 
            this.lblNPCInfoFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNPCInfoFont.AutoSize = true;
            this.lblNPCInfoFont.Location = new System.Drawing.Point(3, 0);
            this.lblNPCInfoFont.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblNPCInfoFont.Name = "lblNPCInfoFont";
            this.lblNPCInfoFont.Size = new System.Drawing.Size(154, 13);
            this.lblNPCInfoFont.TabIndex = 54;
            this.lblNPCInfoFont.Text = "{config_appear_npc_info_font}";
            this.lblNPCInfoFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNPCInfoFontDisplay
            // 
            this.lblNPCInfoFontDisplay.AutoSize = true;
            this.lblNPCInfoFontDisplay.Location = new System.Drawing.Point(157, 0);
            this.lblNPCInfoFontDisplay.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblNPCInfoFontDisplay.Name = "lblNPCInfoFontDisplay";
            this.lblNPCInfoFontDisplay.Size = new System.Drawing.Size(45, 13);
            this.lblNPCInfoFontDisplay.TabIndex = 55;
            this.lblNPCInfoFontDisplay.Text = "(not set)";
            this.lblNPCInfoFontDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdNPCInfoFont
            // 
            this.cmdNPCInfoFont.AutoSize = true;
            this.cmdNPCInfoFont.Location = new System.Drawing.Point(286, 409);
            this.cmdNPCInfoFont.Margin = new System.Windows.Forms.Padding(1);
            this.cmdNPCInfoFont.Name = "cmdNPCInfoFont";
            this.cmdNPCInfoFont.Size = new System.Drawing.Size(54, 23);
            this.cmdNPCInfoFont.TabIndex = 73;
            this.cmdNPCInfoFont.Text = "...";
            this.cmdNPCInfoFont.UseVisualStyleBackColor = true;
            this.cmdNPCInfoFont.Click += new System.EventHandler(this.cmdNPCInfoFont_Click);
            // 
            // lblNPCInfoColor
            // 
            this.lblNPCInfoColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNPCInfoColor.AutoSize = true;
            this.lblNPCInfoColor.Location = new System.Drawing.Point(3, 436);
            this.lblNPCInfoColor.Name = "lblNPCInfoColor";
            this.lblNPCInfoColor.Size = new System.Drawing.Size(279, 13);
            this.lblNPCInfoColor.TabIndex = 71;
            this.lblNPCInfoColor.Text = "{config_appear_npc_info_color}";
            // 
            // cmdNPCInfoColor
            // 
            this.cmdNPCInfoColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNPCInfoColor.BackColor = System.Drawing.Color.Black;
            this.cmdNPCInfoColor.Location = new System.Drawing.Point(286, 434);
            this.cmdNPCInfoColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdNPCInfoColor.Name = "cmdNPCInfoColor";
            this.cmdNPCInfoColor.Size = new System.Drawing.Size(54, 18);
            this.cmdNPCInfoColor.TabIndex = 72;
            this.cmdNPCInfoColor.UseVisualStyleBackColor = false;
            this.cmdNPCInfoColor.Click += new System.EventHandler(this.cmdNPCInfoColor_Click);
            // 
            // lblMOBSize
            // 
            this.lblMOBSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMOBSize.AutoSize = true;
            this.lblMOBSize.Location = new System.Drawing.Point(3, 459);
            this.lblMOBSize.Name = "lblMOBSize";
            this.lblMOBSize.Size = new System.Drawing.Size(279, 13);
            this.lblMOBSize.TabIndex = 68;
            this.lblMOBSize.Text = "{config_appear_enemy_size}";
            // 
            // udMOBSize
            // 
            this.udMOBSize.DecimalPlaces = 1;
            this.udMOBSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udMOBSize.Location = new System.Drawing.Point(288, 456);
            this.udMOBSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udMOBSize.Name = "udMOBSize";
            this.udMOBSize.Size = new System.Drawing.Size(50, 20);
            this.udMOBSize.TabIndex = 69;
            this.udMOBSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udMOBSize.ValueChanged += new System.EventHandler(this.udMOBSize_ValueChanged);
            // 
            // lblEnemyColor
            // 
            this.lblEnemyColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnemyColor.AutoSize = true;
            this.lblEnemyColor.Location = new System.Drawing.Point(3, 482);
            this.lblEnemyColor.Name = "lblEnemyColor";
            this.lblEnemyColor.Size = new System.Drawing.Size(279, 13);
            this.lblEnemyColor.TabIndex = 4;
            this.lblEnemyColor.Text = "{config_appear_enemy_color}";
            // 
            // cmdEnemyColor
            // 
            this.cmdEnemyColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnemyColor.BackColor = System.Drawing.Color.Black;
            this.cmdEnemyColor.Location = new System.Drawing.Point(286, 480);
            this.cmdEnemyColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdEnemyColor.Name = "cmdEnemyColor";
            this.cmdEnemyColor.Size = new System.Drawing.Size(54, 18);
            this.cmdEnemyColor.TabIndex = 7;
            this.cmdEnemyColor.UseVisualStyleBackColor = false;
            this.cmdEnemyColor.Click += new System.EventHandler(this.cmdEnemyColor_Click);
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.lblMOBInfoFont);
            this.flowLayoutPanel6.Controls.Add(this.lblMOBInfoFontDisplay);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(0, 505);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(218, 13);
            this.flowLayoutPanel6.TabIndex = 74;
            // 
            // lblMOBInfoFont
            // 
            this.lblMOBInfoFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMOBInfoFont.AutoSize = true;
            this.lblMOBInfoFont.Location = new System.Drawing.Point(3, 0);
            this.lblMOBInfoFont.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblMOBInfoFont.Name = "lblMOBInfoFont";
            this.lblMOBInfoFont.Size = new System.Drawing.Size(167, 13);
            this.lblMOBInfoFont.TabIndex = 54;
            this.lblMOBInfoFont.Text = "{config_appear_enemy_info_font}";
            this.lblMOBInfoFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOBInfoFontDisplay
            // 
            this.lblMOBInfoFontDisplay.AutoSize = true;
            this.lblMOBInfoFontDisplay.Location = new System.Drawing.Point(170, 0);
            this.lblMOBInfoFontDisplay.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblMOBInfoFontDisplay.Name = "lblMOBInfoFontDisplay";
            this.lblMOBInfoFontDisplay.Size = new System.Drawing.Size(45, 13);
            this.lblMOBInfoFontDisplay.TabIndex = 55;
            this.lblMOBInfoFontDisplay.Text = "(not set)";
            this.lblMOBInfoFontDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdMOBInfoFont
            // 
            this.cmdMOBInfoFont.AutoSize = true;
            this.cmdMOBInfoFont.Location = new System.Drawing.Point(286, 500);
            this.cmdMOBInfoFont.Margin = new System.Windows.Forms.Padding(1);
            this.cmdMOBInfoFont.Name = "cmdMOBInfoFont";
            this.cmdMOBInfoFont.Size = new System.Drawing.Size(54, 23);
            this.cmdMOBInfoFont.TabIndex = 75;
            this.cmdMOBInfoFont.Text = "...";
            this.cmdMOBInfoFont.UseVisualStyleBackColor = true;
            this.cmdMOBInfoFont.Click += new System.EventHandler(this.cmdMOBInfoFont_Click);
            // 
            // lblMOBInfoColor
            // 
            this.lblMOBInfoColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMOBInfoColor.AutoSize = true;
            this.lblMOBInfoColor.Location = new System.Drawing.Point(3, 527);
            this.lblMOBInfoColor.Name = "lblMOBInfoColor";
            this.lblMOBInfoColor.Size = new System.Drawing.Size(279, 13);
            this.lblMOBInfoColor.TabIndex = 76;
            this.lblMOBInfoColor.Text = "{config_appear_enemy_info_color}";
            // 
            // cmdMOBInfoColor
            // 
            this.cmdMOBInfoColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMOBInfoColor.BackColor = System.Drawing.Color.Black;
            this.cmdMOBInfoColor.Location = new System.Drawing.Point(286, 525);
            this.cmdMOBInfoColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdMOBInfoColor.Name = "cmdMOBInfoColor";
            this.cmdMOBInfoColor.Size = new System.Drawing.Size(54, 18);
            this.cmdMOBInfoColor.TabIndex = 77;
            this.cmdMOBInfoColor.UseVisualStyleBackColor = false;
            this.cmdMOBInfoColor.Click += new System.EventHandler(this.cmdMOBInfoColor_Click);
            // 
            // lblSpawnSelectSize
            // 
            this.lblSpawnSelectSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpawnSelectSize.AutoSize = true;
            this.lblSpawnSelectSize.Location = new System.Drawing.Point(3, 550);
            this.lblSpawnSelectSize.Name = "lblSpawnSelectSize";
            this.lblSpawnSelectSize.Size = new System.Drawing.Size(279, 13);
            this.lblSpawnSelectSize.TabIndex = 43;
            this.lblSpawnSelectSize.Text = "{config_appear_spawn_select_size}";
            // 
            // udSpawnSelectSize
            // 
            this.udSpawnSelectSize.DecimalPlaces = 1;
            this.udSpawnSelectSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udSpawnSelectSize.Location = new System.Drawing.Point(288, 547);
            this.udSpawnSelectSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udSpawnSelectSize.Name = "udSpawnSelectSize";
            this.udSpawnSelectSize.Size = new System.Drawing.Size(50, 20);
            this.udSpawnSelectSize.TabIndex = 47;
            this.udSpawnSelectSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udSpawnSelectSize.ValueChanged += new System.EventHandler(this.udSpawnSelectSize_ValueChanged);
            // 
            // lblSelectedColor
            // 
            this.lblSelectedColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedColor.AutoSize = true;
            this.lblSelectedColor.Location = new System.Drawing.Point(3, 573);
            this.lblSelectedColor.Name = "lblSelectedColor";
            this.lblSelectedColor.Size = new System.Drawing.Size(279, 13);
            this.lblSelectedColor.TabIndex = 10;
            this.lblSelectedColor.Text = "{config_appear_selected_color}";
            // 
            // cmdSelectedColor
            // 
            this.cmdSelectedColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectedColor.BackColor = System.Drawing.Color.Black;
            this.cmdSelectedColor.Location = new System.Drawing.Point(286, 571);
            this.cmdSelectedColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdSelectedColor.Name = "cmdSelectedColor";
            this.cmdSelectedColor.Size = new System.Drawing.Size(54, 18);
            this.cmdSelectedColor.TabIndex = 13;
            this.cmdSelectedColor.UseVisualStyleBackColor = false;
            this.cmdSelectedColor.Click += new System.EventHandler(this.cmdSelectedColor_Click);
            // 
            // lblTextGlowColor
            // 
            this.lblTextGlowColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextGlowColor.AutoSize = true;
            this.lblTextGlowColor.Location = new System.Drawing.Point(3, 639);
            this.lblTextGlowColor.Name = "lblTextGlowColor";
            this.lblTextGlowColor.Size = new System.Drawing.Size(279, 13);
            this.lblTextGlowColor.TabIndex = 11;
            this.lblTextGlowColor.Text = "{config_appear_glow_color}";
            // 
            // cmdTextGlowColor
            // 
            this.cmdTextGlowColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTextGlowColor.BackColor = System.Drawing.Color.Black;
            this.cmdTextGlowColor.Location = new System.Drawing.Point(286, 637);
            this.cmdTextGlowColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdTextGlowColor.Name = "cmdTextGlowColor";
            this.cmdTextGlowColor.Size = new System.Drawing.Size(54, 18);
            this.cmdTextGlowColor.TabIndex = 14;
            this.cmdTextGlowColor.UseVisualStyleBackColor = false;
            this.cmdTextGlowColor.Click += new System.EventHandler(this.cmdTextGlowColor_Click);
            // 
            // udSpawnGroupSize
            // 
            this.udSpawnGroupSize.DecimalPlaces = 1;
            this.udSpawnGroupSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udSpawnGroupSize.Location = new System.Drawing.Point(288, 659);
            this.udSpawnGroupSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udSpawnGroupSize.Name = "udSpawnGroupSize";
            this.udSpawnGroupSize.Size = new System.Drawing.Size(50, 20);
            this.udSpawnGroupSize.TabIndex = 48;
            this.udSpawnGroupSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udSpawnGroupSize.ValueChanged += new System.EventHandler(this.udSpawnGroupSize_ValueChanged);
            // 
            // lblSpawnGroupSize
            // 
            this.lblSpawnGroupSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpawnGroupSize.AutoSize = true;
            this.lblSpawnGroupSize.Location = new System.Drawing.Point(3, 662);
            this.lblSpawnGroupSize.Name = "lblSpawnGroupSize";
            this.lblSpawnGroupSize.Size = new System.Drawing.Size(279, 13);
            this.lblSpawnGroupSize.TabIndex = 44;
            this.lblSpawnGroupSize.Text = "{config_appear_spawn_group_size}";
            // 
            // lblSpawnHuntSize
            // 
            this.lblSpawnHuntSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpawnHuntSize.AutoSize = true;
            this.lblSpawnHuntSize.Location = new System.Drawing.Point(3, 688);
            this.lblSpawnHuntSize.Name = "lblSpawnHuntSize";
            this.lblSpawnHuntSize.Size = new System.Drawing.Size(279, 13);
            this.lblSpawnHuntSize.TabIndex = 45;
            this.lblSpawnHuntSize.Text = "{config_appear_spawn_hunt_size}";
            // 
            // udSpawnHuntSize
            // 
            this.udSpawnHuntSize.DecimalPlaces = 1;
            this.udSpawnHuntSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udSpawnHuntSize.Location = new System.Drawing.Point(288, 685);
            this.udSpawnHuntSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udSpawnHuntSize.Name = "udSpawnHuntSize";
            this.udSpawnHuntSize.Size = new System.Drawing.Size(50, 20);
            this.udSpawnHuntSize.TabIndex = 49;
            this.udSpawnHuntSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udSpawnHuntSize.ValueChanged += new System.EventHandler(this.udSpawnHuntSize_ValueChanged);
            // 
            // chkDrawHeadingLines
            // 
            this.chkDrawHeadingLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDrawHeadingLines.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkDrawHeadingLines, 2);
            this.chkDrawHeadingLines.Location = new System.Drawing.Point(3, 711);
            this.chkDrawHeadingLines.Name = "chkDrawHeadingLines";
            this.chkDrawHeadingLines.Size = new System.Drawing.Size(335, 17);
            this.chkDrawHeadingLines.TabIndex = 78;
            this.chkDrawHeadingLines.Text = "{config_appear_draw_heading_lines}";
            this.chkDrawHeadingLines.UseVisualStyleBackColor = true;
            this.chkDrawHeadingLines.CheckedChanged += new System.EventHandler(this.chkDrawHeadingLines_CheckedChanged);
            // 
            // lblHeadingLineColor
            // 
            this.lblHeadingLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeadingLineColor.AutoSize = true;
            this.lblHeadingLineColor.Location = new System.Drawing.Point(3, 734);
            this.lblHeadingLineColor.Name = "lblHeadingLineColor";
            this.lblHeadingLineColor.Size = new System.Drawing.Size(279, 13);
            this.lblHeadingLineColor.TabIndex = 79;
            this.lblHeadingLineColor.Text = "{config_appear_heading_line_color}";
            // 
            // cmdHeadingLineColor
            // 
            this.cmdHeadingLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHeadingLineColor.BackColor = System.Drawing.Color.Black;
            this.cmdHeadingLineColor.Location = new System.Drawing.Point(286, 732);
            this.cmdHeadingLineColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdHeadingLineColor.Name = "cmdHeadingLineColor";
            this.cmdHeadingLineColor.Size = new System.Drawing.Size(54, 18);
            this.cmdHeadingLineColor.TabIndex = 80;
            this.cmdHeadingLineColor.UseVisualStyleBackColor = false;
            this.cmdHeadingLineColor.Click += new System.EventHandler(this.cmdHeadingLineColor_Click);
            // 
            // chkShowRadarRange
            // 
            this.chkShowRadarRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowRadarRange.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowRadarRange, 2);
            this.chkShowRadarRange.Location = new System.Drawing.Point(3, 754);
            this.chkShowRadarRange.Name = "chkShowRadarRange";
            this.chkShowRadarRange.Size = new System.Drawing.Size(335, 17);
            this.chkShowRadarRange.TabIndex = 15;
            this.chkShowRadarRange.Text = "{config_appear_show_range}";
            this.chkShowRadarRange.UseVisualStyleBackColor = true;
            this.chkShowRadarRange.CheckedChanged += new System.EventHandler(this.chkShowRadarRange_CheckedChanged);
            // 
            // lblRadarRangeColor
            // 
            this.lblRadarRangeColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRadarRangeColor.AutoSize = true;
            this.lblRadarRangeColor.Location = new System.Drawing.Point(3, 777);
            this.lblRadarRangeColor.Name = "lblRadarRangeColor";
            this.lblRadarRangeColor.Size = new System.Drawing.Size(279, 13);
            this.lblRadarRangeColor.TabIndex = 16;
            this.lblRadarRangeColor.Text = "{config_appear_range_color}";
            // 
            // cmdRadarRangeColor
            // 
            this.cmdRadarRangeColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRadarRangeColor.BackColor = System.Drawing.Color.Black;
            this.cmdRadarRangeColor.Location = new System.Drawing.Point(286, 775);
            this.cmdRadarRangeColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdRadarRangeColor.Name = "cmdRadarRangeColor";
            this.cmdRadarRangeColor.Size = new System.Drawing.Size(54, 18);
            this.cmdRadarRangeColor.TabIndex = 17;
            this.cmdRadarRangeColor.UseVisualStyleBackColor = false;
            this.cmdRadarRangeColor.Click += new System.EventHandler(this.cmdRadarRangeColor_Click);
            // 
            // lblAppearSectionLines
            // 
            this.lblAppearSectionLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAppearSectionLines.AutoSize = true;
            this.lblAppearSectionLines.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableLayoutPanel4.SetColumnSpan(this.lblAppearSectionLines, 2);
            this.lblAppearSectionLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppearSectionLines.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblAppearSectionLines.Location = new System.Drawing.Point(3, 794);
            this.lblAppearSectionLines.Name = "lblAppearSectionLines";
            this.lblAppearSectionLines.Padding = new System.Windows.Forms.Padding(1);
            this.lblAppearSectionLines.Size = new System.Drawing.Size(335, 15);
            this.lblAppearSectionLines.TabIndex = 51;
            this.lblAppearSectionLines.Text = "{config_appear_section_lines}";
            this.lblAppearSectionLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkShowHuntLines
            // 
            this.chkShowHuntLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowHuntLines.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowHuntLines, 2);
            this.chkShowHuntLines.Location = new System.Drawing.Point(3, 812);
            this.chkShowHuntLines.Name = "chkShowHuntLines";
            this.chkShowHuntLines.Size = new System.Drawing.Size(335, 17);
            this.chkShowHuntLines.TabIndex = 36;
            this.chkShowHuntLines.Text = "{config_appear_show_hunt_lines}";
            this.chkShowHuntLines.UseVisualStyleBackColor = true;
            this.chkShowHuntLines.CheckedChanged += new System.EventHandler(this.chkShowHuntLines_CheckedChanged);
            // 
            // lblHuntLineColor
            // 
            this.lblHuntLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHuntLineColor.AutoSize = true;
            this.lblHuntLineColor.Location = new System.Drawing.Point(3, 835);
            this.lblHuntLineColor.Name = "lblHuntLineColor";
            this.lblHuntLineColor.Size = new System.Drawing.Size(279, 13);
            this.lblHuntLineColor.TabIndex = 18;
            this.lblHuntLineColor.Text = "{config_appear_hunt_line_color}";
            // 
            // cmdHuntLineColor
            // 
            this.cmdHuntLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuntLineColor.BackColor = System.Drawing.Color.Black;
            this.cmdHuntLineColor.Location = new System.Drawing.Point(286, 833);
            this.cmdHuntLineColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdHuntLineColor.Name = "cmdHuntLineColor";
            this.cmdHuntLineColor.Size = new System.Drawing.Size(54, 18);
            this.cmdHuntLineColor.TabIndex = 19;
            this.cmdHuntLineColor.UseVisualStyleBackColor = false;
            this.cmdHuntLineColor.Click += new System.EventHandler(this.cmdHuntLineColor_Click);
            // 
            // lblHuntLockedLineColor
            // 
            this.lblHuntLockedLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHuntLockedLineColor.AutoSize = true;
            this.lblHuntLockedLineColor.Location = new System.Drawing.Point(3, 855);
            this.lblHuntLockedLineColor.Name = "lblHuntLockedLineColor";
            this.lblHuntLockedLineColor.Size = new System.Drawing.Size(279, 13);
            this.lblHuntLockedLineColor.TabIndex = 53;
            this.lblHuntLockedLineColor.Text = "{config_appear_hunt_locked_line_color}";
            // 
            // cmdHuntLockedLineColor
            // 
            this.cmdHuntLockedLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuntLockedLineColor.BackColor = System.Drawing.Color.Black;
            this.cmdHuntLockedLineColor.Location = new System.Drawing.Point(286, 853);
            this.cmdHuntLockedLineColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdHuntLockedLineColor.Name = "cmdHuntLockedLineColor";
            this.cmdHuntLockedLineColor.Size = new System.Drawing.Size(54, 18);
            this.cmdHuntLockedLineColor.TabIndex = 24;
            this.cmdHuntLockedLineColor.UseVisualStyleBackColor = false;
            this.cmdHuntLockedLineColor.Click += new System.EventHandler(this.cmdHuntLockedLineColor_Click);
            // 
            // chkShowClaimLines
            // 
            this.chkShowClaimLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowClaimLines.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowClaimLines, 2);
            this.chkShowClaimLines.Location = new System.Drawing.Point(3, 875);
            this.chkShowClaimLines.Name = "chkShowClaimLines";
            this.chkShowClaimLines.Size = new System.Drawing.Size(335, 17);
            this.chkShowClaimLines.TabIndex = 37;
            this.chkShowClaimLines.Text = "{config_appear_show_claim_lines}";
            this.chkShowClaimLines.UseVisualStyleBackColor = true;
            this.chkShowClaimLines.CheckedChanged += new System.EventHandler(this.chkShowClaimLines_CheckedChanged);
            // 
            // lblClaimLineColor
            // 
            this.lblClaimLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClaimLineColor.AutoSize = true;
            this.lblClaimLineColor.Location = new System.Drawing.Point(3, 901);
            this.lblClaimLineColor.Name = "lblClaimLineColor";
            this.lblClaimLineColor.Size = new System.Drawing.Size(279, 13);
            this.lblClaimLineColor.TabIndex = 20;
            this.lblClaimLineColor.Text = "{config_appear_claim_line_color}";
            // 
            // cmdClaimLineColor
            // 
            this.cmdClaimLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClaimLineColor.BackColor = System.Drawing.Color.Black;
            this.cmdClaimLineColor.Location = new System.Drawing.Point(286, 898);
            this.cmdClaimLineColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdClaimLineColor.Name = "cmdClaimLineColor";
            this.cmdClaimLineColor.Size = new System.Drawing.Size(54, 18);
            this.cmdClaimLineColor.TabIndex = 21;
            this.cmdClaimLineColor.UseVisualStyleBackColor = false;
            this.cmdClaimLineColor.Click += new System.EventHandler(this.cmdClaimLineColor_Click);
            // 
            // chkShowPetLines
            // 
            this.chkShowPetLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowPetLines.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowPetLines, 2);
            this.chkShowPetLines.Location = new System.Drawing.Point(3, 923);
            this.chkShowPetLines.Name = "chkShowPetLines";
            this.chkShowPetLines.Size = new System.Drawing.Size(335, 16);
            this.chkShowPetLines.TabIndex = 38;
            this.chkShowPetLines.Text = "{config_appear_show_pet_lines}";
            this.chkShowPetLines.UseVisualStyleBackColor = true;
            this.chkShowPetLines.CheckedChanged += new System.EventHandler(this.chkShowPetLines_CheckedChanged);
            // 
            // lblPetLineColor
            // 
            this.lblPetLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPetLineColor.AutoSize = true;
            this.lblPetLineColor.Location = new System.Drawing.Point(3, 942);
            this.lblPetLineColor.Name = "lblPetLineColor";
            this.lblPetLineColor.Size = new System.Drawing.Size(279, 13);
            this.lblPetLineColor.TabIndex = 22;
            this.lblPetLineColor.Text = "{config_appear_pet_line_color}";
            // 
            // cmdPetLineColor
            // 
            this.cmdPetLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPetLineColor.BackColor = System.Drawing.Color.Black;
            this.cmdPetLineColor.Location = new System.Drawing.Point(286, 943);
            this.cmdPetLineColor.Margin = new System.Windows.Forms.Padding(1);
            this.cmdPetLineColor.Name = "cmdPetLineColor";
            this.cmdPetLineColor.Size = new System.Drawing.Size(54, 11);
            this.cmdPetLineColor.TabIndex = 23;
            this.cmdPetLineColor.UseVisualStyleBackColor = false;
            this.cmdPetLineColor.Click += new System.EventHandler(this.cmdPetLineColor_Click);
            // 
            // chkShowTextOutline
            // 
            this.chkShowTextOutline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowTextOutline.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowTextOutline, 2);
            this.chkShowTextOutline.Location = new System.Drawing.Point(3, 616);
            this.chkShowTextOutline.Name = "chkShowTextOutline";
            this.chkShowTextOutline.Size = new System.Drawing.Size(335, 17);
            this.chkShowTextOutline.TabIndex = 12;
            this.chkShowTextOutline.Text = "{config_appear_use_glow}";
            this.chkShowTextOutline.UseVisualStyleBackColor = true;
            this.chkShowTextOutline.CheckedChanged += new System.EventHandler(this.chkShowTextOutline_CheckedChanged);
            // 
            // chkShowTextShadow
            // 
            this.chkShowTextShadow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowTextShadow.AutoEllipsis = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkShowTextShadow, 2);
            this.chkShowTextShadow.Location = new System.Drawing.Point(3, 593);
            this.chkShowTextShadow.Name = "chkShowTextShadow";
            this.chkShowTextShadow.Size = new System.Drawing.Size(335, 17);
            this.chkShowTextShadow.TabIndex = 82;
            this.chkShowTextShadow.Text = "{config_appear_use_shadow}";
            this.chkShowTextShadow.UseVisualStyleBackColor = true;
            this.chkShowTextShadow.CheckedChanged += new System.EventHandler(this.chkShowTextShadow_CheckedChanged);
            // 
            // tpDepthFilter
            // 
            this.tpDepthFilter.AutoScroll = true;
            this.tpDepthFilter.Controls.Add(this.tableLayoutPanel5);
            this.tpDepthFilter.Location = new System.Drawing.Point(4, 22);
            this.tpDepthFilter.Name = "tpDepthFilter";
            this.tpDepthFilter.Padding = new System.Windows.Forms.Padding(3);
            this.tpDepthFilter.Size = new System.Drawing.Size(364, 406);
            this.tpDepthFilter.TabIndex = 3;
            this.tpDepthFilter.Text = "{tab_depthfilter}";
            this.tpDepthFilter.UseVisualStyleBackColor = true;
            // 
            // tpHotkeys
            // 
            this.tpHotkeys.Controls.Add(this.tableLayoutPanel8);
            this.tpHotkeys.Location = new System.Drawing.Point(4, 22);
            this.tpHotkeys.Name = "tpHotkeys";
            this.tpHotkeys.Padding = new System.Windows.Forms.Padding(3);
            this.tpHotkeys.Size = new System.Drawing.Size(364, 406);
            this.tpHotkeys.TabIndex = 5;
            this.tpHotkeys.Text = "{tab_hotkeys}";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.chkEnableInputFiltering, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblActionKey, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.lvHotKeyBindings, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.lblActionKeyHelp, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.chkEnableHotkeyCancel, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.txtActionKey, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.lblHotkeyHelp, 0, 5);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 6;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.Size = new System.Drawing.Size(358, 400);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // chkEnableInputFiltering
            // 
            this.chkEnableInputFiltering.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnableInputFiltering.AutoEllipsis = true;
            this.tableLayoutPanel8.SetColumnSpan(this.chkEnableInputFiltering, 2);
            this.chkEnableInputFiltering.Location = new System.Drawing.Point(3, 3);
            this.chkEnableInputFiltering.Name = "chkEnableInputFiltering";
            this.chkEnableInputFiltering.Size = new System.Drawing.Size(352, 17);
            this.chkEnableInputFiltering.TabIndex = 0;
            this.chkEnableInputFiltering.Text = "{config_hk_enable_filtering}";
            this.chkEnableInputFiltering.UseVisualStyleBackColor = true;
            this.chkEnableInputFiltering.CheckedChanged += new System.EventHandler(this.chkEnableInputFiltering_CheckedChanged);
            // 
            // lblActionKey
            // 
            this.lblActionKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActionKey.AutoSize = true;
            this.lblActionKey.Location = new System.Drawing.Point(3, 52);
            this.lblActionKey.Name = "lblActionKey";
            this.lblActionKey.Size = new System.Drawing.Size(120, 13);
            this.lblActionKey.TabIndex = 1;
            this.lblActionKey.Text = "{config_hk_action_key}";
            // 
            // lvHotKeyBindings
            // 
            this.lvHotKeyBindings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cHotKey,
            this.cBinding});
            this.tableLayoutPanel8.SetColumnSpan(this.lvHotKeyBindings, 2);
            this.lvHotKeyBindings.ContextMenuStrip = this.cmHotkeyBindings;
            this.lvHotKeyBindings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHotKeyBindings.FullRowSelect = true;
            this.lvHotKeyBindings.HideSelection = false;
            this.lvHotKeyBindings.Location = new System.Drawing.Point(3, 88);
            this.lvHotKeyBindings.MultiSelect = false;
            this.lvHotKeyBindings.Name = "lvHotKeyBindings";
            this.lvHotKeyBindings.Size = new System.Drawing.Size(352, 296);
            this.lvHotKeyBindings.TabIndex = 3;
            this.lvHotKeyBindings.UseCompatibleStateImageBehavior = false;
            this.lvHotKeyBindings.View = System.Windows.Forms.View.Details;
            this.lvHotKeyBindings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvHotKeyBindings_KeyDown);
            // 
            // cHotKey
            // 
            this.cHotKey.Text = "{config_hk_col_action_name}";
            this.cHotKey.Width = 182;
            // 
            // cBinding
            // 
            this.cBinding.Text = "{config_hk_col_action_binding}";
            this.cBinding.Width = 133;
            // 
            // cmHotkeyBindings
            // 
            this.cmHotkeyBindings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClearBinding,
            this.miUseDefaultBinding,
            this.miResetBinding});
            this.cmHotkeyBindings.Name = "cmHotkeyBindings";
            this.cmHotkeyBindings.Size = new System.Drawing.Size(180, 70);
            // 
            // miClearBinding
            // 
            this.miClearBinding.Name = "miClearBinding";
            this.miClearBinding.Size = new System.Drawing.Size(179, 22);
            this.miClearBinding.Text = "Unbind Assignment";
            // 
            // miUseDefaultBinding
            // 
            this.miUseDefaultBinding.Name = "miUseDefaultBinding";
            this.miUseDefaultBinding.Size = new System.Drawing.Size(179, 22);
            this.miUseDefaultBinding.Text = "Use Default";
            // 
            // miResetBinding
            // 
            this.miResetBinding.Name = "miResetBinding";
            this.miResetBinding.Size = new System.Drawing.Size(179, 22);
            this.miResetBinding.Text = "Reset Assignment";
            // 
            // lblActionKeyHelp
            // 
            this.lblActionKeyHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActionKeyHelp.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.lblActionKeyHelp, 2);
            this.lblActionKeyHelp.Location = new System.Drawing.Point(3, 72);
            this.lblActionKeyHelp.Name = "lblActionKeyHelp";
            this.lblActionKeyHelp.Size = new System.Drawing.Size(352, 13);
            this.lblActionKeyHelp.TabIndex = 4;
            this.lblActionKeyHelp.Text = "{config_hk_action_key_help}";
            // 
            // chkEnableHotkeyCancel
            // 
            this.chkEnableHotkeyCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnableHotkeyCancel.AutoEllipsis = true;
            this.tableLayoutPanel8.SetColumnSpan(this.chkEnableHotkeyCancel, 2);
            this.chkEnableHotkeyCancel.Location = new System.Drawing.Point(3, 26);
            this.chkEnableHotkeyCancel.Name = "chkEnableHotkeyCancel";
            this.chkEnableHotkeyCancel.Size = new System.Drawing.Size(352, 17);
            this.chkEnableHotkeyCancel.TabIndex = 5;
            this.chkEnableHotkeyCancel.Text = "{config_hk_allow_cancel}";
            this.chkEnableHotkeyCancel.UseVisualStyleBackColor = true;
            this.chkEnableHotkeyCancel.CheckedChanged += new System.EventHandler(this.chkEnableHotkeyCancel_CheckedChanged);
            // 
            // txtActionKey
            // 
            this.txtActionKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActionKey.Location = new System.Drawing.Point(129, 49);
            this.txtActionKey.Name = "txtActionKey";
            this.txtActionKey.ReadOnly = true;
            this.txtActionKey.Size = new System.Drawing.Size(226, 20);
            this.txtActionKey.TabIndex = 6;
            this.txtActionKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtActionKey_KeyDown);
            // 
            // lblHotkeyHelp
            // 
            this.lblHotkeyHelp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHotkeyHelp.AutoSize = true;
            this.tableLayoutPanel8.SetColumnSpan(this.lblHotkeyHelp, 2);
            this.lblHotkeyHelp.Location = new System.Drawing.Point(3, 387);
            this.lblHotkeyHelp.Name = "lblHotkeyHelp";
            this.lblHotkeyHelp.Size = new System.Drawing.Size(88, 13);
            this.lblHotkeyHelp.TabIndex = 7;
            this.lblHotkeyHelp.Text = "{config_hk_help}";
            // 
            // tpSignatures
            // 
            this.tpSignatures.AutoScroll = true;
            this.tpSignatures.Controls.Add(this.tableLayoutPanel7);
            this.tpSignatures.Location = new System.Drawing.Point(4, 22);
            this.tpSignatures.Name = "tpSignatures";
            this.tpSignatures.Padding = new System.Windows.Forms.Padding(3);
            this.tpSignatures.Size = new System.Drawing.Size(364, 406);
            this.tpSignatures.TabIndex = 4;
            this.tpSignatures.Text = "{tab_sig}";
            this.tpSignatures.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.cmdSigDefaults);
            this.flowLayoutPanel3.Controls.Add(this.cmdResetSigs);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(127, 240);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.MinimumSize = new System.Drawing.Size(20, 20);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(194, 29);
            this.flowLayoutPanel3.TabIndex = 15;
            // 
            // cmdResetSigs
            // 
            this.cmdResetSigs.AutoSize = true;
            this.cmdResetSigs.Location = new System.Drawing.Point(107, 3);
            this.cmdResetSigs.Name = "cmdResetSigs";
            this.cmdResetSigs.Size = new System.Drawing.Size(84, 23);
            this.cmdResetSigs.TabIndex = 12;
            this.cmdResetSigs.Text = "{button_reset}";
            this.cmdResetSigs.UseVisualStyleBackColor = true;
            this.cmdResetSigs.Click += new System.EventHandler(this.cmdResetSigs_Click);
            // 
            // cmdSigDefaults
            // 
            this.cmdSigDefaults.AutoSize = true;
            this.cmdSigDefaults.Location = new System.Drawing.Point(3, 3);
            this.cmdSigDefaults.Name = "cmdSigDefaults";
            this.cmdSigDefaults.Size = new System.Drawing.Size(98, 23);
            this.cmdSigDefaults.TabIndex = 13;
            this.cmdSigDefaults.Text = "{button_defaults}";
            this.cmdSigDefaults.UseVisualStyleBackColor = true;
            this.cmdSigDefaults.Click += new System.EventHandler(this.cmdSigDefaults_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel3, 1, 11);
            this.tableLayoutPanel7.Controls.Add(this.pbStatus_SIG_INSTANCE_ID, 2, 8);
            this.tableLayoutPanel7.Controls.Add(this.lblSIG_INSTANCE_ID, 0, 8);
            this.tableLayoutPanel7.Controls.Add(this.lblSigWarning, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblSigHelp, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.txtSIG_ZONE_ID, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.lblSIG_MY_TARGET, 0, 7);
            this.tableLayoutPanel7.Controls.Add(this.txtSIG_ZONE_SHORT, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.lblSIG_ZONE_SHORT, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.lblSIG_ZONE_ID, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.lblSIG_SPAWN_START, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.lblSIG_SPAWN_END, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.lblSIG_MY_ID, 0, 6);
            this.tableLayoutPanel7.Controls.Add(this.txtSIG_SPAWN_START, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.txtSIG_SPAWN_END, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.txtSIG_MY_ID, 1, 6);
            this.tableLayoutPanel7.Controls.Add(this.txtSIG_MY_TARGET, 1, 7);
            this.tableLayoutPanel7.Controls.Add(this.txtSIG_INSTANCE_ID, 1, 8);
            this.tableLayoutPanel7.Controls.Add(this.lblSigNoActivePID, 1, 9);
            this.tableLayoutPanel7.Controls.Add(this.lblSigRestartWarning, 1, 10);
            this.tableLayoutPanel7.Controls.Add(this.pbStatus_SIG_MY_TARGET, 2, 7);
            this.tableLayoutPanel7.Controls.Add(this.pbStatus_SIG_MY_ID, 2, 6);
            this.tableLayoutPanel7.Controls.Add(this.pbStatus_SIG_SPAWN_END, 2, 5);
            this.tableLayoutPanel7.Controls.Add(this.pbStatus_SIG_SPAWN_START, 2, 4);
            this.tableLayoutPanel7.Controls.Add(this.pbStatus_SIG_ZONE_SHORT, 2, 3);
            this.tableLayoutPanel7.Controls.Add(this.pbStatus_SIG_ZONE_ID, 2, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 12;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(358, 269);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // pbStatus_SIG_INSTANCE_ID
            // 
            this.pbStatus_SIG_INSTANCE_ID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbStatus_SIG_INSTANCE_ID.Image = ((System.Drawing.Image)(resources.GetObject("pbStatus_SIG_INSTANCE_ID.Image")));
            this.pbStatus_SIG_INSTANCE_ID.Location = new System.Drawing.Point(339, 193);
            this.pbStatus_SIG_INSTANCE_ID.Name = "pbStatus_SIG_INSTANCE_ID";
            this.pbStatus_SIG_INSTANCE_ID.Size = new System.Drawing.Size(16, 16);
            this.pbStatus_SIG_INSTANCE_ID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus_SIG_INSTANCE_ID.TabIndex = 21;
            this.pbStatus_SIG_INSTANCE_ID.TabStop = false;
            // 
            // lblSIG_INSTANCE_ID
            // 
            this.lblSIG_INSTANCE_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSIG_INSTANCE_ID.AutoSize = true;
            this.lblSIG_INSTANCE_ID.Location = new System.Drawing.Point(3, 194);
            this.lblSIG_INSTANCE_ID.Name = "lblSIG_INSTANCE_ID";
            this.lblSIG_INSTANCE_ID.Size = new System.Drawing.Size(121, 13);
            this.lblSIG_INSTANCE_ID.TabIndex = 8;
            this.lblSIG_INSTANCE_ID.Text = "{SIG_INSTANCE_ID}";
            // 
            // lblSigWarning
            // 
            this.lblSigWarning.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.lblSigWarning, 2);
            this.lblSigWarning.Location = new System.Drawing.Point(3, 0);
            this.lblSigWarning.Name = "lblSigWarning";
            this.lblSigWarning.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lblSigWarning.Size = new System.Drawing.Size(147, 19);
            this.lblSigWarning.TabIndex = 1;
            this.lblSigWarning.Text = "{config_general_sig_warning}";
            // 
            // lblSigHelp
            // 
            this.lblSigHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSigHelp.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.lblSigHelp, 2);
            this.lblSigHelp.Location = new System.Drawing.Point(3, 19);
            this.lblSigHelp.Name = "lblSigHelp";
            this.lblSigHelp.Size = new System.Drawing.Size(330, 13);
            this.lblSigHelp.TabIndex = 2;
            this.lblSigHelp.Text = "{config_general_sig_help}";
            // 
            // txtSIG_ZONE_ID
            // 
            this.txtSIG_ZONE_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSIG_ZONE_ID.Location = new System.Drawing.Point(130, 35);
            this.txtSIG_ZONE_ID.Name = "txtSIG_ZONE_ID";
            this.txtSIG_ZONE_ID.Size = new System.Drawing.Size(203, 20);
            this.txtSIG_ZONE_ID.TabIndex = 1;
            this.txtSIG_ZONE_ID.TextChanged += new System.EventHandler(this.txtSIG_ZONE_ID_TextChanged);
            // 
            // lblSIG_MY_TARGET
            // 
            this.lblSIG_MY_TARGET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSIG_MY_TARGET.AutoSize = true;
            this.lblSIG_MY_TARGET.Location = new System.Drawing.Point(3, 168);
            this.lblSIG_MY_TARGET.Name = "lblSIG_MY_TARGET";
            this.lblSIG_MY_TARGET.Size = new System.Drawing.Size(121, 13);
            this.lblSIG_MY_TARGET.TabIndex = 7;
            this.lblSIG_MY_TARGET.Text = "{SIG_MY_TARGET}";
            // 
            // txtSIG_ZONE_SHORT
            // 
            this.txtSIG_ZONE_SHORT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSIG_ZONE_SHORT.Location = new System.Drawing.Point(130, 61);
            this.txtSIG_ZONE_SHORT.Name = "txtSIG_ZONE_SHORT";
            this.txtSIG_ZONE_SHORT.Size = new System.Drawing.Size(203, 20);
            this.txtSIG_ZONE_SHORT.TabIndex = 3;
            this.txtSIG_ZONE_SHORT.TextChanged += new System.EventHandler(this.txtSIG_ZONE_SHORT_TextChanged);
            // 
            // lblSIG_ZONE_SHORT
            // 
            this.lblSIG_ZONE_SHORT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSIG_ZONE_SHORT.AutoSize = true;
            this.lblSIG_ZONE_SHORT.Location = new System.Drawing.Point(3, 64);
            this.lblSIG_ZONE_SHORT.Name = "lblSIG_ZONE_SHORT";
            this.lblSIG_ZONE_SHORT.Size = new System.Drawing.Size(121, 13);
            this.lblSIG_ZONE_SHORT.TabIndex = 2;
            this.lblSIG_ZONE_SHORT.Text = "{SIG_ZONE_SHORT}";
            // 
            // lblSIG_ZONE_ID
            // 
            this.lblSIG_ZONE_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSIG_ZONE_ID.AutoSize = true;
            this.lblSIG_ZONE_ID.Location = new System.Drawing.Point(3, 38);
            this.lblSIG_ZONE_ID.Name = "lblSIG_ZONE_ID";
            this.lblSIG_ZONE_ID.Size = new System.Drawing.Size(121, 13);
            this.lblSIG_ZONE_ID.TabIndex = 0;
            this.lblSIG_ZONE_ID.Text = "{SIG_ZONE_ID}";
            // 
            // lblSIG_SPAWN_START
            // 
            this.lblSIG_SPAWN_START.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSIG_SPAWN_START.AutoSize = true;
            this.lblSIG_SPAWN_START.Location = new System.Drawing.Point(3, 90);
            this.lblSIG_SPAWN_START.Name = "lblSIG_SPAWN_START";
            this.lblSIG_SPAWN_START.Size = new System.Drawing.Size(121, 13);
            this.lblSIG_SPAWN_START.TabIndex = 4;
            this.lblSIG_SPAWN_START.Text = "{SIG_SPAWN_START}";
            // 
            // lblSIG_SPAWN_END
            // 
            this.lblSIG_SPAWN_END.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSIG_SPAWN_END.AutoSize = true;
            this.lblSIG_SPAWN_END.Location = new System.Drawing.Point(3, 116);
            this.lblSIG_SPAWN_END.Name = "lblSIG_SPAWN_END";
            this.lblSIG_SPAWN_END.Size = new System.Drawing.Size(121, 13);
            this.lblSIG_SPAWN_END.TabIndex = 5;
            this.lblSIG_SPAWN_END.Text = "{SIG_SPAWN_END}";
            // 
            // lblSIG_MY_ID
            // 
            this.lblSIG_MY_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSIG_MY_ID.AutoSize = true;
            this.lblSIG_MY_ID.Location = new System.Drawing.Point(3, 142);
            this.lblSIG_MY_ID.Name = "lblSIG_MY_ID";
            this.lblSIG_MY_ID.Size = new System.Drawing.Size(121, 13);
            this.lblSIG_MY_ID.TabIndex = 6;
            this.lblSIG_MY_ID.Text = "{SIG_MY_ID}";
            // 
            // txtSIG_SPAWN_START
            // 
            this.txtSIG_SPAWN_START.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSIG_SPAWN_START.Location = new System.Drawing.Point(130, 87);
            this.txtSIG_SPAWN_START.Name = "txtSIG_SPAWN_START";
            this.txtSIG_SPAWN_START.Size = new System.Drawing.Size(203, 20);
            this.txtSIG_SPAWN_START.TabIndex = 8;
            this.txtSIG_SPAWN_START.TextChanged += new System.EventHandler(this.txtSIG_SPAWN_START_TextChanged);
            // 
            // txtSIG_SPAWN_END
            // 
            this.txtSIG_SPAWN_END.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSIG_SPAWN_END.Location = new System.Drawing.Point(130, 113);
            this.txtSIG_SPAWN_END.Name = "txtSIG_SPAWN_END";
            this.txtSIG_SPAWN_END.Size = new System.Drawing.Size(203, 20);
            this.txtSIG_SPAWN_END.TabIndex = 9;
            this.txtSIG_SPAWN_END.TextChanged += new System.EventHandler(this.txtSIG_SPAWN_END_TextChanged);
            // 
            // txtSIG_MY_ID
            // 
            this.txtSIG_MY_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSIG_MY_ID.Location = new System.Drawing.Point(130, 139);
            this.txtSIG_MY_ID.Name = "txtSIG_MY_ID";
            this.txtSIG_MY_ID.Size = new System.Drawing.Size(203, 20);
            this.txtSIG_MY_ID.TabIndex = 10;
            this.txtSIG_MY_ID.TextChanged += new System.EventHandler(this.txtSIG_MY_ID_TextChanged);
            // 
            // txtSIG_MY_TARGET
            // 
            this.txtSIG_MY_TARGET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSIG_MY_TARGET.Location = new System.Drawing.Point(130, 165);
            this.txtSIG_MY_TARGET.Name = "txtSIG_MY_TARGET";
            this.txtSIG_MY_TARGET.Size = new System.Drawing.Size(203, 20);
            this.txtSIG_MY_TARGET.TabIndex = 11;
            this.txtSIG_MY_TARGET.TextChanged += new System.EventHandler(this.txtSIG_MY_TARGET_TextChanged);
            // 
            // txtSIG_INSTANCE_ID
            // 
            this.txtSIG_INSTANCE_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSIG_INSTANCE_ID.Location = new System.Drawing.Point(130, 191);
            this.txtSIG_INSTANCE_ID.Name = "txtSIG_INSTANCE_ID";
            this.txtSIG_INSTANCE_ID.Size = new System.Drawing.Size(203, 20);
            this.txtSIG_INSTANCE_ID.TabIndex = 22;
            this.txtSIG_INSTANCE_ID.TextChanged += new System.EventHandler(this.txtSIG_INSTANCE_ID_TextChanged);
            // 
            // lblSigNoActivePID
            // 
            this.lblSigNoActivePID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSigNoActivePID.AutoSize = true;
            this.lblSigNoActivePID.Location = new System.Drawing.Point(130, 214);
            this.lblSigNoActivePID.Name = "lblSigNoActivePID";
            this.lblSigNoActivePID.Size = new System.Drawing.Size(203, 13);
            this.lblSigNoActivePID.TabIndex = 21;
            this.lblSigNoActivePID.Text = "{config_general_sig_no_active_pid}";
            // 
            // lblSigRestartWarning
            // 
            this.lblSigRestartWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSigRestartWarning.AutoSize = true;
            this.lblSigRestartWarning.Location = new System.Drawing.Point(130, 227);
            this.lblSigRestartWarning.Name = "lblSigRestartWarning";
            this.lblSigRestartWarning.Size = new System.Drawing.Size(203, 13);
            this.lblSigRestartWarning.TabIndex = 13;
            this.lblSigRestartWarning.Text = "{config_general_sig_restart_warning}";
            // 
            // pbStatus_SIG_MY_TARGET
            // 
            this.pbStatus_SIG_MY_TARGET.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbStatus_SIG_MY_TARGET.Image = ((System.Drawing.Image)(resources.GetObject("pbStatus_SIG_MY_TARGET.Image")));
            this.pbStatus_SIG_MY_TARGET.Location = new System.Drawing.Point(339, 167);
            this.pbStatus_SIG_MY_TARGET.Name = "pbStatus_SIG_MY_TARGET";
            this.pbStatus_SIG_MY_TARGET.Size = new System.Drawing.Size(16, 16);
            this.pbStatus_SIG_MY_TARGET.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus_SIG_MY_TARGET.TabIndex = 20;
            this.pbStatus_SIG_MY_TARGET.TabStop = false;
            // 
            // pbStatus_SIG_MY_ID
            // 
            this.pbStatus_SIG_MY_ID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbStatus_SIG_MY_ID.Image = ((System.Drawing.Image)(resources.GetObject("pbStatus_SIG_MY_ID.Image")));
            this.pbStatus_SIG_MY_ID.Location = new System.Drawing.Point(339, 141);
            this.pbStatus_SIG_MY_ID.Name = "pbStatus_SIG_MY_ID";
            this.pbStatus_SIG_MY_ID.Size = new System.Drawing.Size(16, 16);
            this.pbStatus_SIG_MY_ID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus_SIG_MY_ID.TabIndex = 19;
            this.pbStatus_SIG_MY_ID.TabStop = false;
            // 
            // pbStatus_SIG_SPAWN_END
            // 
            this.pbStatus_SIG_SPAWN_END.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbStatus_SIG_SPAWN_END.Image = ((System.Drawing.Image)(resources.GetObject("pbStatus_SIG_SPAWN_END.Image")));
            this.pbStatus_SIG_SPAWN_END.Location = new System.Drawing.Point(339, 115);
            this.pbStatus_SIG_SPAWN_END.Name = "pbStatus_SIG_SPAWN_END";
            this.pbStatus_SIG_SPAWN_END.Size = new System.Drawing.Size(16, 16);
            this.pbStatus_SIG_SPAWN_END.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus_SIG_SPAWN_END.TabIndex = 18;
            this.pbStatus_SIG_SPAWN_END.TabStop = false;
            // 
            // pbStatus_SIG_SPAWN_START
            // 
            this.pbStatus_SIG_SPAWN_START.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbStatus_SIG_SPAWN_START.Image = ((System.Drawing.Image)(resources.GetObject("pbStatus_SIG_SPAWN_START.Image")));
            this.pbStatus_SIG_SPAWN_START.Location = new System.Drawing.Point(339, 89);
            this.pbStatus_SIG_SPAWN_START.Name = "pbStatus_SIG_SPAWN_START";
            this.pbStatus_SIG_SPAWN_START.Size = new System.Drawing.Size(16, 16);
            this.pbStatus_SIG_SPAWN_START.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus_SIG_SPAWN_START.TabIndex = 17;
            this.pbStatus_SIG_SPAWN_START.TabStop = false;
            // 
            // pbStatus_SIG_ZONE_SHORT
            // 
            this.pbStatus_SIG_ZONE_SHORT.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbStatus_SIG_ZONE_SHORT.Image = ((System.Drawing.Image)(resources.GetObject("pbStatus_SIG_ZONE_SHORT.Image")));
            this.pbStatus_SIG_ZONE_SHORT.Location = new System.Drawing.Point(339, 63);
            this.pbStatus_SIG_ZONE_SHORT.Name = "pbStatus_SIG_ZONE_SHORT";
            this.pbStatus_SIG_ZONE_SHORT.Size = new System.Drawing.Size(16, 16);
            this.pbStatus_SIG_ZONE_SHORT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus_SIG_ZONE_SHORT.TabIndex = 16;
            this.pbStatus_SIG_ZONE_SHORT.TabStop = false;
            // 
            // pbStatus_SIG_ZONE_ID
            // 
            this.pbStatus_SIG_ZONE_ID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbStatus_SIG_ZONE_ID.Image = ((System.Drawing.Image)(resources.GetObject("pbStatus_SIG_ZONE_ID.Image")));
            this.pbStatus_SIG_ZONE_ID.Location = new System.Drawing.Point(339, 37);
            this.pbStatus_SIG_ZONE_ID.Name = "pbStatus_SIG_ZONE_ID";
            this.pbStatus_SIG_ZONE_ID.Size = new System.Drawing.Size(16, 16);
            this.pbStatus_SIG_ZONE_ID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus_SIG_ZONE_ID.TabIndex = 15;
            this.pbStatus_SIG_ZONE_ID.TabStop = false;
            // 
            // fCustomize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 489);
            this.Controls.Add(this.tcOptions);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(316, 293);
            this.Name = "fCustomize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{dialog_config}";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tpMap.ResumeLayout(false);
            this.tpMap.PerformLayout();
            this.gSpawnInfo.ResumeLayout(false);
            this.gSpawnInfo.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.gMapPack.ResumeLayout(false);
            this.gMapPack.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapPackWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMapImageOpacity)).EndInit();
            this.gBehavior.ResumeLayout(false);
            this.gBehavior.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilePathWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPollFrequency)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDepthAlphaMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDepthAlphaMax)).EndInit();
            this.tcOptions.ResumeLayout(false);
            this.tpAppearance.ResumeLayout(false);
            this.tpAppearance.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPlayerSize)).EndInit();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udNPCSize)).EndInit();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMOBSize)).EndInit();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpawnSelectSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpawnGroupSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpawnHuntSize)).EndInit();
            this.tpDepthFilter.ResumeLayout(false);
            this.tpDepthFilter.PerformLayout();
            this.tpHotkeys.ResumeLayout(false);
            this.tpHotkeys.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.cmHotkeyBindings.ResumeLayout(false);
            this.tpSignatures.ResumeLayout(false);
            this.tpSignatures.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_INSTANCE_ID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_MY_TARGET)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_MY_ID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_SPAWN_END)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_SPAWN_START)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_ZONE_SHORT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus_SIG_ZONE_ID)).EndInit();
            this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ColorDialog dColorChanger;
      private System.Windows.Forms.Button cmdCancel;
      private System.Windows.Forms.Button cmdOK;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.LinkLabel lblProductName;
      private System.Windows.Forms.TabPage tpMap;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
      private System.Windows.Forms.Label lblFillColor;
      private System.Windows.Forms.Button cmdChangeBackColor;
      private System.Windows.Forms.Label lblFillOpacity;
      private System.Windows.Forms.TrackBar tbOpacity;
      private System.Windows.Forms.Label lblOpacityPercent;
      private System.Windows.Forms.Label lblMapAltOpacityTitle;
      private System.Windows.Forms.TrackBar tbMapImageOpacity;
      private System.Windows.Forms.Label lblMapImageOpacityPercent;
      private System.Windows.Forms.TabControl tcOptions;
      private System.Windows.Forms.Label lblMapPathTitle;
      private System.Windows.Forms.Label lblMapPath;
      private System.Windows.Forms.Button cmdBrowseMapPath;
      private System.Windows.Forms.FolderBrowserDialog dBrowseFolder;
      private System.Windows.Forms.CheckBox chkShowMapAlternative;
      private System.Windows.Forms.CheckBox chkShowMapAlternativeAlways;
      private System.Windows.Forms.CheckBox chkAutoRangeSnap;
      private System.Windows.Forms.TabPage tpAppearance;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
      private System.Windows.Forms.CheckBox chkShowGridTicks;
      private System.Windows.Forms.Button cmdGridLineColor;
      private System.Windows.Forms.Label lblGridLineColor;
      private System.Windows.Forms.Label lblGridTickColor;
      private System.Windows.Forms.Label lblNPCColor;
      private System.Windows.Forms.Label lblEnemyColor;
      private System.Windows.Forms.Button cmdGridTickColor;
      private System.Windows.Forms.Button cmdNPCColor;
      private System.Windows.Forms.Button cmdEnemyColor;
      private System.Windows.Forms.CheckBox chkShowGridLines;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
      private System.Windows.Forms.CheckBox chkEnableSpawnDepthFilter;
      private System.Windows.Forms.CheckBox chkEnableLineDepthFilter;
      private System.Windows.Forms.CheckBox chkDepthFilterUseAlpha;
      private System.Windows.Forms.Label lblDepthAlphaMin;
      private System.Windows.Forms.Label lblDepthAlphaMax;
      private System.Windows.Forms.Label lblDepthDistance;
      private System.Windows.Forms.Label lblDepthCutoff;
      private System.Windows.Forms.NumericUpDown udDepthAlphaMin;
      private System.Windows.Forms.NumericUpDown udDepthAlphaMax;
      private System.Windows.Forms.TextBox txtDepthDistance;
      private System.Windows.Forms.TextBox txtDepthCutoff;
      private System.Windows.Forms.TabPage tpDepthFilter;
      private System.Windows.Forms.Label lblTextGlowColor;
      private System.Windows.Forms.CheckBox chkShowTextOutline;
      private System.Windows.Forms.Label lblSelectedColor;
      private System.Windows.Forms.Button cmdSelectedColor;
      private System.Windows.Forms.Button cmdTextGlowColor;
      private System.Windows.Forms.CheckBox chkShowRadarRange;
      private System.Windows.Forms.Label lblRadarRangeColor;
      private System.Windows.Forms.Button cmdRadarRangeColor;
      private System.Windows.Forms.Label lblHuntLineColor;
      private System.Windows.Forms.Button cmdHuntLineColor;
      private System.Windows.Forms.Label lblClaimLineColor;
      private System.Windows.Forms.Button cmdClaimLineColor;
      private System.Windows.Forms.Label lblPetLineColor;
      private System.Windows.Forms.Button cmdPetLineColor;
      private System.Windows.Forms.Label lblGroupColor;
      private System.Windows.Forms.Button cmdGroupMemberColor;
      private System.Windows.Forms.Label lblRaidColor;
      private System.Windows.Forms.Button cmdRaidMemberColor;
      private System.Windows.Forms.Label lblInfoTemplateTitle;
      private System.Windows.Forms.CheckBox chkInfoAllPlayers;
      private System.Windows.Forms.CheckBox chkInfoAllNPCs;
      private System.Windows.Forms.CheckBox chkInfoAllEnemies;
      private System.Windows.Forms.Label lblInfoTemplateHelp;
      private System.Windows.Forms.TextBox txtInfoTemplate;
      private System.Windows.Forms.Label lblGridSize;
      private System.Windows.Forms.NumericUpDown udGridSize;
      private System.Windows.Forms.Button cmdMainPlayerColor;
      private System.Windows.Forms.Label lblYouColor;
      private System.Windows.Forms.CheckBox chkUseMainPlayerFill;
      private System.Windows.Forms.Label lblYouFillColor;
      private System.Windows.Forms.Button cmdMainPlayerFillColor;
      private System.Windows.Forms.CheckBox chkShowHuntLines;
      private System.Windows.Forms.CheckBox chkShowClaimLines;
      private System.Windows.Forms.CheckBox chkShowPetLines;
      private System.Windows.Forms.GroupBox gMapPack;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
      private System.Windows.Forms.GroupBox gSpawnInfo;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
      private System.Windows.Forms.Label lblPollFreq;
      private System.Windows.Forms.NumericUpDown udPollFrequency;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.GroupBox gBehavior;
      private System.Windows.Forms.Label lblMapPackImagePathTitle;
      private System.Windows.Forms.Label lblMapPackImagePath;
      private System.Windows.Forms.Button cmdBrowseMapPackImagePath;
      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
      private System.Windows.Forms.PictureBox pbMapPackWarning;
      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
      private System.Windows.Forms.PictureBox pbFilePathWarning;
      private System.Windows.Forms.TabPage tpHotkeys;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
      private System.Windows.Forms.CheckBox chkEnableInputFiltering;
      private System.Windows.Forms.Label lblActionKey;
      private System.Windows.Forms.ListView lvHotKeyBindings;
      private System.Windows.Forms.ColumnHeader cHotKey;
      private System.Windows.Forms.ColumnHeader cBinding;
      private System.Windows.Forms.Label lblActionKeyHelp;
      private System.Windows.Forms.CheckBox chkEnableHotkeyCancel;
      private System.Windows.Forms.TextBox txtActionKey;
      private System.Windows.Forms.ContextMenuStrip cmHotkeyBindings;
      private System.Windows.Forms.ToolStripMenuItem miClearBinding;
      private System.Windows.Forms.Label lblHotkeyHelp;
      private System.Windows.Forms.ToolStripMenuItem miUseDefaultBinding;
      private System.Windows.Forms.ToolStripMenuItem miResetBinding;
      private System.Windows.Forms.Button cmdPlayerColor;
      private System.Windows.Forms.CheckBox chkUsePlayerColor;
      private System.Windows.Forms.Label lblPlayerSize;
      private System.Windows.Forms.Label lblSpawnSelectSize;
      private System.Windows.Forms.Label lblSpawnGroupSize;
      private System.Windows.Forms.Label lblSpawnHuntSize;
      private System.Windows.Forms.NumericUpDown udPlayerSize;
      private System.Windows.Forms.NumericUpDown udSpawnHuntSize;
      private System.Windows.Forms.NumericUpDown udSpawnGroupSize;
      private System.Windows.Forms.NumericUpDown udSpawnSelectSize;
      private System.Windows.Forms.Label lblAppearSectionSpawn;
      private System.Windows.Forms.Label lblAppearSectionLines;
      private System.Windows.Forms.CheckBox chkScaleLines;
      private System.Windows.Forms.Label lblAppearSectionGrid;
      private System.Windows.Forms.Label lblHuntLockedLineColor;
      private System.Windows.Forms.Button cmdHuntLockedLineColor;
      private System.Windows.Forms.Button cmdPlayerInfoFont;
      private System.Windows.Forms.Button cmdPlayerInfoColor;
      private System.Windows.Forms.Label lblPlayerInfoColor;
      private System.Windows.Forms.FontDialog dFontChanger;
      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
      private System.Windows.Forms.Label lblNPCInfoFont;
      private System.Windows.Forms.Label lblNPCInfoFontDisplay;
      private System.Windows.Forms.NumericUpDown udNPCSize;
      private System.Windows.Forms.Label lblNPCSize;
      private System.Windows.Forms.NumericUpDown udMOBSize;
      private System.Windows.Forms.Label lblMOBSize;
      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
      private System.Windows.Forms.Label lblPlayerInfoFont;
      private System.Windows.Forms.Label lblPlayerInfoFontDisplay;
      private System.Windows.Forms.Button cmdNPCInfoFont;
      private System.Windows.Forms.Label lblNPCInfoColor;
      private System.Windows.Forms.Button cmdNPCInfoColor;
      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
      private System.Windows.Forms.Label lblMOBInfoFont;
      private System.Windows.Forms.Label lblMOBInfoFontDisplay;
      private System.Windows.Forms.Button cmdMOBInfoFont;
      private System.Windows.Forms.Label lblMOBInfoColor;
      private System.Windows.Forms.Button cmdMOBInfoColor;
      private System.Windows.Forms.CheckBox chkDrawHeadingLines;
      private System.Windows.Forms.Button cmdHeadingLineColor;
      private System.Windows.Forms.Label lblHeadingLineColor;
      private System.Windows.Forms.CheckBox chkShowPlayerPosition;
      private System.Windows.Forms.CheckBox chkShowTextShadow;
        private System.Windows.Forms.TabPage tpSignatures;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.PictureBox pbStatus_SIG_INSTANCE_ID;
        private System.Windows.Forms.Label lblSIG_INSTANCE_ID;
        private System.Windows.Forms.Label lblSigWarning;
        private System.Windows.Forms.Label lblSigHelp;
        private System.Windows.Forms.TextBox txtSIG_ZONE_ID;
        private System.Windows.Forms.Label lblSIG_MY_TARGET;
        private System.Windows.Forms.TextBox txtSIG_ZONE_SHORT;
        private System.Windows.Forms.Label lblSIG_ZONE_SHORT;
        private System.Windows.Forms.Label lblSIG_ZONE_ID;
        private System.Windows.Forms.Label lblSIG_SPAWN_START;
        private System.Windows.Forms.Label lblSIG_SPAWN_END;
        private System.Windows.Forms.Label lblSIG_MY_ID;
        private System.Windows.Forms.TextBox txtSIG_SPAWN_START;
        private System.Windows.Forms.TextBox txtSIG_SPAWN_END;
        private System.Windows.Forms.TextBox txtSIG_MY_ID;
        private System.Windows.Forms.TextBox txtSIG_MY_TARGET;
        private System.Windows.Forms.TextBox txtSIG_INSTANCE_ID;
        private System.Windows.Forms.Label lblSigNoActivePID;
        private System.Windows.Forms.Label lblSigRestartWarning;
        private System.Windows.Forms.PictureBox pbStatus_SIG_MY_TARGET;
        private System.Windows.Forms.PictureBox pbStatus_SIG_MY_ID;
        private System.Windows.Forms.PictureBox pbStatus_SIG_SPAWN_END;
        private System.Windows.Forms.PictureBox pbStatus_SIG_SPAWN_START;
        private System.Windows.Forms.PictureBox pbStatus_SIG_ZONE_SHORT;
        private System.Windows.Forms.PictureBox pbStatus_SIG_ZONE_ID;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button cmdResetSigs;
        private System.Windows.Forms.Button cmdSigDefaults;
    }
}