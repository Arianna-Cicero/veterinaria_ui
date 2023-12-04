using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Fatura
    {
        public int fatura_id { get; set; }
        public DateTime data { get; set; }
        public float custo { get; set; }
        public int consulta_id { get; set; }
    }
}
