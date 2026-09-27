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
using DotExtensions.Exceptions;
using Microsoft.Extensions.Primitives;

namespace DotExtensions.Tests.Exceptions;

// Coverage for the merged v11 guard module (ArgumentExceptionExtensions in DotExtensions.Exceptions):
// genuine failure cases for all eight public members, the exception-tier contract, the StringValues
// any-value whitespace doctrine, and caller-name capture.
//
// Invocation shapes:
// - StringSegment and char span/memory members are driven through ArgumentException.ThrowIf*(...):
//   the BCL string overloads are not applicable to those receivers, so the merged members bind.
// - The StringValues members are driven through ArgumentExceptionExtensions.ThrowIf*(...): at the
//   ArgumentException.* call shape the BCL string overload wins (StringValues converts to string
//   implicitly) and would silently test the BCL rather than this module.
public class GuardModuleTests
{
    #region StringSegment

    [Test]
    public async Task ThrowIfNullOrEmpty_StringSegment_Null_ThrowsExactlyArgumentNullException()
    {
        StringSegment? target = null;

        await Assert.That(() => ArgumentException.ThrowIfNullOrEmpty(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_StringSegment_Empty_ThrowsExactlyArgumentNullException()
    {
        StringSegment? target = new StringSegment(string.Empty);

        await Assert.That(() => ArgumentException.ThrowIfNullOrEmpty(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_StringSegment_WhiteSpaceOnly_ThrowsNothing()
    {
        StringSegment? target = new StringSegment("   ");

        await Assert.That(() => ArgumentException.ThrowIfNullOrEmpty(target)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_StringSegment_ValidValue_ThrowsNothing()
    {
        StringSegment? target = new StringSegment("Hello");

        await Assert.That(() => ArgumentException.ThrowIfNullOrEmpty(target)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringSegment_Null_ThrowsExactlyArgumentNullException()
    {
        StringSegment? target = null;

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringSegment_WhiteSpaceOnly_ThrowsExactlyArgumentException()
    {
        StringSegment? target = new StringSegment("   ");

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    // Genuine behaviour: the whitespace predicate reports false for empty content, so empty input
    // passes this member - the empty-only member above is what rejects it.
    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringSegment_Empty_ThrowsNothing()
    {
        StringSegment? target = new StringSegment(string.Empty);

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(target)).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringSegment_MixedContent_ThrowsNothing()
    {
        StringSegment? target = new StringSegment(" Hello ");

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(target)).ThrowsNothing();
    }

    #endregion

    #region StringValues

    [Test]
    public async Task ThrowIfNullOrEmpty_StringValues_Null_ThrowsExactlyArgumentNullException()
    {
        StringValues? target = null;

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrEmpty(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_StringValues_EmptyCollection_ThrowsExactlyArgumentNullException()
    {
        StringValues? target = StringValues.Empty;

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrEmpty(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_StringValues_EmptyStringValue_ThrowsExactlyArgumentNullException()
    {
        StringValues? target = new StringValues(string.Empty);

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrEmpty(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_StringValues_WhiteSpaceOnly_ThrowsNothing()
    {
        StringValues? target = new StringValues("   ");

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrEmpty(target))
            .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_StringValues_ValidValue_ThrowsNothing()
    {
        StringValues? target = new StringValues("Hello");

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrEmpty(target))
            .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringValues_Null_ThrowsExactlyArgumentNullException()
    {
        StringValues? target = null;

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrWhiteSpace(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringValues_WhiteSpaceOnly_ThrowsExactlyArgumentException()
    {
        StringValues? target = new StringValues("   ");

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringValues_EmptyStringValue_ThrowsExactlyArgumentException()
    {
        StringValues? target = new StringValues(string.Empty);

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringValues_ValidValue_ThrowsNothing()
    {
        StringValues? target = new StringValues("Hello");

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrWhiteSpace(target))
            .ThrowsNothing();
    }

    #endregion

    #region StringValues whitespace doctrine (any-value rule)

    // The predicate is satisfied when ANY value is null or whitespace-only - not only when all
    // values are - so a single offending value among valid ones must still throw.
    [Test]
    [Arguments("Hello", " ")]
    [Arguments(" ", "Hello")]
    public async Task ThrowIfNullOrWhiteSpace_StringValues_AnyValueWhiteSpace_ThrowsExactlyArgumentException(
        string first, string second)
    {
        StringValues? target = new StringValues(new[] { first, second });

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringValues_NullValue_ThrowsExactlyArgumentException()
    {
        StringValues? target = new StringValues(new[] { "Hello", null! });

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    // A StringValues holding no values contains no offending value, so the whitespace member lets
    // it through; the documented remedy is the empty-only member, which rejects it.
    [Test]
    public async Task ThrowIfNullOrWhiteSpace_StringValues_EmptyCollection_PassesWhileEmptyOnlyMemberRejects()
    {
        StringValues? target = StringValues.Empty;

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrWhiteSpace(target))
            .ThrowsNothing();

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrEmpty(target))
            .ThrowsExactly<ArgumentNullException>();
    }

    #endregion

    #region char span and memory

    // Span/Memory receivers are non-nullable structs: their Empty sentinel (also the default value)
    // is the null-equivalent, empty case.

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_Span_Empty_ThrowsExactlyArgumentException()
    {
        await Assert.That(() =>
        {
            Span<char> target = Span<char>.Empty;
            ArgumentException.ThrowIfEmptyOrWhiteSpace(target);
        }).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_Span_WhiteSpaceOnly_ThrowsExactlyArgumentException()
    {
        char[] content = "   ".ToCharArray();

        await Assert.That(() =>
        {
            Span<char> target = content.AsSpan();
            ArgumentException.ThrowIfEmptyOrWhiteSpace(target);
        }).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_Span_ValidValue_ThrowsNothing()
    {
        char[] content = "Hello".ToCharArray();

        await Assert.That(() =>
        {
            Span<char> target = content.AsSpan();
            ArgumentException.ThrowIfEmptyOrWhiteSpace(target);
        }).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_ReadOnlySpan_Empty_ThrowsExactlyArgumentException()
    {
        await Assert.That(() =>
        {
            ReadOnlySpan<char> target = ReadOnlySpan<char>.Empty;
            ArgumentException.ThrowIfEmptyOrWhiteSpace(target);
        }).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_ReadOnlySpan_WhiteSpaceOnly_ThrowsExactlyArgumentException()
    {
        await Assert.That(() =>
        {
            ReadOnlySpan<char> target = "   ".AsSpan();
            ArgumentException.ThrowIfEmptyOrWhiteSpace(target);
        }).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_ReadOnlySpan_ValidValue_ThrowsNothing()
    {
        await Assert.That(() =>
        {
            ReadOnlySpan<char> target = "Hello".AsSpan();
            ArgumentException.ThrowIfEmptyOrWhiteSpace(target);
        }).ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_Memory_Empty_ThrowsExactlyArgumentException()
    {
        Memory<char> target = Memory<char>.Empty;

        await Assert.That(() => ArgumentException.ThrowIfEmptyOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_Memory_WhiteSpaceOnly_ThrowsExactlyArgumentException()
    {
        Memory<char> target = "   ".ToCharArray();

        await Assert.That(() => ArgumentException.ThrowIfEmptyOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_Memory_ValidValue_ThrowsNothing()
    {
        Memory<char> target = "Hello".ToCharArray();

        await Assert.That(() => ArgumentException.ThrowIfEmptyOrWhiteSpace(target))
            .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_ReadOnlyMemory_Empty_ThrowsExactlyArgumentException()
    {
        ReadOnlyMemory<char> target = ReadOnlyMemory<char>.Empty;

        await Assert.That(() => ArgumentException.ThrowIfEmptyOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_ReadOnlyMemory_WhiteSpaceOnly_ThrowsExactlyArgumentException()
    {
        ReadOnlyMemory<char> target = "   ".ToCharArray();

        await Assert.That(() => ArgumentException.ThrowIfEmptyOrWhiteSpace(target))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfEmptyOrWhiteSpace_ReadOnlyMemory_ValidValue_ThrowsNothing()
    {
        ReadOnlyMemory<char> target = "Hello".ToCharArray();

        await Assert.That(() => ArgumentException.ThrowIfEmptyOrWhiteSpace(target))
            .ThrowsNothing();
    }

    #endregion

    #region Caller-name capture

    [Test]
    public async Task ExplicitParamName_OnNullOrEmptyPath_SurfacesInParameterNameAndMessage()
    {
        StringSegment? target = new StringSegment(string.Empty);

        await Assert.That(() => ArgumentException.ThrowIfNullOrEmpty(target, "explicitName"))
            .ThrowsExactly<ArgumentNullException>()
            .WithParameterName("explicitName");

        await Assert.That(() => ArgumentException.ThrowIfNullOrEmpty(target, "explicitName"))
            .ThrowsExactly<ArgumentNullException>()
            .WithMessageContaining("explicitName");
    }

    [Test]
    public async Task ExplicitParamName_OnWhiteSpacePath_SurfacesInParameterNameAndMessage()
    {
        StringSegment? target = new StringSegment("   ");

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(target, "explicitName"))
            .ThrowsExactly<ArgumentException>()
            .WithParameterName("explicitName");

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(target, "explicitName"))
            .ThrowsExactly<ArgumentException>()
            .WithMessageContaining("explicitName");
    }

    [Test]
    public async Task ExplicitParamName_OnCharFamilyPath_SurfacesInParameterName()
    {
        char[] content = "   ".ToCharArray();

        await Assert.That(() =>
        {
            Span<char> target = content.AsSpan();
            ArgumentException.ThrowIfEmptyOrWhiteSpace(target, "explicitName");
        }).ThrowsExactly<ArgumentException>().WithParameterName("explicitName");
    }

    // With no explicit name the compiler supplies the caller's argument expression - the local's
    // name here - not the member's parameter name ("target") and not an empty default.
    [Test]
    public async Task OmittedParamName_CapturesCallerExpression_OnStringSegmentMember()
    {
        StringSegment? callerCapturedArgument = new StringSegment(string.Empty);

        await Assert.That(() => ArgumentException.ThrowIfNullOrEmpty(callerCapturedArgument))
            .ThrowsExactly<ArgumentNullException>()
            .WithParameterName("callerCapturedArgument");
    }

    [Test]
    public async Task OmittedParamName_CapturesCallerExpression_OnStringValuesMember()
    {
        StringValues? callerCapturedArgument = StringValues.Empty;

        await Assert.That(() => ArgumentExceptionExtensions.ThrowIfNullOrEmpty(callerCapturedArgument))
            .ThrowsExactly<ArgumentNullException>()
            .WithParameterName("callerCapturedArgument");
    }

    [Test]
    public async Task OmittedParamName_CapturesCallerExpression_OnCharFamilyMember()
    {
        char[] content = "   ".ToCharArray();

        await Assert.That(() =>
        {
            Span<char> callerCapturedArgument = content.AsSpan();
            ArgumentException.ThrowIfEmptyOrWhiteSpace(callerCapturedArgument);
        }).ThrowsExactly<ArgumentException>().WithParameterName("callerCapturedArgument");
    }

    #endregion
}
