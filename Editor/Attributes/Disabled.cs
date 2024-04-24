using System;
using UnityEngine;
using UnityEditor;

using CinderUtils.Attributes;


namespace CinderUtils.Editor {

    [CustomPropertyDrawer(typeof(DisabledAttribute), true)]
    public class DisabledDrawer : CinderPropertyDrawer {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool prevStatus = GUI.enabled;
            GUI.enabled = IsEnabled;

            // TODO: Debug why the property looses it's target.
            EditorGUI.PropertyField(position, property, label, false);
            GUI.enabled = prevStatus;
        }

        public virtual bool IsEnabled { get => false; }
    }

}