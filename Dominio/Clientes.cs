using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMbanco
{
    internal class Clientes
    {
        private int idCliente;
        private string nombre;
        private string apellido;
        private int dni;
        
        public List<Cuenta> Cuentas { get; set; }

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
       

        public Clientes()
        {
            this.IdCliente = 0;
            this.nombre = "";
            this.apellido = "";
            this.dni = 0;
            Cuentas = new List<Cuenta>();


        }
        public Clientes(int cliente, string nombre, string apellido, int dni)
        {
            this.idCliente = cliente;
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni= dni;
          
        }

        public void AgregarCuenta(Cuenta nueva)
        {
            Cuentas.Add(nueva);
        }
        public void EliminarCuenta(int cbu)
        {
            Cuentas.RemoveAt(cbu);
        }
        public override string ToString()
        {
            return  "Cliente: " + this.nombre + ", " + this.apellido + " dni: " + 
                this.dni  ;
        }
    }
}
