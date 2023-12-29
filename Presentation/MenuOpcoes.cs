using Data;
using Logic;
using Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria_ui.Presentation
{
    public class MenuOpcoes
    {
        public void Menuopcoes() 
        {
            LoginManager loginManager = new LoginManager();
            ConsultaManager consultaManager = new ConsultaManager();
            FaturaManager faturamanager = new FaturaManager();
            AnimalManager animalManager = new AnimalManager();
            Fatura fatura = new Fatura();
            Login login = new Login();
            Animal animal = new Animal();
            int largura = 40;


            LoopDeco.ExibirLinhaDecorativa(largura);
            LoopDeco.ExibirLinhaCentralizada("Menu opções", largura);
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("1. Ver consultas");
            Console.WriteLine("2. Consultar Faturas ");
            Console.WriteLine("3. Consultar Pagamentos");
            Console.WriteLine("4. Pedido de Marcação de Consultas");
            if (login.permissao == 2 || login.permissao == 1)
            {
                Console.WriteLine("5. Consultar Animais");
                Console.WriteLine("6. Registar Animais");
                Console.WriteLine("7. Agendar Consultas");
                if(login.permissao == 1)
                {
                    Console.WriteLine("8. Gerir funcionarios");

                }
            }
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("Escreva a opção pretendida: ");
            string input = Console.ReadLine();
            int opcao = int.Parse(input);

            Console.WriteLine("Entrada inválida. Certifique-se de inserir algum dos números das opções.");

            switch (opcao)
            {
                case 1:
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    Console.WriteLine("1. Listar todas as consultas existentes");    
                    Console.WriteLine("2. Listar todas as consultas de um cliente");
                    Console.Write("Escreva a opção que deseja: ");
                    string inputConsulta = Console.ReadLine();
                    int opcaoConsulta = int.Parse(inputConsulta);
                    switch (opcaoConsulta)
                    {
                        case 1:
                            LoopDeco.ExibirLinhaDecorativa(largura);
                            LoopDeco.ExibirLinhaCentralizada("Lista de todas as consultas existentes", largura);
                            LoopDeco.ExibirLinhaDecorativa(largura);
                            consultaManager.ListarTodasConsultas();

                            break;

                        case 2: break;

                        default: 
                            
                            break;
                    }
                    break;
                case 2:
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    if (login.permissao == 1 || login.permissao == 2)
                    {
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("1. Consultar todas as faturas existentes", largura);
                        Console.WriteLine("2. Criar fatura", largura);
                        Console.Write("Escreva a opção que deseja: ");
                        string inputFatura = Console.ReadLine();
                        int opcaoFatura = int.Parse(inputFatura);
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        switch (opcaoFatura)
                        {
                            case 1:
                                opcao
                                break;
                            case 2: 

                                break;
                            default:

                                break;
                        }
                        faturamanager.GetAllFaturas();
                    }
                    if (login.permissao == 3 )
                    {
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("Consultar as faturas do cliente", largura);
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        faturamanager.GetFaturaByUser(login.user);
                    }
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    if (login.permissao == 2)
                    {
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("Consultar animais");
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("Insira o ID do animal:");
                        if (int.TryParse(Console.ReadLine(), out int animalId))
                        {
                            // Chamada para consultar o animal pelo ID
                            Animal animalConsultado = animalManager.GetAnimalById(animalId);

                            if (animalConsultado != null)
                            {
                                Console.WriteLine("Animal encontrado:");
                                animalManager.GetAnimalInfo(animalConsultado); // Mostra as informações do animal
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID do animal inválido.");
                        }
                        break;

                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                        Menuopcoes();

                    }
                    break;
                case 6:
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    if (login.permissao == 2)
                    {
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("Registar animais");
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        animalManager.GetAnimalInfoFromUser();
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                        Menuopcoes();

                    }
                    break;
                case 7:
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    if (login.permissao == 2)
                    {
                        Console.WriteLine("Agendar consulta");

                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                        Menuopcoes();

                    }
                    break;
                case 8:
                    if (login.permissao == 1)
                    {
                        Console.WriteLine("1. Adicionar funcionario");
                        Console.WriteLine("2. Editar funcionario");
                        Console.WriteLine("3. Remover funcionario");
                        Console.Write("Escolha a opção que deseja: ");
                        string input2 = Console.ReadLine();
                        int opcao2 = int.Parse(input2);
                        switch(opcao2)
                        {
                            case 1: Console.WriteLine();
                                break; 
                            case 2: Console.WriteLine();
                                break;
                            case 3: Console.WriteLine();
                                break;
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                        Menuopcoes();

                    }
                    break;
                default:
                    Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                    Menuopcoes();
                    break;
            }


        }
    }
}
