using System;

namespace GimnasioManager.Models
{
    public class Instructor
    {
        public int ID_Instructor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Nombre} {Apellido} - {Especialidad}";
        }
    }
}