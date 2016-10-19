using System;

namespace Geta.Net.Extensions
{
    public static class FluentExtensions
    {
        public static T If<T>(this T source, bool condition, Func<T, T> func)
        {
            return source.If(_ => condition, func);
        }

        public static T If<T>(this T source, Func<bool> condition, Func<T, T> func)
        {
            return source.If(_ => condition(), func);
        }

        public static T If<T>(this T source, Func<T, bool> condition, Func<T, T> func)
        {
            return condition(source) ? func(source) : source;
        }

        public static T Fluent<T>(this T source, Action<T> action)
        {
            action(source);
            return source;
        }
    }
}