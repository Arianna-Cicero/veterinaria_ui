using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Funcionario
    {
        public int funcionario_id { get; set; }
        public String nome { get; set; }
        public String tipo_funcionario { get; set; }
        public DateTime data_nasc { get; set; }
        public String sexo { get; set; }
        public String user { get; set; }
        public int permissao { get; set; }
    }
}

