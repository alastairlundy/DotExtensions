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

using System.Security.AccessControl;

namespace DotExtensions.IO.Permissions;

public static partial class PermissionExtensions
{
    /// <param name="rules">The Windows file permission to check.</param>
    extension(AuthorizationRuleCollection rules)
    {
        /// <summary>
        /// Whether the specified Windows file permission has execute permission.
        /// </summary>
        [SupportedOSPlatform("windows")]
        public bool HasExecutePermission
        {
            get
            {
                return rules.Cast<FileSystemAccessRule>().Any(rule =>
                    rule.FileSystemRights.HasFlag(FileSystemRights.ExecuteFile) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.FullControl) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.ReadAndExecute));
            }
        }

        /// <summary>
        /// Whether the specified Windows file permission has execute permission.
        /// </summary>
        [SupportedOSPlatform("windows")]
        public bool HasWritePermission
        {
            get
            {
                return rules.Cast<FileSystemAccessRule>().Any(rule =>
                    rule.FileSystemRights.HasFlag(FileSystemRights.Write) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.FullControl) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.WriteData) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.AppendData) || 
                    rule.FileSystemRights.HasFlag(FileSystemRights.CreateFiles) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.CreateDirectories));
            }   
        }

        /// <summary>
        /// Whether the specified Windows file permission has execute permission.
        /// </summary>
        [SupportedOSPlatform("windows")]
        public bool HasReadPermission
        {
            get
            {
                return rules.Cast<FileSystemAccessRule>().Any(rule =>
                    rule.FileSystemRights.HasFlag(FileSystemRights.Read) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.FullControl) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.ReadAndExecute) ||
                    rule.FileSystemRights.HasFlag(FileSystemRights.ReadData));
            }
        }
    }
}