using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

public class Hook : IDisposable {   
   private delegate IntPtr LowLevelHookProc(int nCode, IntPtr wParam, IntPtr lParam);
   public delegate void HookKeyEventHandler(Hook sender, KeyEventArgs e, CancelEventArgs cancel);
   public delegate void HookMouseEventHandler(Hook sender, MouseEventArgs e, CancelEventArgs cancel);

   private const int WH_KEYBOARD_LL = 13;
   private const int WM_KEYDOWN     = 0x0100;
   private const int WM_KEYUP       = 0x0101;
   private const int WM_SYSKEYDOWN  = 0x0104;
   private const int WM_SYSKEYUP    = 0x0105;

   private const int WH_MOUSE_LL    = 14;
   private const int WM_LBUTTONDOWN = 0x0201;
   private const int WM_LBUTTONUP   = 0x0202;
   private const int WM_MOUSEMOVE   = 0x0200;
   private const int WM_RBUTTONDOWN = 0x0204;
   private const int WM_RBUTTONUP   = 0x0205;
   private const int WM_MOUSEWHEEL  = 0x020A;

   private IntPtr hKeyboard;
   private IntPtr hMouse;
   private IntPtr hMod;
   private int  lastKey;
   private bool lastPressed;
   private bool lastCanceled;

   private bool m_enabled;
   private bool m_silent;
   private bool m_suppressmultiple;
   private bool m_allowCancel;
   private HookType m_type;

   public event HookKeyEventHandler KeyDown;
   public event HookKeyEventHandler KeyUp;

   public event HookMouseEventHandler MouseDown;
   public event HookMouseEventHandler MouseUp;
   public event HookMouseEventHandler MouseMove;
   public event HookMouseEventHandler MouseWheel;

   private LowLevelHookProc pKeyboard;
   private LowLevelHookProc pMouse;

   [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
   private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelHookProc lpfn, IntPtr hMod, uint dwThreadId);

