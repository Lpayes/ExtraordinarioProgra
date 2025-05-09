using System;
using System.Collections.Generic;

namespace GimnasioManager.Models
{
    public class Instructor
    {
        public int ID_Instructor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;

        // Relación con Clases (uno a muchos)
        public List<Clase> Clases { get; set; } = new List<Clase>();

        public override string ToString()
        {
            return $"{Nombre} {Apellido} - {Especialidad}";
        }
    }
}