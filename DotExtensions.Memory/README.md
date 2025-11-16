# DotExtensions.Memory
A collection of useful Span<T> and Memory<T> related extension methods for .NET.

**NOTE**: Version 9.0 and onwards requires projects using DotExtension.Memory to set language version to 14 or higher.


 [![NuGet](https://img.shields.io/nuget/v/AlastairLundy.DotExtensions.Memory.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions.Memory/)  
 [![Latest Pre-release NuGet](https://img.shields.io/nuget/vpre/AlastairLundy.DotExtensions.Memory.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions.memory/)
 [![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.DotExtensions.Memory.svg)](https://www.nuget.org/packages/AlastairLundy.DotExtensions.Memory/)
[![License](https://img.shields.io/github/license/alastairlundy/DotExtensions)]()

## Table of Contents
* [Features](#features)
* [Installing](#how-to-install-and-use-dotextensionsmemory)
    * [Compatibility](#compatibility)
* [License](#license)

## Features
* Helpful ``Span<T>`` extension methods:
    * ``OptimisticCopy`` and ``CopyTo``
    * ``Resize``
    * ``IsEmptyOrWhiteSpace`` for ``Span<string>``
    * ``ToList``

## How to install and use DotExtensions.Memory
DotExtensions.Memory can be installed via the .NET SDK CLI, Nuget via your IDE or code editor's package interface, or via the Nuget website.

| Package Name                       | Nuget Link                                                                                                | .NET SDK CLI command                                      |
|------------------------------------|-----------------------------------------------------------------------------------------------------------|-----------------------------------------------------------|
| AlastairLundy.DotExtensions.Memory | [AlastairLundy.DotExtensions.Memory Nuget](https://nuget.org/packages/AlastairLundy.DotExtensions.Memory) | ``dotnet add package AlastairLundy.DotExtensions.Memory`` |


### Compatibility
DotExtensions.Memory supports:
* .NET Standard 2.0
* .NET 8
* .NET 9
* .NET 10

However, it is important to note that not all features may be supported by all TFMs.

**Note**: This library is not a primitive library and does not seek to add new interfaces or implementations of interfaces. It is just a library for extension members.

## License
This project is licensed under the MIT license.
