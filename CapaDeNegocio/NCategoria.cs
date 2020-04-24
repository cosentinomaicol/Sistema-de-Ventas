using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos;
using System.Data;
namespace CapaDeNegocio
{
    public class NCategoria
    {
        //Creo los metodos necesarios para comunicarme con la capa de Datos
        public static string Insertar(string descripcion) {

            DCategoria Obj = new DCategoria();
            Obj.Descripcion = descripcion;
            return Obj.Insertar(Obj);
        }

        public static string Editar(int idcategoria,string descripcion)
        {

            DCategoria Obj = new DCategoria();
            Obj.Id = idcategoria;
            Obj.Descripcion = descripcion;
            return Obj.Editar(Obj);
        }
        public static string Eliminar(int idcategoria)
        {

            DCategoria Obj = new DCategoria();
            Obj.Id = idcategoria;
            return Obj.Eliminar(Obj);
        }
        public static DataTable Mostrar() {
            return new DCategoria().Mostrar();

        }

        public static DataTable BuscarTexto(string textobuscar) {
            DCategoria Obj = new DCategoria();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarElemento(Obj);

        }
    }
}
