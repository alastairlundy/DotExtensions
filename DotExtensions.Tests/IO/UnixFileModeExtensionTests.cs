using System.IO;
using DotExtensions.IO.Permissions;

namespace DotExtensions.Tests.IO;

public class UnixFileModeExtensionTests
{
    [Test]
    public async Task UnixFileMode_HasExecutePermissions_True()
    {
        UnixFileMode expectedUser =
            UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute;
        UnixFileMode expectedGroup =
            UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute;
        UnixFileMode expectedOthers =
            UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute;

        await Assert.That(expectedUser.HasExecutePermission).IsTrue();
        await Assert.That(expectedGroup.HasExecutePermission).IsTrue();
        await Assert.That(expectedOthers.HasExecutePermission).IsTrue();
    }
}
