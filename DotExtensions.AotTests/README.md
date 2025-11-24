DotExtensions.AotTests
======================

Purpose
-------
This small console application is intended to consume the DotExtensions libraries in an AoT-friendly way to help validate AoT (NativeAOT) compatibility.

Build
-----
You can build the project normally:

- dotnet build DotExtensions.AotTests

Publish with NativeAOT
----------------------
To produce a NativeAOT binary, use a supported runtime identifier (RID) for your platform and set PublishAot=true. Examples:

- Linux x64:
  - dotnet publish DotExtensions.AotTests -c Release -r linux-x64 -p:PublishAot=true --self-contained true

- Windows x64:
  - dotnet publish DotExtensions.AotTests -c Release -r win-x64 -p:PublishAot=true --self-contained true

- macOS Apple Silicon:
  - dotnet publish DotExtensions.AotTests -c Release -r osx-arm64 -p:PublishAot=true --self-contained true

Note:
- The project enables AoT analyzers during normal builds to surface potential issues early.
- Some APIs in the libraries may be conditionally compiled or marked as not AoT-safe; the test app avoids reflection/dynamic features and uses representative APIs.
