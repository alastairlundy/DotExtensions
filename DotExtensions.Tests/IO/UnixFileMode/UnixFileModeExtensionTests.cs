/*
        MIT License
       
       Copyright (c) 2026 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using DotExtensions.IO.Permissions;
using DotExtensions.IO.Permissions.Unix;
using DotExtensions.IO.Permissions.Windows;
#pragma warning disable CS0618 // Type or member is obsolete

namespace DotExtensions.Tests.IO.UnixFileMode;

public class UnixFileModeExtensionTests
{
    [Test]
    public async Task UnixFileMode_HasExecutePermissions_True()
    {
        global::System.IO.UnixFileMode expectedUser =
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite | global::System.IO.UnixFileMode.UserExecute;
        global::System.IO.UnixFileMode expectedGroup =
            global::System.IO.UnixFileMode.GroupRead | global::System.IO.UnixFileMode.GroupWrite | global::System.IO.UnixFileMode.GroupExecute;
        global::System.IO.UnixFileMode expectedOthers =
            global::System.IO.UnixFileMode.OtherRead | global::System.IO.UnixFileMode.OtherWrite | global::System.IO.UnixFileMode.OtherExecute;

        await Assert.That(expectedUser.HasExecutePermission).IsTrue();
        await Assert.That(expectedGroup.HasExecutePermission).IsTrue();
        await Assert.That(expectedOthers.HasExecutePermission).IsTrue();
    }

    [Test]
    public async Task Parse_RwxSymbolNotation_755_ReturnsExpectedMode()
    {
        global::System.IO.UnixFileMode expected =
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite | global::System.IO.UnixFileMode.UserExecute |
            global::System.IO.UnixFileMode.GroupRead | global::System.IO.UnixFileMode.GroupExecute |
            global::System.IO.UnixFileMode.OtherRead | global::System.IO.UnixFileMode.OtherExecute;

        await Assert.That(global::System.IO.UnixFileMode.Parse("-rwxr-xr-x")).IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_RwxSymbolNotation_644_ReturnsExpectedMode()
    {
        global::System.IO.UnixFileMode expected =
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite |
            global::System.IO.UnixFileMode.GroupRead |
            global::System.IO.UnixFileMode.OtherRead;

        await Assert.That(global::System.IO.UnixFileMode.Parse("-rw-r--r--")).IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_RwxSymbolNotation_700_ReturnsExpectedMode()
    {
        global::System.IO.UnixFileMode expected =
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite | global::System.IO.UnixFileMode.UserExecute;

        await Assert.That(global::System.IO.UnixFileMode.Parse("-rwx------")).IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_RwxSymbolNotation_DirectoryFileTypeChar_ReturnsExpectedMode()
    {
        global::System.IO.UnixFileMode expected =
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite | global::System.IO.UnixFileMode.UserExecute |
            global::System.IO.UnixFileMode.GroupRead | global::System.IO.UnixFileMode.GroupExecute |
            global::System.IO.UnixFileMode.OtherRead | global::System.IO.UnixFileMode.OtherExecute;

        await Assert.That(global::System.IO.UnixFileMode.Parse("drwxr-xr-x")).IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_NumericNotation_4755_ReturnsSetUserAnd755()
    {
        global::System.IO.UnixFileMode expected =
            global::System.IO.UnixFileMode.SetUser |
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite | global::System.IO.UnixFileMode.UserExecute |
            global::System.IO.UnixFileMode.GroupRead | global::System.IO.UnixFileMode.GroupExecute |
            global::System.IO.UnixFileMode.OtherRead | global::System.IO.UnixFileMode.OtherExecute;

        await Assert.That(global::System.IO.UnixFileMode.Parse("4755")).IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_NumericNotation_1777_ReturnsStickyBitAnd777()
    {
        global::System.IO.UnixFileMode expected =
            global::System.IO.UnixFileMode.StickyBit |
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite | global::System.IO.UnixFileMode.UserExecute |
            global::System.IO.UnixFileMode.GroupRead | global::System.IO.UnixFileMode.GroupWrite | global::System.IO.UnixFileMode.GroupExecute |
            global::System.IO.UnixFileMode.OtherRead | global::System.IO.UnixFileMode.OtherWrite | global::System.IO.UnixFileMode.OtherExecute;

        await Assert.That(global::System.IO.UnixFileMode.Parse("1777")).IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_NumericNotation_2775_ReturnsSetGroupAnd775()
    {
        global::System.IO.UnixFileMode expected =
            global::System.IO.UnixFileMode.SetGroup |
            global::System.IO.UnixFileMode.UserRead | global::System.IO.UnixFileMode.UserWrite | global::System.IO.UnixFileMode.UserExecute |
            global::System.IO.UnixFileMode.GroupRead | global::System.IO.UnixFileMode.GroupWrite | global::System.IO.UnixFileMode.GroupExecute |
            global::System.IO.UnixFileMode.OtherRead | global::System.IO.UnixFileMode.OtherExecute;

        await Assert.That(global::System.IO.UnixFileMode.Parse("2775")).IsEqualTo(expected);
    }

    [Test]
    public async Task TryParse_InvalidNumericNotations_ReturnsFalse()
    {
        await Assert.That(global::System.IO.UnixFileMode.TryParse("189", out _)).IsFalse();
        await Assert.That(global::System.IO.UnixFileMode.TryParse(" 777", out _)).IsFalse();
    }

    [Test]
    public async Task IsValidNumericNotation_EmptyString_ReturnsFalseWithoutThrowing()
    {
        await Assert.That(string.Empty.IsValidNumericNotation()).IsFalse();
    }

    [Test]
    public async Task IsValidRwxSymbolNotation_VariousInputs_ReturnsExpected()
    {
        await Assert.That("-rwxr-xr-x".IsValidRwxSymbolNotation()).IsTrue();
        await Assert.That("----------".IsValidRwxSymbolNotation()).IsTrue(); //10 dashes: valid, all permissions unset.
        await Assert.That("not-a-mode!".IsValidRwxSymbolNotation()).IsFalse();
    }

    [Test]
    public async Task Parse_InvalidNumericNotation_ThrowsArgumentExceptionWithNotationParamName()
    {
        ArgumentException? thrown = null;

        try
        {
            _ = global::System.IO.UnixFileMode.Parse("089");
        }
        catch (ArgumentException exception)
        {
            thrown = exception;
        }

        await Assert.That(thrown).IsNotNull();
        await Assert.That(thrown!.ParamName).IsEqualTo("notation");
    }

    [Test]
    public async Task Parse_SymbolicSetUserId_IncludesSetUserFlag()
    {
        global::System.IO.UnixFileMode result = global::System.IO.UnixFileMode.Parse("-rwsr-xr-x");

        await Assert.That(result.HasFlag(global::System.IO.UnixFileMode.SetUser)).IsTrue();
    }

    [Test]
    public async Task Parse_SymbolicStickyBit_IncludesStickyBitFlag()
    {
        global::System.IO.UnixFileMode result = global::System.IO.UnixFileMode.Parse("-rwxr-xr-t");

        await Assert.That(result.HasFlag(global::System.IO.UnixFileMode.StickyBit)).IsTrue();
    }

    [Test]
    public async Task HasExecutePermission_DenyOnlyRule_ReturnsFalse()
    {
        if (!OperatingSystem.IsWindows())
            return;

        FileSecurity security = new();

        security.AddAccessRule(new FileSystemAccessRule(
            new SecurityIdentifier(WellKnownSidType.WorldSid, null),
            FileSystemRights.ExecuteFile,
            AccessControlType.Deny));

        AuthorizationRuleCollection rules = security.GetAccessRules(includeExplicit: true,
            includeInherited: false, typeof(SecurityIdentifier));

        await Assert.That(rules.HasExecutePermission).IsFalse();
    }

    [Test]
    public async Task HasExecutePermission_AllowRule_ReturnsTrue()
    {
        if (!OperatingSystem.IsWindows())
            return;

        FileSecurity security = new();

        security.AddAccessRule(new FileSystemAccessRule(
            new SecurityIdentifier(WellKnownSidType.WorldSid, null),
            FileSystemRights.ExecuteFile,
            AccessControlType.Allow));

        AuthorizationRuleCollection rules = security.GetAccessRules(includeExplicit: true,
            includeInherited: false, typeof(SecurityIdentifier));

        await Assert.That(rules.HasExecutePermission).IsTrue();
    }
}
