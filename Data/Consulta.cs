using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using veterinaria_ui.Logic;

namespace Data
{

    public class Consulta
    {
        //private readonly resetVariables variableResetter = new resetVariables();
        public int Consulta_id { get; set; }
        public DateTime Data_consulta { get; set; }
        public string Motivo { get; set; }
        public int Animal_id { get; set; }
        public int Funcionario_id { get; set; }


        //public Consulta()
        //{
        //    variableResetter.ResetVariables(this);
        //}

        //public void ResetConsultaProperties()
        //{
        //    variableResetter.ResetVariables(this);
        //}
    }
}
