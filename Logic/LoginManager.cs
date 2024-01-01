using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using veterinaria_ui.Presentation;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Logic
{
    public class LoginManager
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private List<string> UsernamesExistentes = new List<string>();
        public void GetLoginInfoFromUser()
        {
            Login login = new Login();
            int opcao;
            int largura = 40;
            Console.WriteLine("1. Realizar registo");
            Console.WriteLine("2. Login");
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("Escreva a opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());
            LoopDeco.ExibirLinhaDecorativa(largura);
            if (opcao == 1)
            {
                Console.WriteLine("Insira o username que pretende utilizar:");
                login.user = Console.ReadLine();
                if (GetValidUsername(login))
                {
                    Console.WriteLine("Insira a password que pretende utilizar:");
                    login.password = Console.ReadLine();
                    if (IsValidPassword(login))
                    {
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("O seu registo foi realizado com sucesso");
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        using (SqlConnection connection = DatabaseManager.GetConnection())
                        {
                            DatabaseManager.OpenConnection(connection);

                            string query = "INSERT INTO Login (username, password) VALUES (@Username, @Password)";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                
                                command.Parameters.AddWithValue("@Username", login.user);
                                command.Parameters.AddWithValue("@Password", login.password);

                                try
                                {
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
                                        Console.WriteLine("Falha ao inserir registo na base de dados.");
                                        LoopDeco.ExibirLinhaDecorativa(largura);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LoopDeco.ExibirLinhaDecorativa(largura);
                                    Console.WriteLine("Erro ao inserir registo na base de dados: " + ex.Message);
                                    LoopDeco.ExibirLinhaDecorativa(largura);
                                }
                            }
                        }
                    }
                }
            }
            else if (opcao == 2)
            {
                Console.WriteLine("Insira o seu username: ");
                string usernameInput = Console.ReadLine();

                Console.WriteLine("Insira a sua senha: ");
                string passwordInput = Console.ReadLine();

                if (IsLoginValid(usernameInput, passwordInput))
                {
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    Console.WriteLine("Login bem-sucedido!");
                    LoopDeco.ExibirLinhaDecorativa(largura);
                }
                else
                {
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    Console.WriteLine("Nome de utilizador ou password incorretos. Tente novamente.");
                    LoopDeco.ExibirLinhaDecorativa(largura);
                }
            }
        }
        public bool IsLoginValid(string enteredUsername, string enteredPassword)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT username, password FROM Login WHERE username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", enteredUsername);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPasswordFromDatabase = reader.GetString(reader.GetOrdinal("password"));

                            if (VerifyPassword(enteredPassword, hashedPasswordFromDatabase))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
        private string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool VerifyPassword(string enteredPassword, string hashedPasswordFromDatabase)
        {
            string hashedEnteredPassword = GetHash(enteredPassword);

            return string.Equals(hashedEnteredPassword, hashedPasswordFromDatabase, StringComparison.OrdinalIgnoreCase);
        }
        private bool IsValidPassword(Login login)
        {
            
            Console.WriteLine("Insira novamente a sua senha para verificação:");
            string Verificacao = Console.ReadLine();

            if (login.password != Verificacao)
            {
                Console.WriteLine("As senhas não coincidem. Tente novamente.");
                return false;
            }

            if (login.password.Length < 8)
            {
                Console.WriteLine("A senha deve ter pelo menos 8 caracteres.");
                return false;
            }

            login.password = Verificacao;
            return true;
        }


        private bool GetValidUsername(Login login)
        {
            string newUsername;
            do
            {
                newUsername = Console.ReadLine();

                if (string.IsNullOrEmpty(newUsername))
                {
                    Console.WriteLine("Username não pode estar vazio. Insira o username novamente:");
                    login.user = Console.ReadLine();
                }
                else if (UsernamesExistentes.Contains(newUsername))
                {
                    Console.WriteLine("Username já existe. Escolha outro:");
                    login.password = Console.ReadLine();
                }
            } while (string.IsNullOrEmpty(newUsername) || UsernamesExistentes.Contains(newUsername));

            UsernamesExistentes.Add(newUsername);
            login.user = newUsername;

            return true;
        }
    }
}

