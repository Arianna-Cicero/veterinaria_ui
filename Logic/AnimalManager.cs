using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{

    public class AnimalManager
    {
        //RETRIEVE
        public void GetAnimalInfoFromUser(Animal animal)
        {
            Console.WriteLine("Insira as informações do animal");
            //TODO validação char nome
            animal.animal_id = IDgenerator.GenerateUniqueRandomID();
            Console.WriteLine("ID do animal: ");
            Console.WriteLine("Nome:");
            animal.nome = Console.ReadLine();
            Console.WriteLine("Data nascimento (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                animal.data_nasc = dataNasc;
            }
            else
            {
                Console.WriteLine("Data de Nascimento inválida. Utilice o valor padrão (yyyy-MM-dd).");
                animal.data_nasc = DateTime.MinValue;
            }
            //TODO validação char sexo
            Console.WriteLine("Sexo:");
            animal.sexo = Console.ReadLine();
            if (animal.sexo != "Feminino" || animal.sexo != "Masculino")
            {
                Console.Write("Erro. O sexo assinalado é incorreto.");
                Console.WriteLine("As opções disponiveis são Feminino ou Masculino");
                Console.WriteLine("Sexo:");
                animal.sexo = Console.ReadLine();
            }
            //TODO validação char cor
            Console.WriteLine("Cor:");
            animal.cor = Console.ReadLine();
            //TODO validação char raça
            Console.WriteLine("Raça:");
            animal.raca = Console.ReadLine();
            //TODO validação char especie
            Console.WriteLine("Especie:");
            animal.especie = Console.ReadLine();
            //TODO validação char especie
            //Fazer logica proprietario_id diferente
            Console.WriteLine("Proprietario:");
            animal.proprietario_id = int.Parse(Console.ReadLine());
        }
        //GET
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
