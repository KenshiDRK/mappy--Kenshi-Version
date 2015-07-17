using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;

public abstract class Config {
   public abstract Config OpenKey(string name);
   public abstract Config OpenKey(string name, bool nocreate);
   public abstract void RemoveKey(string name);
   public abstract int KeyCount { get; }
   public abstract string[] GetKeyNames();

   public abstract void Load();
   public abstract void Save();
   public abstract void Suspend();
   public abstract void Resume();

   public abstract object this[string key] { get; set; }
   public abstract TType GetValue<TType>(string key);
   public abstract TType GetValue<TType>(string key, TType defaultvalue);
   public abstract TType Get<TType>(string key, TType defaultvalue);
   public abstract void Set<TType>(string key, TType value);
   public abstract bool Get(string key, bool defaultvalue);
   public abstract int Get(string key, int defaultvalue);
   public abstract float Get(string key, float defaultvalue);
   public abstract double Get(string key, double defaultvalue);
   public abstract string Get(string key, string defaultvalue);
   public abstract void Set(string key, bool value);
   public abstract int Count { get; }
   public abstract bool Exists(string name);
   public abstract void Remove(string name);
   public abstract bool Dirty { get; }
   public abstract Dictionary<string, object>.Enumerator GetEnumerator();
}

public class RegistryConfig : Config {
   private RegistryKey m_key = null;
   private Dictionary<string, object> m_config = new Dictionary<string,object>();
   private bool m_suspended = false;
   private bool m_dirty = false;

   public RegistryConfig(string key) : this(key, null) {}
   public RegistryConfig(string key, Dictionary<string, object>values) {
      try {
         //Open the configuration key
         this.m_key = Registry.CurrentUser.OpenSubKey(key, true);
         if (this.m_key == null)
            this.m_key = Registry.CurrentUser.CreateSubKey(key);

         if(values != null)
            CopyValues(values);
         Load();
#if DEBUG
      } catch (Exception ex) {
         System.Diagnostics.Trace.WriteLine("INIT: error opening registry key: " + ex.Message);
      }
#else            
      } catch {}
#endif
   }
   public RegistryConfig(RegistryKey key) : this(key, null) {}
   public RegistryConfig(RegistryKey key, Dictionary<string, object>values) {
      this.m_key = key;
      if (this.m_key != null) {
         if(values != null)
            CopyValues(values);
         Load();
      }
   }
   private void CopyValues(Dictionary<string, object> values) {
      foreach(KeyValuePair<string, object> pair in values)
         m_config[pair.Key] = pair.Value;
   }
   public override Config OpenKey(string name) {
      return OpenKey(name, false);
   }
   public override Config OpenKey(string name, bool nocreate) {
      try {
         RegistryKey subkey = m_key.OpenSubKey(name, true);
         if(subkey == null && !nocreate)
            subkey = m_key.CreateSubKey(name);
         return new RegistryConfig(subkey);
#if DEBUG
      } catch(Exception ex) {
         System.Diagnostics.Trace.WriteLine("OpenKey: " + ex.Message);
      }
#else         
      } catch {}
#endif
      return null;
   }
   public override void RemoveKey(string name) {
      m_key.DeleteSubKey(name);
   }
   public override int KeyCount {
      get { return m_key.SubKeyCount; }
   }
   public override string[] GetKeyNames() {
      return m_key.GetSubKeyNames();
   }
   public override bool Exists(string name) {
      return m_config.ContainsKey(name);
   }
   public override void Remove(string name) {
      try {
         if(m_config.ContainsKey(name)) {
            m_config.Remove(name);
            m_key.DeleteValue(name);
         }
      } catch { }
   }
   public override void Load() {
      if(m_key.ValueCount > 0) {
         foreach(string name in m_key.GetValueNames())
            m_config[name] = m_key.GetValue(name);
      }
   }
   public override void Save() {
      if(m_config.Count > 0) {
         foreach(KeyValuePair<string, object> item in m_config) {
            if(item.Value is bool)
               m_key.SetValue(item.Key, Convert.ToInt32(item.Value));
            else
               m_key.SetValue(item.Key, item.Value);
         }
      }
   }
   public override object this[string key] {
      get {
         try {
            if(m_config.ContainsKey(key))
               return m_config[key];
         } catch {}
         return null;
      }
      set {
         if (!m_suspended) {
            m_config[key] = value;
            m_dirty = true;
         }
      }
   }
   public override TType GetValue<TType>(string key) {
      return GetValue<TType>(key, default(TType));
   }
   public override TType GetValue<TType>(string key, TType defaultvalue) {
      try {
         if (m_config.ContainsKey(key))
            return (TType)m_config[key];
         return defaultvalue;
      } catch(NullReferenceException) {
         return defaultvalue;
      } catch(InvalidCastException) {
         #if DEBUG
            System.Diagnostics.Trace.WriteLine("WARNING: Invalid Cast for option key '" + key + "'");
         #endif
         return defaultvalue;
      }
   }

   public override bool Get(string key, bool defaultvalue) {
      try {
         if(m_config.ContainsKey(key))
            return ((int)m_config[key]) != 0;
         return defaultvalue;
      } catch {
         return defaultvalue;
      }
   }

   public override int Get(string key, int defaultvalue) {
      try {
         if(m_config.ContainsKey(key))
            return int.Parse(m_config[key].ToString(), CultureInfo.InvariantCulture);
         return defaultvalue;
      } catch {
         return defaultvalue;
      }
   }

   public override float Get(string key, float defaultvalue) {
      try {
         if(m_config.ContainsKey(key))
            return float.Parse(m_config[key].ToString(), CultureInfo.InvariantCulture);
         return defaultvalue;
      } catch {
         return defaultvalue;
      }
   }

   public override double Get(string key, double defaultvalue) {
      try {
         if(m_config.ContainsKey(key))
            return double.Parse(m_config[key].ToString(), CultureInfo.InvariantCulture);
         return defaultvalue;
      } catch {
         return defaultvalue;
      }
   }


   public override string Get(string key, string defaultvalue) {
      try {
         if(m_config.ContainsKey(key))
            return m_config[key].ToString();
         return defaultvalue;
      } catch {
         return defaultvalue;
      }
   }

   public override Dictionary<string, object>.Enumerator GetEnumerator() {
      return m_config.GetEnumerator();
   }
   public override void Suspend() {
      m_suspended = true;
   }
   public override void Resume() {
      m_suspended = false;
   }
   public override int Count {
      get { return m_config.Count; }
   }
   public override bool Dirty {
      get { return m_dirty; }
   }

   public override TType Get<TType>(string key, TType defaultvalue) {
      try {
         if (m_config.ContainsKey(key)) {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(TType));
            return (TType)tc.ConvertFromString((string)m_config[key]);
         }
         return defaultvalue;
      } catch {
         return defaultvalue;
      }
   }

   public override void Set<TType>(string key, TType value) {
      try {
         TypeConverter tc = TypeDescriptor.GetConverter(typeof(TType));
         m_config[key] = tc.ConvertToString(value);
      } catch {}
   }

   public override void Set(string key, bool value) {
      m_config[key] = Convert.ToInt32(value);
   }
}