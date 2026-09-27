# DotExtensions
An extension member library that enhances the experience of using .NET's types with useful features.

**NOTE**: Version 9.0 and onwards requires projects using DotExtension to set the C# language version to 14 or higher.

[![NuGet](https://img.shields.io/nuget/v/DotExtensions.svg)](https://www.nuget.org/packages/DotExtensions/)
[![Latest Pre-release NuGet](https://img.shields.io/nuget/vpre/DotExtensions.svg)](https://www.nuget.org/packages/DotExtensions/)
[![NuGet](https://img.shields.io/nuget/dt/DotExtensions.svg)](https://www.nuget.org/packages/DotExtensions/)
![License](https://img.shields.io/github/license/alastairlundy/DotExtensions)


## Table of Contents
* [Features](#features)
* [Installing](#how-to-install-and-use-dotextensions)
    * [Compatibility](#compatibility)
* [Migrating to v11](#migrating-to-v11)
* [Contributing](#how-to-contribute)
* [Roadmap](#roadmap)
* [License](#license)

## Features
* Empty directory detection via the ``IsEmpty`` extension property for the ``DirectoryInfo`` class.
* Support for comparing versions via easy-to-understand methods e.g. ``version.IsNewerThan(Version otherVersion)`` etc.
* DateOnly week-number extensions e.g. ``WeekOfMonth`` and ``WeekOfYear`` with an explicit ``CultureInfo``
* Support for Detecting and Removing Special Characters
* Support for Detecting and Removing Escape Characters

^1 - StringSegment extensions are part of the ``DotExtensions.MsExtensions`` package.

## How to install and use DotExtensions
DotExtensions can be installed via the .NET SDK CLI, Nuget via your IDE or code editor's package interface, or via the Nuget website.

| Package Name               | Nuget Link                                                                                | .NET SDK CLI command                              |
|----------------------------|-------------------------------------------------------------------------------------------|---------------------------------------------------|
| DotExtensions              | [DotExtensions Nuget](https://nuget.org/packages/DotExtensions)                           | ``dotnet add package DotExtensions``              |
| DotExtensions.Memory       | [DotExtensions.Memory Nuget](https://nuget.org/packages/DotExtensions.Memory)             | ``dotnet add package DotExtensions.Memory``       |


### Compatibility
DotExtensions supports:
* .NET 10

However, it is important to note that not all features may be supported by all TFMs. 

**Note for DateOnly**: Though DateOnly was originally part of .NET 6, this library's DateOnly extension methods require .NET 10.

## Migrating to v11
Version 11 reduces the library's public surface: the Dates area loses its arithmetic and day-count members, and the three ``ArgumentExceptionExtensions`` guard modules merge into one type. The tables below name the replacement for every removed member family; the full entry lives in the [CHANGELOG](https://github.com/alastairlundy/DotExtensions/blob/main/CHANGELOG.md).

**Dates** - every replacement is a plain BCL or C# construct:

| Removed member family | Use instead |
|---|---|
| ``Add(DateTime)``, ``Subtract(DateTime)``, ``SubtractMonths(double)``, ``SubtractYears(double)`` | BCL operators ``a + b`` and ``a - b``, or ``d.AddMonths(-n)``, ``d.AddYears(-n)`` |
| ``Difference`` overloads (``DateTime``, ``TimeOnly``, ``DateOnly``) | ``(a - b).Duration()`` |
| ``DateOnly`` ``SubtractDays`` / ``SubtractMonths`` / ``SubtractYears`` | ``d.AddDays(-n)``, ``d.AddMonths(-n)``, ``d.AddYears(-n)`` |
| ``DateOnly`` ``ToDateTime`` extension | BCL ``dateOnly.ToDateTime()`` and ``DateOnly.FromDateTime(...)`` |
| ``CalculateDayOfWeekAsInteger`` (both receivers) | compose ``date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)date.DayOfWeek`` |
| ``CalculateNumberOfDaysInYear`` (both receivers) | ``DateTime.IsLeapYear(date.Year) ? 366 : 365`` |
| ``WeekOfMonth`` / ``WeekOfYear`` week properties (``DateTime`` and ``DateOnly`` receivers) and the optional ``CalendarWeekRule`` parameter | both members are ``DateOnly``-only and take a required ``CultureInfo``: ``dateOnly.WeekOfMonth(culture)`` and ``dateOnly.WeekOfYear(culture)`` (``WeekOfMonth`` returns ``0`` for a date before the culture's first week start) |

**Guards** - the surviving surface is the eight-member ``ArgumentExceptionExtensions`` in ``DotExtensions.Exceptions``:

| Removed member family | Use instead |
|---|---|
| ``ThrowIfNullOrEmpty(SecureString)``, ``ThrowIfNullOrWhiteSpace(SecureString)`` | BCL ``ArgumentNullException.ThrowIfNull(secureString)`` plus a local length check (``secureString.Length == 0``) |
| ``ThrowIfOSPlatform`` / ``ThrowIfNotOSPlatform`` (all overloads) | BCL ``OperatingSystem.IsOSPlatform(...)`` plus a caller-thrown ``PlatformNotSupportedException`` |
| The ``DotExtensions.MsExtensions.Primitives`` and ``DotExtensions.Memory.Exceptions`` ``ArgumentExceptionExtensions`` types | the single ``ArgumentExceptionExtensions`` in ``DotExtensions.Exceptions`` - member names are unchanged and ``paramName`` is now captured via ``CallerArgumentExpression`` |
| ``ThrowIfNullOrWhitespace`` (old casing) | ``ThrowIfNullOrWhiteSpace`` |
| ``ThrowIfEmptyOrWhiteSpace`` over spans/memory of ``string``, and the public ``ThrowIfSpanIsEmpty`` / ``ThrowIfMemoryIsEmpty`` guards | the surviving ``char``-family ``ThrowIfEmptyOrWhiteSpace`` guards; for other element types, a local emptiness check |

## How to Build the code
Please see [Building.md](https://github.com/alastairlundy/DotExtensions/blob/main/docs/Building.md).

## How to Contribute
Thank you in advance for considering contributing to DotExtensions.

Please see the [CONTRIBUTING.md file](https://github.com/alastairlundy/DotExtensions/blob/main/CONTRIBUTING.md) for code and localization contributions.

If you want to file a bug report or suggest a potential feature to add, please check out the [GitHub issues page](https://github.com/alastairlundy/DotExtensions/issues/) to see if a similar or identical issue is already open.
If there is not already a relevant issue filed, please [file one here](https://github.com/alastairlundy/DotExtensions/issues/new) and follow the respective guidance from the appropriate issue template.

## Roadmap
DotExtensions aims to make working with different types in the System namespace in C# easier.

All stable releases must be stable and should not contain regressions.

Future updates should aim to focus on one or more of the following:
* Adding extension methods that improve ease of use
* Enhancing existing extension methods

**Note**: This library is not a primitives library and does not seek to add new interfaces or implementations of interfaces. It is purely a library for extension methods.

## License
This project is licensed under the MIT license.