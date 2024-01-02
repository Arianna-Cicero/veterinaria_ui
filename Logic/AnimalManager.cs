using Data;
using System;
using System.Configuration;
using System.Data.SqlClient;
using veterinaria_ui.Presentation;

namespace Logic
{
    public class AnimalManager
    {
        Animal animal = new Animal();
        int largura = 40;
        public void GetAnimalInfoFromUser()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);

            Console.WriteLine("Insira as informações do animal");

            LoopDeco.ExibirLinhaDecorativa(largura);

            animal.Animal_id = IDgenerator.GenerateUniqueRandomID();

            Console.WriteLine("Nome:");
            string nomeInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nomeInput))
            {
                Console.WriteLine("Nome inválido. Por favor, insira um nome válido:");
                nomeInput = Console.ReadLine();
            }
            animal.Nome = nomeInput;

            Console.WriteLine("Data de nascimento (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                animal.Data_nasc = dataNasc;
            }
            else
            {
                Console.WriteLine("Data de Nascimento inválida. Utilize o valor padrão (yyyy-MM-dd).");
                animal.Data_nasc = DateTime.MinValue;
            }

            Console.WriteLine("Sexo (Feminino/Masculino):");
            string sexoInput = Console.ReadLine();
            while (sexoInput.ToLower() != "feminino" && sexoInput.ToLower() != "masculino")
            {
                Console.WriteLine("Opção de sexo incorreta. As opções disponíveis são Feminino ou Masculino:");
                sexoInput = Console.ReadLine();
            }
            animal.Sexo = sexoInput;

            Console.WriteLine("Cor:");
            animal.Cor = Console.ReadLine();

            Console.WriteLine("Raça:");
            animal.Raca = Console.ReadLine();

            Console.WriteLine("Especie:");
            animal.Especie = Console.ReadLine();

            Console.WriteLine("ID do Proprietário:");
            if (int.TryParse(Console.ReadLine(), out int proprietarioId))
            {
                animal.Proprietario_id = proprietarioId;
            }
            else
            {
                Console.WriteLine("ID do proprietário inválido.");
                return;
            }

            SaveAnimalToDatabase(animal);
        }

        public void SaveAnimalToDatabase(Animal animal)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Animal (animal_id, nome, data_nasc, sexo, cor, raca, especie, proprietario_id) " +
                               "VALUES (@AnimalId, @Nome, @DataNasc, @Sexo, @Cor, @Raca, @Especie, @ProprietarioId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimalId", animal.Animal_id);
                    command.Parameters.AddWithValue("@Nome", animal.Nome);
                    command.Parameters.AddWithValue("@DataNasc", animal.Data_nasc);
                    command.Parameters.AddWithValue("@Sexo", animal.Sexo);
                    command.Parameters.AddWithValue("@Cor", animal.Cor);
                    command.Parameters.AddWithValue("@Raca", animal.Raca);
                    command.Parameters.AddWithValue("@Especie", animal.Especie);
                    command.Parameters.AddWithValue("@ProprietarioId", animal.Proprietario_id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Animal registrado com sucesso na base de dados!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao registrar o animal na base de dados.");
                    }
                }
            }
        }

        public Animal GetAnimalById(string proprietarioID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Animal_id, Nome, Data_nasc, Especie, Cor, Raca, Proprietario_id FROM Animal WHERE Proprietario_id = @proprietarioId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@proprietarioId", proprietarioID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Animal animal = new Animal
                            {
                                Animal_id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Data_nasc = reader.GetDateTime(2),
                                Especie = reader.GetString(3),
                                Cor = reader.GetString(4),
                                Raca = reader.GetString(5),
                                Proprietario_id = reader.GetInt32(6)
                            };

                            return animal;
                        }
                        else
                        {
                            Console.WriteLine("Animal não encontrado na base de dados.");
                            return null;
                        }
                    }
                }
            }
        }

        public void GetAnimalInfo(Animal animal)
        {
            Console.WriteLine($"ID do Animal : {animal.Animal_id}");
            Console.WriteLine($"Nome: {animal.Nome}");
            Console.WriteLine($"Data de Nascimento: {animal.Data_nasc.ToShortDateString()}");
            Console.WriteLine($"Especie: {animal.Especie}");
            Console.WriteLine($"Cor: {animal.Cor}");
            Console.WriteLine($"Raça: {animal.Raca}");
            Console.WriteLine($"ID de proprietario: {animal.Proprietario_id}");
        }

        public void ListAllAnimals()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);

            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Listar todos os animais");
            Console.WriteLine("2. Listar animal por ID");
            Console.WriteLine("Opção: ");
            


            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                LoopDeco.ExibirLinhaDecorativa(largura);
                switch (opcao)
                {
                    case 1:
                        ListAllAnimalsFromDatabase();
                        break;
                    case 2:
                        ListAnimalById();
                        break;
                    default:
                        LoopDeco.ExibirLinhaDecorativa(largura);

                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                LoopDeco.ExibirLinhaDecorativa(largura);

                Console.WriteLine("Opção inválida.");
            }
        }

        private void ListAllAnimalsFromDatabase()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Animal";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LoopDeco.ExibirLinhaDecorativa(largura);

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.WriteLine($"{reader.GetName(i)}: {reader.GetValue(i)}");
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoopDeco.ExibirLinhaDecorativa(largura);

                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        private void ListAnimalById()
        {
            LoopDeco.ExibirLinhaDecorativa(largura);
            Console.WriteLine("Insira o ID do animal: ");
            


            if (int.TryParse(Console.ReadLine(), out int animalId))
            {
                LoopDeco.ExibirLinhaDecorativa(largura);
                Animal animal = GetAnimalById(animalId);

                if (animal != null)
                {
                    PrintAnimalInfo(animal);
                }
            }
            else
            {
                Console.WriteLine("ID do animal inválido.");
                LoopDeco.ExibirLinhaDecorativa(largura);

            }
        }

        private void PrintAnimalInfo(Animal animal)
        {
            Console.WriteLine($"ID do Animal: {animal.Animal_id}");
            Console.WriteLine($"Nome: {animal.Nome}");
            Console.WriteLine($"Data de Nascimento: {animal.Data_nasc.ToShortDateString()}");
            Console.WriteLine($"Sexo: {animal.Sexo}");
            Console.WriteLine($"Cor: {animal.Cor}");
            Console.WriteLine($"Raça: {animal.Raca}");
            Console.WriteLine($"Especie: {animal.Especie}");
            Console.WriteLine($"ID de proprietário: {animal.Proprietario_id}");
        }
    }
}
