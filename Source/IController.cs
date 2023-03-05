using MapEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static mappy.Controller;

namespace mappy {
   public interface IController {
      GameInstance CreateInstance(Process process, fMap window);
      void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon);
      void Terminate();
      void RefreshInstances(Dictionary<int, Controller.ProcessItem> gamelist, ToolStripComboBox instance, ToolStripMenuItem saveDefault, ToolStripMenuItem clearDefault);
      void miInstance_SelectedIndexChanged(object sender, EventArgs e);
      void miRefresh_Click(object sender, EventArgs e);
      void miSaveDefault_Click(object sender, EventArgs e);
      void miClearDefault_Click(object sender, EventArgs e);
      Config Config { get; }
      fMap Active { get; }
      Keys ActionKey { get; set; }
      bool ActionKeyState { get; }
      bool HotkeyEnabled { get; set; }
      bool HotkeySilent { get; set; }
      bool HotkeyConsumeInput { get; set; }
      Actions<Keys> Actions { get; }
      MapEditors Editors { get; }
   }
}