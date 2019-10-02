// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Geta.Net.Extensions
{
    /// <summary>
    ///     String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Transforms list into a separated string.
        /// </summary>
        /// <param name="strings">The source</param>
        /// <param name="separator">String separator</param>
        /// <param name="skipNullOrWhitespace">If list items that are null or empty strings should be skipped</param>
        /// <returns></returns>
        public static string JoinStrings(this IEnumerable<string> strings, string separator = ", ", bool skipNullOrWhitespace = true)
        {
            var stringsToJoin = strings
                .Where(x => !skipNullOrWhitespace || !string.IsNullOrWhiteSpace(x));

            return string.Join(separator, stringsToJoin);
        }

        /// <summary>
        ///     Creates URL / Html friendly slug
        /// </summary>
        /// <param name="phrase">Source string.</param>
        /// <param name="maxLength">Max length</param>
        /// <param name="wordSeparator">Word separator</param>
        /// <returns></returns>
        public static string GenerateSlug(this string phrase, int maxLength = 50, string wordSeparator = "-")
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return string.Empty;

            // seperate words in camel case
            var charList = new List<char>();
            for (int pos = 0; pos < phrase.Length; pos++)
            {
                var ch1 = phrase[pos];
                charList.Add(ch1);

                if (pos < phrase.Length - 1)
                {
                    var ch2 = phrase[pos + 1];

                    if ((char.IsLower(ch1) && (char.IsUpper(ch2) || char.IsNumber(ch2))) ||
                        (char.IsUpper(ch1) && (char.IsNumber(ch2) || char.IsUpper(ch2))) ||
                        (char.IsNumber(ch1) && (char.IsLower(ch2) || char.IsUpper(ch2))))
                    {
                        charList.Add('-');
                    }
                }
            }
            // remove accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(charList.ToArray());
            var str = Encoding.ASCII.GetString(bytes);
            // to lower case
            str = str.ToLowerInvariant();
            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            // replace spaces with hyphens
            str = Regex.Replace(str, @"\s", "-");
            str = str.Replace("-", wordSeparator);
            // cut and trim it to maxLenght
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();

            return str;
        }

        public static string GenerateSlugWithoutHyphens(this string phrase, int maxLength = 50)
        {
            return phrase.GenerateSlug(maxLength, string.Empty);
        }

        /// <summary>
        ///     Parses string to nullable int (Int32).
        /// </summary>
        /// <param name="input">Source string.</param>
        /// <returns>int (Int32) value if parse succeeds otherwise null.</returns>
        public static int? TryParseInt32(this string input)
        {
            int outValue;
            return Int32.TryParse(input, out outValue) ? (int?)outValue : null;
        }

        /// <summary>
        ///     Parses string to nullable long (Int64).
        /// </summary>
        /// <param name="input">Source string.</param>
        /// <returns>long (Int64) value if parse succeeds otherwise null.</returns>
        public static long? TryParseInt64(this string input)
        {
            long outValue;
            return long.TryParse(input, out outValue) ? (long?)outValue : null;
        }

        /// <summary>
        /// Answers true if this String is either null or empty.
        /// </summary>
        /// <param name="input">The string to check.</param>
        public static bool IsNullOrEmpty(this string input) => string.IsNullOrEmpty(input);

        /// <summary>
        /// Answers true if this String is neither null or empty.
        /// </summary>
        /// <param name="input">The string to check.</param>
        public static bool HasValue(this string input) => !IsNullOrEmpty(input);

        /// <summary>
        /// Encodes the string as HTML.
        /// </summary>
        /// <param name="input">The dangerous string to encode.</param>
        /// <returns>The safely encoded HTML string.</returns>
        public static string HtmlEncode(this string input) => input.HasValue() ? WebUtility.HtmlEncode(input) : input;

        /// <summary>
        /// Decodes an HTML string.
        /// </summary>
        /// <param name="input">The HTML string to decode.</param>
        /// <returns>The decoded HTML string.</returns>
        public static string HtmlDencode(this string input) => input.HasValue() ? WebUtility.HtmlDecode(input) : input;

        /// <summary>
        /// Encodes the string for URLs.
        /// </summary>
        /// <param name="input">The dangerous string to URL encode.</param>
        /// <returns>The safely encoded URL string.</returns>
        public static string UrlEncode(this string input) => input.HasValue() ? WebUtility.UrlEncode(input) : input;

        /// <summary>
        /// Decodes a URL-encoded string.
        /// </summary>
        /// <param name="input">The URL-encoded string to decode.</param>
        /// <returns>The decoded string.</returns>
        public static string UrlDecode(this string input) => input.HasValue() ? WebUtility.UrlDecode(input) : input;

        /// <summary>
        /// Checks if a string is absolute URL.
        /// </summary>
        /// <param name="url">The string to check.</param>
        /// <returns>Returns true, if it is absolute URL and false when not.</returns>
        public static bool IsAbsoluteUrl(this string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var _);
        }

        /// <summary>
        /// Checks if a string is relative URL.
        /// </summary>
        /// <param name="url">The string to check.</param>
        /// <returns>Returns true, if it is relative URL and false when not.</returns>
        public static bool IsRelativeUrl(this string url)
        {
            return Uri.TryCreate(url, UriKind.Relative, out var _);
        }
    }
}