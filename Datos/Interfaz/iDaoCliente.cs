using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco.Datos.Interfaz
{
    internal interface iDaoCliente
    {
        int ObtenerProximo();

       List<TipoCuenta> ObtenerTodos();
    }
}
