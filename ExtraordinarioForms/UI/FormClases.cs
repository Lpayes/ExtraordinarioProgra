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
    public partial class FormClases : Form
    {
        private int contadorEliminarClase = 0;
        private int accionActualClase = 0;
        private readonly ClaseService _claseService;
        public FormClases()
        {
            InitializeComponent();
            _claseService = new ClaseService(new DatabaseManager());
        }

        private void FormClases_Load(object sender, EventArgs e)
        {
            LlenarComboBoxClases();
        }

        private void LlenarComboBoxClases()
        {
            comboBoxNombreClase.Items.Clear();
            var clases = _claseService.ObtenerTodos();
            if (clases != null && clases.Any())
            {
                foreach (var clase in clases)
                {
                    comboBoxNombreClase.Items.Add(clase.NombreClase);
                }
            }
            else
            {
                MessageBox.Show("No se encontraron clases.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ConfigurarControlesClase(string accion)
        {
            comboBoxNombreClase.Enabled = false;
            numericUpDownCapacidadMaxima.Enabled = false;
            maskedTextBoxHorario.Enabled = false;
            textBoxIdClase.Enabled = false;
            textBoxIdInstructorClase.Enabled = false;

            switch (accion.ToLower())
            {
                case "registrar":
                    comboBoxNombreClase.Enabled = true;
                    numericUpDownCapacidadMaxima.Enabled = true;
                    maskedTextBoxHorario.Enabled = true;
                    textBoxIdInstructorClase.Enabled = true;
                    break;

                case "mostrar":
                    break;

                case "buscar":
                    comboBoxNombreClase.Enabled = true;
                    break;

                case "actualizar":
                    textBoxIdClase.Enabled = true;
                    comboBoxNombreClase.Enabled = true;
                    numericUpDownCapacidadMaxima.Enabled = true;
                    maskedTextBoxHorario.Enabled = true;
                    textBoxIdInstructorClase.Enabled = true;
                    break;

                case "eliminar":
                    textBoxIdClase.Enabled = true;
                    break;

                default:
                    MessageBox.Show("Acción no reconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void CompletarControlesClase(Clase clase)
        {
            if (clase == null)
            {
                MessageBox.Show("No se encontró una clase válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBoxIdClase.Text = clase.ID_Clase.ToString();
            comboBoxNombreClase.Text = clase.NombreClase;
            numericUpDownCapacidadMaxima.Value = clase.CapacidadMaxima;
            maskedTextBoxHorario.Text = clase.Horario.ToString(@"hh\:mm\:ss");
            textBoxIdInstructorClase.Text = clase.ID_Instructor.ToString();
        }

        private void LimpiarControlesClase()
        {
            textBoxIdClase.Clear();
            comboBoxNombreClase.SelectedIndex = -1;
            comboBoxNombreClase.Text = string.Empty;
            numericUpDownCapacidadMaxima.Value = 0;
            maskedTextBoxHorario.Clear();
            textBoxIdInstructorClase.Clear();
        }

        private void RegistrarClase()
        {
            if (string.IsNullOrWhiteSpace(comboBoxNombreClase.Text))
            {
                MessageBox.Show("El campo 'Nombre de la Clase' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxIdInstructorClase.Text, out int idInstructor))
            {
                MessageBox.Show("Por favor, ingrese un ID de instructor válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParse(maskedTextBoxHorario.Text, out TimeSpan horario))
            {
                MessageBox.Show("Por favor, ingrese un horario válido en formato HH:mm:ss.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numericUpDownCapacidadMaxima.Value <= 0)
            {
                MessageBox.Show("La capacidad máxima debe ser mayor a 0.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clases = _claseService.ObtenerTodos();
            var conflicto = clases.Any(c => c.ID_Instructor == idInstructor &&
                                             Math.Abs((c.Horario - horario).TotalMinutes) < 60);

            if (conflicto)
            {
                MessageBox.Show("El instructor ya tiene una clase asignada en un horario conflictivo.",
                                 "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clase = new Clase
            {
                NombreClase = comboBoxNombreClase.Text,
                Horario = horario,
                CapacidadMaxima = (int)numericUpDownCapacidadMaxima.Value,
                ID_Instructor = idInstructor
            };

            try
            {
                _claseService.Crear(clase);
                MessageBox.Show("¡Clase registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarComboBoxClases();
                LimpiarControlesClase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la clase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListarClases()
        {
            try
            {
                var clases = _claseService.ObtenerTodos();
                if (clases != null && clases.Any())
                {
                    dataGridViewClase.DataSource = clases;
                }
                else
                {
                    MessageBox.Show("No se encontraron clases.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar las clases: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarClasePorNombre()
        {
            var nombreClase = comboBoxNombreClase.Text;
            if (!string.IsNullOrEmpty(nombreClase))
            {
                try
                {
                    var clases = _claseService.ObtenerTodos()
                        .Where(c => c.NombreClase.Contains(nombreClase, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (clases.Any())
                    {
                        dataGridViewClase.DataSource = clases;
                        var claseSeleccionada = clases.FirstOrDefault();
                        if (claseSeleccionada != null)
                        {
                            CompletarControlesClase(claseSeleccionada);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron clases con ese nombre.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar la clase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarClase()
        {
            if (!int.TryParse(textBoxIdClase.Text, out int id))
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var clase = _claseService.ObtenerPorId(id);
                if (clase == null)
                {
                    MessageBox.Show("No se encontró una clase con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!TimeSpan.TryParse(maskedTextBoxHorario.Text, out TimeSpan horario))
                {
                    MessageBox.Show("Por favor, ingrese un horario válido en formato HH:mm:ss.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (int.TryParse(textBoxIdInstructorClase.Text, out int idInstructor))
                {
                    var clases = _claseService.ObtenerTodos();
                    var conflicto = clases.Any(c => c.ID_Instructor == idInstructor &&
                                                     c.ID_Clase != id &&
                                                     Math.Abs((c.Horario - horario).TotalMinutes) < 60);

                    if (conflicto)
                    {
                        MessageBox.Show("El instructor ya tiene una clase asignada en un horario conflictivo.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    clase.ID_Instructor = idInstructor;
                }

                clase.NombreClase = !string.IsNullOrWhiteSpace(comboBoxNombreClase.Text) ? comboBoxNombreClase.Text : clase.NombreClase;
                clase.Horario = horario;
                clase.CapacidadMaxima = numericUpDownCapacidadMaxima.Value > 0 ? (int)numericUpDownCapacidadMaxima.Value : clase.CapacidadMaxima;

                _claseService.Actualizar(clase);
                MessageBox.Show("¡Clase actualizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarComboBoxClases();
                LimpiarControlesClase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la clase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarClase()
        {
            if (contadorEliminarClase == 0)
            {
                if (int.TryParse(textBoxIdClase.Text, out int id))
                {
                    var clase = _claseService.ObtenerPorId(id);
                    if (clase != null)
                    {
                        CompletarControlesClase(clase);
                        MessageBox.Show($"Se ha encontrado la clase: {clase.NombreClase}. Haz clic de nuevo en 'Guardar Clase' para eliminarla.",
                                        "Confirmar eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        contadorEliminarClase++;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró una clase con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (int.TryParse(textBoxIdClase.Text, out int id))
                {
                    _claseService.Eliminar(id);
                    MessageBox.Show("¡Clase eliminada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    contadorEliminarClase = 0;
                    LlenarComboBoxClases();
                }
            }
        }

        private void buttonRegistrarClase_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesClase();
            accionActualClase = 1;
            ConfigurarControlesClase("registrar");
            buttonGuardarClase.Text = "Guardar";
        }

        private void buttonMostrarClases_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesClase();
            accionActualClase = 2;
            ConfigurarControlesClase("mostrar");
            buttonGuardarClase.Text = "Mostrar";
        }

        private void buttonBuscarClase_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesClase();
            accionActualClase = 3;
            ConfigurarControlesClase("buscar");
            buttonGuardarClase.Text = "Buscar";
        }

        private void buttonActualizarClase_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesClase();
            accionActualClase = 4;
            ConfigurarControlesClase("actualizar");
            buttonGuardarClase.Text = "Actualizar";
        }

        private void buttonEliminarClase_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesClase();
            accionActualClase = 5;
            ConfigurarControlesClase("eliminar");
            buttonGuardarClase.Text = "Eliminar";
        }

        private void buttonGuardarClase_Click_1(object sender, EventArgs e)
        {
            switch (accionActualClase)
            {
                case 1:
                    RegistrarClase();
                    break;
                case 2:
                    ListarClases();
                    break;
                case 3:
                    BuscarClasePorNombre();
                    break;
                case 4:
                    ActualizarClase();
                    break;
                case 5:
                    EliminarClase();
                    break;
                default:
                    MessageBox.Show("Seleccione una acción válida antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
