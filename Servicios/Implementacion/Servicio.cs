using ABMbanco.Datos.Implementacion;
using ABMbanco.Datos.Interfaz;
using ABMbanco.Servicios.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ABMbanco.Servicios.Implementacion
{
    internal class Servicio : iServicio
    {
        private iDaoCliente dao;
        
        public Servicio()//iDaoCliente dao
        {
            dao = new ClienteDao();
        }

        public int ObtenerProximo()
        {
            return dao.ObtenerProximo();
        }

        public List<TipoCuenta> ObtenerTodos() // obtengo lista de objetos se usa para: cargar combos
        {
            return dao.ObtenerTodos();
        }

    }
}
