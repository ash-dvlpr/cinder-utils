using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Reflection {

    public static partial class AssemblyUtils {
        internal static Dictionary<PredefinedAssembly, Assembly> predefinedAssemblyCache;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        internal static void Initialize() {
            predefinedAssemblyCache = GetPredefinedAssemblies();
            Debug.Log("CinderUtils.Reflection: AssemblyUtils initialized.");
        }

        internal static PredefinedAssembly? GetAssemblyType(string assemblyName) {
            return assemblyName switch {
                "Assembly-CSharp" => PredefinedAssembly.Assembly_CSharp,
                "Assembly-CSharp-Editor" => PredefinedAssembly.Assembly_CSharpEditor,
                "Assembly-CSharp-firstpass" => PredefinedAssembly.Assembly_CSharp_FirstPass,
                "Assembly-CSharp-Editor-firstpass" => PredefinedAssembly.Assembly_CSharpEditor_FirstPass,
                _ => null
            };
        }

        internal static bool TryGetAssemblyType(string assemblyName, out PredefinedAssembly? type) {
            type = GetAssemblyType(assemblyName);
            return type != null && type != PredefinedAssembly.Unknown;
        }

        // Returns a collection of Types per AssemblyType.
        internal static Dictionary<PredefinedAssembly, Assembly> GetPredefinedAssemblies() {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<PredefinedAssembly, Assembly> assemblyCache = new();

            // Extract all the types from the defined assemblies (AssemblyType)
            foreach (Assembly asm in assemblies) {
                if (TryGetAssemblyType(asm.GetName().Name, out var asmType)) {
                    assemblyCache.Add((PredefinedAssembly) asmType, asm);
                }
            }

            return assemblyCache;
        }

        internal static HashSet<Type> GetSubtypesInAssemblyOf<T>(IEnumerable<Assembly> assemblies) {
            HashSet<Type> subtypes = new();

            foreach (var asm in assemblies) {
                GetSubtypesInAssemblyOf<T>(asm).ForEach(type => subtypes.Add(type));
            }

            return subtypes;
        }

        internal static HashSet<Type> GetSubtypesInAssemblyOf<T>(Assembly assembly) {
            if (assembly == null) return null;

            var parentType = typeof(T);
            HashSet<Type> subtypes = new();

            foreach (Type type in assembly.GetTypes()) {
                // Could a variable of type "parentType" get assigned a value of the type "type"?
                // If so, the type is a subtype
                if (parentType.IsAssignableFrom(type)) {
                    //Debug.Log($"CinderUtils: AssemblyUtils: Found Subtype of '{parentType}': '{type.Name}' in Assembly '{assembly.GetName().Name}'.");
                    subtypes.Add(type);
                }
            }

            subtypes.Remove(typeof(T));
            return subtypes;
        }
    }

}