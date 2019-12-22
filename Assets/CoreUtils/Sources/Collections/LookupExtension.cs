using System;
using System.Collections.Generic;

namespace CoreUtils.Collections
{
    public static class LookupExtension
    {
        public static T Min<T, TArgs>(this IEnumerable<T> list, Func<T, TArgs, float> valueGetter, TArgs args)
        {
            return Lookup<T, TArgs>(list, valueGetter, args, MinComparer);
        }

        public static T Max<T, TArgs>(this IEnumerable<T> list, Func<T, TArgs, float> valueGetter, TArgs args)
        {
            return Lookup<T, TArgs>(list, valueGetter, args, MaxComparer);
        }

        public static void Filter<T, TArgs>(this IEnumerable<T> list, Func<T, TArgs, bool> filter, TArgs args,
            List<T> result)
        {
            result.Clear();
            foreach (var item in list)
            {
                if (!filter(item, args)) continue;
                result.Add(item);
            }
        }

        private static T Lookup<T, TArgs>(this IEnumerable<T> list, Func<T, TArgs, float> valueGetter, TArgs args, Func<float, float, bool> comparer)
        {
            var result = default(T);
            var nearest = 0f;
            var hasNearest = false;
            foreach (var item in list)
            {
                var distance = valueGetter(item, args);
                if (!comparer(distance, nearest) && hasNearest) continue;
                hasNearest = true;
                result = item;
                nearest = distance;
            }

            return result;
        }

        private static bool MinComparer(float value, float nearestValue)
        {
            return value < nearestValue;
        }

        private static bool MaxComparer(float value, float nearestValue)
        {
            return value > nearestValue;
        }

    }
}
