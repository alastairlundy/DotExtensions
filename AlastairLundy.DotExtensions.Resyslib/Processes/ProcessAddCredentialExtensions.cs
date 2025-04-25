/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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
using System.Diagnostics;

#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#endif

using AlastairLundy.ProcessInvoke.Primitives;
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.Resyslib.Processes
{
    public static class ProcessAddCredentialExtensions
    {

        /// <summary>
        /// Attempts to add the specified Credential to the current Process object.
        /// </summary>
        /// <param name="process">The current Process object.</param>
        /// <param name="credential">The credential to be added.</param>
        /// <returns>True if successfully applied; false otherwise.</returns>
        public static bool TryAddUserCredential(this Process process, UserCredential credential)
        {
            if (credential.IsSupportedOnCurrentOS())
            {
                try
                {
                    AddUserCredential(process, credential);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    
        /// <summary>
        /// Adds the specified Credential to the current Process object.
        /// </summary>
        /// <param name="process">The current Process object.</param>
        /// <param name="credential">The credential to be added.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if not supported on the current operating system.</exception>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public static void AddUserCredential(this Process process, UserCredential credential)
        {
#pragma warning disable CA1416
            if (credential.IsSupportedOnCurrentOS())
            {
                if (credential.Domain is not null)
                {
                    process.StartInfo.Domain = credential.Domain;
                }

                if (credential.UserName is not null)
                {
                    process.StartInfo.UserName = credential.UserName;
                }

                if (credential.Password is not null)
                {
                    process.StartInfo.Password = credential.Password;
                }

                if (credential.LoadUserProfile is not null)
                {
                    process.StartInfo.LoadUserProfile = (bool)credential.LoadUserProfile;
                }
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
#pragma warning restore CA1416
        }
    
        /// <summary>
        /// Adds the specified Credential to the current ProcessStartInfo object.
        /// </summary>
        /// <param name="processStartInfo">The current ProcessStartInfo object.</param>
        /// <param name="credential">The credential to be added.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public static void AddUserCredential(this ProcessStartInfo processStartInfo, UserCredential credential)
        {
#pragma warning disable CA1416
            if (credential.IsSupportedOnCurrentOS())
            {
                if (credential.Domain is not null)
                {
                    processStartInfo.Domain = credential.Domain;
                }

                if (credential.UserName is not null)
                {
                    processStartInfo.UserName = credential.UserName;
                }

                if (credential.Password is not null)
                {
                    processStartInfo.Password = credential.Password;
                }

                if (credential.LoadUserProfile is not null)
                {
                    processStartInfo.LoadUserProfile = (bool)credential.LoadUserProfile;
                }
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
#pragma warning restore CA1416
        }
    }
}