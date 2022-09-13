using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABMbanco.Reporte;

namespace ABMbanco.Formulario
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void nuevaCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
         frmCuentas nuevoFrmCuentas = new frmCuentas();
            nuevoFrmCuentas.ShowDialog();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmConsultarCuentas frmConsulta = new frmConsultarCuentas();
            frmConsulta.ShowDialog();
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReporte reporte = new frmReporte();
            reporte.ShowDialog();
        }
    }
}
