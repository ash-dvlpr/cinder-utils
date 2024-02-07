using System;
using System.Collections.Generic;
using UnityEngine;


namespace CinderUtils.Extensions {

    // Extensions for Transforms
    public static class TransformExtensions {

        /// <summary>
        /// Easy to use way of fetching all the children of a transform.
        /// </summary>
        /// <param name="parent">Parent Transform</param>
        /// <returns>IEnumerable with all the child Transforms.</returns>
        public static IEnumerable<Transform> Children(this Transform parent) {
            foreach (Transform child in parent) {
                yield return child;
            }
        }
    }

}
