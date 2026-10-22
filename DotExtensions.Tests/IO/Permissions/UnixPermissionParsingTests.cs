using System;
using System.IO;
using DotExtensions.IO.Permissions.Unix;

namespace DotExtensions.Tests.IO.Permissions;

public class UnixPermissionParsingTests
{
    private static readonly UnixFileMode AllUserPermissions =
        UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute;

    private static readonly UnixFileMode AllGroupPermissions =
        UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute;

    private static readonly UnixFileMode AllOtherPermissions =
        UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute;

    private static readonly UnixFileMode AllRwxPermissions =
        AllUserPermissions | AllGroupPermissions | AllOtherPermissions;

    [Test]
    public async Task Parse_ThreeDigit777_ReturnsAllRwxPermissions()
    {
        UnixFileMode result = UnixFileMode.Parse("777");

        await Assert.That(result)
            .IsEqualTo(AllRwxPermissions);

        await Assert.That(result.HasFlag(UnixFileMode.SetUser)).IsFalse();
        await Assert.That(result.HasFlag(UnixFileMode.SetGroup)).IsFalse();
        await Assert.That(result.HasFlag(UnixFileMode.StickyBit)).IsFalse();
    }

    [Test]
    public async Task Parse_ThreeDigit511_ReturnsUserRwExecuteGroupAndOtherExecuteOnly()
    {
        UnixFileMode result = UnixFileMode.Parse("511");

        UnixFileMode expected =
            UnixFileMode.UserRead | UnixFileMode.UserExecute |
            UnixFileMode.GroupExecute |
            UnixFileMode.OtherExecute;

        await Assert.That(result)
            .IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_FourDigit4777_MapsFirstDigitToStickyBit()
    {
        UnixFileMode result = UnixFileMode.Parse("4777");

        UnixFileMode expected = UnixFileMode.StickyBit | AllRwxPermissions;

        await Assert.That(result)
            .IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_FourDigit2775_MapsFirstDigitToSetGroupAndLastThreeToPermissions()
    {
        UnixFileMode result = UnixFileMode.Parse("2775");

        UnixFileMode expected =
            UnixFileMode.SetGroup |
            AllUserPermissions |
            AllGroupPermissions |
            UnixFileMode.OtherRead | UnixFileMode.OtherExecute;

        await Assert.That(result)
            .IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_FourDigit1000_MapsFirstDigitToSetUserOnly()
    {
        UnixFileMode result = UnixFileMode.Parse("1000");

        await Assert.That(result)
            .IsEqualTo(UnixFileMode.SetUser);
    }

    [Test]
    public async Task Parse_ThreeDigitWithLeadingZero077_KeepsLeadingZeroAsUserDigit()
    {
        UnixFileMode result = UnixFileMode.Parse("077");

        UnixFileMode expected = AllGroupPermissions | AllOtherPermissions;

        await Assert.That(result)
            .IsEqualTo(expected);

        // Regression: "077" used to be unpadded and misparsed as "777".
        await Assert.That(result.HasFlag(UnixFileMode.UserRead)).IsFalse();
        await Assert.That(result.HasFlag(UnixFileMode.UserWrite)).IsFalse();
        await Assert.That(result.HasFlag(UnixFileMode.UserExecute)).IsFalse();
    }

    [Test]
    public async Task Parse_NegativeNumericNotation_ThrowsArgumentException()
    {
        // Regression: signed input used to slip through int.TryParse-based validation.
        await Assert.That(() =>
        {
            UnixFileMode.Parse("-777");
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task Parse_PrefixedNumericNotation_ThrowsArgumentException()
    {
        await Assert.That(() =>
        {
            UnixFileMode.Parse("+777");
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task Parse_SymbolicRwRReadRead_ReturnsUserWriteAndReadOnlyBits()
    {
        UnixFileMode result = UnixFileMode.Parse("-rw-r--r--");

        UnixFileMode expected =
            UnixFileMode.UserRead | UnixFileMode.UserWrite |
            UnixFileMode.GroupRead |
            UnixFileMode.OtherRead;

        await Assert.That(result)
            .IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_SymbolicDirectoryRwxRXRx_ReturnsExpectedBits()
    {
        UnixFileMode result = UnixFileMode.Parse("drwxr-xr-x");

        UnixFileMode expected =
            UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute |
            UnixFileMode.GroupRead | UnixFileMode.GroupExecute |
            UnixFileMode.OtherRead | UnixFileMode.OtherExecute;

        await Assert.That(result)
            .IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_SymbolicRwxForEveryone_ReturnsAllRwxPermissions()
    {
        UnixFileMode result = UnixFileMode.Parse("-rwxrwxrwx");

        await Assert.That(result)
            .IsEqualTo(AllRwxPermissions);
    }

    [Test]
    public async Task Parse_SymbolicWithoutTypeCharacter_IsAccepted()
    {
        // The type character is optional: "rw-r--r--" is structurally identical to "-rw-r--r--".
        UnixFileMode result = UnixFileMode.Parse("rw-r--r--");

        UnixFileMode expected =
            UnixFileMode.UserRead | UnixFileMode.UserWrite |
            UnixFileMode.GroupRead |
            UnixFileMode.OtherRead;

        await Assert.That(result)
            .IsEqualTo(expected);
    }

    [Test]
    public async Task Parse_SymbolicAllDashes_ReturnsNone()
    {
        UnixFileMode result = UnixFileMode.Parse("----------");

        await Assert.That(result)
            .IsEqualTo(UnixFileMode.None);
    }

    [Test]
    public async Task Parse_SymbolicWithLetterInWriteSlot_ThrowsArgumentException()
    {
        // User triple is "rxr" - 'x' is not allowed in the write position.
        await Assert.That(() =>
        {
            UnixFileMode.Parse("-rxr-r--r--");
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task Parse_SymbolicWithUnknownTypeCharacter_ThrowsArgumentException()
    {
        await Assert.That(() =>
        {
            UnixFileMode.Parse("trwxrwxrwx");
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task Parse_SymbolicWithLetterInOtherSlot_ThrowsArgumentException()
    {
        await Assert.That(() =>
        {
            UnixFileMode.Parse("-rw-r--r-y");
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task Parse_GarbageString_ThrowsArgumentException()
    {
        await Assert.That(() =>
        {
            UnixFileMode.Parse("not-a-permission");
        }).Throws<ArgumentException>();
    }
}
