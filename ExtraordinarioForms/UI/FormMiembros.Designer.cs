namespace GimnasioManager.UI
{
    partial class FormMiembros
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMiembros));
            labelTitulo = new Label();
            textBoxIdMembresiaMiembro = new TextBox();
            labelIdMembresia = new Label();
            buttonEliminarMiembro = new Button();
            comboBoxNombreMiembro = new ComboBox();
            textBoxIdMiembro = new TextBox();
            labelIdMiembro = new Label();
            dataGridViewMiembro = new DataGridView();
            buttonGuardarMiembro = new Button();
            buttonActualizarMiembro = new Button();
            buttonBuscarMiembro = new Button();
            buttonMostrarMiembros = new Button();
            buttonRegistrarMiembro = new Button();
            textBoxTelefonoMiembro = new TextBox();
            textBoxEmailMiembro = new TextBox();
            dateTimePickerFNacimeinto = new DateTimePicker();
            textBoxApellidoMiembro = new TextBox();
            labelTelefono = new Label();
            labelEmail = new Label();
            labelFechaNacimiento = new Label();
            labelApellido = new Label();
            labelNombre = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMiembro).BeginInit();
            SuspendLayout();
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.BackColor = SystemColors.Info;
            labelTitulo.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(280, 29);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(318, 35);
            labelTitulo.TabIndex = 47;
            labelTitulo.Text = "Gestión de Miembros";
            // 
            // textBoxIdMembresiaMiembro
            // 
            textBoxIdMembresiaMiembro.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdMembresiaMiembro.Location = new Point(297, 290);
            textBoxIdMembresiaMiembro.Name = "textBoxIdMembresiaMiembro";
            textBoxIdMembresiaMiembro.Size = new Size(288, 25);
            textBoxIdMembresiaMiembro.TabIndex = 46;
            // 
            // labelIdMembresia
            // 
            labelIdMembresia.AutoSize = true;
            labelIdMembresia.BackColor = SystemColors.HotTrack;
            labelIdMembresia.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelIdMembresia.Location = new Point(104, 283);
            labelIdMembresia.Name = "labelIdMembresia";
            labelIdMembresia.Size = new Size(138, 19);
            labelIdMembresia.TabIndex = 45;
            labelIdMembresia.Text = "ID de Membresía";
            // 
            // buttonEliminarMiembro
            // 
            buttonEliminarMiembro.BackColor = SystemColors.HotTrack;
            buttonEliminarMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonEliminarMiembro.Location = new Point(661, 290);
            buttonEliminarMiembro.Name = "buttonEliminarMiembro";
            buttonEliminarMiembro.Size = new Size(126, 61);
            buttonEliminarMiembro.TabIndex = 44;
            buttonEliminarMiembro.Text = "Eliminar Miembro";
            buttonEliminarMiembro.UseVisualStyleBackColor = false;
            buttonEliminarMiembro.Click += buttonEliminarMiembro_Click_1;
            // 
            // comboBoxNombreMiembro
            // 
            comboBoxNombreMiembro.Font = new Font("Arial", 9F, FontStyle.Bold);
            comboBoxNombreMiembro.FormattingEnabled = true;
            comboBoxNombreMiembro.Location = new Point(297, 113);
            comboBoxNombreMiembro.Name = "comboBoxNombreMiembro";
            comboBoxNombreMiembro.Size = new Size(288, 26);
            comboBoxNombreMiembro.TabIndex = 43;
            // 
            // textBoxIdMiembro
            // 
            textBoxIdMiembro.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdMiembro.Location = new Point(295, 79);
            textBoxIdMiembro.Name = "textBoxIdMiembro";
            textBoxIdMiembro.Size = new Size(290, 25);
            textBoxIdMiembro.TabIndex = 42;
            // 
            // labelIdMiembro
            // 
            labelIdMiembro.AutoSize = true;
            labelIdMiembro.BackColor = SystemColors.HotTrack;
            labelIdMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelIdMiembro.Location = new Point(106, 78);
            labelIdMiembro.Name = "labelIdMiembro";
            labelIdMiembro.Size = new Size(121, 19);
            labelIdMiembro.TabIndex = 41;
            labelIdMiembro.Text = "ID de Miembro";
            // 
            // dataGridViewMiembro
            // 
            dataGridViewMiembro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMiembro.Location = new Point(154, 389);
            dataGridViewMiembro.Name = "dataGridViewMiembro";
            dataGridViewMiembro.RowHeadersWidth = 51;
            dataGridViewMiembro.Size = new Size(579, 104);
            dataGridViewMiembro.TabIndex = 40;
            // 
            // buttonGuardarMiembro
            // 
            buttonGuardarMiembro.BackColor = SystemColors.HotTrack;
            buttonGuardarMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonGuardarMiembro.Location = new Point(367, 322);
            buttonGuardarMiembro.Name = "buttonGuardarMiembro";
            buttonGuardarMiembro.Size = new Size(126, 61);
            buttonGuardarMiembro.TabIndex = 39;
            buttonGuardarMiembro.Text = "Guardar";
            buttonGuardarMiembro.UseVisualStyleBackColor = false;
            buttonGuardarMiembro.Click += buttonGuardarMiembro_Click_1;
            // 
            // buttonActualizarMiembro
            // 
            buttonActualizarMiembro.BackColor = SystemColors.HotTrack;
            buttonActualizarMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonActualizarMiembro.Location = new Point(661, 223);
            buttonActualizarMiembro.Name = "buttonActualizarMiembro";
            buttonActualizarMiembro.Size = new Size(126, 61);
            buttonActualizarMiembro.TabIndex = 38;
            buttonActualizarMiembro.Text = "Actualizar Miembro";
            buttonActualizarMiembro.UseVisualStyleBackColor = false;
            buttonActualizarMiembro.Click += buttonActualizarMiembro_Click_1;
            // 
            // buttonBuscarMiembro
            // 
            buttonBuscarMiembro.BackColor = SystemColors.HotTrack;
            buttonBuscarMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonBuscarMiembro.Location = new Point(661, 159);
            buttonBuscarMiembro.Name = "buttonBuscarMiembro";
            buttonBuscarMiembro.Size = new Size(126, 61);
            buttonBuscarMiembro.TabIndex = 37;
            buttonBuscarMiembro.Text = "Buscar Miembro";
            buttonBuscarMiembro.UseVisualStyleBackColor = false;
            buttonBuscarMiembro.Click += buttonBuscarMiembro_Click_1;
            // 
            // buttonMostrarMiembros
            // 
            buttonMostrarMiembros.BackColor = SystemColors.HotTrack;
            buttonMostrarMiembros.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonMostrarMiembros.Location = new Point(661, 92);
            buttonMostrarMiembros.Name = "buttonMostrarMiembros";
            buttonMostrarMiembros.Size = new Size(126, 61);
            buttonMostrarMiembros.TabIndex = 36;
            buttonMostrarMiembros.Text = "Mostrar Miembros";
            buttonMostrarMiembros.UseVisualStyleBackColor = false;
            buttonMostrarMiembros.Click += buttonMostrarMiembros_Click_1;
            // 
            // buttonRegistrarMiembro
            // 
            buttonRegistrarMiembro.BackColor = SystemColors.HotTrack;
            buttonRegistrarMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonRegistrarMiembro.Location = new Point(661, 28);
            buttonRegistrarMiembro.Name = "buttonRegistrarMiembro";
            buttonRegistrarMiembro.Size = new Size(126, 61);
            buttonRegistrarMiembro.TabIndex = 35;
            buttonRegistrarMiembro.Text = "Registrar Miembro";
            buttonRegistrarMiembro.UseVisualStyleBackColor = false;
            buttonRegistrarMiembro.Click += buttonRegistrarMiembro_Click_1;
            // 
            // textBoxTelefonoMiembro
            // 
            textBoxTelefonoMiembro.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxTelefonoMiembro.Location = new Point(297, 259);
            textBoxTelefonoMiembro.Name = "textBoxTelefonoMiembro";
            textBoxTelefonoMiembro.Size = new Size(288, 25);
            textBoxTelefonoMiembro.TabIndex = 34;
            // 
            // textBoxEmailMiembro
            // 
            textBoxEmailMiembro.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxEmailMiembro.Location = new Point(297, 222);
            textBoxEmailMiembro.Name = "textBoxEmailMiembro";
            textBoxEmailMiembro.Size = new Size(290, 25);
            textBoxEmailMiembro.TabIndex = 33;
            // 
            // dateTimePickerFNacimeinto
            // 
            dateTimePickerFNacimeinto.CustomFormat = "\"\"";
            dateTimePickerFNacimeinto.Font = new Font("Arial", 9F, FontStyle.Bold);
            dateTimePickerFNacimeinto.Location = new Point(295, 182);
            dateTimePickerFNacimeinto.Name = "dateTimePickerFNacimeinto";
            dateTimePickerFNacimeinto.Size = new Size(290, 25);
            dateTimePickerFNacimeinto.TabIndex = 32;
            dateTimePickerFNacimeinto.ValueChanged += dateTimePickerFNacimeinto_ValueChanged_1;
            // 
            // textBoxApellidoMiembro
            // 
            textBoxApellidoMiembro.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxApellidoMiembro.Location = new Point(295, 147);
            textBoxApellidoMiembro.Name = "textBoxApellidoMiembro";
            textBoxApellidoMiembro.Size = new Size(290, 25);
            textBoxApellidoMiembro.TabIndex = 31;
            // 
            // labelTelefono
            // 
            labelTelefono.AutoSize = true;
            labelTelefono.BackColor = SystemColors.HotTrack;
            labelTelefono.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelTelefono.Location = new Point(106, 250);
            labelTelefono.Name = "labelTelefono";
            labelTelefono.Size = new Size(75, 19);
            labelTelefono.TabIndex = 30;
            labelTelefono.Text = "Teléfono";
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.BackColor = SystemColors.HotTrack;
            labelEmail.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelEmail.Location = new Point(104, 221);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(51, 19);
            labelEmail.TabIndex = 29;
            labelEmail.Text = "Email";
            // 
            // labelFechaNacimiento
            // 
            labelFechaNacimiento.AutoSize = true;
            labelFechaNacimiento.BackColor = SystemColors.HotTrack;
            labelFechaNacimiento.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelFechaNacimiento.Location = new Point(104, 178);
            labelFechaNacimiento.Name = "labelFechaNacimiento";
            labelFechaNacimiento.Size = new Size(96, 38);
            labelFechaNacimiento.TabIndex = 28;
            labelFechaNacimiento.Text = "Fecha de \r\nNacimiento";
            // 
            // labelApellido
            // 
            labelApellido.AutoSize = true;
            labelApellido.BackColor = SystemColors.HotTrack;
            labelApellido.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelApellido.Location = new Point(106, 146);
            labelApellido.Name = "labelApellido";
            labelApellido.Size = new Size(71, 19);
            labelApellido.TabIndex = 27;
            labelApellido.Text = "Apellido";
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.BackColor = SystemColors.HotTrack;
            labelNombre.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelNombre.Location = new Point(106, 111);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(71, 19);
            labelNombre.TabIndex = 26;
            labelNombre.Text = "Nombre";
            // 
            // FormMiembros
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HotTrack;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(880, 505);
            Controls.Add(labelTitulo);
            Controls.Add(textBoxIdMembresiaMiembro);
            Controls.Add(labelIdMembresia);
            Controls.Add(buttonEliminarMiembro);
            Controls.Add(comboBoxNombreMiembro);
            Controls.Add(textBoxIdMiembro);
            Controls.Add(labelIdMiembro);
            Controls.Add(dataGridViewMiembro);
            Controls.Add(buttonGuardarMiembro);
            Controls.Add(buttonActualizarMiembro);
            Controls.Add(buttonBuscarMiembro);
            Controls.Add(buttonMostrarMiembros);
            Controls.Add(buttonRegistrarMiembro);
            Controls.Add(textBoxTelefonoMiembro);
            Controls.Add(textBoxEmailMiembro);
            Controls.Add(dateTimePickerFNacimeinto);
            Controls.Add(textBoxApellidoMiembro);
            Controls.Add(labelTelefono);
            Controls.Add(labelEmail);
            Controls.Add(labelFechaNacimiento);
            Controls.Add(labelApellido);
            Controls.Add(labelNombre);
            DoubleBuffered = true;
            Name = "FormMiembros";
            Text = "FormMiembros";
            Load += FormMiembros_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewMiembro).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitulo;
        private TextBox textBoxIdMembresiaMiembro;
        private Label labelIdMembresia;
        private Button buttonEliminarMiembro;
        private ComboBox comboBoxNombreMiembro;
        private TextBox textBoxIdMiembro;
        private Label labelIdMiembro;
        private DataGridView dataGridViewMiembro;
        private Button buttonGuardarMiembro;
        private Button buttonActualizarMiembro;
        private Button buttonBuscarMiembro;
        private Button buttonMostrarMiembros;
        private Button buttonRegistrarMiembro;
        private TextBox textBoxTelefonoMiembro;
        private TextBox textBoxEmailMiembro;
        private DateTimePicker dateTimePickerFNacimeinto;
        private TextBox textBoxApellidoMiembro;
        private Label labelTelefono;
        private Label labelEmail;
        private Label labelFechaNacimiento;
        private Label labelApellido;
        private Label labelNombre;
    }
}