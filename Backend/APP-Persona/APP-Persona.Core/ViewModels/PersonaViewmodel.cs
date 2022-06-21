using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP_Persona.Core.ViewModels
{
    public class PersonaViewmodel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }


        [DataType(DataType.Date)]
        public string FechaDeNacimiento { get; set; }


    }
}
