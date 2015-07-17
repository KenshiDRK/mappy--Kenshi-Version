namespace mappy {
    partial class fEditReplacements
    {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
            this.txtReplaceRegex = new System.Windows.Forms.TextBox();
            this.cmdAddReplacement = new System.Windows.Forms.Button();
            this.lvReplacements = new System.Windows.Forms.ListView();
            this.colPattern = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReplacement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPemanent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkPermanent = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReplaceEntry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtReplaceRegex
            // 
            this.txtReplaceRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplaceRegex.Location = new System.Drawing.Point(6, 20);
            this.txtReplaceRegex.Name = "txtReplaceRegex";
            this.txtReplaceRegex.Size = new System.Drawing.Size(118, 20);
            this.txtReplaceRegex.TabIndex = 0;
            // 
            // cmdAddReplacement
            // 
            this.cmdAddReplacement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddReplacement.Location = new System.Drawing.Point(288, 18);
            this.cmdAddReplacement.Name = "cmdAddReplacement";
            this.cmdAddReplacement.Size = new System.Drawing.Size(46, 23);
            this.cmdAddReplacement.TabIndex = 1;
            this.cmdAddReplacement.Text = "Add";
            this.cmdAddReplacement.UseVisualStyleBackColor = true;
            this.cmdAddReplacement.Click += new System.EventHandler(this.cmdAddReplacement_Click);
            // 
            // lvReplacements
            // 
            this.lvReplacements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvReplacements.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPattern,
            this.colReplacement,
            this.colPemanent});
            this.lvReplacements.FullRowSelect = true;
            this.lvReplacements.Location = new System.Drawing.Point(6, 47);
            this.lvReplacements.Name = "lvReplacements";
            this.lvReplacements.Size = new System.Drawing.Size(328, 150);
            this.lvReplacements.TabIndex = 2;
            this.lvReplacements.UseCompatibleStateImageBehavior = false;
            this.lvReplacements.View = System.Windows.Forms.View.Details;
            // 
            // colPattern
            // 
            this.colPattern.Text = "Pattern";
            this.colPattern.Width = 120;
            // 
            // colReplacement
            // 
            this.colReplacement.Text = "Replacement";
            this.colReplacement.Width = 120;
            // 
            // colPemanent
            // 
            this.colPemanent.Text = "Permanent";
            this.colPemanent.Width = 64;
            // 
            // chkPermanent
            // 
            this.chkPermanent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPermanent.AutoSize = true;
            this.chkPermanent.Location = new System.Drawing.Point(254, 23);
            this.chkPermanent.Name = "chkPermanent";
            this.chkPermanent.Size = new System.Drawing.Size(15, 14);
            this.chkPermanent.TabIndex = 3;
            this.chkPermanent.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "RegEx Pattern";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Perm";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Double click to remove/edit; entries are case-sensitive";
            // 
            // txtReplaceEntry
            // 
            this.txtReplaceEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplaceEntry.Location = new System.Drawing.Point(130, 20);
            this.txtReplaceEntry.Name = "txtReplaceEntry";
            this.txtReplaceEntry.Size = new System.Drawing.Size(118, 20);
            this.txtReplaceEntry.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Replacement";
            // 
            // fEditReplacements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 217);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReplaceEntry);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkPermanent);
            this.Controls.Add(this.lvReplacements);
            this.Controls.Add(this.cmdAddReplacement);
            this.Controls.Add(this.txtReplaceRegex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(276, 215);
            this.Name = "fEditReplacements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spawn Replacements";
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox txtReplaceRegex;
      private System.Windows.Forms.Button cmdAddReplacement;
      private System.Windows.Forms.ListView lvReplacements;
      private System.Windows.Forms.ColumnHeader colPattern;
      private System.Windows.Forms.ColumnHeader colPemanent;
      private System.Windows.Forms.CheckBox chkPermanent;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.ColumnHeader colReplacement;
      private System.Windows.Forms.TextBox txtReplaceEntry;
      private System.Windows.Forms.Label label4;
   }
}