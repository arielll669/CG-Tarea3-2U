namespace curvasBzierBspline
{
    partial class frmGradoN
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
            this.components = new System.ComponentModel.Container();
            this.panelDibujo = new System.Windows.Forms.Panel();
            this.timerAnimacion = new System.Windows.Forms.Timer(this.components);
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.trackBarT = new System.Windows.Forms.TrackBar();
            this.trackBarVelocidad = new System.Windows.Forms.TrackBar();
            this.lblCantidadPuntos = new System.Windows.Forms.Label();
            this.lblValorT = new System.Windows.Forms.Label();
            this.lblVelocidad = new System.Windows.Forms.Label();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            this.chkMostrarPoligono = new System.Windows.Forms.CheckBox();
            this.chkNumerarPuntos = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDibujo
            // 
            this.panelDibujo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelDibujo.Location = new System.Drawing.Point(410, 127);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(1060, 594);
            this.panelDibujo.TabIndex = 0;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(73, 132);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(75, 39);
            this.btnPlayPause.TabIndex = 1;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(164, 132);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 39);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(263, 132);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(87, 39);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // trackBarT
            // 
            this.trackBarT.Location = new System.Drawing.Point(6, 70);
            this.trackBarT.Name = "trackBarT";
            this.trackBarT.Size = new System.Drawing.Size(344, 56);
            this.trackBarT.TabIndex = 5;
            // 
            // trackBarVelocidad
            // 
            this.trackBarVelocidad.Location = new System.Drawing.Point(6, 221);
            this.trackBarVelocidad.Name = "trackBarVelocidad";
            this.trackBarVelocidad.Size = new System.Drawing.Size(344, 56);
            this.trackBarVelocidad.TabIndex = 6;
            // 
            // lblCantidadPuntos
            // 
            this.lblCantidadPuntos.AutoSize = true;
            this.lblCantidadPuntos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadPuntos.Location = new System.Drawing.Point(631, 93);
            this.lblCantidadPuntos.Name = "lblCantidadPuntos";
            this.lblCantidadPuntos.Size = new System.Drawing.Size(160, 18);
            this.lblCantidadPuntos.TabIndex = 7;
            this.lblCantidadPuntos.Text = "Puntos: 0 (Grado: -)";
            // 
            // lblValorT
            // 
            this.lblValorT.AutoSize = true;
            this.lblValorT.Location = new System.Drawing.Point(15, 37);
            this.lblValorT.Name = "lblValorT";
            this.lblValorT.Size = new System.Drawing.Size(65, 18);
            this.lblValorT.TabIndex = 8;
            this.lblValorT.Text = "t = 0.00";
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(15, 189);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(109, 18);
            this.lblVelocidad.TabIndex = 9;
            this.lblVelocidad.Text = "Velocidad: 20";
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucciones.Location = new System.Drawing.Point(413, 93);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(201, 31);
            this.lblInstrucciones.TabIndex = 10;
            this.lblInstrucciones.Text = "Clic izquierdo: Agregar";
            // 
            // chkMostrarPoligono
            // 
            this.chkMostrarPoligono.AutoSize = true;
            this.chkMostrarPoligono.Checked = true;
            this.chkMostrarPoligono.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarPoligono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMostrarPoligono.Location = new System.Drawing.Point(14, 40);
            this.chkMostrarPoligono.Name = "chkMostrarPoligono";
            this.chkMostrarPoligono.Size = new System.Drawing.Size(213, 22);
            this.chkMostrarPoligono.TabIndex = 11;
            this.chkMostrarPoligono.Text = "Mostrar polígono de control";
            this.chkMostrarPoligono.UseVisualStyleBackColor = true;
            // 
            // chkNumerarPuntos
            // 
            this.chkNumerarPuntos.AutoSize = true;
            this.chkNumerarPuntos.Checked = true;
            this.chkNumerarPuntos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumerarPuntos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNumerarPuntos.Location = new System.Drawing.Point(14, 75);
            this.chkNumerarPuntos.Name = "chkNumerarPuntos";
            this.chkNumerarPuntos.Size = new System.Drawing.Size(137, 22);
            this.chkNumerarPuntos.TabIndex = 12;
            this.chkNumerarPuntos.Text = "Numerar puntos";
            this.chkNumerarPuntos.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkNumerarPuntos);
            this.groupBox1.Controls.Add(this.chkMostrarPoligono);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 118);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones de Visualización";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLimpiar);
            this.groupBox2.Controls.Add(this.trackBarT);
            this.groupBox2.Controls.Add(this.btnPlayPause);
            this.groupBox2.Controls.Add(this.lblVelocidad);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.lblValorT);
            this.groupBox2.Controls.Add(this.trackBarVelocidad);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 261);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 290);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controles de Animación";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Book", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(613, 44);
            this.label1.TabIndex = 15;
            this.label1.Text = "Curvas de Bézier de Grado N";
            // 
            // frmGradoN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1482, 938);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblInstrucciones);
            this.Controls.Add(this.lblCantidadPuntos);
            this.Controls.Add(this.panelDibujo);
            this.Name = "frmGradoN";
            this.Text = "Curva de grado N";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDibujo;
        private System.Windows.Forms.Timer timerAnimacion;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TrackBar trackBarT;
        private System.Windows.Forms.TrackBar trackBarVelocidad;
        private System.Windows.Forms.Label lblCantidadPuntos;
        private System.Windows.Forms.Label lblValorT;
        private System.Windows.Forms.Label lblVelocidad;
        private System.Windows.Forms.Label lblInstrucciones;
        private System.Windows.Forms.CheckBox chkMostrarPoligono;
        private System.Windows.Forms.CheckBox chkNumerarPuntos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
    }
}