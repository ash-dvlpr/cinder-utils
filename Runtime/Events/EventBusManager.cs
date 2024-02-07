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
        internal static HashSet<Type> eventTypes;
        internal static HashSet<Type> eventBusTypes;

        public static IReadOnlyCollection<Type> EventTypes { get => eventTypes; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize() {
            eventTypes = AssemblyUtils.GetSubtypesOf<IEvent>(false);
            eventBusTypes = GetEventBusTypes();

            Debug.Log("CinderUtils.Events: EventBusManager initialized.");
        }

#if UNITY_EDITOR
        static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        static void InitializeEditor() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state) {
            PlayModeState = state;
            if (state == PlayModeStateChange.ExitingPlayMode) {
                ClearBuses();
            }
        }
#endif


        static HashSet<Type> GetEventBusTypes() {
            HashSet<Type> eventBusTypes = new();

            var baseBusType = typeof(EventBus<>);
            foreach (var eventType in eventTypes) {
                var busType = baseBusType.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
            }

            return eventBusTypes;
        }

        static void ClearBuses() {
            foreach (var busType in eventBusTypes) {
                var clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod?.Invoke(null, null);
            }

            //Debug.Log("CinderUtils.Events: EventBusManager: Event buses cleaned.");
        }
    }

}
