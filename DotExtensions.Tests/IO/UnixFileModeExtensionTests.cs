using System.IO;

#if NET7_0_OR_GREATER
using AlastairLundy.DotExtensions.IO.Unix;
#endif

namespace DotExtensions.Tests.IO;

public class UnixFileModeExtensionTests
{

    #if NET7_0_OR_GREATER
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
    #endif
    
}