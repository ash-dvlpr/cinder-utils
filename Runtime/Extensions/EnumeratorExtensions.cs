using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace CinderUtils.Extensions {

    // Extensions for IEnumerations and IEnumerables
    public static class EnumeratorExtensions {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> e) {
            while (e.MoveNext()) {
                yield return e.Current;
            }
        }

        public static bool NullOrEmpty<T>(this IEnumerable<T> enumerable) {
            return ( enumerable == null || enumerable.Count() == 0 );
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (T item in enumerable) {
                action(item);
            }
        }

        public static string ToDebugString<T>(this IEnumerable<T> enumerable) {
            StringBuilder sb = new StringBuilder("[");

            var i = 0;
            foreach (T item in enumerable) {
                sb.Append('"');
                sb.Append(item.ToString());
                sb.Append('"');

                if (++i != enumerable.Count()) sb.Append(", ");
            }

            sb.Append("]");
            return sb.ToString();
        }
    }

}
