using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using MapEngine;

namespace mappy {
   /// <summary>Responsible for the main ui, controlling map instances, and global hotkey processing.</summary>
   public partial class Controller : Component, IController {
      //private storage
      private Process m_instance = null;
      private fMap    m_window = null;
      private Config  m_config = null;
      private Hook    m_hook;
      private bool    m_actionkeyState = false;
      private Keys    m_actionkey = Keys.Control | Keys.ShiftKey;

      private Actions<Keys> m_actions;
      private MapEditors m_editors;

      //class and collection definitions
      private struct ProcessItem {
         public Process process;
         public override string ToString() {
            return process.MainWindowTitle;
         }
      }
      private Dictionary<int, ProcessItem> m_gamelist;

      //=================================================================================
      // Constructor
      //=================================================================================
      public Controller() {
         InitializeComponent();
         InitializeLanguage();

         //defines a list of built-in action handlers that may be bound to at run time
         m_actions = new Actions<Keys>();
         m_actions.RegisterAction("hk_center", Program.GetLang("hotkey_center"), HK_CenterView, Keys.Control | Keys.Alt | Keys.C);
         m_actions.RegisterAction("hk_snap", Program.GetLang("hotkey_snap"), HK_SnapRange, Keys.Control | Keys.Alt | Keys.S);
         m_actions.RegisterAction("hk_zoom_increase", Program.GetLang("hotkey_zoom_increase"), HK_ZoomIncrease, Keys.Control | Keys.Alt | Keys.OemPeriod);
         m_actions.RegisterAction("hk_zoom_decrease", Program.GetLang("hotkey_zoom_decrease"), HK_ZoomDecrease, Keys.Control | Keys.Alt | Keys.Oemcomma);
         m_actions.RegisterAction("hk_togglegrid", Program.GetLang("hotkey_togglegrid"), HK_ToggleGrid, Keys.Control | Keys.Alt | Keys.G);
         m_actions.RegisterAction("hk_togglelines", Program.GetLang("hotkey_togglelines"), HK_ToggleLines, Keys.Control | Keys.Alt | Keys.M);
         m_actions.RegisterAction("hk_togglelabels", Program.GetLang("hotkey_togglelabels"), HK_ToggleLabels, Keys.Control | Keys.Alt | Keys.L);
         m_actions.RegisterAction("hk_togglehidden", Program.GetLang("hotkey_togglehidden"), HK_ToggleHidden, Keys.Control | Keys.Alt | Keys.H);
         m_actions.RegisterAction("hk_toggleclickthru", Program.GetLang("hotkey_toggleclickthru"), HK_ToggleClickThrough, Keys.Control | Keys.Alt | Keys.T);
         m_actions.RegisterAction("hk_togglemapimage", Program.GetLang("hotkey_togglemapimage"), HK_ToggleMapImage, Keys.Control | Keys.Alt | Keys.I);
         m_actions.RegisterAction("hk_openprefs", Program.GetLang("hotkey_openprefs"), HK_OpenPreferences, Keys.Control | Keys.Alt | Keys.P);
         m_actions.RegisterAction("hk_exit", Program.GetLang("hotkey_exit"), HK_Exit, Keys.Control | Keys.Alt | Keys.Shift | Keys.X);
         m_actions.RegisterAction("hk_edit_start", Program.GetLang("hotkey_edit_start"), HK_EditStartLine, Keys.Alt | Keys.Shift | Keys.S);
         m_actions.RegisterAction("hk_edit_cancel", Program.GetLang("hotkey_edit_cancel"), HK_EditCancelLine, Keys.Alt | Keys.Shift | Keys.C);
         m_actions.RegisterAction("hk_edit_add", Program.GetLang("hotkey_edit_add"), HK_EditAddPoint, Keys.Alt | Keys.Shift | Keys.A);
         m_actions.RegisterAction("hk_edit_remove", Program.GetLang("hotkey_edit_remove"), HK_EditRemovePoint, Keys.Alt | Keys.Shift | Keys.R);

         //Set the initial editor. This must be available before form events begin to fire.
         m_editors = new MapEditors();
         m_editors.RegisterEditor(new MapNavigator());
         m_editors.RegisterEditor(new FFXIMapImageEditor());

         m_gamelist = new Dictionary<int, ProcessItem>();
         m_config = new RegistryConfig(Program.ConfigKey);

         //bind actions from the config
         foreach (KeyValuePair<string, Actions<Keys>.Action> pair in m_actions) {
            pair.Value.Trigger = m_config.GetValue<Keys>(pair.Key, pair.Value.Trigger);
         }

         TrayIcon.MouseDoubleClick += new MouseEventHandler(TrayIcon_MouseDoubleClick);
         TrayMenu.Opening += new CancelEventHandler(TrayMenu_Opening);
         miTrayExit.Click += new EventHandler(miTrayExit_Click);
         miInstance.SelectedIndexChanged += new EventHandler(miInstance_SelectedIndexChanged);
         miRefresh.Click += new EventHandler(miRefresh_Click);
         miSaveDefault.Click += new EventHandler(miSaveDefault_Click);
         miClearDefault.Click += new EventHandler(miClearDefault_Click);
         miActiveOnTop.CheckedChanged += new EventHandler(miActiveOnTop_CheckedChanged);
         miActiveClickthru.CheckedChanged += new EventHandler(miActiveClickthru_CheckedChanged);
         miActiveResizable.CheckedChanged += new EventHandler(miActiveResizable_CheckedChanged);
         miActiveDocked.CheckedChanged += new EventHandler(miActiveDocked_CheckedChanged);
         miActiveDraggable.CheckedChanged += new EventHandler(miActiveDraggable_CheckedChanged);
         miActiveShowSpawns.CheckedChanged += new EventHandler(miActiveShowSpawns_CheckedChanged);
         miActiveShowHidden.CheckedChanged += new EventHandler(miActiveShowHidden_CheckedChanged);
         miActiveShowLines.CheckedChanged += new EventHandler(miActiveShowLines_CheckedChanged);
         miActiveShowLabels.CheckedChanged += new EventHandler(miActiveShowLabels_CheckedChanged);
         miActiveShowMapImage.CheckedChanged += new EventHandler(miActiveShowMapImage_CheckedChanged);
         miActiveCenter.Click += new EventHandler(miActiveCenter_Click);
         miActiveSnapRange.Click += new EventHandler(miActiveSnapRange_Click);
         miAbout.Click += new EventHandler(miAbout_Click);
         miOptions.Click += new EventHandler(miOptions_Click);
         SetIcon(MapRes.MappyIcon);

         RefreshInstances();

         m_hook = new Hook(Hook.HookType.Keyboard | Hook.HookType.Mouse);
         m_hook.AllowCancel = m_config.Get("HotkeysConsume", false);
         m_hook.KeyDown += new Hook.HookKeyEventHandler(m_hook_KeyDown);
         m_hook.KeyUp += new Hook.HookKeyEventHandler(m_hook_KeyUp);
         m_hook.MouseWheel += new Hook.HookMouseEventHandler(m_hook_MouseWheel);
         m_hook.Enabled = m_config.Get("UseGlobalHotkeys", false);

         bool dns = m_config.Get("DNS_UseGlobalHotkeys", false);
         if(!m_hook.Enabled && !dns) {
            int button = MessageBoxEx.Show(Program.GetLang("msg_hotkey_enable_text"), Program.GetLang("msg_hotkey_enable_title"), new string[] { Program.GetLang("button_enable"), Program.GetLang("button_decline") }, MessageBoxIcon.Question, out dns);
            m_config["DNS_UseGlobalHotkeys"] = dns;
            if(button == 0) {
               m_hook.Enabled = true;
            }
         }

         m_actionkey = m_config.GetValue<Keys>("HotkeyModifier", m_actionkey);
         m_actionkeyState = false;
      }

