using System;

namespace GimnasioManager.Models
{
    public class Membresia
    {
        public int ID_Membresia { get; set; } // Identificador único de la membresía
        public string TipoMembresia { get; set; } = string.Empty; // Tipo de membresía (ej. Mensual, Anual)
        public decimal Precio { get; set; } // Precio de la membresía
        public DateTime FechaInicio { get; set; } // Fecha de inicio de la membresía
        public DateTime FechaFin { get; set; } // Fecha de fin de la membresía

        public override string ToString()
        {
            return $"{TipoMembresia} - ${Precio} (Desde: {FechaInicio:yyyy-MM-dd} Hasta: {FechaFin:yyyy-MM-dd})";
        }
    }
}