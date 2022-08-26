using DotNetSql.Cli;
using Moq;
using Spectre.Console.Cli;

namespace DotNetSql.Tests;

public class Tests
{
    [Test]
    public void When_executing_command()
    {
        var sqlServerAdapter = new Mock<ISqlServerAdapter>();
        var fileSystem = new Mock<IFileSystem>();
        fileSystem.Setup(x => x.ReadAllText("file-name")).Returns("sqlCommand");
        var command = new ExecuteSqlCommand(sqlServerAdapter.Object, fileSystem.Object);
        var remainingArguments = new Mock<IRemainingArguments>();
        var context = new CommandContext(remainingArguments.Object, "commandName", null);
        var settings = new SqlSettings
        {
            UserName = "user-name",
            Password = "password",
            HostName = "host-name",
            ScriptPath = "file-name"
        };
        
        var result = command.Execute(context, settings);
        
        Assert.That(result, Is.EqualTo(0));
        sqlServerAdapter.Verify(x => x.Execute("sqlCommand", settings.BuildConnectionString()));
    }
    
     [Test]
    public void When_executing_command_without_a_script()
    {
        var sqlServerAdapter = new Mock<ISqlServerAdapter>();
        var fileSystem = new Mock<IFileSystem>();
        fileSystem.Setup(x => x.ReadAllText("file-name")).Returns("sqlCommand");
        var command = new ExecuteSqlCommand(sqlServerAdapter.Object, fileSystem.Object);
        var remainingArguments = new Mock<IRemainingArguments>();
        var context = new CommandContext(remainingArguments.Object, "commandName", null);
        var settings = new SqlSettings
        {
            UserName = "user-name",
            Password = "password",
            HostName = "host-name",
        };
        
        var result = command.Execute(context, settings);
        
        Assert.That(result, Is.EqualTo(-1));
    }
    
    [Test]
    public void When_executing_command_and_no_username()
    {
        var sqlServerAdapter = new Mock<ISqlServerAdapter>();
        var fileSystem = new Mock<IFileSystem>();
        fileSystem.Setup(x => x.ReadAllText("file-name")).Returns("sqlCommand");
        var command = new ExecuteSqlCommand(sqlServerAdapter.Object, fileSystem.Object);
        var remainingArguments = new Mock<IRemainingArguments>();
        var context = new CommandContext(remainingArguments.Object, "commandName", null);
        var settings = new SqlSettings
        {
            Password = "password",
            HostName = "host-name",
            ScriptPath = "file-name"
        };
        
        var result = command.Execute(context, settings);
        
        Assert.That(result, Is.EqualTo(-1));
    }
    
    [Test]
    public void When_executing_command_and_no_password()
    {
        var sqlServerAdapter = new Mock<ISqlServerAdapter>();
        var fileSystem = new Mock<IFileSystem>();
        fileSystem.Setup(x => x.ReadAllText("file-name")).Returns("sqlCommand");
        var command = new ExecuteSqlCommand(sqlServerAdapter.Object, fileSystem.Object);
        var remainingArguments = new Mock<IRemainingArguments>();
        var context = new CommandContext(remainingArguments.Object, "commandName", null);
        var settings = new SqlSettings
        {            
            UserName = "user-name",
            HostName = "host-name",
            ScriptPath = "file-name"
        };
        
        var result = command.Execute(context, settings);
        
        Assert.That(result, Is.EqualTo(-1));
    }

    
    [Test]
    public void When_executing_command_and_no_hostname()
    {
        var sqlServerAdapter = new Mock<ISqlServerAdapter>();
        var fileSystem = new Mock<IFileSystem>();
        fileSystem.Setup(x => x.ReadAllText("file-name")).Returns("sqlCommand");
        var command = new ExecuteSqlCommand(sqlServerAdapter.Object, fileSystem.Object);
        var remainingArguments = new Mock<IRemainingArguments>();
        var context = new CommandContext(remainingArguments.Object, "commandName", null);
        var settings = new SqlSettings
        {            
            UserName = "user-name",
            Password = "password",
            ScriptPath = "file-name"
        };
        
        var result = command.Execute(context, settings);
        
        Assert.That(result, Is.EqualTo(-1));
    }
}