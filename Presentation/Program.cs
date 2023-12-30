using Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria_ui
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoginManager loginManager = new LoginManager();
            int opcao = 0;

            do
            {
                Console.Clear(); // Limpa a consola a cada iteração para um visual mais limpo
                Console.WriteLine("Bem-vindo à veterinária patinhas!");
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Inicio de sessão");
                Console.WriteLine("2. Registo");
                Console.WriteLine("3. Ajuda");
                Console.WriteLine("4. Sair");

                Console.Write("Escolha a opção desejada: ");
                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            
                            break;

                        case 2:
                            loginManager.GetLoginInfoFromUser();
                            break;

                        case 3:
                            break;

                        case 4:
                            break;

                        default:
                            break;
                    }
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira um número.");
                    Console.ReadKey();
                }

            } while (opcao != 4);


            ////TESTE DA BASE DE DADOS
            //using (SqlConnection connection = DatabaseManager.GetConnection())
            //{
            //    DatabaseManager.OpenConnection(connection);
            //    try
            //    {
            //        connection.Open();
            //        // Ejecutar una consulta simple
            //        string query = "SELECT 1";
            //        using (SqlCommand command = new SqlCommand(query, connection))
            //        {
            //            int result = (int)command.ExecuteScalar();
            //            Console.WriteLine("Resultado da consulta: " + result);
            //        }
            //        Console.WriteLine("0 para sair");
            //        String opcao = Console.ReadLine();
            //        if (opcao == "0")
            //        {
            //            Console.WriteLine("close");
            //            DatabaseManager.CloseConnection(connection);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Error: " + ex.Message);
            //    }

            //}
        }
    }
}
