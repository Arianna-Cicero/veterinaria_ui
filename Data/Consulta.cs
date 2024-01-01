using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Consulta
    {
        public int Consulta_id { get; set; }
        public DateTime Data_consulta { get; set; }
        public string Motivo { get; set; }
        public int Animal_id { get; set; }
        public int Funcionario_id { get; set; }
    }
}
