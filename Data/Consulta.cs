using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Consulta
    {
        public int consulta_id { get; set; }
        public DateTime data_consulta { get; set; }
        public string motivo { get; set; }
        public int animal_id { get; set; }
        public int funcionario_id { get; set; }
    }
}
