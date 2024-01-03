using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria_ui.Presentation
{
    public class MenuOpcoes
    {
        private readonly ConsultaManager consultaManager;
        private readonly FaturaManager faturamanager;
        private readonly AnimalManager animalManager;
        private readonly FuncionarioManager funcionarioManager;
        private readonly Funcionario funcionario;
        private readonly int largura = 40;
        private readonly Login login;

        public MenuOpcoes(Login sharedLogin, ConsultaManager consultaManager, FaturaManager faturamanager, AnimalManager animalManager, FuncionarioManager funcionarioManager, Funcionario funcionario)
        {
            this.login = sharedLogin;
            this.consultaManager = consultaManager;
            this.faturamanager = faturamanager;
            this.animalManager = animalManager;
            this.funcionarioManager = funcionarioManager;
            this.funcionario = funcionario;
        }

        public void ShowMenu()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            LoopDeco.ExibirLinhaCentralizada("Menu opções", largura);
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("1. Ver consultas");
            Console.WriteLine("2. Consultar Faturas ");
            Console.WriteLine("3. Informações do meu animal");
            if (login.Permissao == 2 || login.Permissao == 1)
            {
                Console.WriteLine("4. Marcação de consultas");
                Console.WriteLine("5. Consultar Animais");
                Console.WriteLine("6. Registar Animais");
                if (login.Permissao == 1)
                {
                    Console.WriteLine("7. Gerir funcionarios");
                }
            }
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("Escreva a opção pretendida: ");
            string input = Console.ReadLine();
            int opcao = int.Parse(input);

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
                    ShowMenu();
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
                        Console.WriteLine("Por favor, escreva o ID do animal: ");
                        string inputIdanimal = Console.ReadLine();
                        int opcaoIdAnimal = int.Parse(inputIdanimal);
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        LoopDeco.ExibirLinhaCentralizada("Lista de todas as consultas existentes do cliente", largura);
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        consultaManager.ListarConsultasPorCliente(opcaoIdAnimal);
                        
                        break;
                    default:
                        Console.WriteLine("Opção invalida. Voltara ao menu inicial.");
                        ShowMenu();
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
            animalManager.ListAnimalsByOwner();
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

            Console.WriteLine("Data da consulta (yyyy-MM-dd): ");
            string dataConsultaString = Console.ReadLine();
            DateTime dataConsulta;

            if (DateTime.TryParse(dataConsultaString, out dataConsulta))
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Data inválida. Por favor, insira uma data no formato correto (yyyy-MM-dd).");
                return;
            }

            Console.Write("Motivo: ");
            string motivo = Console.ReadLine();

            Console.Write("ID de funcionario: ");
            string funcionarioIdstring = Console.ReadLine();
            int funcionarioId = int.Parse(funcionarioIdstring);

            consultaManager.AgendarConsulta(animalId, dataConsulta, motivo, funcionarioId);
        }
        public void ConsultarAnimais()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            if (login.Permissao == 2 || login.Permissao == 1)
            {
                LoopDeco.ExibirLinhaDecorativa(largura);
                Console.WriteLine("Consultar animais");
                LoopDeco.ExibirLinhaDecorativa(largura);
                animalManager.ListAllAnimals();
                
            }
            else
            {
                Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                ShowMenu();

            }
        }

        public void GerirFuncionarios()
        {
            if (login.Permissao == 1)
            {
                Console.WriteLine("1. Adicionar funcionario");
                Console.WriteLine("2. Editar funcionario");            
                Console.WriteLine("3. Consultar funcionarios");
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
                        funcionario.Funcionario_id = int.Parse(Console.ReadLine());
                        funcionarioManager.EditarFuncionarioPorId(funcionario.Funcionario_id);
                        break;
                    
                    case 3:
                        Console.WriteLine();
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        funcionarioManager.GetFuncionario();
                        LoopDeco.ExibirLinhaDecorativa(largura);
                        break;
                    default:
                        Console.WriteLine("Opção invalida. Voltara ao menu inicial.");
                        ShowMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                ShowMenu();

            }
        }
        public void ResgistarAnimais()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            if (login.Permissao == 2 || login.Permissao == 1)
            {
                LoopDeco.ExibirLinhaDecorativa(largura);
                Console.WriteLine("Registar animais");
                LoopDeco.ExibirLinhaDecorativa(largura);
                animalManager.GetAnimalInfoFromUser();
            }
            else
            {
                Console.WriteLine("Opção inválida. Certifique-se de inserir alguma das opções: ");
                ShowMenu();
            }
        }       
    }
}
