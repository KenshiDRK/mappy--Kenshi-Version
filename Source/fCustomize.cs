using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using MapEngine;

namespace mappy {
    public partial class fCustomize : Form
    {
        private static fCustomize currentCustomization;
        private static string lastTab = "";

        fMap parent;
        IController controller;

        Color original_backColor;
        double original_backOpacity;
        float original_mapimgOpacity;
        int original_gridSize;
        bool original_showGridLines;
        bool original_showGridTicks;
        Color original_gridLineColor;
        Color original_gridTickColor;
        bool original_showPlayerPosition;
        Color original_playerColor;
        Color original_npcColor;
        Color original_enemyColor;
        bool original_usePlayerColor;
        bool original_showAlt;
        bool original_showAltAlways;
        string original_mapPath;
        string original_mapPackPath;
        bool original_autoRangeSnap;
        bool original_depthFilterLines;
        bool original_depthFilterSpawns;
        bool original_depthFilterAlpha;
        int original_depthFilterAlphaMin;
        int original_depthFilterAlphaMax;
        float original_depthFilterDistance;
        float original_depthFilterCutoff;
        int original_pollfreq;
        bool original_scaleLines;

        private float original_playerSize;
        private float original_NPCSize;
        private float original_MOBSize;
        private float original_spawnSelectSize;
        private float original_spawnGroupSize;
        private float original_spawnHuntSize;
        private Font original_playerInfoFont;
        private Font original_NPCInfoFont;
        private Font original_MOBInfoFont;
        private Color original_playerInfoColor;
        private Color original_NPCInfoColor;
        private Color original_MOBInfoColor;
        private Color original_selectedColor;
        private bool original_showTextOutline;
        private bool original_showTextShadow;
        private Color original_textOutlineColor;
        private bool original_showRadarRange;
        private Color original_radarRangeColor;
        private Color original_huntLineColor;
        private Color original_huntLockedLineColor;
        private Color original_claimLineColor;
        private Color original_petLineColor;
        private Color original_groupMemberColor;
        private Color original_raidMemberColor;
        private Color original_mainPlayerColor;
        private Color original_mainPlayerFillColor;
        private bool original_mainPlayerFillEnabled;
        private string original_infoTemplate;
        private bool original_infoAllPlayers;
        private bool original_infoAllNPCs;
        private bool original_infoAllEnemies;
        private bool original_showClaimLines;
        private bool original_showHuntLines;
        private bool original_showPetLines;

        private bool original_drawHeadingLines;
        private Color original_headingLineColor;

        private bool original_enable_filtering;
        private Keys original_action_key;
        private bool original_consume_input;
        private Dictionary<Actions<Keys>.Action, Keys> original_bindlist;

        private string original_SIG_ZONE_ID;
        private string original_SIG_ZONE_SHORT;
        private string original_SIG_SPAWN_START;
        private string original_SIG_SPAWN_END;
        private string original_SIG_MY_ID;
        private string original_SIG_MY_TARGET;
        private string original_SIG_INSTANCE_ID;

        public static void BeginCustomization(IController Controller, fMap Owner)
        {
            BeginCustomization(Controller, Owner, lastTab);
        }
        public static void BeginCustomization(IController Controller, fMap Owner, string StartingTab)
        {
            if (currentCustomization == null || currentCustomization.IsDisposed || !currentCustomization.IsHandleCreated)
            {
                currentCustomization = new fCustomize(Controller, Owner);
                if (currentCustomization.tcOptions.TabPages.ContainsKey(StartingTab))
                    currentCustomization.tcOptions.SelectedTab = currentCustomization.tcOptions.TabPages[StartingTab];
                currentCustomization.Show(Owner);
            }
            else
            {
                currentCustomization.Activate();
            }
        }

        private fCustomize()
        {
            InitializeComponent();
            InitializeLanguage();
            lblProductName.Text = Application.ProductName + " " + Application.ProductVersion;
            tpMap.Enabled = false;
            tpAppearance.Enabled = false;
            tpDepthFilter.Enabled = false;

            cmHotkeyBindings.Opening += new CancelEventHandler(cmHotkeyBindings_Opening);
        }

        private void cmHotkeyBindings_Opening(object sender, CancelEventArgs e)
        {
            if (lvHotKeyBindings.SelectedItems.Count != 1)
            {
                e.Cancel = true;
                return;
            }
        }

        private fCustomize(IController Controller)
            : this()
        {
            //------------------------------------------------------------------------------------------
            // initialize global config options
            //------------------------------------------------------------------------------------------
            controller = Controller;

            original_SIG_ZONE_ID = controller.Config.Get("SIG_ZONE_ID", FFXIGameInstance.SIG_ZONE_ID);
            original_SIG_ZONE_SHORT = controller.Config.Get("SIG_ZONE_SHORT", FFXIGameInstance.SIG_ZONE_SHORT);
            original_SIG_SPAWN_START = controller.Config.Get("SIG_SPAWN_START", FFXIGameInstance.SIG_SPAWN_START);
            original_SIG_SPAWN_END = controller.Config.Get("SIG_SPAWN_END", FFXIGameInstance.SIG_SPAWN_END);
            original_SIG_MY_ID = controller.Config.Get("SIG_MY_ID", FFXIGameInstance.SIG_MY_ID);
            original_SIG_MY_TARGET = controller.Config.Get("SIG_MY_TARGET", FFXIGameInstance.SIG_MY_TARGET);
            original_SIG_INSTANCE_ID = controller.Config.Get("SIG_INSTANCE_ID", FFXIGameInstance.SIG_INSTANCE_ID);

            original_enable_filtering = controller.HotkeyEnabled;
            original_action_key = controller.ActionKey;
            original_consume_input = controller.HotkeyConsumeInput;
            original_bindlist = new Dictionary<Actions<Keys>.Action, Keys>(); //create a quick copy of the action bindings
            foreach (KeyValuePair<string, Actions<Keys>.Action> pair in controller.Actions)
            {
                original_bindlist[pair.Value] = pair.Value.Trigger;
            }

            txtSIG_ZONE_ID.Text = original_SIG_ZONE_ID;
            txtSIG_ZONE_SHORT.Text = original_SIG_ZONE_SHORT;
            txtSIG_SPAWN_START.Text = original_SIG_SPAWN_START;
            txtSIG_SPAWN_END.Text = original_SIG_SPAWN_END;
            txtSIG_MY_ID.Text = original_SIG_MY_ID;
            txtSIG_MY_TARGET.Text = original_SIG_MY_TARGET;
            txtSIG_INSTANCE_ID.Text = original_SIG_INSTANCE_ID;

            chkEnableInputFiltering.Checked = original_enable_filtering;
            chkEnableHotkeyCancel.Checked = original_consume_input;
            txtActionKey.Text = GetPrettyKey(original_action_key);

            lvHotKeyBindings.Items.Clear();
            foreach (KeyValuePair<string, Actions<Keys>.Action> pair in controller.Actions)
            {
                ListViewItem item = new ListViewItem(pair.Value.ToString());
                item.SubItems.Add(GetPrettyKey(pair.Value.Trigger));
                item.Tag = pair.Value;
                lvHotKeyBindings.Items.Add(item);
            }
            //disable hotkey processing while in the config screen.
            controller.HotkeySilent = true;

            miClearBinding.Click += new EventHandler(miClearBinding_Click);
            miUseDefaultBinding.Click += new EventHandler(miUseDefaultBinding_Click);
            miResetBinding.Click += new EventHandler(miResetBinding_Click);
        }

