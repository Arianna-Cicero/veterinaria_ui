using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using veterinaria_ui.Logic;

namespace Data
{
    public class Proprietario
    {
        //private readonly resetVariables variableResetter = new resetVariables();

        public int Proprietario_id { get; set; }
        public String Nome { get; set; }
        public DateTime Data_nasc { get; set; }
        public String Sexo { get; set; }
        public String User { get; set; }
        //public Proprietario()
        //{
        //    variableResetter.ResetVariables(this);
        //}

        //public void ResetPropritarioProperties()
        //{
        //    variableResetter.ResetVariables(this);
        //}
    }
}