      //=================================================================================
      // Global hotkey filtering
      //=================================================================================
      private void m_hook_MouseWheel(Hook sender, MouseEventArgs e, CancelEventArgs cancel) {
         if(!m_actionkeyState || m_window == null || m_window.Focused || m_window.IsDisposed || !m_window.IsHandleCreated)
            return;
         m_window.Zoom += (e.Delta / SystemInformation.MouseWheelScrollDelta);
         cancel.Cancel = true;
      }

      private void m_hook_KeyDown(Hook sender, KeyEventArgs e, CancelEventArgs cancel) {
         if(e.KeyData == m_actionkey)
            ActionKeyState = true;
         if (m_actions.Fire(e.KeyData))
            cancel.Cancel = true;
      }
      private void m_hook_KeyUp(Hook sender, KeyEventArgs e, CancelEventArgs cancel) {
         if((e.KeyCode & m_actionkey) > 0)
            ActionKeyState = false;
      }

      //=================================================================================
      // Properties
      //=================================================================================
      /// <summary>Gets the global config shared across all instances.</summary>
      public Config Config {
         get { return m_config; }
      }

      /// <summary>Gets the active game instance.</summary>
      public fMap Active {
         get {
            if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
               return null;
            return m_window;
         }
      }

      /// <summary>Gets or sets the action key.</summary>
      public Keys ActionKey {
         get { return m_actionkey; }
         set {
            m_actionkey = value;
            ActionKeyState = false; //in case the user changes it while its pressed
         }
      }

