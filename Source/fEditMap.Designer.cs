namespace mappy {
   partial class fEditMap {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if(disposing && (components != null)) {
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
         this.cmdStartLine = new System.Windows.Forms.Button();
         this.cmdCancelLine = new System.Windows.Forms.Button();
         this.cmdAddPoint = new System.Windows.Forms.Button();
         this.cmdRemoveLastPoint = new System.Windows.Forms.Button();
         this.cmdChangeLineColor = new System.Windows.Forms.Button();
         this.cColorDialog = new System.Windows.Forms.ColorDialog();
         this.cmdSaveMap = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // cmdStartLine
         // 
         this.cmdStartLine.Location = new System.Drawing.Point(4, 6);
         this.cmdStartLine.Name = "cmdStartLine";
         this.cmdStartLine.Size = new System.Drawing.Size(85, 23);
         this.cmdStartLine.TabIndex = 0;
         this.cmdStartLine.Text = "Start New Line";
         this.cmdStartLine.UseVisualStyleBackColor = true;
         this.cmdStartLine.Click += new System.EventHandler(this.cmdStartLine_Click);
         // 
         // cmdCancelLine
         // 
         this.cmdCancelLine.Location = new System.Drawing.Point(4, 35);
         this.cmdCancelLine.Name = "cmdCancelLine";
         this.cmdCancelLine.Size = new System.Drawing.Size(85, 23);
         this.cmdCancelLine.TabIndex = 1;
         this.cmdCancelLine.Text = "Cancel Line";
         this.cmdCancelLine.UseVisualStyleBackColor = true;
         this.cmdCancelLine.Click += new System.EventHandler(this.cmdCancelLine_Click);
         // 
         // cmdAddPoint
         // 
         this.cmdAddPoint.Location = new System.Drawing.Point(4, 64);
         this.cmdAddPoint.Name = "cmdAddPoint";
         this.cmdAddPoint.Size = new System.Drawing.Size(85, 23);
         this.cmdAddPoint.TabIndex = 2;
         this.cmdAddPoint.Text = "Add Point";
         this.cmdAddPoint.UseVisualStyleBackColor = true;
         this.cmdAddPoint.Click += new System.EventHandler(this.cmdAddPoint_Click);
         // 
         // cmdRemoveLastPoint
         // 
         this.cmdRemoveLastPoint.Location = new System.Drawing.Point(4, 93);
         this.cmdRemoveLastPoint.Name = "cmdRemoveLastPoint";
         this.cmdRemoveLastPoint.Size = new System.Drawing.Size(85, 23);
         this.cmdRemoveLastPoint.TabIndex = 3;
         this.cmdRemoveLastPoint.Text = "Remove Last";
         this.cmdRemoveLastPoint.UseVisualStyleBackColor = true;
         this.cmdRemoveLastPoint.Click += new System.EventHandler(this.cmdRemoveLastPoint_Click);
         // 
         // cmdChangeLineColor
         // 
         this.cmdChangeLineColor.BackColor = System.Drawing.Color.White;
         this.cmdChangeLineColor.Location = new System.Drawing.Point(4, 122);
         this.cmdChangeLineColor.Name = "cmdChangeLineColor";
         this.cmdChangeLineColor.Size = new System.Drawing.Size(85, 23);
         this.cmdChangeLineColor.TabIndex = 4;
         this.cmdChangeLineColor.UseVisualStyleBackColor = false;
         this.cmdChangeLineColor.Click += new System.EventHandler(this.cmdChangeLineColor_Click);
         // 
         // cmdSaveMap
         // 
         this.cmdSaveMap.Location = new System.Drawing.Point(4, 151);
         this.cmdSaveMap.Name = "cmdSaveMap";
         this.cmdSaveMap.Size = new System.Drawing.Size(85, 23);
         this.cmdSaveMap.TabIndex = 5;
         this.cmdSaveMap.Text = "Save";
         this.cmdSaveMap.UseVisualStyleBackColor = true;
         this.cmdSaveMap.Click += new System.EventHandler(this.cmdSaveMap_Click);
         // 
         // fEditMap
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(94, 180);
         this.Controls.Add(this.cmdSaveMap);
         this.Controls.Add(this.cmdChangeLineColor);
         this.Controls.Add(this.cmdRemoveLastPoint);
         this.Controls.Add(this.cmdAddPoint);
         this.Controls.Add(this.cmdCancelLine);
         this.Controls.Add(this.cmdStartLine);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Name = "fEditMap";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Edit Map";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button cmdStartLine;
      private System.Windows.Forms.Button cmdCancelLine;
      private System.Windows.Forms.Button cmdAddPoint;
      private System.Windows.Forms.Button cmdRemoveLastPoint;
      private System.Windows.Forms.Button cmdChangeLineColor;
      private System.Windows.Forms.ColorDialog cColorDialog;
      private System.Windows.Forms.Button cmdSaveMap;
   }
}