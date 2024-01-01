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

    internal class ConsultaManager
    {

        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public void ListarTodasConsultas()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Consultas";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");
                        Console.WriteLine("| ID da consulta | Data da consulta | Motivo    | ID do animal | ID do funcionario |");
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

                                Console.WriteLine($"| {consultaId,-15} | {data,-17:dd/MM/yyyy} | {motivo,-10} | {animalId,-13} | {funcionarioId,-17} |");
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

        public void ListarConsultasPorCliente(int proprietarioId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Consultas WHERE animal_id IN (SELECT animal_id FROM Animais WHERE proprietario_id = @ProprietarioId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ProprietarioId", proprietarioId);

                        Console.WriteLine("+-----------------+---------------------+------------+---------------+---------------------+");

                        Console.WriteLine("|  ID da consulta |   Data da consulta  |   Motivo   | ID do animal  |  ID do funcionario  |");

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

                                Console.WriteLine($"| {consultaId,-15} | {data,-19:dd/MM/yyyy} | {motivo,-10} | {animalId,-15} | {funcionarioId,-19} |");

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
        public void AgendarConsulta(int animalId, DateTime dataConsulta, string motivo)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Consultas (animal_id, data_consulta, motivo) VALUES (@AnimalId, @DataConsulta, @Motivo)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@AnimalId", animalId);
                        command.Parameters.AddWithValue("@DataConsulta", dataConsulta);
                        command.Parameters.AddWithValue("@Motivo", motivo);
                        

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


