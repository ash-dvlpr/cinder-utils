using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;
using System.Linq;

namespace CinderUtils.Reflection {
    public static partial class AssemblyUtils {

        internal static IEnumerable<Type> GetSubtypesInAssemblyOf<T>(Assembly assembly) {
            return assembly.GetTypes().Where(t => {
#if CINDER_DEBUG
                if (t.Is<T>()) 
                    CinderDebug.Log($"CinderUtils.AssemblyUtils: Found Subtype of '{typeof(T)}': '{t.Name}' in Assembly '{assembly.GetName().Name}'.");
#endif

                return t.Is<T>();
            });
        }

        internal static IEnumerable<Type> GetTypesInAssemblyWithAttribute<TAttr>(Assembly assembly) where TAttr : Attribute {
            return assembly.GetTypes().Where(t => {
#if CINDER_DEBUG
                if (t.Is<T>()) 
                    CinderDebug.Log($"CinderUtils.AssemblyUtils: Found Type with Attribute '{nameof(TAttr)}': '{type.Name}' in Assembly '{assembly.GetName().Name}'.");
#endif

                return t.HasAttribute<TAttr>();
            });
        }
    }
}