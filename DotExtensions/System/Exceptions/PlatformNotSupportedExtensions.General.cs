/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
// ReSharper disable InconsistentNaming

namespace AlastairLundy.DotExtensions.Exceptions;

/// <summary>
/// Contains extension methods for enhancing the handling of <see cref="PlatformNotSupportedException"/> exceptions.
/// </summary>
public static partial class PlatformNotSupportedExceptionExtensions
{
    /// <summary>
    /// Provides extension methods for handling <see cref="PlatformNotSupportedException"/> based on platform-specific checks.
    /// </summary>
    extension(PlatformNotSupportedException)
    {
        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform matches the
        /// specified <see cref="OSPlatform"/>.
        /// </summary>
        /// <param name="osPlatform">The <see cref="OSPlatform"/> to check against the current platform.</param>
        /// <param name="exceptionMessage">
        /// An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        /// If not provided, a default message is used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform matches the specified <see cref="OSPlatform"/>.
        /// </exception>
        public static void ThrowIfOSPlatform(OSPlatform osPlatform, string? exceptionMessage = null)
        {
            if (RuntimeInformation.IsOSPlatform(osPlatform))
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                    
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform does not match the
        /// specified <see cref="OSPlatform"/>.
        /// </summary>
        /// <param name="osPlatform">The <see cref="OSPlatform"/> to check against the current platform.</param>
        /// <param name="exceptionMessage">
        /// An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        /// If not provided, a default message is used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform does not match the specified <see cref="OSPlatform"/>.
        /// </exception>
        public static void ThrowIfNotOSPlatform(OSPlatform osPlatform, string? exceptionMessage = null)
        {
            if (!RuntimeInformation.IsOSPlatform(osPlatform))
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                    
            throw new PlatformNotSupportedException();
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform matches any platform
        /// specified by <see cref="UnsupportedOSPlatformAttribute"/> applied to the declaring type.
        /// </summary>
        /// <remarks>
        /// This method relies on Reflection to determine whether the platform is unsupported based on
        /// the <see cref="UnsupportedOSPlatformAttribute"/> declared on the type. It is not suitable for
        /// use in AoT (Ahead-of-Time) compiled environments or trimmed assemblies due to its reliance on Reflection.
        /// </remarks>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform matches an unsupported platform defined
        /// by the <see cref="UnsupportedOSPlatformAttribute"/> applied to the declaring type.
        /// If the attribute specifies a message, it will be included in the exception.
        /// </exception>
        /// <exception cref="RequiresUnreferencedCodeAttribute">
        /// Indicates that this method requires code that cannot be analysed statically for compatibility
        /// with AoT or assembly trimming scenarios due to the use of Reflection.
        /// </exception>
        [RequiresUnreferencedCode(
            "This method requires the use of Reflection, which is not supported on AoT and may not work as intended on Trimmed Assemblies.")]
        public static void ThrowIfUnSupportedPlatform()
        {
            MethodBase? method;
            try
            {
                method = MethodBase.GetCurrentMethod();
            }
            catch (TargetException)
            {
                // This member was invoked with a late-binding mechanism.
                return;
            }

            Type? type = method?.DeclaringType;

            if (method is null || type is null)
                return;

            UnsupportedOSPlatformAttribute[] unsupportedOsPlatformAttributes =
               (UnsupportedOSPlatformAttribute[])Attribute.GetCustomAttributes(type, typeof(UnsupportedOSPlatformAttribute));
            
            foreach (UnsupportedOSPlatformAttribute unsupportedPlatform in unsupportedOsPlatformAttributes)
            {
                if (OperatingSystem.IsOSPlatform(unsupportedPlatform.PlatformName))
                {
                    if(unsupportedPlatform.Message is not null)
                        throw new PlatformNotSupportedException(unsupportedPlatform.Message);
                    
                    throw new PlatformNotSupportedException();
                }
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform matches
        /// any of the unsupported platforms specified in the <paramref name="unsupportedOsPlatforms"/> array.
        /// </summary>
        /// <remarks>This method is AoT compatible and does not rely on reflection.</remarks>
        /// <param name="exceptionMessage">
        /// An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        /// If not provided, a default message is used.
        /// </param>
        /// <param name="unsupportedOsPlatforms">
        /// An array of strings representing unsupported operating system platforms.
        /// Each string should correspond to a platform identifier, such as "Windows" or "Linux".
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform matches any of the specified <paramref name="unsupportedOsPlatforms"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="unsupportedOsPlatforms"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the elements in <paramref name="unsupportedOsPlatforms"/> is null or empty.
        /// </exception>
        public static void ThrowIfUnSupportedPlatform(string? exceptionMessage = null,
            params string[] unsupportedOsPlatforms)
        {
            ArgumentNullException.ThrowIfNull(unsupportedOsPlatforms);

            foreach (string unsupportedPlatform in unsupportedOsPlatforms)
            {
                ArgumentException.ThrowIfNullOrEmpty(unsupportedPlatform);
                
                if (OperatingSystem.IsOSPlatform(unsupportedPlatform))
                {
                    if(exceptionMessage is not null)
                        throw new PlatformNotSupportedException(exceptionMessage);
                    
                    throw new PlatformNotSupportedException();
                }
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform
        /// is not among the supported platforms defined by the <see cref="SupportedOSPlatformAttribute"/>
        /// applied to the declaring type of the method.
        /// </summary>
        /// <param name="exceptionMessage">
        /// An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        /// If not provided, a default message is used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform is not supported based on
        /// <see cref="SupportedOSPlatformAttribute"/> annotations.
        /// </exception>
        [RequiresUnreferencedCode(
            "This method requires the use of Reflection, which is not supported on AoT and may not work as intended on Trimmed Assemblies.")]
        public static void ThrowIfNotSupportedPlatform(string? exceptionMessage = null)
        {
            MethodBase? method;

            #if NET8_0_OR_GREATER
            if (!RuntimeFeature.IsDynamicCodeSupported)
                return;
            #endif
            
            try
            {
                method = MethodBase.GetCurrentMethod();
            }
            catch (TargetException)
            {
                // This member was invoked with a late-binding mechanism.
                return;
            }

            Type? type = method?.DeclaringType;

            if (method is null || type is null)
                return;

            SupportedOSPlatformAttribute[] supportedOsPlatformAttributes =
                (SupportedOSPlatformAttribute[])Attribute.GetCustomAttributes(type,
                    typeof(SupportedOSPlatformAttribute));

            bool osNotSupported = supportedOsPlatformAttributes.All(p => !OperatingSystem.IsOSPlatform(p.PlatformName));

            if (osNotSupported)
            {
                if(exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                    
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform does not match any of the
        /// specified supported OS platforms.
        /// </summary>
        /// <remarks>This method is AoT compatible and does not rely on reflection.</remarks>
        /// <param name="exceptionMessage">An optional custom exception message to use when throwing the <see cref="PlatformNotSupportedException"/>.</param>
        /// <param name="supportedOsPlatforms">An array of strings representing the names of OS platforms that are supported.</param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform does not match any of the specified supported OS platforms.
        /// </exception>
        public static void ThrowIfNotSupportedPlatform(string? exceptionMessage = null,
            params string[] supportedOsPlatforms)
        {
            ArgumentNullException.ThrowIfNull(supportedOsPlatforms);

            bool osNotSupported = supportedOsPlatforms.All(p => !OperatingSystem.IsOSPlatform(p));

            if (osNotSupported)
            {
                if(exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                    
                throw new PlatformNotSupportedException();
            }
        }
    }
}