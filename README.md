# DotExtensions
An extension method library that enhances the experience of using .NET's types with useful features.

[DotExtensions](https://www.nuget.org/packages/AlastairLundy.DotExtensions/) [![NuGet](https://img.shields.io/nuget/v/AlastairLundy.DotExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions/)  [![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.DotExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions/)

## Table of Contents
* [Features](#features)
* [Installing](#how-to-install-and-use-dotextensions)
    * [Compatibility](#compatibility)
* [Contributing](#how-to-contribute)
* [Roadmap](#roadmap)
* [License](#license)

## Features
* Brings LINQ style extension methods to Span<T> and StringSegment ^1
* A number of IEnumerable<T>, ICollection<T>, and IList<T> extension methods including ``AddRange``, ``GetRange``, ``RemoveRange`` and more.
* Empty directory detection via ``IsDirectoryEmpty`` extension method for the ``DirectoryInfo`` class.
* Support for comparing versions via easy to understand methods e.g. ``version.IsNewerThan(Version otherVersion)`` etc.
* DateTime and DateOnly extensions e.g. DateOnly's ToDateTime extension
* Support for Detecting and Removing Special Characters
* Support for Detecting and Removing Escape Characters

^1 - StringSegment extensions are part of the ``AlastairLundy.DotExtensions.MsExtensions`` package.

## How to install and use DotExtensions
DotExtensions can be installed via the .NET SDK CLI, Nuget via your IDE or code editor's package interface, or via the Nuget website.

| Package Name                | Nuget Link                                                                                  | .NET SDK CLI command                               |
|-----------------------------|---------------------------------------------------------------------------------------------|----------------------------------------------------|
| AlastairLundy.DotExtensions | [AlastairLundy.DotExtensions Nuget](https://nuget.org/packages/AlastairLundy.DotExtensions) | ``dotnet add package AlastairLundy.DotExtensions`` |


### Compatibility
DotExtensions supports:
* .NET 8
* .NET 9
* .NET Standard 2.0

However, it is important to note that not all features may be supported by all TFMs. 

**Note for DateOnly**: Though DateOnly was originally part of .NET 6, this library's DateOnly extension methods are only supported for .NET 8 and .NET 9 users since we don't target .NET 6 as a TFM due to being out of support.

## How to Build the code

### Requirements
DotExtensions requires the latest .NET release SDK to be installed to target all supported TFM (Target Framework Moniker) build targets.

Currently, the required .NET SDK is .NET 9.

The current build targets include:
* .NET 8
* .NET 9
* .NET Standard 2.0

Any version of the .NET 9 SDK can be used, but using the latest version is preferred.

### Versioning new releases
DotExtensions aims to follow Semantic versioning with ```[Major].[Minor].[Build]``` for most circumstances and an optional ``.[Revision]`` when only a configuration change is made, or a new build of a preview release or a new build of a previous release is made.

#### Pre-releases
Pre-release versions should have a suffix of -alpha, -beta, -rc, or -preview followed by a ``.`` and what pre-release version number they are. The number should be incremented by 1 after each release unless it only contains a configuration change, or another packaging, or build change. An example pre-release version may look like 1.1.0-alpha.2 , this version string would indicate it is the 2nd alpha pre-release version of 1.1.0 .

#### Stable Releases
Stable versions should follow semantic versioning and should only increment the Revision number if a release only contains configuration or build packaging changes, with no change in functionality, features, or even bug or security fixes.

Releases that only implement bug fixes should see the Build version incremented.

Releases that add new non-breaking changes should increment the Minor version. Minor breaking changes may be permitted in Minor version releases where doing so is necessary to maintain compatibility with an existing supported platform, or an existing piece of code that requires a breaking change to continue to function as intended.

Releases that add major breaking changes or significantly affect the API should increment the Major version. Major version releases should not be released with excessive frequency and should be released when there is a genuine need for the API to change significantly for the improvement of the project.


### Building for Testing
You can build for testing by building the project within your IDE or VS Code, or manually by entering the following command: ``dotnet build -c Debug``.

If you encounter any bugs or issues, try testing your code with breakpoints in the affected code where appropriate. Failing that, please [report the issue](https://github.com/alastairlundy/DotExtensions/issues/new/) if one doesn't already exist for the bug(s).

### Building for Release
Before building a release build, ensure you apply the relevant changes to the ``AlastairLundy.DotExtensions.csproj`` file:
* Update the Package Version variable
* Update the project file's Changelog

You should ensure the project builds under debug settings before producing a release build.

#### Producing Release Builds
To manually build for release, enter ``dotnet build -c Release`` for a release with [SourceLink](https://github.com/dotnet/sourcelink) enabled.

Builds should generally always include SourceLink and symbol packages if intended for wider distribution.

## How to Contribute
Thank you in advance for considering contributing to DotExtensions .

Please see the [CONTRIBUTING.md file](https://github.com/alastairlundy/DotExtensions/blob/main/CONTRIBUTING.md) for code and localization contributions.

If you want to file a bug report or suggest a potential feature to add, please check out the [GitHub issues page](https://github.com/alastairlundy/DotExtensions/issues/) to see if a similar or identical issue is already open.
If there is not already a relevant issue filed, please [file one here](https://github.com/alastairlundy/DotExtensions/issues/new) and follow the respective guidance from the appropriate issue template.

Thanks.

## Roadmap
DotExtensions aims to make working with different types in the System namespace in C# easier.

All stable releases must be stable and should not contain regressions.

Future updates should aim focus on one or more of the following:
* Adding extension methods that improve ease of use
* Enhancing existing extension methods

**Note**: This library is not a primitive library and also does not seek to add new interfaces or implementations of interfaces. It is purely a library for extension methods.

## License
This project is licensed under the MIT license.
