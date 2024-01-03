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

namespace Logic
{

    public class ConsultaManager
    {

        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private readonly MenuOpcoes menuOpcoes;

        public void ListarTodasConsultas()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Consulta";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");
                        Console.WriteLine("|  ID da consulta |   Data da consulta  |   Motivo   |  ID do animal |  ID do funcionario  |");
                        Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int consultaId = reader.GetInt32(reader.GetOrdinal("consulta_id"));
                                DateTime data = reader.GetDateTime(reader.GetOrdinal("data_consulta"));
                                string motivo = reader.GetString(reader.GetOrdinal("motivo"));
                                int animalId = reader.GetInt32(reader.GetOrdinal("animal_id"));
                                int funcionarioId = reader.GetInt32(reader.GetOrdinal("funcionario_id"));

                                Console.WriteLine($"| {consultaId,-15} | {data,-17:dd/MM/yyyy} | {motivo,-15} | {animalId,-8} | {funcionarioId,-10} |");
                                Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");

                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }

        public void ListarConsultasPorCliente(int animal_id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Consulta WHERE animal_id IN (SELECT animal_id FROM Animal WHERE Animal_id = @AnimalId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@AnimalId", animal_id);

                        Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");
                        Console.WriteLine("|  ID da consulta |   Data da consulta  |   Motivo   | ID do animal  |  ID do funcionario  |");
                        Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int consultaId = reader.GetInt32(reader.GetOrdinal("consulta_id"));
                                    DateTime data = reader.GetDateTime(reader.GetOrdinal("data_consulta"));
                                    string motivo = reader.GetString(reader.GetOrdinal("motivo"));
                                    int animalId = reader.GetInt32(reader.GetOrdinal("animal_id"));
                                    int funcionarioId = reader.GetInt32(reader.GetOrdinal("funcionario_id"));

                                    Console.WriteLine($"| {consultaId,-15} | {data,-19:dd/MM/yyyy} | {motivo,-10} | {animalId,-15} | {funcionarioId,-19} |");

                                    Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");
                                }
                                

                            }
                            else
                            {
                                Console.WriteLine("Este proprietario nao tem consultas.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }

        public void AgendarConsulta(int animalId, DateTime dataConsulta, string motivo, int funcionarioId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Consulta (data_consulta, motivo, animal_id, funcionario_id) " +
                                "VALUES (@DataConsulta, @Motivo, @AnimalId, @FuncionarioId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@DataConsulta", dataConsulta.Date);
                        command.Parameters.AddWithValue("@Motivo", motivo);
                        command.Parameters.AddWithValue("@AnimalId", animalId);
                        command.Parameters.AddWithValue("@FuncionarioId", funcionarioId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Consulta agendada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao agendar consulta. Verifique os dados e tente novamente.");
                        }

                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}


