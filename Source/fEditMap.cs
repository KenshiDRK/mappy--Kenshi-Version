using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mappy {
   public partial class fEditMap : Form {
      fMap parent;

      public static void BeginEditor(fMap Owner) {
         fEditMap editor = new fEditMap(Owner);
         editor.Show(Owner);
      }

      private fEditMap(fMap Parent) {
         parent = Parent;
         InitializeComponent();
      }

      private void cmdStartLine_Click(object sender, EventArgs e) {
         parent.Engine.Data.EditStartLine(cmdChangeLineColor.BackColor);
      }

      private void cmdCancelLine_Click(object sender, EventArgs e) {
         if (MessageBox.Show(Program.GetLang("msg_cancel_line_text"), Program.GetLang("msg_cancel_line_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            parent.Engine.Data.EditCancelLine();
      }

      private void cmdAddPoint_Click(object sender, EventArgs e) {
         parent.Engine.Data.EditAddPoint();
      }

      private void cmdRemoveLastPoint_Click(object sender, EventArgs e) {
         parent.Engine.Data.EditRemovePoint();
      }

      private void cmdChangeLineColor_Click(object sender, EventArgs e) {
         cColorDialog.Color = cmdChangeLineColor.BackColor;
         if(cColorDialog.ShowDialog(this) == DialogResult.OK) {
            cmdChangeLineColor.BackColor = cColorDialog.Color;
            parent.Engine.Data.EditChangeLineColor(cColorDialog.Color);
         }
      }

      private void cmdSaveMap_Click(object sender, EventArgs e) {
         parent.Engine.Data.Save();
      }
   }
}