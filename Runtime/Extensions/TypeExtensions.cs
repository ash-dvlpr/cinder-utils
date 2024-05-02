using System;
using System.Reflection;
using UnityEngine;


namespace CinderUtils.Extensions {

    // Extensions for Types
    public static class TypeExtensions {

        public static bool Is<T>(this Type type) {
            Type baseType = typeof(T);
            
            // Could a variable of type "parentType" get assigned a value of the type "type"?
            // If so, the type is a subtype
            return baseType.IsAssignableFrom(type);
        }

        public static bool HasAttribute<TAttr>(this Type type) where TAttr : Attribute {
            return type.GetCustomAttribute<TAttr>() != null;
        }

        public static bool IsMonoBehaviour(this Type t) {
            return t.Is<MonoBehaviour>();
        }
    }
}
