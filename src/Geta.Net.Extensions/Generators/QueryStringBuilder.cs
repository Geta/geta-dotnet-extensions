using System;
using System.Linq;
using System.Web;

namespace Geta.Net.Extensions.Generators
{
    /// <summary>
    ///     Helper class for creating and modifying URL's parameters.
    /// </summary>
    public class QueryStringBuilder
    {
        private readonly UriBuilder _uriBuilder;
        private readonly bool _isRelative;

        /// <summary>
        ///     Instantiates new QueryStringBuilder with provided URL.
        /// </summary>
        /// <param name="url">URL for which to build QueryStringBuilder.</param>
        public QueryStringBuilder(string url)
        {
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            {
                if (uri.IsAbsoluteUri)
                {
                    _uriBuilder = new UriBuilder(uri);
                }
                else
                {
                    _uriBuilder = new UriBuilder(CreateDummyDomain(uri));
                    _isRelative = true;
                }
            }
        }

        /// <summary>
        ///     Instantiates new QueryStringBuilder with provided Uri.
        /// </summary>
        /// <param name="uri">Uri for which to build QueryStringBuilder.</param>
        public QueryStringBuilder(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (uri.IsAbsoluteUri)
            {
                _uriBuilder = new UriBuilder(uri);
            }
            else
            {
                _uriBuilder = new UriBuilder(CreateDummyDomain(uri));
                _isRelative = true;
            }
        }

        /// <summary>
        ///     Factory method for instantiating new QueryStringBuilder with provided URL.
        /// </summary>
        /// <param name="url">URL for which to build query.</param>
        /// <returns>Instance of QueryStringBuilder.</returns>
        public static QueryStringBuilder Create(string url)
        {
            return new QueryStringBuilder(url);
        }

        /// <summary>
        ///     Factory method for instantiating new QueryStringBuilder with provided Uri.
        /// </summary>
        /// <param name="uri">Uri for which to build query.</param>
        /// <returns>Instance of QueryStringBuilder.</returns>
        public static QueryStringBuilder Create(Uri uri)
        {
            return new QueryStringBuilder(uri);
        }

        /// <summary>
        ///     Adds query string parameter to query URL encoded.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        /// <returns>Instance of QueryStringBuilder.</returns>
        public QueryStringBuilder Add(string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var query = HttpUtility.ParseQueryString(_uriBuilder.Query);
                query[name] = value;
                _uriBuilder.Query = query.ToString();
            }

            return this;
        }

        /// <summary>
        ///     Adds query string parameter to query URL encoded.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        /// <returns>Instance of QueryStringBuilder.</returns>
        public QueryStringBuilder Add(string name, object value)
        {
            return Add(name, value?.ToString());
        }

        /// <summary>
        ///     Removes query string parameter from query.
        /// </summary>
        /// <param name="name">Name of parameter to remove.</param>
        /// <returns>Instance of QueryStringBuilder.</returns>
        public QueryStringBuilder Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this;
            }

            var query = HttpUtility.ParseQueryString(_uriBuilder.Query);
            query.Remove(name);
            _uriBuilder.Query = query.ToString();

            return this;
        }

        /// <summary>
        ///     Adds query string parameter to query string if it is not already present, otherwise it removes it.
        /// </summary>
        /// <param name="name">Name of parameter to add or remove.</param>
        /// <param name="value">Value of parameter to add.</param>
        /// <returns>Instance of modified QueryStringBuilder.</returns>
        public QueryStringBuilder Toggle(string name, string value)
        {
            if (string.IsNullOrEmpty(name) || value == null)
            {
                return this;
            }

            var query = HttpUtility.ParseQueryString(_uriBuilder.Query);
            var exists = query.AllKeys.Any(x => x == name);
            return exists ? Remove(name) : Add(name, value);
        }

        /// <summary>
        ///     Adds query string parameter to query string if it is not already present, otherwise it removes it.
        /// </summary>
        /// <param name="name">Name of parameter to add or remove.</param>
        /// <param name="value">Value of parameter to add.</param>
        /// <returns>Instance of modified QueryStringBuilder.</returns>
        public QueryStringBuilder Toggle(string name, object value)
        {
            return Toggle(name, value?.ToString());
        }

        /// <summary>
        ///     Returns string representation of URL with query string.
        /// </summary>
        /// <returns>String representation of URL with query string.</returns>
        public override string ToString()
        {
            return !_isRelative ? _uriBuilder.Uri.AbsoluteUri : _uriBuilder.Uri.PathAndQuery;
        }

        private Uri CreateDummyDomain(Uri uri)
        {
            var baseUri = new Uri("http://dummy.com");
            var dummyUri = new Uri(baseUri, uri);
            return dummyUri;
        }
    }
}
