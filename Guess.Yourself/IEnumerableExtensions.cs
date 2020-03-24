using System;
using System.Collections.Generic;

namespace Guess.Yourself
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Update<T>(this IEnumerable<T> source, Action<T> act)
        {
            foreach (var element in source)
            {
                act(element);
                yield return element;
            };
        }
    }
}