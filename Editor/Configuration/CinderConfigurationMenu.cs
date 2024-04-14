using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Editor {
    public class CinderConfigurationMenu : MonoBehaviour {

        #region CinderDebug Toggling
#if CINDER_DEBUG
        [MenuItem("CinderUtils/Disable Debug Mode", false, -1000)]
        private static void DisableCinderDebug() {
            bool result = SetDefine(CinderDebug.CINDER_DEBUG, false);
            if (result) Debug.LogWarning($"CinderUtils: Disabled debug features.");
        }
#else
        [MenuItem("CinderUtils/Enable Debug Mode", false, -1000)]
        private static void EnableCinderDebug() {
            bool result = SetDefine(CinderDebug.CINDER_DEBUG, true);
            if (result) Debug.LogWarning($"CinderUtils: Enabled debug features.");
        }
#endif
        #endregion

        #region Common Stuff
        internal static bool SetDefine(string define, bool status) {
            // Get defines
            string currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            HashSet<string> defines = new();

            // Split DEFINES
            currentDefines.Split(';').ForEach(define => {
                defines.Add(define);
            });

            // Cache old number
            int startingCount = defines.Count;

            // Do the operation
            if (status) defines.Add(define);
            else defines.Remove(define);

            // Save the modifications
            bool modified = (defines.Count != startingCount);
            if (modified) {
                string newDefines = string.Join(";", defines);
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, newDefines);
            }

            return modified;
        }

        #endregion
    }
}
