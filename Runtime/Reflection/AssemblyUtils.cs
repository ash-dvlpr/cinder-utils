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
        public static IReadOnlyDictionary<PredefinedAssembly, Assembly> PredefinedAssemblyCache {
            get => predefinedAssemblyCache;
        }

        // Predefined Assemblies: https://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html 
        public enum PredefinedAssembly : byte {
            Unknown = 0,
            Assembly_CSharp_FirstPass           = 1, // Assembly-CSharp-firstpass
            Assembly_CSharpEditor_FirstPass     = 2, // Assembly-CSharp-Editor-firstpass
            Assembly_CSharp                     = 3, // Assembly-CSharp
            Assembly_CSharpEditor               = 4, // Assembly-CSharp-Editor
        }

        // Returns all the subtypes of some Type T inside the target AssemblyTypes.
        // By default will only look in the Assembly_CSharp, and Assembly_CSharpEditor assemblies.
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
