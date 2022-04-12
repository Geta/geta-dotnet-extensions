using Xunit;

namespace Geta.Net.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("http://mysite/mypage")]
        [InlineData("http://mysite.com/mypage")]
        [InlineData("http://mysite.com")]
        [InlineData("http://mysite.com/")]
        public void IsAbsoluteUrl_with_absolute_url_is_true(string url)
        {
            Assert.True(url.IsAbsoluteUrl());
        }

        [Theory]
        [InlineData("mypage")]
        [InlineData("/mypage")]
        [InlineData("mypage/anotherpage")]
        [InlineData("/mypage/anotherpage")]
        public void IsAbsoluteUrl_with_relative_url_is_false(string url)
        {
            Assert.False(url.IsAbsoluteUrl());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsAbsoluteUrl_with_invalid_url_is_false(string url)
        {
            Assert.False(url.IsAbsoluteUrl());
        }

        [Theory]
        [InlineData("http://mysite/mypage")]
        [InlineData("http://mysite.com/mypage")]
        [InlineData("http://mysite.com")]
        [InlineData("http://mysite.com/")]
        public void IsRelativeUrl_with_absolute_url_is_false(string url)
        {
            Assert.False(url.IsRelativeUrl());
        }

        [Theory]
        [InlineData("mypage")]
        [InlineData("/mypage")]
        [InlineData("mypage/anotherpage")]
        [InlineData("/mypage/anotherpage")]
        public void IsRelativeUrl_with_relative_url_is_true(string url)
        {
            Assert.True(url.IsRelativeUrl());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsRelativeUrl_with_invalid_url_is_false(string url)
        {
            Assert.False(url.IsAbsoluteUrl());
        }

        [Fact]
        public void CapitalizeShouldMakeSingleLetterCapital()
        {
            var str = "a";

            var result = str.Capitalize();

            Assert.Equal("A", result);
        }

        [Fact]
        public void CapitalizeShouldReturnEmptyStringForEmptyString()
        {
            var str = string.Empty;

            var result = str.Capitalize();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void CapitalizeShouldReturnNullForNullString()
        {
            string str = null;

            var result = str.Capitalize();

            Assert.Null(result);
        }

        [Theory]
        [InlineData("mytest", "Mytest")]
        [InlineData("my test", "My Test")]
        [InlineData("MyTest", "Mytest")]
        [InlineData("MYTEST", "Mytest")]
        [InlineData("My test", "My Test")]
        [InlineData("my-test", "My-Test")]
        [InlineData("mYTEST", "Mytest")]
        public void CapitalizeOnlyFirstLetterInAllWords(string word, string expected)
        {
            var result = word.Capitalize();

            Assert.Equal(expected, result);
        }
    }
}
