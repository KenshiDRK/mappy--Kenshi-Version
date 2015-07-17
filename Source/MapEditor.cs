using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using MapEngine;

namespace mappy {
   /// <summary>An editor that defines how the user interacts with the map interface.</summary>
   public interface IMapEditor {
      void OnEngage(fMap Window);
      void OnDisengage();
      void OnMouseDown(MouseEventArgs e);
      void OnMouseUp(MouseEventArgs e);
      void OnMouseMove(MouseEventArgs e);
      void OnMouseWheel(MouseEventArgs e);
      void OnMouseEnter(EventArgs e);
      void OnMouseLeave(EventArgs e);
      void OnMove(EventArgs e);
      void OnKeyDown(KeyEventArgs e);
      void OnKeyPress(KeyPressEventArgs e);
      void OnKeyUp(KeyEventArgs e);
      void OnResize(EventArgs e);
      void OnBeforePaint(PaintEventArgs e);
      void OnAfterPaint(PaintEventArgs e);
      bool Engaged { get; }
      string Name { get; }
   }

   public class MapEditors {
      private List<IMapEditor> editors;
      private IMapEditor defaultEditor;

      public MapEditors() {
         editors = new List<IMapEditor>();
      }

      public void RegisterEditor(IMapEditor editor) {
         RegisterEditor(editor, false);
      }
      public void RegisterEditor(IMapEditor editor, bool defaultEditor) {
         if (!editors.Contains(editor))
            editors.Add(editor);
         if (defaultEditor || this.defaultEditor == null)
            this.defaultEditor = editor;
      }

      public IMapEditor this[int index] {
         get { return editors[index]; }
      }

      public List<IMapEditor>.Enumerator GetEnumerator() {
         return editors.GetEnumerator();
      }

      public int Count {
         get { return editors.Count; }
      }
      public IMapEditor Default {
         get { return defaultEditor; }
      }
   }

   /// <summary>Enables basic map panning, spawn selection, and zooming.</summary>
   public class MapNavigator : IMapEditor {
      private bool   engaged        = false;
      private fMap   window         = null;
      private bool   panMode        = false;
      private Point  panMouseOrigin = new Point(0, 0);
      private PointF panViewOrigin  = new PointF(0, 0);

      public virtual void OnEngage(fMap Window) {
         if (engaged)
            throw new Exception("Editor already engaged.");
         engaged = true;
         window = Window;
      }
      public virtual void OnDisengage() {
         engaged = false;
         window = null;
      }

      public virtual void OnMouseDown(MouseEventArgs e) {
         if (e.Button == MouseButtons.Left) {
            //if panning the map, then capture the current mouse location as the origin and enable the panning mode
            panViewOrigin = window.Engine.ViewLocation;
            panMouseOrigin = e.Location;
            panMode = true;
         }
      }

      public virtual void OnMouseMove(MouseEventArgs e) {
         if (panMode) {
            //if panning, then update the view location based on the offset the mouse has floated from its drag origin
            window.Engine.ViewLocation = new PointF(
               (float)(panViewOrigin.X - (float)(panMouseOrigin.X - e.X)),
               (float)(panViewOrigin.Y - (float)(panMouseOrigin.Y - e.Y))
            );
         } else {
            //highlight the hovered spawn
            GameSpawn spawn = window.Engine.FindSpawn(e.X, e.Y);
            if (spawn != window.Engine.Game.Highlighted) {
               if (spawn != null && spawn.DEBUGHOVER != "") {
                  Debug.WriteLine("DEBUG HOVER: " + spawn.DEBUGHOVER);
               }
               window.Engine.Game.Highlighted = spawn;
               window.Invalidate();
            }
         }
      }

      public virtual void OnMouseUp(MouseEventArgs e) {
         if (e.Button == MouseButtons.Left && e.X == panMouseOrigin.X && e.Y == panMouseOrigin.Y) {
            //selected clicked spawn, or deselect if nothing clicked
            GameSpawn spawn = window.Engine.FindSpawn(e.X, e.Y);
            if (spawn != window.Engine.Game.Selected) {
               window.Engine.Game.Selected = spawn;
               window.Invalidate();
            }
         } else if (e.Button == MouseButtons.Right) {
            window.ShowContextMenu(window.Engine.FindSpawn(e.Location), e.Location);
         }
         panMode = false;
      }

      public virtual void OnMouseWheel(MouseEventArgs e) {
         if (e.Delta > 0)
            window.Engine.ZoomIn();
         else
            window.Engine.ZoomOut();
         window.Config["Zoom"] = window.Engine.Zoom;
      }

      public virtual void OnMouseEnter(EventArgs e) {}
      public virtual void OnMouseLeave(EventArgs e) {
         if (window.Engine.Game.Highlighted != null)
            window.Engine.Game.Highlighted = null;
      }
      public virtual bool Engaged {
         get { return engaged; }
      }
      public string Name {
         get { return "navigator"; }
      }

      public virtual void OnBeforePaint(PaintEventArgs e) {}
      public virtual void OnAfterPaint(PaintEventArgs e) {}
      public virtual void OnMove(EventArgs e) {}
      public virtual void OnResize(EventArgs e) {}
      public virtual void OnKeyDown(KeyEventArgs e) {}
      public virtual void OnKeyPress(KeyPressEventArgs e) {}
      public virtual void OnKeyUp(KeyEventArgs e) {}
      public override string ToString() {
         return Program.GetLang("mode_" + Name);
      }
   }
}