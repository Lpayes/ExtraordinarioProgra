using GimnasioManager.Models;
using GimnasioManager.Services;
using System.Data;
using GimnasioManager.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GimnasioManager.UI
{
    public partial class FormInstructores : Form
    {
        private int contadorEliminarInstructor = 0;
        private int accionActualInstructor = 0;
        private readonly InstructorService _instructorService;
        private readonly ClaseService _claseService;

        public FormInstructores()
        {
            InitializeComponent();
            _instructorService = new InstructorService(new DatabaseManager());
            _claseService = new ClaseService(new DatabaseManager());
        }

        private void FormInstructores_Load(object sender, EventArgs e)
        {
            LlenarComboBoxInstructores();
        }

        private void LlenarComboBoxInstructores()
        {
            comboBoxNombreInstructor.Items.Clear();
            var instructores = _instructorService.ObtenerTodos();
            if (instructores != null && instructores.Any())
            {
                foreach (var instructor in instructores)
                {
                    comboBoxNombreInstructor.Items.Add(instructor.Nombre);
                }
            }
            else
            {
                MessageBox.Show("No se encontraron instructores.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ConfigurarControlesInstructor(string accion)
        {
            comboBoxNombreInstructor.Enabled = false;
            textBoxApellidoInstructor.Enabled = false;
            textBoxEspecialidadInstructor.Enabled = false;
            textBoxIdInstructor.Enabled = false;

            switch (accion.ToLower())
            {
                case "registrar":
                    comboBoxNombreInstructor.Enabled = true;
                    textBoxApellidoInstructor.Enabled = true;
                    textBoxEspecialidadInstructor.Enabled = true;
                    break;

                case "mostrar":
                    break;

                case "buscar":
                    comboBoxNombreInstructor.Enabled = true;
                    break;

                case "actualizar":
                    textBoxIdInstructor.Enabled = true;
                    comboBoxNombreInstructor.Enabled = true;
                    textBoxApellidoInstructor.Enabled = true;
                    textBoxEspecialidadInstructor.Enabled = true;
                    break;

                case "eliminar":
                    textBoxIdInstructor.Enabled = true;
                    break;

                default:
                    MessageBox.Show("Acción no reconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void CompletarControlesInstructor(Instructor instructor)
        {
            if (instructor == null)
            {
                MessageBox.Show("No se encontró un instructor válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBoxIdInstructor.Text = instructor.ID_Instructor.ToString();
            comboBoxNombreInstructor.Text = instructor.Nombre;
            textBoxApellidoInstructor.Text = instructor.Apellido;
            textBoxEspecialidadInstructor.Text = instructor.Especialidad;
        }

        private void LimpiarControlesInstructor()
        {
            textBoxIdInstructor.Clear();
            comboBoxNombreInstructor.SelectedIndex = -1;
            comboBoxNombreInstructor.Text = string.Empty;
            textBoxApellidoInstructor.Clear();
            textBoxEspecialidadInstructor.Clear();
            dataGridViewInstructor.DataSource = null;
        }

        private void RegistrarInstructor()
        {
            if (string.IsNullOrWhiteSpace(comboBoxNombreInstructor.Text))
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxNombreInstructor.Text.Any(char.IsDigit))
            {
                MessageBox.Show("El nombre no debe contener números.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxApellidoInstructor.Text))
            {
                MessageBox.Show("El campo 'Apellido' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxApellidoInstructor.Text.Any(char.IsDigit))
            {
                MessageBox.Show("El apellido no debe contener números.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxEspecialidadInstructor.Text))
            {
                MessageBox.Show("El campo 'Especialidad' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validar especialidad permitida (si tienes una lista)  
            if (!InstructorService.EspecialidadesPermitidas.Contains(textBoxEspecialidadInstructor.Text))
            {
                MessageBox.Show("La especialidad no es válida. Debe ser: " + string.Join(", ", InstructorService.EspecialidadesPermitidas), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validar que no se repita nombre y apellido  
            if (_instructorService.ObtenerTodos().Any(i =>
                i.Nombre.Equals(comboBoxNombreInstructor.Text, StringComparison.OrdinalIgnoreCase) &&
                i.Apellido.Equals(textBoxApellidoInstructor.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un instructor con ese nombre y apellido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var instructor = new Instructor
            {
                Nombre = comboBoxNombreInstructor.Text,
                Apellido = textBoxApellidoInstructor.Text,
                Especialidad = textBoxEspecialidadInstructor.Text
            };

            try
            {
                _instructorService.Crear(instructor);
                MessageBox.Show("¡Instructor registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarComboBoxInstructores();
                ListarInstructores();
                LimpiarControlesInstructor();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el instructor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListarInstructores()
        {
            try
            {
                var instructores = _instructorService.ObtenerTodos();
                dataGridViewInstructor.DataSource = null;
                if (instructores != null && instructores.Any())
                {
                    dataGridViewInstructor.DataSource = instructores;
                }
                else
                {
                    dataGridViewInstructor.Rows.Clear();
                    MessageBox.Show("No se encontraron instructores.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar los instructores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarInstructorPorNombre()
        {
            var nombre = comboBoxNombreInstructor.Text;
            if (!string.IsNullOrEmpty(nombre))
            {
                try
                {
                    var instructores = _instructorService.ObtenerTodos()
                        .Where(i => i.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (instructores.Any())
                    {
                        dataGridViewInstructor.DataSource = instructores;
                        CompletarControlesInstructor(instructores.First());

                        if (instructores.Count > 1)
                        {
                            MessageBox.Show("Se encontró más de un instructor con ese nombre. El primero se mostrará en los controles, pero todos incluyendo el primero están en el DataGridView.",
                                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron instructores con ese nombre.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar el instructor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarInstructor()
        {
            if (int.TryParse(textBoxIdInstructor.Text, out int id))
            {
                try
                {
                    var instructor = _instructorService.ObtenerPorId(id);
                    if (instructor != null)
                    {
                        var clasesAsignadas = _claseService.ObtenerTodos().Where(c => c.ID_Instructor == id).ToList();
                        string nuevaEspecialidad = !string.IsNullOrWhiteSpace(textBoxEspecialidadInstructor.Text)
                            ? textBoxEspecialidadInstructor.Text
                            : instructor.Especialidad;

                        if (!string.IsNullOrWhiteSpace(textBoxEspecialidadInstructor.Text) &&
                            !InstructorService.EspecialidadesPermitidas.Contains(textBoxEspecialidadInstructor.Text))
                        {
                            MessageBox.Show("La especialidad no es válida. Debe ser: " + string.Join(", ", InstructorService.EspecialidadesPermitidas), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (clasesAsignadas.Any() && !string.Equals(instructor.Especialidad, nuevaEspecialidad, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show(
                                "Según los requerimientos del proyecto, solo se puede manejar una especialidad por instructor y no existe una tabla de especialidades. " +
                                "Para cambiar la especialidad, primero debe eliminar todas las clases asociadas a este instructor.",
                                "Restricción del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(comboBoxNombreInstructor.Text))
                        {
                            if (comboBoxNombreInstructor.Text.Any(char.IsDigit))
                            {
                                MessageBox.Show("El nombre no debe contener números.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(textBoxApellidoInstructor.Text))
                        {
                            if (textBoxApellidoInstructor.Text.Any(char.IsDigit))
                            {
                                MessageBox.Show("El apellido no debe contener números.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        string nuevoNombre = !string.IsNullOrWhiteSpace(comboBoxNombreInstructor.Text) ? comboBoxNombreInstructor.Text : instructor.Nombre;
                        string nuevoApellido = !string.IsNullOrWhiteSpace(textBoxApellidoInstructor.Text) ? textBoxApellidoInstructor.Text : instructor.Apellido;
                        if (_instructorService.ObtenerTodos().Any(i =>
                            i.ID_Instructor != id &&
                            i.Nombre.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase) &&
                            i.Apellido.Equals(nuevoApellido, StringComparison.OrdinalIgnoreCase)))
                        {
                            MessageBox.Show("Ya existe otro instructor con ese nombre y apellido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        instructor.Nombre = nuevoNombre;
                        instructor.Apellido = nuevoApellido;
                        instructor.Especialidad = nuevaEspecialidad;

                        _instructorService.ActualizarInstructor(instructor);
                        MessageBox.Show("¡Instructor actualizado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LlenarComboBoxInstructores();
                        ListarInstructores();
                        LimpiarControlesInstructor();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un instructor con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el instructor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EliminarInstructor()
        {
            if (contadorEliminarInstructor == 0)
            {
                if (int.TryParse(textBoxIdInstructor.Text, out int id))
                {
                    var instructor = _instructorService.ObtenerPorId(id);
                    if (instructor != null)
                    {
                        CompletarControlesInstructor(instructor);

                        MessageBox.Show($"Se ha encontrado al instructor: {instructor.Nombre}. Haz clic de nuevo en 'Guardar Instructor' para eliminarlo.",
                                        "Confirmar eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        contadorEliminarInstructor++;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un instructor con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (int.TryParse(textBoxIdInstructor.Text, out int id))
                {
                    var clasesAsignadas = _claseService.ObtenerTodos().Where(c => c.ID_Instructor == id).ToList();

                    if (clasesAsignadas.Any())
                    {
                        MessageBox.Show("No se puede eliminar el instructor porque tiene clases asignadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _instructorService.Eliminar(id);
                    MessageBox.Show("¡Instructor eliminado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    contadorEliminarInstructor = 0;
                    LlenarComboBoxInstructores();
                }
            }
        }

        private void buttonRegistrarInstructor_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesInstructor();
            accionActualInstructor = 1;
            ConfigurarControlesInstructor("registrar");
            buttonGuardarInstructor.Text = "Guardar";
        }

        private void buttonListarInstructores_Click(object sender, EventArgs e)
        {
            LimpiarControlesInstructor();
            accionActualInstructor = 2;
            ConfigurarControlesInstructor("mostrar");
            buttonGuardarInstructor.Text = "Mostrar";
        }

        private void buttonBuscarInstructor_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesInstructor();
            accionActualInstructor = 3;
            ConfigurarControlesInstructor("buscar");
            buttonGuardarInstructor.Text = "Buscar";
        }

        private void buttonActualizarInstructor_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesInstructor();
            accionActualInstructor = 4;
            ConfigurarControlesInstructor("actualizar");
            buttonGuardarInstructor.Text = "Actualizar";
        }

        private void buttonEliminarInstructor_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesInstructor();
            accionActualInstructor = 5;
            ConfigurarControlesInstructor("eliminar");
            buttonGuardarInstructor.Text = "Eliminar";
        }

        private void buttonGuardarInstructor_Click_1(object sender, EventArgs e)
        {
            switch (accionActualInstructor)
            {
                case 1:
                    RegistrarInstructor();
                    break;
                case 2:
                    ListarInstructores();
                    break;
                case 3:
                    BuscarInstructorPorNombre();
                    break;
                case 4:
                    ActualizarInstructor();
                    break;
                case 5:
                    EliminarInstructor();
                    break;
                default:
                    MessageBox.Show("Seleccione una acción válida antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
