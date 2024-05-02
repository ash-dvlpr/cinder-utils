using System;
using System.Reflection;
using UnityEngine;


namespace CinderUtils.Extensions {

    // Extensions for Types
    public static class TypeExtensions {

        public static bool Is<T>(this Type type) {
            Type baseType = typeof(T);
            return baseType.IsAssignableFrom(type);
        }

        public static bool IsMonoBehaviour(this Type t) {
            return t.Is<MonoBehaviour>();
        }
    }
}
