namespace curvasBzierBspline
{
    partial class frmLineal
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarT = new System.Windows.Forms.TrackBar();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.lblValorT = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblVelocidad = new System.Windows.Forms.Label();
            this.trackBarVelocidad = new System.Windows.Forms.TrackBar();
            this.chkMostrarLineas = new System.Windows.Forms.CheckBox();
            this.timerAnimacion = new System.Windows.Forms.Timer(this.components);
            this.lblP1 = new System.Windows.Forms.Label();
            this.lblP0 = new System.Windows.Forms.Label();
            this.panelDibujo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarT);
            this.groupBox1.Controls.Add(this.btnPlayPause);
            this.groupBox1.Controls.Add(this.lblValorT);
            this.groupBox1.Controls.Add(this.lblTitulo);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblVelocidad);
            this.groupBox1.Controls.Add(this.trackBarVelocidad);
            this.groupBox1.Controls.Add(this.chkMostrarLineas);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(24, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 371);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controles";
            // 
            // trackBarT
            // 
            this.trackBarT.Location = new System.Drawing.Point(6, 77);
            this.trackBarT.Name = "trackBarT";
            this.trackBarT.Size = new System.Drawing.Size(305, 56);
            this.trackBarT.TabIndex = 0;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(141, 139);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(75, 35);
            this.btnPlayPause.TabIndex = 2;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblValorT
            // 
            this.lblValorT.AutoSize = true;
            this.lblValorT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorT.Location = new System.Drawing.Point(103, 39);
            this.lblValorT.Name = "lblValorT";
            this.lblValorT.Size = new System.Drawing.Size(40, 18);
            this.lblValorT.TabIndex = 7;
            this.lblValorT.Text = "0.00";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 39);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(85, 18);
            this.lblTitulo.TabIndex = 10;
            this.lblTitulo.Text = "Valor de t:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(222, 139);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 35);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(10, 243);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(109, 18);
            this.lblVelocidad.TabIndex = 6;
            this.lblVelocidad.Text = "Velocidad: 20";
            // 
            // trackBarVelocidad
            // 
            this.trackBarVelocidad.Location = new System.Drawing.Point(9, 283);
            this.trackBarVelocidad.Name = "trackBarVelocidad";
            this.trackBarVelocidad.Size = new System.Drawing.Size(302, 56);
            this.trackBarVelocidad.TabIndex = 9;
            // 
            // chkMostrarLineas
            // 
            this.chkMostrarLineas.AutoSize = true;
            this.chkMostrarLineas.Location = new System.Drawing.Point(9, 195);
            this.chkMostrarLineas.Name = "chkMostrarLineas";
            this.chkMostrarLineas.Size = new System.Drawing.Size(214, 22);
            this.chkMostrarLineas.TabIndex = 4;
            this.chkMostrarLineas.Text = "Mostrar líneas auxiliares";
            this.chkMostrarLineas.UseVisualStyleBackColor = true;
            // 
            // lblP1
            // 
            this.lblP1.AutoSize = true;
            this.lblP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1.Location = new System.Drawing.Point(533, 88);
            this.lblP1.Name = "lblP1";
            this.lblP1.Size = new System.Drawing.Size(78, 18);
            this.lblP1.TabIndex = 5;
            this.lblP1.Text = "P1: (0, 0)";
            // 
            // lblP0
            // 
            this.lblP0.AutoSize = true;
            this.lblP0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP0.Location = new System.Drawing.Point(356, 88);
            this.lblP0.Name = "lblP0";
            this.lblP0.Size = new System.Drawing.Size(78, 18);
            this.lblP0.TabIndex = 8;
            this.lblP0.Text = "P0: (0, 0)";
            // 
            // panelDibujo
            // 
            this.panelDibujo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelDibujo.Location = new System.Drawing.Point(359, 109);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(1060, 707);
            this.panelDibujo.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Book", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(613, 44);
            this.label2.TabIndex = 15;
            this.label2.Text = "Curvas de Bézier Lineal";
            // 
            // frmLineal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1359, 857);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblP1);
            this.Controls.Add(this.panelDibujo);
            this.Controls.Add(this.lblP0);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmLineal";
            this.Text = "frmLineal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLineal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVelocidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBarT;
        private System.Windows.Forms.Timer timerAnimacion;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkMostrarLineas;
        private System.Windows.Forms.Label lblP1;
        private System.Windows.Forms.Label lblVelocidad;
        private System.Windows.Forms.Label lblValorT;
        private System.Windows.Forms.Label lblP0;
        private System.Windows.Forms.TrackBar trackBarVelocidad;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelDibujo;
        private System.Windows.Forms.Label label2;
    }
}