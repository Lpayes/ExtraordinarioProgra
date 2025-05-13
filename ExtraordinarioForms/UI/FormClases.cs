using GimnasioManager.Models;
using GimnasioManager.Services;
using GimnasioManager.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GimnasioManager.UI
{
    public partial class FormClases : Form
    {
        private int contadorEliminarClase = 0;
        private int accionActualClase = 0;
        private readonly ClaseService _claseService;
        private readonly InstructorService _instructorService;
        private readonly ReservaService _reservaService;

        public FormClases()
        {
            InitializeComponent();
            _claseService = new ClaseService(new DatabaseManager());
            _instructorService = new InstructorService(new DatabaseManager());
            _reservaService = new ReservaService(new DatabaseManager());
        }

        private void FormClases_Load(object sender, EventArgs e)
        {
            comboBoxNombreClase.Items.Clear();
            comboBoxNombreClase.Items.AddRange(new object[] { "Yoga", "Spinning", "CrossFit", "Boxeo" });
            comboBoxNombreClase.DropDownStyle = ComboBoxStyle.DropDownList;
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
            dataGridViewClase.DataSource = null;
        }

        private void RegistrarClase()
        {
            if (comboBoxNombreClase.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un nombre de clase.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            var instructor = _instructorService.ObtenerPorId(idInstructor);
            if (instructor == null)
            {
                MessageBox.Show("No se encontró un instructor con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nombreClase = comboBoxNombreClase.SelectedItem.ToString();
            if (!string.Equals(instructor.Especialidad, nombreClase, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"El instructor no tiene la especialidad requerida para esta clase. Especialidad del instructor: {instructor.Especialidad}.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var clases = _claseService.ObtenerTodos();
            var conflicto = clases.Any(c => c.ID_Instructor == idInstructor &&
                                             (horario >= c.Horario && horario < c.Horario.Add(TimeSpan.FromHours(1)) ||
                                              c.Horario >= horario && c.Horario < horario.Add(TimeSpan.FromHours(1))));

            if (conflicto)
            {
                MessageBox.Show("El instructor ya tiene una clase asignada en este horario o en un horario que se superpone (duración de 1 hora).",
                                 "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clase = new Clase
            {
                NombreClase = nombreClase,
                Horario = horario,
                CapacidadMaxima = (int)numericUpDownCapacidadMaxima.Value,
                ID_Instructor = idInstructor
            };

            try
            {
                _claseService.Crear(clase);
                MessageBox.Show("¡Clase registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        if (clases.Count > 1)
                        {
                            MessageBox.Show(
                                "El primer resultado se muestra en los controles, pero todas las clases encontradas están listadas en la tabla.",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (!int.TryParse(textBoxIdClase.Text, out int idClase))
            {
                MessageBox.Show("Por favor, ingrese un ID de clase válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // Validación adicional: Verificar si la clase tiene reservas asociadas  
                var reservas = _reservaService.ObtenerTodos().Where(r => r.ID_Clase == idClase).ToList();
                if (reservas.Any())
                {
                    MessageBox.Show("No se puede actualizar la clase porque tiene reservas asociadas. Elimine primero todas las reservas de esta clase.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxNombreClase.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione un nombre de clase.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!TimeSpan.TryParse(maskedTextBoxHorario.Text, out TimeSpan horario))
                {
                    MessageBox.Show("Por favor, ingrese un horario válido en formato HH:mm:ss.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBoxIdInstructorClase.Text, out int idInstructor))
                {
                    MessageBox.Show("Por favor, ingrese un ID de instructor válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var instructor = _instructorService.ObtenerPorId(idInstructor);
                if (instructor == null)
                {
                    MessageBox.Show("No se encontró un instructor con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombreClase = comboBoxNombreClase.SelectedItem.ToString();
                if (!string.Equals(instructor.Especialidad, nombreClase, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"El instructor no tiene la especialidad requerida para esta clase. Especialidad del instructor: {instructor.Especialidad}.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.Equals(clase.NombreClase, nombreClase, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("No se puede cambiar el tipo de clase porque no coincide con la especialidad del instructor.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var clases = _claseService.ObtenerTodos();
                var conflicto = clases.Any(c => c.ID_Instructor == idInstructor &&
                                                 c.ID_Clase != idClase &&
                                                 (horario >= c.Horario && horario < c.Horario.Add(TimeSpan.FromHours(1)) ||
                                                  c.Horario >= horario && c.Horario < horario.Add(TimeSpan.FromHours(1))));

                if (conflicto)
                {
                    MessageBox.Show("El instructor ya tiene una clase asignada en este horario o en un horario que se superpone (duración de 1 hora).",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                clase.Horario = horario;
                clase.CapacidadMaxima = numericUpDownCapacidadMaxima.Value > 0 ? (int)numericUpDownCapacidadMaxima.Value : clase.CapacidadMaxima;
                clase.ID_Instructor = idInstructor;

                _claseService.Actualizar(clase);
                MessageBox.Show("¡Clase actualizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    var reservas = _reservaService.ObtenerTodos().Where(r => r.ID_Clase == id).ToList();
                    if (reservas.Any())
                    {
                        MessageBox.Show("No se puede eliminar la clase porque tiene reservas asociadas. Elimine primero todas las reservas de esta clase.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        contadorEliminarClase = 0;
                        return;
                    }

                    _claseService.Eliminar(id);
                    MessageBox.Show("¡Clase eliminada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    contadorEliminarClase = 0;
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
