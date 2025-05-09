namespace GimnasioManager.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            buttonMiembro = new Button();
            buttonInstructor = new Button();
            buttonClase = new Button();
            buttonMembresia = new Button();
            buttonReserva = new Button();
            labelTitulo = new Label();
            labelFrase = new Label();
            SuspendLayout();
            // 
            // buttonMiembro
            // 
            buttonMiembro.BackColor = SystemColors.HotTrack;
            buttonMiembro.Location = new Point(182, 171);
            buttonMiembro.Name = "buttonMiembro";
            buttonMiembro.Size = new Size(177, 80);
            buttonMiembro.TabIndex = 0;
            buttonMiembro.Text = "Gestión de Miembros ";
            buttonMiembro.UseVisualStyleBackColor = false;
            buttonMiembro.Click += buttonMiembro_Click;
            // 
            // buttonInstructor
            // 
            buttonInstructor.BackColor = SystemColors.HotTrack;
            buttonInstructor.Location = new Point(423, 171);
            buttonInstructor.Name = "buttonInstructor";
            buttonInstructor.Size = new Size(177, 80);
            buttonInstructor.TabIndex = 1;
            buttonInstructor.Text = "Gestión de Instructores";
            buttonInstructor.UseVisualStyleBackColor = false;
            buttonInstructor.Click += buttonInstructor_Click;
            // 
            // buttonClase
            // 
            buttonClase.BackColor = SystemColors.HotTrack;
            buttonClase.Location = new Point(677, 171);
            buttonClase.Name = "buttonClase";
            buttonClase.Size = new Size(177, 80);
            buttonClase.TabIndex = 2;
            buttonClase.Text = "Gestión de Clases";
            buttonClase.UseVisualStyleBackColor = false;
            buttonClase.Click += buttonClase_Click;
            // 
            // buttonMembresia
            // 
            buttonMembresia.BackColor = SystemColors.HotTrack;
            buttonMembresia.Location = new Point(303, 307);
            buttonMembresia.Name = "buttonMembresia";
            buttonMembresia.Size = new Size(177, 80);
            buttonMembresia.TabIndex = 3;
            buttonMembresia.Text = "Gestión de Membresías";
            buttonMembresia.UseVisualStyleBackColor = false;
            buttonMembresia.Click += buttonMembresia_Click;
            // 
            // buttonReserva
            // 
            buttonReserva.BackColor = SystemColors.HotTrack;
            buttonReserva.Location = new Point(519, 307);
            buttonReserva.Name = "buttonReserva";
            buttonReserva.Size = new Size(177, 80);
            buttonReserva.TabIndex = 4;
            buttonReserva.Text = "Gestión de Reservaciones";
            buttonReserva.UseVisualStyleBackColor = false;
            buttonReserva.Click += buttonReserva_Click;
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.BackColor = Color.Chocolate;
            labelTitulo.Font = new Font("Blackadder ITC", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(60, 102);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(863, 56);
            labelTitulo.TabIndex = 27;
            labelTitulo.Text = "Bienvenido al Sistema de Gestión del Gimnasio Payes";
            // 
            // labelFrase
            // 
            labelFrase.AutoSize = true;
            labelFrase.BackColor = Color.Transparent;
            labelFrase.Font = new Font("Blackadder ITC", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFrase.ForeColor = Color.Blue;
            labelFrase.Location = new Point(222, 416);
            labelFrase.Name = "labelFrase";
            labelFrase.Size = new Size(621, 112);
            labelFrase.TabIndex = 29;
            labelFrase.Text = "\"La constancia es la clave para alcanzar\r\ntus sueños.\"";
            labelFrase.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(977, 524);
            Controls.Add(labelFrase);
            Controls.Add(labelTitulo);
            Controls.Add(buttonReserva);
            Controls.Add(buttonMembresia);
            Controls.Add(buttonClase);
            Controls.Add(buttonInstructor);
            Controls.Add(buttonMiembro);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonMiembro;
        private Button buttonInstructor;
        private Button buttonClase;
        private Button buttonMembresia;
        private Button buttonReserva;
        private Label labelTitulo;
        private Label labelFrase;
    }
}