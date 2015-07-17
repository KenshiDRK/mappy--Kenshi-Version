using System;
using System.Collections.Generic;

namespace mappy {
   /// <summary>Defines a collection of event handlers that may be fired in response to predefined actions.</summary>
   public class Actions<TKey> {
      /// <summary>Provides data when an action is fired.</summary>
      public class ActionEventArgs : EventArgs {
         private Action m_action;
         private object m_state;
         private bool   m_toggled;

         public ActionEventArgs(Action action) {
            m_action = action;
         }
         public ActionEventArgs(Action action, object state, bool toggled) : this(action) {
            m_state = state;
            m_toggled = toggled;
         }
         /// <summary>Gets the action associated with this event.</summary>
         public Action Action {
            get { return m_action; }
         }
         /// <summary>Gets the state object.</summary>
         public object State {
            get { return m_state; }
         }
         /// <summary>Gets whether the key is acknowledged to be in the toggled state.</summary>
         public bool Toggled {
            get { return m_toggled; }
         }
      }
      public delegate bool ActionHandlerDelegate(ActionEventArgs e);

      /// <summary>Defines an action handler that is registered by the host application.</summary>
      public class Action : ICloneable {
         private string m_name;
         private bool   m_toggle;
         private object m_state;
         private string m_description;
         private ActionHandlerDelegate m_handler;
         private TKey   m_trigger;
         private TKey   m_default;
         private Actions<TKey> m_parent;

         public Action(Actions<TKey> parent, string name, string description, TKey trigger, ActionHandlerDelegate handler) : this(parent, name, description, trigger, handler, null) {}
         public Action(Actions<TKey> parent, string name, string description, TKey trigger, ActionHandlerDelegate handler, object state) {
            m_parent = parent;
            m_name = name;
            m_description = description;
            m_handler = handler;
            m_state = state;
            m_toggle = false;
            m_trigger = trigger;
            m_default = trigger;
            m_parent.Bind(this, trigger);
         }
         /// <summary>Gets the action list associated with this action.</summary>
         public Actions<TKey> Actions {
            get { return m_parent; }
         }
         /// <summary>Gets the name of the action.</summary>
         public string Name {
            get { return m_name; }
         }
         /// <summary>Gets the description of the action.</summary>
         public string Description {
            get { return m_description; }
         }
         /// <summary>Gets or sets the toggle state of this action. The registering app must set this flag explicity it is not toggled automatically.</summary>
         public bool Toggled {
            get { return m_toggle; }
            set { m_toggle = value; }
         }
         /// <summary>Gets or sets the trigger associated with this action.</summary>
         public TKey Trigger {
            get { return m_trigger; }
            set {
               m_trigger = value;
               m_parent.Bind(this, value);
            }
         }
         internal void SetTrigger(TKey trigger) {
            m_trigger = trigger;
         }
         /// <summary>Gets the default trigger associated with this action.</summary>
         public TKey DefaultTrigger {
            get { return m_default; }
         }
         /// <summary>Gets or sets the state object that is passed to the fired event handler.</summary>
         public object State {
            get { return m_state; }
            set { m_state = value; }
         }
         /// <summary>Fires the event handler.</summary>
         public bool Fire() {
            return m_handler(new ActionEventArgs(this, m_state, m_toggle));
         }
         /// <summary>Fires the event handler with the given toggle state.</summary>
         public bool Fire(bool Toggled) {
            m_toggle = Toggled;
            return Fire();
         }
         /// <summary>Gets the friendly name of this action.</summary>
         public override string ToString() {
            return m_description;
         }

         /// <summary>Unbinds the action from the associated trigger.</summary>
         public void Unbind() {
            m_trigger = default(TKey);
         }

         /// <summary>Clones the action.</summary>
         public Action Clone() {
            return (Action)((ICloneable)this).Clone();
         }

         object ICloneable.Clone() {
            return this.MemberwiseClone();
         }
      }
      private Dictionary<string, Action> m_actionlist;

      public Actions() {
         m_actionlist = new Dictionary<string, Action>();
      }
      /// <summary>Gets an action based on its id.</summary>
      public Action this[string key] {
         get {
            if (m_actionlist.ContainsKey(key))
               return m_actionlist[key];
            return default(Action);
         }
      }
      /// <summary>Gets the number of actions registered on the system.</summary>
      public int Count {
         get { return m_actionlist.Count; }
      }
      public Dictionary<string, Action>.Enumerator GetEnumerator() {
         return m_actionlist.GetEnumerator();
      }
      /// <summary>Determines if the action id has been registered on the system.</summary>
      public bool ContainsAction(string key) {
         return m_actionlist.ContainsKey(key);
      }
      /// <summary>Registers an event handler with the action list.</summary>
      public void RegisterAction(string name, string description, ActionHandlerDelegate handler, TKey trigger) {
         RegisterAction(name, description, handler, trigger, null);
      }
      /// <summary>Registers an event handler with the action list.</summary>
      public void RegisterAction(string name, string description, ActionHandlerDelegate handler, TKey trigger, object state) {
         if (!m_actionlist.ContainsKey(name)) {
            Action action = new Action(this, name, description, trigger, handler, state);
            m_actionlist[name] = action;
         }
      }
      /// <summary>Binds a trigger to an action.</summary>
      public void Bind(Action action, TKey trigger) {
         //if any action is already bound to the key then unbind it immediatly
         foreach(KeyValuePair<string, Action> pair in m_actionlist) {
            if(pair.Value.Trigger.Equals(trigger))
               pair.Value.Unbind();
         }
         action.SetTrigger(trigger);
      }
      /// <summary>Unbinds an action</summary>
      public void Unbind(Action action) {
         action.Unbind();
      }
      /// <summary>Unbinds all actions</summary>
      public void UnbindAll() {
         foreach(KeyValuePair<string, Action> pair in m_actionlist)
            pair.Value.Unbind();
      }
      /// <summary>Determines if the specified trigger is bound to an action.</summary>
      public bool Bound(TKey trigger) {
         foreach(KeyValuePair<string, Action> pair in m_actionlist) {
            if(pair.Value.Trigger.Equals(trigger))
               return true;
         }
         return false;
      }
      /// <summary>Fires the event handler with the specified name.</summary>
      public bool FireAction(string name, bool toggled) {
         if (m_actionlist.ContainsKey(name))
            return m_actionlist[name].Fire(toggled);
         return false;
      }
      /// <summary>Fires the action currently associated with the trigger.</summary>
      public bool Fire(TKey trigger) {
         foreach(KeyValuePair<string, Action> pair in m_actionlist) {
            if(pair.Value.Trigger.Equals(trigger))
               return pair.Value.Fire();
         }
         return false;
      }
      /// <summary>Fires the action currently associated with the trigger.</summary>
      public bool Fire(TKey trigger, bool toggled) {
         foreach(KeyValuePair<string, Action> pair in m_actionlist) {
            if(pair.Value.Trigger.Equals(trigger))
               return pair.Value.Fire(toggled);
         }
         return false;
      }
   }
}