      /// <summary>Gets if the action key is currently being held down.</summary>
      public bool ActionKeyState {
         get { return m_actionkeyState; }
         private set {
            if(m_actionkeyState != value) {
               m_actionkeyState = value;
               if(m_window != null && !m_window.IsDisposed && m_window.IsHandleCreated)
                  m_window.SetActionState(value);
            }
         }
      }

      /// <summary>Gets or sets whether the application performs global hotkey filtering.</summary>
      public bool HotkeyEnabled {
         get { return m_hook.Enabled; }
         set { m_hook.Enabled = value; }
      }

      /// <summary>Temporarily suppress message processing without destroying the hook.</summary>
      public bool HotkeySilent {
         get { return m_hook.Silent; }
         set { m_hook.Silent = value; }
      }

      /// <summary>Gets or sets whether user input handled by an action is suppressed or forwarded to the active window.</summary>
      public bool HotkeyConsumeInput {
         get { return m_hook.AllowCancel; }
         set { m_hook.AllowCancel = value; }
      }

      /// <summary>Gets the list of bindable actions available to the application.</summary>
      public Actions<Keys> Actions {
         get { return m_actions; }
      }

      //=================================================================================
      // Methods
      //=================================================================================
      /// <summary>Sets the tray icon to the specified bitmap.</summary>
      public void SetIcon(Bitmap bitmap) {
         SetIcon(Icon.FromHandle(bitmap.GetHicon()));
      }
      /// <summary>Sets the tray icon to the specified icon.</summary>
      public void SetIcon(Icon icon) {
         TrayIcon.Icon = icon;
      }
      /// <summary>Displays a balloon help tip on the tray icon.</summary>
      public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon) {
         TrayIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
      }

      //=================================================================================
      // Private Functions
      //=================================================================================     
      // scan the process list and add any new target instances to the list
      private void RefreshInstances() {
         foreach (string Instance in Program.ProcessName) {
            Process[] processlist = Process.GetProcessesByName(Instance);
         
            foreach (Process process in processlist) {
                if (!m_gamelist.ContainsKey(process.Id)) {
                    ProcessItem item;
                    item.process = process;
                    m_gamelist.Add(process.Id, item);
                    miInstance.Items.Add(item);
                }
            }
         }

         if (m_gamelist.Count == 0) {
            ShowBalloonTip(1500, Program.GetLang("bubble_warn_nogames_title"), Program.GetLang("bubble_warn_nogames_text"), ToolTipIcon.Warning);
            string defInstance = m_config.Get("DefaultInstance", "");
            miSaveDefault.Enabled = false;
            if (defInstance == "")
               miClearDefault.Enabled = false;
         } else if (miInstance.SelectedIndex < 0) {
            miSaveDefault.Enabled = true;
            bool found = false;
            string defInstance = m_config.Get("DefaultInstance", "");
            if (defInstance == "")
               miClearDefault.Enabled = false;
            foreach (ProcessItem item in miInstance.Items) {
               if (item.ToString() == m_config.Get("DefaultInstance", "")) {
                  found = true;
                  miInstance.SelectedItem = item;
                  break;
               }
            }
            if(!found)
               miInstance.SelectedIndex = 0;
         }
         miInstance.Enabled = (m_gamelist.Count > 0);
      }

