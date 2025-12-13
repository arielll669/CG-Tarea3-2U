using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace curvasBzierBspline
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        // Método de ayuda para simplificar el código
        private void ShowSingletonForm(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized)
            {
                form.WindowState = FormWindowState.Normal;
            }
            form.Show();
            form.BringToFront();
        }

        private void linealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLineal form1 = frmLineal.ObtenerInstancia();
            ShowSingletonForm(form1);
        }

        private void cuadraticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCuadratica form2 = frmCuadratica.ObtenerInstancia();
            ShowSingletonForm(form2);
        }

        private void cubicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCubica form3 = frmCubica.ObtenerInstancia();
            ShowSingletonForm(form3);
        }

        private void gradoNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGradoN form4 = frmGradoN.ObtenerInstancia();
            ShowSingletonForm(form4);
        }
    }
}
