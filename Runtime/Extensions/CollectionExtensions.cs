using System;
using System.Linq;
using System.Collections.Generic;

using Random = UnityEngine.Random;


namespace CinderUtils.Extensions {

    // Extensions for Collections
    public static class CollectionExtensions {

        public static T GetRandom<T>(this ICollection<T> collection) {
            if (collection.Count == 0) return default(T);
            else return collection.ElementAt(Random.Range(0, collection.Count));
        }
    }

}
