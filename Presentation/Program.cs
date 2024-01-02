using Data;
using Logic;
using Logics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using veterinaria_ui.Presentation;

namespace veterinaria_ui
{

    internal class Program
    {        
        static void Main(string[] args)
        {
            Login sharedLogin = new Login();
            ConsultaManager consultaManager = new ConsultaManager(); 
            FaturaManager faturaManager = new FaturaManager(); 
            AnimalManager animalManager = new AnimalManager(); 
            FuncionarioManager funcionarioManager = new FuncionarioManager(); 
            Funcionario funcionario = new Funcionario(); 
            MenuOpcoes sharedMenuOpcoes = new MenuOpcoes(sharedLogin, consultaManager, faturaManager, animalManager, funcionarioManager, funcionario);
            LoginManager loginManager = new LoginManager(sharedLogin, sharedMenuOpcoes);
            int opcao;
            
            do
            {
                Console.Clear();
                Console.WriteLine("Bem-vindo à veterinária patinhas!");
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Entrar");
                Console.WriteLine("2. Sair");

                Console.Write("Escolha a opção desejada: ");
                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            loginManager.GetLoginInfoFromUser();
                            break;
                        case 2:
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