      // starts a new map instance using the given process
      private void LoadProcess(Process process) {
         if (m_instance != process) {
            try {
               Config cProcess = m_config.OpenKey("profiles\\" + process.MainWindowTitle);

               if (m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated) {
                  m_window = new fMap(this, process, cProcess);
                  m_window.Show();
               } else {
                  m_window.Process = process;
                  m_window.Config = cProcess;
               }
               return;
            } catch(MemoryReaderException) {
               ShowBalloonTip(1500, string.Format(Program.GetLang("bubble_error_loadpid_title"), process.MainWindowTitle), string.Format(Program.GetLang("bubble_error_loadpid_text"), process.MainWindowTitle), ToolTipIcon.Error);
            }
            m_instance = null;
         }
      }

      //=================================================================================
      // Action Handlers (hotkeys)
      //=================================================================================
      public bool HK_ToggleGrid(Actions<Keys>.ActionEventArgs e) {
         if (m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.ShowGridLines = !m_window.Engine.ShowGridLines;
         m_window.Engine.ShowGridTicks = m_window.Engine.ShowGridLines;
         return true;
      }

      public bool HK_OpenPreferences(Actions<Keys>.ActionEventArgs e) {
         fCustomize.BeginCustomization(this, m_window);
         return true;
      }

      public bool HK_Exit(Actions<Keys>.ActionEventArgs e) {
         Exit();
         return true;
      }

      public bool HK_CenterView(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.CenterView();
         return true;
      }

      public bool HK_SnapRange(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.SnapToRange();
         return true;
      }

      public bool HK_ToggleLines(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.ShowLines = !m_window.Engine.ShowLines;
         return true;
      }

      public bool HK_ToggleLabels(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.ShowLabels = !m_window.Engine.ShowLabels;
         return true;
      }

      public bool HK_ToggleHidden(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.ShowHiddenSpawns = !m_window.Engine.ShowHiddenSpawns;
         return true;
      }

      public bool HK_ToggleClickThrough(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.ClickThrough = !m_window.ClickThrough;
         return true;
      }

      public bool HK_ToggleMapImage(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.ShowMapAlternative = !m_window.Engine.ShowMapAlternative;
         return true;
      }

      public bool HK_EditStartLine(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.Data.EditStartLine();
         return true;
      }

      public bool HK_EditCancelLine(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.Data.EditCancelLine();
         return true;
      }

      public bool HK_EditAddPoint(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.Data.EditAddPoint();
         return true;
      }

      public bool HK_EditRemovePoint(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.Data.EditRemovePoint();
         return true;
      }

      public bool HK_ZoomIncrease(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.ZoomIn();
         return true;
      }

      public bool HK_ZoomDecrease(Actions<Keys>.ActionEventArgs e) {
         if(m_window == null || m_window.IsDisposed || !m_window.IsHandleCreated)
            return false;
         m_window.Engine.ZoomOut();
         return true;
      }

      //=================================================================================
      // Events
      //=================================================================================
      private void TrayMenu_Opening(object sender, CancelEventArgs e) {
         if (m_window == null) {
            miActiveClickthru.Enabled = false;
            miActiveOnTop.Enabled = false;
            miActiveDocked.Enabled = false;
            miActiveDraggable.Enabled = false;
            miActiveResizable.Enabled = false;
            miActiveShowSpawns.Enabled = false;
            miActiveShowHidden.Enabled = false;
            miActiveShowLines.Enabled = false;
            miActiveShowLabels.Enabled = false;
            miActiveCenter.Enabled = false;
            miActiveSnapRange.Enabled = false;
            miActiveShowMapImage.Enabled = false;
         } else {
            m_window.SetActionState(false);
            miActiveOnTop.Checked = m_window.OnTop;
            miActiveClickthru.Checked = m_window.ClickThrough;
            miActiveDocked.Checked = m_window.Docked;
            miActiveDraggable.Checked = m_window.Draggable;
            miActiveResizable.Checked = m_window.Resizable;
            miActiveShowSpawns.Checked = m_window.Engine.ShowSpawns;
            miActiveShowHidden.Checked = m_window.Engine.ShowHiddenSpawns;
            miActiveShowLines.Checked = m_window.Engine.ShowLines;
            miActiveShowLabels.Checked = m_window.Engine.ShowLabels;
            miActiveShowMapImage.Checked = m_window.Engine.ShowMapAlternative;

            miActiveClickthru.Enabled = true;
            miActiveOnTop.Enabled = true;
            miActiveDocked.Enabled = true;
            miActiveDraggable.Enabled = true;
            miActiveResizable.Enabled = true;
            miActiveShowSpawns.Enabled = true;
            miActiveShowHidden.Enabled = m_window.Engine.ShowSpawns;
            miActiveShowLines.Enabled = true;
            miActiveShowLabels.Enabled = true;
            miActiveShowMapImage.Enabled = true;
            miActiveCenter.Enabled = true;
            miActiveSnapRange.Enabled = true;
         }
      }
      
      private void miActiveDocked_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Docked = miActiveDocked.Checked;
      }

