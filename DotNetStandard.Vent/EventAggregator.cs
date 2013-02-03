using System;
using System.Collections.Generic;

namespace DotNetStandard.Vent
{
    public sealed class EventAggregator
    {
        private readonly Dictionary<Event, HashSet<Action>> _ventMap;

        public void Subscribe(Event vent, Action callback)
        {
            if (_ventMap.ContainsKey(vent))
                _ventMap[vent].Add(callback);
            else
                _ventMap.Add(vent, new HashSet<Action> {callback});
        }

        public void Unsubscribe(Event vent, Action callback)
        {
            if (!_ventMap.ContainsKey(vent))
                return;
            _ventMap[vent].Remove(callback);
        }

        public void Trigger(Event vent)
        {
            if (!_ventMap.ContainsKey(vent))
                return;

            foreach (Action action in _ventMap[vent])
                action.Invoke();
        }

        private EventAggregator()
        {
            _ventMap = new Dictionary<Event, HashSet<Action>>();
        }

        public static EventAggregator Instance
        {
            get
            {
                return Nested.Instance;
            }
        }        

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() {}    
            internal static readonly EventAggregator Instance = new EventAggregator();
        }
    }
}