using System;
using UnityEngine;
using UnityEditor;

using CinderUtils.Attributes;


namespace CinderUtils.Editor {

    [CustomPropertyDrawer(typeof(DisabledOnPlayAttribute))]
    public class DisabledOnPlayDrawer : PropertyDrawer {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var playing = Application.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode;
            GUI.enabled = !playing;

            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

}

