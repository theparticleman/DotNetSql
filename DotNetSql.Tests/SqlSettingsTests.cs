using DotNetSql.Cli;

namespace DotNetSql.Tests;

public class SqlSettingsTests
{
    [Test]
    public void When_building_connection_string()
    {
        var classUnderTest = new SqlSettings
        {
            UserName = "user-name",
            Password = "password",
            HostName = "sql-server"
        };

        var result = classUnderTest.BuildConnectionString();
        
        Assert.That(result, Is.EqualTo("Data Source=sql-server;User ID=user-name;Password=password"));
    }

    [Test]
    public void When_building_a_connection_string_with_a_nondefault_port()
    {
        var classUnderTest = new SqlSettings
        {
            UserName = "user-name",
            Password = "password",
            HostName = "sql-server",
            Port = 1234
        };

        var result = classUnderTest.BuildConnectionString();
        
        Assert.That(result, Is.EqualTo("Data Source=sql-server,1234;User ID=user-name;Password=password"));        
    }
}