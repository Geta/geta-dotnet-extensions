
# Changelog

All notable changes to this project will be documented in this file.

## [3.0.0]
 
- Removed `ConfigurationHelper`

## [2.1.3]

- `Enumerable<T>` extension methods: `ForEach`, `Concat`;
- `String` extension methods: `GetHead`, `GetTail`, `TryParseBool`, `TryParseTimeSpan`, `Capitalize`;

## [2.1.2]

- Fixed `System.Configuration.ConfigurationManager` version in nuspec.

## [2.0.1]

### Added
- `QueryStringBuilder` helper class to build absolute and relative urls with parameters

## [2.0.0]

### Added
- .NET Standard 2.0 support;
- `DateTime` extension methods: `EndOfDay`, `BeginningOfDay`, `IsToday`, `IsTomorrow`, `IsYesterday`, `ToTimestamp`;
- `Enumerable` extension methods: `Singleton`, `Partition`, `DistinctBy`;
- `String` extension methods: `TryParseDecimal`. 

## [1.0.0]

Initial release.
