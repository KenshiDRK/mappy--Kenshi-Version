namespace MapEngine {
   partial class MapControl {
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapControl));
         this.MapEngine = new Engine(this.components);
         this.SuspendLayout();
         // 
         // MapEngine
         // 
         this.MapEngine.ClientRectangle = new System.Drawing.Rectangle(0, 0, 100, 100);
         this.MapEngine.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(200)))), ((int)(((byte)(75)))));
         this.MapEngine.GridTickColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(200)))), ((int)(((byte)(75)))));
         this.MapEngine.GridTickFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.MapEngine.SpawnSelectSize = 4F;
         this.MapEngine.ViewLocation = ((System.Drawing.PointF)(resources.GetObject("MapEngine.ViewLocation")));
         this.ResumeLayout(false);

      }

      #endregion

      private Engine MapEngine;
   }
}