        private fCustomize(IController Controller, fMap Parent)
            : this(Controller)
        {
            //------------------------------------------------------------------------------------------
            // initialize profile config options
            //------------------------------------------------------------------------------------------
            if (Parent == null || Parent.IsDisposed || !Parent.IsHandleCreated)
                return;
            parent = Parent;
            tpMap.Enabled = true;
            tpAppearance.Enabled = true;
            tpDepthFilter.Enabled = true;
            lblSigNoActivePID.Visible = false;

            //store the current values in case cancel is pressed. (most settings give a live preview)
            original_backColor = parent.BackColor;
            original_backOpacity = parent.LayerOpacity;
            original_showGridLines = parent.Engine.ShowGridLines;
            original_showGridTicks = parent.Engine.ShowGridTicks;
            original_gridSize = parent.Engine.GridSize;
            original_gridLineColor = parent.Engine.GridLineColor;
            original_gridTickColor = parent.Engine.GridTickColor;
            original_showPlayerPosition = parent.Engine.ShowPlayerPosition;
            original_playerColor = parent.Engine.PlayerColor;
            original_npcColor = parent.Engine.NPCColor;
            original_enemyColor = parent.Engine.MOBColor;
            original_usePlayerColor = parent.Engine.UsePlayerColor;
            original_mapimgOpacity = parent.Engine.MapAlternativeOpacity;
            original_showAlt = parent.Engine.ShowMapAlternative;
            original_showAltAlways = parent.Engine.ShowMapAlternativeAlways;
            original_mapPath = parent.Engine.Data.FilePath;
            original_mapPackPath = parent.MapPackPath;
            original_autoRangeSnap = parent.Engine.AutoRangeSnap;
            original_depthFilterLines = parent.Engine.DepthFilterLines;
            original_depthFilterSpawns = parent.Engine.DepthFilterSpawns;
            original_depthFilterAlpha = parent.Engine.DepthFilterAlpha;
            original_depthFilterAlphaMin = parent.Engine.DepthAlphaMin;
            original_depthFilterAlphaMax = parent.Engine.DepthAlphaMax;
            original_depthFilterDistance = parent.Engine.DepthDistance;
            original_depthFilterCutoff = parent.Engine.DepthCutoff;
            original_pollfreq = parent.PollFrequency;
            original_scaleLines = parent.Engine.ScaleLines;
            original_selectedColor = parent.Engine.SelectedColor;
            original_showTextOutline = parent.Engine.TextOutlineEnabled;
            original_showTextShadow = parent.Engine.TextShadowEnabled;
            original_textOutlineColor = parent.Engine.TextOutlineColor;
            original_showRadarRange = parent.Engine.ShowRadarRange;
            original_radarRangeColor = parent.Engine.RadarRangeColor;
            original_huntLineColor = parent.Engine.HuntChainColor;
            original_huntLockedLineColor = parent.Engine.HuntLockedChainColor;
            original_claimLineColor = parent.Engine.ClaimChainColor;
            original_petLineColor = parent.Engine.PetChainColor;
            original_groupMemberColor = parent.Engine.GroupMemberColor;
            original_raidMemberColor = parent.Engine.RaidMemberColor;
            original_infoTemplate = parent.Engine.InfoTemplate;
            original_infoAllPlayers = parent.Engine.ShowAllPlayerInfo;
            original_infoAllNPCs = parent.Engine.ShowAllNPCInfo;
            original_infoAllEnemies = parent.Engine.ShowAllEnemyInfo;
            original_mainPlayerColor = parent.Engine.MainPlayerColor;
            original_mainPlayerFillColor = parent.Engine.MainPlayerFillColor;
            original_mainPlayerFillEnabled = parent.Engine.MainPlayerFillEnabled;
            original_showClaimLines = parent.Engine.ShowClaimLines;
            original_showHuntLines = parent.Engine.ShowHuntLines;
            original_showPetLines = parent.Engine.ShowPetLines;
            original_playerSize = parent.Engine.PlayerSize;
            original_NPCSize = parent.Engine.NPCSize;
            original_MOBSize = parent.Engine.MOBSize;
            original_spawnSelectSize = parent.Engine.SpawnSelectSize;
            original_spawnGroupSize = parent.Engine.SpawnGroupMemberSize;
            original_spawnHuntSize = parent.Engine.SpawnHuntSize;
            original_playerInfoFont = parent.Engine.PlayerInfoFont;
            original_playerInfoColor = parent.Engine.PlayerInfoColor;
            original_NPCInfoFont = parent.Engine.NPCInfoFont;
            original_NPCInfoColor = parent.Engine.NPCInfoColor;
            original_MOBInfoFont = parent.Engine.MOBInfoFont;
            original_MOBInfoColor = parent.Engine.MOBInfoColor;
            original_drawHeadingLines = parent.Engine.DrawHeadings;
            original_headingLineColor = parent.Engine.HeadingColor;

            //------------------------------------------------------------------------------------------
            //update controls with current state
            //------------------------------------------------------------------------------------------
            cmdChangeBackColor.BackColor = original_backColor;
            cmdGridLineColor.BackColor = original_gridLineColor;
            cmdGridTickColor.BackColor = original_gridTickColor;
            chkShowPlayerPosition.Checked = original_showPlayerPosition;
            cmdPlayerColor.BackColor = original_playerColor;
            cmdNPCColor.BackColor = original_npcColor;
            cmdEnemyColor.BackColor = original_enemyColor;
            chkUsePlayerColor.Checked = original_usePlayerColor;
            cmdPlayerColor.Enabled = original_usePlayerColor;
            udGridSize.Value = original_gridSize;
            chkShowGridLines.Checked = original_showGridLines;
            chkShowGridTicks.Checked = original_showGridTicks;
            chkShowMapAlternative.Checked = original_showAlt;
            chkShowMapAlternativeAlways.Checked = original_showAltAlways;
            chkAutoRangeSnap.Checked = original_autoRangeSnap;
            tbOpacity.Value = (int)(original_backOpacity * 100);
            lblOpacityPercent.Text = ((int)(original_backOpacity * 100)) + "%";
            tbMapImageOpacity.Value = (int)(original_mapimgOpacity * 100);
            lblMapImageOpacityPercent.Text = ((int)(original_mapimgOpacity * 100)) + "%";
            chkShowMapAlternativeAlways.Enabled = original_showAlt;
            lblMapAltOpacityTitle.Enabled = original_showAlt;
            tbMapImageOpacity.Enabled = original_showAlt;
            lblMapImageOpacityPercent.Enabled = original_showAlt;
            chkEnableLineDepthFilter.Checked = original_depthFilterLines;
            chkEnableSpawnDepthFilter.Checked = original_depthFilterSpawns;
            chkDepthFilterUseAlpha.Checked = original_depthFilterAlpha;
            udDepthAlphaMin.Value = original_depthFilterAlphaMin;
            udDepthAlphaMax.Value = original_depthFilterAlphaMax;
            udDepthAlphaMin.Enabled = original_depthFilterAlpha;
            udDepthAlphaMax.Enabled = original_depthFilterAlpha;
            txtDepthDistance.Text = original_depthFilterDistance.ToString();
            txtDepthCutoff.Text = original_depthFilterCutoff.ToString();
            udPollFrequency.Value = (decimal)original_pollfreq;
            chkScaleLines.Checked = original_scaleLines;
            cmdSelectedColor.BackColor = original_selectedColor;
            chkShowTextOutline.Checked = original_showTextOutline;
            chkShowTextShadow.Checked = original_showTextShadow;
            cmdTextGlowColor.BackColor = original_textOutlineColor;
            chkShowRadarRange.Checked = original_showRadarRange;
            cmdRadarRangeColor.BackColor = original_radarRangeColor;
            cmdHuntLineColor.BackColor = original_huntLineColor;
            cmdHuntLockedLineColor.BackColor = original_huntLockedLineColor;
            cmdClaimLineColor.BackColor = original_claimLineColor;
            cmdPetLineColor.BackColor = original_petLineColor;
            cmdGroupMemberColor.BackColor = original_groupMemberColor;
            cmdRaidMemberColor.BackColor = original_raidMemberColor;
            txtInfoTemplate.Text = original_infoTemplate;
            chkInfoAllPlayers.Checked = original_infoAllPlayers;
            chkInfoAllNPCs.Checked = original_infoAllNPCs;
            chkInfoAllEnemies.Checked = original_infoAllEnemies;
            cmdMainPlayerColor.BackColor = parent.Engine.MainPlayerColor;
            cmdMainPlayerFillColor.BackColor = parent.Engine.MainPlayerFillColor;
            chkUseMainPlayerFill.Checked = parent.Engine.MainPlayerFillEnabled;
            chkShowClaimLines.Checked = original_showClaimLines;
            chkShowHuntLines.Checked = original_showHuntLines;
            chkShowPetLines.Checked = original_showPetLines;
            udPlayerSize.Value = (decimal)original_playerSize;
            udNPCSize.Value = (decimal)original_NPCSize;
            udMOBSize.Value = (decimal)original_MOBSize;
            udSpawnSelectSize.Value = (decimal)original_spawnSelectSize;
            udSpawnGroupSize.Value = (decimal)original_spawnGroupSize;
            udSpawnHuntSize.Value = (decimal)original_spawnHuntSize;
            lblPlayerInfoFontDisplay.Text = GetPrettyFont(original_playerInfoFont);
            lblNPCInfoFontDisplay.Text = GetPrettyFont(original_NPCInfoFont);
            lblMOBInfoFontDisplay.Text = GetPrettyFont(original_MOBInfoFont);
            cmdPlayerInfoColor.BackColor = original_playerInfoColor;
            cmdNPCInfoColor.BackColor = original_NPCInfoColor;
            cmdMOBInfoColor.BackColor = original_MOBInfoColor;
            chkDrawHeadingLines.Checked = original_drawHeadingLines;
            cmdHeadingLineColor.BackColor = original_headingLineColor;

            displayMapPath();
            displayMapPackPath();

            //force text change event so that feedback elements are correctly set
            txtSIG_ZONE_ID_TextChanged(this, new EventArgs());
            txtSIG_ZONE_SHORT_TextChanged(this, new EventArgs());
            txtSIG_SPAWN_START_TextChanged(this, new EventArgs());
            txtSIG_SPAWN_END_TextChanged(this, new EventArgs());
            txtSIG_MY_ID_TextChanged(this, new EventArgs());
            txtSIG_MY_TARGET_TextChanged(this, new EventArgs());
            txtSIG_INSTANCE_ID_TextChanged(this, new EventArgs());
        }

