using System;
using System.Collections.Generic;
using System.Windows;

namespace SS.ShareScreen.Extensions
{
    public static class CommonExtensions
    {
        
        public static Visibility ToVisibility(this bool arg)
        {
            return arg ? Visibility.Visible : Visibility.Collapsed;
        }
         

        public static bool ToBool(this Visibility arg)
        {
            return arg == Visibility.Visible;
        }

        public static bool IsNull(this object ths)
        {
            return ReferenceEquals(ths,null);
        }

        public static bool IsNotNull(this object ths)
        {
            return !ReferenceEquals(ths, null);
        }


        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

    }
}