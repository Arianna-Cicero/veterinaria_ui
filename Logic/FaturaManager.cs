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
    public class FaturaManager
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        int largura = 40;
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
                fatura.Username = userInput;
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {              
                connection.Open();

                string query = "SELECT fatura_id, data, custo, consulta_id, user FROM Fatura WHERE user = @Username";

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Fatura_id, Data, Custo, Consulta_id, Username FROM Fatura";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID da fatura: {reader["Fatura_id"]}");
                            Console.WriteLine($"Data da Fatura: {Convert.ToDateTime(reader["Data"]).ToShortDateString()}");
                            Console.WriteLine($"Custo: {reader["Custo"]}");
                            Console.WriteLine($"ID da consulta: {reader["Consulta_id"]}");
                            Console.WriteLine($"Utilizador: {reader["Username"]}");
                            LoopDeco.ExibirLinhaDecorativa(largura);
                        }
                    }
                }
            }
        }
        public bool UserExists(string username)
        {

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Login WHERE username = @Username";

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Fatura (fatura_id, user, data, custo) " +
                               "VALUES (@FaturaId, @User, @Data, @Custo)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FaturaId", fatura.Fatura_id);
                    command.Parameters.AddWithValue("@User", fatura.Username);
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