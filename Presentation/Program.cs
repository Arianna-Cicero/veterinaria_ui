using Data;
using Logic;
using System;
using veterinaria_ui.Presentation;

internal class Program
{
    static void Main(string[] args)
    {
        Data.Login sharedLogin = new Data.Login(); // Use the correct namespace for Login
        ConsultaManager consultaManager = new ConsultaManager();
        FaturaManager faturaManager = new FaturaManager();
        FuncionarioManager funcionarioManager = new FuncionarioManager();
        Funcionario funcionario = new Funcionario();
        AnimalManager animalManager = new AnimalManager(sharedLogin);
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

        } while (opcao != 2);
    }
}
