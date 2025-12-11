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
    public partial class frmLineal : Form
    {
        private static frmLineal instancia;
        public frmLineal()
        {
            InitializeComponent();
        }

        public static frmLineal ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new frmLineal();
            }
            return instancia;
        }
    }
}
