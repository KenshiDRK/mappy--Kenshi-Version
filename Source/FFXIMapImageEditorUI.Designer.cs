namespace mappy
{
   partial class FFXIMapImageEditorUI
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.tbTools = new System.Windows.Forms.ToolStrip();
         this.miMapMode = new System.Windows.Forms.ToolStripButton();
         this.miAreaMode = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.miSave = new System.Windows.Forms.ToolStripButton();
         this.lstMapTopMost = new System.Windows.Forms.ToolStripComboBox();
         this.tbMapMode = new System.Windows.Forms.ToolStrip();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.miNewMap = new System.Windows.Forms.ToolStripButton();
         this.miRemoveMap = new System.Windows.Forms.ToolStripButton();
         this.label1 = new System.Windows.Forms.Label();
         this.udCreateMapID = new System.Windows.Forms.NumericUpDown();
         this.lstCreateMapScale = new System.Windows.Forms.ComboBox();
         this.label2 = new System.Windows.Forms.Label();
         this.cmdCreateMap = new System.Windows.Forms.Button();
         this.cmdCancelCreateMap = new System.Windows.Forms.Button();
         this.pCreateMap = new System.Windows.Forms.FlowLayoutPanel();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.tbTools.SuspendLayout();
         this.tbMapMode.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.udCreateMapID)).BeginInit();
         this.pCreateMap.SuspendLayout();
         this.SuspendLayout();
         // 
         // tbTools
         // 
         this.tbTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMapMode,
            this.miAreaMode,
            this.toolStripSeparator1,
            this.miSave,
            this.toolStripSeparator3,
            this.lstMapTopMost});
         this.tbTools.Location = new System.Drawing.Point(0, 0);
         this.tbTools.Name = "tbTools";
         this.tbTools.Size = new System.Drawing.Size(410, 25);
         this.tbTools.TabIndex = 0;
         this.tbTools.Text = "toolStrip1";
         // 
         // miMapMode
         // 
         this.miMapMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.miMapMode.Image = global::mappy.Properties.Resources.icon_image;
         this.miMapMode.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.miMapMode.Name = "miMapMode";
         this.miMapMode.Size = new System.Drawing.Size(23, 22);
         this.miMapMode.Text = "Edit Map Image";
         this.miMapMode.Click += new System.EventHandler(this.miMapMode_Click);
         // 
         // miAreaMode
         // 
         this.miAreaMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.miAreaMode.Image = global::mappy.Properties.Resources.icon_group;
         this.miAreaMode.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.miAreaMode.Name = "miAreaMode";
         this.miAreaMode.Size = new System.Drawing.Size(23, 22);
         this.miAreaMode.Text = "Edit Map Areas";
         this.miAreaMode.Click += new System.EventHandler(this.miAreaMode_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // miSave
         // 
         this.miSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.miSave.Image = global::mappy.Properties.Resources.icon_save;
         this.miSave.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.miSave.Name = "miSave";
         this.miSave.Size = new System.Drawing.Size(23, 22);
         this.miSave.Text = "Save Changes";
         this.miSave.Click += new System.EventHandler(this.miSave_Click);
         // 
         // lstMapTopMost
         // 
         this.lstMapTopMost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.lstMapTopMost.DropDownWidth = 75;
         this.lstMapTopMost.Name = "lstMapTopMost";
         this.lstMapTopMost.Size = new System.Drawing.Size(75, 25);
         this.lstMapTopMost.SelectedIndexChanged += new System.EventHandler(this.lstMapTopMost_SelectedIndexChanged);
         // 
         // tbMapMode
         // 
         this.tbMapMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.miNewMap,
            this.miRemoveMap});
         this.tbMapMode.Location = new System.Drawing.Point(0, 25);
         this.tbMapMode.Name = "tbMapMode";
         this.tbMapMode.Size = new System.Drawing.Size(410, 25);
         this.tbMapMode.TabIndex = 1;
         this.tbMapMode.Text = "toolStrip1";
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
         // 
         // miNewMap
         // 
         this.miNewMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.miNewMap.Image = global::mappy.Properties.Resources.icon_newitem;
         this.miNewMap.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.miNewMap.Name = "miNewMap";
         this.miNewMap.Size = new System.Drawing.Size(23, 22);
         this.miNewMap.Text = "New Map";
         this.miNewMap.Click += new System.EventHandler(this.miNewMap_Click);
         // 
         // miRemoveMap
         // 
         this.miRemoveMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.miRemoveMap.Image = global::mappy.Properties.Resources.icon_delete;
         this.miRemoveMap.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.miRemoveMap.Name = "miRemoveMap";
         this.miRemoveMap.Size = new System.Drawing.Size(23, 22);
         this.miRemoveMap.Text = "Remove Map";
         this.miRemoveMap.Click += new System.EventHandler(this.miRemoveMap_Click);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(3, 2);
         this.label1.Margin = new System.Windows.Forms.Padding(3, 2, 0, 2);
         this.label1.Name = "label1";
         this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
         this.label1.Size = new System.Drawing.Size(21, 19);
         this.label1.TabIndex = 0;
         this.label1.Text = "ID:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // udCreateMapID
         // 
         this.udCreateMapID.Location = new System.Drawing.Point(27, 3);
         this.udCreateMapID.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
         this.udCreateMapID.Name = "udCreateMapID";
         this.udCreateMapID.Size = new System.Drawing.Size(43, 20);
         this.udCreateMapID.TabIndex = 1;
         // 
         // lstCreateMapScale
         // 
         this.lstCreateMapScale.FormattingEnabled = true;
         this.lstCreateMapScale.Items.AddRange(new object[] {
            "0.1",
            "0.2",
            "0.4"});
         this.lstCreateMapScale.Location = new System.Drawing.Point(116, 3);
         this.lstCreateMapScale.Name = "lstCreateMapScale";
         this.lstCreateMapScale.Size = new System.Drawing.Size(77, 21);
         this.lstCreateMapScale.TabIndex = 2;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(76, 2);
         this.label2.Margin = new System.Windows.Forms.Padding(3, 2, 0, 2);
         this.label2.Name = "label2";
         this.label2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
         this.label2.Size = new System.Drawing.Size(37, 19);
         this.label2.TabIndex = 3;
         this.label2.Text = "Scale:";
         // 
         // cmdCreateMap
         // 
         this.cmdCreateMap.AutoSize = true;
         this.cmdCreateMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.cmdCreateMap.Location = new System.Drawing.Point(199, 3);
         this.cmdCreateMap.Name = "cmdCreateMap";
         this.cmdCreateMap.Size = new System.Drawing.Size(48, 23);
         this.cmdCreateMap.TabIndex = 4;
         this.cmdCreateMap.Text = "Create";
         this.cmdCreateMap.UseVisualStyleBackColor = true;
         this.cmdCreateMap.Click += new System.EventHandler(this.cmdCreateMap_Click);
         // 
         // cmdCancelCreateMap
         // 
         this.cmdCancelCreateMap.AutoSize = true;
         this.cmdCancelCreateMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.cmdCancelCreateMap.Location = new System.Drawing.Point(253, 3);
         this.cmdCancelCreateMap.Name = "cmdCancelCreateMap";
         this.cmdCancelCreateMap.Size = new System.Drawing.Size(50, 23);
         this.cmdCancelCreateMap.TabIndex = 5;
         this.cmdCancelCreateMap.Text = "Cancel";
         this.cmdCancelCreateMap.UseVisualStyleBackColor = true;
         this.cmdCancelCreateMap.Click += new System.EventHandler(this.cmdCancelCreateMap_Click);
         // 
         // pCreateMap
         // 
         this.pCreateMap.AutoSize = true;
         this.pCreateMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.pCreateMap.Controls.Add(this.label1);
         this.pCreateMap.Controls.Add(this.udCreateMapID);
         this.pCreateMap.Controls.Add(this.label2);
         this.pCreateMap.Controls.Add(this.lstCreateMapScale);
         this.pCreateMap.Controls.Add(this.cmdCreateMap);
         this.pCreateMap.Controls.Add(this.cmdCancelCreateMap);
         this.pCreateMap.Dock = System.Windows.Forms.DockStyle.Top;
         this.pCreateMap.Location = new System.Drawing.Point(0, 50);
         this.pCreateMap.Name = "pCreateMap";
         this.pCreateMap.Size = new System.Drawing.Size(410, 29);
         this.pCreateMap.TabIndex = 3;
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
         // 
         // FFXIMapImageEditorUI
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(410, 304);
         this.Controls.Add(this.pCreateMap);
         this.Controls.Add(this.tbMapMode);
         this.Controls.Add(this.tbTools);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Name = "FFXIMapImageEditorUI";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
         this.Load += new System.EventHandler(this.MapImageEditorUI_Load);
         this.tbTools.ResumeLayout(false);
         this.tbTools.PerformLayout();
         this.tbMapMode.ResumeLayout(false);
         this.tbMapMode.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.udCreateMapID)).EndInit();
         this.pCreateMap.ResumeLayout(false);
         this.pCreateMap.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ToolStrip tbTools;
      private System.Windows.Forms.ToolStripButton miMapMode;
      private System.Windows.Forms.ToolStripButton miAreaMode;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripButton miSave;
      private System.Windows.Forms.ToolStrip tbMapMode;
      private System.Windows.Forms.ToolStripComboBox lstMapTopMost;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripButton miNewMap;
      private System.Windows.Forms.Button cmdCancelCreateMap;
      private System.Windows.Forms.Button cmdCreateMap;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ComboBox lstCreateMapScale;
      private System.Windows.Forms.NumericUpDown udCreateMapID;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.FlowLayoutPanel pCreateMap;
      private System.Windows.Forms.ToolStripButton miRemoveMap;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

   }
}