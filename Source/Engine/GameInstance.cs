using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using MapEngine;

namespace MapEngine {
   public class GameInstance {
      internal Process       process; //These variables may be accessed directly by subclasses instead
      internal Engine        engine;  // of a property getter in order to maintain performance standards.
      internal Config        config;
      internal MemoryReader  reader;
      private  bool          m_valid = false;
      private  bool          m_switching = false;
      private  string        m_module = "";

      public GameInstance(Engine engine, Config config, string moduleName) {
         this.engine = engine;
         this.config = config;
         this.m_module = moduleName;
         //note that process loading is done either after the instance is constructed, or during the subclass construction
      }
      
      /// <summary>Gets or sets the process to bind to  to update the game state from.</summary>
      public Process Process {
         get { return process; }
         set {
            if (process == value)
               return;

            m_switching = true;
            m_valid = false;
            process = value;

            //if this instance was already handling another process then close the reader down
            if (reader != null) {
               reader.Close();
               reader = null;
               engine.Clear();
            }

            //only start the reader if a valid process was used
            if (process != null) {
               reader = MemoryReader.Open(process, m_module);
               if (reader == null)
                  throw new MemoryReaderException("The reader failed to initialize.");
               OnInitializeProcess(); //initialize is responsible for setting valid flag!
            }

            m_switching = false;
         }
      }

      /// <summary>Gets the memory reader for the game instance.</summary>
      public MemoryReader Reader {
         get { return reader; }
      }
      /// <summary>Gets the engine associated with this instance.</summary>
      public Engine Engine {
         get { return engine; }
      }
      /// <summary>Gets the configuration object associated with this instance.</summary>
      internal Config Config {
         get { return config; }
      }
      /// <summary>Gets if the process is currently switching to a new context. During the transition, process operations must be paused.</summary>
      internal bool Switching {
         get { return m_switching; }
      }
      /// <summary>Gets whether the instance has been successfully initialized or not.</summary>
      public bool Valid {
         get { return m_valid; }
         internal set { m_valid = value; } //valid state must be set during initialization, prior to polling!
      }

      //these functions must be overridden by the subclass.
      /// <summary>Called when a new process is loaded for the instance.</summary>
      protected virtual void OnInitializeProcess() {
         throw new Exception("Not implemented yet.");
      }
      /// <summary>Polls the game for status updates and informs the engine accordingly.</summary>
      public virtual bool Poll() { 
         throw new Exception("Not implemented yet.");
      }
   }
}

/// <summary>Custom exception class for catch filtering.</summary>
public sealed class InstanceException : Exception {
   InstanceExceptionType m_type;
   public InstanceException(string message, InstanceExceptionType type) : base(message) { m_type = type; }
   public InstanceException(string message, InstanceExceptionType type, Exception innerException) : base(message, innerException) { m_type = type; }
   public InstanceExceptionType ExceptionType { get { return m_type; } }
}

public enum InstanceExceptionType : int {
   SigFailure = 1,
   InvalidContext = 2
}