using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos;

namespace CapaDeNegocio
{
    public class NProducto
    {
        public static DataTable Mostrar()
        {
            return new DProducto().Mostrar();

        }

        public static string Insertar(int idcategoria,string Nombre,string Descripcion, double Stock, double PrecioCompra, double PrecioVenta, DateTime FechaVencimiento, DateTime FechaCreado,byte[] imagen)
        {

            DProducto Obj = new DProducto();
            Obj.CategoriaId = idcategoria;
            Obj.Nombre = Nombre;
            Obj.Descripcion = Descripcion;
            Obj.Stock = Stock;
            Obj.PrecioCompra = PrecioCompra;
            Obj.PrecioVenta = PrecioVenta;
            Obj.FechaVencimiento = FechaVencimiento;
            Obj.FechaCreado = FechaCreado;
            Obj.imagen = imagen;
            return Obj.Insertar(Obj);
        }

        public static string Editar(int idproducto,int idcategoria, string Nombre, string Descripcion, double Stock, double PrecioCompra, double PrecioVenta, DateTime FechaVencimiento, DateTime FechaCreado, byte[] imagen)
        {

            DProducto Obj = new DProducto();
            Obj.Id = idproducto;
            Obj.CategoriaId = idcategoria;
            Obj.Nombre = Nombre;
            Obj.Descripcion = Descripcion;
            Obj.Stock = Stock;
            Obj.PrecioCompra = PrecioCompra;
            Obj.PrecioVenta = PrecioVenta;
            Obj.FechaVencimiento = FechaVencimiento;
            Obj.FechaCreado = FechaCreado;
            Obj.imagen = imagen;
            return Obj.Editar(Obj);
        }

        public static string Eliminar(int idcliente)
        {

            DProducto Obj = new DProducto();
            Obj.Id = idcliente;
            return Obj.Eliminar(Obj);
        }
    }
}
