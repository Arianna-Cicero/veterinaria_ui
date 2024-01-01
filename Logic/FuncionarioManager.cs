using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Logic
{
    public class FuncionarioManager
    {
        public void GetFuncionarioInfoFromUser(Funcionario funcionario)
        {
            Console.WriteLine("Insira as informações do Funcionário");

            funcionario.Funcionario_id = IDgenerator.GenerateUniqueRandomID();

            Console.WriteLine($"ID do Funcionário: {funcionario.Funcionario_id}");

            Console.WriteLine("Nome:");
            funcionario.Nome = Console.ReadLine();

            Console.WriteLine("Data de nascimento (yyyy-MM-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                Console.WriteLine("Data de Nascimento inválida. Utilize o valor padrão (yyyy-MM-dd).");
                funcionario.Data_nasc = DateTime.MinValue;
            }
            else
            {
                funcionario.Data_nasc = dataNasc;
            }

            Console.WriteLine("Sexo (Feminino/Masculino):");
            funcionario.Sexo = Console.ReadLine();
            if (funcionario.Sexo.ToLower() != "feminino" && funcionario.Sexo.ToLower() != "masculino")
            {
                Console.WriteLine("Erro. O sexo assinalado é incorreto. As opções disponíveis são Feminino ou Masculino.");
                return;
            }

            Console.WriteLine("Tipo de funcionário:");
            funcionario.Tipo_funcionario = Console.ReadLine();

            SaveFuncionarioToDatabase(funcionario);
        }

        public void SaveFuncionarioToDatabase(Funcionario funcionario)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "INSERT INTO Funcionario (nome, data_nasc, sexo, tipo_funcionario) " +
                               "VALUES (@Nome, @DataNasc, @Sexo, @TipoFuncionario )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuncionarioId", funcionario.Funcionario_id);
                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@DataNasc", funcionario.Data_nasc);
                    command.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                    command.Parameters.AddWithValue("@TipoFuncionario", funcionario.Tipo_funcionario);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Funcionário registrado com sucesso na base de dados!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao registrar o funcionário na base de dados.");
                    }
                }
            }
        }
        public void EditarFuncionarioPorId(int funcionarioId)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT * FROM Funcionarios WHERE funcionario_id = @FuncionarioId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuncionarioId", funcionarioId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Funcionario funcionario = new Funcionario
                            {
                                Funcionario_id = reader.GetInt32(reader.GetOrdinal("funcionario_id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Data_nasc = reader.GetDateTime(reader.GetOrdinal("data_nasc")),
                                Sexo = reader.GetString(reader.GetOrdinal("sexo")),
                                Tipo_funcionario = reader.GetString(reader.GetOrdinal("tipo_funcionario")),
                            };

                            Console.WriteLine($"Editar Funcionário: {funcionario.Nome}");
                            Console.WriteLine("Novo nome (pressione Enter para manter o mesmo):");
                            string novoNome = Console.ReadLine();
                            if (!string.IsNullOrEmpty(novoNome))
                            {
                                funcionario.Nome = novoNome;
                            }

                            Console.WriteLine("Nova data de nascimento (yyyy-MM-dd) (pressione Enter para manter a mesma):");
                            string novaData = Console.ReadLine();
                            if (!string.IsNullOrEmpty(novaData) && DateTime.TryParse(novaData, out DateTime dataNasc))
                            {
                                funcionario.Data_nasc = dataNasc;
                            }

                            AtualizarFuncionario(funcionario);
                        }
                        else
                        {
                            Console.WriteLine("Funcionário não encontrado.");
                        }
                    }
                }
            }
        }
        public void AtualizarFuncionario(Funcionario funcionario)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "UPDATE Funcionarios " +
                               "SET nome = @Nome, data_nasc = @DataNasc, sexo = @Sexo, tipo_funcionario = @TipoFuncionario " +
                               "WHERE funcionario_id = @FuncionarioId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuncionarioId", funcionario.Funcionario_id);
                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@DataNasc", funcionario.Data_nasc);
                    command.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                    command.Parameters.AddWithValue("@TipoFuncionario", funcionario.Tipo_funcionario);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Funcionário atualizado com sucesso na base de dados!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao atualizar o funcionário na base de dados.");
                    }
                }
            }
        }
        public void RemoverFuncionarioPorId(int funcionarioId)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "DELETE FROM Funcionarios WHERE funcionario_id = @FuncionarioId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuncionarioId", funcionarioId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Funcionário removido com sucesso da base de dados!");
                    }
                    else
                    {
                        Console.WriteLine("Funcionário não encontrado ou erro ao remover da base de dados.");
                    }
                }
            }
        }

        //GET
        public void GetFuncionario()
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT * FROM Funcionarios";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader.GetInt32(reader.GetOrdinal("funcionario_id"))}");
                            Console.WriteLine($"Nome: {reader.GetString(reader.GetOrdinal("nome"))}");
                            Console.WriteLine($"Data de Nascimento: {reader.GetDateTime(reader.GetOrdinal("data_nasc")).ToShortDateString()}");
                            Console.WriteLine($"Sexo: {reader.GetString(reader.GetOrdinal("sexo"))}");
                            Console.WriteLine($"Tipo de Funcionário: {reader.GetString(reader.GetOrdinal("tipo_funcionario"))}");
                            Console.WriteLine(); 
                        }
                    }
                }
            }
        }

        //PUT
    }
}
