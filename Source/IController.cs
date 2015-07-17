using MapEngine;
using System.Diagnostics;
using System.Windows.Forms;

namespace mappy {
   public interface IController {
      GameInstance CreateInstance(Process process, fMap window);
      void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon);
      void Terminate();
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