        private void displayMapPath()
        {
            DirectoryInfo mapPath = new DirectoryInfo(parent.Engine.Data.FilePath);
            if (mapPath.Exists)
            {
                lblMapPath.Text = mapPath.FullName;
                pbFilePathWarning.Visible = false;
            }
            else
            {
                lblMapPath.Text = parent.Engine.Data.FilePath;
                pbFilePathWarning.Visible = true;
            }
        }

        private void displayMapPackPath()
        {
            DirectoryInfo mapPath = new DirectoryInfo(parent.MapPackPath);
            if (mapPath.Exists && File.Exists(parent.MapPackPath + Program.MapIniFile))
            {
                lblMapPackImagePath.Text = mapPath.FullName;
                pbMapPackWarning.Visible = false;
            }
            else
            {
                lblMapPackImagePath.Text = parent.MapPackPath;
                pbMapPackWarning.Visible = true;
            }
        }

        private void setMapPath(string path)
        {
            DirectoryInfo mapPath = new DirectoryInfo(path);
            if (!mapPath.Exists)
                return;

            parent.Engine.Data.FilePath = path;
            displayMapPath();
        }

        private void setMapPackPath(string path)
        {
            DirectoryInfo mapPath = new DirectoryInfo(path);
            if (!mapPath.Exists)
                return;

            parent.MapPackPath = path;
            ((IFFXIMapImageContainer)parent.Instance).ResetImageMap();
            displayMapPackPath();

            if (((IFFXIMapImageContainer)parent.Instance).Maps.Empty)
            {
                MessageBox.Show(string.Format("Warning: The {0} file in the path {1} did not contains valid zone data. Please ensure it is a valid map pack file and that it has not been corrupted.", Program.MapIniFile, path), "Map Pack Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string GetPrettyFont(Font font)
        {
            return font.Name + ", " + font.SizeInPoints + "pt";
        }

        private void cmdChangeBackColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = cmdChangeBackColor.BackColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdChangeBackColor.BackColor = dColorChanger.Color;
                parent.BackColor = dColorChanger.Color;
            }
        }

        private void tbOpacity_Scroll(object sender, EventArgs e)
        {
            lblOpacityPercent.Text = tbOpacity.Value + "%";
            parent.LayerOpacity = ((double)tbOpacity.Value / 100);
        }

        private void tbMapImageOpacity_Scroll(object sender, EventArgs e)
        {
            lblMapImageOpacityPercent.Text = tbMapImageOpacity.Value + "%";
            parent.Engine.MapAlternativeOpacity = ((float)tbMapImageOpacity.Value / 100);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                //Save GLOBAL config
                if (isSigsDefault())
                {
                    //if the entered signatures are the same as the internal default version, then clear entries from the registry (they are unneeded)
                    controller.Config.Remove("sigversion");
                    controller.Config.Remove("SIG_ZONE_ID");
                    controller.Config.Remove("SIG_ZONE_SHORT");
                    controller.Config.Remove("SIG_SPAWN_START");
                    controller.Config.Remove("SIG_SPAWN_END");
                    controller.Config.Remove("SIG_MY_ID");
                    controller.Config.Remove("SIG_MY_TARGET");
                    controller.Config.Remove("SIG_INSTANCE_ID");
                }
                else if (isSigsDirty())
                {
                    bool alreadyCustom = controller.Config.Exists("sigversion");

                    //only save the signatures if the user actually modified them
                    controller.Config["sigversion"] = Application.ProductVersion;
                    controller.Config["SIG_ZONE_ID"] = FFXIGameInstance.SIG_ZONE_ID;
                    controller.Config["SIG_ZONE_SHORT"] = FFXIGameInstance.SIG_ZONE_SHORT;
                    controller.Config["SIG_SPAWN_START"] = FFXIGameInstance.SIG_SPAWN_START;
                    controller.Config["SIG_SPAWN_END"] = FFXIGameInstance.SIG_SPAWN_END;
                    controller.Config["SIG_MY_ID"] = FFXIGameInstance.SIG_MY_ID;
                    controller.Config["SIG_MY_TARGET"] = FFXIGameInstance.SIG_MY_TARGET;
                    controller.Config["SIG_INSTANCE_ID"] = FFXIGameInstance.SIG_INSTANCE_ID;

                    if (!alreadyCustom)
                    {
                        //warn the user that any hand entered sigs will get clobbered without confirmation when upgrading to a new app version
                        MessageBox.Show(Program.GetLang("msg_sig_warning_text"), Program.GetLang("msg_sig_warning_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                controller.Config["UseGlobalHotkeys"] = controller.HotkeyEnabled;
                controller.Config["HotkeysConsume"] = controller.HotkeyConsumeInput;
                foreach (KeyValuePair<string, Actions<Keys>.Action> pair in controller.Actions)
                {
                    controller.Config[pair.Key] = (int)pair.Value.Trigger;
                }

                //Save PROFILE config
                if (parent != null)
                {
                    parent.Config["LayerOpacity"] = parent.LayerOpacity;
                    parent.Config["BackColor"] = parent.BackColor.ToArgb();
                    parent.Config["ShowGridLines"] = parent.Engine.ShowGridLines;
                    parent.Config["ShowGridTicks"] = parent.Engine.ShowGridTicks;
                    parent.Config["GridSize"] = parent.Engine.GridSize;
                    parent.Config["GridLineColor"] = parent.Engine.GridLineColor.ToArgb();
                    parent.Config["GridTickColor"] = parent.Engine.GridTickColor.ToArgb();
                    parent.Config["ShowPlayerPosition"] = parent.Engine.ShowPlayerPosition;
                    parent.Config["PlayerColor"] = parent.Engine.PlayerColor.ToArgb();
                    parent.Config["NPCColor"] = parent.Engine.NPCColor.ToArgb();
                    parent.Config["MOBColor"] = parent.Engine.MOBColor.ToArgb();
                    parent.Config["UsePlayerColor"] = parent.Engine.UsePlayerColor;
                    parent.Config["MapImgOpacity"] = parent.Engine.MapAlternativeOpacity;
                    parent.Config["ShowMapAlt"] = parent.Engine.ShowMapAlternative;
                    parent.Config["ShowMapAltAlways"] = parent.Engine.ShowMapAlternativeAlways;
                    parent.Config["MapPath"] = parent.Engine.Data.FilePath;
                    parent.Config["MapPackPath"] = parent.MapPackPath;
                    parent.Config["AutoRangeSnap"] = parent.Engine.AutoRangeSnap;
                    parent.Config["DepthFilterLines"] = parent.Engine.DepthFilterLines;
                    parent.Config["DepthFilterSpawns"] = parent.Engine.DepthFilterSpawns;
                    parent.Config["DepthFilterAlpha"] = parent.Engine.DepthFilterAlpha;
                    parent.Config["DepthAlphaMin"] = parent.Engine.DepthAlphaMin;
                    parent.Config["DepthAlphaMax"] = parent.Engine.DepthAlphaMax;
                    parent.Config["DepthDistance"] = parent.Engine.DepthDistance;
                    parent.Config["DepthCutoff"] = parent.Engine.DepthCutoff;
                    parent.Config["PollFreq"] = parent.PollFrequency;
                    parent.Config["ScaleLines"] = parent.Engine.ScaleLines;
                    parent.Config["SelectedColor"] = parent.Engine.SelectedColor.ToArgb();
                    parent.Config["TextOutlineEnabled"] = parent.Engine.TextOutlineEnabled;
                    parent.Config["TextOutlineColor"] = parent.Engine.TextOutlineColor.ToArgb();
                    parent.Config["TextShadowEnabled"] = parent.Engine.TextShadowEnabled;
                    parent.Config["ShowRadarRange"] = parent.Engine.ShowRadarRange;
                    parent.Config["RadarRangeColor"] = parent.Engine.RadarRangeColor.ToArgb();
                    parent.Config["HuntChainColor"] = parent.Engine.HuntChainColor.ToArgb();
                    parent.Config["HuntLockedChainColor"] = parent.Engine.HuntLockedChainColor.ToArgb();
                    parent.Config["ClaimChainColor"] = parent.Engine.ClaimChainColor.ToArgb();
                    parent.Config["PetChainColor"] = parent.Engine.PetChainColor.ToArgb();
                    parent.Config["GroupMemberColor"] = parent.Engine.GroupMemberColor.ToArgb();
                    parent.Config["RaidMemberColor"] = parent.Engine.RaidMemberColor.ToArgb();
                    parent.Config["InfoTemplate"] = parent.Engine.InfoTemplate;
                    parent.Config["ShowAllPlayerInfo"] = parent.Engine.ShowAllPlayerInfo;
                    parent.Config["ShowAllNPCInfo"] = parent.Engine.ShowAllNPCInfo;
                    parent.Config["ShowAllEnemyInfo"] = parent.Engine.ShowAllEnemyInfo;
                    parent.Config["MainPlayerColor"] = parent.Engine.MainPlayerColor.ToArgb();
                    parent.Config["MainPlayerFillColor"] = parent.Engine.MainPlayerFillColor.ToArgb();
                    parent.Config["MainPlayerFillEnabled"] = parent.Engine.MainPlayerFillEnabled;
                    parent.Config["ShowClaimLines"] = parent.Engine.ShowClaimLines;
                    parent.Config["ShowHuntLines"] = parent.Engine.ShowHuntLines;
                    parent.Config["ShowPetLines"] = parent.Engine.ShowPetLines;
                    parent.Config["PlayerSize"] = parent.Engine.PlayerSize;
                    parent.Config["NPCSize"] = parent.Engine.NPCSize;
                    parent.Config["MOBSize"] = parent.Engine.MOBSize;
                    parent.Config["SpawnSelectSize"] = parent.Engine.SpawnSelectSize;
                    parent.Config["SpawnGroupMemberSize"] = parent.Engine.SpawnGroupMemberSize;
                    parent.Config["SpawnHuntSize"] = parent.Engine.SpawnHuntSize;
                    parent.Config["PlayerInfoColor"] = parent.Engine.PlayerInfoColor.ToArgb();
                    parent.Config["NPCInfoColor"] = parent.Engine.NPCInfoColor.ToArgb();
                    parent.Config["MOBInfoColor"] = parent.Engine.MOBInfoColor.ToArgb();
                    parent.Config.Set<Font>("PlayerInfoFont", parent.Engine.PlayerInfoFont);
                    parent.Config.Set<Font>("NPCInfoFont", parent.Engine.NPCInfoFont);
                    parent.Config.Set<Font>("MOBInfoFont", parent.Engine.MOBInfoFont);
                    parent.Config["DrawHeadingLines"] = parent.Engine.DrawHeadings;
                    parent.Config["HeadingLineColor"] = parent.Engine.HeadingColor.ToArgb();
                }
            }
            else
            {
                // Cancel GLOBAL config
                FFXIGameInstance.SIG_ZONE_ID = original_SIG_ZONE_ID;
                FFXIGameInstance.SIG_ZONE_SHORT = original_SIG_ZONE_SHORT;
                FFXIGameInstance.SIG_SPAWN_START = original_SIG_SPAWN_START;
                FFXIGameInstance.SIG_SPAWN_END = original_SIG_SPAWN_END;
                FFXIGameInstance.SIG_MY_ID = original_SIG_MY_ID;
                FFXIGameInstance.SIG_MY_TARGET = original_SIG_MY_TARGET;
                FFXIGameInstance.SIG_INSTANCE_ID = original_SIG_INSTANCE_ID;

                controller.HotkeyEnabled = original_enable_filtering;
                controller.ActionKey = original_action_key;
                controller.HotkeyConsumeInput = original_consume_input;
                foreach (KeyValuePair<string, Actions<Keys>.Action> pair in controller.Actions)
                { //push the original bindings back out
                    pair.Value.Trigger = original_bindlist[pair.Value];
                }

                // Cancel PROFILE config
                if (parent != null)
                {
                    parent.LayerOpacity = original_backOpacity;
                    parent.BackColor = original_backColor;
                    parent.Engine.GridSize = original_gridSize;
                    parent.Engine.ShowGridLines = original_showGridLines;
                    parent.Engine.ShowGridTicks = original_showGridTicks;
                    parent.Engine.GridLineColor = original_gridLineColor;
                    parent.Engine.GridTickColor = original_gridTickColor;
                    parent.Engine.ShowPlayerPosition = original_showPlayerPosition;
                    parent.Engine.PlayerColor = original_playerColor;
                    parent.Engine.NPCColor = original_npcColor;
                    parent.Engine.MOBColor = original_enemyColor;
                    parent.Engine.UsePlayerColor = original_usePlayerColor;
                    parent.Engine.MapAlternativeOpacity = original_mapimgOpacity;
                    parent.Engine.ShowMapAlternative = original_showAlt;
                    parent.Engine.ShowMapAlternativeAlways = original_showAltAlways;

                    //only wreck the map data if there is a reason to
                    if (parent.Engine.Data.FilePath != original_mapPath)
                        parent.Engine.Data.FilePath = original_mapPath;
                    if (parent.MapPackPath != original_mapPackPath)
                    {
                        parent.MapPackPath = original_mapPackPath;
                        ((IFFXIMapImageContainer)parent.Instance).ResetImageMap();
                    }

                    parent.Engine.AutoRangeSnap = original_autoRangeSnap;
                    parent.Engine.DepthFilterLines = original_depthFilterLines;
                    parent.Engine.DepthFilterSpawns = original_depthFilterSpawns;
                    parent.Engine.DepthFilterAlpha = original_depthFilterAlpha;
                    parent.Engine.DepthAlphaMin = original_depthFilterAlphaMin;
                    parent.Engine.DepthAlphaMax = original_depthFilterAlphaMax;
                    parent.Engine.DepthDistance = original_depthFilterDistance;
                    parent.Engine.DepthCutoff = original_depthFilterCutoff;
                    parent.PollFrequency = original_pollfreq;
                    parent.Engine.ScaleLines = original_scaleLines;
                    parent.Engine.SelectedColor = original_selectedColor;
                    parent.Engine.TextOutlineEnabled = original_showTextOutline;
                    parent.Engine.TextOutlineColor = original_textOutlineColor;
                    parent.Engine.TextShadowEnabled = original_showTextShadow;
                    parent.Engine.ShowRadarRange = original_showRadarRange;
                    parent.Engine.RadarRangeColor = original_radarRangeColor;
                    parent.Engine.HuntChainColor = original_huntLineColor;
                    parent.Engine.HuntLockedChainColor = original_huntLockedLineColor;
                    parent.Engine.ClaimChainColor = original_claimLineColor;
                    parent.Engine.PetChainColor = original_petLineColor;
                    parent.Engine.GroupMemberColor = original_groupMemberColor;
                    parent.Engine.RaidMemberColor = original_raidMemberColor;
                    parent.Engine.InfoTemplate = original_infoTemplate;
                    parent.Engine.ShowAllPlayerInfo = original_infoAllPlayers;
                    parent.Engine.ShowAllNPCInfo = original_infoAllNPCs;
                    parent.Engine.ShowAllEnemyInfo = original_infoAllEnemies;
                    parent.Engine.MainPlayerColor = original_mainPlayerColor;
                    parent.Engine.MainPlayerFillColor = original_mainPlayerFillColor;
                    parent.Engine.MainPlayerFillEnabled = original_mainPlayerFillEnabled;
                    parent.Engine.ShowClaimLines = original_showClaimLines;
                    parent.Engine.ShowHuntLines = original_showHuntLines;
                    parent.Engine.ShowPetLines = original_showPetLines;
                    parent.Engine.PlayerSize = original_playerSize;
                    parent.Engine.NPCSize = original_NPCSize;
                    parent.Engine.MOBSize = original_MOBSize;
                    parent.Engine.SpawnSelectSize = original_spawnSelectSize;
                    parent.Engine.SpawnGroupMemberSize = original_spawnGroupSize;
                    parent.Engine.SpawnHuntSize = original_spawnHuntSize;
                    parent.Engine.PlayerInfoFont = original_playerInfoFont;
                    parent.Engine.PlayerInfoColor = original_playerInfoColor;
                    parent.Engine.NPCInfoFont = original_NPCInfoFont;
                    parent.Engine.NPCInfoColor = original_NPCInfoColor;
                    parent.Engine.MOBInfoFont = original_MOBInfoFont;
                    parent.Engine.MOBInfoColor = original_MOBInfoColor;
                    parent.Engine.DrawHeadings = original_drawHeadingLines;
                    parent.Engine.HeadingColor = original_headingLineColor;
                }
            }
            controller.HotkeySilent = false;
            base.OnClosing(e);
        }

        private void lblProductName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.ResGlobal.GetString("config_app_url"));
        }

        private void chkShowGridLines_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowGridLines = chkShowGridLines.Checked;
        }

        private void chkShowGridTicks_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowGridTicks = chkShowGridTicks.Checked;
        }

