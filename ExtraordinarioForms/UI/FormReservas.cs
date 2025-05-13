using GimnasioManager.Models;        
using GimnasioManager.Services;       
using GimnasioManager.Utils;          
using System;                        
using System.Linq;                   
using System.Windows.Forms;   

namespace GimnasioManager.UI
{
    public partial class FormReservas : Form
    {
        private int contadorEliminarReserva = 0;
        private int accionActualReserva = 0;
        private readonly ReservaService _reservaService;
        private readonly ClaseService _claseService;
        readonly MembresiaService _membresiaService;
        public FormReservas()
        {
            InitializeComponent();
            _reservaService = new ReservaService(new DatabaseManager());
            _claseService = new ClaseService(new DatabaseManager());
            _membresiaService = new MembresiaService(new DatabaseManager());
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

                case "buscarporidmiembro":
                    textBoxIdMiembroReserva.Enabled = true;
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
            if (accionActualReserva == 3)
            {
                textBoxIdMiembroReserva.Clear();
            }
            else
            {
                textBoxIdReserva.Clear();
                textBoxIdMiembroReserva.Clear();
                textBoxIdClaseReserva.Clear();
                dateTimePickerFReserva.Format = DateTimePickerFormat.Custom;
                dateTimePickerFReserva.CustomFormat = " ";
                dataGridView5.DataSource = null;
                dataGridView5.Rows.Clear();
            }
        }
        private bool ValidarMembresiaActiva(int idMiembro, DateTime fechaReserva)
        {
            var membresia = _membresiaService.ObtenerMembresiaPorMiembro(idMiembro);

            if (membresia == null)
            {
                MessageBox.Show("El miembro seleccionado no tiene una membresía activa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime fechaInicio = membresia.FechaInicio.Date;
            DateTime fechaFin = membresia.FechaFin.Date;
            DateTime fechaReservaNormalizada = fechaReserva.Date;

            if (fechaReservaNormalizada >= fechaInicio && fechaReservaNormalizada <= fechaFin)
            {
                return true;
            }

            MessageBox.Show("La reserva no es válida. La fecha de reserva está fuera del rango de vigencia de la membresía del miembro.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
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

            if (!ValidarMembresiaActiva(idMiembro, fechaReserva))
            {
                return;
            }

            try
            {
                var clase = _claseService.ObtenerPorId(idClase);
                if (clase == null)
                {
                    MessageBox.Show("No se encontró una clase con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var reservaDuplicada = _reservaService.ObtenerTodos()
                    .Any(r => r.ID_Miembro == idMiembro && r.ID_Clase == idClase && r.FechaReserva.Date == fechaReserva.Date);

                if (reservaDuplicada)
                {
                    MessageBox.Show("No se puede realizar la reserva. El miembro ya tiene una reserva para esta clase en la misma fecha.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var reservasClase = _reservaService.ObtenerTodos()
                    .Where(r => r.ID_Clase == idClase && r.FechaReserva.Date == fechaReserva.Date)
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

        private void BuscarReservasPorIdMiembro()
        {
            if (!int.TryParse(textBoxIdMiembroReserva.Text, out int idMiembro))
            {
                MessageBox.Show("Por favor, ingrese un ID de miembro válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var reservas = _reservaService.ObtenerTodos()
                    .Where(r => r.ID_Miembro == idMiembro)
                    .ToList();

                if (reservas.Any())
                {
                    dataGridView5.DataSource = reservas;
                }
                else
                {
                    MessageBox.Show("No se encontraron reservas para este miembro.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView5.DataSource = null;
                    dataGridView5.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar las reservas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (int.TryParse(textBoxIdMiembroReserva.Text, out int idMiembro))
                {
                    if (!ValidarMembresiaActiva(idMiembro, reserva.FechaReserva))
                    {
                        return;
                    }
                    reserva.ID_Miembro = idMiembro;
                }

                if (int.TryParse(textBoxIdClaseReserva.Text, out int idClase))
                {
                    var clase = _claseService.ObtenerPorId(idClase);
                    if (clase == null)
                    {
                        MessageBox.Show("No se encontró una clase con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var reservasClase = _reservaService.ObtenerTodos()
                        .Where(r => r.ID_Clase == idClase && r.FechaReserva.Date == reserva.FechaReserva.Date && r.ID_Reserva != idReserva)
                        .ToList();

                    if (reservasClase.Count >= clase.CapacidadMaxima)
                    {
                        MessageBox.Show("No se puede actualizar la reserva. La clase ya alcanzó su capacidad máxima para esta fecha.",
                                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    reserva.ID_Clase = idClase;
                }

                if (dateTimePickerFReserva.CustomFormat != " ")
                {
                    DateTime nuevaFechaReserva = dateTimePickerFReserva.Value;

                    if (!ValidarMembresiaActiva(reserva.ID_Miembro, nuevaFechaReserva))
                    {
                        return;
                    }

                    reserva.FechaReserva = nuevaFechaReserva;
                }

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

        private void buttonBuscarReservasPorIdMiembro_Click(object sender, EventArgs e)
        {
            LimpiarControlesReserva();
            accionActualReserva = 3;
            ConfigurarControlesReserva("buscarporidmiembro");
            GuardarReserva.Text = "Buscar Reservas";
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
                    BuscarReservasPorIdMiembro();
                    break;
                case 4:
                    ActualizarReserva();
                    break;
                case 5:
                    EliminarReserva();
                    break;
                default:
                    MessageBox.Show("Seleccione una acción válida antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

    }
}
