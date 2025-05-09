using System;

namespace GimnasioManager.Models
{
    public class Reserva
    {
        public int ID_Reserva { get; set; }
        public int ID_Miembro { get; set; }
        public string NombreMiembro { get; set; } = string.Empty; // Nombre completo del miembro
        public int ID_Clase { get; set; }
        public string NombreClase { get; set; } = string.Empty; // Nombre de la clase
        public DateTime FechaReserva { get; set; }

        public override string ToString()
        {
            return $"Reserva #{ID_Reserva} - Miembro: {NombreMiembro}, Clase: {NombreClase}, Fecha: {FechaReserva:yyyy-MM-dd}";
        }
    }
}