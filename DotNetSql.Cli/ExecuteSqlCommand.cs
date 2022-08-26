using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DotNetSql.Cli;

public class ExecuteSqlCommand : Command<SqlSettings>
{
    private readonly ISqlServerAdapter sqlServerAdapter;
    private readonly IFileSystem fileSystem;

    public ExecuteSqlCommand(ISqlServerAdapter sqlServerAdapter, IFileSystem fileSystem)
    {
        this.sqlServerAdapter = sqlServerAdapter;
        this.fileSystem = fileSystem;
    }

    public override int Execute(CommandContext context, SqlSettings settings)
    {
        if(settings.Version.HasValue && settings.Version.Value) 
        {
            PrintVersion();
            return 0;
        }

        if (string.IsNullOrEmpty(settings.ScriptPath)
            || string.IsNullOrEmpty(settings.UserName)
            || string.IsNullOrEmpty(settings.Password)
            || string.IsNullOrEmpty(settings.HostName))
        {
            AnsiConsole.WriteLine("Missing argument parameters; use --help to see instructions");
            return -1;
        }
       
        var fileContents = fileSystem.ReadAllText(settings.ScriptPath);
        sqlServerAdapter.Execute(fileContents, settings.BuildConnectionString());
        return 0;
    }

    private void PrintVersion()
    {
        var assembly = Assembly.GetCallingAssembly();
        var fileVersion = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule!.FileName!);
        var version = fileVersion?.ProductVersion ?? assembly!.GetName()!.Version!.ToString();
        AnsiConsole.WriteLine("sql-cli");
        AnsiConsole.Write(new Rule());
        AnsiConsole.WriteLine($"version {version}");
        AnsiConsole.WriteLine("License: MIT");
    }
}

public class SqlSettings : CommandSettings
{
    [CommandOption("-U|--user")]
    [Description("DB username")]
    public string? UserName { get; set; }
    [CommandOption("-P|--password")]
    public string? Password { get; set; }
    [CommandOption("-H|-S|--hostname")]
    public string? HostName { get; set; }
    [CommandOption("-i|--input-file")]
    public string? ScriptPath { get; set; }
    [CommandOption("--port")]
    public int? Port { get; set; }
    [CommandOption("-v|--version")]
    public bool? Version {get; set;}

    public string BuildConnectionString()
    {
        var dataSource = Port.HasValue ? $"{HostName},{Port}" : HostName;
        var builder = new SqlConnectionStringBuilder
        {
            UserID = UserName,
            Password = Password,
            DataSource = dataSource,
            Encrypt = SqlConnectionEncryptOption.Optional
        };
        return builder.ConnectionString;
    }
}