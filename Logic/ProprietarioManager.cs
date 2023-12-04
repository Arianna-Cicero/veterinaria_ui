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
            proprietario.proprietario_id = IDgenerator.GenerateUniqueRandomID();
            Console.WriteLine($"ID do proprietario: {proprietario.proprietario_id}");
            //TODO validação char nome
            Console.WriteLine("Nome:");
            proprietario.nome = Console.ReadLine();
            //TODO validação datetime data
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                proprietario.data_nasc = dataNasc;
            }
            else
            {
                Console.WriteLine("Data de Nascimento inválida. Utilice o valor padrão (yyyy-MM-dd).");
                proprietario.data_nasc = DateTime.MinValue;
            }
            //TODO validação char sexo
            Console.WriteLine("Sexo:");
            proprietario.sexo = Console.ReadLine();
            if (proprietario.sexo != "Masculino" || proprietario.sexo != "Feminino")
            {
                Console.WriteLine("O sexo inserido está incorreto");
                Console.WriteLine("As opções disponiveis são Feminino ou Masculino");
                Console.WriteLine("Sexo:");
                proprietario.sexo = Console.ReadLine();
            }
        }

        public void GetProprietario(Proprietario proprietario)
        {
            Console.WriteLine($"ID de proprietario: {proprietario.proprietario_id}");
            Console.WriteLine($"Nome: {proprietario.nome}");
            Console.WriteLine($"Data de Nascimento: {proprietario.data_nasc.ToShortDateString()}");
            Console.WriteLine($"Sexo: {proprietario.sexo}");
            Console.WriteLine($"Utilizador: {proprietario.user}");
        }
    }
}
