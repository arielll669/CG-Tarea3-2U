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
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDibujo
            // 
            this.panelDibujo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelDibujo.Location = new System.Drawing.Point(564, 29);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(685, 363);
            this.panelDibujo.TabIndex = 0;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(37, 72);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(75, 23);
            this.btnPlayPause.TabIndex = 1;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(37, 106);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(37, 135);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // trackBarT
            // 
            this.trackBarT.Location = new System.Drawing.Point(327, 58);
            this.trackBarT.Name = "trackBarT";
            this.trackBarT.Size = new System.Drawing.Size(104, 56);
            this.trackBarT.TabIndex = 5;
            // 
            // trackBarVelocidad
            // 
            this.trackBarVelocidad.Location = new System.Drawing.Point(327, 120);
            this.trackBarVelocidad.Name = "trackBarVelocidad";
            this.trackBarVelocidad.Size = new System.Drawing.Size(104, 56);
            this.trackBarVelocidad.TabIndex = 6;
            // 
            // lblCantidadPuntos
            // 
            this.lblCantidadPuntos.AutoSize = true;
            this.lblCantidadPuntos.Location = new System.Drawing.Point(180, 58);
            this.lblCantidadPuntos.Name = "lblCantidadPuntos";
            this.lblCantidadPuntos.Size = new System.Drawing.Size(120, 16);
            this.lblCantidadPuntos.TabIndex = 7;
            this.lblCantidadPuntos.Text = "Puntos: 0 (Grado: -)";
            // 
            // lblValorT
            // 
            this.lblValorT.AutoSize = true;
            this.lblValorT.Location = new System.Drawing.Point(180, 79);
            this.lblValorT.Name = "lblValorT";
            this.lblValorT.Size = new System.Drawing.Size(47, 16);
            this.lblValorT.TabIndex = 8;
            this.lblValorT.Text = "t = 0.00";
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(180, 106);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(89, 16);
            this.lblVelocidad.TabIndex = 9;
            this.lblVelocidad.Text = "Velocidad: 20";
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.AutoSize = true;
            this.lblInstrucciones.Location = new System.Drawing.Point(180, 135);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(142, 16);
            this.lblInstrucciones.TabIndex = 10;
            this.lblInstrucciones.Text = "Clic izquierdo: Agregar";
            // 
            // chkMostrarPoligono
            // 
            this.chkMostrarPoligono.AutoSize = true;
            this.chkMostrarPoligono.Checked = true;
            this.chkMostrarPoligono.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarPoligono.Location = new System.Drawing.Point(185, 202);
            this.chkMostrarPoligono.Name = "chkMostrarPoligono";
            this.chkMostrarPoligono.Size = new System.Drawing.Size(192, 20);
            this.chkMostrarPoligono.TabIndex = 11;
            this.chkMostrarPoligono.Text = "Mostrar polígono de control";
            this.chkMostrarPoligono.UseVisualStyleBackColor = true;
            // 
            // chkNumerarPuntos
            // 
            this.chkNumerarPuntos.AutoSize = true;
            this.chkNumerarPuntos.Checked = true;
            this.chkNumerarPuntos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumerarPuntos.Location = new System.Drawing.Point(189, 224);
            this.chkNumerarPuntos.Name = "chkNumerarPuntos";
            this.chkNumerarPuntos.Size = new System.Drawing.Size(124, 20);
            this.chkNumerarPuntos.TabIndex = 12;
            this.chkNumerarPuntos.Text = "Numerar puntos";
            this.chkNumerarPuntos.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(19, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones de Visualización";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(260, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controles de Animación";
            // 
            // frmGradoN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkNumerarPuntos);
            this.Controls.Add(this.chkMostrarPoligono);
            this.Controls.Add(this.lblInstrucciones);
            this.Controls.Add(this.lblVelocidad);
            this.Controls.Add(this.lblValorT);
            this.Controls.Add(this.lblCantidadPuntos);
            this.Controls.Add(this.trackBarVelocidad);
            this.Controls.Add(this.trackBarT);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPlayPause);
            this.Controls.Add(this.panelDibujo);
            this.Name = "frmGradoN";
            this.Text = "Curva de grado N";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).EndInit();
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
    }
}