namespace curvasBzierBspline
{
    partial class frmCuadratica
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
            this.btnLimpiarRastro = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblVelocidad = new System.Windows.Forms.Label();
            this.trackBarVelocidad = new System.Windows.Forms.TrackBar();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.trackBarT = new System.Windows.Forms.TrackBar();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkMostrarPoligono = new System.Windows.Forms.CheckBox();
            this.chkMostrarConstruccion = new System.Windows.Forms.CheckBox();
            this.chkDejarRastro = new System.Windows.Forms.CheckBox();
            this.lblTituloP0 = new System.Windows.Forms.Label();
            this.lblP0 = new System.Windows.Forms.Label();
            this.lblTituloP1 = new System.Windows.Forms.Label();
            this.lblP1 = new System.Windows.Forms.Label();
            this.lblValorT = new System.Windows.Forms.Label();
            this.lblTituloP2 = new System.Windows.Forms.Label();
            this.lblTituloT = new System.Windows.Forms.Label();
            this.lblP2 = new System.Windows.Forms.Label();
            this.timerAnimacion = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDibujo
            // 
            this.panelDibujo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelDibujo.Location = new System.Drawing.Point(402, 148);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(1019, 514);
            this.panelDibujo.TabIndex = 0;
            // 
            // btnLimpiarRastro
            // 
            this.btnLimpiarRastro.Location = new System.Drawing.Point(181, 124);
            this.btnLimpiarRastro.Name = "btnLimpiarRastro";
            this.btnLimpiarRastro.Size = new System.Drawing.Size(75, 32);
            this.btnLimpiarRastro.TabIndex = 8;
            this.btnLimpiarRastro.Text = "Limpiar Rastro";
            this.btnLimpiarRastro.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblVelocidad);
            this.groupBox1.Controls.Add(this.btnLimpiarRastro);
            this.groupBox1.Controls.Add(this.trackBarVelocidad);
            this.groupBox1.Controls.Add(this.btnPlayPause);
            this.groupBox1.Controls.Add(this.trackBarT);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblTituloT);
            this.groupBox1.Controls.Add(this.lblValorT);
            this.groupBox1.Font = new System.Drawing.Font("Myanmar Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(30, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 275);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controles de Animación";
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(21, 172);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(106, 27);
            this.lblVelocidad.TabIndex = 16;
            this.lblVelocidad.Text = "Velocidad: 20";
            // 
            // trackBarVelocidad
            // 
            this.trackBarVelocidad.Location = new System.Drawing.Point(19, 206);
            this.trackBarVelocidad.Name = "trackBarVelocidad";
            this.trackBarVelocidad.Size = new System.Drawing.Size(253, 56);
            this.trackBarVelocidad.TabIndex = 5;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(19, 124);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(75, 32);
            this.btnPlayPause.TabIndex = 6;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            // 
            // trackBarT
            // 
            this.trackBarT.Location = new System.Drawing.Point(19, 66);
            this.trackBarT.Name = "trackBarT";
            this.trackBarT.Size = new System.Drawing.Size(253, 56);
            this.trackBarT.TabIndex = 4;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(100, 124);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkMostrarPoligono);
            this.groupBox2.Controls.Add(this.chkMostrarConstruccion);
            this.groupBox2.Controls.Add(this.chkDejarRastro);
            this.groupBox2.Font = new System.Drawing.Font("Myanmar Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(30, 439);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 184);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones de Visualización";
            // 
            // chkMostrarPoligono
            // 
            this.chkMostrarPoligono.AutoSize = true;
            this.chkMostrarPoligono.Checked = true;
            this.chkMostrarPoligono.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarPoligono.Location = new System.Drawing.Point(31, 50);
            this.chkMostrarPoligono.Name = "chkMostrarPoligono";
            this.chkMostrarPoligono.Size = new System.Drawing.Size(231, 31);
            this.chkMostrarPoligono.TabIndex = 17;
            this.chkMostrarPoligono.Text = "Mostrar polígono de control";
            this.chkMostrarPoligono.UseVisualStyleBackColor = true;
            // 
            // chkMostrarConstruccion
            // 
            this.chkMostrarConstruccion.AutoSize = true;
            this.chkMostrarConstruccion.Checked = true;
            this.chkMostrarConstruccion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarConstruccion.Location = new System.Drawing.Point(30, 88);
            this.chkMostrarConstruccion.Name = "chkMostrarConstruccion";
            this.chkMostrarConstruccion.Size = new System.Drawing.Size(253, 31);
            this.chkMostrarConstruccion.TabIndex = 18;
            this.chkMostrarConstruccion.Text = "Mostrar algoritmo De Casteljau";
            this.chkMostrarConstruccion.UseVisualStyleBackColor = true;
            // 
            // chkDejarRastro
            // 
            this.chkDejarRastro.AutoSize = true;
            this.chkDejarRastro.Location = new System.Drawing.Point(31, 124);
            this.chkDejarRastro.Name = "chkDejarRastro";
            this.chkDejarRastro.Size = new System.Drawing.Size(196, 31);
            this.chkDejarRastro.TabIndex = 19;
            this.chkDejarRastro.Text = "Dejar rastro de la curva";
            this.chkDejarRastro.UseVisualStyleBackColor = true;
            // 
            // lblTituloP0
            // 
            this.lblTituloP0.AutoSize = true;
            this.lblTituloP0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloP0.Location = new System.Drawing.Point(397, 118);
            this.lblTituloP0.Name = "lblTituloP0";
            this.lblTituloP0.Size = new System.Drawing.Size(37, 20);
            this.lblTituloP0.TabIndex = 8;
            this.lblTituloP0.Text = "P0:";
            // 
            // lblP0
            // 
            this.lblP0.AutoSize = true;
            this.lblP0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP0.ForeColor = System.Drawing.Color.Red;
            this.lblP0.Location = new System.Drawing.Point(440, 118);
            this.lblP0.Name = "lblP0";
            this.lblP0.Size = new System.Drawing.Size(54, 20);
            this.lblP0.TabIndex = 9;
            this.lblP0.Text = "(0, 0)";
            // 
            // lblTituloP1
            // 
            this.lblTituloP1.AutoSize = true;
            this.lblTituloP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloP1.Location = new System.Drawing.Point(539, 118);
            this.lblTituloP1.Name = "lblTituloP1";
            this.lblTituloP1.Size = new System.Drawing.Size(37, 20);
            this.lblTituloP1.TabIndex = 10;
            this.lblTituloP1.Text = "P1:";
            // 
            // lblP1
            // 
            this.lblP1.AutoSize = true;
            this.lblP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1.ForeColor = System.Drawing.Color.Orange;
            this.lblP1.Location = new System.Drawing.Point(582, 118);
            this.lblP1.Name = "lblP1";
            this.lblP1.Size = new System.Drawing.Size(54, 20);
            this.lblP1.TabIndex = 11;
            this.lblP1.Text = "(0, 0)";
            // 
            // lblValorT
            // 
            this.lblValorT.AutoSize = true;
            this.lblValorT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorT.ForeColor = System.Drawing.Color.Blue;
            this.lblValorT.Location = new System.Drawing.Point(139, 31);
            this.lblValorT.Name = "lblValorT";
            this.lblValorT.Size = new System.Drawing.Size(44, 20);
            this.lblValorT.TabIndex = 15;
            this.lblValorT.Text = "0.00";
            // 
            // lblTituloP2
            // 
            this.lblTituloP2.AutoSize = true;
            this.lblTituloP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloP2.Location = new System.Drawing.Point(693, 118);
            this.lblTituloP2.Name = "lblTituloP2";
            this.lblTituloP2.Size = new System.Drawing.Size(37, 20);
            this.lblTituloP2.TabIndex = 12;
            this.lblTituloP2.Text = "P2:";
            // 
            // lblTituloT
            // 
            this.lblTituloT.AutoSize = true;
            this.lblTituloT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloT.Location = new System.Drawing.Point(26, 31);
            this.lblTituloT.Name = "lblTituloT";
            this.lblTituloT.Size = new System.Drawing.Size(97, 20);
            this.lblTituloT.TabIndex = 14;
            this.lblTituloT.Text = "Valor de t:";
            // 
            // lblP2
            // 
            this.lblP2.AutoSize = true;
            this.lblP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2.ForeColor = System.Drawing.Color.Blue;
            this.lblP2.Location = new System.Drawing.Point(736, 118);
            this.lblP2.Name = "lblP2";
            this.lblP2.Size = new System.Drawing.Size(54, 20);
            this.lblP2.TabIndex = 13;
            this.lblP2.Text = "(0, 0)";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Book", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(613, 44);
            this.label1.TabIndex = 14;
            this.label1.Text = "Curvas de Bézier Cuadrática";
            // 
            // frmCuadratica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(1433, 717);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTituloP0);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblP1);
            this.Controls.Add(this.lblP0);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblP2);
            this.Controls.Add(this.panelDibujo);
            this.Controls.Add(this.lblTituloP1);
            this.Controls.Add(this.lblTituloP2);
            this.Name = "frmCuadratica";
            this.Text = "frmCuadratica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCuadratica_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDibujo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timerAnimacion;
        private System.Windows.Forms.TrackBar trackBarT;
        private System.Windows.Forms.TrackBar trackBarVelocidad;
        private System.Windows.Forms.Button btnLimpiarRastro;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblTituloP0;
        private System.Windows.Forms.Label lblP0;
        private System.Windows.Forms.Label lblTituloP1;
        private System.Windows.Forms.Label lblP1;
        private System.Windows.Forms.Label lblTituloP2;
        private System.Windows.Forms.Label lblP2;
        private System.Windows.Forms.Label lblTituloT;
        private System.Windows.Forms.Label lblValorT;
        private System.Windows.Forms.Label lblVelocidad;
        private System.Windows.Forms.CheckBox chkMostrarPoligono;
        private System.Windows.Forms.CheckBox chkMostrarConstruccion;
        private System.Windows.Forms.CheckBox chkDejarRastro;
        private System.Windows.Forms.Label label1;
    }
}