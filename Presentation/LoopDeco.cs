using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria_ui.Presentation
{
    internal class LoopDeco
    {
        public static void ExibirLinhaDecorativa(int largura)
        {
            for (int i = 0; i < largura; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine(); 
        }

        public static void ExibirLinhaCentralizada(string texto, int largura)
        {
            int posicaoInicial = (largura - texto.Length) / 2;
            for (int i = 0; i < posicaoInicial; i++)
            {
                Console.Write("-");
            }
            Console.Write(texto); 

            for (int i = posicaoInicial + texto.Length; i < largura; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();        
        }   
    }
}
