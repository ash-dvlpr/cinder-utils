using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using CinderUtils.Extensions;
using CinderUtils.Reflection;


namespace CinderUtils.Events {

    public static class EventBusManager {
        internal static List<Type> eventTypes;
        internal static List<Type> eventBusTypes;

        public static IReadOnlyList<Type> EventTypes { get => eventTypes.AsReadOnly(); }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize() {
            eventTypes = AssemblyUtils.GetSubtypesOf<IEvent>();
            eventBusTypes = GetEventBusTypes();

            Debug.Log("CinderUtils.Events: EventBusManager initialized.");
        }

#if UNITY_EDITOR
        internal static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        internal static void InitializeEditor() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        internal static void OnPlayModeStateChanged(PlayModeStateChange state) {
            PlayModeState = state;
            if (state == PlayModeStateChange.ExitingPlayMode) {
                ClearBuses();
            }
        }
#endif


        static List<Type> GetEventBusTypes() {
            List<Type> eventBusTypes = new();

            var baseBusType = typeof(EventBus<>);
            foreach (var eventType in eventTypes) {
                var busType = baseBusType.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
            }

            return eventBusTypes;
        }

        public static void ClearBuses() {
            foreach (var busType in eventBusTypes) {
                var clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod?.Invoke(null, null);
            }
        }
    }

}
