using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria_ui
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TESTE DA BASE DE DADOS
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);
                try
                {
                    connection.Open();
                    // Ejecutar una consulta simple
                    string query = "SELECT 1";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int result = (int)command.ExecuteScalar();
                        Console.WriteLine("Resultado da consulta: " + result);
                    }
                    Console.WriteLine("0 para sair");
                    String opcao = Console.ReadLine();
                    if (opcao == "0")
                    {
                        Console.WriteLine("close");
                        DatabaseManager.CloseConnection(connection);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
        }
    }
}
