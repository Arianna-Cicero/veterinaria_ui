using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    internal class FaturaManager
    {
        public void GetFaturaInfoFromUser(Fatura fatura)
        {
           Console.WriteLine("Informações sobre Fatura SIIUuuuuuuuuUUU");
            fatura.fatura_id = IDgenerator.GenerateUniqueRandomID();
            Console.WriteLine($"ID da fatura: {fatura.fatura_id}");
            Console.WriteLine("Data da fatura (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                fatura.data = data;
            }
            else
            {
                Console.WriteLine("Data inválida. Utilice o valor padrão (yyyy-MM-dd).");
                fatura.data = DateTime.MinValue;
            }
            Console.WriteLine("Insira o custo da fatura:");
            fatura.custo = float.Parse(Console.ReadLine());
        }


        public void GetFatura(Fatura fatura)
        {
            Console.WriteLine($"ID da fatura: {fatura.fatura_id}");
            Console.WriteLine($"Data da Fatura: {fatura.data.ToShortDateString()}");
            Console.WriteLine($"Custo: {fatura.custo}");
            Console.WriteLine($"ID da consulta: {fatura.consulta_id}");
        }
    }
}