using System.Linq;
using Geta.Net.Extensions.Generators;
using Xunit;

namespace Geta.Net.Extensions.Tests
{
    public class StringGeneratorTests
    {
        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(10, 0, 0, 0)]
        [InlineData(0, 10, 0, 0)]
        [InlineData(0, 0, 10, 0)]
        [InlineData(0, 0, 0, 10)]
        [InlineData(5, 5, 5, 5)]
        public void String_generation_includes_all_required_chars(int expectedUppercaseCharCount,
            int expectedLowerCaseCharCount, int expectedDigitCount, int expectedSymbolCount)
        {
            var value = StringGenerator.GenerateRandomString(expectedUppercaseCharCount, expectedLowerCaseCharCount,
                expectedDigitCount, expectedSymbolCount);

            const string allLowercaseChars = "abcdefghijkmnopqrstuvwxyz";
            const string allUpperCaseChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string allDigits = "0123456789";
            const string allSymbols = "!&%$*";

            var lowercaseCharCount = value.Count(x => allLowercaseChars.Contains(x));
            var uppercaseCharCount = value.Count(x => allUpperCaseChars.Contains(x));
            var digitCount = value.Count(x => allDigits.Contains(x));
            var symbolCount = value.Count(x => allSymbols.Contains(x));
            var totalCount = value.Length;

            Assert.Equal(expectedLowerCaseCharCount, lowercaseCharCount);
            Assert.Equal(expectedUppercaseCharCount, uppercaseCharCount);
            Assert.Equal(expectedDigitCount, digitCount);
            Assert.Equal(expectedSymbolCount, symbolCount);

            var expectedTotalCount = expectedUppercaseCharCount + expectedLowerCaseCharCount + expectedDigitCount +
                                     expectedSymbolCount;

            Assert.Equal(expectedTotalCount, totalCount);
        }

        [Fact]
        public void String_generation_is_random()
        {
            var values = Enumerable.Range(1, 100)
                .Select(x => StringGenerator.GenerateRandomString(5, 5, 5, 5))
                .ToList();

            var firstDuplicate = values
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .FirstOrDefault();

            Assert.Null(firstDuplicate);
        }
    }
}