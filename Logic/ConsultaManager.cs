using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{

    internal class ConsultaManager
    {
        public void GetConsultaFromUser(Consulta consulta)
        {
            Console.WriteLine("Informações sobre a consulnnta  aaaa");
            consulta.consulta_id = IDgenerator.GenerateUniqueRandomID();
            Console.WriteLine($"ID da consulta: {consulta.consulta_id}");
            Console.WriteLine("Data da consulta (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                consulta.data_consulta = data;
            }
            else
            {
                Console.WriteLine("Data inválida. Utilice o valor padrão (yyyy-MM-dd).");
                consulta.data_consulta = DateTime.MinValue;
            }
            Console.WriteLine("Insira o motivo da consulta:");
            consulta.motivo = Console.ReadLine();
        }
        public void GetConsulta(Consulta consulta)
        {
            Console.WriteLine($"ID da Consulta: {consulta.consulta_id}");
            Console.WriteLine($"Data da consulta: {consulta.data_consulta.ToShortDateString()}");
            Console.WriteLine($"Motivo: {consulta.motivo}");
            Console.WriteLine($"ID da animal: {consulta.animal_id}");
            Console.WriteLine($"ID do Funcionario:{consulta.funcionario_id} ");
        }
    }
}


