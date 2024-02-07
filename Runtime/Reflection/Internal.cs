using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Reflection {

    public static partial class AssemblyUtils {
        internal static Dictionary<AssemblyType, Assembly> assemblyCache;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        internal static void Initialize() {
            assemblyCache = GetAssemblies();
            Debug.Log("CinderUtils.Reflection: AssemblyUtils initialized.");
        }

        internal static AssemblyType? GetAssemblyType(string assemblyName) {
            return assemblyName switch {
                "Assembly-CSharp" => AssemblyType.Assembly_CSharp,
                "Assembly-CSharp-Editor" => AssemblyType.Assembly_CSharpEditor,
                "Assembly-CSharp-firstpass" => AssemblyType.Assembly_CSharp_FirstPass,
                "Assembly-CSharp-Editor-firstpass" => AssemblyType.Assembly_CSharpEditor_FirstPass,
                _ => null
            };
        }

        internal static bool TryGetAssemblyType(string assemblyName, out AssemblyType? type) {
            type = GetAssemblyType(assemblyName);
            return type != null && type != AssemblyType.Unknown;
        }

        // Returns a collection of Types per AssemblyType.
        internal static Dictionary<AssemblyType, Assembly> GetAssemblies() {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<AssemblyType, Assembly> assemblyCache = new();

            // Extract all the types from the defined assemblies (AssemblyType)
            foreach (Assembly asm in assemblies) {
                if (TryGetAssemblyType(asm.GetName().Name, out var asmType)) {
                    assemblyCache.Add((AssemblyType) asmType, asm);
                }
            }

            return assemblyCache;
        }

        internal static List<Type> GetSubtypesInAssemblyOf<T>(Assembly assembly) {
            if (assembly == null) return null;

            var parentType = typeof(T);
            List<Type> subtypes = new();

            foreach (Type type in assembly.GetTypes()) {
                // Could variable for type "parentType" get assigned a value of type "type"?
                // If so, the type is a subtype
                if (parentType.IsAssignableFrom(type)) {
                    //Debug.Log($"CinderUtils: AssemblyUtils: Found Subtype of '{parentType}': '{type.Name}' in Assembly '{assembly.GetName().Name}'.");
                    subtypes.Add(type);
                }
            }

            return subtypes;
        }
    }

}