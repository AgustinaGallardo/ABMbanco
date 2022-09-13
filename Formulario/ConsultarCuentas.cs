using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMbanco.Formulario
{
    public partial class frmConsultarCuentas : Form
    {
        public frmConsultarCuentas()
        {
            InitializeComponent();
        }

        private void gbFiltros_Enter(object sender, EventArgs e)
        {

        }

        private void frmConsultarCuentas_Load(object sender, EventArgs e)
        {
            dtpFechaBaja.Value = DateTime.Now;

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string sp_nombre = "sp_consultarCuenta";

            List<Parametros> lst = new List<Parametros>();

            lst.Add(new Parametros ("@apellidoCliente", txtApellido.Text));
            lst.Add(new Parametros("@nombreCliente", txtNombre.Text));
            lst.Add(new Parametros("@dni",txtDni.Text));
                        
            dgvResultados.Rows.Clear();

            DataTable dt = Helper.ObtenerInstancia().ConsultarBD(sp_nombre,lst);

            foreach (DataRow fila in dt.Rows)
            {
                dgvResultados.Rows.Add(new object[]
                {
                    fila[0].ToString(),
                    fila[1].ToString(),
                    fila[2].ToString(),
                    fila[3].ToString()        });
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que desea quitar la cuenta seleccionada?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dgvResultados.CurrentRow != null)
                {
                    int cbu = int.Parse(dgvResultados.CurrentRow.Cells["CBU"].Value.ToString());
                    List<Parametros> lst = new List<Parametros>();
                    lst.Add(new Parametros("@cbu", cbu));

                    int afectadas = Helper.ObtenerInstancia().EjecutarBD("SP_ELIMINAR_CUENTA", lst);
                    if (afectadas == 1)
                    {
                        MessageBox.Show("Se quito la cuenta!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnConsultar_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("La cuenta NO se quitó exitosamente!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}

