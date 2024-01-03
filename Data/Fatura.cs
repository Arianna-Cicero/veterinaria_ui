using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Fatura
    {
        public int Fatura_id { get; set; }
        public DateTime Data { get; set; }
        public float Custo { get; set; }
        public int Consulta_id { get; set; }
        public string Username { get; set; }    
    }
}
