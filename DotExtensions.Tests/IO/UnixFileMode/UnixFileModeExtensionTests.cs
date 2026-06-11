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

using System.IO;
using DotExtensions.IO.Permissions;
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
}
