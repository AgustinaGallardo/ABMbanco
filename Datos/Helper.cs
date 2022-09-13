using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ABMbanco
{
    internal class Helper
    {
        private static Helper instancia;
        SqlConnection cnn = new SqlConnection(Properties.Resources.cnnBanco);
        SqlCommand cmd = new SqlCommand();
        
        public void conectar()
        {         
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cnn.Open();
        }

        public static Helper ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new Helper();
            return instancia;
        }

        public DataTable ConsultarSQL(string nombreSP)
        {
            SqlCommand cmdConsulta = new SqlCommand();
            DataTable table = new DataTable();
            cnn.Open();
            cmdConsulta.Connection = cnn;
            cmdConsulta.CommandType = CommandType.StoredProcedure;
            cmdConsulta.CommandText = nombreSP;
            table.Load(cmdConsulta.ExecuteReader());
            cnn.Close();
            return table;

        }
        public DataTable ConsultarBD(string sp_nombre,List<Parametros>values)
        {
            DataTable tabla = new DataTable();
            cnn.Open();
            SqlCommand cmd = new SqlCommand(sp_nombre, cnn);
            cmd.CommandType= CommandType.StoredProcedure; 
            if(values != null)
            {
                foreach(Parametros oParametro in values)
                {
                    cmd.Parameters.AddWithValue(oParametro.Clave, oParametro.Valor);
                }
            }
            
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }

        public int EjecutarBD(string strSql, List<Parametros> values)
        {
            int afectadas = 0;
            SqlTransaction t = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strSql;
                cmd.Transaction = t;

                if (values != null)
                {
                    foreach (Parametros param in values)
                    {
                        cmd.Parameters.AddWithValue(param.Clave, param.Valor);
                    }
                }

                afectadas = cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null) { t.Rollback(); }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

            }

            return afectadas;
        }
        public int ProximoCliente(string sp_nombre)
        {
            conectar();
            cmd.CommandText=sp_nombre;
            SqlParameter OutPut=new SqlParameter();
            OutPut.ParameterName = "@Next";
            OutPut.DbType = DbType.Int32;
            OutPut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(OutPut);
            cmd.ExecuteNonQuery();
            cnn.Close();
            return (int)OutPut.Value;

        }      

        public bool ConfirmarCliente(Clientes c)
        {
            bool ok = true;

            SqlTransaction t = null;

            try
            {
                conectar();
                t = cnn.BeginTransaction();
                cmd.Parameters.Clear();
                cmd.CommandText = "insertCliente";

                cmd.Transaction=t;

                cmd.Parameters.AddWithValue("@apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@dni", c.Dni);

                SqlParameter OutPut = new SqlParameter();
                OutPut.ParameterName = "@cod_cliente";
                OutPut.DbType = DbType.Int32;
                OutPut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(OutPut);
                cmd.ExecuteNonQuery();
               

                int cod_cliente = (int)OutPut.Value;

                SqlCommand cmdDetalle;
                
                foreach (Cuenta item in c.Cuentas)
                {
                    cmdDetalle = new SqlCommand("insertCuenta", cnn, t);
                   
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@cbu",item.Cbu);
                    cmdDetalle.Parameters.AddWithValue("@id_tipoCuenta", Int32.Parse(item.tipoCuenta.pTipo));
                    cmdDetalle.Parameters.AddWithValue("@saldo", item.Saldo);
                    cmdDetalle.Parameters.AddWithValue("@ultimomovimiento", item.UltimoMovimiento);
                    cmdDetalle.Parameters.AddWithValue("@cod_cliente",cod_cliente);
                   
                    cmdDetalle.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch (Exception )
            {
                if (t != null)
                {
                    t.Rollback();
                    ok=false;
                }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;
        }
        public bool ModificarCuentas(Clientes c)
        {
            bool ok = true;
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_modificar_clientes";
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Dni", c.Dni);
                cmd.ExecuteNonQuery();

                SqlCommand cmdDetalle;
                int detalleNro = 1;

                foreach (Cuenta item in c.Cuentas)
                {
                    cmdDetalle = new SqlCommand("insertCuenta", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@cbu",item.Cbu);
                    cmdDetalle.Parameters.AddWithValue("@saldo", item.Saldo);
                    cmdDetalle.Parameters.AddWithValue("@ultimoMov", item.UltimoMovimiento);
                    cmdDetalle.Parameters.AddWithValue("@id_cliente",c.IdCliente);
                    cmdDetalle.Parameters.AddWithValue("@id_tipoC",item.tipoCuenta.ToString());
                    cmdDetalle.ExecuteNonQuery();
                    
                    detalleNro++;
                }                
                t.Commit();
            }
            catch (Exception)
            {
                if(t != null)
                    t.Rollback();
                ok=false;
            }
            finally
            {
                if(cnn != null && cnn.State ==ConnectionState.Open)
                    cnn.Close();
            }
            return ok;
        }




        
        
    }
}
