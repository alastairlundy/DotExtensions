using System.IO;

using AlastairLundy.DotExtensions.IO.Unix;

namespace DotExtensions.Tests.IO;

public class UnixFileModeExtensionTests
{

    [Fact]
    public void UnixFileMode_HasExecutePermissions_True()
    {
        UnixFileMode expectedUser = UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute;
        UnixFileMode expectedGroup = UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute;
        UnixFileMode expectedOthers = UnixFileMode.OtherRead |  UnixFileMode.OtherWrite | UnixFileMode.OtherExecute;
        
        Assert.True(expectedUser.HasExecutePermission());
        Assert.True(expectedGroup.HasExecutePermission());
        Assert.True(expectedOthers.HasExecutePermission());
    }
}