      private void miActiveDraggable_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Draggable = miActiveDraggable.Checked;
      }

      private void miActiveResizable_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Resizable = miActiveResizable.Checked;
      }

      private void miActiveClickthru_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.ClickThrough = miActiveClickthru.Checked;
      }

      private void miActiveOnTop_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.OnTop = miActiveOnTop.Checked;
      }

      private void miActiveShowHidden_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Engine.ShowHiddenSpawns = miActiveShowHidden.Checked;
      }

      private void miActiveShowLabels_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Engine.ShowLabels = miActiveShowLabels.Checked;
      }

      private void miActiveShowSpawns_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null) {
            m_window.Engine.ShowSpawns = miActiveShowSpawns.Checked;
            miActiveShowHidden.Enabled = miActiveShowSpawns.Checked;
         }
      }

      private void miActiveShowLines_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Engine.ShowLines = miActiveShowLines.Checked;
      }

      private void miActiveShowMapImage_CheckedChanged(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Engine.ShowMapAlternative = miActiveShowMapImage.Checked;
      }

      private void miActiveSnapRange_Click(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Engine.SnapToRange();
      }

      private void miActiveCenter_Click(object sender, EventArgs e) {
         if(m_window != null)
            m_window.Engine.CenterView();
      }

      private void miRefresh_Click(object sender, EventArgs e) {
         RefreshInstances();
      }

      private void miInstance_SelectedIndexChanged(object sender, EventArgs e) {
         ProcessItem item = (ProcessItem)miInstance.SelectedItem;
         LoadProcess(item.process);
      }

      private void miSaveDefault_Click(object sender, EventArgs e) {
         if (miInstance.SelectedItem != null) {
            m_config["DefaultInstance"] = miInstance.SelectedItem.ToString();
            miClearDefault.Enabled = true;
         }
      }

      void miClearDefault_Click(object sender, EventArgs e) {
         m_config["DefaultInstance"] = "";
         miClearDefault.Enabled = false;
      }

      private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
         if (e.Button == MouseButtons.Left && m_window != null && !m_window.IsDisposed)
            m_window.Activate();
      }

      private void miOptions_Click(object sender, EventArgs e) {
         fCustomize.BeginCustomization(this, m_window);
      }

      private void miAbout_Click(object sender, EventArgs e) {
         fAboutBox about = new fAboutBox();
         about.Show();
      }

      private void miTrayExit_Click(object sender, EventArgs e) {
         Exit();
      }

      private void Save() {
         m_config["UseGlobalHotkeys"] = m_hook.Enabled;
         m_config["HotkeyModifier"] = (int)m_actionkey;

         m_config.Save();
      }

      /// <summary>Terminates the app after giving the user the chance to save modifications made to the map data.</summary>
      public void Exit() {
         if(m_window != null && m_window.Engine.Data.Dirty) {
            switch(System.Windows.Forms.MessageBox.Show(Program.GetLang("msg_unsaved_text"), Program.GetLang("msg_unsaved_title"), System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
               case DialogResult.Yes:
                  m_window.Engine.Data.Save();
                  break;
               case DialogResult.Cancel:
                  return;
            }
         }
         Terminate();
      }

      /// <summary>Terminates the app in its entirety</summary>
      public void Terminate() {
         //Save the global config
         Save();

         //Close the active map window, which is also responsible for saving profile changes to the config
         if (m_window != null && !m_window.IsDisposed)
            m_window.Close();
            
         //Deallocate objects and kill the app
         TrayIcon.Dispose();
         Application.Exit();
      }

      public GameInstance CreateInstance(Process process, fMap window) {
      return new FFXIGameInstance(process, window.Engine, m_config, window.MapPackPath, Program.ModuleName);
      }

      public MapEditors Editors {
         get { return m_editors; }
      }
   }
}