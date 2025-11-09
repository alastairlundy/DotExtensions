# DotExtensions
An extension method library that enhances the experience of using .NET's types with useful features.
 
[![NuGet](https://img.shields.io/nuget/v/AlastairLundy.DotExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions/)
[![NuGet](https://img.shields.io/nuget/vpre/AlastairLundy.DotExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions/)
[![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.DotExtensions.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions/)
![License](https://img.shields.io/github/license/alastairlundy/DotExtensions)

## Table of Contents
* [Features](#features)
* [Installing](#how-to-install-and-use-dotextensions)
    * [Compatibility](#compatibility)
* [Contributing](#how-to-contribute)
* [License](#license)

## Features
* Empty directory detection via ``IsDirectoryEmpty`` extension method for the ``DirectoryInfo`` class.
* Support for comparing versions via easy-to-understand methods e.g. ``version.IsNewerThan(Version otherVersion)`` etc.
* DateTime and DateOnly extensions e.g. DateOnly's ToDateTime extension
* Support for Detecting and Removing Special Characters
* Support for Detecting and Removing Escape Characters

^1 - StringSegment extensions are part of the ``AlastairLundy.DotExtensions.MsExtensions`` package.

## How to install and use DotExtensions
DotExtensions can be installed via the .NET SDK CLI, Nuget via your IDE or code editor's package interface, or via the Nuget website.

| Package Name                             | Nuget Link                                                                                                            | .NET SDK CLI command                                            |
|------------------------------------------|-----------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------|
| AlastairLundy.DotExtensions              | [AlastairLundy.DotExtensions Nuget](https://nuget.org/packages/AlastairLundy.DotExtensions)                           | ``dotnet add package AlastairLundy.DotExtensions``              |
| AlastairLundy.DotExtensions.MsExtensions | [AlastairLundy.DotExtensions.MsExtensions Nuget](https://nuget.org/packages/AlastairLundy.DotExtensions.MsExtensions) | ``dotnet add package AlastairLundy.DotExtensions.MsExtensions`` |


### Compatibility
DotExtensions supports:
* .NET Standard 2.0 (Limited Support)
* .NET 8
* .NET 9

However, it is important to note that not all TFMs may support all features. 

**Note for DateOnly**: Though DateOnly was originally part of .NET 6, this library's DateOnly extension methods are only supported for .NET 8 and .NET 9 users since we don't target .NET 6 as a TFM due to being out of support.

## How to Contribute
Thank you in advance for considering contributing to DotExtensions.

Please see the [CONTRIBUTING.md file](https://github.com/alastairlundy/DotExtensions/blob/main/CONTRIBUTING.md) for code and localization contributions.

If you want to file a bug report or suggest a potential feature to add, please check out the [GitHub issues page](https://github.com/alastairlundy/DotExtensions/issues/) to see if a similar or identical issue is already open.
If there is not already a relevant issue filed, please [file one here](https://github.com/alastairlundy/DotExtensions/issues/new) and follow the respective guidance from the appropriate issue template.

## License
This project is licensed under the MIT license.
