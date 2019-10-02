// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information
using System;
using System.Globalization;
using System.Linq;

namespace Geta.Net.Extensions.Helpers
{
    public static class CultureInfoHelpers
    {
        /// <summary>
        /// Checks if there is a culture by the provided name.
        /// </summary>
        /// <param name="cultureName">Culture name to find.</param>
        /// <returns>Returns true if culture exists and false if not.</returns>
        public static bool Exists(string cultureName)
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .Any(culture => string.Equals(culture.Name, cultureName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}