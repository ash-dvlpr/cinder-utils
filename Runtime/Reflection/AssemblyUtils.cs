using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Reflection {

    public static partial class AssemblyUtils {
        public static IReadOnlyDictionary<AssemblyType, Assembly> AssemblyCache {
            get => assemblyCache;
        }

        // Predefined Assemblies: https://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html 
        public enum AssemblyType : byte {
            Unknown = 0,
            Assembly_CSharp_FirstPass           = 1, // Assembly-CSharp-firstpass
            Assembly_CSharpEditor_FirstPass     = 2, // Assembly-CSharp-Editor-firstpass
            Assembly_CSharp                     = 3, // Assembly-CSharp
            Assembly_CSharpEditor               = 4, // Assembly-CSharp-Editor
        }

        // Returns all the subtypes of some Type T inside the target AssemblyTypes.
        // By default will only look in the Assembly_CSharp, and Assembly_CSharpEditor assemblies.
        public static List<Type> GetSubtypesOf<T>(ICollection<AssemblyType> targetAssemblies = null) {
            if (targetAssemblies.NullOrEmpty()) {
                targetAssemblies = new List<AssemblyType>() {
                    AssemblyType.Assembly_CSharp,
                    AssemblyType.Assembly_CSharpEditor,
                };
            }

            List<Type> subtypes = new();
            foreach (var target in targetAssemblies) {
                if (AssemblyCache.ContainsKey(target)) { 
                    subtypes.AddRange(GetSubtypesInAssemblyOf<T>(AssemblyCache[target]));
                }
            }

            return subtypes;
        }
    }

}
