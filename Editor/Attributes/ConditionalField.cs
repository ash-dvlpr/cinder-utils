using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

using CinderUtils.Attributes;
using CinderUtils.Extensions;


namespace CinderUtils.Editor {

    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute), true)]
    public class ConditionalFieldAttributeDrawer : CinderPropertyDrawer {
        private bool shouldBeDrawn = true;

        /// <param name="property">The serialized value of the "field" that is being drawn.</param>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            // Don't do anything if it's not our custom class, or if IsSet is false (missing requirements)
            if (!( attribute is ConditionalFieldAttribute condAttr ) || !condAttr.IsSet) return EditorGUI.GetPropertyHeight(property);

            // 1. Get the "fieldToCheck" field (SerializedProperty)
            var targetValue = TryGetRelativeFieldValue(property, condAttr.targetFieldName);
            shouldBeDrawn = false;

            // 2. If the field was found
            if (targetValue != null) {
                // 3. Check if the field's value matches with one of the provided "compareValues".
                shouldBeDrawn = condAttr.compareValues.Contains(targetValue);

                // 4. Invert the logic if it was configured like so on the attribute.
                shouldBeDrawn = condAttr.inverse ? !shouldBeDrawn : shouldBeDrawn;
            }

            // Return the height the drawer should have, negative one hides it.
            if (!shouldBeDrawn) return -EditorGUIUtility.standardVerticalSpacing;
            else return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (!shouldBeDrawn) return;

            EditorGUI.PropertyField(position, property, label, true);
        }

        internal object TryGetRelativeFieldValue(SerializedProperty relativeOrigin, string fieldName) {
            // TODO: Improve algorithim. Make recursive, if not found in the parent level, go to it's parent recursivelly.
            
            // Get the parent object (component/array)
            var parent = GetParent(relativeOrigin);
            var child = GetValue(parent, fieldName);

            return child;
        }
    }

}