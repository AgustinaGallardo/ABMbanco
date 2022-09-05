using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco
{
    internal class Cuenta
    {
        private int cbu;
        private double saldo;
        private DateTime ultimoMovimiento;
        public TipoCuenta tipoCuenta;

        public int Cbu
        { get { return cbu; } set { cbu = value; } }
        public double Saldo
        { get { return saldo; } set { saldo = value; } }
        public DateTime UltimoMovimiento
        { get { return ultimoMovimiento; } set { ultimoMovimiento = value; } }  
        
        public Cuenta   ()
        {
            this.cbu=0;
            this.Saldo=0;
            this.ultimoMovimiento=DateTime.Today;
            tipoCuenta = new TipoCuenta ();
        }
        public override string ToString()
        {
            return "cbu: " + this.cbu + ", Saldo: " + this.saldo + "$";
        }
    }


    
}
