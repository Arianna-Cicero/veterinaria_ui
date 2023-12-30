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
        //RETRIEVE
        public void GetFuncionarioInfoFromUser(Funcionario funcionario)
        {
            Console.WriteLine("Insira as informações do Funcionário");

            funcionario.funcionario_id = IDgenerator.GenerateUniqueRandomID();

            Console.WriteLine($"ID do Funcionário: {funcionario.funcionario_id}");

            Console.WriteLine("Nome:");
            funcionario.nome = Console.ReadLine();

            Console.WriteLine("Data de nascimento (yyyy-MM-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                Console.WriteLine("Data de Nascimento inválida. Utilize o valor padrão (yyyy-MM-dd).");
                funcionario.data_nasc = DateTime.MinValue;
            }
            else
            {
                funcionario.data_nasc = dataNasc;
            }

            Console.WriteLine("Sexo (Feminino/Masculino):");
            funcionario.sexo = Console.ReadLine();
            if (funcionario.sexo.ToLower() != "feminino" && funcionario.sexo.ToLower() != "masculino")
            {
                Console.WriteLine("Erro. O sexo assinalado é incorreto. As opções disponíveis são Feminino ou Masculino.");
                return;
            }

            Console.WriteLine("Tipo de funcionário:");
            funcionario.tipo_funcionario = Console.ReadLine();

            // Definindo a permissão do funcionário como 2 (já que é um funcionário)
            funcionario.permissao = 2;

            // Após validar as informações, chama o método para salvar no banco de dados
            SaveFuncionarioToDatabase(funcionario);
        }

        public void SaveFuncionarioToDatabase(Funcionario funcionario)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "INSERT INTO Funcionarios (funcionario_id, nome, data_nasc, sexo, tipo_funcionario, permissao) " +
                               "VALUES (@FuncionarioId, @Nome, @DataNasc, @Sexo, @TipoFuncionario, @Permissao)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuncionarioId", funcionario.funcionario_id);
                    command.Parameters.AddWithValue("@Nome", funcionario.nome);
                    command.Parameters.AddWithValue("@DataNasc", funcionario.data_nasc);
                    command.Parameters.AddWithValue("@Sexo", funcionario.sexo);
                    command.Parameters.AddWithValue("@TipoFuncionario", funcionario.tipo_funcionario);
                    command.Parameters.AddWithValue("@Permissao", funcionario.permissao);

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
                                funcionario_id = reader.GetInt32(reader.GetOrdinal("funcionario_id")),
                                nome = reader.GetString(reader.GetOrdinal("nome")),
                                data_nasc = reader.GetDateTime(reader.GetOrdinal("data_nasc")),
                                sexo = reader.GetString(reader.GetOrdinal("sexo")),
                                tipo_funcionario = reader.GetString(reader.GetOrdinal("tipo_funcionario")),
                                permissao = reader.GetInt32(reader.GetOrdinal("permissao"))
                            };

                            Console.WriteLine($"Editar Funcionário: {funcionario.nome}");
                            Console.WriteLine("Novo nome (pressione Enter para manter o mesmo):");
                            string novoNome = Console.ReadLine();
                            if (!string.IsNullOrEmpty(novoNome))
                            {
                                funcionario.nome = novoNome;
                            }

                            Console.WriteLine("Nova data de nascimento (yyyy-MM-dd) (pressione Enter para manter a mesma):");
                            string novaData = Console.ReadLine();
                            if (!string.IsNullOrEmpty(novaData) && DateTime.TryParse(novaData, out DateTime dataNasc))
                            {
                                funcionario.data_nasc = dataNasc;
                            }

                            // Repita o processo para os outros campos que deseja editar

                            // Agora, faça a atualização na base de dados
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
                    command.Parameters.AddWithValue("@FuncionarioId", funcionario.funcionario_id);
                    command.Parameters.AddWithValue("@Nome", funcionario.nome);
                    command.Parameters.AddWithValue("@DataNasc", funcionario.data_nasc);
                    command.Parameters.AddWithValue("@Sexo", funcionario.sexo);
                    command.Parameters.AddWithValue("@TipoFuncionario", funcionario.tipo_funcionario);

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
