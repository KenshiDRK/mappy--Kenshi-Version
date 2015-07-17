using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MapEngine;

namespace mappy {
   public sealed class FFXIMapImageEditor : IMapEditor {
      private bool   engaged        = false;
      private fMap   window         = null;
      private bool   panMode        = false;
      private Point  panMouseOrigin = new Point(0, 0);
      private PointF panViewOrigin  = new PointF(0, 0);
      private IFFXIGameContainer icontainer;
      private IFFXIMapImageContainer imap;
      private FFXIImageMap m_hovered;
      private FFXIImageMap m_selectedMap;
      private Engine engine;
      private Pen pHandleBorder = new Pen(Color.Red);
      private Brush bTitle = new SolidBrush(Color.White);
      private Brush bTitleBG = new SolidBrush(Color.FromArgb(150, Color.Black));

      private Brush bRangeColor = new SolidBrush(Color.FromArgb(50, Color.Black));
      private Brush bRangeHovered = new SolidBrush(Color.FromArgb(100, Color.Green));
      private Brush bRangeSelected = new SolidBrush(Color.FromArgb(150, Color.Blue));

      private int editmode = 0;
      private int edittarget = 0;

      private FFXIMapImageEditorUI ui;
      private LinkedList<FFXIImageMap> mapList;

      public FFXIMapImageEditor() {
         m_hovered = null;
         mapList = new LinkedList<FFXIImageMap>();
      }

      public void OnEngage(fMap Window) {
         if (engaged)
            throw new Exception("Editor already engaged.");
         if (!(Window.Instance is IFFXIGameContainer))
            throw new Exception("Instance does not support FFXI.");
         if (!(Window.Instance is IFFXIMapImageContainer))
            throw new Exception("Instance does not support FFXI image maps.");

         engaged = true;
         window = Window;
         engine = Window.Engine;
         window.Engine.Game.Highlighted = null;
         icontainer = window.Instance as IFFXIGameContainer;
         imap = Window.Instance as IFFXIMapImageContainer;

         if (ui == null)
            ui = new FFXIMapImageEditorUI(this);
         ui.Show(window);
         ui.Left = window.Left;
         ui.Top = window.Bottom;
         ui.Width = window.Width;
         engine.MapAlternativeImage = null;

         BuildMapList();
         ui.updateMapInfo();
         icontainer.ZoneChanged += new EventHandler(icontainer_ZoneChanged);
      }

      private void BuildMapList() {
         //build the linked list from the zone collection
         mapList.Clear();
         foreach (KeyValuePair<int,FFXIImageMap> pair in imap.CurrentZone)
            mapList.AddLast(pair.Value);

         //move the current map to the top of the z-order
         if (imap.CurrentMap != null && imap.CurrentMap != mapList.First.Value) {
            mapList.Remove(imap.CurrentMap);
            mapList.AddFirst(imap.CurrentMap);
            ui.updateTopMostMap();
         }
      }

      public FFXIImageMap TopMostMap {
         get { 
            if (mapList.Count > 0)
               return mapList.First.Value;
            return null;
         }
         set {
            if (mapList.Count == 0 || mapList.First.Value != value) {
               mapList.Remove(value);
               mapList.AddFirst(value);
               ui.updateTopMostMap();
            }
         }
      }

      private void icontainer_ZoneChanged(object sender, EventArgs e) {
         BuildMapList();
         ui.updateMapInfo();
         window.Engine.MapAlternativeImage = null;
      }

      public int Mode {
         get { return editmode; }
         set {
            editmode = value;
            window.Cursor = Cursors.Default;
         }
      }

      public void CreateMap(int ID, float Scale) {
         FFXIImageMap map = imap.CurrentZone.Create(ID, Scale);
         mapList.AddFirst(map);
         ui.updateMapInfo();
      }

      public void RemoveMap(FFXIImageMap map) {
         if (imap.CurrentZone.ContainsValue(map)) {
            imap.CurrentZone.Remove(map.MapID);
            mapList.Remove(map);
            ui.updateMapInfo();
         }
      }

      public void OnDisengage() {
         engine.MapAlternativeBounds = imap.CurrentMap.Bounds;
         icontainer.ZoneChanged -= new EventHandler(icontainer_ZoneChanged);
         window.Cursor = Cursors.Default;
         engaged = false;
         ui.Hide();
         window = null;
         engine.MapAlternativeImage = imap.CurrentMap.GetImage();
      }

