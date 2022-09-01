﻿using System;
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
        Helper helper = new Helper();   
        List<Clientes> lClientes = new List<Clientes>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cargarCombo();
            cargarDGV();
            Limpiar();
            Habilitar(false);
            cargarProximoCliente();
        }

        private void cargarProximoCliente()
        {
            int next = helper.ProximoCliente("sp_ProximoCiente");
            if (next >1)
            
                lblProximoCliente.Text = "Cliente Nro: " + next.ToString();
              else
                    MessageBox.Show("Error de datos. No se puede obtener Nº de Cliente!",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        private void Habilitar(bool v)
        {
            txtApellido.Enabled = v;
            txtNombre.Enabled = v;
            txtDni.Enabled = v;
            txtSaldo.Enabled =v;
            txtcbu.Enabled = v;
            txtUltimoMov.Enabled = v;
            cboTipoCuenta.Enabled = v;
            btnBorrar.Enabled = v;
            btnGrabar.Enabled = v;
            btnNuevo.Enabled =!v;
            btnSalir.Enabled = !v;
        }
        private void Limpiar()
        {
            txtApellido.Text="";
            txtNombre.Text="";
            txtDni.Text="";
            txtSaldo.Text="";
            txtcbu.Text="";
            cboTipoCuenta.SelectedIndex=-1;
            txtUltimoMov.Text="";
        }
        private void cargarDGV()
        {           
          
        }
        private void cargarCombo()
        {
            DataTable tabla = helper.ConsultarBD("sp_comboTiposCuentas");
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
            if (dgvClientes.Rows.Count != 0)
            {
                txtApellido.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                txtDni.Text = (dgvClientes.CurrentRow.Cells[2].Value.ToString());                
                txtSaldo.Text= dgvClientes.CurrentRow.Cells[3].Value.ToString();
                txtUltimoMov.Text=dgvClientes.CurrentRow.Cells[4].Value.ToString();
            }
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                Cuenta c = new Cuenta();
                Clientes cl = new Clientes();

                cl.Apellido=Convert.ToString(txtApellido.Text);
                cl.Nombre=Convert.ToString(txtNombre.Text);
                cl.Dni=Convert.ToInt32(txtDni.Text);
                c.Cbu=Convert.ToInt32(txtcbu.Text);
               // c.TipoCuenta=Convert.ToInt32(cboTipoCuenta.SelectedValue);
                c.Saldo=Convert.ToDouble(txtSaldo.Text);
                c.UltimoMovimiento=Convert.ToDateTime(txtUltimoMov.Text);

                string sp_nombre = "InsertarClientes";

                if(helper.actualidarBD(sp_nombre,cl,c) > 0)
                {
                    MessageBox.Show("Se agrego con exito!!!");
                    dgvClientes.Rows.Add(new object[] {
                    cl.Apellido,
                    cl.Nombre,
                    cl.Dni,
                    c.Saldo,
                    c.UltimoMovimiento
                });
                }
                Limpiar();
            }
        }
        private bool validar()
        {
            if(txtApellido.Text=="")
            {
                MessageBox.Show("Tiene que agregar el apellido del cliente!!!");
                txtApellido.Focus();
                return false;
            }
            if(txtNombre.Text=="")
            {
                MessageBox.Show("Tiene que agregar el nombre del cliente!!!");
                txtNombre.Focus();
                return false;
            }
            if(txtDni.Text=="")
            {
                MessageBox.Show("Tiene que agregar el dni del cliente!!!");
                txtDni.Focus();
                return false;
            }
            if(txtSaldo.Text=="")
            {
                MessageBox.Show("Tiene que agregar el saldo del cliente!!!");
                txtSaldo.Focus();
                return false;
            }
            if(txtUltimoMov.Text=="")
            {
                MessageBox.Show("Tiene que agregar el ultimo movimiento del cliente!!!");
                txtUltimoMov.Focus();
                return false;
            }
            if(cboTipoCuenta.SelectedIndex==-1)
            {
                MessageBox.Show("Tiene que seleccionar el tipo de cuenta del cliente!!!");
                cboTipoCuenta.Focus();
                return false;
            }
            return true;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            btnSalir.Enabled = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Cuenta c = new Cuenta();
                Clientes cl = new Clientes();

                cl.Apellido=Convert.ToString(txtApellido.Text);
                cl.Nombre=Convert.ToString(txtNombre.Text);
                cl.Dni=Convert.ToInt32(txtDni.Text);
                c.Cbu=Convert.ToInt32(txtcbu.Text);
                c.Saldo=Convert.ToDouble(txtSaldo.Text);
                c.UltimoMovimiento=Convert.ToDateTime(txtUltimoMov.Text);


                dgvClientes.Rows.Add(new object[] {
                    cl.Apellido,
                    cl.Nombre,
                    cl.Dni,
                    c.Saldo,
                    c.UltimoMovimiento
                });
            }

        }
    }
}
