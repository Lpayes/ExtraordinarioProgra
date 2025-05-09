using System;

namespace GimnasioManager.Models
{
    public class Miembro
    {
        public int ID_Miembro { get; set; } // Identificador único del miembro
        public string Nombre { get; set; } = string.Empty; // Nombre del miembro
        public string Apellido { get; set; } = string.Empty; // Apellido del miembro
        public DateTime FechaNacimiento { get; set; } // Fecha de nacimiento del miembro
        public string Email { get; set; } = string.Empty; // Correo electrónico del miembro
        public string Telefono { get; set; } = string.Empty; // Teléfono del miembro
        public int ID_Membresia { get; set; } // Clave foránea obligatoria que apunta a la tabla Membresias

        public override string ToString()
        {
            return $"ID: {ID_Miembro} - {Nombre} {Apellido} - {Email} - Teléfono: {Telefono} - ID Membresía: {ID_Membresia}";
        }
    }
}