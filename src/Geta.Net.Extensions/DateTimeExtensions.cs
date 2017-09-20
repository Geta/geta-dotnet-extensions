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
        public static long ToEpochTime(this DateTime dateTime) => (long) (dateTime - Epoch).TotalSeconds;

        /// <summary>
        /// Returns a Unix Epoch time if given a value, and null otherwise.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
        /// <returns></returns>
        public static long? ToEpochTime(this DateTime? dateTime) => dateTime.HasValue ? (long?) ToEpochTime(dateTime) : null;
    }
}