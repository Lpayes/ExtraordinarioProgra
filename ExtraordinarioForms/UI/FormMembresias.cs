using GimnasioManager.Models;
using GimnasioManager.Services;
using GimnasioManager.Utils;
using System;
using System.Windows.Forms;

namespace GimnasioManager.UI
{
    public partial class FormMembresias : Form
    {
        private int contadorEliminarMembresia = 0;
        private int accionActualMembresia = 0;
        private readonly MembresiaService _membresiaService;
        private readonly MiembroService _miembroService;
        public FormMembresias()
        {
            InitializeComponent();
            _membresiaService = new MembresiaService(new DatabaseManager());
            _miembroService = new MiembroService(new DatabaseManager());
        }

        private void FormMembresias_Load(object sender, EventArgs e)
        {
            comboBoxTipodeMembresia.Items.Clear();
            comboBoxTipodeMembresia.Items.Add("Mensual");
            comboBoxTipodeMembresia.Items.Add("Trimestral");
            comboBoxTipodeMembresia.Items.Add("Anual");

            dateTimePickerFInicio.Format = DateTimePickerFormat.Custom;
            dateTimePickerFInicio.CustomFormat = " ";

            dateTimePickerFFin.Format = DateTimePickerFormat.Custom;
            dateTimePickerFFin.CustomFormat = " ";
        }

