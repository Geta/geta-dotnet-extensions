using Geta.Net.Extensions.Generators;
using Xunit;

namespace Geta.Net.Extensions.Tests
{
    public class QueryStringBuilderTests
    {
        [Fact]
        public void Add_string_param_to_absolute_url()
        {
            var builder = new QueryStringBuilder("http://domain.com");
            builder.Add("p1", "v1");
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void Add_secondary_string_param_to_absolute_url()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", "v2");
            Assert.Equal("http://domain.com/?p1=v1&p2=v2", builder.ToString());
        }

        [Fact]
        public void Add_string_param_to_absolute_url_with_path()
        {
            var builder = new QueryStringBuilder("http://domain.com/some/path");
            builder.Add("p1", "v1");
            Assert.Equal("http://domain.com/some/path?p1=v1", builder.ToString());
        }

        [Fact]
        public void Add_object_param_to_absolute_url()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", 10);
            Assert.Equal("http://domain.com/?p1=v1&p2=10", builder.ToString());
        }

        [Fact]
        public void Add_handle_empty_string()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", "");
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void Add_handle_null_object()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", null);
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void Remove_parameter()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Remove("p1");
            Assert.Equal("http://domain.com/", builder.ToString());
        }

        [Fact]
        public void Remove_and_handle_non_existing_param()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Remove("p2");
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void Remove_handle_null()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Remove(null);
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void Toggle_param_should_remove()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Toggle("p1", "v2");
            Assert.Equal("http://domain.com/", builder.ToString());
        }

        [Fact]
        public void Toggle_param_should_add()
        {
            var builder = new QueryStringBuilder("http://domain.com/");
            builder.Toggle("p1", "v2");
            Assert.Equal("http://domain.com/?p1=v2", builder.ToString());
        }

        [Fact]
        public void Toggle_param_should_handle_null()
        {
            var builder = new QueryStringBuilder("http://domain.com/");
            builder.Toggle("p1", null);
            Assert.Equal("http://domain.com/", builder.ToString());
        }

        [Fact]
        public void Add_param_to_relative_url()
        {
            var builder = new QueryStringBuilder("/?p1=v1");
            builder.Add("p2", "v2");
            Assert.Equal("/?p1=v1&p2=v2", builder.ToString());
        }

        [Fact]
        public void Add_multiple_params_to_relative_url()
        {
            var builder = new QueryStringBuilder("/");
            builder.Add("p1", "v1");
            builder.Add("p2", "v2");
            builder.Add("p3", "v3");
            Assert.Equal("/?p1=v1&p2=v2&p3=v3", builder.ToString());
        }

        [Fact]
        public void Remove_param_from_relative_url()
        {
            var builder = new QueryStringBuilder("/?p1=v1");
            builder.Remove("p1");
            Assert.Equal("/", builder.ToString());
        }
    }
}