      public void OnMouseDown(MouseEventArgs e) {
         if (e.Button == MouseButtons.Left) {
            //if panning the map, then capture the current mouse location as the origin and enable the panning mode
            panViewOrigin = window.Engine.ViewLocation;
            panMouseOrigin = e.Location;
            panMode = true;

            if (editmode == 0) {
               m_selectedMap = GetMapFromPoint(e.Location);
               if (m_selectedMap == null || (UserControl.ModifierKeys & Keys.Control) > 0) {
                  edittarget = 0;
               } else {
                  if (m_selectedMap != mapList.First.Value) {
                     mapList.Remove(m_selectedMap);
                     mapList.AddFirst(m_selectedMap);
                     ui.updateTopMostMap();
                  }
                  panViewOrigin = new PointF(m_selectedMap.XOffset, m_selectedMap.YOffset);
                  edittarget = 1;
               }
            }
         }
      }

      public void OnMouseMove(MouseEventArgs e) {
         if (panMode) {
            if (editmode == 0 && edittarget == 1) {
               m_selectedMap.SetMapLocation(
                  panViewOrigin.X + (float)(((panMouseOrigin.X - e.X) * m_selectedMap.XScale) / engine.Scale),
                  panViewOrigin.Y + (float)(((panMouseOrigin.Y - e.Y) * -m_selectedMap.YScale) / engine.Scale)
               );
               engine.UpdateMap();
            } else {
               //default pan mode behavior
               window.Engine.ViewLocation = new PointF(
                  (float)(panViewOrigin.X - (float)(panMouseOrigin.X - e.X)),
                  (float)(panViewOrigin.Y - (float)(panMouseOrigin.Y - e.Y))
               );
            }
         } else {
            if (editmode == 0) {
               m_hovered = GetMapFromPoint(e.Location);
               if (m_hovered == null || (UserControl.ModifierKeys & Keys.Control) > 0) {
                  window.Cursor = Cursors.Default;
               } else {
                  window.Cursor = Cursors.SizeAll;
               }
            }
         }
      }

      private FFXIImageMap GetMapFromPoint(Point point) {
         Engine e = window.Engine;
         float x = point.X;
         float y = point.Y;
         RectangleF bounds;

         LinkedListNode<FFXIImageMap> node = mapList.First;
         while (node != null) {
            bounds = node.Value.Bounds;
            if (x <= e.CalcClientCoordX(bounds.Right) && x >= e.CalcClientCoordX(bounds.Left) && y <= e.CalcClientCoordY(bounds.Bottom) && y >= e.CalcClientCoordY(bounds.Top))
               return node.Value;
            node = node.Next;
         }
         return null;
      }

      public void OnMouseUp(MouseEventArgs e) {
         if (e.Button == MouseButtons.Right) {
            window.ShowContextMenu(null, e.Location);
         }
         panMode = false;
      }

      public void OnMouseWheel(MouseEventArgs e) {
         if (e.Delta > 0)
            window.Engine.ZoomIn();
         else
            window.Engine.ZoomOut();
         window.Config["Zoom"] = window.Engine.Zoom;
      }

      public void OnMouseEnter(EventArgs e) { }
      public void OnMouseLeave(EventArgs e) {
         m_hovered = null;
      }

      public void OnBeforePaint(PaintEventArgs e) {
         if (mapList.Count > 0) {
            LinkedListNode<FFXIImageMap> node = mapList.Last;
            while (node != null) {
               DrawMap(node.Value, e.Graphics);
               node = node.Previous;
            }
         }
      }

