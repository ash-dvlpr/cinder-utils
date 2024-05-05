using System;
using System.Linq;
using System.Collections.Generic;


namespace CinderUtils.Extensions {

    // Extensions for Collections
    public static class CollectionExtensions {

        public static T GetRandom<T>(this ICollection<T> collection) {
            if (collection.Count == 0) return default(T);
            else return collection.ElementAt(UnityEngine.Random.Range(0, collection.Count));
        }

        public static T GetRandom<T>(this ICollection<T> collection, System.Random RNG) {
            if (collection.Count == 0) return default(T);
            else return collection.ElementAt(RNG.Next(0, collection.Count));
        }

        public static T LoopingGet<T>(this ICollection<T> collection, int i) {
            if (collection.Count == 0) return default(T);
            else return collection.ElementAt(i % collection.Count);
        }

        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> items) {
            foreach (var item in items) {
                collection.Add(item);
            }
        }
    }

    // Extensions for Dictionaries
    public static class DictionaryExtensions {

        public static V GetValue<K, V>(this IDictionary<K, V> dict, K key, V defaultValue = default(V)) {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }

}

