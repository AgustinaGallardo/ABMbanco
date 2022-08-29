using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ABMbanco
{
    internal class AccesoDatos
    {
        SqlConnection conexion = new SqlConnection(Properties.Resources.String1);
        SqlCommand cmd = new SqlCommand();
        

        public void conectar(string sp_nombre)
        {         
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText=sp_nombre;
            conexion.Open();
        }

        public void desconectar()
        {
            conexion.Close();
        }

        public DataTable ConsultarBD(string sp_nombre)
        {
            DataTable tabla = new DataTable();
            conectar(sp_nombre);
               
            
            tabla.Load(cmd.ExecuteReader());
            desconectar();

            return tabla;
        }
        public int actualidarBD(string sp_nombre, Clientes cl, Cuenta c)
        {
            SqlTransaction t = null;

            bool ok = true;
            int filasAfectadas = 0;
            try
            {
                conectar(sp_nombre);
                t = conexion.BeginTransaction();

                cmd.CommandText = sp_nombre;

                cmd.Transaction=t;

                cmd.Parameters.AddWithValue("@apellido", cl.Apellido);
                cmd.Parameters.AddWithValue("@nombre", cl.Nombre);
                cmd.Parameters.AddWithValue("@dni", cl.Dni);
                cmd.Parameters.AddWithValue("@cbu", c.Cbu);
                cmd.Parameters.AddWithValue("@saldo", c.Saldo);
                cmd.Parameters.AddWithValue("@ultimomovimiento", c.UltimoMovimiento);
                cmd.Parameters.AddWithValue("id_tipo_cuenta", c.TipoCuenta);

                filasAfectadas=cmd.ExecuteNonQuery();
                
                t.Commit();
                desconectar();

            }

            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                    ok=false;
                }
            }
            return filasAfectadas;

        }
    }
}
