using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mappy {
   public partial class fEditLabel : Form {
      fMap parent;
      private fEditLabel(fMap parent) {
         this.parent = parent;
         InitializeComponent();
      }

      public static void BeginAdd(fMap Owner) {
         fEditLabel editor = new fEditLabel(Owner);
         if (editor.ShowDialog(Owner) == DialogResult.OK) {
            MapEngine.MapLabel label = new MapEngine.MapLabel(editor.txtCaption.Text, editor.cmdLabelColor.BackColor);
            if (Owner.Engine.Game.Player != null) {
               label.setLocation(
                  Owner.Engine.Game.Player.Location.X,
                  Owner.Engine.Game.Player.Location.Y,
                  Owner.Engine.Game.Player.Location.Z
               );
            }
            Owner.Engine.Data.AddLabel(label);
            Owner.Engine.UpdateMap();
         }
      }

      public static void BeginEdit(fMap Owner, MapEngine.MapLabel label) {
         fEditLabel editor = new fEditLabel(Owner);
         editor.txtCaption.Text = label.Caption;
         editor.cmdLabelColor.BackColor = label.Color;

         if (editor.ShowDialog(Owner) == DialogResult.OK) {
            label.Caption = editor.txtCaption.Text;
            label.Color = editor.cmdLabelColor.BackColor;
            Owner.Engine.UpdateMap();
         }
      }

      private void cmdLabelColor_Click(object sender, EventArgs e) {
         dColorPicker.Color = cmdLabelColor.BackColor;
         if (dColorPicker.ShowDialog() == DialogResult.OK) {
            cmdLabelColor.BackColor = dColorPicker.Color;
         }
      }
   }
}
