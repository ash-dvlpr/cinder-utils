using System;
using UnityEditor;
using UnityEngine;


namespace CinderUtils.Events {
    public static class EventBusManagerEditor {

        static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        static void InitializeEditor() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state) {
            PlayModeState = state;
            if (state == PlayModeStateChange.ExitingPlayMode) {
                EventBusManager.ClearBuses();
            }
        }

    }
}
