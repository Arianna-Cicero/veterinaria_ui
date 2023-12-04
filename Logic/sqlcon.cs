using System;
using System.Data.SqlClient;

public static class DatabaseManager
{
    private static readonly string ConnectionString = "Data Source=ARIANNA;Initial Catalog=veterinaria;User ID=arianna;Password=123456789;";
    public static SqlConnection GetConnection()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        return connection;
    }

    public static void OpenConnection(SqlConnection connection)
    {
        try
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao abrir a conexão: " + ex.Message);
        }
    }

    public static void CloseConnection(SqlConnection connection)
    {
        try
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao fechar a conexão: " + ex.Message);
        }
    }
}


