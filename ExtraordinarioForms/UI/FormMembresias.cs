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
    public partial class FormMembresias : Form
    {
        private int contadorEliminarMembresia = 0;
        private int accionActualMembresia = 0;
        private readonly MembresiaService _membresiaService;
        public FormMembresias()
        {
            InitializeComponent();
            _membresiaService = new MembresiaService(new DatabaseManager());
        }

        private void FormMembresias_Load(object sender, EventArgs e)
        {
            LlenarComboBoxMembresias();
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
                    dateTimePickerFFin.Enabled = true;
                    break;

                case "mostrar":
                    break;

                case "buscar":
                    comboBoxTipodeMembresia.Enabled = true;
                    break;

                case "actualizar":
                    textBoxIdMembresia.Enabled = true;
                    comboBoxTipodeMembresia.Enabled = true;
                    numericUpDownPrecio.Enabled = true;
                    dateTimePickerFInicio.Enabled = true;
                    dateTimePickerFFin.Enabled = true;
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

            textBoxIdMembresia.Text = membresia.ID_Membresia.ToString();
            comboBoxTipodeMembresia.Text = membresia.TipoMembresia;
            numericUpDownPrecio.Value = membresia.Precio;

            dateTimePickerFInicio.Value = membresia.FechaInicio;
            dateTimePickerFFin.Value = membresia.FechaFin;
        }

        private void LimpiarControlesMembresia()
        {
            textBoxIdMembresia.Clear();
            comboBoxTipodeMembresia.SelectedIndex = -1;
            comboBoxTipodeMembresia.Text = string.Empty;
            numericUpDownPrecio.Value = 0;

            dateTimePickerFInicio.Format = DateTimePickerFormat.Custom;
            dateTimePickerFInicio.CustomFormat = " ";

            dateTimePickerFFin.Format = DateTimePickerFormat.Custom;
            dateTimePickerFFin.CustomFormat = " ";
        }

        private void RegistrarMembresia()
        {
            if (string.IsNullOrWhiteSpace(comboBoxTipodeMembresia.Text))
            {
                MessageBox.Show("El campo 'Tipo de Membresía' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numericUpDownPrecio.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a 0.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePickerFInicio.CustomFormat == " " || dateTimePickerFFin.CustomFormat == " ")
            {
                MessageBox.Show("Por favor, seleccione las fechas de inicio y fin.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePickerFInicio.Value >= dateTimePickerFFin.Value)
            {
                MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var membresia = new Membresia
            {
                TipoMembresia = comboBoxTipodeMembresia.Text,
                Precio = numericUpDownPrecio.Value,
                FechaInicio = dateTimePickerFInicio.Value,
                FechaFin = dateTimePickerFFin.Value
            };

            try
            {
                _membresiaService.Crear(membresia);
                MessageBox.Show("¡Membresía registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LlenarComboBoxMembresias();
                LimpiarControlesMembresia();
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

                if (dateTimePickerFInicio.CustomFormat != " " && dateTimePickerFFin.CustomFormat != " ")
                {
                    if (dateTimePickerFInicio.Value >= dateTimePickerFFin.Value)
                    {
                        MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    membresia.FechaInicio = dateTimePickerFInicio.Value;
                    membresia.FechaFin = dateTimePickerFFin.Value;
                }

                membresia.TipoMembresia = !string.IsNullOrWhiteSpace(comboBoxTipodeMembresia.Text) ? comboBoxTipodeMembresia.Text : membresia.TipoMembresia;
                membresia.Precio = numericUpDownPrecio.Value > 0 ? numericUpDownPrecio.Value : membresia.Precio;

                _membresiaService.Actualizar(membresia);
                MessageBox.Show("¡Membresía actualizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LlenarComboBoxMembresias();
                LimpiarControlesMembresia();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la membresía: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    _membresiaService.Eliminar(id);
                    MessageBox.Show("¡Membresía eliminada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    contadorEliminarMembresia = 0;
                    LlenarComboBoxMembresias();
                }
            }
        }

        private void dateTimePickerFInicio_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFInicio.Format = DateTimePickerFormat.Short;
            dateTimePickerFInicio.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePickerFFin_ValueChanged(object sender, EventArgs e)
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
        }

        private void buttonMostrarMembresias_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 2;
            ConfigurarControlesMembresia("mostrar");
        }

        private void buttonActualizarMembresia_Click(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 3;
            ConfigurarControlesMembresia("actualizar");
            buttonGuardarMembresia.Text = "Actualizar";
        }

        private void buttonEliminarMembresia_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMembresia();
            accionActualMembresia = 4;
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
                    ActualizarMembresia();
                    break;
                case 4:
                    EliminarMembresia();
                    break;
                default:
                    MessageBox.Show("Seleccione una acción válida antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