   [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
   [return: MarshalAs(UnmanagedType.Bool)]
   private static extern bool UnhookWindowsHookEx(IntPtr hhk);

   [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
   private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

   [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
   private static extern IntPtr GetModuleHandle(string lpModuleName);

   [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
   public static extern short GetAsyncKeyState(int vkey);

   [StructLayout(LayoutKind.Sequential)]
   private struct POINT {
      public int x;
      public int y;
   }

   [StructLayout(LayoutKind.Sequential)]
   private struct MSLLHOOKSTRUCT {
      public POINT pt;
      public uint mouseData;
      public uint flags;
      public uint time;
      public IntPtr dwExtraInfo;
   }

   public enum HookType : int {
      Keyboard = 1,
      Mouse = 2
   }

   public Hook(HookType Type) {
      m_type = Type;
      m_suppressmultiple = true;
      lastPressed = false;
      lastCanceled = false;
      m_allowCancel = true;

      //cache the main module address for the current process
      Process p = Process.GetCurrentProcess();
      hMod = GetModuleHandle(p.MainModule.ModuleName);

      //must store a reference to the delagate to prevent it from being GC'd
      pKeyboard = new LowLevelHookProc(KeyboardProc);
      pMouse = new LowLevelHookProc(MouseProc);
   }
   ~Hook() {
      Dispose();
   }

   /// <summary>Gets or sets whether the hook is active.</summary>
   public bool Enabled {
      get { return m_enabled; }
      set {
         if (m_enabled != value) {
            m_enabled = value;
            if (m_enabled) {
               //Set the hook
               if ((m_type & HookType.Keyboard) > 0)
                  hKeyboard = SetWindowsHookEx(WH_KEYBOARD_LL, pKeyboard, hMod, 0);
               if ((m_type & HookType.Mouse) > 0)
                  hMouse = SetWindowsHookEx(WH_MOUSE_LL, pMouse, hMod, 0);
            } else {
               //Remove the hook
               if (hKeyboard != IntPtr.Zero)
                  UnhookWindowsHookEx(hKeyboard);
               if (hMouse != IntPtr.Zero)
                  UnhookWindowsHookEx(hMouse);
               hKeyboard = IntPtr.Zero;
               hMouse = IntPtr.Zero;
            }
         }
      }
   }

   /// <summary>Gets or sets whether message processing is suppressed.</summary>
   public bool Silent {
      get { return m_silent; }
      set { m_silent = value; }
   }

   /// <summary>Suppress additional key down messages until key is released. Lowlevel hooks will continue to raise messages while the key is held.</summary>
   public bool SuppressMultiple {
      get { return m_suppressmultiple; }
      set { m_suppressmultiple = value; }
   }

   /// <summary>Gets or sets whether the cancel flag is honored from the event handler.</summary>
   public bool AllowCancel {
      get { return m_allowCancel; }
      set { m_allowCancel = value; }
   }

   /// <summary>Gets the combined state of Control, Alt, and Shift keys</summary>
   public Keys ModifierKeys {
      get {
         Keys mods = Keys.None;
         if((GetAsyncKeyState((int)Keys.ControlKey) & 0x8000) > 0)
            mods |= Keys.Control;
         if((GetAsyncKeyState((int)Keys.Menu) & 0x8000) > 0)
            mods |= Keys.Alt;
         if((GetAsyncKeyState((int)Keys.ShiftKey) & 0x8000) > 0)
            mods |= Keys.Shift;
         return mods;
      }
   }

   /// <summary>Adds the combined state of the Control, Alt, and Shift keys to the passed in key. If the actual key is a modifier itself then it's state will not be combined in the final result.</summary>
   /// <param name="CurrentKey">The actual key as reported by windows</param>
   private Keys AddModifiers(Keys CurrentKey) {
      Keys key = CurrentKey;
      if(CurrentKey != Keys.ControlKey && (GetAsyncKeyState((int)Keys.ControlKey) & 0x8000) > 0)
         key |= Keys.Control;
      if(CurrentKey != Keys.Menu && (GetAsyncKeyState((int)Keys.Menu) & 0x8000) > 0)
         key |= Keys.Alt;
      if(CurrentKey != Keys.ShiftKey && (GetAsyncKeyState((int)Keys.ShiftKey) & 0x8000) > 0)
         key |= Keys.Shift;
      return key;
   }

   /// <summary>Normalizes a virtual key code into a compatible Keys enumeration</summary>
   private Keys NormalizeKey(int vkCode) {
      Keys key = (Keys)vkCode;

      //Windows form events cannot differentiate between left and right mod keys
      if(key == Keys.LControlKey || key == Keys.RControlKey)
         key = Keys.ControlKey;
      if(key == Keys.LMenu || key == Keys.RMenu)
         key = Keys.Menu;
      if(key == Keys.LShiftKey || key == Keys.RShiftKey)
         key = Keys.ShiftKey;
      return AddModifiers(key);
   }

   private IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam) {
      if(nCode > -1 && !m_silent) {
         int msg = (int)wParam;
         if (KeyDown != null && (msg == WM_KEYDOWN || msg == WM_SYSKEYDOWN)) {
            int vkCode = Marshal.ReadInt32(lParam);

            //The hook will continuously send the message as long as the key is held down.
            // To prevent the KeyDown event from being raised more than necessary, the last key is
            // tracked and ignored if it is the same as the current one
            if (!m_suppressmultiple || !lastPressed || vkCode != lastKey) {
               lastKey = vkCode;
               lastPressed = true;
               CancelEventArgs cancel = new CancelEventArgs(false);
               KeyDown(this, new KeyEventArgs(NormalizeKey(vkCode)), cancel);
               lastCanceled = cancel.Cancel && m_allowCancel;
               if(lastCanceled)
                  return (IntPtr)1;
            } else if (lastPressed && vkCode == lastKey && lastCanceled) {
               return (IntPtr)1;
            }
         }
         if (KeyUp != null && (msg == WM_KEYUP || msg == WM_SYSKEYUP)) {
            int vkCode = Marshal.ReadInt32(lParam);
            lastPressed = false;
            CancelEventArgs cancel = new CancelEventArgs(false);
            KeyUp(this, new KeyEventArgs(NormalizeKey(vkCode)), cancel);
            if(cancel.Cancel && m_allowCancel)
               return (IntPtr)1;
         }
      }
      return CallNextHookEx(hKeyboard, nCode, wParam, lParam);
   }
   private IntPtr MouseProc(int nCode, IntPtr wParam, IntPtr lParam) {
      if(nCode > -1 & !m_silent) {
         int msg = (int)wParam;
         
         //Only marshall the structure if the event is being handled by the client in an attempt to save some cycles.
         if (MouseDown != null && (msg == WM_LBUTTONDOWN | msg == WM_RBUTTONDOWN)) {
            MSLLHOOKSTRUCT data = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            MouseButtons button = MouseButtons.Left;
            if (msg == WM_RBUTTONDOWN)
               button = MouseButtons.Right;
            CancelEventArgs cancel = new CancelEventArgs(false);
            MouseDown(this, new MouseEventArgs(button, 1, data.pt.x, data.pt.y, 0), cancel);
            if(cancel.Cancel && m_allowCancel)
               return (IntPtr)1;
         }
         if (MouseUp != null && (msg == WM_LBUTTONUP | msg == WM_RBUTTONUP)) {
            MSLLHOOKSTRUCT data = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            MouseButtons button = MouseButtons.Left;
            if (msg == WM_RBUTTONUP)
               button = MouseButtons.Right;
            CancelEventArgs cancel = new CancelEventArgs(false);
            MouseUp(this, new MouseEventArgs(button, 1, data.pt.x, data.pt.y, 0), cancel);
            if(cancel.Cancel && m_allowCancel)
               return (IntPtr)1;
         }
         if (MouseMove != null && msg == WM_MOUSEMOVE) {
            MSLLHOOKSTRUCT data = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            CancelEventArgs cancel = new CancelEventArgs(false);
            MouseMove(this, new MouseEventArgs(MouseButtons.None, 0, data.pt.x, data.pt.y, 0), cancel);
            if(cancel.Cancel && m_allowCancel)
               return (IntPtr)1;
         }
         if (MouseWheel != null && msg == WM_MOUSEWHEEL) {
            MSLLHOOKSTRUCT data = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            int delta = ((int)data.mouseData >> 16);
            CancelEventArgs cancel = new CancelEventArgs(false);
            MouseWheel(this, new MouseEventArgs(MouseButtons.None, 0, data.pt.x, data.pt.y, delta), cancel);
            if(cancel.Cancel && m_allowCancel)
               return (IntPtr)1;
         }
      }
      return CallNextHookEx(hMouse, nCode, wParam, lParam);
   }

   public void Dispose() {
      //always unhook during garbage collection
      if (hKeyboard != IntPtr.Zero)
         UnhookWindowsHookEx(hKeyboard);
      if (hMouse != IntPtr.Zero)
         UnhookWindowsHookEx(hMouse);
   }
}