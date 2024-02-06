using System;
using System.Linq;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

using CinderUtils.Attributes;
using CinderUtils.Extensions;


namespace CinderUtils.Editor {

    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute), true)]
    public class ConditionalFieldAttributeDrawer : PropertyDrawer {
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
                shouldBeDrawn = condAttr.compareValues.Contains(targetValue.ToString());

                // 4. Invert the logic if it was configured like so on the attribute.
                shouldBeDrawn = condAttr.inverse ? !shouldBeDrawn : shouldBeDrawn;
            }

            // Return the hight (-2 hides it)
            if (!shouldBeDrawn) return -2;
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


        // Get the parent targetObject of some property
        internal object GetParent(SerializedProperty property) {
            // Sanitize the path for values inside of collections.
            var path = property.propertyPath.Replace(".Array.data[", "[");

            // Get the object to which the "field" (property) belongs
            object obj = property.serializedObject.targetObject;
            var elements = path.Split('.');

            // Find parent recursivelly
            foreach (var element in elements.Take(elements.Length - 1)) {
                // Handle collection indexing
                if (element.Contains("[")) {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[","").Replace("]",""));
                    obj = GetValue(obj, elementName, index);
                }
                else {
                    obj = GetValue(obj, element);
                }
            }

            return obj;
        }

        // Get value of a field/property
        //! Credit: https://discussions.unity.com/t/get-the-instance-the-serializedproperty-belongs-to-in-a-custompropertydrawer/66954/2
        internal object GetValue(object source, string name) {
            if (source == null) return null;

            // Get the type 
            var type = source.GetType();

            // Get field's value
            var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null) {
                return field.GetValue(source);
            }
            // Otherwise get property's value
            else {
                var property = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                
                if (property == null) return null;
                else return property.GetValue(source, null);
            }
        }

        // Get value of a field inside a collection
        internal object GetValue(object source, string name, int index) {
            var enumerable = GetValue(source, name) as IEnumerable;
            var enm = enumerable.GetEnumerator();
            while (index-- >= 0) { 
                enm.MoveNext();
            }
            return enm.Current;
        }
    }

}