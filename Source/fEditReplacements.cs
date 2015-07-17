using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mappy {
   public partial class fEditReplacements : Form {
      private fMap m_parent;
      private fEditReplacements(fMap parent)
      {
         m_parent = parent;
         InitializeComponent();
         PopulateList();

         lvReplacements.ItemActivate += new EventHandler(lvReplacements_ItemActivate);
         m_parent.Engine.Data.Replacements.DataChanged += new MapEngine.GenericEvent(Replacements_DataChanged);
      }

      public static void BeginEdit(fMap Owner) {
         fEditReplacements editor = new fEditReplacements(Owner);
         editor.Show(Owner);
      }

      private void PopulateList() {
         lvReplacements.Items.Clear();
         foreach (KeyValuePair<string, MapEngine.MapReplacement> pair in m_parent.Engine.Data.Replacements) {
            ListViewItem item = new ListViewItem(pair.Value.Replacement.ToString());
            item.SubItems.Add(pair.Value.ReplacedName.ToString());
            item.SubItems.Add(pair.Value.Permanent.ToString());
            item.Tag = pair.Value;

            lvReplacements.Items.Add(item);
         }
      }

      private void lvReplacements_ItemActivate(object sender, EventArgs e)
      {
         if (lvReplacements.SelectedItems.Count > 0) {
            ListViewItem item = lvReplacements.SelectedItems[0];

            MapEngine.MapReplacement rep = (MapEngine.MapReplacement)item.Tag;
            txtReplaceRegex.Text = rep.Replacement.ToString();
            txtReplaceEntry.Text = rep.ReplacedName.ToString();
            chkPermanent.Checked = rep.Permanent;

            m_parent.Engine.Data.Replacements.Remove(rep);
            lvReplacements.Items.Remove(item);
         }
      }

      private void cmdAddReplacement_Click(object sender, EventArgs e)
      {
         MapEngine.MapReplacement rep = m_parent.Engine.Data.Replacements.Add(txtReplaceRegex.Text, txtReplaceEntry.Text, chkPermanent.Checked);
         if (rep != null) {
            txtReplaceRegex.Text = "";
            txtReplaceEntry.Text = "";
            chkPermanent.Checked = false;
         }
      }

      private void Replacements_DataChanged() {
         PopulateList();
      }
   }
}
