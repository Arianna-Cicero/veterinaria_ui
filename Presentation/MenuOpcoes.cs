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
        ConsultaManager consultaManager = new ConsultaManager();
        FaturaManager faturamanager = new FaturaManager();
        AnimalManager animalManager = new AnimalManager();
        FuncionarioManager funcionarioManager = new FuncionarioManager();
        Funcionario funcionario = new Funcionario();
        Login login = new Login();
        readonly int largura = 40;

        public void Menuopcoes() 
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            LoopDeco.ExibirLinhaCentralizada("Menu opções", largura);
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("1. Ver consultas");
            Console.WriteLine("2. Consultar Faturas ");
            Console.WriteLine("3. Informações do animal");
            if (login.Permissao == 2 || login.Permissao == 1)
            {
                Console.WriteLine("4. Marcação de consultas");
                Console.WriteLine("5. Consultar Animais");
                Console.WriteLine("6. Registar Animais");
                if(login.Permissao == 1)
                {
                    Console.WriteLine("7. Gerir funcionarios");
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
                    VerConsultas();
                    break;
                case 2:
                    ConsultarFaturas();
                    break;
                case 3:
                    InformacoesAnimal();                 
                    break;
                case 4:
                    MarcacaoConsultas();
                    break;              
                case 5:
                    ConsultarAnimais();
                    break;
                case 6:
                    ResgistarAnimais();
                    break;             
                case 7:
                    GerirFuncionarios();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                    Menuopcoes();
                    break;
            }
        }

        public void VerConsultas()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            if (login.Permissao == 1 || login.Permissao == 2)
            {
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

                    case 2:
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("Por favor, escreva o ID do cliente: ");
                        string inputIdCliente = Console.ReadLine();
                        int opcaoIdCliente = int.Parse(inputIdCliente);
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        LoopDeco.ExibirLinhaCentralizada("Lista de todas as consultas existentes do cliente", largura);
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        consultaManager.ListarConsultasPorCliente(opcaoIdCliente);
                        break;
                    default:
                        Console.WriteLine("Opção invalida. Voltara ao menu inicial.");
                        Menuopcoes();
                        break;
                }
            }
        }
        public void ConsultarFaturas()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            if (login.Permissao == 1 || login.Permissao == 2)
            {
                bool opcaoValida = false;

                while (!opcaoValida)
                {
                    LoopDeco.ExibirLinhaDecorativa(largura);
                    Console.WriteLine("1. Consultar todas as faturas existentes");
                    Console.WriteLine("2. Criar fatura");
                    Console.Write("Escreva a opção que deseja: ");
                    string inputFatura = Console.ReadLine();

                    if (int.TryParse(inputFatura, out int opcaoFatura))
                    {
                        LoopDeco.ExibirLinhaDecorativa(largura);

                        switch (opcaoFatura)
                        {
                            case 1:
                                faturamanager.GetAllFaturas();
                                break;
                            case 2:
                                faturamanager.CreateFaturaInfoFromUser();
                                break;
                            default:
                                Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções.");
                                continue;
                        }
                        opcaoValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Por favor, insira um número válido.");
                    }
                }

            }
            if (login.Permissao == 3)
            {
                LoopDeco.ExibirLinhaDecorativa(largura);
                Console.WriteLine("Consultar as faturas do cliente", largura);
                LoopDeco.ExibirLinhaDecorativa(largura);
                faturamanager.GetFaturaByUser(login.Username);
            }
        }

        public void InformacoesAnimal()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("As informações sobre o seu animal:");
            LoopDeco.ExibirLinhaDecorativa(largura);
        }

        public void MarcacaoConsultas()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("Marcação de consultas", largura);
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("Indique as seguintes informações para a marcação da consulta:");
            Console.WriteLine("ID do animal: ");
            string id_animal = Console.ReadLine();
            int animalId = int.Parse(id_animal);
            Console.WriteLine("Data da consulta: ");
            string dataConsultaString = Console.ReadLine();
            DateTime dataConsulta;
            if (DateTime.TryParse(dataConsultaString, out dataConsulta))
            {
                Console.WriteLine($"Date of Consultation: {dataConsulta.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("Data invalida. Por favor insira uma data no formato correto (dia/mês/ano).");

            }
            Console.Write("Motivo: ");
            string motivo = Console.ReadLine();

            consultaManager.AgendarConsulta(animalId, dataConsulta, motivo);
        }
        public void ConsultarAnimais()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            if (login.Permissao == 2)
            {
                LoopDeco.ExibirLinhaDecorativa(largura);
                Console.WriteLine("Consultar animais");
                LoopDeco.ExibirLinhaDecorativa(largura);
                Console.WriteLine("Insira o ID do animal:");
                if (int.TryParse(Console.ReadLine(), out int animalID))
                {
                    Animal animalConsultado = animalManager.GetAnimalById(animalID);

                    if (animalConsultado != null)
                    {
                        Console.WriteLine("Animal encontrado:");
                        animalManager.GetAnimalInfo(animalConsultado);
                    }
                }
                else
                {
                    Console.WriteLine("ID do animal inválido.");
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                Menuopcoes();

            }
        }

        public void GerirFuncionarios()
        {
            if (login.Permissao == 1)
            {
                Console.WriteLine("1. Adicionar funcionario");
                Console.WriteLine("2. Editar funcionario");
                Console.WriteLine("3. Remover funcionario");
                Console.WriteLine("4. Consultar funcionarios");
                Console.Write("Escolha a opção que deseja: ");
                string input2 = Console.ReadLine();
                int opcao2 = int.Parse(input2);
                switch (opcao2)
                {
                    case 1:
                        Console.WriteLine();
                        funcionarioManager.GetFuncionarioInfoFromUser(funcionario);
                        break;
                    case 2:
                        Console.WriteLine();
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("Insira o ID do funcionario que pretente editar");
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        funcionario.funcionario_id = int.Parse(Console.ReadLine());
                        funcionarioManager.EditarFuncionarioPorId(funcionario.funcionario_id);
                        break;
                    case 3:
                        Console.WriteLine();
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        Console.WriteLine("Insira o ID do funcionário que pretende remover:");
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        if (int.TryParse(Console.ReadLine(), out int funcionarioId))
                        {
                            funcionarioManager.RemoverFuncionarioPorId(funcionarioId);
                        }
                        else
                        {
                            Console.WriteLine("Por favor, insira um ID válido (número inteiro).");
                            Console.WriteLine("Opção invalida. Voltara ao menu inicial.");
                            Menuopcoes();
                            break;
                        }
                        break;
                    case 4:
                        Console.WriteLine();
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        funcionarioManager.GetFuncionario();
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        break;
                    default:
                        Console.WriteLine("Opção invalida. Voltara ao menu inicial.");
                        Menuopcoes();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                Menuopcoes();

            }
        }
        public void ResgistarAnimais()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            if (login.Permissao == 2)
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
        }       
    }
}
