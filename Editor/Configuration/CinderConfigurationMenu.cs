using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Editor {
    public class CinderConfigurationMenu : MonoBehaviour {

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

        #region CinderDebug Toggling
#if CINDER_DEBUG
        [MenuItem("CinderUtils/Disable Debug Mode", false, -1000)]
        private static void ToggleCinderDebug() {
            bool enabled = true;
#else
        [MenuItem("CinderUtils/Enable Debug Mode", false, -1000)]
        private static void ToggleCinderDebug() {
            bool enabled = false;
#endif
            bool result = SetDefine(CinderDebug.CINDER_DEBUG, !enabled);
            if (result) {
                string display = enabled ? "Disabled" : "Enabled";
                Debug.LogWarning($"CinderUtils: {display} debug features.");
            }
        }
        #endregion

        #region 'MY_DEBUG' Toggling
#if MY_DEBUG
        [MenuItem("CinderUtils/Disable 'MY_DEBUG'", false, -1000)]
        private static void ToggleMyDebugFlag() {
            bool enabled = true;
#else
        [MenuItem("CinderUtils/Enable 'MY_DEBUG'", false, -1000)]
        private static void ToggleMyDebugFlag() {
            bool enabled = false;
#endif
            bool result = SetDefine(Utils.MY_DEBUG, !enabled);
            if (result) {
                string display = enabled ? "Disabled" : "Enabled";
                Debug.LogWarning($"CinderUtils: {display} the 'MY_DEBUG' custom define.");
            }
        }
        #endregion

    }
}
