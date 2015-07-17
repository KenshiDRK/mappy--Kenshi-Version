using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace MapEngine {
   public delegate void MapLineEvent(MapLine line);
   public delegate void MapLabelEvent(MapLabel label);
   public delegate void MapHuntEvent(MapHunt hunt);
   public delegate void MapReplacementEvent(MapReplacement hunt);

   /// <summary>An engine component that drives all map related data.</summary>
   public class MapData : ICloneable {
      private RectangleF mapRect = new RectangleF();
      private RectangleF mapRectOverride;
      private float      mapMinX, lastMapMinX;
      private float      mapMaxX, lastMapMaxX;
      private float      mapMinY, lastMapMinY;
      private float      mapMaxY, lastMapMaxY;

      private Engine     m_engine;
      private string     m_AreaName;
      private string     m_FileName = "";
      private string     m_FileExt = "map";
      private string     m_FilePath = "";
      private bool       m_Empty = true;

      private MapLine    m_editline;
      private Color      m_editcolorlast = Color.White;
      private bool       m_dirty = false;
      private bool       m_suspendDirty = false;
      private bool       m_useOverride = false;

      private List<MapLine>  m_lines;
      private List<MapLabel> m_labels;
      private MapHunts       m_hunts;
      private MapReplacements m_replacements;

      public event GenericEvent DataChanged;

      //====================================================================================================
      // Constructor
      //====================================================================================================
      public MapData(Engine engine) {
         m_engine = engine;
         m_lines = new List<MapLine>();
         m_labels = new List<MapLabel>();
         m_hunts = new MapHunts(this);
         m_replacements = new MapReplacements(this);
         m_suspendDirty = false;

         m_hunts.Updated += new MapHuntEvent(m_hunts_Updated);
         m_replacements.Updated += new MapReplacementEvent(m_replacements_Updated);

         Clear();
      }

      //====================================================================================================
      // Properties
      //====================================================================================================
      /// <summary>Gets the boundaries of the current map data in MAP coordinates.</summary>
      public RectangleF Bounds {
         get {
            if (m_useOverride)
               return mapRectOverride;
            return mapRect; 
         }
      }
      /// <summary>Gets the size of the current map data.</summary>
      public SizeF Size {
         get { return mapRect.Size; }
      }
      /// <summary>Gets the vector map data.</summary>
      public List<MapLine> Lines {
         get { return m_lines; }
      }
      /// <summary>Gets the map labels (aka points of interest).</summary>
      public List<MapLabel> Labels {
         get { return m_labels; }
      }
      /// <summary>Gets the hunts for the current zone.</summary>
      public MapHunts Hunts {
         get { return m_hunts; }
      }
      /// <summary>Gets the replacements for the current zone.</summary>
      public MapReplacements Replacements
      {
          get { return m_replacements; }
      }
      /// <summary>Gets or sets the current zone name.</summary>
      public string ZoneName {
         get { return m_AreaName; }
         set {
            m_AreaName = value;
            m_Empty = false;
         }
      }
      /// <summary>Determines if there is any data available.</summary>
      public bool Empty {
         get { return m_Empty; }
      }
      /// <summary>Determines if there have been any changes made since the last save.</summary>
      public bool Dirty {
         get { return m_dirty; }
         private set {
            if (!m_suspendDirty)
               m_dirty = value;
         }
      }
      /// <summary>Gets the engine associated with this data container.</summary>
      public Engine Engine {
         get { return m_engine; }
      }
      /// <summary>Gets or sets the file path of the map data.</summary>
      public string FilePath {
         get { return m_FilePath; }
         set {
            if (value != "" && value.Substring(value.Length - 1, 1) != "\\")
               value += "\\";
            m_FilePath = value;
            Reload();
         }
      }

      //====================================================================================================
      // Methods
      //====================================================================================================
      
      /// <summary>Clears all map data.</summary>
      public void Clear() {
         m_Empty = true;
         m_AreaName = "";
         m_FileName = "";
         mapMinX = -50;
         mapMaxX = 50;
         mapMinY = -50;
         mapMaxY = 50;
         lastMapMinX = 0;
         lastMapMinY = 0;
         lastMapMaxX = 0;
         lastMapMaxY = 0;
         m_useOverride = false;
         mapRectOverride = RectangleF.Empty;
         CheckBatchEnd();
         m_lines.Clear();
         m_labels.Clear();
         m_hunts.Clear();
         m_replacements.Clear();
         m_dirty = false;
      }

      /// <summary>Creates a deep copy of the map data. Note that objects within collections are only a shallow copy.</summary>
      public MapData Clone() {
         //Create shallow copy
         MapData copy = (MapData)((ICloneable)this).Clone();
         
         //Deepen copy for the collections.
         copy.m_lines = new List<MapLine>();
         foreach (MapLine line in m_lines)
            copy.m_lines.Add(line);

         copy.m_labels = new List<MapLabel>();
         foreach (MapLabel label in m_labels)
            copy.m_labels.Add(label);

         copy.m_hunts = m_hunts.Clone();
         copy.m_replacements = m_replacements.Clone();

         return copy;
      }

      object ICloneable.Clone() {
         return this.MemberwiseClone();
      }

      /// <summary>Temporarily overrides the map boundary with the given dimension, specifically to influence zooming calculations within the map engine.</summary>
      public void OverrideBounds(RectangleF bounds) {
         mapRectOverride = bounds;
         m_useOverride = true;
         if (DataChanged != null)
            DataChanged();
      }

      /// <summary>Clears any previous boundary override.</summary>
      public void ClearOverride() {
         mapRectOverride = RectangleF.Empty;
         m_useOverride = false;
         if (DataChanged != null)
            DataChanged();
      }

      /// <summary>Adds a new regular expression as a hunt.</summary>
      public void AddHunt(string hunt, bool permanent) {
         m_hunts.Add(hunt, permanent);
      }

      /// <summary>Adds a new regular expression as a hunt.</summary>
      public void AddReplacement(string regex, string replacement, bool permanent)
      {
          m_replacements.Add(regex, replacement, permanent);
      }

      /// <summary>Adds a label to the current map.</summary>
      public void AddLabel(MapLabel label) {
         m_labels.Add(label);
         label.Updated += new MapLabelEvent(label_Updated);
         if (DataChanged != null)
            DataChanged();
      }

      /// <summary>Adds a vector line to the current map.</summary>
      public void AddLine(MapLine line) {
         m_lines.Add(line);
         line.Updated += new MapLineEvent(line_Updated);
         line.Update();
      }

      /// <summary>Creates a new vector line at the players current location and enables editting mode.</summary>
      public void EditStartLine() {
         if (m_editline != null && m_editline.Color != m_editcolorlast)
            m_editcolorlast = m_editline.Color;
         EditStartLine(m_editcolorlast);
      }
      /// <summary>Creates a new vector line with the specified color at the players current location and enables editting mode.</summary>
      public void EditStartLine(Color linecolor) {
         if (m_engine.Game.Player == null)
            return;
         m_editline = new MapLine(linecolor, m_engine.Game.Player.Location);
         m_lines.Add(m_editline);
         m_editline.Updated += new MapLineEvent(line_Updated);
         m_editline.Update();
         m_editcolorlast = linecolor;
      }
      /// <summary>Changes the line color of the current line being editted (if any) and sets the default color for all future lines.</summary>
      public void EditChangeLineColor(Color linecolor) {
         m_editcolorlast = linecolor;
         if (m_editline == null)
            return;
         m_editline.Color = linecolor;
         if (DataChanged != null)
            DataChanged();
      }

      /// <summary>The last vector line created in the current session is removed and edit mode disabled.</summary>
      public void EditCancelLine() {
         if (m_editline == null)
            return;
         m_editline.Updated -= new MapLineEvent(line_Updated);
         m_lines.Remove(m_editline);
         m_editline = null;
         if (DataChanged != null)
            DataChanged();
      }

      /// <summary>Adds a new point to the current line being editted. If no line is being editted then this does nothing.</summary>
      public void EditAddPoint() {
         if (m_editline == null || m_engine.Game.Player == null)
            return;
         m_editline.Add(m_engine.Game.Player.Location);
         m_editline.Update();
      }

      /// <summary>Removes the last point added the currently editted line. If no line is being editted or only the origin point is left then this does nothing.</summary>
      public void EditRemovePoint() {
         if (m_editline == null || m_editline.Count < 2) //have to cancel the line to change the first point
            return;
         m_editline.RemoveLast();
         m_editline.Update();
      }

      /// <summary>Clears and reloads all map data from the last file loaded (if any).</summary>
      public void Reload() {
         if(m_FileName != "") {
            Clear();
            Load(m_FileName);
         }
      }

      /// <summary>Loads the map data based on the zone name.</summary>
      public void LoadZone(string ZoneName) {
         if (ZoneName == "")
            return;
         Load(ZoneName + "." + m_FileExt);
      }
      /// <summary>Loads the map data from the given file. Do not specify a base path, but rather change the FilePath property as required.</summary>
      public void Load(string filename) {
         if (filename == "")
            return;
         m_FileName = filename; //set even if it doesnt exist, so that a (potential) future save can use it

         string fullfilepath = m_FilePath + filename;
         Debug.WriteLine("Load Map: " + fullfilepath);

         //Exit if the file does not exist
         if (!File.Exists(fullfilepath)) {
            Debug.WriteLine("FILE NOT FOUND: " + fullfilepath);
            return;
         }

         //LAYOUT
         //M RRGGBB,x,y,z,x,y,z (so on) 3D Map line. must have at least a pair of vectors.
         //P RRGGBB,x,y,z,text          Label for on map display.
         //H pattern                    Regex pattern to match against spawn names. One pattern per line.

         try {
            m_suspendDirty = true;
            FileStream stream = File.OpenRead(fullfilepath);
            StreamReader reader = new StreamReader(stream);
            string line = "";
            string lineData = "";
            string[] parts;
            int linepos = 0; //for error tracking

            while ((line = reader.ReadLine()) != null) {
               lineData = line.Substring(2);
               switch (line[0]) {
                  case 'M': //map line
                     parts = lineData.Split(',');
                     if (parts.Length == 0) //malformed entry if there are no delimiters
                        continue;
                     if (parts[0].Length != 6) //the first item is the line color
                        continue;
                     if ((parts.Length - 1) % 3 != 0) //the line should always have 3 coords per vector
                        continue;

                     try {
                        //covert the RRGGBB line color to a Color object
                        int red = System.Convert.ToByte(parts[0].Substring(0, 2), 16);
                        int green = System.Convert.ToByte(parts[0].Substring(2, 2), 16);
                        int blue = System.Convert.ToByte(parts[0].Substring(4, 2), 16);
                        MapLine mapline = new MapLine(Color.FromArgb(red, green, blue));

                        int i = 1;
                        while (i < parts.Length) {
                           mapline.Add(
                              float.Parse(parts[i], CultureInfo.InvariantCulture),
                              float.Parse(parts[i + 1], CultureInfo.InvariantCulture),
                              float.Parse(parts[i + 2], CultureInfo.InvariantCulture)
                           );
                           i += 3;
                        }
                        mapline.Updated += new MapLineEvent(line_Updated);
                        mapline.Update();
                        m_lines.Add(mapline);
                     } catch (Exception ex) {
                        //swallow any parsing errors here. this will effectivly reject the line in whole
                        Debug.WriteLine("Error occurred parsing M type on line #" + linepos + ": " + ex.Message);
                     }

                     break;
                  case 'P': //point of interest (map label)
                     parts = lineData.Split(',');
                     if (parts.Length != 5) //there are exactly 5 parts to each label. too bad if someone added a comma in the label text!
                        continue;
                     if (parts[0].Length != 6) //the first item is the line color
                        continue;

                     try {
                        //covert the RRGGBB line color to a Color object
                        int red = System.Convert.ToByte(parts[0].Substring(0, 2), 16);
                        int green = System.Convert.ToByte(parts[0].Substring(2, 2), 16);
                        int blue = System.Convert.ToByte(parts[0].Substring(4, 2), 16);

                        //create the label object
                        MapLabel label = new MapLabel(
                           parts[4],
                           new MapPoint(
                              float.Parse(parts[1], CultureInfo.InvariantCulture),
                              float.Parse(parts[2], CultureInfo.InvariantCulture),
                              float.Parse(parts[3], CultureInfo.InvariantCulture)
                           ),
                           Color.FromArgb(red, green, blue)
                        );
                        m_labels.Add(label);
                     } catch (Exception ex) {
                        //swallow any parsing errors here. this will effectivly reject the line in whole
                        Debug.WriteLine("Error occurred parsing P type on line #" + linepos + ": " + ex.Message);
                     }

                     break;
                  case 'H': //auto-hunt
                     m_hunts.Add(lineData, true);
                     break;
                  case 'R': //auto-replacement
                     parts = lineData.Split(',');
                     m_replacements.Add(parts[0], parts[1], true);
                     break;
               }
               linepos++;
            }

            //close the writer
            reader.Close();
            m_Empty = false;

            //automatically snap the range into view (if enabled)
            if (m_engine.AutoRangeSnap)
               m_engine.SnapToRange();
         } catch (Exception ex) {
            Debug.WriteLine("READ ERROR: " + ex.Message);
         } finally {
            m_suspendDirty = false;
         }
      }
      
      /// <summary>Saves the map data using the last loaded file. If no file load was attempted then this does nothing.</summary>
      public void Save() {
         if (m_FileName == "")
            return;
         Save(m_FileName);
      }
      /// <summary>Saves the map data using the given file name. Do not specify a base path, but rather change the FilePath property as required.</summary>
      public void Save(string filename) {
         if (filename == "")
            return;
         string outline = "";
         string fullfilepath = m_FilePath + filename;

         Debug.WriteLine("Save Map: " + fullfilepath);

         try {
            FileStream stream = File.Open(fullfilepath, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            //Write each map line to the file
            foreach (MapLine line in m_lines) {
               outline = string.Format(CultureInfo.InvariantCulture, "M {0:X2}{1:X2}{2:X2}", line.Color.R, line.Color.G, line.Color.B);
               foreach (MapPoint point in line) {
                  outline += string.Format(CultureInfo.InvariantCulture, ",{0:0.###},{1:0.###},{2:0.###}", point.X, point.Y, point.Z);
               }
               writer.WriteLine(outline);
            }

            //Write each map label to the file
            foreach (MapLabel label in m_labels) {
               outline = string.Format(
                  CultureInfo.InvariantCulture,
                  "P {0:X2}{1:X2}{2:X2},{3:0.###},{4:0.###},{5:0.###},{6}",
                  label.Color.R, label.Color.G, label.Color.B,
                  label.Location.X, label.Location.Y, label.Location.Z,
                  label.Caption
               );
               writer.WriteLine(outline);
            }

            //write each permanent hunt to the file
            foreach (KeyValuePair<string, MapHunt> pair in m_hunts) {
               if (pair.Value.Permanent)
                  writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "H {0}", pair.Value.ToString()));
            }

            //write each permanent replacement to the file
            foreach (KeyValuePair<string, MapReplacement> pair in m_replacements)
            {
                if (pair.Value.Permanent)
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "R {0},{1}", pair.Value.ToString(), pair.Value.ReplacedName));
            }

            //close the writer
            writer.Close();
            m_dirty = false;
         } catch (Exception ex) {
            Debug.WriteLine("SAVE ERROR: " + ex.Message);
         }
      }

      /// <summary>Checks the given point against the current map boundaries and expands it as necessary.</summary>
      public bool CheckPoint(MapPoint point) {
         return CheckPoint(point.X, point.Y);
      }
      /// <summary>Checks the given point against the current map boundaries and expands it as necessary.</summary>
      public bool CheckPoint(float X, float Y) {
         bool OOB = false;

         if (X > mapMaxX) {
            mapMaxX = X;
            OOB = true;
         }
         if (Y > mapMaxY) {
            mapMaxY = Y;
            OOB = true;
         }
         if (X < mapMinX) {
            mapMinX = X;
            OOB = true;
         }
         if (Y < mapMinY) {
            mapMinY = Y;
            OOB = true;
         }

         if (OOB)
            CheckBatchEnd();
         return OOB;
      }

      /// <summary>Checks the given box againt the current map boundaries and expands it as necessary.</summary>
      public void CheckBounds(RectangleF Bounds) {
         CheckBatch(Bounds.X, Bounds.Y);
         CheckBatch(Bounds.Right, Bounds.Bottom);
         CheckBatchEnd();
      }

      /// <summary>Checks the given point against the current map boundaries and expands it as necessary as part of a batch. Use CheckBatchend when finished.</summary>
      public void CheckBatch(MapPoint point) {
         CheckBatch(point.X, point.Y);
      }
      /// <summary>Checks the given point against the current map boundaries and expands it as necessary as part of a batch. Use CheckBatchend when finished.</summary>
      public void CheckBatch(float X, float Y) {
         if (X > mapMaxX)
            mapMaxX = X;
         if (Y > mapMaxY)
            mapMaxY = Y;
         if (X < mapMinX)
            mapMinX = X;
         if (Y < mapMinY)
            mapMinY = Y;
      }
      /// <summary>Recalculates the current map boundaries after checking points in batch.</summary>
      public void CheckBatchEnd() {
         //prevent bad data from hosing the app...
         if (mapMaxX > 50000)
            mapMaxX = 1000;
         if (mapMaxY > 50000)
            mapMaxY = 1000;
         if (mapMinX < -50000)
            mapMinX = -1000;
         if (mapMinY < -50000)
            mapMinY = -1000;

         if (mapMaxX != lastMapMaxX || mapMaxY != lastMapMaxY || mapMinX != lastMapMinX || mapMinY != lastMapMinY) {
            lastMapMaxX = mapMaxX;
            lastMapMaxY = mapMaxY;
            lastMapMinX = mapMinX;
            lastMapMinY = mapMinY;

            mapRect = RectangleF.FromLTRB(mapMinX, mapMinY, mapMaxX, mapMaxY);
            if (DataChanged != null)
               DataChanged();
         }
      }

      //capture updates made to any item in the collections
      private void line_Updated(MapLine line) {
         m_Empty = false;
         Dirty = true;
         CheckBatch(line.Bounds.Left, line.Bounds.Top);
         CheckBatch(line.Bounds.Right, line.Bounds.Bottom);
         CheckBatchEnd();
      }
      private void label_Updated(MapLabel label) {
         m_Empty = false;
         Dirty = true;
         if (DataChanged != null)
            DataChanged();
      }
      private void m_hunts_Updated(MapHunt hunt) {
         if (hunt.Permanent) {
            m_Empty = false;
            Dirty = true;
         }
         if (DataChanged != null)
            DataChanged();
      }

      private void m_replacements_Updated(MapReplacement rep)
      {
          if (rep.Permanent)
          {
              m_Empty = false;
              Dirty = true;
          }
          if (DataChanged != null)
              DataChanged();
      }
   }

   /// <summary>A collection of MapPoints that define a single line to be rendered to the client.</summary>
   public class MapLine {
      private Color          m_color;
      private RectangleF     m_bounds;
      private List<MapPoint> m_points;

      public event MapLineEvent Updated;

      public MapLine(Color color) {
         m_color = color;
         m_points = new List<MapPoint>();
         m_bounds = new Rectangle(0, 0, 0, 0);
      }
      public MapLine(Color color, MapPoint start) {
         m_color = color;
         m_points = new List<MapPoint>();
         m_bounds = new Rectangle(0, 0, 0, 0);
         Add(start);
      }

      /// <summary>The bounds is not automatically calculated after a point is added or changed.</summary>
      public void Update() {
         if (m_points.Count <= 0)
            return;

         float minX, maxX, minY, maxY;
         bool updated = false;

         minX = maxX = m_points[0].X;
         minY = maxY = m_points[0].Y;

         foreach (MapPoint point in m_points) {
            if (point.X < minX) {
               minX = point.X;
               updated = true;
            } else if (point.X > maxX) {
               maxX = point.X;
               updated = true;
            }
            if (point.Y < minY) {
               minY = point.Y;
               updated = true;
            } else if (point.Y > maxY) {
               maxY = point.Y;
               updated = true;
            }
         }

         if (updated) {
            m_bounds = RectangleF.FromLTRB(minX, minY, maxX, maxY);
            if (Updated != null)
               Updated(this);
         }
      }

      /// <summary>Gets the total 2D space the map line occupies across all of its vertexes.</summary>
      public RectangleF Bounds {
         get { return m_bounds; }
      }

      /// <summary>Gets or sets the color for the line.</summary>
      public Color Color {
         get { return m_color; }
         set { m_color = value; }
      }

      /// <summary>Gets the number of vertexes in the line.</summary>
      public int Count {
         get { return m_points.Count; }
      }

      /// <summary>Clear all vertexes from the current line.</summary>
      public void Clear() {
         if (m_points.Count > 0) {
            m_points.Clear();
            m_bounds = new Rectangle(0, 0, 0, 0);
            if (Updated != null)
               Updated(this);
         }
      }

      /// <summary>Retrieve all vertexes in bulk.</summary>
      public MapPoint[] ToArray() {
         return m_points.ToArray();
      }

      /// <summary>Converts each vertex to a 2D coodinate and returns the points in an array. Meant to be used in conjunction with Graphics.DrawPoints function.</summary>
      public PointF[] getPoints() {
         PointF[] points = new PointF[m_points.Count];
         for (int i = 0; i < m_points.Count; i++) {
            points[i] = m_points[i].point;
         }
         return points;
      }

      /// <summary>Remove the vertex at the specific index.</summary>
      public void RemoveAt(int index) {
         if (index < 0 || index >= m_points.Count)
            return;
         m_points.RemoveAt(index);
         if (m_points.Count <= 0) {
            m_bounds = new Rectangle(0, 0, 0, 0);
            if (Updated != null)
               Updated(this);
         } else {
            Update();
         }
      }

      /// <summary>Remove the last point added.</summary>
      public void RemoveLast() {
         if (m_points.Count == 0)
            return;
         m_points.RemoveAt(m_points.Count - 1);
      }

      /// <summary>Gets the vertex at the requested index.</summary>
      public MapPoint this[int index] {
         get { return m_points[index]; }
      }
      /// <summary>Inserts the vertex at the requested index.</summary>
      public void Insert(int index, float X, float Y, float Z) {
         m_points.Insert(index, new MapPoint(X, Y, Z));
      }
      /// <summary>Appends the vertex to the end of the line.</summary>
      public void Add(float X, float Y, float Z) {
         m_points.Add(new MapPoint(X, Y, Z));
      }
      /// <summary>Appends the MapPoint to the end of the line.</summary>
      public void Add(MapPoint point) {
         m_points.Add(new MapPoint(point.X, point.Y, point.Z)); //create a copy
      }
      public List<MapPoint>.Enumerator GetEnumerator() {
         return m_points.GetEnumerator();
      }
   }

   /// <summary>Defines a single vertex in 3D space.</summary>
   public class MapPoint : ICloneable {
      private float m_x, m_y, m_z;
      public static readonly MapPoint Empty;

      //===================================================================================================
      // constructors
      //===================================================================================================
      public MapPoint() {
         m_x = 0f;
         m_y = 0f;
         m_z = 0f;
      }
      public MapPoint(float x, float y, float z) {
         m_x = x;
         m_y = y;
         m_z = z;
      }
      public MapPoint(MapPoint point) {
         m_x = point.X;
         m_y = point.Y;
         m_z = point.Z;
      }

      //===================================================================================================
      // operators
      //===================================================================================================
      public static MapPoint operator +(MapPoint left, MapPoint right) {
         return new MapPoint(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
      }
      public static MapPoint operator -(MapPoint left, MapPoint right) {
         return new MapPoint(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
      }
      public static MapPoint operator *(MapPoint point, int coef) {
         return new MapPoint(point.X * coef, point.Y * coef, point.Z * coef);
      }
      public static MapPoint operator *(MapPoint point, float coef) {
         return new MapPoint(point.X * coef, point.Y * coef, point.Z * coef);
      }
      public static MapPoint operator /(MapPoint point, int coef) {
         return new MapPoint(point.X / coef, point.Y / coef, point.Z / coef);
      }
      public static MapPoint operator /(MapPoint point, float coef) {
         return new MapPoint(point.X / coef, point.Y / coef, point.Z / coef);
      }

      //===================================================================================================
      // properties
      //===================================================================================================
      /// <summary>Gets or sets the X coordinate of this vertex.</summary>
      public float X {
         get { return m_x; }
         set { m_x = value; }
      }
      /// <summary>Gets or sets the Y coordinate of this vertex.</summary>
      public float Y {
         get { return m_y; }
         set { m_y = value; }
      }
      /// <summary>Gets or sets the Z coordinate of this vertex.</summary>
      public float Z {
         get { return m_z; }
         set { m_z = value; }
      }
      /// <summary>Gets the 2D point representation of this vertex.</summary>
      public PointF point {
         get { return new PointF(m_x, m_y); }
      }

      //===================================================================================================
      // methods
      //===================================================================================================
      /// <summary>Sets the vertex data for this MapPoint.</summary>
      public void Set(float x, float y, float z) {
         m_x = x;
         m_y = y;
         m_z = z;
      }
      /// <summary>Copies the vertex information from the given MapPoint.</summary>
      public void Set(MapPoint point) {
         m_x = point.X;
         m_y = point.Y;
         m_z = point.Z;
      }
      /// <summary>Increases the current vertex data by the given amount.</summary>
      public void Add(float x, float y, float z) {
         m_x += x;
         m_y += y;
         m_z += z;
      }
      /// <summary>Determines if this vertex is at the same 3D location as the given data.</summary>
      public bool isEqual(float x, float y, float z) {
         return (m_x == x && m_y == y && m_z == z);
      }
      /// <summary>Determines if this vertex is at the same 3D location of another MapPoint.</summary>
      public bool isEqual(MapPoint point) {
         return (m_x == point.X && m_y == point.Y && m_z == point.Z);
      }
      /// <summary>Determines if this vertex is positioned at the origin.</summary>
      public bool isNull() {
         return (m_x == 0 && m_y == 0 && m_z == 0);
      }

      public PointF offsetPoint(PointF center, float ratio) {
         return new PointF(
            center.X - (m_x / ratio),
            center.Y - (m_y / ratio)
         );
      }
      public PointF inverseOffsetPoint(PointF center, float ratio) {
         return new PointF(
            center.X - (m_x / ratio),
            center.Y - (m_y / ratio)
         );
      }

      public double calcDist2D(float x, float y) {
         float xDiff = m_x - x;
         float yDiff = m_y - y;
         return Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff));
      }
      public double calcDist2D(MapPoint point) {
         return calcDist2D(point.X, point.Y);
      }

      public double calcDist(float x, float y, float z) {
         float xDiff = m_x - x;
         float yDiff = m_y - y;
         float zDiff = m_z - z;
         return Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff) + (zDiff * zDiff));
      }
      public double calcDist(MapPoint point) {
         return calcDist(point.X, point.Y, point.Z);
      }

      public float calcDistSquared(MapPoint point) {
         return calcDistSquared(point.X, point.Y, point.Z);
      }
      public float calcDistSquared(float x, float y, float z) {
         float xDiff = m_x - x;
         float yDiff = m_y - y;
         float zDiff = m_z - z;
         return ((xDiff * xDiff) + (yDiff * yDiff) + (zDiff * zDiff));
      }

      /// <summary>Create a copy of the current MapPoint</summary>
      public MapPoint Clone() {
         return (MapPoint)((ICloneable)this).Clone();
      }

      object ICloneable.Clone() {
         return this.MemberwiseClone();
      }

      /// <summary>Returns a human friendly string of the vertex data.</summary>
      public override string ToString() {
         return "{X=" + m_x + ", Y=" + m_y + ", Z=" + m_z + "}";
      }
   }

   /// <summary>Defines a user string positioned at a given map coordinate.</summary>
   public class MapLabel {
      private string   m_caption;
      private MapPoint m_location;
      private Color    m_color;

      public event MapLabelEvent Updated;

      public MapLabel(string caption, Color color) : this(caption, new MapPoint(0, 0, 0), color) { }
      public MapLabel(string caption, MapPoint location, Color color) : this(caption, location.X, location.Y, location.Z, color) { }  //always make a copy of the location
      public MapLabel(string caption, float x, float y, float z, Color color) {
         m_caption = caption;
         m_location = new MapPoint(x, y, z);
         m_color = color;
      }

      /// <summary>Gets or sets the caption to display.</summary>
      public string Caption {
         get { return m_caption; }
         set {
            m_caption = value;
            if (Updated != null)
               Updated(this);
         }
      }

      /// <summary>Gets the location the caption will be rendered at.</summary>
      public MapPoint Location {
         get { return m_location; }
      }
      /// <summary>Gets or sets the color of the caption.</summary>
      public Color Color {
         get { return m_color; }
         set {
            m_color = value;
            if (Updated != null)
               Updated(this);
         }
      }
      /// <summary>Sets the current map location of the label.</summary>
      public void setLocation(float x, float y, float z) {
         m_location.X = x;
         m_location.Y = y;
         m_location.Z = z;
         if (Updated != null)
            Updated(this);
      }
   }

   /// <summary>Defines a collection of regular expressions that are used to match against spawn names.</summary>
   public class MapHunts : ICloneable {
      private Dictionary<string, MapHunt> m_hunts;
      private MapData m_data;

      public event MapHuntEvent Updated;
      public event GenericEvent DataChanged;

      public MapHunts(MapData Data) {
         m_hunts = new Dictionary<string, MapHunt>();
         m_data = Data;
      }

      /// <summary>Adds the pattern as a hunt.</summary>
      /// <param name="pattern">A regular expression to compare against the spawn name.</param>
      /// <param name="permanent">Determines if the hunt will be saved with the map data.</param>
      public MapHunt Add(string pattern, bool permanent) {
         if (pattern == "" || m_hunts.ContainsKey(pattern))
            return null;
         try
         {
             Regex.Match("", pattern);
         }
         catch (ArgumentException)
         {
             return null;
         }
         MapHunt hunt = new MapHunt(pattern, permanent);
         m_hunts.Add(pattern, hunt);
         Bind(hunt);

         if (Updated != null)
            Updated(hunt);
         if(DataChanged != null)
            DataChanged();

         return hunt;
      }

      /// <summary>Removes the hunt from the list.</summary>
      public void Remove(MapHunt hunt) {
         Remove(hunt.Hunt.ToString());
      }
      /// <summary>Removes the pattern from the hunt list.</summary>
      public void Remove(string pattern) {
         if (m_hunts.ContainsKey(pattern)) {
            MapHunt hunt = m_hunts[pattern];
            m_hunts.Remove(pattern);
            BindReset();
            if(Updated != null)
               Updated(hunt);
            if(DataChanged != null)
               DataChanged();
         }
      }

      public Dictionary<string, MapHunt>.Enumerator GetEnumerator() {
         return m_hunts.GetEnumerator();
      }

      /// <summary>Forcefully re-evaluates each spawn to determine its hunt status.</summary>
      public void BindReset() {
         foreach (KeyValuePair<uint, GameSpawn> pair in m_data.Engine.Game.Spawns)
            pair.Value.Hunt = isHunt(pair.Value);
      }

      /// <summary>Bind the hunt to the spawn collection. Additive only.</summary>
      public void Bind(MapHunt hunt) {
         foreach (KeyValuePair<uint, GameSpawn> pair in m_data.Engine.Game.Spawns) {
            if (hunt.isMatch(pair.Value))
               pair.Value.Hunt = true;
         }
      }

      /// <summary>Bind the spawn to the hunt collection. Additive only.</summary>
      public void Bind(GameSpawn spawn) {
         if (isHunt(spawn))
            spawn.Hunt = true;
      }

      /// <summary>Determine if ANY hunt applies to the specified spawn.</summary>
      public bool isHunt(GameSpawn spawn) {
         foreach (KeyValuePair<string, MapHunt> pair in m_hunts) {
            if (pair.Value.isMatch(spawn))
               return true;
         }
         return false;
      }

      /// <summary>Clears all hunts from the collection.</summary>
      public void Clear() {
         m_hunts.Clear();
         if(DataChanged != null)
            DataChanged();
      }

      /// <summary>Creates a deep copy of the hunt data.</summary>
      public MapHunts Clone() {
         MapHunts copy = (MapHunts)((ICloneable)this).Clone(); //Create shallow copy
         copy.m_hunts = new Dictionary<string, MapHunt>();
         foreach (KeyValuePair<string, MapHunt> pair in m_hunts) {
            copy.m_hunts.Add(pair.Key, pair.Value);
         }
         return copy;
      }
      object ICloneable.Clone() {
         return this.MemberwiseClone();
      }
   }

   /// <summary>Defines a regular expression used to bind to spawn based on its name.</summary>
   public class MapHunt {
      private Regex m_regex;
      private bool  m_permanent;

      public MapHunt(string pattern, bool permanent) {
         m_regex = new Regex(pattern);
         m_permanent = permanent;
      }

      /// <summary>Gets the regular expression object for this hunt.</summary>
      public Regex Hunt {
         get { return m_regex; }
      }

      /// <summary>Determines if the spawn matches this hunt.</summary>
      public bool isMatch(GameSpawn spawn) {
         /// IHM EDIT
         //if (spawn.Type == SpawnType.MOB || spawn.Type == SpawnType.Hidden)
            //If regex is A-F see if it matches only the ID. Does anyone hunt a mob by name with only one letter?!
            if (ToString() == "A" || ToString() == "B" || ToString() == "C" || ToString() == "D" ||
            ToString() == "E" || ToString() == "F")
                return m_regex.IsMatch(spawn.ID.ToString("X"));
            //if not search both the name or the ID (if it is an hex number it will never match the name and the other way around)
            else
                return m_regex.IsMatch(spawn.Name) || m_regex.IsMatch(spawn.ID.ToString("X"));
         /*else
            //If not a MOB or hidden spawn don't search based on A-F
            if (ToString() == "A" || ToString() == "B" || ToString() == "C" || ToString() == "D" ||
            ToString() == "E" || ToString() == "F")
                return false;
            //search players and NPCs based on name
            else
                return m_regex.IsMatch(spawn.Name);*/
         //return m_regex.IsMatch(spawn.Name);
      }

      /// <summary>Gets or sets whether the hunt will be saved with the map data.</summary>
      public bool Permanent {
         get { return m_permanent; }
      }

      /// <summary>Returns the regular expression pattern defined for this hunt.</summary>
      public override string ToString() {
          return m_regex.ToString();
      }
   }


   /// <summary>Defines a collection of regular expressions that are used to match against spawn names.</summary>
   public class MapReplacements : ICloneable
   {
       private Dictionary<string, MapReplacement> m_replacements;
       private MapData m_data;

       public event MapReplacementEvent Updated;
       public event GenericEvent DataChanged;

       public MapReplacements(MapData Data)
       {
           m_replacements = new Dictionary<string, MapReplacement>();
           m_data = Data;
       }

       /// <summary>Adds the pattern as a hunt.</summary>
       /// <param name="pattern">A regular expression to compare against the spawn name.</param>
       /// <param name="permanent">Determines if the hunt will be saved with the map data.</param>
       public MapReplacement Add(string pattern, string replacement, bool permanent)
       {
           if (pattern == "" || m_replacements.ContainsKey(pattern))
               return null;
           try
           {
               Regex.Match("", pattern);
           }
           catch (ArgumentException)
           {
               return null;
           }
           MapReplacement rep = new MapReplacement(pattern, replacement, permanent);
           m_replacements.Add(pattern, rep);
           Bind(rep);

           if (Updated != null)
               Updated(rep);
           if (DataChanged != null)
               DataChanged();

           return rep;
       }

       /// <summary>Removes the hunt from the list.</summary>
       public void Remove(MapReplacement replacement)
       {
           Remove(replacement.Replacement.ToString());
       }
       /// <summary>Removes the pattern from the hunt list.</summary>
       public void Remove(string pattern)
       {
           if (m_replacements.ContainsKey(pattern))
           {
               MapReplacement rep = m_replacements[pattern];
               m_replacements.Remove(pattern);
               BindReset();
               if (Updated != null)
                   Updated(rep);
               if (DataChanged != null)
                   DataChanged();
           }
       }

       public Dictionary<string, MapReplacement>.Enumerator GetEnumerator()
       {
           return m_replacements.GetEnumerator();
       }

       /// <summary>Forcefully re-evaluates each spawn to determine its hunt status.</summary>
       public void BindReset()
       {
           foreach (KeyValuePair<uint, GameSpawn> pair in m_data.Engine.Game.Spawns) {
               pair.Value.Replacement = isReplacement(pair.Value);
           }
       }

       /// <summary>Bind the hunt to the spawn collection. Additive only.</summary>
       public void Bind(MapReplacement replacement)
       {
           foreach (KeyValuePair<uint, GameSpawn> pair in m_data.Engine.Game.Spawns)
           {
               if (replacement.isMatch(pair.Value))
               {
                   pair.Value.Replacement = true;
                   pair.Value.RepName = replacement.ReplacedName;
               }
           }
       }

       /// <summary>Bind the spawn to the hunt collection. Additive only.</summary>
       public void Bind(GameSpawn spawn)
       {
           spawn.Replacement = isReplacement(spawn);
       }

       /// <summary>Determine if ANY hunt applies to the specified spawn.</summary>
       public bool isReplacement(GameSpawn spawn)
       {
           foreach (KeyValuePair<string, MapReplacement> pair in m_replacements)
           {
               if (pair.Value.isMatch(spawn))
               {
                   spawn.RepName = pair.Value.ReplacedName;
                   return true;
               }
           }
           spawn.RepName = "";
           return false;
       }

       /// <summary>Clears all hunts from the collection.</summary>
       public void Clear()
       {
           m_replacements.Clear();
           if (DataChanged != null)
               DataChanged();
       }

       /// <summary>Creates a deep copy of the hunt data.</summary>
       public MapReplacements Clone()
       {
           MapReplacements copy = (MapReplacements)((ICloneable)this).Clone(); //Create shallow copy
           copy.m_replacements = new Dictionary<string, MapReplacement>();
           foreach (KeyValuePair<string, MapReplacement> pair in m_replacements)
           {
               copy.m_replacements.Add(pair.Key, pair.Value);
           }
           return copy;
       }
       object ICloneable.Clone()
       {
           return this.MemberwiseClone();
       }
   }

   /// <summary>Defines a regular expression used to bind to spawn based on its name.</summary>
   public class MapReplacement
   {
       private Regex m_regex;
       private string m_replacement;
       private bool m_permanent;

       public MapReplacement(string pattern, string replacement, bool permanent)
       {
           m_regex = new Regex(pattern);
           m_replacement = replacement;
           m_permanent = permanent;
       }

       /// <summary>Gets the regular expression object for this hunt.</summary>
       public Regex Replacement
       {
           get { return m_regex; }
       }

       /// <summary>Determines if the spawn matches this hunt.</summary>
       public bool isMatch(GameSpawn spawn)
       {
           /// IHM EDIT
           //if (spawn.Type == SpawnType.MOB || spawn.Type == SpawnType.Hidden)
               //If regex is A-F see if it matches only the ID. Does anyone hunt a mob by name with only one letter?!
               if (ToString() == "A" || ToString() == "B" || ToString() == "C" || ToString() == "D" ||
               ToString() == "E" || ToString() == "F")
                   return m_regex.IsMatch(spawn.ID.ToString("X"));
               //if not search both the name or the ID (if it is an hex number it will never match the name and the other way around)
               else
                   return m_regex.IsMatch(spawn.Name) || m_regex.IsMatch(spawn.ID.ToString("X"));
           /*else
               //If not a MOB or hidden spawn don't search based on A-F
               if (ToString() == "A" || ToString() == "B" || ToString() == "C" || ToString() == "D" ||
               ToString() == "E" || ToString() == "F")
                   return false;
               //search players and NPCs based on name
               else
                   return m_regex.IsMatch(spawn.Name);*/
           //return m_regex.IsMatch(spawn.Name);
       }

       /// <summary>Gets or sets whether the hunt will be saved with the map data.</summary>
       public string ReplacedName
       {
           get { return m_replacement; }
       }

       /// <summary>Gets or sets whether the hunt will be saved with the map data.</summary>
       public bool Permanent
       {
           get { return m_permanent; }
       }

       /// <summary>Returns the regular expression pattern defined for this hunt.</summary>
       public override string ToString()
       {
           return m_regex.ToString();
       }
   }
}