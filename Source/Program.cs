using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Diagnostics;

namespace mappy {
   static class Program {
      public static ResourceManager ResGlobal;
      public static ResourceManager ResLocal;
      public static readonly string ConfigKey  = "SOFTWARE\\MappyXI";
      public static readonly string MapIniFile = "map.ini";
      public static readonly string MapFileExt = ".png|.gif";

      public static readonly string[] ProcessName = {"pol", "xiloader", "wingsloader", "edenxi"};
      public static readonly string ModuleName = "FFXiMain.dll";

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main() {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         //Bind embedded and satellite resouce files
         ResGlobal = new ResourceManager("mappy.Properties.Resources", Assembly.GetExecutingAssembly());
         ResLocal = new ResourceManager("mappy.Lang.app", Assembly.GetExecutingAssembly());
         MessageBoxEx.DoNotShowText = ResLocal.GetString("msg_donotshow");

         //Create the app driver and start a generic pump
         Controller controller = new Controller();
         Application.Run();
      }

      /// <summary>Gets the language specific text using the passed in key</summary>
      public static string GetLang(string name) {
         return ResLocal.GetString(name);
      }

#if DEBUG
      public static void DumpHex(byte[] buffer) {
         DumpHex(buffer, 16, 0, -1);
      }
      public static void DumpHex(byte[] buffer, int bytesperline) {
         DumpHex(buffer, bytesperline, 0, -1);
      }
      public static void DumpHex(byte[] buffer, int bytesperline, int startas, int maxlength) {
         StringBuilder line = null;
         StringBuilder linesub = null;

         int length = buffer.Length;
         if(maxlength > 0 && length > maxlength)
            length = maxlength;

         for(int i = 0; i < length; i++) {
            if((i % bytesperline) == 0) {
               if(line != null)
                  System.Diagnostics.Trace.WriteLine(line.ToString() + " " + linesub.ToString());
               line = new StringBuilder();
               linesub = new StringBuilder();
               line.AppendFormat("{0:X10}", startas + i);
            }
            line.AppendFormat(" {0:X2}", buffer[i]);
            if(buffer[i] > 31 && buffer[i] < 127) //Only display standard ascii values
               linesub.Append(System.Text.Encoding.ASCII.GetString(buffer, i, 1));
            else
               linesub.Append(".");
         }
         int mod = (bytesperline - (length % bytesperline));
         for(int i = 0; i < mod; i++) {
            line.Append("   ");
            linesub.Append(" ");
         }
         if(line != null)
            System.Diagnostics.Trace.WriteLine(line.ToString() + " " + linesub.ToString());
      }
#endif
   }
}
