# .NET extensions and helpers library

![](http://tc.geta.no/app/rest/builds/buildType:(id:TeamFrederik_NetExtensions_BuildNuGetPackage)/statusIcon)
[![Platform](https://img.shields.io/badge/Platform-.NET%20Standard%202.0-blue.svg?style=flat)](https://docs.microsoft.com/en-us/dotnet/core/)

## Description
Geta.Net.Extensions package contains common handlers, extension methods and other common utility methods that can be shared between multiple projects.

## How to get started?
```
Install-Package Geta.Net.Extensions
```

## Features

Features included in package:
### Helpers

- `ConfigurationHelper` - Read app settings from config files. Key should be in format `{prefix}:{name}`:
  - `GetConfig<T>(string prefix, string name, T defaultValue)`
  - `GetConfigRequired(string prefix, string name)` - will throw `ConfigurationErrorsException` if config key will not be found;
  - `GetConfigRequired<T>(string prefix, string name)`
- `CultureInfoHelpers`
  - `Exists(string cultureName)` - Checks if there is a culture by the provided name.
- `GenerateRandomString`
  - `GenerateRandomString(int uppercaseChars, int lowerCaseChars, int digits, int symbols)` - Generates a random string.

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
  - `DistinctBy` - Selects distinct values from list.
- `StringExtensions`:
  - `JoinStrings` - Transforms list into a separated string;
  - `GenerateSlug`, `GenerateSlugWithoutHyphens` - Creates URL / Html friendly slug;
  - `TryParseInt32` - Parses string to nullable int (Int32);
  - `TryParseInt64` - Parses string to nullable long (Int64);
  - `IsNullOrEmpty` - Answers true if this String is either null or empty;
  - `HasValue` - Answers true if this String is neither null or empty;
  - `HtmlEncode` - Encodes the string as HTML;
  - `HtmlDencode` - Decodes an HTML string;
  - `UrlEncode` - Encodes the string for URLs;
  - `UrlDecode` - Decodes a URL-encoded string;
  - `IsAbsoluteUrl` - Checks if a string is absolute URL;
  - `IsRelativeUrl` - Checks if a string is relative URL.
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
