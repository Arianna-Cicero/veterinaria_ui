using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class FuncionarioManager
    {
        //RETRIEVE
        public void GetFuncionarioInfoFromUser(Funcionario funcionario)
        {
            Console.WriteLine("Insira as informações do Funcionario");
            //TODO validação char nome
            Console.WriteLine($"ID do Funcionario: {funcionario.funcionario_id}");
            Console.WriteLine("Nome:");
            funcionario.nome = Console.ReadLine();
            Console.WriteLine("Data nascimento (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                funcionario.data_nasc = dataNasc;
            }
            else
            {
                Console.WriteLine("Data de Nascimento inválida. Utilice o valor padrão (yyyy-MM-dd).");
                funcionario.data_nasc = DateTime.MinValue;
            }
            //TODO validação char sexo
            Console.WriteLine("Sexo:");
            funcionario.sexo = Console.ReadLine();
            if (funcionario.sexo != "Feminino" || funcionario.sexo != "Masculino")
            {
                Console.Write("Erro. O sexo assinalado é incorreto.");
                Console.WriteLine("As opções disponiveis são Feminino ou Masculino");
                Console.WriteLine("Sexo:");
                funcionario.sexo = Console.ReadLine();
            }
            //TODO validação char nome
            Console.WriteLine("Tipo de funcionario:");
            funcionario.nome = Console.ReadLine();

        }
        //GET
        public void GetFuncionario(Funcionario funcionario)
        {
            Console.WriteLine($"ID do funcionario : {funcionario.funcionario_id}");
            Console.WriteLine($"Nome: {funcionario.nome}");
            Console.WriteLine($"Data de Nascimento: {funcionario.data_nasc.ToShortDateString()}");
            Console.WriteLine($"Sexo: {funcionario.sexo}");
            Console.WriteLine($"Tipo de funcionario: {funcionario.tipo_funcionario}");
        }

        //PUT
    }
}
