namespace mappy {
   partial class fEditHunts {
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
         this.txtHuntEntry = new System.Windows.Forms.TextBox();
         this.cmdAddHunt = new System.Windows.Forms.Button();
         this.lvHunts = new System.Windows.Forms.ListView();
         this.colPattern = new System.Windows.Forms.ColumnHeader();
         this.colPemanent = new System.Windows.Forms.ColumnHeader();
         this.chkPermanent = new System.Windows.Forms.CheckBox();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // txtHuntEntry
         // 
         this.txtHuntEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.txtHuntEntry.Location = new System.Drawing.Point(6, 20);
         this.txtHuntEntry.Name = "txtHuntEntry";
         this.txtHuntEntry.Size = new System.Drawing.Size(242, 20);
         this.txtHuntEntry.TabIndex = 0;
         // 
         // cmdAddHunt
         // 
         this.cmdAddHunt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.cmdAddHunt.Location = new System.Drawing.Point(288, 18);
         this.cmdAddHunt.Name = "cmdAddHunt";
         this.cmdAddHunt.Size = new System.Drawing.Size(46, 23);
         this.cmdAddHunt.TabIndex = 1;
         this.cmdAddHunt.Text = "Add";
         this.cmdAddHunt.UseVisualStyleBackColor = true;
         this.cmdAddHunt.Click += new System.EventHandler(this.cmdAddHunt_Click);
         // 
         // lvHunts
         // 
         this.lvHunts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lvHunts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPattern,
            this.colPemanent});
         this.lvHunts.FullRowSelect = true;
         this.lvHunts.Location = new System.Drawing.Point(6, 47);
         this.lvHunts.Name = "lvHunts";
         this.lvHunts.Size = new System.Drawing.Size(328, 150);
         this.lvHunts.TabIndex = 2;
         this.lvHunts.UseCompatibleStateImageBehavior = false;
         this.lvHunts.View = System.Windows.Forms.View.Details;
         // 
         // colPattern
         // 
         this.colPattern.Text = "Pattern";
         this.colPattern.Width = 213;
         // 
         // colPemanent
         // 
         this.colPemanent.Text = "Permanent";
         this.colPemanent.Width = 68;
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
         // fEditHunts
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(338, 217);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.chkPermanent);
         this.Controls.Add(this.lvHunts);
         this.Controls.Add(this.cmdAddHunt);
         this.Controls.Add(this.txtHuntEntry);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
         this.MinimumSize = new System.Drawing.Size(276, 215);
         this.Name = "fEditHunts";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Hunts";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox txtHuntEntry;
      private System.Windows.Forms.Button cmdAddHunt;
      private System.Windows.Forms.ListView lvHunts;
      private System.Windows.Forms.ColumnHeader colPattern;
      private System.Windows.Forms.ColumnHeader colPemanent;
      private System.Windows.Forms.CheckBox chkPermanent;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
   }
}