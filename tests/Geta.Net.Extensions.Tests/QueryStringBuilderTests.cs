using System;
using System.Collections.Generic;
using System.Text;
using Geta.Net.Extensions.Generators;
using Xunit;

namespace Geta.Net.Extensions.Tests
{
    public class QueryStringBuilderTests
    {
        [Fact]
        public void AddStringParameterToAbsoluteUrl()
        {
            var builder = new QueryStringBuilder("http://domain.com");
            builder.Add("p1", "v1");
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void AddSecondaryStringParameterToAbsoluteUrl()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", "v2");
            Assert.Equal("http://domain.com/?p1=v1&p2=v2", builder.ToString());
        }

        [Fact]
        public void AddStringParameterToAbsoluteUrlWithPath()
        {
            var builder = new QueryStringBuilder("http://domain.com/some/path");
            builder.Add("p1", "v1");
            Assert.Equal("http://domain.com/some/path?p1=v1", builder.ToString());
        }

        [Fact]
        public void AddObjectParameterToAbsoluteUrl()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", 10);
            Assert.Equal("http://domain.com/?p1=v1&p2=10", builder.ToString());
        }

        [Fact]
        public void AddHandleEmptyString()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", "");
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void AddHandleNullObject()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Add("p2", null);
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void RemoveParameter()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Remove("p1");
            Assert.Equal("http://domain.com/", builder.ToString());
        }

        [Fact]
        public void RemoveHandleNonExistingParameter()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Remove("p2");
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void RemoveHandleNull()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Remove(null);
            Assert.Equal("http://domain.com/?p1=v1", builder.ToString());
        }

        [Fact]
        public void ToggleParameterRemove()
        {
            var builder = new QueryStringBuilder("http://domain.com/?p1=v1");
            builder.Toggle("p1", "v2");
            Assert.Equal("http://domain.com/", builder.ToString());
        }

        [Fact]
        public void ToggleParameterAdd()
        {
            var builder = new QueryStringBuilder("http://domain.com/");
            builder.Toggle("p1", "v2");
            Assert.Equal("http://domain.com/?p1=v2", builder.ToString());
        }

        [Fact]
        public void ToggleHandleNull()
        {
            var builder = new QueryStringBuilder("http://domain.com/");
            builder.Toggle("p1", null);
            Assert.Equal("http://domain.com/", builder.ToString());
        }

        [Fact]
        public void AddParameterToRelative()
        {
            var builder = new QueryStringBuilder("/?p1=v1");
            builder.Add("p2", "v2");
            Assert.Equal("/?p1=v1&p2=v2", builder.ToString());
        }

        [Fact]
        public void AddMultipleParametersToRelative()
        {
            var builder = new QueryStringBuilder("/");
            builder.Add("p1", "v1");
            builder.Add("p2", "v2");
            builder.Add("p3", "v3");
            Assert.Equal("/?p1=v1&p2=v2&p3=v3", builder.ToString());
        }

        [Fact]
        public void RemoveParameterFromRelative()
        {
            var builder = new QueryStringBuilder("/?p1=v1");
            builder.Remove("p1");
            Assert.Equal("/", builder.ToString());
        }
    }
}
