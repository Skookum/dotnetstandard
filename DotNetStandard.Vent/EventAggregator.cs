using System;
using System.Collections.Generic;

namespace DotNetStandard.Vent
{
    public sealed class EventAggregator
    {
        private readonly Dictionary<Event, HashSet<Action<dynamic>>> _ventMap;

        public void Subscribe(Event vent, Action<dynamic> callback)
        {
            if (_ventMap.ContainsKey(vent))
                _ventMap[vent].Add(callback);
            else
                _ventMap.Add(vent, new HashSet<Action<dynamic>> {callback});
        }

        public void Unsubscribe(Event vent, Action<dynamic> callback)
        {
            if (!_ventMap.ContainsKey(vent))
                return;
            _ventMap[vent].Remove(callback);
        }

        public void Trigger(Event vent, dynamic param)
        {
            if (!_ventMap.ContainsKey(vent))
                return;

            foreach (Action<dynamic> action in _ventMap[vent])
                action.Invoke(param);
        }

        private EventAggregator()
        {
            _ventMap = new Dictionary<Event, HashSet<Action<dynamic>>>();
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