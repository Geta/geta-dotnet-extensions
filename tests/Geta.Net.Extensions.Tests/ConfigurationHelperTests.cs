using System.Configuration;
using Geta.Net.Extensions.Helpers;
using Xunit;

namespace Geta.Net.Extensions.Tests
{
    public class ConfigurationHelperTests
    {
        private static readonly string Prefix = "ConfigurationHelperTests";

        private enum TestEnum
        {
            This = 0,
            Is = 1,
            Test = 2
        }

        [Fact]
        public void Enum_config_can_be_read()
        {
            var value = ConfigurationHelper.GetConfigRequired<TestEnum>(Prefix, "Enum");

            Assert.Equal(TestEnum.Test, value);
        }

        [Fact]
        public void Int_config_can_be_read()
        {
            var value = ConfigurationHelper.GetConfigRequired<int>(Prefix, "Int");

            Assert.Equal(12345, value);
        }

        [Fact]
        public void String_config_can_be_read()
        {
            var value = ConfigurationHelper.GetConfigRequired(Prefix, "String");

            Assert.Equal("Hello world!", value);
        }

        [Fact]
        public void Missing_required_int_config_returns_default()
        {
            var value = ConfigurationHelper.GetConfig(Prefix, "Missing", 98765);

            Assert.Equal(98765, value);
        }

        [Fact]
        public void Missing_required_string_config_returns_default()
        {
            var value = ConfigurationHelper.GetConfig(Prefix, "Missing", "Default");

            Assert.Equal("Default", value);
        }
        
        [Fact]
        public void Missing_required_enum_config_returns_default()
        {
            var value = ConfigurationHelper.GetConfig(Prefix, "Missing", TestEnum.Is);

            Assert.Equal(TestEnum.Is, value);
        }

        [Fact]
        public void Missing_required_config_throws()
        {
            Assert.Throws<ConfigurationErrorsException>(() => ConfigurationHelper.GetConfigRequired(Prefix, "Missing"));
        }
    }
}