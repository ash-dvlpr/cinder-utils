using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CinderUtils.Reflection {

    public static partial class AssemblyUtils {
        /// <summary>
        /// Returns all subtypes of some Type <paramref name="T"/> found inside the Application's Assemblies.
        /// </summary>
        /// 
        /// <typeparam name="T">Some generic Type</typeparam>
        public static IEnumerable<Type> GetSubtypesOf<T>() {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(asm => GetSubtypesInAssemblyOf<T>(asm));
        }

        /// <summary>
        /// Returns all Types that have the custom attribute <paramref name="TAttr"/> found inside the Application's Assemblies.
        /// </summary>
        /// 
        /// <typeparam name="TAttr">Some Custom <see cref="System.Attribute">Attribute</see></typeparam>
        public static IEnumerable<Type> GetTypesWithCustomAttribute<TAttr>() where TAttr : Attribute {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(asm => GetTypesInAssemblyWithAttribute<TAttr>(asm));
        }
    }
}
