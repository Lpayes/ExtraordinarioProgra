namespace GimnasioManager.UI
{
    partial class FormInstructores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInstructores));
            labelTitulo = new Label();
            comboBoxNombreInstructor = new ComboBox();
            textBoxIdInstructor = new TextBox();
            labelIdInstructor = new Label();
            dataGridViewInstructor = new DataGridView();
            buttonGuardarInstructor = new Button();
            buttonEliminarInstructor = new Button();
            buttonBuscarInstructor = new Button();
            buttonActualizarInstructor = new Button();
            buttonListarInstructores = new Button();
            buttonRegistrarInstructor = new Button();
            textBoxEspecialidadInstructor = new TextBox();
            textBoxApellidoInstructor = new TextBox();
            labelEspecialidad = new Label();
            labelApellido = new Label();
            labelNombre = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInstructor).BeginInit();
            SuspendLayout();
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.BackColor = SystemColors.Info;
            labelTitulo.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(214, 11);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(359, 35);
            labelTitulo.TabIndex = 42;
            labelTitulo.Text = "Gestión de Instructores ";
            // 
            // comboBoxNombreInstructor
            // 
            comboBoxNombreInstructor.FormattingEnabled = true;
            comboBoxNombreInstructor.Location = new Point(285, 122);
            comboBoxNombreInstructor.Name = "comboBoxNombreInstructor";
            comboBoxNombreInstructor.Size = new Size(258, 28);
            comboBoxNombreInstructor.TabIndex = 41;
            // 
            // textBoxIdInstructor
            // 
            textBoxIdInstructor.Location = new Point(285, 79);
            textBoxIdInstructor.Name = "textBoxIdInstructor";
            textBoxIdInstructor.Size = new Size(258, 27);
            textBoxIdInstructor.TabIndex = 40;
            // 
            // labelIdInstructor
            // 
            labelIdInstructor.AutoSize = true;
            labelIdInstructor.BackColor = SystemColors.HotTrack;
            labelIdInstructor.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelIdInstructor.Location = new Point(120, 81);
            labelIdInstructor.Name = "labelIdInstructor";
            labelIdInstructor.Size = new Size(108, 19);
            labelIdInstructor.TabIndex = 39;
            labelIdInstructor.Text = "ID Instructor";
            // 
            // dataGridViewInstructor
            // 
            dataGridViewInstructor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInstructor.Location = new Point(243, 338);
            dataGridViewInstructor.Name = "dataGridViewInstructor";
            dataGridViewInstructor.RowHeadersWidth = 51;
            dataGridViewInstructor.Size = new Size(370, 103);
            dataGridViewInstructor.TabIndex = 38;
            // 
            // buttonGuardarInstructor
            // 
            buttonGuardarInstructor.BackColor = SystemColors.HotTrack;
            buttonGuardarInstructor.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonGuardarInstructor.Location = new Point(350, 260);
            buttonGuardarInstructor.Name = "buttonGuardarInstructor";
            buttonGuardarInstructor.Size = new Size(126, 61);
            buttonGuardarInstructor.TabIndex = 37;
            buttonGuardarInstructor.Text = "Guardar";
            buttonGuardarInstructor.UseVisualStyleBackColor = false;
            buttonGuardarInstructor.Click += buttonGuardarInstructor_Click_1;
            // 
            // buttonEliminarInstructor
            // 
            buttonEliminarInstructor.BackColor = SystemColors.HotTrack;
            buttonEliminarInstructor.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonEliminarInstructor.Location = new Point(602, 271);
            buttonEliminarInstructor.Name = "buttonEliminarInstructor";
            buttonEliminarInstructor.Size = new Size(126, 61);
            buttonEliminarInstructor.TabIndex = 36;
            buttonEliminarInstructor.Text = "Eliminar Instructor";
            buttonEliminarInstructor.UseVisualStyleBackColor = false;
            buttonEliminarInstructor.Click += buttonEliminarInstructor_Click_1;
            // 
            // buttonBuscarInstructor
            // 
            buttonBuscarInstructor.BackColor = SystemColors.HotTrack;
            buttonBuscarInstructor.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonBuscarInstructor.Location = new Point(602, 146);
            buttonBuscarInstructor.Name = "buttonBuscarInstructor";
            buttonBuscarInstructor.Size = new Size(126, 61);
            buttonBuscarInstructor.TabIndex = 35;
            buttonBuscarInstructor.Text = "Buscar por Nombre";
            buttonBuscarInstructor.UseVisualStyleBackColor = false;
            buttonBuscarInstructor.Click += buttonBuscarInstructor_Click_1;
            // 
            // buttonActualizarInstructor
            // 
            buttonActualizarInstructor.BackColor = SystemColors.HotTrack;
            buttonActualizarInstructor.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonActualizarInstructor.Location = new Point(602, 208);
            buttonActualizarInstructor.Name = "buttonActualizarInstructor";
            buttonActualizarInstructor.Size = new Size(126, 61);
            buttonActualizarInstructor.TabIndex = 34;
            buttonActualizarInstructor.Text = "Actualizar Intructor";
            buttonActualizarInstructor.UseVisualStyleBackColor = false;
            buttonActualizarInstructor.Click += buttonActualizarInstructor_Click_1;
            // 
            // buttonListarInstructores
            // 
            buttonListarInstructores.BackColor = SystemColors.HotTrack;
            buttonListarInstructores.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonListarInstructores.Location = new Point(602, 85);
            buttonListarInstructores.Name = "buttonListarInstructores";
            buttonListarInstructores.Size = new Size(126, 61);
            buttonListarInstructores.TabIndex = 33;
            buttonListarInstructores.Text = "Mostrar Instructores";
            buttonListarInstructores.UseVisualStyleBackColor = false;
            buttonListarInstructores.Click += buttonListarInstructores_Click;
            // 
            // buttonRegistrarInstructor
            // 
            buttonRegistrarInstructor.BackColor = SystemColors.HotTrack;
            buttonRegistrarInstructor.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonRegistrarInstructor.Location = new Point(602, 22);
            buttonRegistrarInstructor.Name = "buttonRegistrarInstructor";
            buttonRegistrarInstructor.Size = new Size(126, 61);
            buttonRegistrarInstructor.TabIndex = 32;
            buttonRegistrarInstructor.Text = "Registrar Instructor";
            buttonRegistrarInstructor.UseVisualStyleBackColor = false;
            buttonRegistrarInstructor.Click += buttonRegistrarInstructor_Click_1;
            // 
            // textBoxEspecialidadInstructor
            // 
            textBoxEspecialidadInstructor.Location = new Point(285, 214);
            textBoxEspecialidadInstructor.Name = "textBoxEspecialidadInstructor";
            textBoxEspecialidadInstructor.Size = new Size(258, 27);
            textBoxEspecialidadInstructor.TabIndex = 31;
            // 
            // textBoxApellidoInstructor
            // 
            textBoxApellidoInstructor.Location = new Point(285, 169);
            textBoxApellidoInstructor.Name = "textBoxApellidoInstructor";
            textBoxApellidoInstructor.Size = new Size(258, 27);
            textBoxApellidoInstructor.TabIndex = 30;
            // 
            // labelEspecialidad
            // 
            labelEspecialidad.AutoSize = true;
            labelEspecialidad.BackColor = SystemColors.HotTrack;
            labelEspecialidad.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelEspecialidad.Location = new Point(121, 216);
            labelEspecialidad.Name = "labelEspecialidad";
            labelEspecialidad.Size = new Size(107, 19);
            labelEspecialidad.TabIndex = 29;
            labelEspecialidad.Text = "Especialidad";
            // 
            // labelApellido
            // 
            labelApellido.AutoSize = true;
            labelApellido.BackColor = SystemColors.HotTrack;
            labelApellido.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelApellido.Location = new Point(123, 169);
            labelApellido.Name = "labelApellido";
            labelApellido.Size = new Size(71, 19);
            labelApellido.TabIndex = 28;
            labelApellido.Text = "Apellido";
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.BackColor = SystemColors.HotTrack;
            labelNombre.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelNombre.Location = new Point(121, 124);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(71, 19);
            labelNombre.TabIndex = 27;
            labelNombre.Text = "Nombre";
            // 
            // FormInstructores
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(839, 450);
            Controls.Add(labelTitulo);
            Controls.Add(comboBoxNombreInstructor);
            Controls.Add(textBoxIdInstructor);
            Controls.Add(labelIdInstructor);
            Controls.Add(dataGridViewInstructor);
            Controls.Add(buttonGuardarInstructor);
            Controls.Add(buttonEliminarInstructor);
            Controls.Add(buttonBuscarInstructor);
            Controls.Add(buttonActualizarInstructor);
            Controls.Add(buttonListarInstructores);
            Controls.Add(buttonRegistrarInstructor);
            Controls.Add(textBoxEspecialidadInstructor);
            Controls.Add(textBoxApellidoInstructor);
            Controls.Add(labelEspecialidad);
            Controls.Add(labelApellido);
            Controls.Add(labelNombre);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormInstructores";
            Text = "FormInstructores";
            Load += FormInstructores_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewInstructor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitulo;
        private ComboBox comboBoxNombreInstructor;
        private TextBox textBoxIdInstructor;
        private Label labelIdInstructor;
        private DataGridView dataGridViewInstructor;
        private Button buttonGuardarInstructor;
        private Button buttonEliminarInstructor;
        private Button buttonBuscarInstructor;
        private Button buttonActualizarInstructor;
        private Button buttonListarInstructores;
        private Button buttonRegistrarInstructor;
        private TextBox textBoxEspecialidadInstructor;
        private TextBox textBoxApellidoInstructor;
        private Label labelEspecialidad;
        private Label labelApellido;
        private Label labelNombre;
    }
}