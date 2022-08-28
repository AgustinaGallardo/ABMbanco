using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMbanco
{
    public partial class Form1 : Form
    {
        AccesoDatos oBD = new AccesoDatos();
        List<Clientes> lClientes = new List<Clientes>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarCombo();
            cargarLista();
        }

        private void cargarLista()
        {
            lstClientes.Items.Clear();

            dgvClientes.Rows.Clear();

            DataTable tabla = oBD.ConsultarBD("cargarDGV");

            for(int i = 0; i < tabla.Rows.Count; i++)
            {
                Banco b = new Banco();

                b.Cuenta.Clientes.Nombre=Convert.ToString(tabla.Rows[i][0]);
                b.Cuenta.Clientes.Apellido=Convert.ToString(tabla.Rows[i][1]);
                b.Cuenta.Clientes.Dni=Convert.ToInt32(tabla.Rows [i][2]);
                b.Cuenta.TipoCuenta=Convert.ToInt32(tabla.Rows[i][3]);
                b.Cuenta.Saldo=Convert.ToDouble(tabla.Rows[i][4]);
                b.Cuenta.UltimoMovimiento=Convert.ToDouble(tabla.Rows[i][5]);                

                //lClientes.Add(b);
                //lstClientes.Items.Add(b);             
                dgvClientes.Rows.Add(new object[] {
                    b.Cuenta.Clientes.Nombre,
                    b.Cuenta.Clientes.Apellido,
                    b.Cuenta.Clientes.Dni,
                    b.Cuenta.TipoCuenta,
                    b.Cuenta.Saldo,
                    b.Cuenta.UltimoMovimiento
                });
            }            
        }
        private void cargarCombo()
        {
            DataTable tabla = oBD.ConsultarBD("SP_tiposCuentas");
            cboTipoCuenta.DataSource = tabla;
            cboTipoCuenta.ValueMember =tabla.Columns[0].ColumnName;
            cboTipoCuenta.DisplayMember=tabla.Columns[1].ColumnName;
            cboTipoCuenta.DropDownStyle=ComboBoxStyle.DropDownList;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void lstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro quiere salir de la aplicacion??", "SALIR",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button2)==DialogResult.Yes)
                Close();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvClientes.Rows.Count != 0 )
            {
                txtApellido.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                txtDni.Text = (dgvClientes.CurrentRow.Cells[2].Value.ToString());
                cboTipoCuenta.SelectedValue = dgvClientes.CurrentRow.Cells[3];
                txtSaldo.Text= dgvClientes.CurrentRow.Cells[4].Value.ToString();
                txtUltimoMov.Text=dgvClientes.CurrentRow.Cells[5].Value.ToString();

            }
        }
    }
}
