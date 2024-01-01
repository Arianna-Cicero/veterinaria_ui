using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProprietarioManager
    {
        public void GetProprietarioInfoFromUser(Proprietario proprietario)
        {
            Console.WriteLine("Informações sobre o proprietario");
            proprietario.Proprietario_id = IDgenerator.GenerateUniqueRandomID();
            Console.WriteLine($"ID do proprietario: {proprietario.Proprietario_id}");
            //TODO validação char nome
            Console.WriteLine("Nome:");
            proprietario.Nome = Console.ReadLine();
            //TODO validação datetime data
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                proprietario.Data_nasc = dataNasc;
            }
            else
            {
                Console.WriteLine("Data de Nascimento inválida. Utilice o valor padrão (yyyy-MM-dd).");
                proprietario.Data_nasc = DateTime.MinValue;
            }
            //TODO validação char sexo
            Console.WriteLine("Sexo:");
            proprietario.Sexo = Console.ReadLine();
            if (proprietario.Sexo != "Masculino" || proprietario.Sexo != "Feminino")
            {
                Console.WriteLine("O sexo inserido está incorreto");
                Console.WriteLine("As opções disponiveis são Feminino ou Masculino");
                Console.WriteLine("Sexo:");
                proprietario.Sexo = Console.ReadLine();
            }
        }

        public void GetProprietario(Proprietario proprietario)
        {
            Console.WriteLine($"ID de proprietario: {proprietario.Proprietario_id}");
            Console.WriteLine($"Nome: {proprietario.Nome}");
            Console.WriteLine($"Data de Nascimento: {proprietario.Data_nasc.ToShortDateString()}");
            Console.WriteLine($"Sexo: {proprietario.Sexo}");
            Console.WriteLine($"Utilizador: {proprietario.User}");
        }
    }
}