        private void chkShowPlayerPosition_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowPlayerPosition = chkShowPlayerPosition.Checked;
        }


        private void chkShowMapAlternative_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowMapAlternative = chkShowMapAlternative.Checked;
            chkShowMapAlternativeAlways.Enabled = chkShowMapAlternative.Checked;
            lblMapAltOpacityTitle.Enabled = chkShowMapAlternative.Checked;
            tbMapImageOpacity.Enabled = chkShowMapAlternative.Checked;
            lblMapImageOpacityPercent.Enabled = chkShowMapAlternative.Checked;
        }

        private void chkShowMapAlternativeAlways_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowMapAlternativeAlways = chkShowMapAlternativeAlways.Checked;
        }

        private void cmdGridLineColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = cmdGridLineColor.BackColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdGridLineColor.BackColor = dColorChanger.Color;
                parent.Engine.GridLineColor = dColorChanger.Color;
            }
        }

        private void cmdGridTickColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = cmdGridTickColor.BackColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdGridTickColor.BackColor = dColorChanger.Color;
                parent.Engine.GridTickColor = dColorChanger.Color;
            }
        }

        private void cmdNPCColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = cmdNPCColor.BackColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdNPCColor.BackColor = dColorChanger.Color;
                parent.Engine.NPCColor = dColorChanger.Color;
            }
        }

        private void cmdEnemyColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = cmdEnemyColor.BackColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdEnemyColor.BackColor = dColorChanger.Color;
                parent.Engine.MOBColor = dColorChanger.Color;
            }
        }

        private void cmdBrowseMapPath_Click(object sender, EventArgs e)
        {
            //display clobber warning to the user
            if (parent.Engine.Data.Dirty)
            {
                if (MessageBox.Show(Program.GetLang("msg_config_pathchangeloss_text"), Program.GetLang("msg_config_pathchangeloss_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }

            //show the browse dialog
            dBrowseFolder.SelectedPath = lblMapPath.Text;
            if (dBrowseFolder.ShowDialog() == DialogResult.OK)
            {
                setMapPath(dBrowseFolder.SelectedPath);
            }
        }

        private void cmdBrowseMapPackImagePath_Click(object sender, EventArgs e)
        {
            dBrowseFolder.SelectedPath = parent.MapPackPath;
            if (dBrowseFolder.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dBrowseFolder.SelectedPath + "\\" + Program.MapIniFile))
                {
                    setMapPackPath(dBrowseFolder.SelectedPath);
                }
                else
                {
                    MessageBox.Show(string.Format(Program.GetLang("msg_mappack_badsel_text"), dBrowseFolder.SelectedPath, Program.MapIniFile), Program.GetLang("msg_mappack_badsel_title"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void chkAutoRangeSnap_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.AutoRangeSnap = chkAutoRangeSnap.Checked;
        }

        private void chkEnableLineDepthFilter_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.DepthFilterLines = chkEnableLineDepthFilter.Checked;
        }

        private void chkEnableSpawnDepthFilter_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.DepthFilterSpawns = chkEnableSpawnDepthFilter.Checked;
        }

        private void chkDepthFilterUseAlpha_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.DepthFilterAlpha = chkDepthFilterUseAlpha.Checked;
            udDepthAlphaMin.Enabled = chkDepthFilterUseAlpha.Checked;
            udDepthAlphaMax.Enabled = chkDepthFilterUseAlpha.Checked;
        }

        private void udDepthAlphaMin_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.DepthAlphaMin = (int)udDepthAlphaMin.Value;
        }

        private void udDepthAlphaMax_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.DepthAlphaMax = (int)udDepthAlphaMax.Value;
        }

        private void txtDepthDistance_Validating(object sender, CancelEventArgs e)
        {
            float newValue;
            if (float.TryParse(txtDepthDistance.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out newValue))
            {
                parent.Engine.DepthDistance = newValue;
            }
            else
            {
                txtDepthDistance.Text = parent.Engine.DepthDistance.ToString();
            }
        }

        private void txtDepthCutoff_Validating(object sender, CancelEventArgs e)
        {
            float newValue;
            if (float.TryParse(txtDepthCutoff.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out newValue))
            {
                parent.Engine.DepthCutoff = newValue;
            }
            else
            {
                txtDepthCutoff.Text = parent.Engine.DepthCutoff.ToString();
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tcOptions_Selected(object sender, TabControlEventArgs e)
        {
            fCustomize.lastTab = e.TabPage.Name;
        }

        private void udPollFrequency_ValueChanged(object sender, EventArgs e)
        {
            parent.PollFrequency = (int)udPollFrequency.Value;
        }

        private void cmdSelectedColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.SelectedColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdSelectedColor.BackColor = dColorChanger.Color;
                parent.Engine.SelectedColor = dColorChanger.Color;
            }
        }

        private void chkShowTextOutline_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.TextOutlineEnabled = chkShowTextOutline.Checked;
        }

        private void chkShowTextShadow_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.TextShadowEnabled = chkShowTextShadow.Checked;
        }

        private void cmdTextGlowColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.TextOutlineColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                parent.Engine.TextOutlineColor = dColorChanger.Color;
                cmdTextGlowColor.BackColor = parent.Engine.TextOutlineColor; //becuase of the applied alpha
            }
        }

        private void chkShowRadarRange_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowRadarRange = chkShowRadarRange.Checked;
        }

        private void cmdRadarRangeColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.RadarRangeColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdRadarRangeColor.BackColor = dColorChanger.Color;
                parent.Engine.RadarRangeColor = dColorChanger.Color;
            }
        }

        private void cmdHuntLineColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.HuntChainColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdHuntLineColor.BackColor = dColorChanger.Color;
                parent.Engine.HuntChainColor = dColorChanger.Color;
            }
        }

        private void cmdClaimLineColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.ClaimChainColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdClaimLineColor.BackColor = dColorChanger.Color;
                parent.Engine.ClaimChainColor = dColorChanger.Color;
            }
        }

        private void cmdPetLineColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.PetChainColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdPetLineColor.BackColor = dColorChanger.Color;
                parent.Engine.PetChainColor = dColorChanger.Color;
            }
        }

        private void cmdGroupMemberColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.GroupMemberColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdGroupMemberColor.BackColor = dColorChanger.Color;
                parent.Engine.GroupMemberColor = dColorChanger.Color;
            }
        }

        private void cmdRaidMemberColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.RaidMemberColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdRaidMemberColor.BackColor = dColorChanger.Color;
                parent.Engine.RaidMemberColor = dColorChanger.Color;
            }
        }

        private void chkInfoAllPlayers_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowAllPlayerInfo = chkInfoAllPlayers.Checked;
        }

        private void chkInfoAllNPCs_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowAllNPCInfo = chkInfoAllNPCs.Checked;
        }

        private void chkInfoAllEnemies_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowAllEnemyInfo = chkInfoAllEnemies.Checked;
        }

        private void txtInfoTemplate_TextChanged(object sender, EventArgs e)
        {
            parent.Engine.InfoTemplate = txtInfoTemplate.Text;
        }

        private void udGridSize_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.GridSize = (int)udGridSize.Value;
        }

        private void chkScaleLines_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ScaleLines = chkScaleLines.Checked;
        }

        private void cmdMainPlayerColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.MainPlayerColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdMainPlayerColor.BackColor = dColorChanger.Color;
                parent.Engine.MainPlayerColor = dColorChanger.Color;
            }
        }

        private void chkUseMainPlayerFill_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.MainPlayerFillEnabled = chkUseMainPlayerFill.Checked;
        }

        private void cmdMainPlayerFillColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.MainPlayerFillColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdMainPlayerFillColor.BackColor = dColorChanger.Color;
                parent.Engine.MainPlayerFillColor = dColorChanger.Color;
            }
        }

        private void chkShowHuntLines_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowHuntLines = chkShowHuntLines.Checked;
        }

        private void chkShowClaimLines_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowClaimLines = chkShowClaimLines.Checked;
        }

        private void chkShowPetLines_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.ShowPetLines = chkShowPetLines.Checked;
        }

        private void cmdResetSigs_Click(object sender, EventArgs e)
        {
            txtSIG_ZONE_ID.Text = original_SIG_ZONE_ID;
            txtSIG_ZONE_SHORT.Text = original_SIG_ZONE_SHORT;
            txtSIG_SPAWN_START.Text = original_SIG_SPAWN_START;
            txtSIG_SPAWN_END.Text = original_SIG_SPAWN_END;
            txtSIG_MY_ID.Text = original_SIG_MY_ID;
            txtSIG_MY_TARGET.Text = original_SIG_MY_TARGET;
            txtSIG_INSTANCE_ID.Text = original_SIG_INSTANCE_ID;
        }

        private void cmdSigDefaults_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Program.GetLang("msg_confirm_sigreset_text"), Program.GetLang("msg_confirm_sigreset_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtSIG_ZONE_ID.Text = FFXIGameInstance.DEFAULT_SIG_ZONE_ID;
                txtSIG_ZONE_SHORT.Text = FFXIGameInstance.DEFAULT_SIG_ZONE_SHORT;
                txtSIG_SPAWN_START.Text = FFXIGameInstance.DEFAULT_SIG_SPAWN_START;
                txtSIG_SPAWN_END.Text = FFXIGameInstance.DEFAULT_SIG_SPAWN_END;
                txtSIG_MY_ID.Text = FFXIGameInstance.DEFAULT_SIG_MY_ID;
                txtSIG_MY_TARGET.Text = FFXIGameInstance.DEFAULT_SIG_MY_TARGET;
                txtSIG_INSTANCE_ID.Text = FFXIGameInstance.DEFAULT_SIG_INSTANCE_ID;
            }
        }

        private bool isSigsDirty()
        {
            bool isDirty = false;

            isDirty = isDirty || (FFXIGameInstance.SIG_ZONE_ID != original_SIG_MY_ID);
            isDirty = isDirty || (FFXIGameInstance.SIG_ZONE_SHORT != original_SIG_ZONE_SHORT);
            isDirty = isDirty || (FFXIGameInstance.SIG_SPAWN_START != original_SIG_SPAWN_START);
            isDirty = isDirty || (FFXIGameInstance.SIG_SPAWN_END != original_SIG_SPAWN_END);
            isDirty = isDirty || (FFXIGameInstance.SIG_MY_ID != original_SIG_MY_ID);
            isDirty = isDirty || (FFXIGameInstance.SIG_MY_TARGET != original_SIG_MY_TARGET);
            isDirty = isDirty || (FFXIGameInstance.SIG_INSTANCE_ID != original_SIG_INSTANCE_ID);

            return isDirty;
        }

        private bool isSigsDefault()
        {
            bool isDefault = true;

            isDefault = isDefault && (FFXIGameInstance.SIG_ZONE_ID == FFXIGameInstance.DEFAULT_SIG_ZONE_ID);
            isDefault = isDefault && (FFXIGameInstance.SIG_ZONE_SHORT == FFXIGameInstance.DEFAULT_SIG_ZONE_SHORT);
            isDefault = isDefault && (FFXIGameInstance.SIG_SPAWN_START == FFXIGameInstance.DEFAULT_SIG_SPAWN_START);
            isDefault = isDefault && (FFXIGameInstance.SIG_SPAWN_END == FFXIGameInstance.DEFAULT_SIG_SPAWN_END);
            isDefault = isDefault && (FFXIGameInstance.SIG_MY_ID == FFXIGameInstance.DEFAULT_SIG_MY_ID);
            isDefault = isDefault && (FFXIGameInstance.SIG_MY_TARGET == FFXIGameInstance.DEFAULT_SIG_MY_TARGET);
            isDefault = isDefault && (FFXIGameInstance.SIG_INSTANCE_ID == FFXIGameInstance.DEFAULT_SIG_INSTANCE_ID);

            return isDefault;
        }

        private void txtSIG_ZONE_ID_TextChanged(object sender, EventArgs e)
        {
            FFXIGameInstance.SIG_ZONE_ID = txtSIG_ZONE_ID.Text;
            if (parent != null)
            {
                try
                {
                    IntPtr pTest = parent.Instance.Reader.FindSignature(txtSIG_ZONE_ID.Text, Program.ModuleName);
                    if (pTest == IntPtr.Zero)
                    {
                        pbStatus_SIG_ZONE_ID.Image = Properties.Resources.icon_no;
                    }
                    else
                    {
                        pbStatus_SIG_ZONE_ID.Image = Properties.Resources.icon_ok;
                    }
                }
                catch (Exception)
                {
                    pbStatus_SIG_ZONE_ID.Image = Properties.Resources.icon_no;
                }
            }
        }

        private void txtSIG_ZONE_SHORT_TextChanged(object sender, EventArgs e)
        {
            FFXIGameInstance.SIG_ZONE_SHORT = txtSIG_ZONE_SHORT.Text;
            if (parent != null)
            {
                try
                {
                    IntPtr pTest = parent.Instance.Reader.FindSignature(txtSIG_ZONE_SHORT.Text, Program.ModuleName);
                    if (pTest == IntPtr.Zero)
                    {
                        pbStatus_SIG_ZONE_SHORT.Image = Properties.Resources.icon_no;
                    }
                    else
                    {
                        pbStatus_SIG_ZONE_SHORT.Image = Properties.Resources.icon_ok;
                    }
                }
                catch (Exception)
                {
                    pbStatus_SIG_ZONE_SHORT.Image = Properties.Resources.icon_no;
                }
            }
        }

        private void txtSIG_SPAWN_START_TextChanged(object sender, EventArgs e)
        {
            FFXIGameInstance.SIG_SPAWN_START = txtSIG_SPAWN_START.Text;
            if (parent != null)
            {
                try
                {
                    IntPtr pTest = parent.Instance.Reader.FindSignature(txtSIG_SPAWN_START.Text, Program.ModuleName);
                    if (pTest == IntPtr.Zero)
                    {
                        pbStatus_SIG_SPAWN_START.Image = Properties.Resources.icon_no;
                    }
                    else
                    {
                        pbStatus_SIG_SPAWN_START.Image = Properties.Resources.icon_ok;
                    }
                }
                catch (Exception)
                {
                    pbStatus_SIG_SPAWN_START.Image = Properties.Resources.icon_no;
                }
            }
        }

        private void txtSIG_SPAWN_END_TextChanged(object sender, EventArgs e)
        {
            FFXIGameInstance.SIG_SPAWN_END = txtSIG_SPAWN_END.Text;
            if (parent != null)
            {
                try
                {
                    IntPtr pTest = parent.Instance.Reader.FindSignature(txtSIG_SPAWN_END.Text, Program.ModuleName);
                    if (pTest == IntPtr.Zero)
                    {
                        pbStatus_SIG_SPAWN_END.Image = Properties.Resources.icon_no;
                    }
                    else
                    {
                        pbStatus_SIG_SPAWN_END.Image = Properties.Resources.icon_ok;
                    }
                }
                catch (Exception)
                {
                    pbStatus_SIG_SPAWN_END.Image = Properties.Resources.icon_no;
                }
            }
        }

        private void txtSIG_MY_ID_TextChanged(object sender, EventArgs e)
        {
            FFXIGameInstance.SIG_MY_ID = txtSIG_MY_ID.Text;
            if (parent != null)
            {
                try
                {
                    IntPtr pTest = parent.Instance.Reader.FindSignature(txtSIG_MY_ID.Text, Program.ModuleName);
                    if (pTest == IntPtr.Zero)
                    {
                        pbStatus_SIG_MY_ID.Image = Properties.Resources.icon_no;
                    }
                    else
                    {
                        pbStatus_SIG_MY_ID.Image = Properties.Resources.icon_ok;
                    }
                }
                catch (Exception)
                {
                    pbStatus_SIG_MY_ID.Image = Properties.Resources.icon_no;
                }
            }
        }

        private void txtSIG_MY_TARGET_TextChanged(object sender, EventArgs e)
        {
            FFXIGameInstance.SIG_MY_TARGET = txtSIG_MY_TARGET.Text;
            if (parent != null)
            {
                try
                {
                    IntPtr pTest = parent.Instance.Reader.FindSignature(txtSIG_MY_TARGET.Text, Program.ModuleName);
                    if (pTest == IntPtr.Zero)
                    {
                        pbStatus_SIG_MY_TARGET.Image = Properties.Resources.icon_no;
                    }
                    else
                    {
                        pbStatus_SIG_MY_TARGET.Image = Properties.Resources.icon_ok;
                    }
                }
                catch (Exception)
                {
                    pbStatus_SIG_MY_TARGET.Image = Properties.Resources.icon_no;
                }
            }
        }

        private void txtSIG_INSTANCE_ID_TextChanged(object sender, EventArgs e)
        {
            FFXIGameInstance.SIG_INSTANCE_ID = txtSIG_INSTANCE_ID.Text;
            if (parent != null)
            {
                try
                {
                    IntPtr pTest = parent.Instance.Reader.FindSignature(txtSIG_INSTANCE_ID.Text, Program.ModuleName);
                    if (pTest == IntPtr.Zero)
                    {
                        pbStatus_SIG_INSTANCE_ID.Image = Properties.Resources.icon_no;
                    }
                    else
                    {
                        pbStatus_SIG_INSTANCE_ID.Image = Properties.Resources.icon_ok;
                    }
                }
                catch (Exception)
                {
                    pbStatus_SIG_INSTANCE_ID.Image = Properties.Resources.icon_no;
                }
            }
        }

        private void chkEnableInputFiltering_CheckedChanged(object sender, EventArgs e)
        {
            controller.HotkeyEnabled = chkEnableInputFiltering.Checked;
        }

        private string GetPrettyKey(Keys key)
        {
            Keys code = key & Keys.KeyCode;
            string output = "";
            if (code != Keys.ControlKey && (key & Keys.Control) > 0)
                output += (output == "" ? "" : " + ") + "CTRL";
            if (code != Keys.Menu && (key & Keys.Alt) > 0)
                output += (output == "" ? "" : " + ") + "ALT";
            if (code != Keys.ShiftKey && (key & Keys.Shift) > 0)
                output += (output == "" ? "" : " + ") + "SHFT";
            output += (output == "" ? "" : " + ") + code.ToString();
            return output;
        }

        private Keys ConvertKey(Keys key)
        {
            Keys code = key & Keys.KeyCode;
            Keys result = code;
            if (code != Keys.ControlKey && (key & Keys.Control) > 0)
                result |= Keys.Control;
            if (code != Keys.Menu && (key & Keys.Alt) > 0)
                result |= Keys.Alt;
            if (code != Keys.ShiftKey && (key & Keys.Shift) > 0)
                result |= Keys.Shift;
            return result;
        }

        private void txtActionKey_KeyDown(object sender, KeyEventArgs e)
        {
            txtActionKey.Text = GetPrettyKey(e.KeyData);
            controller.ActionKey = ConvertKey(e.KeyData);
        }

        private void lvHotKeyBindings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down) //lets try not to break accessibility
                return;
            else if (lvHotKeyBindings.SelectedItems.Count == 1)
            {
                ListViewItem item = lvHotKeyBindings.SelectedItems[0];
                Actions<Keys>.Action action = (Actions<Keys>.Action)item.Tag;
                bool updateall = false;
                if (e.KeyData == Keys.Escape)
                {
                    action.Unbind();
                }
                else
                {
                    Keys key = ConvertKey(e.KeyData);
                    updateall = controller.Actions.Bound(key);
                    action.Trigger = key;
                }
                item.SubItems[1].Text = GetPrettyKey(action.Trigger);
                if (updateall)
                    RefreshBindings();
            }
        }

        private void RefreshBindings()
        {
            foreach (ListViewItem item in lvHotKeyBindings.Items)
            {
                Actions<Keys>.Action action = (Actions<Keys>.Action)item.Tag;
                item.SubItems[1].Text = GetPrettyKey(action.Trigger);
            }
        }

        private void miClearBinding_Click(object sender, EventArgs e)
        {
            if (lvHotKeyBindings.SelectedItems.Count == 1)
            {
                ListViewItem item = lvHotKeyBindings.SelectedItems[0];
                Actions<Keys>.Action action = (Actions<Keys>.Action)item.Tag;
                action.Unbind();
                item.SubItems[1].Text = GetPrettyKey(action.Trigger);
            }
        }

        private void miResetBinding_Click(object sender, EventArgs e)
        {
            if (lvHotKeyBindings.SelectedItems.Count == 1)
            {
                ListViewItem item = lvHotKeyBindings.SelectedItems[0];
                Actions<Keys>.Action action = (Actions<Keys>.Action)item.Tag;
                action.Trigger = original_bindlist[action];
                item.SubItems[1].Text = GetPrettyKey(action.Trigger);
            }
        }

        private void miUseDefaultBinding_Click(object sender, EventArgs e)
        {
            if (lvHotKeyBindings.SelectedItems.Count == 1)
            {
                ListViewItem item = lvHotKeyBindings.SelectedItems[0];
                Actions<Keys>.Action action = (Actions<Keys>.Action)item.Tag;
                action.Trigger = action.DefaultTrigger;
                item.SubItems[1].Text = GetPrettyKey(action.Trigger);
            }
        }

        private void chkEnableHotkeyCancel_CheckedChanged(object sender, EventArgs e)
        {
            controller.HotkeyConsumeInput = chkEnableHotkeyCancel.Checked;
        }

        private void chkUsePlayerColor_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.UsePlayerColor = chkUsePlayerColor.Checked;
            cmdPlayerColor.Enabled = chkUsePlayerColor.Checked;
        }

        private void cmdPlayerColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.PlayerColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdPlayerColor.BackColor = dColorChanger.Color;
                parent.Engine.PlayerColor = dColorChanger.Color;
            }
        }

        private void udPlayerSize_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.PlayerSize = (float)udPlayerSize.Value;
        }

        private void udNPCSize_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.NPCSize = (float)udNPCSize.Value;
        }

        private void udMOBSize_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.MOBSize = (float)udMOBSize.Value;
        }

        private void udSpawnSelectSize_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.SpawnSelectSize = (float)udSpawnSelectSize.Value;
        }

        private void udSpawnGroupSize_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.SpawnGroupMemberSize = (float)udSpawnGroupSize.Value;
        }

        private void udSpawnHuntSize_ValueChanged(object sender, EventArgs e)
        {
            parent.Engine.SpawnHuntSize = (float)udSpawnHuntSize.Value;
        }

        private void cmdHuntLockedLineColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.HuntLockedChainColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdHuntLockedLineColor.BackColor = dColorChanger.Color;
                parent.Engine.HuntLockedChainColor = dColorChanger.Color;
            }
        }

        private void cmdPlayerInfoFont_Click(object sender, EventArgs e)
        {
            dFontChanger.Font = parent.Engine.PlayerInfoFont;
            if (dFontChanger.ShowDialog() == DialogResult.OK)
            {
                lblPlayerInfoFontDisplay.Text = GetPrettyFont(dFontChanger.Font);
                parent.Engine.PlayerInfoFont = dFontChanger.Font;
            }
        }

        private void cmdPlayerInfoColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.PlayerInfoColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdPlayerInfoColor.BackColor = dColorChanger.Color;
                parent.Engine.PlayerInfoColor = dColorChanger.Color;
            }
        }


        private void cmdNPCInfoFont_Click(object sender, EventArgs e)
        {
            dFontChanger.Font = parent.Engine.NPCInfoFont;
            if (dFontChanger.ShowDialog() == DialogResult.OK)
            {
                lblNPCInfoFontDisplay.Text = GetPrettyFont(dFontChanger.Font);
                parent.Engine.NPCInfoFont = dFontChanger.Font;
            }
        }

        private void cmdNPCInfoColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.NPCInfoColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdNPCInfoColor.BackColor = dColorChanger.Color;
                parent.Engine.NPCInfoColor = dColorChanger.Color;
            }
        }

        private void cmdMOBInfoFont_Click(object sender, EventArgs e)
        {
            dFontChanger.Font = parent.Engine.MOBInfoFont;
            if (dFontChanger.ShowDialog() == DialogResult.OK)
            {
                lblMOBInfoFontDisplay.Text = GetPrettyFont(dFontChanger.Font);
                parent.Engine.MOBInfoFont = dFontChanger.Font;
            }
        }

        private void cmdMOBInfoColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.MOBInfoColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdMOBInfoColor.BackColor = dColorChanger.Color;
                parent.Engine.MOBInfoColor = dColorChanger.Color;
            }
        }

        private void chkDrawHeadingLines_CheckedChanged(object sender, EventArgs e)
        {
            parent.Engine.DrawHeadings = chkDrawHeadingLines.Checked;
        }

        private void cmdHeadingLineColor_Click(object sender, EventArgs e)
        {
            dColorChanger.Color = parent.Engine.HeadingColor;
            if (dColorChanger.ShowDialog() == DialogResult.OK)
            {
                cmdHeadingLineColor.BackColor = dColorChanger.Color;
                parent.Engine.HeadingColor = dColorChanger.Color;
            }
        }

    }
}