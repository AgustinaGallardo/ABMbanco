using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco
{
    internal class Banco
    {
        private Cuenta cuenta;      
        public Cuenta  Cuenta
        { get { return this.cuenta; } set { this.cuenta = value; } }
        public Banco (Cuenta cuenta)
        {  
            this.cuenta=cuenta;
        }
        public Banco()
        {           
            this.cuenta=new Cuenta();   
        }
        public override string ToString()
        {
            return cuenta.ToString();
        }
    }
}
