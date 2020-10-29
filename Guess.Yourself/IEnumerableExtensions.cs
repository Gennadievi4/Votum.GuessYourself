using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

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

        [CanBeNull]
        public static DependencyObject FindVisualRoot([CanBeNull] this DependencyObject obj)
        {
            if (obj is null) return null;
            do
            {
                var parent = VisualTreeHelper.GetParent(obj);
                if (parent is null) return obj;
                obj = parent;
            } while (true);
        }
    }
}