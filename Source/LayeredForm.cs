using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.ComponentModel;

namespace System.Windows.Forms {
   public class LayeredForm : Form {
      private bool m_layered = false;
      private bool m_clickthru = false;
      private bool m_ontop = false;
      private bool m_docked = false;
      private bool m_draggable = false;
      private bool m_resizable = true;

      private int  m_resizeThreshold = 3;
      private IntPtr hWindowDC;
      private IntPtr hBufferDC;
      private Graphics gBuffer;
      private Rectangle rBounds;
      private bool bUpdateNeeded = true;
      private double m_layerOpacity = 0;

      [StructLayout(LayoutKind.Sequential)]
      private struct POINT {
         public Int32 x;
         public Int32 y;
         public POINT(Int32 x, Int32 y) { this.x = x; this.y = y; }
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct SIZE {
         public Int32 cx;
         public Int32 cy;
         public SIZE(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
      }

      [StructLayout(LayoutKind.Sequential, Pack = 1)]
      private struct ARGB {
         public byte Blue;
         public byte Green;
         public byte Red;
         public byte Alpha;
      }

      [StructLayout(LayoutKind.Sequential, Pack = 1)]
      private struct BLENDFUNCTION {
         public byte BlendOp;
         public byte BlendFlags;
         public byte SourceConstantAlpha;
         public byte AlphaFormat;
      }

      private const int   WM_NCHITTEST      = 0x0084;
      private const int   WM_NCLBUTTONDOWN  = 0xA1;
      private const int   WM_MOUSEWHEEL     = 0x020A;
      private const int   WM_LBUTTONDOWN    = 0x201;

      private const int   HTERROR           = (-2);
      private const int   HTTRANSPARENT     = (-1);
      private const int   HTNOWHERE         = 0;
      private const int   HTCLIENT          = 1;
      private const int   HTCAPTION         = 2;
      private const int   HTSYSMENU         = 3;
      private const int   HTGROWBOX         = 4;
      private const int   HTSIZE            = HTGROWBOX;
      private const int   HTMENU            = 5;
      private const int   HTHSCROLL         = 6;
      private const int   HTVSCROLL         = 7;
      private const int   HTMINBUTTON       = 8;
      private const int   HTMAXBUTTON       = 9;
      private const int   HTLEFT            = 10;
      private const int   HTRIGHT           = 11;
      private const int   HTTOP             = 12;
      private const int   HTTOPLEFT         = 13;
      private const int   HTTOPRIGHT        = 14;
      private const int   HTBOTTOM          = 15;
      private const int   HTBOTTOMLEFT      = 16;
      private const int   HTBOTTOMRIGHT     = 17;
      private const int   HTBORDER          = 18;
      private const int   HTREDUCE          = HTMINBUTTON;
      private const int   HTZOOM            = HTMAXBUTTON;
      private const int   HTSIZEFIRST       = HTLEFT;
      private const int   HTSIZELAST        = HTBOTTOMRIGHT;
      private const int   HTOBJECT          = 19;
      private const int   HTCLOSE           = 20;
      private const int   HTHELP            = 21;

      private const int   WS_EX_TRANSPARENT = 0x020;
      private const int   WS_EX_LAYERED = 0x80000;
      private const int   WS_EX_COMPOSITED  = 0x02000000;

      private const Int32 ULW_COLORKEY = 0x1;
      private const Int32 ULW_ALPHA = 0x2;
      private const Int32 ULW_OPAQUE = 0x4;
      private const int   LWA_ALPHA = 0x2;
      private const int   LWA_COLORKEY = 0x1;
      private const int   GWL_EXSTYLE = -20;
      private const byte  AC_SRC_OVER = 0x00;
      private const byte  AC_SRC_ALPHA = 0x01;

      [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
      private static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, uint dwFlags);

      [DllImport("user32.dll")]
      private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

      [DllImport("user32.dll")]
      private static extern IntPtr GetDC(IntPtr hWnd);

      [DllImport("user32.dll")]
      private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

      [DllImport("gdi32.dll", SetLastError = true)]
      private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
      
      [DllImport("gdi32.dll")]
      private static extern bool DeleteDC(IntPtr hdc);

      [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
      private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

      [DllImport("gdi32.dll")]
      private static extern bool DeleteObject(IntPtr hObject);

      [DllImport("user32.dll", SetLastError = true)]
      private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

      [DllImport("user32.dll")]
      private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

      [DllImportAttribute("user32.dll")]
      private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

      [DllImportAttribute("user32.dll")]
      private static extern bool ReleaseCapture();

      //foreground forcing
      [DllImport("user32.dll")]
      public static extern IntPtr GetForegroundWindow();

      [DllImport("user32.dll")]
      private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

      [DllImport("user32.dll")]
      private static extern bool IsIconic(IntPtr hWnd);

      [DllImport("user32.dll")]
      private static extern bool IsZoomed(IntPtr hWnd);

      [DllImport("kernel32.dll")]
      private static extern IntPtr GetCurrentThread();

      [DllImport("kernel32.dll")]
      private static extern uint GetCurrentThreadId();

      [DllImport("user32.dll")]
      private static extern IntPtr SetFocus(IntPtr hWnd);

      [DllImport("User32.Dll")]
      private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);

      [DllImport("user32.dll")]
      private static extern bool SetForegroundWindow(IntPtr hWnd);

      [DllImport("user32.dll")]
      private static extern IntPtr BringWindowToTop(IntPtr hWnd);

      [DllImport("user32.dll")]
      private static extern bool ShowWindow(IntPtr hWnd, ShowWindowFlags nCmdShow);

      private enum ShowWindowFlags : uint {
         SW_HIDE = 0,
         SW_SHOWNORMAL = 1,
         SW_NORMAL = 1,
         SW_SHOWMINIMIZED = 2,
         SW_SHOWMAXIMIZED = 3,
         SW_MAXIMIZE = 3,
         SW_SHOWNOACTIVATE = 4,
         SW_SHOW = 5,
         SW_MINIMIZE = 6,
         SW_SHOWMINNOACTIVE = 7,
         SW_SHOWNA = 8,
         SW_RESTORE = 9,
         SW_SHOWDEFAULT = 10,
         SW_FORCEMINIMIZE = 11,
         SW_MAX = 11
      }

      protected override void OnHandleCreated(EventArgs e) {
         rBounds = new Rectangle(0, 0, Width - 1, Height - 1);
         hWindowDC = GetDC(Handle);
         hBufferDC = CreateCompatibleDC(hWindowDC);
         base.OnHandleCreated(e);
         if (Layered && IsHandleCreated && !DesignMode)
            this.OnPaint(null); //call directly because invalidate doesnt work during initialization
      }
      protected override void OnHandleDestroyed(EventArgs e) {
         if(gBuffer != null)
            gBuffer.Dispose();
         DeleteDC(hBufferDC);
         ReleaseDC(IntPtr.Zero, hWindowDC);
         base.OnHandleDestroyed(e);
      }

      /// <summary>Creates the System.Drawing.Graphics for the control.</summary>
      new public Graphics CreateGraphics() {
         if (Layered && !DesignMode) {
            //The buffer is a memory bitmap with an alpha channel. If the form size has changed since this buffer was made then it will need to be recreated.
            if (bUpdateNeeded) {
               //create a memory buffer with an alpha channel
               Bitmap surfaceNew = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
               IntPtr surfaceOld = SelectObject(hBufferDC, surfaceNew.GetHbitmap());
               if (surfaceOld != IntPtr.Zero)
                  DeleteObject(surfaceOld);

               //create the GDI+ object using the device context
               Graphics buffer = Graphics.FromHdc(hBufferDC);
               buffer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
               buffer.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

               //dispose the old buffer if required
               if(gBuffer != null)
                  gBuffer.Dispose();
               gBuffer = buffer;
               bUpdateNeeded = false;
            }

            //Prepare drawing by clearing all color information
            gBuffer.Clear(Color.FromArgb((int)(255 * m_layerOpacity), BackColor));
            return gBuffer;
         } else {
            return base.CreateGraphics();
         }
      }

      /// <summary>Causes the control to redraw the invalidated regions within its client area.</summary>
      new public void Update() {
         if (Layered && !DesignMode) {
            //Send the buffer to the o/s
            SIZE size = new SIZE(Width, Height);
            POINT pointSource = new POINT(0, 0);
            POINT topPos = new POINT(Left, Top);
            BLENDFUNCTION blend = new BLENDFUNCTION();
            blend.BlendOp = AC_SRC_OVER;
            blend.BlendFlags = 0;
            blend.SourceConstantAlpha = 255;
            blend.AlphaFormat = AC_SRC_ALPHA;
            UpdateLayeredWindow(Handle, IntPtr.Zero, ref topPos, ref size, hBufferDC, ref pointSource, 0, ref blend, ULW_ALPHA);
         } else {
            base.Update();
         }
      }

      protected override void OnResize(EventArgs e) {
         base.OnResize(e);

         //Update the draw region and recreated the buffer bitmap at the new size
         rBounds.Width = Width - 1;
         rBounds.Height = Height - 1;
         bUpdateNeeded = true;
         Invalidate();
      }

      protected override void OnPaint(PaintEventArgs e) {
         if (Layered && !DesignMode) {
            //Generate our own graphic buffer if in layered mode
            base.OnPaint(new PaintEventArgs(this.CreateGraphics(), rBounds));
            Update();
         } else {
            //othewise lets use the buffer provided by the control
            base.OnPaint(new PaintEventArgs(e.Graphics, this.ClientRectangle));
         }
      }

      [DefaultValue(false)]
      [Category("Layered")]
      [Description("Gets or sets whether the form uses a window layer to render its content.")]
      public bool Layered {
         get { return m_layered; }
         set {
            //only do something if the mode is being changed
            if(m_layered != value) {
               m_layered = value;

               if (!DesignMode) {
                  //Do not toggle the layered mode if the window has not been created yet
                  if (!IsHandleCreated)
                     return;

                  //If we are turning layered mode ON and the layered style is already set (usually due to form opacity),
                  //  then remove it immediatly to kill the paint redirection.
                  int style = GetWindowLong(Handle, GWL_EXSTYLE);
                  if(m_layered && ((style & WS_EX_LAYERED) > 0))
                     SetWindowLong(Handle, GWL_EXSTYLE, style - WS_EX_LAYERED);

                  //Add the layered style if turning layered mode on, or remove the style if turning off
                  style |= WS_EX_LAYERED;
                  if(!value && Opacity == 0) //keep layered style on if were going to use the regular form opacity mode
                     style -= WS_EX_LAYERED;
                  SetWindowLong(Handle, GWL_EXSTYLE, style);
                  if(!m_layered && Opacity > 0) //layered mode is being turned off. if the normal form opacity is set then turn on the simple LWA mode
                     SetLayeredWindowAttributes(Handle, 0, (byte)(255 * Opacity), LWA_ALPHA);

                  //Force repaint
                  bUpdateNeeded = true;
                  Invalidate();
               }
            }
         }
      }

      [DefaultValue(0)]
      [Category("Layered")]
      [Description("Gets or sets the opacity percentage of the background when in layered mode or the entire form when not in layered mode.")]
      [TypeConverterAttribute(typeof(OpacityConverter))]
      [DisplayName("Opacity Layer")]
      public double LayerOpacity {
         get { return m_layerOpacity; }
         set {
            m_layerOpacity = value;
            
            //opacity is a decimal notation percentage.
            if(m_layerOpacity < 0)
               m_layerOpacity = 0;
            if(m_layerOpacity > 1)
               m_layerOpacity = 1;
            Invalidate();
         }
      }

      [DefaultValue(false)]
      [Category("Layered")]
      [Description("Gets or sets whether the form ignores mouse input, passing it to the window underneath.")]
      public bool ClickThrough {
         get { return m_clickthru; }
         set { 
            //only do something if the mode is being changed
            if(m_clickthru != value) {
               m_clickthru = value;

               if (!DesignMode) {
                  int style = GetWindowLong(Handle, GWL_EXSTYLE);
                  style |= WS_EX_TRANSPARENT;
                  if(!value)
                     style -= WS_EX_TRANSPARENT;
                  SetWindowLong(Handle, GWL_EXSTYLE, style);
               }
            }
         }
      }

      [DefaultValue(false)]
      [Category("Layered")]
      [Description("Gets or sets whether the form is always on top.")]
      public bool OnTop {
         get { return m_ontop; }
         set { m_ontop = value; }
      }

      [DefaultValue(false)]
      [Category("Layered")]
      [Description("Gets or sets whether the form get docked to the FFXI Instance.")]
      public bool Docked {
         get { return m_docked; }
         set { m_docked = value; }
      }

      [DefaultValue(false)]
      [Category("Layered")]
      [Description("Gets or sets whether the form can be moved by dragging the client area with the mouse.")]
      public bool Draggable {
         get { return m_draggable; }
         set { m_draggable = value; }
      }
      
      [DefaultValue(false)]
      [Category("Layered")]
      [Description("Gets or sets whether the form can be resized without a border when in layered mode.")]
      public bool Resizable {
         get { return m_resizable; }
         set { m_resizable = value; }
      }

      [DefaultValue(3)]
      [Category("Layered")]
      [Description("Gets or sets how thick the resize \"grip\" is.")]
      public int ResizeThreshold {
         get { return m_resizeThreshold; }
         set { m_resizeThreshold = value; }
      }

      /// <summary>Force the window to the active state regardless if another process owns the foreground.</summary>
      public void ForceToFront() {
         //switchmon: go go reusable code.
         uint a;
         IntPtr hWndForeground = GetForegroundWindow();
         if(hWndForeground != this.Handle) {
            uint thread1 = GetWindowThreadProcessId(hWndForeground, out a);
            uint thread2 = GetCurrentThreadId();

            if(thread1 != thread2) {
               AttachThreadInput(thread1, thread2, true);
               BringWindowToTop(this.Handle);
               if(IsIconic(this.Handle)) {
                  ShowWindow(this.Handle, ShowWindowFlags.SW_SHOWNORMAL);
               } else {
                  ShowWindow(this.Handle, ShowWindowFlags.SW_SHOW);
               }
               AttachThreadInput(thread1, thread2, false);
            } else {
               SetForegroundWindow(this.Handle);
            }
            if(IsIconic(this.Handle)) {
               ShowWindow(this.Handle, ShowWindowFlags.SW_SHOWNORMAL);
            } else {
               ShowWindow(this.Handle, ShowWindowFlags.SW_SHOW);
            }
         }
      }

      //Support setting the layered flag during initialization if the designer wishes the layered capabilities during load
      protected override CreateParams CreateParams {
         get {
            CreateParams cp = base.CreateParams;
            if (m_layered && !DesignMode)
               cp.ExStyle |= WS_EX_LAYERED;
            return cp;
         }
      }

      protected override void WndProc(ref Message m) {
         if (m.Msg == WM_NCHITTEST) {
            //decode the mouse coordinates
            int lp = (int)m.LParam;
            int x = lp & 0xffff;
            int y = (lp >> 16) & 0xffff;

            //inform the o/s where the hit test landed
            if (m_layered && m_resizable && x >= Bounds.Left && x <= Bounds.Left + m_resizeThreshold) {
               m.Result = (IntPtr)HTLEFT;
            } else if (m_layered && m_resizable && x >= Bounds.Right - m_resizeThreshold && x <= Bounds.Right) {
               m.Result = (IntPtr)HTRIGHT;
            } else if (m_layered && m_resizable && y >= Bounds.Top && y <= Bounds.Top + m_resizeThreshold) {
               m.Result = (IntPtr)HTTOP;
            } else if (m_layered && m_resizable && y >= Bounds.Bottom - m_resizeThreshold && y <= Bounds.Bottom) {
               m.Result = (IntPtr)HTBOTTOM;
            } else {
               base.WndProc(ref m);
            }
         } else if (m.Msg == WM_LBUTTONDOWN) {
           var isCtrlDown = (Form.ModifierKeys == Keys.Control);
           if ((m_draggable && !isCtrlDown) || (!m_draggable && isCtrlDown)) {
             //Simulate title bar dragging for drag mode and only if the left mouse is down (to allow context menu to still fire)
             SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
           } else {
             base.WndProc(ref m);
           }
         } else {
           base.WndProc(ref m);
         }
      }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayeredForm));
            this.SuspendLayout();
            // 
            // LayeredForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LayeredForm";
            this.ResumeLayout(false);

        }
    }
}
