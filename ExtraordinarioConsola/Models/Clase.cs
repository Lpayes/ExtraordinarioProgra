using System;

namespace GimnasioManager.Models
{
    public class Clase
    {
        public int ID_Clase { get; set; } // Identificador único de la clase
        public string NombreClase { get; set; } = string.Empty; // Nombre de la clase
        public TimeSpan Horario { get; set; } // Horario de la clase
        public int CapacidadMaxima { get; set; } // Capacidad máxima de la clase
        public int ID_Instructor { get; set; } // Clave foránea obligatoria que apunta a la tabla Instructores

        // Propiedad adicional para mostrar el nombre completo del instructor
        public string NombreInstructor { get; set; } = string.Empty; // Nombre completo del instructor

        public override string ToString()
        {
            return $"{NombreClase} - {Horario} (Capacidad: {CapacidadMaxima}) - Instructor: {NombreInstructor}";
        }
    }
}