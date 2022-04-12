// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;

namespace Geta.Net.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Returns a Unix Epoch time given a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
        public static long ToEpochTime(this DateTime dateTime) => (long)(dateTime - Epoch).TotalSeconds;

        /// <summary>
        /// Returns a Unix Epoch time if given a value, and null otherwise.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
        /// <returns></returns>
        public static long? ToEpochTime(this DateTime? dateTime) => dateTime.HasValue ? (long?)ToEpochTime(dateTime.Value) : null;

        /// <summary>
        /// Returns end of the day datetime "dd.mm.yyyy 23:59:59"
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> source.</param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);
        }

        /// <summary>
        /// Returns beginning of the day datetime "dd.mm.yyyy 00:00:00"
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> source.</param>
        /// <returns></returns>
        public static DateTime BeginningOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Checks if date is today (Should be in UTC).
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsToday(this DateTime date)
        {
            return date.Date == DateTime.UtcNow.Date;
        }

        /// <summary>
        /// Checks if date is tomorrow (Should be in UTC).
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsTomorrow(this DateTime date)
        {
            return DateTime.UtcNow.Date == date.Date.AddDays(-1);
        }

        /// <summary>
        /// Checks if date is yesterday (Should be in UTC).
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsYesterday(this DateTime date)
        {
            return DateTime.UtcNow.Date == date.Date.AddDays(+1);
        }

        /// <summary>
        /// Converts datetime to timestamp.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to convert</param>
        /// <returns>Timestamp.</returns>
        public static int ToTimestamp(this DateTime date)
        {
            return (int)date.ToUniversalTime().Subtract(Epoch).TotalSeconds;
        }
    }
}
