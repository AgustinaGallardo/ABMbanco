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
        SqlConnection cnn = new SqlConnection(Properties.Resources.cnnBanco);
        SqlCommand cmd = new SqlCommand();
        
        public void conectar(string sp_nombre)
        {         
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText=sp_nombre;
            cnn.Open();
        }
        public void desconectar()
        {
            cnn.Close();
        }
        public DataTable ConsultarBD(string sp_nombre)
        {
            DataTable tabla = new DataTable();
            conectar(sp_nombre);
               
            
            tabla.Load(cmd.ExecuteReader());
            desconectar();

            return tabla;
        }
        public int ProximoCliente(string sp_nombre)
        {
            conectar(sp_nombre);
            cmd.CommandText=sp_nombre;
            SqlParameter OutPut=new SqlParameter();
            OutPut.ParameterName = "@Next";
            OutPut.DbType = DbType.Int32;
            OutPut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(OutPut);
            cmd.ExecuteNonQuery();
            desconectar();
            return (int)OutPut.Value;

        }
        public int actualidarBD(string sp_nombre, Clientes cl, Cuenta c)
        {
            SqlTransaction t = null;

            bool ok = true;
            int filasAfectadas = 0;
            try
            {
                conectar(sp_nombre);
                t = cnn.BeginTransaction();

                cmd.CommandText = sp_nombre;

                cmd.Transaction=t;

                cmd.Parameters.AddWithValue("@apellido", cl.Apellido);
                cmd.Parameters.AddWithValue("@nombre", cl.Nombre);
                cmd.Parameters.AddWithValue("@dni", cl.Dni);
                cmd.Parameters.AddWithValue("@cbu", c.Cbu);
                cmd.Parameters.AddWithValue("@saldo", c.Saldo);
                cmd.Parameters.AddWithValue("@ultimomovimiento", c.UltimoMovimiento);
                

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
