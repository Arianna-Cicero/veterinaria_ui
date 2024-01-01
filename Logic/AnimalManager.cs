using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{

    public class AnimalManager
    {
        Animal animal = new Animal();
        
        //RETRIEVE
        public void GetAnimalInfoFromUser()
        {
            Console.WriteLine("Insira as informações do animal");

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
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "INSERT INTO Animais (animal_id, nome, data_nasc, sexo, cor, raca, especie, proprietario_id) " +
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
        public Animal GetAnimalById(int animalId)
        {
            using (SqlConnection connection = DatabaseManager.GetConnection())
            {
                DatabaseManager.OpenConnection(connection);

                string query = "SELECT animal_id, nome, data_nasc, especie, cor, raca, proprietario_id FROM Animais WHERE animal_id = @AnimalId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimalId", animalId);

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
                            return null; // Retorna nulo se o animal não for encontrado
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
    }
}
