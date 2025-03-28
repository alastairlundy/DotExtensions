/*
        MIT License
       
       Copyright (c) 2020-2025 Alastair Lundy
       
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
// ReSharper disable ArrangeNamespaceBody
// ReSharper disable CheckNamespace

namespace AlastairLundy.DotExtensions.Versions
{
    public static class IsAtLeastExtension
    {
        /// <summary>
        /// Returns whether the specified version is newer than or equal to the current version object.
        /// </summary>
        /// <param name="version">The current version object.</param>
        /// <param name="versionToBeCompared">The version to be compared.</param>
        /// <returns>true if the specified compared version is newer than or equal to the current version, and returns false otherwise.</returns>
        public static bool IsAtLeastVersion(this Version version, Version versionToBeCompared)
        {
            return versionToBeCompared >= version;
        }
    }
}