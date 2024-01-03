using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using veterinaria_ui.Presentation;

namespace Logic
{
    public class FuncionarioManager
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private readonly Login login;
        private readonly List<string> UsernamesExistentes = new List<string>();
        private readonly MenuOpcoes menuOpcoes;
        readonly int largura = 40;

        public void GetFuncionarioInfoFromUser(Funcionario funcionario)
        {

            Console.WriteLine("Insira o username para o utilizador do funcionario:");
            string Username = Console.ReadLine();
            if (GetValidUsername(Username))
            {
                Console.WriteLine("Insira a password para o utilizador do funcionario:");
                string Password = Console.ReadLine();
                if (IsValidPassword(Password))
                {
                    int Permissao = 2;

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string query = "INSERT INTO Login (Username, Password, Permissao) VALUES (@Username, @Password, @Permissao)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", Username);
                            command.Parameters.AddWithValue("@Password", Password);
                            command.Parameters.AddWithValue("@Permissao", Permissao);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                LoopDeco.ExibirLinhaDecorativa(largura);
                                Console.WriteLine("O seu registo foi realizado com sucesso e inserido na base de dados!");
                                LoopDeco.ExibirLinhaDecorativa(largura);                              
                            }
                            else
                            {
                                LoopDeco.ExibirLinhaDecorativa(largura);
                                Console.WriteLine("Erro ao inserir registo na base de dados. Voltará ao menu inicial");
                                LoopDeco.ExibirLinhaDecorativa(largura);
                                Console.ReadKey();
                                
                            }
                        }
                    }
                }
            }

            LoopDeco.ExibirLinhaDecorativa(largura);
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

            funcionario.Username = Username;
            SaveFuncionarioToDatabase(funcionario);
        }

        public void SaveFuncionarioToDatabase(Funcionario funcionario)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Funcionario (nome, data_nasc, sexo, tipo_funcionario, Username) " +
                               "VALUES (@Nome, @DataNasc, @Sexo, @TipoFuncionario, @Username )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuncionarioId", funcionario.Funcionario_id);
                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@DataNasc", funcionario.Data_nasc);
                    command.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                    command.Parameters.AddWithValue("@TipoFuncionario", funcionario.Tipo_funcionario);
                    command.Parameters.AddWithValue("@Username", funcionario.Username);

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
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT * FROM Funcionario WHERE funcionario_id = @FuncionarioId";

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
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Funcionario " +
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


        public void GetFuncionario()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Funcionario";

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

        private bool IsValidPassword(string password)
        {

            Console.WriteLine("Insira novamente a sua senha para verificação:");
            string Verificacao = Console.ReadLine();
            if (password.Length < 8)
            {
                Console.WriteLine("A senha deve ter pelo menos 8 caracteres.");
                return false;
            }
            else
            {
                if (password != Verificacao)
                {
                    Console.WriteLine("As senhas não coincidem");
                    return false;

                }
                else
                {
                    return true;

                }
            }
        }

        private bool GetValidUsername(string newUsername)
        {
            do
            {
                if (string.IsNullOrEmpty(newUsername))
                {
                    Console.WriteLine("Username não pode estar vazio. Insira o username novamente:");
                    newUsername = Console.ReadLine();
                }
                else if (CheckUsernameExistsInDatabase(newUsername))
                {
                    Console.WriteLine("Username já existe. Escolha outro:");
                    newUsername = Console.ReadLine();
                    GetValidUsername(newUsername);
                }
            } while (string.IsNullOrEmpty(newUsername) || CheckUsernameExistsInDatabase(newUsername));

            UsernamesExistentes.Add(newUsername);
            

            return true;
        }
        private bool CheckUsernameExistsInDatabase(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Login WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(checkQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        public void CallShowMenu()
        {
            if (menuOpcoes != null)
            {
                menuOpcoes.ShowMenu();
            }
            else
            {
                Console.WriteLine("MenuOpcoes instance is not available.");
            }
        }

    }
}
