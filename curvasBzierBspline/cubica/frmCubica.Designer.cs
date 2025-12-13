namespace curvasBzierBspline
{
    partial class frmCubica
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
            this.trackBarT = new System.Windows.Forms.TrackBar();
            this.trackBarVelocidad = new System.Windows.Forms.TrackBar();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnLimpiarRastro = new System.Windows.Forms.Button();
            this.lblTituloP0 = new System.Windows.Forms.Label();
            this.lblP0 = new System.Windows.Forms.Label();
            this.lblTituloP1 = new System.Windows.Forms.Label();
            this.lblP1 = new System.Windows.Forms.Label();
            this.lblTituloP2 = new System.Windows.Forms.Label();
            this.lblP2 = new System.Windows.Forms.Label();
            this.lblTituloP3 = new System.Windows.Forms.Label();
            this.lblP3 = new System.Windows.Forms.Label();
            this.lblTituloT = new System.Windows.Forms.Label();
            this.lblValorT = new System.Windows.Forms.Label();
            this.lblVelocidad = new System.Windows.Forms.Label();
            this.chkMostrarPoligono = new System.Windows.Forms.CheckBox();
            this.chkMostrarNivel1 = new System.Windows.Forms.CheckBox();
            this.chkMostrarNivel2 = new System.Windows.Forms.CheckBox();
            this.chkMostrarNivel3 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
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
            this.panelDibujo.Location = new System.Drawing.Point(402, 81);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(1060, 707);
            this.panelDibujo.TabIndex = 0;
            // 
            // trackBarT
            // 
            this.trackBarT.Location = new System.Drawing.Point(17, 45);
            this.trackBarT.Name = "trackBarT";
            this.trackBarT.Size = new System.Drawing.Size(284, 56);
            this.trackBarT.TabIndex = 1;
            // 
            // trackBarVelocidad
            // 
            this.trackBarVelocidad.Location = new System.Drawing.Point(17, 188);
            this.trackBarVelocidad.Name = "trackBarVelocidad";
            this.trackBarVelocidad.Size = new System.Drawing.Size(284, 56);
            this.trackBarVelocidad.TabIndex = 2;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(17, 107);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(75, 34);
            this.btnPlayPause.TabIndex = 3;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(109, 107);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 34);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarRastro
            // 
            this.btnLimpiarRastro.Location = new System.Drawing.Point(202, 109);
            this.btnLimpiarRastro.Name = "btnLimpiarRastro";
            this.btnLimpiarRastro.Size = new System.Drawing.Size(84, 32);
            this.btnLimpiarRastro.TabIndex = 5;
            this.btnLimpiarRastro.Text = "Limpiar Rastro";
            this.btnLimpiarRastro.UseVisualStyleBackColor = true;
            // 
            // lblTituloP0
            // 
            this.lblTituloP0.AutoSize = true;
            this.lblTituloP0.BackColor = System.Drawing.SystemColors.Control;
            this.lblTituloP0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloP0.Location = new System.Drawing.Point(46, 107);
            this.lblTituloP0.Name = "lblTituloP0";
            this.lblTituloP0.Size = new System.Drawing.Size(33, 18);
            this.lblTituloP0.TabIndex = 6;
            this.lblTituloP0.Text = "P0:";
            // 
            // lblP0
            // 
            this.lblP0.AutoSize = true;
            this.lblP0.BackColor = System.Drawing.SystemColors.Control;
            this.lblP0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP0.ForeColor = System.Drawing.Color.Red;
            this.lblP0.Location = new System.Drawing.Point(96, 107);
            this.lblP0.Name = "lblP0";
            this.lblP0.Size = new System.Drawing.Size(48, 18);
            this.lblP0.TabIndex = 7;
            this.lblP0.Text = "(0, 0)";
            // 
            // lblTituloP1
            // 
            this.lblTituloP1.AutoSize = true;
            this.lblTituloP1.BackColor = System.Drawing.SystemColors.Control;
            this.lblTituloP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloP1.Location = new System.Drawing.Point(46, 146);
            this.lblTituloP1.Name = "lblTituloP1";
            this.lblTituloP1.Size = new System.Drawing.Size(33, 18);
            this.lblTituloP1.TabIndex = 8;
            this.lblTituloP1.Text = "P1:";
            // 
            // lblP1
            // 
            this.lblP1.AutoSize = true;
            this.lblP1.BackColor = System.Drawing.SystemColors.Control;
            this.lblP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1.ForeColor = System.Drawing.Color.Orange;
            this.lblP1.Location = new System.Drawing.Point(96, 146);
            this.lblP1.Name = "lblP1";
            this.lblP1.Size = new System.Drawing.Size(48, 18);
            this.lblP1.TabIndex = 9;
            this.lblP1.Text = "(0, 0)";
            // 
            // lblTituloP2
            // 
            this.lblTituloP2.AutoSize = true;
            this.lblTituloP2.BackColor = System.Drawing.SystemColors.Control;
            this.lblTituloP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloP2.Location = new System.Drawing.Point(46, 186);
            this.lblTituloP2.Name = "lblTituloP2";
            this.lblTituloP2.Size = new System.Drawing.Size(33, 18);
            this.lblTituloP2.TabIndex = 10;
            this.lblTituloP2.Text = "P2:";
            // 
            // lblP2
            // 
            this.lblP2.AutoSize = true;
            this.lblP2.BackColor = System.Drawing.SystemColors.Control;
            this.lblP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2.ForeColor = System.Drawing.Color.Purple;
            this.lblP2.Location = new System.Drawing.Point(96, 186);
            this.lblP2.Name = "lblP2";
            this.lblP2.Size = new System.Drawing.Size(48, 18);
            this.lblP2.TabIndex = 11;
            this.lblP2.Text = "(0, 0)";
            // 
            // lblTituloP3
            // 
            this.lblTituloP3.AutoSize = true;
            this.lblTituloP3.BackColor = System.Drawing.SystemColors.Control;
            this.lblTituloP3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloP3.Location = new System.Drawing.Point(46, 227);
            this.lblTituloP3.Name = "lblTituloP3";
            this.lblTituloP3.Size = new System.Drawing.Size(33, 18);
            this.lblTituloP3.TabIndex = 12;
            this.lblTituloP3.Text = "P3:";
            // 
            // lblP3
            // 
            this.lblP3.AutoSize = true;
            this.lblP3.BackColor = System.Drawing.SystemColors.Control;
            this.lblP3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP3.ForeColor = System.Drawing.Color.Blue;
            this.lblP3.Location = new System.Drawing.Point(96, 227);
            this.lblP3.Name = "lblP3";
            this.lblP3.Size = new System.Drawing.Size(48, 18);
            this.lblP3.TabIndex = 13;
            this.lblP3.Text = "(0, 0)";
            // 
            // lblTituloT
            // 
            this.lblTituloT.AutoSize = true;
            this.lblTituloT.BackColor = System.Drawing.SystemColors.Control;
            this.lblTituloT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloT.Location = new System.Drawing.Point(46, 276);
            this.lblTituloT.Name = "lblTituloT";
            this.lblTituloT.Size = new System.Drawing.Size(85, 18);
            this.lblTituloT.TabIndex = 14;
            this.lblTituloT.Text = "Valor de t:";
            // 
            // lblValorT
            // 
            this.lblValorT.AutoSize = true;
            this.lblValorT.BackColor = System.Drawing.SystemColors.Control;
            this.lblValorT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorT.ForeColor = System.Drawing.Color.Blue;
            this.lblValorT.Location = new System.Drawing.Point(138, 276);
            this.lblValorT.Name = "lblValorT";
            this.lblValorT.Size = new System.Drawing.Size(40, 18);
            this.lblValorT.TabIndex = 15;
            this.lblValorT.Text = "0.00";
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(14, 154);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(109, 18);
            this.lblVelocidad.TabIndex = 16;
            this.lblVelocidad.Text = "Velocidad: 20";
            // 
            // chkMostrarPoligono
            // 
            this.chkMostrarPoligono.AutoSize = true;
            this.chkMostrarPoligono.Checked = true;
            this.chkMostrarPoligono.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarPoligono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMostrarPoligono.Location = new System.Drawing.Point(17, 42);
            this.chkMostrarPoligono.Name = "chkMostrarPoligono";
            this.chkMostrarPoligono.Size = new System.Drawing.Size(213, 22);
            this.chkMostrarPoligono.TabIndex = 17;
            this.chkMostrarPoligono.Text = "Mostrar polígono de control";
            this.chkMostrarPoligono.UseVisualStyleBackColor = true;
            // 
            // chkMostrarNivel1
            // 
            this.chkMostrarNivel1.AutoSize = true;
            this.chkMostrarNivel1.Checked = true;
            this.chkMostrarNivel1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarNivel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMostrarNivel1.Location = new System.Drawing.Point(17, 73);
            this.chkMostrarNivel1.Name = "chkMostrarNivel1";
            this.chkMostrarNivel1.Size = new System.Drawing.Size(212, 22);
            this.chkMostrarNivel1.TabIndex = 18;
            this.chkMostrarNivel1.Text = "Mostrar Nivel 1 (Q0,Q1,Q2)";
            this.chkMostrarNivel1.UseVisualStyleBackColor = true;
            // 
            // chkMostrarNivel2
            // 
            this.chkMostrarNivel2.AutoSize = true;
            this.chkMostrarNivel2.Checked = true;
            this.chkMostrarNivel2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarNivel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMostrarNivel2.Location = new System.Drawing.Point(17, 101);
            this.chkMostrarNivel2.Name = "chkMostrarNivel2";
            this.chkMostrarNivel2.Size = new System.Drawing.Size(186, 22);
            this.chkMostrarNivel2.TabIndex = 19;
            this.chkMostrarNivel2.Text = "Mostrar Nivel 2 (R0,R1)";
            this.chkMostrarNivel2.UseVisualStyleBackColor = true;
            // 
            // chkMostrarNivel3
            // 
            this.chkMostrarNivel3.AutoSize = true;
            this.chkMostrarNivel3.Checked = true;
            this.chkMostrarNivel3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarNivel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMostrarNivel3.Location = new System.Drawing.Point(17, 127);
            this.chkMostrarNivel3.Name = "chkMostrarNivel3";
            this.chkMostrarNivel3.Size = new System.Drawing.Size(154, 22);
            this.chkMostrarNivel3.TabIndex = 20;
            this.chkMostrarNivel3.Text = "Mostrar Nivel 3 (S)";
            this.chkMostrarNivel3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarT);
            this.groupBox1.Controls.Add(this.btnPlayPause);
            this.groupBox1.Controls.Add(this.lblVelocidad);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnLimpiarRastro);
            this.groupBox1.Controls.Add(this.trackBarVelocidad);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 337);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 250);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controles de Animación";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkMostrarNivel1);
            this.groupBox2.Controls.Add(this.chkMostrarPoligono);
            this.groupBox2.Controls.Add(this.chkMostrarNivel3);
            this.groupBox2.Controls.Add(this.chkMostrarNivel2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(29, 619);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 169);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones de Visualización";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Snow;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(29, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 239);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Coordenadas";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Book", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(613, 44);
            this.label1.TabIndex = 24;
            this.label1.Text = "Curvas de Bézier Cúbica";
            // 
            // frmCubica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(1485, 903);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblValorT);
            this.Controls.Add(this.lblTituloT);
            this.Controls.Add(this.lblP3);
            this.Controls.Add(this.lblTituloP3);
            this.Controls.Add(this.lblP2);
            this.Controls.Add(this.lblTituloP2);
            this.Controls.Add(this.lblP1);
            this.Controls.Add(this.lblTituloP1);
            this.Controls.Add(this.lblP0);
            this.Controls.Add(this.lblTituloP0);
            this.Controls.Add(this.panelDibujo);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmCubica";
            this.Text = "frmCubica";
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
        private System.Windows.Forms.TrackBar trackBarT;
        private System.Windows.Forms.TrackBar trackBarVelocidad;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnLimpiarRastro;
        private System.Windows.Forms.Label lblTituloP0;
        private System.Windows.Forms.Label lblP0;
        private System.Windows.Forms.Label lblTituloP1;
        private System.Windows.Forms.Label lblP1;
        private System.Windows.Forms.Label lblTituloP2;
        private System.Windows.Forms.Label lblP2;
        private System.Windows.Forms.Label lblTituloP3;
        private System.Windows.Forms.Label lblP3;
        private System.Windows.Forms.Label lblTituloT;
        private System.Windows.Forms.Label lblValorT;
        private System.Windows.Forms.Label lblVelocidad;
        private System.Windows.Forms.CheckBox chkMostrarPoligono;
        private System.Windows.Forms.CheckBox chkMostrarNivel1;
        private System.Windows.Forms.CheckBox chkMostrarNivel2;
        private System.Windows.Forms.CheckBox chkMostrarNivel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
    }
}