      private void DrawMap(FFXIImageMap map, Graphics g) {
         if (map == null)
            return;
         RectangleF bounds = map.Bounds;
         Image image = map.GetImage();

         RectangleF dest = RectangleF.FromLTRB(
            window.Engine.CalcClientCoordX(bounds.Left),
            window.Engine.CalcClientCoordY(bounds.Top),
            window.Engine.CalcClientCoordX(bounds.Right),
            window.Engine.CalcClientCoordY(bounds.Bottom)
         );
         if (image != null) {
            g.DrawImage(image, Rectangle.Round(dest), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
         } else {
            g.FillRectangle(bTitleBG, dest);
            g.DrawLine(pHandleBorder, dest.Left, dest.Top, dest.Right, dest.Bottom);
            g.DrawLine(pHandleBorder, dest.Left, dest.Bottom, dest.Right, dest.Top);
         }

         string mapinfo = "Map " + map.MapID + " Scale: " + map.XScale + " Offset: " + map.XOffset + "," + map.YOffset;
         SizeF size = g.MeasureString(mapinfo, SystemFonts.DefaultFont);
         float x = engine.CalcClientCoordX(bounds.X);
         float y = engine.CalcClientCoordY(bounds.Y);
         g.FillRectangle(bTitleBG, x, y, size.Width + 2, size.Height + 2);
         g.DrawString(mapinfo, SystemFonts.DefaultFont, bTitle, x + 1, y + 1);
      }

      public void OnAfterPaint(PaintEventArgs e) {
         if (imap.CurrentZone != null) {
            if (editmode == 0 && m_hovered != null) {
               //map editor
               RectangleF bounds = m_hovered.Bounds;
               e.Graphics.DrawRectangle(pHandleBorder, engine.CalcClientCoordX(bounds.X), engine.CalcClientCoordY(bounds.Y), bounds.Width * engine.Scale, -bounds.Height * engine.Scale);
            } else if (editmode == 1) {
               //area editor
               if (mapList.Count > 0) {
                  Brush fill = bRangeColor;
                  LinkedListNode<FFXIImageMap> node = mapList.Last;
                  while (node != null) {
                     if (node == mapList.First) {
                        fill = bRangeSelected;
                     } else {
                        fill = bRangeColor;
                     }
                     foreach (FFXIImageMapRange range in node.Value) {
                        RectangleF dest = RectangleF.FromLTRB(              //top/bottom swapped for rendering due to top->bottom coordinate space of the window
                           window.Engine.CalcClientCoordX(range.Left),
                           window.Engine.CalcClientCoordY(range.Bottom),
                           window.Engine.CalcClientCoordX(range.Right),
                           window.Engine.CalcClientCoordY(range.Top)
                        );

                        e.Graphics.FillRectangle(fill, dest);
                        e.Graphics.DrawRectangle(pHandleBorder, Rectangle.Round(dest));
                     }
                     
                     node = node.Previous;
                  }

               }
            }
         }

         if (window.Engine.Game.Player != null) {
            MapPoint p = window.Engine.Game.Player.Location;
            string zoneinfo = "Zone: " + imap.CurrentZone.ZoneID + " (" + imap.CurrentZone.ZoneID.ToString("X2") + "); Pos: " + p.X + "," + p.Y + "," + p.Z;
            SizeF size = e.Graphics.MeasureString(zoneinfo, SystemFonts.DefaultFont);
            e.Graphics.FillRectangle(bTitleBG, 0, window.Height - SystemFonts.DefaultFont.Height - 4, size.Width + 4, size.Height + 2);
            e.Graphics.DrawString(zoneinfo, SystemFonts.DefaultFont, bTitle, 2, window.Height - SystemFonts.DefaultFont.Height - 2);
         }
      }

      public void OnMove(EventArgs e) {
         ui.Left = window.Left;
         ui.Top = window.Bottom;
      }
      public void OnResize(EventArgs e) {
         ui.Left = window.Left;
         ui.Top = window.Bottom;
         ui.Width = window.Width;
      }
      public void OnKeyDown(KeyEventArgs e) {
         if (editmode == 0 && e.KeyCode == Keys.ControlKey)
            window.Cursor = Cursors.Default;
      }
      public void OnKeyUp(KeyEventArgs e) {
         if (editmode == 0 && e.KeyCode == Keys.ControlKey)
            window.Cursor = Cursors.SizeAll;
      }
      public void OnKeyPress(KeyPressEventArgs e) { }

      public bool Engaged {
         get { return engaged; }
      }
      public string Name {
         get { return "imagemapeditor"; }
      }
      public override string ToString() {
         return Program.GetLang("mode_" + Name);
      }

      public IFFXIMapImageContainer Container {
         get { return imap; }
      }
      public fMap Window {
         get { return window; }
      }
   }
}
