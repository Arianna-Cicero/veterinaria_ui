using Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System;
using veterinaria_ui.Presentation;
using System.Runtime.Remoting.Messaging;

public class LoginManager
{

    private readonly List<string> UsernamesExistentes = new List<string>();
    private readonly MenuOpcoes menuOpcoes;
    private readonly Login login;
    private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;


    public void GetLoginInfoFromUser()
    {      
        int largura = 40;
        int opcao;
        do
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("1. Realizar registo");
            Console.WriteLine("2. Login");
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("Escreva a opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());
            LoopDeco.ExibirLinhaDecorativa(largura);

            if (opcao == 1)
            {
                Console.WriteLine("Insira o username que pretende utilizar:");
                login.Username = Console.ReadLine();
                if (GetValidUsername(login.Username))
                {
                    Console.WriteLine("Insira a password que pretende utilizar:");
                    login.Password = Console.ReadLine();
                    if (IsValidPassword(login.Password))
                    {                    
                        login.Permissao = 3;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "INSERT INTO Login (Username, Password, Permissao) VALUES (@Username, @Password, @Permissao)";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Username", login.Username);
                                command.Parameters.AddWithValue("@Password", login.Password);
                                command.Parameters.AddWithValue("@Permissao", login.Permissao);
                  
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    LoopDeco.ExibirLinhaDecorativa(largura);
                                    Console.WriteLine("O seu registo foi realizado com sucesso e inserido na base de dados!");
                                    LoopDeco.ExibirLinhaDecorativa(largura);
                                    CallShowMenu();
                                }
                                else
                                {
                                    LoopDeco.ExibirLinhaDecorativa(largura);
                                    Console.WriteLine("Erro ao inserir registo na base de dados. Voltará ao menu inicial");
                                    LoopDeco.ExibirLinhaDecorativa(largura);
                                    Console.ReadKey();
                                    break;
                                }                            
                            }
                        }
                    }
                }
            }
            else if (opcao == 2)
            {
                Console.WriteLine("Insira o seu username: ");
                 login.Username = Console.ReadLine();

                Console.WriteLine("Insira a sua senha: ");
                 login.Password = Console.ReadLine();

                if (IsLoginValid())
                {
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    Console.WriteLine("Login bem-sucedido!");                  
                    CallShowMenu();
                }
                else
                {
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    Console.WriteLine("Nome de utilizador ou password incorretos. Tente novamente.");
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    GetLoginInfoFromUser();
                }
            }

        } while (opcao != 0);
    }

    public LoginManager(Login sharedLogin, MenuOpcoes sharedMenuOpcoes)
    {
        login = sharedLogin;
        menuOpcoes = sharedMenuOpcoes;
    }

    private bool IsLoginValid()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

             
                string username = login.Username;
                string password = login.Password;

                string loginQuery = "SELECT Username, Password, Permissao FROM Login WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(loginQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);                   
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            login.Username = reader["Username"].ToString();
                            login.Password = reader["Password"].ToString();
                            login.Permissao = Convert.ToInt32(reader["Permissao"]);
                            
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return true;      
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
                login.Username = Console.ReadLine();
            }
            else if (CheckUsernameExistsInDatabase(newUsername))
            {
                Console.WriteLine("Username já existe. Escolha outro:");
                login.Username = Console.ReadLine();
                GetValidUsername(login.Username);
            }
        } while (string.IsNullOrEmpty(newUsername) || CheckUsernameExistsInDatabase(newUsername));

        UsernamesExistentes.Add(newUsername);
        login.Username = newUsername;

        return true;
    }
    private bool CheckUsernameExistsInDatabase(string username)
    {
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

    public int GetProprietarioIdByLoginUsername(string username)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Proprietario_id FROM Proprietario WHERE Username = @Username";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; 
                }
            }
        }
    }


}
