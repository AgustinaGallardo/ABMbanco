﻿using System;
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
        private double ultimoMovimiento;        
        private int tipoCuenta;
        private Clientes cliente;

        public Clientes Clientes
        { get { return cliente; } set { cliente = value; } }
        public int Cbu
        { get { return cbu; } set { cbu = value; } }
        public double Saldo
        { get { return saldo; } set { saldo = value; } }
        public double UltimoMovimiento
        { get { return ultimoMovimiento; } set { ultimoMovimiento = value; } }  
        public int TipoCuenta
        { get { return tipoCuenta; } set { tipoCuenta = value; } }
        public Cuenta (int cbu, double saldo, double ultimoMovimiento, int tipoCuenta,Clientes cliente)
        {
            this.cbu = cbu;
            this.saldo = saldo;
            this.ultimoMovimiento=ultimoMovimiento;
            this.tipoCuenta=tipoCuenta;
            this.Clientes = cliente;
        }
        public Cuenta   ()
        {
            this.cbu=0;
            this.Saldo=0;
            this.ultimoMovimiento=0;
            this.tipoCuenta=0;
            this.cliente=cliente;
        }
        public override string ToString()
        {
            return "cbu: " + this.cbu + ", Saldo: " + this.saldo + "$, Cliente: " + this.cliente.ToString();
        }
    }


    
}