using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mappy {
   public partial class fEditHunts : Form {
      private fMap m_parent;
      private fEditHunts(fMap parent) {
         m_parent = parent;
         InitializeComponent();
         PopulateList();

         lvHunts.ItemActivate += new EventHandler(lvHunts_ItemActivate);
         m_parent.Engine.Data.Hunts.DataChanged += new MapEngine.GenericEvent(Hunts_DataChanged);
      }

      public static void BeginEdit(fMap Owner) {
         fEditHunts editor = new fEditHunts(Owner);
         editor.Show(Owner);
      }

      private void PopulateList() {
         lvHunts.Items.Clear();
         foreach (KeyValuePair<string, MapEngine.MapHunt> pair in m_parent.Engine.Data.Hunts) {
            ListViewItem item = new ListViewItem(pair.Value.Hunt.ToString());
            item.SubItems.Add(pair.Value.Permanent.ToString());
            item.Tag = pair.Value;

            lvHunts.Items.Add(item);
         }
      }

      private void lvHunts_ItemActivate(object sender, EventArgs e) {
         if (lvHunts.SelectedItems.Count > 0) {
            ListViewItem item = lvHunts.SelectedItems[0];

            MapEngine.MapHunt hunt = (MapEngine.MapHunt)item.Tag;
            txtHuntEntry.Text = hunt.Hunt.ToString();
            chkPermanent.Checked = hunt.Permanent;

            m_parent.Engine.Data.Hunts.Remove(hunt);
            lvHunts.Items.Remove(item);
         }
      }

      private void cmdAddHunt_Click(object sender, EventArgs e) {
         MapEngine.MapHunt hunt = m_parent.Engine.Data.Hunts.Add(txtHuntEntry.Text, chkPermanent.Checked);
         if (hunt != null) {
            txtHuntEntry.Text = "";
            chkPermanent.Checked = false;
         }
      }

      private void Hunts_DataChanged() {
         PopulateList();
      }
   }
}
