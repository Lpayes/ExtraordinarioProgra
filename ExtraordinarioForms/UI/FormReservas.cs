using GimnasioManager.Models;
using GimnasioManager.Services;
using GimnasioManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GimnasioManager.UI
{
    public partial class FormReservas : Form
    {
        private int contadorEliminarReserva = 0;
        private int accionActualReserva = 0;
        private readonly ReservaService _reservaService;
        private readonly ClaseService _claseService;
        public FormReservas()
        {
            InitializeComponent();
            _reservaService = new ReservaService(new DatabaseManager());
            _claseService = new ClaseService(new DatabaseManager());
        }

        private void FormReservas_Load(object sender, EventArgs e)
        {
            dateTimePickerFReserva.Format = DateTimePickerFormat.Custom;
            dateTimePickerFReserva.CustomFormat = " ";
        }

        private void ConfigurarControlesReserva(string accion)
        {

            textBoxIdMiembroReserva.Enabled = false;
            textBoxIdClaseReserva.Enabled = false;
            dateTimePickerFReserva.Enabled = false;
            textBoxIdReserva.Enabled = false;

            switch (accion.ToLower())
            {
                case "registrar":
                    textBoxIdMiembroReserva.Enabled = true;
                    textBoxIdClaseReserva.Enabled = true;
                    dateTimePickerFReserva.Enabled = true;
                    break;

                case "mostrar":
                    break;

                case "buscar":
                    textBoxIdReserva.Enabled = true;
                    break;

                case "actualizar":
                    textBoxIdReserva.Enabled = true;
                    textBoxIdMiembroReserva.Enabled = true;
                    textBoxIdClaseReserva.Enabled = true;
                    dateTimePickerFReserva.Enabled = true;
                    break;

                case "eliminar":
                    textBoxIdReserva.Enabled = true;
                    break;

                default:
                    MessageBox.Show("Acción no reconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void LimpiarControlesReserva()
        {
            textBoxIdReserva.Clear();
            textBoxIdMiembroReserva.Clear();
            textBoxIdClaseReserva.Clear();

            dateTimePickerFReserva.Format = DateTimePickerFormat.Custom;
            dateTimePickerFReserva.CustomFormat = " ";
        }

        private void RegistrarReserva()
        {
            if (!int.TryParse(textBoxIdMiembroReserva.Text, out int idMiembro))
            {
                MessageBox.Show("Por favor, ingrese un ID de miembro válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxIdClaseReserva.Text, out int idClase))
            {
                MessageBox.Show("Por favor, ingrese un ID de clase válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePickerFReserva.CustomFormat == " ")
            {
                MessageBox.Show("Por favor, seleccione una fecha de reserva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaReserva = dateTimePickerFReserva.Value;

            try
            {

                var clase = _claseService.ObtenerPorId(idClase);
                if (clase == null)
                {
                    MessageBox.Show("No se encontró una clase con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var reservasExistentes = _reservaService.ObtenerTodos()
                    .Where(r => r.ID_Miembro == idMiembro && r.FechaReserva == fechaReserva && r.ID_Clase != idClase)
                    .ToList();

                if (reservasExistentes.Any(r => _claseService.ObtenerPorId(r.ID_Clase).Horario == clase.Horario))
                {
                    MessageBox.Show("No se puede realizar la reserva. El miembro ya tiene una clase reservada en el mismo horario y fecha.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var reservasClase = _reservaService.ObtenerTodos()
                    .Where(r => r.ID_Clase == idClase && r.FechaReserva == fechaReserva)
                    .ToList();

                if (reservasClase.Count >= clase.CapacidadMaxima)
                {
                    MessageBox.Show("No se puede realizar la reserva. La clase ya alcanzó su capacidad máxima para esta fecha.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var reserva = new Reserva
                {
                    ID_Miembro = idMiembro,
                    ID_Clase = idClase,
                    FechaReserva = fechaReserva
                };

                _reservaService.Crear(reserva);
                MessageBox.Show("¡Reserva registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                VerReservas();

                LimpiarControlesReserva();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerReservas()
        {
            try
            {
                var reservas = _reservaService.ObtenerTodos();
                if (reservas != null && reservas.Any())
                {
                    dataGridView5.DataSource = reservas;
                }
                else
                {
                    MessageBox.Show("No se encontraron reservas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar las reservas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarReserva()
        {
            if (!int.TryParse(textBoxIdReserva.Text, out int idReserva))
            {
                MessageBox.Show("Por favor, ingrese un ID de reserva válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var reserva = _reservaService.ObtenerPorId(idReserva);
                if (reserva == null)
                {
                    MessageBox.Show("No se encontró una reserva con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBoxIdMiembroReserva.Text, out int idMiembro))
                {
                    MessageBox.Show("Por favor, ingrese un ID de miembro válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBoxIdClaseReserva.Text, out int idClase))
                {
                    MessageBox.Show("Por favor, ingrese un ID de clase válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dateTimePickerFReserva.CustomFormat == " ")
                {
                    MessageBox.Show("Por favor, seleccione una fecha de reserva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fechaReserva = dateTimePickerFReserva.Value;

                var clase = _claseService.ObtenerPorId(idClase);
                if (clase == null)
                {
                    MessageBox.Show("No se encontró una clase con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var reservasExistentes = _reservaService.ObtenerTodos()
                    .Where(r => r.ID_Miembro == idMiembro && r.FechaReserva == fechaReserva && r.ID_Reserva != idReserva)
                    .ToList();

                if (reservasExistentes.Any(r => _claseService.ObtenerPorId(r.ID_Clase).Horario == clase.Horario))
                {
                    MessageBox.Show("No se puede actualizar la reserva. El miembro ya tiene una clase reservada en el mismo horario y fecha.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var reservasClase = _reservaService.ObtenerTodos()
                    .Where(r => r.ID_Clase == idClase && r.FechaReserva == fechaReserva && r.ID_Reserva != idReserva)
                    .ToList();

                if (reservasClase.Count >= clase.CapacidadMaxima)
                {
                    MessageBox.Show("No se puede actualizar la reserva. La clase ya alcanzó su capacidad máxima para esta fecha.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                reserva.ID_Miembro = idMiembro;
                reserva.ID_Clase = idClase;
                reserva.FechaReserva = fechaReserva;

                _reservaService.Actualizar(reserva);
                MessageBox.Show("¡Reserva actualizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                VerReservas();

                LimpiarControlesReserva();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarReserva()
        {
            if (!int.TryParse(textBoxIdReserva.Text, out int idReserva))
            {
                MessageBox.Show("Por favor, ingrese un ID de reserva válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var reserva = _reservaService.ObtenerPorId(idReserva);
                if (reserva == null)
                {
                    MessageBox.Show("No se encontró una reserva con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar la reserva con ID {idReserva}?",
                                                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    _reservaService.Eliminar(idReserva);
                    MessageBox.Show("¡Reserva eliminada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    VerReservas();

                    LimpiarControlesReserva();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePickerFReserva_ValueChanged_1(object sender, EventArgs e)
        {
            dateTimePickerFReserva.Format = DateTimePickerFormat.Short;
            dateTimePickerFReserva.CustomFormat = "dd/MM/yyyy";
        }


        private void buttonRegistrarReserva_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesReserva();
            accionActualReserva = 1;
            ConfigurarControlesReserva("registrar");
            GuardarReserva.Text = "Guardar";
        }

        private void buttonMostrarReservas_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesReserva();
            accionActualReserva = 2;
            ConfigurarControlesReserva("mostrar");
            GuardarReserva.Text = "Mostrar";
        }

        private void buttonActualizarReserva_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesReserva();
            accionActualReserva = 3;
            ConfigurarControlesReserva("actualizar");
            GuardarReserva.Text = "Actualizar";
        }

        private void buttonEliminarReserva_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesReserva();
            accionActualReserva = 4;
            ConfigurarControlesReserva("eliminar");
            GuardarReserva.Text = "Eliminar";
        }

        private void GuardarReserva_Click_1(object sender, EventArgs e)
        {
            switch (accionActualReserva)
            {
                case 1:
                    RegistrarReserva();
                    break;

                case 2:
                    VerReservas();
                    break;
                case 3:
                    ActualizarReserva();
                    break;
                case 4:
                    EliminarReserva();
                    break;
                default:
                    MessageBox.Show("Seleccione una acción válida antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
