using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CinderUtils.Extensions {

    // Extensions for GameObjects
    public static class GameObjectExtensions {

        /// <summary>
        /// Returns the object or null if the object is null or unity deinitializing.
        /// </summary>
        /// <remarks>
        /// This is usefull to avoid NullReferenceExceptions and to be able to properly use the null coalescing operators.
        /// </remarks>
        /// <typeparam name="T"><see cref="Object">UnityEngine.Object</see></typeparam>
        /// <returns>obj or null if null or deinitializing.</returns>
        public static T OrNull<T>(this T obj) where T : Object {
            return obj ? obj : null;
        }

        public static Vector3 position(this GameObject go) => go.transform.position;

        /// <summary>
        /// Easy to use way of fetching all the children of a GameObject.
        /// </summary>
        /// <param name="parent">Parent GameObject</param>
        /// <returns>IEnumerable with all the child GameObjects.</returns>
        public static IEnumerable<GameObject> Children(this GameObject parent) {
            foreach (Transform child in parent.transform.Children()) {
                yield return child.gameObject;
            }
        }
    }

}
