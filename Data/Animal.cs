using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Animal
    {
        public int animal_id { get; set; }
        public String nome { get; set; } 
        public DateTime data_nasc { get; set; }
        public String sexo { get; set; }
        public String cor { get; set; }
        public String raca { get; set; }
        public String especie { get; set; }
        public int proprietario_id { get; set; }
    }
}

