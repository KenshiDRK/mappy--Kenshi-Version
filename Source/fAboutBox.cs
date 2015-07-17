using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace mappy {
   partial class fAboutBox : Form {
      public fAboutBox() {
         InitializeComponent();
         lblProductName.Text = Application.ProductName + " " + Application.ProductVersion;
         lblUrl.Text = Program.ResGlobal.GetString("config_app_url");
         cmdOk.Text = Program.GetLang("button_ok");
         this.Text = string.Format(Program.GetLang("dialog_about"), Application.ProductName);
         lblCredits.Rtf = (string)Program.ResGlobal.GetObject("credits");
      }

      private void lblUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
         Process.Start(Program.ResGlobal.GetString("config_app_url"));
      }

      private void cmdOk_Click(object sender, EventArgs e) {
         Close();
      }
   }
}
