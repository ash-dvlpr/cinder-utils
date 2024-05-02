using System;
using System.Reflection;

using UnityEngine;


namespace CinderUtils.Extensions {

    // Extensions for Vectors
    public static class VectorExtensions  {

        #region Vector3
        public static Vector3 Offset(this Vector3 vec, Vector3 offsetVector) {
            return vec + offsetVector;
        }

        public static Vector3 OffsetX(this Vector3 vec, float offset) {
            vec.x += offset;
            return vec;
        }

        public static Vector3 OffsetY(this Vector3 vec, float offset) {
            vec.y += offset;
            return vec;
        }

        public static Vector3 OffsetZ(this Vector3 vec, float offset) {
            vec.z += offset;
            return vec;
        }
        #endregion

        #region Vector3Int
        public static Vector3Int Offset(this Vector3Int vec, Vector3Int offsetVector) {
            return vec + offsetVector;
        }

        public static Vector3Int OffsetX(this Vector3Int vec, int offset) {
            vec.x += offset;
            return vec;
        }

        public static Vector3Int OffsetY(this Vector3Int vec, int offset) {
            vec.y += offset;
            return vec;
        }

        public static Vector3Int OffsetZ(this Vector3Int vec, int offset) {
            vec.z += offset;
            return vec;
        }
        #endregion

        #region Vector2
        public static Vector2 Offset(this Vector2 vec, Vector2 offsetVector) {
            return vec + offsetVector;
        }

        public static Vector2 OffsetX(this Vector2 vec, float offset) {
            vec.x += offset;
            return vec;
        }

        public static Vector2 OffsetY(this Vector2 vec, float offset) {
            vec.y += offset;
            return vec;
        }
        #endregion

        #region Vector2Int
        public static Vector2Int Offset(this Vector2Int vec, Vector2Int offsetVector) {
            return vec + offsetVector;
        }

        public static Vector2Int OffsetX(this Vector2Int vec, int offset) {
            vec.x += offset;
            return vec;
        }

        public static Vector2Int OffsetY(this Vector2Int vec, int offset) {
            vec.y += offset;
            return vec;
        }
        #endregion

    }

}
