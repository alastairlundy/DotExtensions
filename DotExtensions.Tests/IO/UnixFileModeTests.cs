using System.IO;

using AlastairLundy.DotExtensions.IO.Unix;

namespace DotExtensions.Tests.IO;

public class UnixFileModeTests
{
    [Fact]
    public void HasExecutePermission_Should_be_True()
    {
        //Arrange
        UnixFileMode fileMode;
        
        //Act
        fileMode = UnixFileMode.GroupExecute | UnixFileMode.GroupRead |  UnixFileMode.GroupWrite;
        
        Assert.True(fileMode.HasExecutePermission());
    }
    
    [Fact]
    public void HasExecutePermission_Should_be_False()
    {
        //Arrange
        UnixFileMode fileMode;
        
        //Act
        fileMode = UnixFileMode.UserRead | UnixFileMode.UserWrite;
        
        Assert.False(fileMode.HasExecutePermission());
    }
}