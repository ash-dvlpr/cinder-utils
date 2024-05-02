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

        public static T LoopingGet<T>(this ICollection<T> collection, int i) {
            if (collection.Count == 0) return default(T);
            else return collection.ElementAt(i % collection.Count);
        }
    }

    // Extensions for Collections
    public static class DictionaryExtensions {

        public static V GetValue<K, V>(this IDictionary<K, V> dict, K key, V defaultValue = default(V)) {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }

}

