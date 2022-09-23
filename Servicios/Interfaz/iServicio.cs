using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco.Servicios
{
    internal interface iServicio
    {
        int ObtenerProximo();

        List<TipoCuenta> ObtenerTodos();
    }
}
