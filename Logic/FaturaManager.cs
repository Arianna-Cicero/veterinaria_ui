using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using veterinaria_ui.Presentation;

namespace Logics
{
    internal class FaturaManager
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        Fatura fatura =  new Fatura();
        public void GetFaturaInfoFromUser(Fatura fatura)
        {
           Console.WriteLine("Informações sobre Fatura");
            fatura.fatura_id = IDgenerator.GenerateUniqueRandomID();
            Console.WriteLine($"ID da fatura: {fatura.fatura_id}");
            Console.WriteLine("Utilizador da fatura:");
            string userInput = Console.ReadLine();
            if (UserExists(userInput))
            {
                fatura.user = userInput;
                Console.WriteLine("Utilizador válido. Fatura atualizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Utilizador não encontrado. Por favor, insira um utilizador válido.");
            }

            Console.WriteLine("Data da fatura (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                fatura.data = data;
            }
            else
            {
                Console.WriteLine("Data inválida. Utilice o valor padrão (yyyy-MM-dd).");
                fatura.data = DateTime.MinValue;
            }
            Console.WriteLine("Insira o custo da fatura:");
            fatura.custo = float.Parse(Console.ReadLine());
        }
        public void GetFaturaByUser(string username)
        {

            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT fatura_id, data, custo, consulta_id, user FROM Faturas WHERE user = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID da fatura: {reader["fatura_id"]}");
                            Console.WriteLine($"Data da Fatura: {Convert.ToDateTime(reader["data"]).ToShortDateString()}");
                            Console.WriteLine($"Custo: {reader["custo"]}");
                            Console.WriteLine($"ID da consulta: {reader["consulta_id"]}");
                            Console.WriteLine($"Utilizador: {reader["user"]}");
                        }
                    }
                }
            }
        }

        public void GetAllFaturas()
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT fatura_id, data, custo, consulta_id, user FROM Faturas";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID da fatura: {reader["fatura_id"]}");
                            Console.WriteLine($"Data da Fatura: {Convert.ToDateTime(reader["data"]).ToShortDateString()}");
                            Console.WriteLine($"Custo: {reader["custo"]}");
                            Console.WriteLine($"ID da consulta: {reader["consulta_id"]}");
                            Console.WriteLine($"Utilizador: {reader["user"]}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        public bool UserExists(string username)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT COUNT(*) FROM Usuarios WHERE username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}