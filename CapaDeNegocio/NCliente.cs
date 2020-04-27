using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos;
using System.Data;
using CapaDeNegocio;

namespace CapaDeNegocio
{
    public class NCliente
    {


        public static DataTable Mostrar()
        {
            return new DCliente().Mostrar();

        }

        public static string Insertar(string Nombre,string Apellido ,int Dni,string Direccion,string Telefono)
        {

            DCliente Obj = new DCliente();
            Obj.Direccion = Direccion;
            Obj.Apellido = Apellido;
            Obj.Dni = Dni;
            Obj.Telefono = Telefono;
            Obj.Nombre = Nombre;
            return Obj.Insertar(Obj);
        }

        public static string Editar(int idcliente, string Nombre, string Apellido, int Dni, string Direccion, string Telefono)
        {

            DCliente Obj = new DCliente();
            Obj.Id = idcliente;
            Obj.Direccion = Direccion;
            Obj.Nombre = Nombre;
            Obj.Telefono = Telefono;
            Obj.Dni = Dni;
            Obj.Apellido = Apellido;
            return Obj.Editar(Obj);
        }
        public static string Eliminar(int idcliente)
        {

            DCliente Obj = new DCliente();
            Obj.Id = idcliente;
            return Obj.Eliminar(Obj);
        }
        public static DataTable BuscarTexto(string seleccion,string textobuscar)
        {
            if (seleccion.Equals("Dni"))
            {
                DCliente Obj = new DCliente();
                Obj.TextoBuscar = textobuscar;
                return Obj.BuscarDNI(Obj);
            }
            else if (seleccion.Equals("Nombre"))
            {
                DCliente Obj = new DCliente();
                Obj.TextoBuscar = textobuscar;
                return Obj.BuscarNombre(Obj);
            }
            else if (seleccion.Equals("Apellido"))
            {
                DCliente Obj = new DCliente();
                Obj.TextoBuscar = textobuscar;
                return Obj.BuscarApellido(Obj);
            }
            else if (seleccion.Equals("Telefono"))
            {
                DCliente Obj = new DCliente();
                Obj.TextoBuscar = textobuscar;
                return Obj.BuscarTelefono(Obj);

            }
            else {
                return null;
            }

        }
    }
}
