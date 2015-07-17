using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mappy {
   public partial class FFXIMapImageEditorUI : Form {
      FFXIMapImageEditor m_editor;
      ToolStrip lastbar;

      public FFXIMapImageEditorUI(FFXIMapImageEditor editor) {
         m_editor = editor;
         InitializeComponent();
         tbMapMode.Visible = false;
         pCreateMap.Visible = false;
         updateMode();
         updateMapInfo();
      }
      private void MapImageEditorUI_Load(object sender, EventArgs e) {
         doLayout();
      }

      private void updateMode() {
         miMapMode.Checked = m_editor.Mode == 0;
         miAreaMode.Checked = m_editor.Mode == 1;
         if (lastbar != null)
            ToolStripManager.RevertMerge(tbTools, lastbar);
         lastbar = null;

         switch (m_editor.Mode) {
            case 0:
               ToolStripManager.Merge(tbMapMode, tbTools);
               lastbar = tbMapMode;
               break;
            default:
               pCreateMap.Visible = false;
               doLayout();
               break;
         }
      }

      public void updateMapInfo() {
         lstMapTopMost.Items.Clear();
         foreach (KeyValuePair<int,FFXIImageMap> pair in m_editor.Container.CurrentZone)
            lstMapTopMost.Items.Add(pair.Value);
         updateTopMostMap();
      }

      public void updateTopMostMap() {
         if (m_editor.TopMostMap != null)
            lstMapTopMost.SelectedItem = m_editor.TopMostMap;
      }

      private void miSave_Click(object sender, EventArgs e) {
         bool alwaysoverwrite = m_editor.Window.Config.Get("DNS_IniOverwrite", false);
         if (alwaysoverwrite || MessageBoxEx.Show(Program.GetLang("msg_iniclobber_text"), Program.GetLang("msg_iniclobber_title"), new string[] { Program.GetLang("button_yes"), Program.GetLang("button_no") }, MessageBoxIcon.Question, out alwaysoverwrite, Program.GetLang("msg_alwaysoverwrite")) == 0) {
            m_editor.Window.Config.Set("DNS_IniOverwrite", alwaysoverwrite);
            m_editor.Container.Save();
         }
      }

      private void miMapMode_Click(object sender, EventArgs e) {
         m_editor.Mode = 0;
         updateMode();
      }

      private void miAreaMode_Click(object sender, EventArgs e) {
         m_editor.Mode = 1;
         updateMode();
      }

      private void lstMapTopMost_SelectedIndexChanged(object sender, EventArgs e) {
         if (lstMapTopMost.SelectedItem != null)
            m_editor.TopMostMap = (FFXIImageMap)lstMapTopMost.SelectedItem;
      }

      private void doLayout() {
         int height = 0;
         foreach (Control c in this.Controls) {
            if (c.Visible)
               height += c.Height;
         }
         this.Height = height;
      }

      private void miNewMap_Click(object sender, EventArgs e) {
         pCreateMap.Visible = true;
         udCreateMapID.Value = (decimal)m_editor.Container.CurrentZone.GetFreeID();
         doLayout();
      }

      private void cmdCreateMap_Click(object sender, EventArgs e) {
         float scale;
         int id = (int)udCreateMapID.Value;

         if (m_editor.Container.CurrentZone.ContainsKey(id)) {
            MessageBox.Show(this, "The map id " + id + " already exists for this zone.", "Unable to create");
            return;
         }
         if (!float.TryParse(lstCreateMapScale.Text, out scale)) {
            MessageBox.Show(this, "You must enter a valid numerical value for the scale.", "Unable to create");
            return;
         }

         m_editor.CreateMap(id, scale);
         udCreateMapID.Value = (decimal)m_editor.Container.CurrentZone.GetFreeID();
      }

      private void cmdCancelCreateMap_Click(object sender, EventArgs e) {
         pCreateMap.Visible = false;
         doLayout();
      }

      private void miRemoveMap_Click(object sender, EventArgs e) {
         if (lstMapTopMost.SelectedItem != null) {
            if (MessageBox.Show(this, "This will remove the image association and all defined range data for this map.\n\nDo you really wish to do this?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
               FFXIImageMap map = (FFXIImageMap)lstMapTopMost.SelectedItem;
               m_editor.RemoveMap(map);
               udCreateMapID.Value = (decimal)m_editor.Container.CurrentZone.GetFreeID();
            }
         }
      }
   }
}
