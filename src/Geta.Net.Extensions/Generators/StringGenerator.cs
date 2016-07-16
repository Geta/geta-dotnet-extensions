using System;

namespace Geta.Net.Extensions.Generators
{
    public static class StringGenerator
    {
        public static string GenerateRandomPassword(int uppercaseChars = 2, int lowerCaseChars = 2, int digits = 2, int symbols = 2)
        {
            return GenerateRandomString(uppercaseChars, lowerCaseChars, digits, symbols);
        }

        public static string GenerateRandomString(int uppercaseChars, int lowerCaseChars, int digits, int symbols)
        {
            const string allowedLowercaseChars = "abcdefghijkmnopqrstuvwxyz";
            const string allowerUpperCaseChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string allowedDigits = "0123456789";
            const string allowedNonAlphaNum = "!&%$*";
            var random = new Random();

            string password = string.Empty;

            for (int i = 0; i < uppercaseChars; i++)
            {
                var pos = random.Next(0, allowerUpperCaseChars.Length);
                password += allowerUpperCaseChars[pos];
            }

            for (int i = 0; i < lowerCaseChars; i++)
            {
                var pos = random.Next(0, allowedLowercaseChars.Length);
                password += allowedLowercaseChars[pos];
            }

            for (int i = 0; i < digits; i++)
            {
                var pos = random.Next(0, allowedDigits.Length);
                password += allowedDigits[pos];
            }

            for (int i = 0; i < symbols; i++)
            {
                var pos = random.Next(0, allowedNonAlphaNum.Length);
                password += allowedNonAlphaNum[pos];
            }

            return password;
        }
    }
}