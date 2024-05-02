using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using UnityEngine;


namespace CinderUtils {
    public static class Utils {
        #region CONSTANTS
        public const string MY_DEBUG = "MY_DEBUG";

        #endregion

        #region 2D Physics
        /// <summary>
        /// Returns the mouse's world position in 2D world space.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetMousePos2D() {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        #endregion

        #region 3D Physics
        /// <summary>
        /// Returns the mouse's position in 3D world space.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetMousePos3D() {
            return ShootMouseRay3D(out var hit) ? hit.point : Vector3.zero;
        }

        /// <summary>
        /// Returns the mouse's position in 3D world space.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetMousePos3D(float maxDistance) {
            return ShootMouseRay3D(out var hit, maxDistance) ? hit.point : Vector3.zero;
        }

        /// <summary>
        /// Returns the mouse's position in 3D world space.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetMousePos3D(float maxDistance, LayerMask layerMask) {
            return ShootMouseRay3D(out var hit, maxDistance, layerMask) ? hit.point : Vector3.zero;
        }

        /// <summary>
        /// Shoots a Raycast from the camera in the proyected direction of the mouse.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShootMouseRay3D(out RaycastHit hit) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit);
        }

        /// <summary>
        /// Shoots a Raycast from the camera in the proyected direction of the mouse.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShootMouseRay3D(out RaycastHit hit, float maxDistance) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, maxDistance);
        }

        /// <summary>
        /// Shoots a Raycast from the camera in the proyected direction of the mouse.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShootMouseRay3D(out RaycastHit hit, float maxDistance, LayerMask layerMask) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, maxDistance, layerMask);
        }
        #endregion

        #region Number Formatting
        /// <summary>
        /// Converts seconds to minutes and seconds (MM:ss).
        /// </summary>
        public static string SecondsToFormattedMinutes(double totalSeconds) {
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);

            return $"{timeSpan.TotalMinutes:00}:{timeSpan.Seconds:00}";
        }

        /// <summary>
        /// Converts seconds to hours, minutes and seconds (HH:MM:ss).
        /// </summary>
        public static string SecondsToFormattedHours(double totalSeconds) {
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);

            return $"{timeSpan.TotalHours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        }
        #endregion

    }
}