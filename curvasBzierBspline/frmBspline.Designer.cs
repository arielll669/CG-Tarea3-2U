namespace curvasBzierBspline
{
    partial class frmBspline
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
            this.lblGrado = new System.Windows.Forms.Label();
            this.numGrado = new System.Windows.Forms.NumericUpDown();
            this.lblTipo = new System.Windows.Forms.Label();
            this.rbUniforme = new System.Windows.Forms.RadioButton();
            this.rbAbiertoUniforme = new System.Windows.Forms.RadioButton();
            this.rbPeriodica = new System.Windows.Forms.RadioButton();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.trackBarT = new System.Windows.Forms.TrackBar();
            this.trackBarVelocidad = new System.Windows.Forms.TrackBar();
            this.lblCantidadPuntos = new System.Windows.Forms.Label();
            this.lblVelocidad = new System.Windows.Forms.Label();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            this.lblDescripcionTipo = new System.Windows.Forms.Label();
            this.chkMostrarPoligono = new System.Windows.Forms.CheckBox();
            this.chkNumerarPuntos = new System.Windows.Forms.CheckBox();
            this.chkMostrarKnots = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblValorT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numGrado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDibujo
            // 
            this.panelDibujo.Location = new System.Drawing.Point(407, 248);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(991, 682);
            this.panelDibujo.TabIndex = 0;
            // 
            // lblGrado
            // 
            this.lblGrado.AutoSize = true;
            this.lblGrado.Location = new System.Drawing.Point(17, 32);
            this.lblGrado.Name = "lblGrado";
            this.lblGrado.Size = new System.Drawing.Size(135, 16);
            this.lblGrado.TabIndex = 1;
            this.lblGrado.Text = "Grado de la B-Spline:";
            // 
            // numGrado
            // 
            this.numGrado.Location = new System.Drawing.Point(170, 26);
            this.numGrado.Name = "numGrado";
            this.numGrado.Size = new System.Drawing.Size(186, 22);
            this.numGrado.TabIndex = 2;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(17, 74);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(111, 16);
            this.lblTipo.TabIndex = 3;
            this.lblTipo.Text = "Tipo de B-Spline:";
            // 
            // rbUniforme
            // 
            this.rbUniforme.AutoSize = true;
            this.rbUniforme.Location = new System.Drawing.Point(20, 102);
            this.rbUniforme.Name = "rbUniforme";
            this.rbUniforme.Size = new System.Drawing.Size(82, 20);
            this.rbUniforme.TabIndex = 4;
            this.rbUniforme.Text = "Uniforme";
            this.rbUniforme.UseVisualStyleBackColor = true;
            // 
            // rbAbiertoUniforme
            // 
            this.rbAbiertoUniforme.AutoSize = true;
            this.rbAbiertoUniforme.Checked = true;
            this.rbAbiertoUniforme.Location = new System.Drawing.Point(20, 128);
            this.rbAbiertoUniforme.Name = "rbAbiertoUniforme";
            this.rbAbiertoUniforme.Size = new System.Drawing.Size(195, 20);
            this.rbAbiertoUniforme.TabIndex = 5;
            this.rbAbiertoUniforme.TabStop = true;
            this.rbAbiertoUniforme.Text = "Abierta/Uniforme (Clamped)";
            this.rbAbiertoUniforme.UseVisualStyleBackColor = true;
            // 
            // rbPeriodica
            // 
            this.rbPeriodica.AutoSize = true;
            this.rbPeriodica.Location = new System.Drawing.Point(20, 154);
            this.rbPeriodica.Name = "rbPeriodica";
            this.rbPeriodica.Size = new System.Drawing.Size(146, 20);
            this.rbPeriodica.TabIndex = 6;
            this.rbPeriodica.Text = "Periódica (Cerrada)";
            this.rbPeriodica.UseVisualStyleBackColor = true;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(114, 221);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(75, 23);
            this.btnPlayPause.TabIndex = 7;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(198, 221);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(279, 221);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(20, 226);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(75, 23);
            this.btnAplicar.TabIndex = 10;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            // 
            // trackBarT
            // 
            this.trackBarT.Location = new System.Drawing.Point(6, 67);
            this.trackBarT.Name = "trackBarT";
            this.trackBarT.Size = new System.Drawing.Size(347, 56);
            this.trackBarT.TabIndex = 11;
            // 
            // trackBarVelocidad
            // 
            this.trackBarVelocidad.Location = new System.Drawing.Point(6, 159);
            this.trackBarVelocidad.Name = "trackBarVelocidad";
            this.trackBarVelocidad.Size = new System.Drawing.Size(352, 56);
            this.trackBarVelocidad.TabIndex = 12;
            // 
            // lblCantidadPuntos
            // 
            this.lblCantidadPuntos.AutoSize = true;
            this.lblCantidadPuntos.Location = new System.Drawing.Point(418, 229);
            this.lblCantidadPuntos.Name = "lblCantidadPuntos";
            this.lblCantidadPuntos.Size = new System.Drawing.Size(61, 16);
            this.lblCantidadPuntos.TabIndex = 13;
            this.lblCantidadPuntos.Text = "Puntos: 0";
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(8, 137);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(89, 16);
            this.lblVelocidad.TabIndex = 15;
            this.lblVelocidad.Text = "Velocidad: 20";
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.Location = new System.Drawing.Point(18, 163);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(186, 54);
            this.lblInstrucciones.TabIndex = 16;
            this.lblInstrucciones.Text = "Clic izquierdo: Agregar punto | Clic derecho: Eliminar | Arrastrar: Move";
            // 
            // lblDescripcionTipo
            // 
            this.lblDescripcionTipo.AutoSize = true;
            this.lblDescripcionTipo.ForeColor = System.Drawing.Color.Blue;
            this.lblDescripcionTipo.Location = new System.Drawing.Point(17, 195);
            this.lblDescripcionTipo.Name = "lblDescripcionTipo";
            this.lblDescripcionTipo.Size = new System.Drawing.Size(174, 16);
            this.lblDescripcionTipo.TabIndex = 17;
            this.lblDescripcionTipo.Text = "(La curva toca los extremos)";
            // 
            // chkMostrarPoligono
            // 
            this.chkMostrarPoligono.AutoSize = true;
            this.chkMostrarPoligono.Checked = true;
            this.chkMostrarPoligono.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarPoligono.Location = new System.Drawing.Point(14, 34);
            this.chkMostrarPoligono.Name = "chkMostrarPoligono";
            this.chkMostrarPoligono.Size = new System.Drawing.Size(192, 20);
            this.chkMostrarPoligono.TabIndex = 18;
            this.chkMostrarPoligono.Text = "Mostrar polígono de control";
            this.chkMostrarPoligono.UseVisualStyleBackColor = true;
            // 
            // chkNumerarPuntos
            // 
            this.chkNumerarPuntos.AutoSize = true;
            this.chkNumerarPuntos.Checked = true;
            this.chkNumerarPuntos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumerarPuntos.Location = new System.Drawing.Point(14, 60);
            this.chkNumerarPuntos.Name = "chkNumerarPuntos";
            this.chkNumerarPuntos.Size = new System.Drawing.Size(124, 20);
            this.chkNumerarPuntos.TabIndex = 19;
            this.chkNumerarPuntos.Text = "Numerar puntos";
            this.chkNumerarPuntos.UseVisualStyleBackColor = true;
            // 
            // chkMostrarKnots
            // 
            this.chkMostrarKnots.AutoSize = true;
            this.chkMostrarKnots.Location = new System.Drawing.Point(14, 86);
            this.chkMostrarKnots.Name = "chkMostrarKnots";
            this.chkMostrarKnots.Size = new System.Drawing.Size(174, 20);
            this.chkMostrarKnots.TabIndex = 20;
            this.chkMostrarKnots.Text = "Mostrar vector de nodos";
            this.chkMostrarKnots.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblGrado);
            this.groupBox1.Controls.Add(this.numGrado);
            this.groupBox1.Controls.Add(this.lblTipo);
            this.groupBox1.Controls.Add(this.rbPeriodica);
            this.groupBox1.Controls.Add(this.rbUniforme);
            this.groupBox1.Controls.Add(this.rbAbiertoUniforme);
            this.groupBox1.Controls.Add(this.lblDescripcionTipo);
            this.groupBox1.Controls.Add(this.btnAplicar);
            this.groupBox1.Location = new System.Drawing.Point(12, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 264);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuración de B-Spline";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblValorT);
            this.groupBox2.Controls.Add(this.trackBarT);
            this.groupBox2.Controls.Add(this.btnPlayPause);
            this.groupBox2.Controls.Add(this.trackBarVelocidad);
            this.groupBox2.Controls.Add(this.lblVelocidad);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.btnLimpiar);
            this.groupBox2.Location = new System.Drawing.Point(21, 670);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 260);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controles de Animación";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkMostrarKnots);
            this.groupBox3.Controls.Add(this.chkMostrarPoligono);
            this.groupBox3.Controls.Add(this.chkNumerarPuntos);
            this.groupBox3.Location = new System.Drawing.Point(15, 529);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(370, 126);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opciones de Visualización";
            // 
            // lblValorT
            // 
            this.lblValorT.AutoSize = true;
            this.lblValorT.Location = new System.Drawing.Point(11, 35);
            this.lblValorT.Name = "lblValorT";
            this.lblValorT.Size = new System.Drawing.Size(47, 16);
            this.lblValorT.TabIndex = 24;
            this.lblValorT.Text = "t = 0.00";
            // 
            // frmBspline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 1055);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblInstrucciones);
            this.Controls.Add(this.lblCantidadPuntos);
            this.Controls.Add(this.panelDibujo);
            this.Name = "frmBspline";
            this.Text = "frmBspline";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numGrado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDibujo;
        private System.Windows.Forms.Timer timerAnimacion;
        private System.Windows.Forms.Label lblGrado;
        private System.Windows.Forms.NumericUpDown numGrado;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.RadioButton rbUniforme;
        private System.Windows.Forms.RadioButton rbAbiertoUniforme;
        private System.Windows.Forms.RadioButton rbPeriodica;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.TrackBar trackBarT;
        private System.Windows.Forms.TrackBar trackBarVelocidad;
        private System.Windows.Forms.Label lblCantidadPuntos;
        private System.Windows.Forms.Label lblVelocidad;
        private System.Windows.Forms.Label lblInstrucciones;
        private System.Windows.Forms.Label lblDescripcionTipo;
        private System.Windows.Forms.CheckBox chkMostrarPoligono;
        private System.Windows.Forms.CheckBox chkNumerarPuntos;
        private System.Windows.Forms.CheckBox chkMostrarKnots;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblValorT;
    }
}