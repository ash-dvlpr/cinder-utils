using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

namespace CinderUtils.Reflection {

    public static partial class AssemblyUtils {
        /// <summary>
        /// Easily accesible cache with references to <see cref="PredefinedAssembly">Unity's Predefined Assemblies</see>.
        /// </summary>
        public static IReadOnlyDictionary<PredefinedAssembly, Assembly> PredefinedAssemblyCache {
            get => predefinedAssemblyCache;
        }
        
        /// <summary>
        /// Enum that represents <see href="https://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html">Unity's Predefined Assemblies</see>.
        /// </summary>
        public enum PredefinedAssembly : byte {
            Unknown = 0,
            Assembly_CSharp_FirstPass           = 1, // Assembly-CSharp-firstpass
            Assembly_CSharpEditor_FirstPass     = 2, // Assembly-CSharp-Editor-firstpass
            Assembly_CSharp                     = 3, // Assembly-CSharp
            Assembly_CSharpEditor               = 4, // Assembly-CSharp-Editor
        }

        /// <summary>
        /// Returns all subtypes of some Type <paramref name="T"/> found inside the Application's Assemblies.
        /// </summary>
        /// 
        /// <typeparam name="T">Some generic Type</typeparam>
        /// <param name="searchPredefinedAssemblies">Whether or not to search only inside Unity's PredefinedAssemblies.</param>
        /// <param name="targetAssemblies">What <see cref="PredefinedAssembly">Predefined Assemblies</see> to seach.</param>
        /// <returns></returns>
        public static HashSet<Type> GetSubtypesOf<T>(bool searchPredefinedAssemblies = true, ICollection<PredefinedAssembly> targetAssemblies = null) {
            IEnumerable<Assembly> assemblies;

            // Select the assemblies to search
            if (searchPredefinedAssemblies) {
                // Place in the default ones if no target assemblies were specified
                if (targetAssemblies.NullOrEmpty()) {
                    targetAssemblies = new List<PredefinedAssembly>() {
                        PredefinedAssembly.Assembly_CSharp,
                        PredefinedAssembly.Assembly_CSharpEditor,
                    };
                }

                // Filter the PredefinedAssemblies
                assemblies = targetAssemblies.Where(
                    t => PredefinedAssemblyCache.ContainsKey(t)
                ).Select(t => PredefinedAssemblyCache[t]);
            }
            else {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            // Search
            return GetSubtypesInAssemblyOf<T>(assemblies);
        }
    }
}
