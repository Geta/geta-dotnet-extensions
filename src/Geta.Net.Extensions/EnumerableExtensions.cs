// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Geta.Net.Extensions
{
    /// <summary>
    ///     Enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Applies an action on each item of the sequence.
        /// </summary>
        /// <typeparam name="T">The type of an item.</typeparam>
        /// <param name="items">The source sequence of items.</param>
        /// <param name="action">The action to apply on an item.</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
            {
                return;
            }

            foreach (var obj in items)
            {
                action(obj);
            }
        }

        /// <summary>
        ///     Filters the elements of an <see cref="T:System.Collections.IEnumerable" /> based on a specified type.
        ///     Returns empty sequence if source sequence is null.
        ///     NOTE: Helper extension to work with legacy APIs which might return null references of IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type to filter the elements of the sequence on.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.IEnumerable" /> whose elements to filter.</param>
        /// <returns>
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence of
        ///     type <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> SafeOfType<T>(this IEnumerable source)
        {
            return source == null ? Enumerable.Empty<T>() : source.OfType<T>().OrEmptyIfNull();
        }

        /// <summary>
        ///     Returns empty sequence if source sequence is null otherwise returns source sequence.
        ///     NOTE: Helper extension to work with legacy APIs which might return null references of IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements of the sequence.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to check.</param>
        /// <returns>
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence of type
        ///     <typeparamref name="T" />
        ///     or empty sequence.
        /// </returns>
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        ///     Checks whether given sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of elements of the sequence.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to check.</param>
        /// <returns>Returns <code>true</code> if given sequence is null or is empty</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Filters by page and page size.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <returns>Filtered sequence.</returns>
        public static IEnumerable<T> FilterPaging<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            var take = pageSize;
            var skip = pageSize * (page - 1);
            return source.Skip(skip).Take(take);
        }
        /// <summary>
        /// Transforms item into IEnumerable with one item.
        /// </summary>
        /// <typeparam name="T">The type of elements of the sequence.</typeparam>
        /// <param name="item">Only member of enumerable.</param>
        /// <returns>Enumerable with one item</returns>
        public static IEnumerable<T> Singleton<T>(T item)
        {
            yield return item;
        }
        /// <summary>
        /// Splits IEnumerable into multiple partitions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="size">Partition size.</param>
        /// <returns>Partitioned enumerable.</returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            List<T> partition = null;

            foreach (var item in source)
            {
                if (partition == null)
                {
                    partition = new List<T>(size);
                }

                partition.Add(item);

                if (partition.Count == size)
                {
                    yield return new ReadOnlyCollection<T>(partition);
                    partition = null;
                }
            }

            if (partition != null && partition.Any())
            {
                yield return new ReadOnlyCollection<T>(partition);
            }
        }
        /// <summary>
        /// Selects distinct values from list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">Property</typeparam>
        /// <param name="items">The source</param>
        /// <param name="property">Property to distinct by</param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}