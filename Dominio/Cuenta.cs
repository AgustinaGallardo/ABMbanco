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
        public List<Cuenta> Cuentas { get; set; }

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
            Cuentas = new List<Cuenta>();

        }
        public override string ToString()
        {
            return "cbu: " + this.cbu + ", Saldo: " + this.saldo + "$";
        }
    }


    
}
