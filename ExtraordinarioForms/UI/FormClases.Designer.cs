namespace GimnasioManager.UI
{
    partial class FormClases
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClases));
            labelTitulo = new Label();
            maskedTextBoxHorario = new MaskedTextBox();
            labelIdInstructor = new Label();
            textBoxIdInstructorClase = new TextBox();
            labelid = new Label();
            textBoxIdClase = new TextBox();
            comboBoxNombreClase = new ComboBox();
            dataGridViewClase = new DataGridView();
            buttonEliminarClase = new Button();
            buttonGuardarClase = new Button();
            buttonActualizarClase = new Button();
            buttonBuscarClase = new Button();
            buttonMostrarClases = new Button();
            buttonRegistrarClase = new Button();
            numericUpDownCapacidadMaxima = new NumericUpDown();
            labelCmaxima = new Label();
            labehorario = new Label();
            labelNombre = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClase).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCapacidadMaxima).BeginInit();
            SuspendLayout();
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.BackColor = SystemColors.Info;
            labelTitulo.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(280, 9);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(275, 35);
            labelTitulo.TabIndex = 45;
            labelTitulo.Text = "Gestión de Clases";
            // 
            // maskedTextBoxHorario
            // 
            maskedTextBoxHorario.Font = new Font("Arial", 9F, FontStyle.Bold);
            maskedTextBoxHorario.Location = new Point(294, 137);
            maskedTextBoxHorario.Mask = "00:00:00";
            maskedTextBoxHorario.Name = "maskedTextBoxHorario";
            maskedTextBoxHorario.Size = new Size(250, 25);
            maskedTextBoxHorario.TabIndex = 29;
            maskedTextBoxHorario.TextAlign = HorizontalAlignment.Right;
            // 
            // labelIdInstructor
            // 
            labelIdInstructor.AutoSize = true;
            labelIdInstructor.BackColor = SystemColors.HotTrack;
            labelIdInstructor.Font = new Font("Arial", 12F, FontStyle.Bold);
            labelIdInstructor.Location = new Point(72, 207);
            labelIdInstructor.Name = "labelIdInstructor";
            labelIdInstructor.Size = new Size(128, 24);
            labelIdInstructor.TabIndex = 44;
            labelIdInstructor.Text = "ID Instructor";
            // 
            // textBoxIdInstructorClase
            // 
            textBoxIdInstructorClase.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdInstructorClase.Location = new Point(294, 203);
            textBoxIdInstructorClase.Name = "textBoxIdInstructorClase";
            textBoxIdInstructorClase.Size = new Size(250, 25);
            textBoxIdInstructorClase.TabIndex = 43;
            // 
            // labelid
            // 
            labelid.AutoSize = true;
            labelid.BackColor = SystemColors.HotTrack;
            labelid.Font = new Font("Arial", 12F, FontStyle.Bold);
            labelid.Location = new Point(72, 73);
            labelid.Name = "labelid";
            labelid.Size = new Size(116, 24);
            labelid.TabIndex = 41;
            labelid.Text = "ID de Clase";
            // 
            // textBoxIdClase
            // 
            textBoxIdClase.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdClase.Location = new Point(294, 67);
            textBoxIdClase.Name = "textBoxIdClase";
            textBoxIdClase.Size = new Size(250, 25);
            textBoxIdClase.TabIndex = 42;
            // 
            // comboBoxNombreClase
            // 
            comboBoxNombreClase.Font = new Font("Arial", 9F, FontStyle.Bold);
            comboBoxNombreClase.FormattingEnabled = true;
            comboBoxNombreClase.Location = new Point(294, 100);
            comboBoxNombreClase.Name = "comboBoxNombreClase";
            comboBoxNombreClase.Size = new Size(250, 26);
            comboBoxNombreClase.TabIndex = 40;
            // 
            // dataGridViewClase
            // 
            dataGridViewClase.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClase.Location = new Point(204, 352);
            dataGridViewClase.Name = "dataGridViewClase";
            dataGridViewClase.RowHeadersWidth = 51;
            dataGridViewClase.Size = new Size(417, 83);
            dataGridViewClase.TabIndex = 39;
            // 
            // buttonEliminarClase
            // 
            buttonEliminarClase.BackColor = SystemColors.HotTrack;
            buttonEliminarClase.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonEliminarClase.Location = new Point(587, 284);
            buttonEliminarClase.Name = "buttonEliminarClase";
            buttonEliminarClase.Size = new Size(126, 61);
            buttonEliminarClase.TabIndex = 38;
            buttonEliminarClase.Text = "EliminarClase";
            buttonEliminarClase.UseVisualStyleBackColor = false;
            buttonEliminarClase.Click += buttonEliminarClase_Click_1;
            // 
            // buttonGuardarClase
            // 
            buttonGuardarClase.BackColor = SystemColors.HotTrack;
            buttonGuardarClase.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonGuardarClase.Location = new Point(350, 261);
            buttonGuardarClase.Name = "buttonGuardarClase";
            buttonGuardarClase.Size = new Size(103, 61);
            buttonGuardarClase.TabIndex = 37;
            buttonGuardarClase.Text = "Guardar";
            buttonGuardarClase.UseVisualStyleBackColor = false;
            buttonGuardarClase.Click += buttonGuardarClase_Click_1;
            // 
            // buttonActualizarClase
            // 
            buttonActualizarClase.BackColor = SystemColors.HotTrack;
            buttonActualizarClase.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonActualizarClase.Location = new Point(587, 216);
            buttonActualizarClase.Name = "buttonActualizarClase";
            buttonActualizarClase.Size = new Size(126, 61);
            buttonActualizarClase.TabIndex = 36;
            buttonActualizarClase.Text = "ActualizarClase";
            buttonActualizarClase.UseVisualStyleBackColor = false;
            buttonActualizarClase.Click += buttonActualizarClase_Click_1;
            // 
            // buttonBuscarClase
            // 
            buttonBuscarClase.BackColor = SystemColors.HotTrack;
            buttonBuscarClase.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonBuscarClase.Location = new Point(587, 150);
            buttonBuscarClase.Name = "buttonBuscarClase";
            buttonBuscarClase.Size = new Size(126, 61);
            buttonBuscarClase.TabIndex = 35;
            buttonBuscarClase.Text = "BuscarClasePorNombre";
            buttonBuscarClase.UseVisualStyleBackColor = false;
            buttonBuscarClase.Click += buttonBuscarClase_Click_1;
            // 
            // buttonMostrarClases
            // 
            buttonMostrarClases.BackColor = SystemColors.HotTrack;
            buttonMostrarClases.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonMostrarClases.Location = new Point(587, 88);
            buttonMostrarClases.Name = "buttonMostrarClases";
            buttonMostrarClases.Size = new Size(126, 61);
            buttonMostrarClases.TabIndex = 34;
            buttonMostrarClases.Text = "Ver Clases";
            buttonMostrarClases.UseVisualStyleBackColor = false;
            buttonMostrarClases.Click += buttonMostrarClases_Click_1;
            // 
            // buttonRegistrarClase
            // 
            buttonRegistrarClase.BackColor = SystemColors.HotTrack;
            buttonRegistrarClase.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonRegistrarClase.Location = new Point(587, 27);
            buttonRegistrarClase.Name = "buttonRegistrarClase";
            buttonRegistrarClase.Size = new Size(126, 61);
            buttonRegistrarClase.TabIndex = 33;
            buttonRegistrarClase.Text = "RegistrarClase";
            buttonRegistrarClase.UseVisualStyleBackColor = false;
            buttonRegistrarClase.Click += buttonRegistrarClase_Click_1;
            // 
            // numericUpDownCapacidadMaxima
            // 
            numericUpDownCapacidadMaxima.Font = new Font("Arial", 9F, FontStyle.Bold);
            numericUpDownCapacidadMaxima.Location = new Point(294, 170);
            numericUpDownCapacidadMaxima.Name = "numericUpDownCapacidadMaxima";
            numericUpDownCapacidadMaxima.Size = new Size(250, 25);
            numericUpDownCapacidadMaxima.TabIndex = 32;
            // 
            // labelCmaxima
            // 
            labelCmaxima.AutoSize = true;
            labelCmaxima.BackColor = SystemColors.HotTrack;
            labelCmaxima.Font = new Font("Arial", 12F, FontStyle.Bold);
            labelCmaxima.Location = new Point(71, 176);
            labelCmaxima.Name = "labelCmaxima";
            labelCmaxima.Size = new Size(193, 24);
            labelCmaxima.TabIndex = 31;
            labelCmaxima.Text = "Capacidad Máxima ";
            // 
            // labehorario
            // 
            labehorario.AutoSize = true;
            labehorario.BackColor = SystemColors.HotTrack;
            labehorario.Font = new Font("Arial", 12F, FontStyle.Bold);
            labehorario.Location = new Point(71, 140);
            labehorario.Name = "labehorario";
            labehorario.Size = new Size(80, 24);
            labehorario.TabIndex = 30;
            labehorario.Text = "Horario";
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.BackColor = SystemColors.HotTrack;
            labelNombre.Font = new Font("Arial", 12F, FontStyle.Bold);
            labelNombre.Location = new Point(72, 109);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(193, 24);
            labelNombre.TabIndex = 28;
            labelNombre.Text = "Nombre de la Clase";
            // 
            // FormClases
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HotTrack;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(792, 450);
            Controls.Add(labelTitulo);
            Controls.Add(maskedTextBoxHorario);
            Controls.Add(labelIdInstructor);
            Controls.Add(textBoxIdInstructorClase);
            Controls.Add(labelid);
            Controls.Add(textBoxIdClase);
            Controls.Add(comboBoxNombreClase);
            Controls.Add(dataGridViewClase);
            Controls.Add(buttonEliminarClase);
            Controls.Add(buttonGuardarClase);
            Controls.Add(buttonActualizarClase);
            Controls.Add(buttonBuscarClase);
            Controls.Add(buttonMostrarClases);
            Controls.Add(buttonRegistrarClase);
            Controls.Add(numericUpDownCapacidadMaxima);
            Controls.Add(labelCmaxima);
            Controls.Add(labehorario);
            Controls.Add(labelNombre);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormClases";
            Text = "FormClases";
            Load += FormClases_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewClase).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCapacidadMaxima).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitulo;
        private MaskedTextBox maskedTextBoxHorario;
        private Label labelIdInstructor;
        private TextBox textBoxIdInstructorClase;
        private Label labelid;
        private TextBox textBoxIdClase;
        private ComboBox comboBoxNombreClase;
        private DataGridView dataGridViewClase;
        private Button buttonEliminarClase;
        private Button buttonGuardarClase;
        private Button buttonActualizarClase;
        private Button buttonBuscarClase;
        private Button buttonMostrarClases;
        private Button buttonRegistrarClase;
        private NumericUpDown numericUpDownCapacidadMaxima;
        private Label labelCmaxima;
        private Label labehorario;
        private Label labelNombre;
    }
}