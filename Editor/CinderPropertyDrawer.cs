using System;
using System.Linq;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

using CinderUtils.Attributes;
using CinderUtils.Extensions;


namespace CinderUtils.Editor {
    public abstract class CinderPropertyDrawer : PropertyDrawer {

        //! Credit for the reflection based GetParent and GetValue logic.
        //! https://discussions.unity.com/t/get-the-instance-the-serializedproperty-belongs-to-in-a-custompropertydrawer/66954/2

        // Gets the parent targetObject of some property
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