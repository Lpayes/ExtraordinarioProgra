namespace GimnasioManager.UI
{
    partial class FormReservas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReservas));
            labelTitulo = new Label();
            textBoxIdClaseReserva = new TextBox();
            textBoxIdMiembroReserva = new TextBox();
            labelIdReserva = new Label();
            textBoxIdReserva = new TextBox();
            dataGridView5 = new DataGridView();
            buttonEliminarReserva = new Button();
            GuardarReserva = new Button();
            buttonMostrarReservas = new Button();
            buttonActualizarReserva = new Button();
            buttonRegistrarReserva = new Button();
            labelFReserva = new Label();
            dateTimePickerFReserva = new DateTimePicker();
            labelIdClase = new Label();
            labelIdMiembro = new Label();
            buttonBuscarReservasPorIdMiembro = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView5).BeginInit();
            SuspendLayout();
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.BackColor = SystemColors.Info;
            labelTitulo.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(189, 18);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(399, 35);
            labelTitulo.TabIndex = 44;
            labelTitulo.Text = "Gestión de Reservaciones ";
            // 
            // textBoxIdClaseReserva
            // 
            textBoxIdClaseReserva.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdClaseReserva.Location = new Point(276, 178);
            textBoxIdClaseReserva.Name = "textBoxIdClaseReserva";
            textBoxIdClaseReserva.Size = new Size(258, 25);
            textBoxIdClaseReserva.TabIndex = 43;
            // 
            // textBoxIdMiembroReserva
            // 
            textBoxIdMiembroReserva.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdMiembroReserva.Location = new Point(276, 137);
            textBoxIdMiembroReserva.Name = "textBoxIdMiembroReserva";
            textBoxIdMiembroReserva.Size = new Size(258, 25);
            textBoxIdMiembroReserva.TabIndex = 42;
            // 
            // labelIdReserva
            // 
            labelIdReserva.AutoSize = true;
            labelIdReserva.BackColor = SystemColors.HotTrack;
            labelIdReserva.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelIdReserva.Location = new Point(116, 98);
            labelIdReserva.Name = "labelIdReserva";
            labelIdReserva.Size = new Size(94, 19);
            labelIdReserva.TabIndex = 41;
            labelIdReserva.Text = "ID Reserva";
            // 
            // textBoxIdReserva
            // 
            textBoxIdReserva.Font = new Font("Arial", 9F, FontStyle.Bold);
            textBoxIdReserva.Location = new Point(276, 98);
            textBoxIdReserva.Name = "textBoxIdReserva";
            textBoxIdReserva.Size = new Size(258, 25);
            textBoxIdReserva.TabIndex = 40;
            // 
            // dataGridView5
            // 
            dataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView5.Location = new Point(205, 355);
            dataGridView5.Name = "dataGridView5";
            dataGridView5.RowHeadersWidth = 51;
            dataGridView5.Size = new Size(417, 83);
            dataGridView5.TabIndex = 37;
            // 
            // buttonEliminarReserva
            // 
            buttonEliminarReserva.BackColor = SystemColors.HotTrack;
            buttonEliminarReserva.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonEliminarReserva.Location = new Point(608, 286);
            buttonEliminarReserva.Name = "buttonEliminarReserva";
            buttonEliminarReserva.Size = new Size(126, 61);
            buttonEliminarReserva.TabIndex = 39;
            buttonEliminarReserva.Text = "Eliminar Reserva";
            buttonEliminarReserva.UseVisualStyleBackColor = false;
            buttonEliminarReserva.Click += buttonEliminarReserva_Click_1;
            // 
            // GuardarReserva
            // 
            GuardarReserva.BackColor = SystemColors.HotTrack;
            GuardarReserva.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            GuardarReserva.Location = new Point(341, 278);
            GuardarReserva.Name = "GuardarReserva";
            GuardarReserva.Size = new Size(126, 61);
            GuardarReserva.TabIndex = 38;
            GuardarReserva.Text = "Guardar";
            GuardarReserva.UseVisualStyleBackColor = false;
            GuardarReserva.Click += GuardarReserva_Click_1;
            // 
            // buttonMostrarReservas
            // 
            buttonMostrarReservas.BackColor = SystemColors.HotTrack;
            buttonMostrarReservas.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonMostrarReservas.Location = new Point(608, 85);
            buttonMostrarReservas.Name = "buttonMostrarReservas";
            buttonMostrarReservas.Size = new Size(126, 61);
            buttonMostrarReservas.TabIndex = 36;
            buttonMostrarReservas.Text = "Ver Reservas";
            buttonMostrarReservas.UseVisualStyleBackColor = false;
            buttonMostrarReservas.Click += buttonMostrarReservas_Click_1;
            // 
            // buttonActualizarReserva
            // 
            buttonActualizarReserva.BackColor = SystemColors.HotTrack;
            buttonActualizarReserva.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonActualizarReserva.Location = new Point(608, 219);
            buttonActualizarReserva.Name = "buttonActualizarReserva";
            buttonActualizarReserva.Size = new Size(126, 61);
            buttonActualizarReserva.TabIndex = 35;
            buttonActualizarReserva.Text = "Actualizar Reservas";
            buttonActualizarReserva.UseVisualStyleBackColor = false;
            buttonActualizarReserva.Click += buttonActualizarReserva_Click_1;
            // 
            // buttonRegistrarReserva
            // 
            buttonRegistrarReserva.BackColor = SystemColors.HotTrack;
            buttonRegistrarReserva.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonRegistrarReserva.Location = new Point(608, 18);
            buttonRegistrarReserva.Name = "buttonRegistrarReserva";
            buttonRegistrarReserva.Size = new Size(126, 61);
            buttonRegistrarReserva.TabIndex = 34;
            buttonRegistrarReserva.Text = "Registrar Reserva";
            buttonRegistrarReserva.UseVisualStyleBackColor = false;
            buttonRegistrarReserva.Click += buttonRegistrarReserva_Click_1;
            // 
            // labelFReserva
            // 
            labelFReserva.AutoSize = true;
            labelFReserva.BackColor = SystemColors.HotTrack;
            labelFReserva.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelFReserva.Location = new Point(114, 221);
            labelFReserva.Name = "labelFReserva";
            labelFReserva.Size = new Size(103, 38);
            labelFReserva.TabIndex = 33;
            labelFReserva.Text = "Fecha de la \r\nReserva";
            // 
            // dateTimePickerFReserva
            // 
            dateTimePickerFReserva.Font = new Font("Arial", 9F, FontStyle.Bold);
            dateTimePickerFReserva.Location = new Point(276, 221);
            dateTimePickerFReserva.Name = "dateTimePickerFReserva";
            dateTimePickerFReserva.Size = new Size(258, 25);
            dateTimePickerFReserva.TabIndex = 32;
            dateTimePickerFReserva.ValueChanged += dateTimePickerFReserva_ValueChanged_1;
            // 
            // labelIdClase
            // 
            labelIdClase.AutoSize = true;
            labelIdClase.BackColor = SystemColors.HotTrack;
            labelIdClase.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelIdClase.Location = new Point(114, 178);
            labelIdClase.Name = "labelIdClase";
            labelIdClase.Size = new Size(73, 19);
            labelIdClase.TabIndex = 31;
            labelIdClase.Text = "ID Clase";
            // 
            // labelIdMiembro
            // 
            labelIdMiembro.AutoSize = true;
            labelIdMiembro.BackColor = SystemColors.HotTrack;
            labelIdMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            labelIdMiembro.Location = new Point(116, 137);
            labelIdMiembro.Name = "labelIdMiembro";
            labelIdMiembro.Size = new Size(97, 19);
            labelIdMiembro.TabIndex = 30;
            labelIdMiembro.Text = "ID Miembro";
            // 
            // buttonBuscarReservasPorIdMiembro
            // 
            buttonBuscarReservasPorIdMiembro.BackColor = SystemColors.HotTrack;
            buttonBuscarReservasPorIdMiembro.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            buttonBuscarReservasPorIdMiembro.Location = new Point(608, 152);
            buttonBuscarReservasPorIdMiembro.Name = "buttonBuscarReservasPorIdMiembro";
            buttonBuscarReservasPorIdMiembro.Size = new Size(126, 61);
            buttonBuscarReservasPorIdMiembro.TabIndex = 45;
            buttonBuscarReservasPorIdMiembro.Text = "Buscar Reservas por Id Miembro";
            buttonBuscarReservasPorIdMiembro.UseVisualStyleBackColor = false;
            buttonBuscarReservasPorIdMiembro.Click += buttonBuscarReservasPorIdMiembro_Click;
            // 
            // FormReservas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonBuscarReservasPorIdMiembro);
            Controls.Add(labelTitulo);
            Controls.Add(textBoxIdClaseReserva);
            Controls.Add(textBoxIdMiembroReserva);
            Controls.Add(labelIdReserva);
            Controls.Add(textBoxIdReserva);
            Controls.Add(dataGridView5);
            Controls.Add(buttonEliminarReserva);
            Controls.Add(GuardarReserva);
            Controls.Add(buttonMostrarReservas);
            Controls.Add(buttonActualizarReserva);
            Controls.Add(buttonRegistrarReserva);
            Controls.Add(labelFReserva);
            Controls.Add(dateTimePickerFReserva);
            Controls.Add(labelIdClase);
            Controls.Add(labelIdMiembro);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormReservas";
            Text = "FormReservas";
            Load += FormReservas_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitulo;
        private TextBox textBoxIdClaseReserva;
        private TextBox textBoxIdMiembroReserva;
        private Label labelIdReserva;
        private TextBox textBoxIdReserva;
        private DataGridView dataGridView5;
        private Button buttonEliminarReserva;
        private Button GuardarReserva;
        private Button buttonMostrarReservas;
        private Button buttonActualizarReserva;
        private Button buttonRegistrarReserva;
        private Label labelFReserva;
        private DateTimePicker dateTimePickerFReserva;
        private Label labelIdClase;
        private Label labelIdMiembro;
        private Button buttonBuscarReservasPorIdMiembro;
    }
}