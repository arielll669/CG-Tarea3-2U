namespace curvasBzierBspline
{
    partial class home
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bezierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuadraticaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cubicaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradoNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bsplineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cruvaBsplineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bezierToolStripMenuItem,
            this.bsplineToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bezierToolStripMenuItem
            // 
            this.bezierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linealToolStripMenuItem,
            this.cuadraticaToolStripMenuItem,
            this.cubicaToolStripMenuItem,
            this.gradoNToolStripMenuItem});
            this.bezierToolStripMenuItem.Name = "bezierToolStripMenuItem";
            this.bezierToolStripMenuItem.Size = new System.Drawing.Size(64, 26);
            this.bezierToolStripMenuItem.Text = "Bezier";
            // 
            // linealToolStripMenuItem
            // 
            this.linealToolStripMenuItem.Name = "linealToolStripMenuItem";
            this.linealToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.linealToolStripMenuItem.Text = "Lineal";
            this.linealToolStripMenuItem.Click += new System.EventHandler(this.linealToolStripMenuItem_Click);
            // 
            // cuadraticaToolStripMenuItem
            // 
            this.cuadraticaToolStripMenuItem.Name = "cuadraticaToolStripMenuItem";
            this.cuadraticaToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.cuadraticaToolStripMenuItem.Text = "Cuadratica";
            this.cuadraticaToolStripMenuItem.Click += new System.EventHandler(this.cuadraticaToolStripMenuItem_Click);
            // 
            // cubicaToolStripMenuItem
            // 
            this.cubicaToolStripMenuItem.Name = "cubicaToolStripMenuItem";
            this.cubicaToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.cubicaToolStripMenuItem.Text = "Cubica";
            this.cubicaToolStripMenuItem.Click += new System.EventHandler(this.cubicaToolStripMenuItem_Click);
            // 
            // gradoNToolStripMenuItem
            // 
            this.gradoNToolStripMenuItem.Name = "gradoNToolStripMenuItem";
            this.gradoNToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.gradoNToolStripMenuItem.Text = "Grado N";
            this.gradoNToolStripMenuItem.Click += new System.EventHandler(this.gradoNToolStripMenuItem_Click);
            // 
            // bsplineToolStripMenuItem
            // 
            this.bsplineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cruvaBsplineToolStripMenuItem});
            this.bsplineToolStripMenuItem.Name = "bsplineToolStripMenuItem";
            this.bsplineToolStripMenuItem.Size = new System.Drawing.Size(77, 26);
            this.bsplineToolStripMenuItem.Text = "B-spline";
            // 
            // cruvaBsplineToolStripMenuItem
            // 
            this.cruvaBsplineToolStripMenuItem.Name = "cruvaBsplineToolStripMenuItem";
            this.cruvaBsplineToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.cruvaBsplineToolStripMenuItem.Text = "Cruva Bspline";
            this.cruvaBsplineToolStripMenuItem.Click += new System.EventHandler(this.cruvaBsplineToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 75);
            this.label1.TabIndex = 1;
            this.label1.Text = "Curvas Bézier  Curva B-Spline";
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "home";
            this.Text = "Curvas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bezierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bsplineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linealToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuadraticaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cubicaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradoNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cruvaBsplineToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

