namespace GimnasioManager.UI
{
    partial class FormMembresias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMembresias));
            labelTitulo = new Label();
            labelIdMiembresia = new Label();
            comboBoxTipodeMembresia = new ComboBox();
            dataGridViewMembresía = new DataGridView();
            buttonGuardarMembresia = new Button();
            buttonEliminarMembresia = new Button();
            buttonMostrarMembresias = new Button();
            buttonRegistrarMembresia = new Button();
            dateTimePickerFFin = new DateTimePicker();
            dateTimePickerFInicio = new DateTimePicker();
            numericUpDownPrecio = new NumericUpDown();
            textBoxIdMembresia = new TextBox();
            labelFFin = new Label();
            labelFInicio = new Label();
            labelPrecio = new Label();
            labelTMembresia = new Label();
            buttonActualizarMembresia = new Button();
            buttonBuscarMembresiaPorId = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMembresía).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrecio).BeginInit();
            SuspendLayout();
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.BackColor = SystemColors.Info;
            labelTitulo.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(230, 9);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(351, 35);
            labelTitulo.TabIndex = 45;
            labelTitulo.Text = "Gestión de Membresías";
            // 
            // labelIdMiembresia
            // 
            labelIdMiembresia.AutoSize = true;
            labelIdMiembresia.BackColor = SystemColors.HotTrack;
            labelIdMiembresia.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelIdMiembresia.Location = new Point(118, 61);
            labelIdMiembresia.Name = "labelIdMiembresia";
            labelIdMiembresia.Size = new Size(118, 19);
            labelIdMiembresia.TabIndex = 44;
            labelIdMiembresia.Text = "ID Miembresia";
            // 
            // comboBoxTipodeMembresia
            // 
            comboBoxTipodeMembresia.Font = new Font("Arial", 9F, FontStyle.Bold);
            comboBoxTipodeMembresia.FormattingEnabled = true;
            comboBoxTipodeMembresia.Location = new Point(261, 105);
            comboBoxTipodeMembresia.Name = "comboBoxTipodeMembresia";
            comboBoxTipodeMembresia.Size = new Size(292, 26);
            comboBoxTipodeMembresia.TabIndex = 43;
            // 
            // dataGridViewMembresía
            // 
            dataGridViewMembresía.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMembresía.Location = new Point(115, 352);
            dataGridViewMembresía.Name = "dataGridViewMembresía";
            dataGridViewMembresía.RowHeadersWidth = 51;
            dataGridViewMembresía.Size = new Size(573, 86);
            dataGridViewMembresía.TabIndex = 42;
            // 
            // buttonGuardarMembresia
            // 
            buttonGuardarMembresia.BackColor = SystemColors.HotTrack;
            buttonGuardarMembresia.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonGuardarMembresia.Location = new Point(341, 285);
            buttonGuardarMembresia.Name = "buttonGuardarMembresia";
            buttonGuardarMembresia.Size = new Size(126, 61);
            buttonGuardarMembresia.TabIndex = 40;
            buttonGuardarMembresia.Text = "Guardar";
            buttonGuardarMembresia.UseVisualStyleBackColor = false;
            buttonGuardarMembresia.Click += buttonGuardarMembresia_Click_1;
            // 
            // buttonEliminarMembresia
            // 
            buttonEliminarMembresia.BackColor = SystemColors.HotTrack;
            buttonEliminarMembresia.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonEliminarMembresia.Location = new Point(602, 280);
            buttonEliminarMembresia.Name = "buttonEliminarMembresia";
            buttonEliminarMembresia.Size = new Size(126, 61);
            buttonEliminarMembresia.TabIndex = 39;
            buttonEliminarMembresia.Text = "Eliminar Membresia";
            buttonEliminarMembresia.UseVisualStyleBackColor = false;
            buttonEliminarMembresia.Click += buttonEliminarMembresia_Click_1;
            // 
            // buttonMostrarMembresias
            // 
            buttonMostrarMembresias.BackColor = SystemColors.HotTrack;
            buttonMostrarMembresias.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonMostrarMembresias.Location = new Point(602, 79);
            buttonMostrarMembresias.Name = "buttonMostrarMembresias";
            buttonMostrarMembresias.Size = new Size(126, 61);
            buttonMostrarMembresias.TabIndex = 38;
            buttonMostrarMembresias.Text = "Mostrar Membresias";
            buttonMostrarMembresias.UseVisualStyleBackColor = false;
            buttonMostrarMembresias.Click += buttonMostrarMembresias_Click_1;
            // 
            // buttonRegistrarMembresia
            // 
            buttonRegistrarMembresia.BackColor = SystemColors.HotTrack;
            buttonRegistrarMembresia.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonRegistrarMembresia.Location = new Point(602, 12);
            buttonRegistrarMembresia.Name = "buttonRegistrarMembresia";
            buttonRegistrarMembresia.Size = new Size(126, 61);
            buttonRegistrarMembresia.TabIndex = 37;
            buttonRegistrarMembresia.Text = "Registrar Membresia";
            buttonRegistrarMembresia.UseVisualStyleBackColor = false;
            buttonRegistrarMembresia.Click += buttonRegistrarMembresia_Click_1;
            // 
            // dateTimePickerFFin
            // 
            dateTimePickerFFin.Font = new Font("Arial", 9F, FontStyle.Bold);
            dateTimePickerFFin.Location = new Point(265, 242);
            dateTimePickerFFin.Name = "dateTimePickerFFin";
            dateTimePickerFFin.Size = new Size(288, 25);
            dateTimePickerFFin.TabIndex = 36;
            dateTimePickerFFin.ValueChanged += dateTimePickerFFin_ValueChanged_1;
            // 
            // dateTimePickerFInicio
            // 
            dateTimePickerFInicio.Font = new Font("Arial", 9F, FontStyle.Bold);
            dateTimePickerFInicio.Location = new Point(265, 200);
            dateTimePickerFInicio.Name = "dateTimePickerFInicio";
            dateTimePickerFInicio.Size = new Size(288, 25);
            dateTimePickerFInicio.TabIndex = 35;
            dateTimePickerFInicio.ValueChanged += dateTimePickerFInicio_ValueChanged_1;
            // 
            // numericUpDownPrecio
            // 
            numericUpDownPrecio.DecimalPlaces = 2;
            numericUpDownPrecio.Font = new Font("Arial", 9F, FontStyle.Bold);
            numericUpDownPrecio.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownPrecio.Location = new Point(265, 158);
            numericUpDownPrecio.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownPrecio.Name = "numericUpDownPrecio";
            numericUpDownPrecio.Size = new Size(288, 25);
            numericUpDownPrecio.TabIndex = 34;
            // 
            // textBoxIdMembresia
            // 
            textBoxIdMembresia.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdMembresia.Location = new Point(265, 61);
            textBoxIdMembresia.Name = "textBoxIdMembresia";
            textBoxIdMembresia.Size = new Size(292, 25);
            textBoxIdMembresia.TabIndex = 33;
            // 
            // labelFFin
            // 
            labelFFin.AutoSize = true;
            labelFFin.BackColor = SystemColors.HotTrack;
            labelFFin.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelFFin.Location = new Point(112, 242);
            labelFFin.Name = "labelFFin";
            labelFFin.Size = new Size(109, 19);
            labelFFin.TabIndex = 32;
            labelFFin.Text = "Fecha de Fin";
            // 
            // labelFInicio
            // 
            labelFInicio.AutoSize = true;
            labelFInicio.BackColor = SystemColors.HotTrack;
            labelFInicio.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelFInicio.Location = new Point(112, 200);
            labelFInicio.Name = "labelFInicio";
            labelFInicio.Size = new Size(126, 19);
            labelFInicio.TabIndex = 31;
            labelFInicio.Text = "Fecha de Inicio";
            // 
            // labelPrecio
            // 
            labelPrecio.AutoSize = true;
            labelPrecio.BackColor = SystemColors.HotTrack;
            labelPrecio.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelPrecio.Location = new Point(118, 158);
            labelPrecio.Name = "labelPrecio";
            labelPrecio.Size = new Size(59, 19);
            labelPrecio.TabIndex = 30;
            labelPrecio.Text = "Precio";
            // 
            // labelTMembresia
            // 
            labelTMembresia.AutoSize = true;
            labelTMembresia.BackColor = SystemColors.HotTrack;
            labelTMembresia.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelTMembresia.Location = new Point(118, 108);
            labelTMembresia.Name = "labelTMembresia";
            labelTMembresia.Size = new Size(93, 38);
            labelTMembresia.TabIndex = 29;
            labelTMembresia.Text = "Tipo de \r\nMembresía\r\n";
            // 
            // buttonActualizarMembresia
            // 
            buttonActualizarMembresia.BackColor = SystemColors.HotTrack;
            buttonActualizarMembresia.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonActualizarMembresia.Location = new Point(602, 213);
            buttonActualizarMembresia.Name = "buttonActualizarMembresia";
            buttonActualizarMembresia.Size = new Size(126, 61);
            buttonActualizarMembresia.TabIndex = 41;
            buttonActualizarMembresia.Text = "Actualizar Membresias";
            buttonActualizarMembresia.UseVisualStyleBackColor = false;
            buttonActualizarMembresia.Click += buttonActualizarMembresia_Click;
            // 
            // buttonBuscarMembresiaPorId
            // 
            buttonBuscarMembresiaPorId.BackColor = SystemColors.HotTrack;
            buttonBuscarMembresiaPorId.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonBuscarMembresiaPorId.Location = new Point(602, 146);
            buttonBuscarMembresiaPorId.Name = "buttonBuscarMembresiaPorId";
            buttonBuscarMembresiaPorId.Size = new Size(126, 61);
            buttonBuscarMembresiaPorId.TabIndex = 46;
            buttonBuscarMembresiaPorId.Text = "Buscar por Id";
            buttonBuscarMembresiaPorId.UseVisualStyleBackColor = false;
            buttonBuscarMembresiaPorId.Click += buttonBuscarMembresiaPorId_Click;
            // 
            // FormMembresias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(buttonBuscarMembresiaPorId);
            Controls.Add(labelTitulo);
            Controls.Add(labelIdMiembresia);
            Controls.Add(comboBoxTipodeMembresia);
            Controls.Add(dataGridViewMembresía);
            Controls.Add(buttonActualizarMembresia);
            Controls.Add(buttonGuardarMembresia);
            Controls.Add(buttonEliminarMembresia);
            Controls.Add(buttonMostrarMembresias);
            Controls.Add(buttonRegistrarMembresia);
            Controls.Add(dateTimePickerFFin);
            Controls.Add(dateTimePickerFInicio);
            Controls.Add(numericUpDownPrecio);
            Controls.Add(textBoxIdMembresia);
            Controls.Add(labelFFin);
            Controls.Add(labelFInicio);
            Controls.Add(labelPrecio);
            Controls.Add(labelTMembresia);
            Name = "FormMembresias";
            Text = "FormMembresias";
            Load += FormMembresias_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewMembresía).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitulo;
        private Label labelIdMiembresia;
        private ComboBox comboBoxTipodeMembresia;
        private DataGridView dataGridViewMembresía;
        private Button buttonGuardarMembresia;
        private Button buttonEliminarMembresia;
        private Button buttonMostrarMembresias;
        private Button buttonRegistrarMembresia;
        private DateTimePicker dateTimePickerFFin;
        private DateTimePicker dateTimePickerFInicio;
        private NumericUpDown numericUpDownPrecio;
        private TextBox textBoxIdMembresia;
        private Label labelFFin;
        private Label labelFInicio;
        private Label labelPrecio;
        private Label labelTMembresia;
        private Button buttonActualizarMembresia;
        private Button buttonBuscarMembresiaPorId;
    }
}