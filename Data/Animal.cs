﻿using System;
//using veterinaria_ui.Logic;

namespace Data
{
    public class Animal
    {
        //private readonly resetVariables variableResetter = new resetVariables();

        public int Animal_id { get; set; }
        public String Nome { get; set; }
        public DateTime Data_nasc { get; set; }
        public String Sexo { get; set; }
        public String Cor { get; set; }
        public String Raca { get; set; }
        public String Especie { get; set; }
        public int Proprietario_id { get; set; }

        //public Animal()
        //{        
        //    variableResetter.ResetVariables(this);
        //}
       
        //public void ResetAnimalProperties()
        //{
        //    variableResetter.ResetVariables(this);
        //}
    }
}
