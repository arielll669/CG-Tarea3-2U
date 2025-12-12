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
            this.bsplineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.cubicaToolStripMenuItem});
            this.bezierToolStripMenuItem.Name = "bezierToolStripMenuItem";
            this.bezierToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.bezierToolStripMenuItem.Text = "Bezier";
            // 
            // linealToolStripMenuItem
            // 
            this.linealToolStripMenuItem.Name = "linealToolStripMenuItem";
            this.linealToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.linealToolStripMenuItem.Text = "Lineal";
            this.linealToolStripMenuItem.Click += new System.EventHandler(this.linealToolStripMenuItem_Click);
            // 
            // cuadraticaToolStripMenuItem
            // 
            this.cuadraticaToolStripMenuItem.Name = "cuadraticaToolStripMenuItem";
            this.cuadraticaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.cuadraticaToolStripMenuItem.Text = "Cuadratica";
            this.cuadraticaToolStripMenuItem.Click += new System.EventHandler(this.cuadraticaToolStripMenuItem_Click);
            // 
            // cubicaToolStripMenuItem
            // 
            this.cubicaToolStripMenuItem.Name = "cubicaToolStripMenuItem";
            this.cubicaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.cubicaToolStripMenuItem.Text = "Cubica";
            // 
            // bsplineToolStripMenuItem
            // 
            this.bsplineToolStripMenuItem.Name = "bsplineToolStripMenuItem";
            this.bsplineToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.bsplineToolStripMenuItem.Text = "B-spline";
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "home";
            this.Text = "Form1";
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
    }
}

