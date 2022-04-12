// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Text;

namespace Geta.Net.Extensions.Generators
{
    public static class StringGenerator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        private const string AllowedLowercaseChars = "abcdefghijkmnopqrstuvwxyz";
        private const string AllowedUpperCaseChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        private const string AllowedDigits = "0123456789";
        private const string AllowedSymbols = "!&%$*";

        public static string GenerateRandomPassword(
            int uppercaseChars = 2,
            int lowerCaseChars = 2,
            int digits = 2,
            int symbols = 2)
        {
            return GenerateRandomString(uppercaseChars, lowerCaseChars, digits, symbols);
        }

        /// <summary>
        /// Generates a random string
        /// </summary>
        /// <param name="uppercaseChars">How many upper case characters string should contain</param>
        /// <param name="lowerCaseChars">How many lower case characters string should contain</param>
        /// <param name="digits">How many digit characters string should contain</param>
        /// <param name="symbols">How many symbol characters string should contain</param>
        /// <returns>string</returns>
        public static string GenerateRandomString(int uppercaseChars, int lowerCaseChars, int digits, int symbols)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < uppercaseChars; i++)
            {
                var pos = Random.Next(0, AllowedUpperCaseChars.Length);
                sb.Append(AllowedUpperCaseChars[pos]);
            }

            for (var i = 0; i < lowerCaseChars; i++)
            {
                var pos = Random.Next(0, AllowedLowercaseChars.Length);
                sb.Append(AllowedLowercaseChars[pos]);
            }

            for (var i = 0; i < digits; i++)
            {
                var pos = Random.Next(0, AllowedDigits.Length);
                sb.Append(AllowedDigits[pos]);
            }

            for (var i = 0; i < symbols; i++)
            {
                var pos = Random.Next(0, AllowedSymbols.Length);
                sb.Append(AllowedSymbols[pos]);
            }

            return sb.ToString();
        }
    }
}
