using Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria_ui
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoginManager loginManager = new LoginManager();
            int opcao;
            do
            {
                Console.Clear(); // Limpa a consola a cada iteração para um visual mais limpo
                Console.WriteLine("Bem-vindo à veterinária patinhas!");
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Inicio de sessão");
                Console.WriteLine("2. Registo");
                Console.WriteLine("3. Sair");

                Console.Write("Escolha a opção desejada: ");
                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            loginManager.GetLoginInfoFromUser();
                            break;

                        case 2:
                            loginManager.GetLoginInfoFromUser();
                            break;

                        case 3:
                            break;
              
                        default:
                            Console.WriteLine("\nPressione qualquer tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira um número.");
                    Main(args);
                }

            } while (opcao != 3);
        
        }
    }
}
