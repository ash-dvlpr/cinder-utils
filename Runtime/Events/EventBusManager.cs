using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;
using CinderUtils.Reflection;


namespace CinderUtils.Events {

    public static class EventBusManager {
        internal static HashSet<Type> eventTypes = new();
        internal static HashSet<Type> eventBusTypes = new();

        public static IReadOnlyCollection<Type> EventTypes { get => eventTypes; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize() {
            GetEventTypes();
            GetEventBusTypes();

            CinderDebug.Log("CinderUtils: EventBus: Initialized.");
        }

        static void GetEventTypes() { 
            AssemblyUtils.GetSubtypesOf<IEvent>().ForEach(t => eventTypes.Add(t));
        }

        static void GetEventBusTypes() {
            var baseBusType = typeof(EventBus<>);

            foreach (var eventType in EventTypes) {
                var busType = baseBusType.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
            }
        }

        public static void ClearBuses() {
            foreach (var busType in eventBusTypes) {
                var clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod?.Invoke(null, null);
            }

            CinderDebug.Log("CinderUtils: EventBus: Buses reset.");
        }
    }

}
