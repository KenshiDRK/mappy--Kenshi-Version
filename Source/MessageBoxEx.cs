using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace mappy {
   public class MessageBoxEx : Form {
      string m_text;
      Icon m_icon;
      Brush bText;
      Rectangle rText;
      Rectangle rIcon;
      Rectangle rButtons;
      int iInnerPadding = 7;
      int m_selectedIndex = -1;
      bool m_dns = false;
      uint m_sound;

      private static string m_dns_default_text = "Do not show again";
      public static string DoNotShowText {
         get { return m_dns_default_text; }
         set { m_dns_default_text = value; }
      }

      public static int Show(string text, string caption, string[] buttons, MessageBoxIcon icon) {
         return Show(null, text, caption, buttons, icon);
      }
      public static int Show(string text, string caption, string[] buttons, MessageBoxIcon icon, out bool DoNotShow) {
         return Show(null, text, caption, buttons, icon, out DoNotShow, m_dns_default_text);
      }
      public static int Show(string text, string caption, string[] buttons, MessageBoxIcon icon, out bool DoNotShow, string DoNotShowText) {
         return Show(null, text, caption, buttons, icon, out DoNotShow, DoNotShowText);
      }
      public static int Show(IWin32Window Owner, string text, string caption, string[] buttons, MessageBoxIcon icon) {
         MessageBoxEx form = new MessageBoxEx(text, caption, buttons, icon, false, "");
         form.ShowDialog(Owner);
         return form.SelectedButton;
      }
      public static int Show(IWin32Window Owner, string text, string caption, string[] buttons, MessageBoxIcon icon, out bool DoNotShow) {
         return Show(Owner, text, caption, buttons, icon, out DoNotShow, m_dns_default_text);
      }
      public static int Show(IWin32Window Owner, string text, string caption, string[] buttons, MessageBoxIcon icon, out bool DoNotShow, string DoNotShowText) {
         MessageBoxEx form = new MessageBoxEx(text, caption, buttons, icon, true, DoNotShowText);
         form.ShowDialog(Owner);
         DoNotShow = form.DoNotShow;
         return form.SelectedButton;
      }

      private uint beep_ok       = 0x00;
      private uint beep_error    = 0x10;
      private uint beep_question = 0x20;
      private uint beep_warning  = 0x30;
      private uint beep_info     = 0x40;

      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      static extern bool MessageBeep(uint type);

      private MessageBoxEx(string text, string caption, string[] buttons, MessageBoxIcon icon, bool useDNS, string DNSDefaultText) {
         this.Font = SystemFonts.MessageBoxFont;
         this.StartPosition = FormStartPosition.CenterScreen;
         this.Text = caption;
         this.FormBorderStyle = FormBorderStyle.FixedSingle;
         this.MinimizeBox = false;
         this.MaximizeBox = false;
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.Padding = new Padding(10);

         if(buttons == null || buttons.Length == 0)
            buttons = new string[] { "Ok" };

         m_text = text;
         bText = new SolidBrush(ForeColor);
         switch(icon) {
            case MessageBoxIcon.Error:
               m_icon = SystemIcons.Error;
               m_sound = beep_error;
               break;
            case MessageBoxIcon.Question:
               m_icon = SystemIcons.Question;
               m_sound = beep_question;
               break;
            case MessageBoxIcon.Exclamation:
               m_icon = SystemIcons.Exclamation;
               m_sound = beep_warning;
               break;
            case MessageBoxIcon.Information:
               m_icon = SystemIcons.Information;
               m_sound = beep_info;
               break;
            default:
               m_icon = null;
               m_sound = beep_ok;
               break;
         }

         //calcuate the size of the message (icon/text)
         if(m_icon != null) {
            rIcon = new Rectangle(Padding.Left, Padding.Top, m_icon.Width, m_icon.Height);
         } else {
            rIcon = new Rectangle(0, 0, 0, 0);
         }
         rText = new Rectangle(new Point(rIcon.Right + iInnerPadding, rIcon.Top), MeasureString(text));
         Rectangle rMessagePart = Rectangle.Union(rIcon, rText);

         //calcuate the size of the generated buttons
         List<Button> buttonlist = new List<Button>();
         int iButtonWidth = -iInnerPadding;
         for(int idx = 0; idx < buttons.Length; idx++) {
            Button cb = new Button();
            cb.Text = buttons[idx];
            cb.Tag = idx;
            cb.AutoSize = true;
            cb.AutoSizeMode = AutoSizeMode.GrowOnly;
            cb.MinimumSize = new Size(70, 0);
            cb.Click += new EventHandler(cb_Click);
            iButtonWidth += cb.Width + iInnerPadding;
            buttonlist.Add(cb);
         }
         int iButtonHeight = buttonlist.Count == 0 ? 0 : buttonlist[0].Height;
         rButtons = new Rectangle(0, rMessagePart.Bottom + iInnerPadding, iButtonWidth, iButtonHeight);

         //if the "Do Not Show" checkbox is to be displayed then factor it into the dialog size
         if(useDNS) {
            CheckBox chkDNS = new CheckBox();
            chkDNS.Text = DNSDefaultText;
            chkDNS.TextAlign = ContentAlignment.MiddleLeft;
            chkDNS.AutoSize = true;
            Controls.Add(chkDNS);
            Rectangle rDNS = new Rectangle(rMessagePart.Left, rButtons.Bottom + iInnerPadding, chkDNS.Width, chkDNS.Height);
            chkDNS.Location = rDNS.Location;
            chkDNS.CheckedChanged += new EventHandler(chkDNS_CheckedChanged);
            rButtons = Rectangle.Union(rButtons, rDNS);
         }

         //calcuate the size of the entire dialog
         Rectangle rDialog = Rectangle.Union(rMessagePart, rButtons);
         rDialog.Width += Padding.Horizontal;
         rDialog.Height += Padding.Vertical;

         //if the buttons are larger than the message then center icon/text
         if(rMessagePart.Width < rDialog.Width) {
            int wOffset = rDialog.Width / 2 - rMessagePart.Width / 2;
            rIcon.X += wOffset;
            rText.X += wOffset;
         }

         //add the buttons to the dialog and center them
         int bx = (rDialog.Width / 2 - rButtons.Width / 2);
         for(int idx = 0; idx < buttonlist.Count; idx++) {
            Button button = buttonlist[idx];
            button.Location = new Point(bx, rButtons.Top);
            Controls.Add(button);
            bx += button.Width + iInnerPadding;
         }

         this.ClientSize = rDialog.Size;
         this.Paint += new PaintEventHandler(MessageBoxExForm_Paint);
      }

      protected override void OnLoad(EventArgs e) {
         MessageBeep(m_sound);
         base.OnLoad(e);
      }

      private void chkDNS_CheckedChanged(object sender, EventArgs e) {
         DoNotShow = ((CheckBox)sender).Checked;
      }

      private void cb_Click(object sender, EventArgs e) {
         Button button = (Button)sender;
         int idx = (int)button.Tag;
         SelectedButton = idx;
         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      public int SelectedButton {
         get { return m_selectedIndex; }
         private set { m_selectedIndex = value; }
      }

      public bool DoNotShow {
         get { return m_dns; }
         private set { m_dns = value; }
      }

      private Size MeasureString(string value) {
         Graphics g = this.CreateGraphics();
         Size result = g.MeasureString(value, SystemFonts.MessageBoxFont).ToSize();
         g.Dispose();
         return result;
      }

      private void MessageBoxExForm_Paint(object sender, PaintEventArgs e) {
         e.Graphics.DrawIcon(m_icon, rIcon);
         e.Graphics.DrawString(m_text, SystemFonts.MessageBoxFont, bText, rText.Location);
      }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxEx));
            this.SuspendLayout();
            // 
            // MessageBoxEx
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageBoxEx";
            this.ResumeLayout(false);

        }
    }
}
