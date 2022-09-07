﻿using System;
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
        
        public void conectar()
        {         
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cnn.Open();
        }
       
        public DataTable ConsultarBD(string sp_nombre)
        {
            DataTable tabla = new DataTable();
            conectar();
            cmd.CommandText=sp_nombre;  
            
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
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
        
        
    }
}
