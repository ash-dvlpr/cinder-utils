using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;
using System.Linq;

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
        public static HashSet<Type> GetSubtypesOf<T>(ICollection<PredefinedAssembly> targetAssemblies = null) {
            if (targetAssemblies.NullOrEmpty()) {
                targetAssemblies = new List<PredefinedAssembly>() {
                    PredefinedAssembly.Assembly_CSharp,
                    PredefinedAssembly.Assembly_CSharpEditor,
                };
            }

            HashSet<Type> subtypes = new();
            foreach (var target in targetAssemblies) {
                if (PredefinedAssemblyCache.ContainsKey(target)) {
                    GetSubtypesInAssemblyOf<T>(PredefinedAssemblyCache[target]).ForEach(type => subtypes.Add(type));
                }
            }

            return subtypes;
        }
    }
}
