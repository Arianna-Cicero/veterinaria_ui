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
        public void CreateFaturaInfoFromUser()
        {
           Console.WriteLine("Informações sobre Fatura");
            fatura.Fatura_id = IDgenerator.GenerateUniqueRandomID();
            Console.WriteLine($"ID da fatura: {fatura.Fatura_id}");
            Console.WriteLine("Utilizador da fatura:");
            string userInput = Console.ReadLine();
            if (UserExists(userInput))
            {
                fatura.User = userInput;
                Console.WriteLine("Utilizador válido. Fatura atualizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Utilizador não encontrado. Por favor, insira um utilizador válido.");
            }

            Console.WriteLine("Data da fatura (yyyy-MM-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                Console.WriteLine("Data inválida. Utilice o valor padrão (yyyy-MM-dd).");
                fatura.Data = DateTime.MinValue;
                return;
            }
            fatura.Data = data;

            Console.WriteLine("Insira o custo da fatura:");
            if (!float.TryParse(Console.ReadLine(), out float custo))
            {
                Console.WriteLine("Custo inválido. Por favor, insira um valor numérico válido.");
                fatura.Custo = 0;
                return;
            }
            fatura.Custo = custo;

            SaveFaturaToDatabase(fatura);
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
        public void SaveFaturaToDatabase(Fatura fatura)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "INSERT INTO Faturas (fatura_id, user, data, custo) " +
                               "VALUES (@FaturaId, @User, @Data, @Custo)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FaturaId", fatura.Fatura_id);
                    command.Parameters.AddWithValue("@User", fatura.User);
                    command.Parameters.AddWithValue("@Data", fatura.Data);
                    command.Parameters.AddWithValue("@Custo", fatura.Custo);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Fatura registrada com sucesso na base de dados!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao registrar a fatura na base de dados.");
                    }
                }
            }
        }
    }
}