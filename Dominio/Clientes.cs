using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco
{
    internal class Clientes
    {
        private int cliente;
        private string nombre;
        private string apellido;
        private int dni;
      
        public int Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }

        public Clientes()
        {
            this.cliente = 0;
            this.nombre = "";
            this.apellido = "";
            this.dni = 0;
        }
        public Clientes(int cliente, string nombre, string apellido, int dni, Cuenta cuenta)
        {
            this.cliente = cliente;
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni= dni;
        }
        public override string ToString()
        {
            return  "Cliente: " + this.nombre + ", " + this.apellido + " dni: " + 
                this.dni  ;
        }
    }
}
