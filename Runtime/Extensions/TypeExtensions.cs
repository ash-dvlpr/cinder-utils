using System;
using System.Reflection;


namespace CinderUtils.Extensions {

    // Extensions for Types
    public static class TypeExtensions {

        public static bool Is<T>(this Type type) {
            Type baseType = typeof(T);
            return baseType.IsAssignableFrom(type);
        }
    }

}
