using System;
using System.Collections.Generic;
using Xunit;

namespace Geta.Net.Extensions.Tests
{
    public class FluentExtensionsTests
    {
        [Fact]
        public void Fluent_makes_method_fluent()
        {
            var list = new List<string>()
                .Fluent(l => l.Add("Hello"))
                .Fluent(l => l.Add(", "))
                .Fluent(l => l.Add("World!"));

            var expected = "Hello, World!";
            var actual = string.Concat(list);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Fluent_allows_to_build_custom_fluent_api()
        {
            var list = new List<string>()
                .FluentAdd("Hello")
                .FluentAdd(", ")
                .FluentAdd("World!");

            var expected = "Hello, World!";
            var actual = string.Concat(list);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void If_allows_to_use_current_value_for_conditions()
        {
            Func<List<string>, bool> lessThan2 = l => l.Count < 2;
            var list = new List<string>()
                .If(lessThan2, l => l.FluentAdd("One"))
                .If(lessThan2, l => l.FluentAdd("Two"))
                .If(lessThan2, l => l.FluentAdd("Three"));

            var expected = 2;
            Assert.Equal(expected, list.Count);
        }

        [Fact]
        public void If_does_not_invoke_nested_condition_predicate_when_parent_condition_is_false()
        {
            var value = "Hello";

            var list = new List<string>()
                .If(false, l => 
                    l.If(() => !string.IsNullOrEmpty(value),
                        l1 => l1.FluentAdd(value)));

            var expected = string.Empty;
            var actual = string.Concat(list);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void If_invokes_nested_condition_predicate_when_parent_condition_is_true()
        {
            var value = "Hello";

            var list = new List<string>()
                .If(true, l =>
                    l.If(() => !string.IsNullOrEmpty(value),
                        l1 => l1.FluentAdd(value)));

            var expected = value;
            var actual = string.Concat(list);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void If_allows_to_use_bool_for_conditions()
        {
            string value1 = null;
            var value2 = string.Empty;
            var value3 = "Hello";

            var list = new List<string>()
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once ExpressionIsAlwaysNull
                .If(!string.IsNullOrEmpty(value1), l => l.FluentAdd(value1))
                .If(!string.IsNullOrEmpty(value2), l => l.FluentAdd(value2))
                .If(!string.IsNullOrEmpty(value3), l => l.FluentAdd(value3));

            var expected = "Hello";
            var actual = string.Concat(list);
            Assert.Equal(expected, actual);
        }
    }

    public static class ExampleFluentListApi
    {
        public static List<T> FluentAdd<T>(this List<T> list, T item)
        {
            return list.Fluent(l => l.Add(item));
        }
    }
}