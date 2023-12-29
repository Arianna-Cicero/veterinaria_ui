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

            animal.animal_id = IDgenerator.GenerateUniqueRandomID();

            Console.WriteLine("Nome:");
            string nomeInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nomeInput))
            {
                Console.WriteLine("Nome inválido. Por favor, insira um nome válido:");
                nomeInput = Console.ReadLine();
            }
            animal.nome = nomeInput;

            Console.WriteLine("Data de nascimento (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                animal.data_nasc = dataNasc;
            }
            else
            {
                Console.WriteLine("Data de Nascimento inválida. Utilize o valor padrão (yyyy-MM-dd).");
                animal.data_nasc = DateTime.MinValue;
            }

            Console.WriteLine("Sexo (Feminino/Masculino):");
            string sexoInput = Console.ReadLine();
            while (sexoInput.ToLower() != "feminino" && sexoInput.ToLower() != "masculino")
            {
                Console.WriteLine("Opção de sexo incorreta. As opções disponíveis são Feminino ou Masculino:");
                sexoInput = Console.ReadLine();
            }
            animal.sexo = sexoInput;

            Console.WriteLine("Cor:");
            animal.cor = Console.ReadLine();

            Console.WriteLine("Raça:");
            animal.raca = Console.ReadLine();

            Console.WriteLine("Especie:");
            animal.especie = Console.ReadLine();

            Console.WriteLine("ID do Proprietário:");
            if (int.TryParse(Console.ReadLine(), out int proprietarioId))
            {
                animal.proprietario_id = proprietarioId;
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
                    command.Parameters.AddWithValue("@AnimalId", animal.animal_id);
                    command.Parameters.AddWithValue("@Nome", animal.nome);
                    command.Parameters.AddWithValue("@DataNasc", animal.data_nasc);
                    command.Parameters.AddWithValue("@Sexo", animal.sexo);
                    command.Parameters.AddWithValue("@Cor", animal.cor);
                    command.Parameters.AddWithValue("@Raca", animal.raca);
                    command.Parameters.AddWithValue("@Especie", animal.especie);
                    command.Parameters.AddWithValue("@ProprietarioId", animal.proprietario_id);

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
        //GET
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
                                animal_id = reader.GetInt32(0),
                                nome = reader.GetString(1),
                                data_nasc = reader.GetDateTime(2),
                                especie = reader.GetString(3),
                                cor = reader.GetString(4),
                                raca = reader.GetString(5),
                                proprietario_id = reader.GetInt32(6)
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
            Console.WriteLine($"ID do Animal : {animal.animal_id}");
            Console.WriteLine($"Nome: {animal.nome}");
            Console.WriteLine($"Data de Nascimento: {animal.data_nasc.ToShortDateString()}");
            Console.WriteLine($"Especie: {animal.especie}");
            Console.WriteLine($"Cor: {animal.cor}");
            Console.WriteLine($"Raça: {animal.raca}");
            Console.WriteLine($"ID de proprietario: {animal.proprietario_id}");
        }

        //PUT
    }
}
