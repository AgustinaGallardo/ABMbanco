using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco
{
    internal class TipoCuenta
    {
        private string tipo;

        public TipoCuenta(string tipo)
        {
            this.tipo = tipo;
        }

        public string pTipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public TipoCuenta()
        {
            tipo = "";
        }
        public override string ToString()
        {
            return "Tipo Cuenta: " +tipo;
        }

    }
}
