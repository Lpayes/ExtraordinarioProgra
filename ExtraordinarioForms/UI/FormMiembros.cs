using GimnasioManager.Services;
using GimnasioManager.Models;
using GimnasioManager.Utils;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GimnasioManager.UI
{
    public partial class FormMiembros : Form
    {
        private int contadorEliminarMiembro = 0;
        private int accionActualMiembro = 0;

        private readonly MiembroService _miembroService;
        private readonly MembresiaService _membresiaService;
        private readonly ReservaService _reservaService;
        public FormMiembros()
        {
            InitializeComponent();
            _miembroService = new MiembroService(new DatabaseManager());
            _membresiaService = new MembresiaService(new DatabaseManager());
            _reservaService = new ReservaService(new DatabaseManager());
        }

        private void FormMiembros_Load(object sender, EventArgs e)
        {
            LlenarComboBoxMiembros();
            dateTimePickerFNacimeinto.Format = DateTimePickerFormat.Custom;
            dateTimePickerFNacimeinto.CustomFormat = " ";
        }

        private void LlenarComboBoxMiembros()
        {
            comboBoxNombreMiembro.Items.Clear();
            var miembros = _miembroService.ObtenerTodos();
            if (miembros != null && miembros.Any())
            {
                foreach (var miembro in miembros)
                {
                    comboBoxNombreMiembro.Items.Add(miembro.Nombre);
                }
            }
            else
            {
                MessageBox.Show("No se encontraron miembros.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ConfigurarControlesMiembro(string accion)
        {
            comboBoxNombreMiembro.Enabled = false;
            textBoxApellidoMiembro.Enabled = false;
            dateTimePickerFNacimeinto.Enabled = false;
            textBoxEmailMiembro.Enabled = false;
            textBoxTelefonoMiembro.Enabled = false;
            textBoxIdMiembro.Enabled = false;
            textBoxIdMembresiaMiembro.Enabled = false;
            switch (accion.ToLower())
            {
                case "registrar":
                    comboBoxNombreMiembro.Enabled = true;
                    textBoxApellidoMiembro.Enabled = true;
                    dateTimePickerFNacimeinto.Enabled = true;
                    textBoxEmailMiembro.Enabled = true;
                    textBoxTelefonoMiembro.Enabled = true;
                    textBoxIdMembresiaMiembro.Enabled = true;
                    break;

                case "mostrar":
                    break;

                case "buscar":
                    comboBoxNombreMiembro.Enabled = true;
                    break;

                case "actualizar":
                    textBoxIdMiembro.Enabled = true;
                    comboBoxNombreMiembro.Enabled = true;
                    textBoxApellidoMiembro.Enabled = true;
                    dateTimePickerFNacimeinto.Enabled = true;
                    textBoxEmailMiembro.Enabled = true;
                    textBoxTelefonoMiembro.Enabled = true;
                    break;

                case "eliminar":
                    textBoxIdMiembro.Enabled = true;
                    break;

                default:
                    MessageBox.Show("Acción no reconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void CompletarControlesMiembro(Miembro miembro)
        {
            if (miembro == null)
            {
                MessageBox.Show("No se encontró un miembro válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBoxIdMiembro.Text = miembro.ID_Miembro.ToString();
            comboBoxNombreMiembro.Text = miembro.Nombre;
            textBoxApellidoMiembro.Text = miembro.Apellido;

            dateTimePickerFNacimeinto.Format = DateTimePickerFormat.Short;
            dateTimePickerFNacimeinto.CustomFormat = "dd/MM/yyyy";
            dateTimePickerFNacimeinto.Value = miembro.FechaNacimiento != DateTime.MinValue
                ? miembro.FechaNacimiento
                : DateTime.Today;

            textBoxEmailMiembro.Text = miembro.Email;
            textBoxTelefonoMiembro.Text = miembro.Telefono;
            textBoxIdMembresiaMiembro.Text = miembro.ID_Membresia.ToString();
        }

        private void LimpiarControlesMiembro()
        {
            textBoxIdMiembro.Clear();
            comboBoxNombreMiembro.SelectedIndex = -1;
            comboBoxNombreMiembro.Text = string.Empty;
            textBoxApellidoMiembro.Clear();
            textBoxEmailMiembro.Clear();
            textBoxTelefonoMiembro.Clear();
            textBoxIdMembresiaMiembro.Clear();

            dateTimePickerFNacimeinto.Format = DateTimePickerFormat.Custom;
            dateTimePickerFNacimeinto.CustomFormat = " ";

            dataGridViewMiembro.DataSource = null;
            dataGridViewMiembro.Rows.Clear();
        }

        private void RegistrarMiembro()
        {
            if (string.IsNullOrWhiteSpace(comboBoxNombreMiembro.Text))
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxApellidoMiembro.Text))
            {
                MessageBox.Show("El campo 'Apellido' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePickerFNacimeinto.CustomFormat == " ")
            {
                MessageBox.Show("Por favor, seleccione una fecha de nacimiento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fechaNacimiento = dateTimePickerFNacimeinto.Value;
            var hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento > hoy.AddYears(-edad)) edad--;

            if (edad < 14)
            {
                MessageBox.Show("Solo se pueden registrar personas mayores o iguales a 14 años.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxEmailMiembro.Text))
            {
                MessageBox.Show("El campo 'Email' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxTelefonoMiembro.Text))
            {
                MessageBox.Show("El campo 'Teléfono' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(textBoxTelefonoMiembro.Text, @"^\d{8}$"))
            {
                MessageBox.Show("El teléfono debe contener exactamente 8 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(textBoxEmailMiembro.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El correo electrónico no tiene un formato válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxIdMembresiaMiembro.Text))
            {
                MessageBox.Show("El campo 'ID de Membresía' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxIdMembresiaMiembro.Text, out int idMembresia))
            {
                MessageBox.Show("El ID de Membresía debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var membresia = _membresiaService.ObtenerPorId(idMembresia);
            if (membresia == null)
            {
                MessageBox.Show("La membresía especificada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que no se registre un miembro con un ID ya existente  
            if (_miembroService.ObtenerTodos().Any(m => m.ID_Membresia == idMembresia))
            {
                MessageBox.Show("Cada miembro tiene su propia membresía y esta ya está asignada a otro miembro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var miembro = new Miembro
            {
                Nombre = comboBoxNombreMiembro.Text,
                Apellido = textBoxApellidoMiembro.Text,
                FechaNacimiento = dateTimePickerFNacimeinto.Value,
                Email = textBoxEmailMiembro.Text,
                Telefono = textBoxTelefonoMiembro.Text,
                ID_Membresia = idMembresia
            };

            try
            {
                _miembroService.Crear(miembro);
                MessageBox.Show("¡Miembro registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarComboBoxMiembros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el miembro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListarMiembros()
        {
            try
            {
                var miembros = _miembroService.ObtenerTodos();
                if (miembros != null && miembros.Any())
                {
                    dataGridViewMiembro.DataSource = miembros;
                }
                else
                {
                    MessageBox.Show("No se encontraron miembros.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar los miembros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarMiembroPorNombre()
        {
            var nombre = comboBoxNombreMiembro.Text;
            if (!string.IsNullOrEmpty(nombre))
            {
                try
                {
                    var miembros = _miembroService.ObtenerTodos()
                        .Where(m => m.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (miembros.Any())
                    {
                        dataGridViewMiembro.DataSource = miembros;

                        var miembroSeleccionado = miembros.FirstOrDefault();
                        if (miembroSeleccionado != null)
                        {
                            CompletarControlesMiembro(miembroSeleccionado);
                        }

                        if (miembros.Count > 1)
                        {
                            MessageBox.Show(
                                "El primer miembro encontrado con ese nombre se muestra en los controles, pero todos los miembros encontrados están listados en la tabla.",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron miembros con ese nombre.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar el miembro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarMiembro()
        {
            if (int.TryParse(textBoxIdMiembro.Text, out int id))
            {
                try
                {
                    var miembro = _miembroService.ObtenerPorId(id);
                    if (miembro != null)
                    {
                        miembro.Nombre = !string.IsNullOrWhiteSpace(comboBoxNombreMiembro.Text) ? comboBoxNombreMiembro.Text : miembro.Nombre;
                        miembro.Apellido = !string.IsNullOrWhiteSpace(textBoxApellidoMiembro.Text) ? textBoxApellidoMiembro.Text : miembro.Apellido;
                        miembro.FechaNacimiento = dateTimePickerFNacimeinto.CustomFormat != " " ? dateTimePickerFNacimeinto.Value : miembro.FechaNacimiento;
                        miembro.Email = !string.IsNullOrWhiteSpace(textBoxEmailMiembro.Text) ? textBoxEmailMiembro.Text : miembro.Email;
                        miembro.Telefono = !string.IsNullOrWhiteSpace(textBoxTelefonoMiembro.Text) ? textBoxTelefonoMiembro.Text : miembro.Telefono;

                        if (!string.IsNullOrWhiteSpace(textBoxTelefonoMiembro.Text) && !Regex.IsMatch(textBoxTelefonoMiembro.Text, @"^\d{8}$"))
                        {
                            MessageBox.Show("El teléfono debe contener exactamente 8 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(textBoxIdMembresiaMiembro.Text) && int.TryParse(textBoxIdMembresiaMiembro.Text, out int idMembresia))
                        {
                            if (idMembresia != miembro.ID_Membresia)
                            {
                                MessageBox.Show("El ID de Membresía no se puede actualizar porque está vinculado al miembro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        _miembroService.Actualizar(miembro);
                        MessageBox.Show("¡Miembro actualizado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LlenarComboBoxMiembros();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un miembro con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el miembro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void EliminarMiembro()
        {
            if (contadorEliminarMiembro == 0)
            {
                if (int.TryParse(textBoxIdMiembro.Text, out int id))
                {
                    var miembro = _miembroService.ObtenerPorId(id);
                    if (miembro != null)
                    {
                        CompletarControlesMiembro(miembro);

                        MessageBox.Show($"Se ha encontrado al miembro: {miembro.Nombre}. Haz clic en el botón 'Eliminar' para confirmar la eliminación.",
                                        "Confirmar eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        contadorEliminarMiembro++;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un miembro con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (int.TryParse(textBoxIdMiembro.Text, out int id))
                {
                    var reservas = _reservaService.ObtenerTodos().Where(r => r.ID_Miembro == id).ToList();
                    if (reservas.Any())
                    {
                        MessageBox.Show("No se puede eliminar el miembro porque tiene reservas asociadas. Elimine primero todas las reservas de este miembro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        contadorEliminarMiembro = 0;
                        return;
                    }

                    _miembroService.Eliminar(id);

                    contadorEliminarMiembro = 0;
                    LlenarComboBoxMiembros();
                }
            }
        }

        private void dateTimePickerFNacimeinto_ValueChanged_1(object sender, EventArgs e)
        {
            dateTimePickerFNacimeinto.Format = DateTimePickerFormat.Short;
            dateTimePickerFNacimeinto.CustomFormat = "dd/MM/yyyy";
        }

        private void buttonRegistrarMiembro_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMiembro();
            accionActualMiembro = 1;
            ConfigurarControlesMiembro("registrar");
            buttonGuardarMiembro.Text = "Guardar";
        }

        private void buttonMostrarMiembros_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMiembro();
            accionActualMiembro = 2;
            ConfigurarControlesMiembro("mostrar");
            buttonGuardarMiembro.Text = "Mostrar";
        }

        private void buttonBuscarMiembro_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMiembro();
            accionActualMiembro = 3;
            ConfigurarControlesMiembro("buscar");
            buttonGuardarMiembro.Text = "Buscar";
        }

        private void buttonActualizarMiembro_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMiembro();
            accionActualMiembro = 4;
            ConfigurarControlesMiembro("actualizar");
            buttonGuardarMiembro.Text = "Actualizar";
        }

        private void buttonEliminarMiembro_Click_1(object sender, EventArgs e)
        {
            LimpiarControlesMiembro();
            accionActualMiembro = 5;
            ConfigurarControlesMiembro("eliminar");
            buttonGuardarMiembro.Text = "Eliminar";
        }

        private void buttonGuardarMiembro_Click_1(object sender, EventArgs e)
        {
            switch (accionActualMiembro)
            {
                case 1:
                    RegistrarMiembro();
                    break;

                case 2:
                    ListarMiembros();
                    break;
                case 3:
                    BuscarMiembroPorNombre();
                    break;
                case 4:
                    ActualizarMiembro();
                    break;
                case 5:
                    EliminarMiembro();
                    break;
                default:
                    MessageBox.Show("Seleccione una acción válida antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
