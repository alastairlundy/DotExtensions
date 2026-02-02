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
using System.Security.Principal;

namespace DotExtensions.IO.Permissions.Windows;

/// <summary>
/// Provides extension methods for managing and inspecting file permissions
/// on Windows-based systems.
/// </summary>
public static class WindowsFilePermissionsExtensions
{
    /// <param name="file">The FileInfo object for which to retrieve the permission.</param>
    extension(FileInfo file)
    {
        /// <summary>
        /// Retrieves the Windows file permission for a given FileInfo object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public AuthorizationRuleCollection GetWindowsFilePermission()
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);
           
            if(!file.Exists)
                throw new FileNotFoundException(Resources.Exceptions_FileNotFound.Replace("{file}", file.FullName));

            FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);

            AuthorizationRuleCollection results = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));

            return results;
        }
        
        /// <summary>
        /// Sets the Windows file permission for a given FileInfo object.
        /// </summary>
        /// <param name="identityReference"></param>
        /// <param name="fileSystemRights"></param>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public void SetWindowsFilePermission(IdentityReference identityReference, FileSystemRights fileSystemRights)
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);
            
            if(!file.Exists)
                throw new FileNotFoundException(Resources.Exceptions_FileNotFound.Replace("{file}", file.FullName));
        
            FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);
            
            fileSecurity.AddAccessRule(new(identityReference, fileSystemRights, AccessControlType.Allow));
        
            file.SetAccessControl(fileSecurity);
        }
    }
    
    /// <param name="directory">The DirectoryInfo object for which to set the permission.</param>
    extension(DirectoryInfo directory)
    {
        /// <summary>
        /// Sets the Windows file permission for a given DirectoryInfo object.
        /// </summary>
        /// <param name="identityReference"></param>
        /// <param name="fileSystemRights"></param>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public void SetWindowsDirectoryPermission(IdentityReference identityReference, FileSystemRights fileSystemRights)
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);
            
            DirectorySecurity directorySecurity = directory.GetAccessControl(AccessControlSections.Access);

            directorySecurity.AddAccessRule(new FileSystemAccessRule(identityReference, fileSystemRights, AccessControlType.Allow));
            directory.SetAccessControl(directorySecurity);
        }

        /// <summary>
        /// Retrieves the Windows directory permission for a given DirectoryInfo object.
        /// </summary>
        /// <returns> </returns>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public AuthorizationRuleCollection GetWindowsDirectoryPermission()
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);
            
            if(!directory.Exists)
                throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{directory}",
                    directory.Name));
        
            DirectorySecurity directorySecurity = directory.GetAccessControl(AccessControlSections.Access);
            AuthorizationRuleCollection results = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));

            return results;
        }
    }
}