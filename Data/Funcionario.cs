using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using veterinaria_ui.Logic;

namespace Data
{
    public class Funcionario
    {
        //private readonly resetVariables variableResetter = new resetVariables();

        public int Funcionario_id { get; set; }
        public String Nome { get; set; }
        public String Tipo_funcionario { get; set; }
        public DateTime Data_nasc { get; set; }
        public String Sexo { get; set; }
        public String User { get; set; }

        //public Funcionario()
        //{
        //    variableResetter.ResetVariables(this);
        //}

        //public void ResetFuncionarioProperties()
        //{
        //    variableResetter.ResetVariables(this);
        //}
    }
}