        private void LlenarComboBoxMembresias()
        {
            comboBoxTipodeMembresia.Items.Clear();
            var membresias = _membresiaService.ObtenerTodos();
            if (membresias != null && membresias.Any())
            {
                foreach (var membresia in membresias)
                {
                    comboBoxTipodeMembresia.Items.Add(membresia.TipoMembresia);
                }
            }
            else
            {
                MessageBox.Show("No se encontraron membresías.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ConfigurarControlesMembresia(string accion)
        {
            comboBoxTipodeMembresia.Enabled = false;
            numericUpDownPrecio.Enabled = false;
            dateTimePickerFInicio.Enabled = false;
            dateTimePickerFFin.Enabled = false;
            textBoxIdMembresia.Enabled = false;

            switch (accion.ToLower())
            {
                case "registrar":
                    comboBoxTipodeMembresia.Enabled = true;
                    numericUpDownPrecio.Enabled = true;
                    dateTimePickerFInicio.Enabled = true;
                    break;

                case "mostrar":
                    break;

                case "buscar":
                    textBoxIdMembresia.Enabled = true;
                    break;

                case "actualizar":
                    textBoxIdMembresia.Enabled = true;
                    comboBoxTipodeMembresia.Enabled = true;
                    numericUpDownPrecio.Enabled = true;
                    dateTimePickerFInicio.Enabled = true;
                    break;

                case "eliminar":
                    textBoxIdMembresia.Enabled = true;
                    break;

                default:
                    MessageBox.Show("Acción no reconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void CompletarControlesMembresia(Membresia membresia)
        {
            if (membresia == null)
            {
                MessageBox.Show("No se encontró una membresía válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBoxIdMembresia.Text = string.IsNullOrWhiteSpace(textBoxIdMembresia.Text) ? membresia.ID_Membresia.ToString() : textBoxIdMembresia.Text;
            comboBoxTipodeMembresia.Text = string.IsNullOrWhiteSpace(comboBoxTipodeMembresia.Text) ? membresia.TipoMembresia : comboBoxTipodeMembresia.Text;
            numericUpDownPrecio.Value = numericUpDownPrecio.Value == numericUpDownPrecio.Minimum ? membresia.Precio : numericUpDownPrecio.Value;
            dateTimePickerFInicio.Value = dateTimePickerFInicio.CustomFormat == " " ? membresia.FechaInicio : dateTimePickerFInicio.Value;
            dateTimePickerFInicio.CustomFormat = dateTimePickerFInicio.CustomFormat == " " ? "dd/MM/yyyy" : dateTimePickerFInicio.CustomFormat;
            dateTimePickerFFin.Value = dateTimePickerFFin.CustomFormat == " " ? membresia.FechaFin : dateTimePickerFFin.Value;
            dateTimePickerFFin.CustomFormat = dateTimePickerFFin.CustomFormat == " " ? "dd/MM/yyyy" : dateTimePickerFFin.CustomFormat;
        }

        private void LimpiarControlesMembresia()
        {
            textBoxIdMembresia.Clear();
            textBoxIdMembresia.Enabled = false;

            comboBoxTipodeMembresia.SelectedIndex = -1;
            comboBoxTipodeMembresia.Text = string.Empty;

            numericUpDownPrecio.Value = numericUpDownPrecio.Minimum;

            dateTimePickerFInicio.Format = DateTimePickerFormat.Custom;
            dateTimePickerFInicio.CustomFormat = " ";

            dateTimePickerFFin.Format = DateTimePickerFormat.Custom;
            dateTimePickerFFin.CustomFormat = " ";
            dateTimePickerFFin.Enabled = false;

            dataGridViewMembresía.DataSource = null;
            dataGridViewMembresía.Rows.Clear();
        }

        private void RegistrarMembresia()
        {
            if (string.IsNullOrWhiteSpace(comboBoxTipodeMembresia.Text))
            {
                MessageBox.Show("Por favor, seleccione un tipo de membresía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePickerFInicio.CustomFormat == " ")
            {
                MessageBox.Show("Por favor, seleccione una fecha de inicio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePickerFInicio.Value < new DateTime(2025, 1, 1))
            {
                MessageBox.Show("La fecha de inicio debe ser a partir del año 2025.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numericUpDownPrecio.Value == 0)
            {
                var confirmacion = MessageBox.Show("El precio está configurado en 0. ¿Desea continuar?", "Confirmar precio en 0", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacion == DialogResult.No)
                {
                    return;
                }
            }

            DateTime fechaInicio = dateTimePickerFInicio.Value;
            DateTime fechaFin;

            switch (comboBoxTipodeMembresia.Text)
            {
                case "Mensual":
                    fechaFin = fechaInicio.AddMonths(1);
                    break;

                case "Trimestral":
                    fechaFin = fechaInicio.AddMonths(3);
                    break;

                case "Anual":
                    fechaFin = fechaInicio.AddYears(1);
                    break;

                default:
                    MessageBox.Show("Tipo de membresía no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            if (fechaFin.Day != fechaInicio.Day)
            {
                fechaFin = new DateTime(fechaFin.Year, fechaFin.Month, DateTime.DaysInMonth(fechaFin.Year, fechaFin.Month));
            }

            dateTimePickerFFin.Value = fechaFin;
            dateTimePickerFFin.Format = DateTimePickerFormat.Short;
            dateTimePickerFFin.CustomFormat = "dd/MM/yyyy";

            var mensajeConfirmacion = $"Tu membresía de tipo {comboBoxTipodeMembresia.Text} será válida desde {fechaInicio:dd/MM/yyyy} hasta {fechaFin:dd/MM/yyyy}.\n¿Deseas confirmar el registro?";
            var resultado = MessageBox.Show(mensajeConfirmacion, "Confirmar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            var membresia = new Membresia
            {
                TipoMembresia = comboBoxTipodeMembresia.Text,
                Precio = numericUpDownPrecio.Value,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            try
            {
                _membresiaService.Crear(membresia);
                MessageBox.Show("¡Membresía registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarControlesMembresia();

                VerMembresias();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la membresía: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerMembresias()
        {
            try
            {
                var membresias = _membresiaService.ObtenerTodos();
                if (membresias != null && membresias.Any())
                {
                    dataGridViewMembresía.DataSource = membresias;
                }
                else
                {
                    MessageBox.Show("No se encontraron membresías.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar las membresías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarMembresia()
        {
            if (!int.TryParse(textBoxIdMembresia.Text, out int id))
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var membresia = _membresiaService.ObtenerPorId(id);
                if (membresia == null)
                {
                    MessageBox.Show("No se encontró una membresía con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(comboBoxTipodeMembresia.Text) ||
                    dateTimePickerFInicio.CustomFormat == " " ||
                    numericUpDownPrecio.Value == 0)
                {
                    MessageBox.Show("Debes llenar todos los campos obligatorios para renovar la membresía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dateTimePickerFInicio.Value < DateTime.Today)
                {
                    MessageBox.Show("La fecha de inicio no puede ser una fecha pasada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fechaInicio = dateTimePickerFInicio.Value;
                DateTime nuevaFechaFin;
                switch (comboBoxTipodeMembresia.Text)
                {
                    case "Mensual":
                        nuevaFechaFin = fechaInicio.AddMonths(1);
                        break;
                    case "Trimestral":
                        nuevaFechaFin = fechaInicio.AddMonths(3);
                        break;
                    case "Anual":
                        nuevaFechaFin = fechaInicio.AddYears(1);
                        break;
                    default:
                        MessageBox.Show("Tipo de membresía no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                if (nuevaFechaFin.Day != fechaInicio.Day)
                {
                    nuevaFechaFin = new DateTime(nuevaFechaFin.Year, nuevaFechaFin.Month, DateTime.DaysInMonth(nuevaFechaFin.Year, nuevaFechaFin.Month));
                }

                var miembro = _miembroService.ObtenerTodos().FirstOrDefault(m => m.ID_Membresia == membresia.ID_Membresia);
                if (miembro != null)
                {
                    var reservas = new ReservaService(new DatabaseManager()).ObtenerTodos()
                        .Where(r => r.ID_Miembro == miembro.ID_Miembro
                            && r.FechaReserva >= membresia.FechaInicio
                            && r.FechaReserva <= membresia.FechaFin)
                        .ToList();

                    if (reservas.Any())
                    {
                        MessageBox.Show("No se puede actualizar la membresía porque existen reservas asociadas a este miembro en el rango de fechas de la membresía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (membresia.FechaFin >= DateTime.Today && nuevaFechaFin <= membresia.FechaFin)
                {
                    MessageBox.Show("Solo puedes actualizar la membresía si la fecha de fin ya ha pasado o si la nueva fecha de fin es mayor a la actual.", "Política de la empresa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numericUpDownPrecio.Value == 0)
                {
                    var confirmacion = MessageBox.Show("El precio está configurado en 0. ¿Desea continuar?", "Confirmar precio en 0", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmacion == DialogResult.No)
                    {
                        return;
                    }
                }

                membresia.TipoMembresia = comboBoxTipodeMembresia.Text;
                membresia.FechaInicio = fechaInicio;
                membresia.FechaFin = nuevaFechaFin;
                membresia.Precio = numericUpDownPrecio.Value;

                _membresiaService.Actualizar(membresia);
                MessageBox.Show("¡Membresía actualizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarControlesMembresia();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la membresía: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarMembresiaPorId()
        {
            if (int.TryParse(textBoxIdMembresia.Text, out int id))
            {
                try
                {
                    var membresia = _membresiaService.ObtenerPorId(id);
                    if (membresia != null)
                    {
                        dataGridViewMembresía.DataSource = new[] { membresia };
                        CompletarControlesMembresia(membresia);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró una membresía con ese ID.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar la membresía: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EliminarMembresia()
        {
            if (contadorEliminarMembresia == 0)
            {
                if (int.TryParse(textBoxIdMembresia.Text, out int id))
                {
                    var membresia = _membresiaService.ObtenerPorId(id);
                    if (membresia != null)
                    {
                        CompletarControlesMembresia(membresia);

                        MessageBox.Show($"Se ha encontrado la membresía: {membresia.TipoMembresia}. Haz clic de nuevo en 'Guardar Membresía' para eliminarla.",
                                        "Confirmar eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        contadorEliminarMembresia++;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró una membresía con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (int.TryParse(textBoxIdMembresia.Text, out int id))
                {
                    var miembrosConMembresia = _miembroService.ObtenerTodos().Where(m => m.ID_Membresia == id).ToList();
                    if (miembrosConMembresia.Any())
                    {
                        MessageBox.Show("No se puede eliminar la membresía porque hay miembros que la tienen asignada. Elimine o actualice primero esos miembros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        contadorEliminarMembresia = 0;
                        return;
                    }

                    _membresiaService.Eliminar(id);
                    MessageBox.Show("¡Membresía eliminada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    contadorEliminarMembresia = 0;
                    LlenarComboBoxMembresias();
                }
            }
        }

        private void dateTimePickerFInicio_ValueChanged_1(object sender, EventArgs e)
        {
            dateTimePickerFInicio.Format = DateTimePickerFormat.Short;
            dateTimePickerFInicio.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePickerFFin_ValueChanged_1(object sender, EventArgs e)
        {
            dateTimePickerFFin.Format = DateTimePickerFormat.Short;
            dateTimePickerFFin.CustomFormat = "dd/MM/yyyy";
        }

        private void buttonRegistrarMembresia_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 1;
            ConfigurarControlesMembresia("registrar");
            buttonGuardarMembresia.Text = "Guardar";

            MessageBox.Show("La fecha de fin se completará automáticamente según el tipo de membresía seleccionado y la fecha de inicio elegida.",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonMostrarMembresias_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 2;
            ConfigurarControlesMembresia("mostrar");
            buttonGuardarMembresia.Text = "Mostrar";
        }

        private void buttonActualizarMembresia_Click(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 4;
            ConfigurarControlesMembresia("actualizar");
            buttonGuardarMembresia.Text = "Actualizar";
        }

        private void buttonEliminarMembresia_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 5;
            ConfigurarControlesMembresia("eliminar");
            buttonGuardarMembresia.Text = "Eliminar";
        }

        private void buttonGuardarMembresia_Click_1(object sender, EventArgs e)
        {
            switch (accionActualMembresia)
            {
                case 1:
                    RegistrarMembresia();
                    break;
                case 2:
                    VerMembresias();
                    break;
                case 3:
                    BuscarMembresiaPorId();
                    break;
                case 4:
                    ActualizarMembresia();
                    break;
                case 5:
                    EliminarMembresia();
                    break;
                default:
                    MessageBox.Show("Seleccione una acción válida antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void buttonBuscarMembresiaPorId_Click(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 3;
            ConfigurarControlesMembresia("buscar");
            buttonGuardarMembresia.Text = "Buscar";
        }
    }
}
