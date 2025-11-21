# DotExtensions.MsExtensions
An extension method library that enhances the experience of using Microsoft.Extensions.* packages with useful features.

Version 9.0 and onwards requires .csproj C# language version to be set to 14 or higher.

[DotExtensions](https://www.nuget.org/packages/AlastairLundy.DotExtensions.MsExtensions/) [![NuGet](https://img.shields.io/nuget/v/AlastairLundy.DotExtensions.MsExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions.MsExtensions/)  [![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.DotExtensions.MsExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions.MsExtensions/)

## Table of Contents
* [Features](#features)
* [Installing](#how-to-install-and-use-dotextensionsmsextensions)
    * [Compatibility](#compatibility)
* [Contributing](#how-to-contribute)
* [Roadmap](#roadmap)
* [License](#license)

## Features
* Helpful ``StringSegment`` extension methods:
  * ``Contains(char)`` and ``Contains(StringSegment)``
  * ``Reverse(StringSegment)``
  * ``IsEmpty`` and ``IsNullOrWhiteSpace(StringSegment)``
  * ``ToCharArray(StringSegment)``
* Helpful ``StringValues`` extension methods:
  * ``IsEmpty`` and ``IsNullOrWhiteSpace(StringValues)``

## How to install and use DotExtensions.MsExtensions
DotExtensions.MsExtensions can be installed via the .NET SDK CLI, Nuget via your IDE or code editor's package interface, or via the Nuget website.

| Package Name                             | Nuget Link                                                                                               | .NET SDK CLI command                                            |
|------------------------------------------|----------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------|
| AlastairLundy.DotExtensions.MsExtensions | [AlastairLundy.DotExtensions Nuget](https://nuget.org/packages/AlastairLundy.DotExtensions.MsExtensions) | ``dotnet add package AlastairLundy.DotExtensions.MsExtensions`` |


### Compatibility
DotExtensions.MsExtensions supports:
* .NET Standard 2.0
* .NET 8
* .NET 9
* .NET 10

However, it is important to note that not all features may be supported by all TFMs.

## Roadmap
DotExtensions.MsExtensions aims to make working with different types in the Microsoft.Extensions.* packages in .NET easier.

All stable releases must be stable and should not contain regressions.

Future updates should aim to focus on one or more of the following:
* Adding extension methods that improve ease of use
* Enhancing existing extension methods

**Note**: This library is not a primitive library and does not seek to add new interfaces or implementations of interfaces. It is just a library for extension methods.

## License
This project is licensed under the MIT license.
