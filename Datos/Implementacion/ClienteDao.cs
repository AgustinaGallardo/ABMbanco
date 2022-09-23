using ABMbanco.Datos.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco.Datos.Implementacion
{
    internal class ClienteDao : iDaoCliente
    {
        public int ObtenerProximo()
        {
            string sp = "ProximoCliente";
            string nombreOutPut = "@Next";
            return Helper.ObtenerInstancia().ObtenerProximo(sp, nombreOutPut);
        }
        //obtener todos
        
        public List<TipoCuenta> ObtenerTodos() // para cargar combos
        {
            List<TipoCuenta> lst = new List<TipoCuenta>();
            string sp = "cboTiposCuentas";
            DataTable table = Helper.ObtenerInstancia().ConsultarBD(sp, null);
            foreach (DataRow dr in table.Rows)
            {
                //Mapear un registro de la table a un objeto de dominio
                int id = int.Parse(dr ["id_tipoCuenta"].ToString()); //nombre de la col de sql
                string tipo = dr ["nombre"].ToString(); //nombre de la col de sql
                TipoCuenta aux = new TipoCuenta(id, tipo);
                lst.Add(aux);
            }
            return lst;
        }
    }
}
