using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco
{
    internal class TipoCuenta
    {
        private int id;
        private string tipo;

        public TipoCuenta(int id,string tipo)
        {
            this.tipo = tipo;
            this.id = id;
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public int Id 
        { 
            get { return id; }
            set { id = value; }
        }
        public TipoCuenta()
        {  
            id = 0;
            tipo = "";
         
        }
        public override string ToString()
        {
            return "Tipo Cuenta: " +tipo ;
        }

    }
}
