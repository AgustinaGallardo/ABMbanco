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
        SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-DBB4CIB\SQLEXPRESS;Initial Catalog=AMBbancoProgII;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        public void conectar(string sp_nombre)
        {
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sp_nombre;
        }

        public void desconectar()
        {
            conexion.Close();
        }

        public DataTable ConsultarBD(string sp_nombre)
        {
            DataTable tabla = new DataTable();
            conectar(sp_nombre);
            cmd.CommandText=sp_nombre;          
            tabla.Load(cmd.ExecuteReader());
            desconectar();
            return tabla;
        }
        public int accesoDatos(string sp_nombre)
        {
            int filasAfectadas = 0;
            conectar(sp_nombre);
           
            filasAfectadas=cmd.ExecuteNonQuery();
            desconectar();
            return filasAfectadas;
        }
        private static void AddSqlParameter(SqlCommand command)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Description";
            parameter.IsNullable = true;
            parameter.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(parameter);
        }
        

        //cmd.Parameters.AddWithValue("@nombre_parametro", objeto);

        /*
         * Asi se agregan parametros de salida:

        SqlParameter output1 = new SqlParameter();
        output1.ParameterName = "nombreParametroSalida";
        output1.Value = "valor";
        output1.Direction= ParameterDirection.Output;
        cmd.Parameters.Add(output1);
        */



    }
}
