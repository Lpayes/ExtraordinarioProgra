using GimnasioManager.UI;
using System;
using System.Windows.Forms;

namespace GimnasioManager.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonMiembro_Click(object sender, EventArgs e)
        {
            FormMiembros formMiembro = new FormMiembros();
            formMiembro.Show();
        }

        private void buttonInstructor_Click(object sender, EventArgs e)
        {
            FormInstructores formInstructor = new FormInstructores();
            formInstructor.Show();
        }

        private void buttonClase_Click(object sender, EventArgs e)
        {
            FormClases formClase = new FormClases();
            formClase.Show();
        }

        private void buttonMembresia_Click(object sender, EventArgs e)
        {
            FormMembresias formMembresia = new FormMembresias();
            formMembresia.Show();
        }

        private void buttonReserva_Click(object sender, EventArgs e)
        {
            FormReservas formReservas = new FormReservas();
            formReservas.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
