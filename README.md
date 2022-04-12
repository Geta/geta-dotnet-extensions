
# .NET extensions and helpers library

![Build](http://tc.geta.no/app/rest/builds/buildType:(id:GetaPackages_GetaNetExtensions_00ci),branch:master/statusIcon)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Geta_geta-dotnet-extensions&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Geta_geta-dotnet-extensions)
~~~~[![Platform](https://img.shields.io/badge/Platform-.NET%20Standard%202.0-blue.svg?style=flat)](https://docs.microsoft.com/en-us/dotnet/core/)

## Description
Geta.Net.Extensions package contains common handlers, extension methods and other common utility methods that can be shared between multiple projects.

## How to get started?
```
dotnet add package Geta.Net.Extensions
```

## Features

Features included in package:
### Helpers

- `CultureInfoHelpers`
  - `Exists(string cultureName)` - Checks if there is a culture by the provided name.
- `GenerateRandomString`
  - `GenerateRandomString(int uppercaseChars, int lowerCaseChars, int digits, int symbols)` - Generates a random string.
- `QueryStringBuilder` - helps to build parametrized URI. Works both with absolute and relative URIs
  - `Add(string name, string value)` - adds string parameter to URL
  - `Add(string name, object value)` - adds object parameter to URL
  - `Remove(string name)` - removes parameter from URL by name
  - `Toggle(string name, string value)` - adds string parameter to query if it is not already present, otherwise it removes it.
  - `Toggle(string name, object value)` - adds object type parameter to query if it is not already present, otherwise it removes it.
  - `ToString()` - generates URL string. If URL is relative then only list of parameters are returned.

`QueryStringBuilder` examples:

```csharp
// Initialize builder with absolute url and parameters
var builder = new QueryStringBuilder("http://domain.com/?param=value");
// Add second parameter
builder.Add("param2", "object");
// Should return http://domain.com/?param=value&param2=object
var url = builder.ToString();
```
```csharp
// Initialize builder with relative url and parameters
var builder = new QueryStringBuilder("/?p1=o1");
// Add second parameter
builder.Add("p2", "o2");
// Should return /?p1=o1&p2=o2
var url = builder.ToString();
```

### Extensions
- `DateTimeExtensions`:
  - `ToEpochTime` - Returns a Unix Epoch time;
  - `EndOfDay` - Returns end of the day datetime `dd.mm.yyyy 23:59:59`;
  - `BeginningOfDay` - Returns beginning of the day datetime `dd.mm.yyyy 00:00:00`;
  - `IsToday` - Checks if date is today (Should be in UTC);
  - `IsTomorrow` - Checks if date is tomorrow (Should be in UTC);
  - `IsYesterday` - Checks if date is yesterday (Should be in UTC);
  - `ToTimestamp` - Converts datetime to timestamp.
- `EnumerableExtensions`:
  - `ForEach` - Applies an action on each item of the sequence;
  - `SafeOfType` - Filters the elements of an IEnumerable based on a specified type;
  - `OrEmptyIfNull` - Returns empty sequence if source sequence is null otherwise returns source sequence;
  - `IsNullOrEmpty` - Checks whether given sequence is null or empty;
  - `FilterPaging` - Filters by page and page size;
  - `Singleton` - Transforms item into IEnumerable with one item;
  - `Partition` - Splits IEnumerable into multiple partitions;
  - `DistinctBy` - Selects distinct values from list;
  - `Concat` - Can concatenate 2 or more sequences into signle.
- `StringExtensions`:
  - `JoinStrings` - Transforms list into a separated string;
  - `GenerateSlug`, `GenerateSlugWithoutHyphens` - Creates URL / Html friendly slug;
  - `TryParseInt32` - Parses string to nullable int (Int32);
  - `TryParseInt64` - Parses string to nullable long (Int64);
  - `TryParseBool` - Parses string to nullable boolean;
  - `TryParseTimeSpan` - Parses string to nullable TimeSpan;
  - `TryParseDecimal` - Parses string to nullable decimal;
  - `IsNullOrEmpty` - Answers true if this String is either null or empty;
  - `HasValue` - Answers true if this String is neither null or empty;
  - `HtmlEncode` - Encodes the string as HTML;
  - `HtmlDencode` - Decodes an HTML string;
  - `UrlEncode` - Encodes the string for URLs;
  - `UrlDecode` - Decodes a URL-encoded string;
  - `IsAbsoluteUrl` - Checks if a string is absolute URL;
  - `IsRelativeUrl` - Checks if a string is relative URL;
  - `GetHead` - Returns beginning of the string and adds ellipse `...` if string is longer that specified by length;
  - `GetTail` - Returns ending of the string and adds ellipse `...` if string is longer that specified by length;
  - `Capitalize` - Capitalizes every word (title case).
- `FluentExtensions` - Provides fluent way of chaining methods
    - `T If<T>(this T source, bool condition, Func<T, T> func)`
    - `T If<T>(this T source, Func<bool> condition, Func<T, T> func)`
    - `T If<T>(this T source, Func<T, bool> condition, Func<T, T> func)`
    - `T Fluent<T>(this T source, Action<T> action)`
   
Some `FluentExtension` examples:
```csharp  
var list = new List<string>()
                .Fluent(l => l.Add("Hello"))
                .Fluent(l => l.Add(", "))
                .Fluent(l => l.Add("World!")); 
```
```csharp  
string value1 = null;
var value2 = string.Empty;
var value3 = "Hello";

var list = new List<string>()
    .If(!string.IsNullOrEmpty(value1), l => l.FluentAdd(value1))
    .If(!string.IsNullOrEmpty(value2), l => l.FluentAdd(value2))
    .If(!string.IsNullOrEmpty(value3), l => l.FluentAdd(value3));
```
```csharp
var value = "Hello";

var list = new List<string>()
    .If(true, l =>
        l.If(() => !string.IsNullOrEmpty(value),
            l1 => l1.FluentAdd(value)));
```
