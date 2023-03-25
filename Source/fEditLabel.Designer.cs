namespace mappy {
   partial class fEditLabel {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fEditLabel));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.cmdLabelColor = new System.Windows.Forms.Button();
            this.cmdConfirm = new System.Windows.Forms.Button();
            this.dColorPicker = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Caption";
            // 
            // txtCaption
            // 
            this.txtCaption.Location = new System.Drawing.Point(8, 22);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(150, 20);
            this.txtCaption.TabIndex = 1;
            // 
            // cmdLabelColor
            // 
            this.cmdLabelColor.BackColor = System.Drawing.Color.Black;
            this.cmdLabelColor.Location = new System.Drawing.Point(164, 20);
            this.cmdLabelColor.Name = "cmdLabelColor";
            this.cmdLabelColor.Size = new System.Drawing.Size(53, 23);
            this.cmdLabelColor.TabIndex = 2;
            this.cmdLabelColor.UseVisualStyleBackColor = false;
            this.cmdLabelColor.Click += new System.EventHandler(this.cmdLabelColor_Click);
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdConfirm.Location = new System.Drawing.Point(223, 20);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Size = new System.Drawing.Size(48, 23);
            this.cmdConfirm.TabIndex = 3;
            this.cmdConfirm.Text = "Ok";
            this.cmdConfirm.UseVisualStyleBackColor = true;
            // 
            // fEditLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 55);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.cmdLabelColor);
            this.Controls.Add(this.txtCaption);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fEditLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Label";
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox txtCaption;
      private System.Windows.Forms.Button cmdLabelColor;
      private System.Windows.Forms.Button cmdConfirm;
      private System.Windows.Forms.ColorDialog dColorPicker;
   }
}