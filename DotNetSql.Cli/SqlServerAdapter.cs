using Microsoft.Data.SqlClient;

namespace DotNetSql.Cli;

public interface ISqlServerAdapter
{
    void Execute(string sql, string connectionString);
}

public class SqlServerAdapter : ISqlServerAdapter
{
    public void Execute(string sql, string connectionString)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}