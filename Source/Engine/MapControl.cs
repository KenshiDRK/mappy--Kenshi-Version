using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MapEngine {
   /// <summary>
   /// A very simple control that implements the most basic interface to the map engine.
   /// </summary>
   public partial class MapControl : Control {
      private bool       panMode          = false;
      private Point      panMouseOrigin   = new Point(0, 0);
      private PointF     panViewOrigin    = new PointF(0, 0);

      public MapControl() {
         this.BackColor = Color.Black;
         this.ForeColor = Color.White;
         this.SetStyle(
            ControlStyles.SupportsTransparentBackColor |
            ControlStyles.UserPaint |
            ControlStyles.ResizeRedraw |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint, true);
         this.DoubleBuffered = true;
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs pe) {
         pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
         MapEngine.Render(pe.Graphics);
      }
      protected override void OnResize(EventArgs e) {
         base.OnResize(e);
         MapEngine.ClientRectangle = ClientRectangle;
      }
      protected override void OnMouseWheel(MouseEventArgs e) {
         base.OnMouseWheel(e);
         MapEngine.Zoom += (e.Delta / SystemInformation.MouseWheelScrollDelta);
      }

      protected override void OnMouseDown(MouseEventArgs e) {
         base.OnMouseDown(e);
         panViewOrigin = MapEngine.ViewLocation;
         panMouseOrigin = e.Location;
         panMode = true;
      }

      protected override void OnMouseMove(MouseEventArgs e) {
         base.OnMouseMove(e);
         if (panMode) {
            MapEngine.ViewLocation = new PointF(
               (float)(panViewOrigin.X - (float)(panMouseOrigin.X - e.X)),
               (float)(panViewOrigin.Y - (float)(panMouseOrigin.Y - e.Y))
            );
         } else {
            //highlight the hovered spawn
            GameSpawn spawn = MapEngine.FindSpawn(e.X, e.Y);
            if (spawn != MapEngine.Game.Highlighted) {
               MapEngine.Game.Highlighted = spawn;
               this.Invalidate();
            }
         }
      }

      protected override void OnMouseUp(MouseEventArgs e) {
         base.OnMouseUp(e);
         if (e.X == panMouseOrigin.X && e.Y == panMouseOrigin.Y) {
            //selected clicked spawn, or deselect if nothing clicked
            GameSpawn spawn = MapEngine.FindSpawn(e.X, e.Y);
            if (spawn != MapEngine.Game.Selected) {
               MapEngine.Game.Selected = spawn;
               this.Invalidate();
            }
         }
         panMode = false;
      }
   }
}
