using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class DProducto
    {
        SqlCommand SqlCmd;
        Conexion conexion;
        private int _Id;
        private int _CategoriaId;
        private string _Nombre;
        private string _Descripcion; 
        private double _Stock;
        private double _PrecioCompra;
        private double _PrecioVenta;
        private DateTime _FechaVencimiento;
        private DateTime _FechaCreado; 
        private byte[]   _imagen;

        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public int CategoriaId
        {
            get
            {
                return _CategoriaId;
            }

            set
            {
                _CategoriaId = value;
            }
        }
        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
            }
        }

        public double Stock
        {
            get
            {
                return _Stock;
            }

            set
            {
                _Stock = value;
            }
        }

        public double PrecioCompra
        {
            get
            {
                return _PrecioCompra;
            }

            set
            {
                _PrecioCompra = value;
            }
        }

        public double PrecioVenta
        {
            get
            {
                return _PrecioVenta;
            }

            set
            {
                _PrecioVenta = value;
            }
        }

        public DateTime  FechaVencimiento
        {
            get
            {
                return _FechaVencimiento;
            }

            set
            {
                _FechaVencimiento = value;
            }
        }

        public DateTime FechaCreado
        {
            get
            {
                return _FechaCreado;
            }

            set
            {
                _FechaCreado = value;
            }
        }

        public byte[] imagen
        {
            get
            {
                return _imagen;
            }

            set
            {
                _imagen = value;
            }
        }

        //Constructor vacio

        public DProducto()
        {

        }
        //procedimiento mostrar datos productos
        public DataTable Mostrar()
        {
            string rpta;
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblProducto");
            //Creo una variable del tipo resultado para llenar la tabla
            try
            {
                conexion = new Conexion();
                rpta = conexion.Conectado();
                if (rpta == "OK")
                {

                    //abro conexion a la base de datos
                    SqlCommand SqlCmd = new SqlCommand();
                    //Creo un comando para comunicarme con SQLServer
                    SqlCmd.Connection = conexion.Cn;
                    SqlCmd.CommandText = "spmostrar_producto";
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure

                    SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                    //creo un SqlAdapter para poder ejecutar el comando y llenar el DataTable
                    SqlDat.Fill(DtResultado);
                    //Lleno el Datatable
                    return DtResultado;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //Si existe algun error no obtengo resultado ninguno
                throw ex;


            }
            finally
            {
                //limpia todos los recursos que se asignan al bloque
                //siempre ejecuta finally
                conexion.Desconectado();
            }

        }

        public string Eliminar(DProducto Producto)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                conexion = new Conexion();
                rpta = conexion.Conectado();
                //Creo un comando para comunicarme con SQLServer
                SqlCommand SqlCmd = new SqlCommand();
                //Creo un comando para comunicarme con SQLServer
                SqlCmd.Connection = conexion.Cn;
                SqlCmd.CommandText = "speliminar_producto";
                //Objeto al que voy hacer referencia en mi base de datos
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //El tipo de objeto al que quiero conectarme es un stored procedure
                SqlParameter Parametros = new SqlParameter();
                //creo los parametros para pasarle al stored procedure
                Parametros.ParameterName = "@Idproducto";
                //nombre del parametro
                Parametros.SqlDbType = SqlDbType.Int;
                //El parametro es de tipo entero

                Parametros.Value = Producto.Id;
                SqlCmd.Parameters.Add(Parametros);




                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se elimino el registro";

            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {
                conexion.Desconectado();
            }
            return rpta;
        }

        public string Editar(DProducto Producto)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                conexion = new Conexion();
                rpta = conexion.Conectado();
                if (rpta == "OK")
                {
                    SqlCommand SqlCmd = new SqlCommand();
                    //Creo un comando para comunicarme con SQLServer
                    SqlCmd.Connection = conexion.Cn;
                    SqlCmd.CommandText = "speditar_producto";
                    //Objeto al que voy hacer referencia en mi base de datos
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter Parametros = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    Parametros.ParameterName = "@Id";
                    //nombre del parametro
                    Parametros.SqlDbType = SqlDbType.Int;
                    //El parametro es de tipo entero

                    Parametros.Value = Producto.Id;
                    SqlCmd.Parameters.Add(Parametros);

                    SqlParameter ParIdCategoria = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParIdCategoria.ParameterName = "@CategoriaId";
                    //nombre del parametro
                    ParIdCategoria.SqlDbType = SqlDbType.Int;
                    //El parametro es de tipo entero

                    ParIdCategoria.Value = Producto.CategoriaId;
                    SqlCmd.Parameters.Add(ParIdCategoria);

                    SqlParameter ParNombre = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParNombre.ParameterName = "@Nombre";
                    //nombre del parametro
                    ParNombre.SqlDbType = SqlDbType.VarChar;
                    ParNombre.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParNombre.Value = Producto.Nombre;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParNombre);

                    SqlParameter ParDescripcion = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParDescripcion.ParameterName = "@Descripcion";
                    //nombre del parametro
                    ParDescripcion.SqlDbType = SqlDbType.VarChar;
                    ParDescripcion.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParDescripcion.Value = Producto.Descripcion;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParDescripcion);

                    SqlParameter ParStock = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParStock.ParameterName = "@Stock";
                    //nombre del parametro
                    ParStock.SqlDbType = SqlDbType.Decimal;

                    //tamaño de caracteres que soporta la variable
                    ParStock.Value = Producto.Stock;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParStock);

                    SqlParameter ParPrecioCompra = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParPrecioCompra.ParameterName = "@PrecioCompra";
                    //nombre del parametro
                    ParPrecioCompra.SqlDbType = SqlDbType.Decimal;

                    //tamaño de caracteres que soporta la variable
                    ParPrecioCompra.Value = Producto.PrecioCompra;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParPrecioCompra);

                    SqlParameter ParPrecioVenta = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParPrecioVenta.ParameterName = "@PrecioVenta";
                    //nombre del parametro
                    ParPrecioVenta.SqlDbType = SqlDbType.Decimal;

                    //tamaño de caracteres que soporta la variable
                    ParPrecioVenta.Value = Producto.PrecioVenta;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParPrecioVenta);

                    SqlParameter ParFechaVencimiento = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParFechaVencimiento.ParameterName = "@FechaVencimiento";
                    //nombre del parametro
                    ParFechaVencimiento.SqlDbType = SqlDbType.Date;

                    //tamaño de caracteres que soporta la variable
                    ParFechaVencimiento.Value = Producto.FechaVencimiento;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParFechaVencimiento);

                    SqlParameter ParFechaCreado = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParFechaCreado.ParameterName = "@FechaCreado";
                    //nombre del parametro
                    ParFechaCreado.SqlDbType = SqlDbType.DateTime;

                    //tamaño de caracteres que soporta la variable
                    ParFechaCreado.Value = Producto.FechaCreado;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParFechaCreado);

                    SqlParameter Parimagen = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    Parimagen.ParameterName = "@imagen";
                    //nombre del parametro
                    Parimagen.SqlDbType = SqlDbType.Image;

                    //tamaño de caracteres que soporta la variable
                    Parimagen.Value = Producto.imagen;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(Parimagen);


                    rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";
                }
            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {
                //limpia todos los recursos que se asignan al bloque
                //siempre ejecuta finally
                conexion.Desconectado();

            }
            return rpta;

        }

        public string Insertar(DProducto Producto)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                conexion = new Conexion();
                rpta = conexion.Conectado();
                if (rpta == "OK")
                {
                    SqlCommand SqlCmd = new SqlCommand();
                    //Creo un comando para comunicarme con SQLServer
                    SqlCmd.Connection = conexion.Cn;
                    SqlCmd.CommandText = "spinsertar_producto";
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter Parametros = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    Parametros.ParameterName = "@CategoriaId";
                    //nombre del parametro
                    Parametros.SqlDbType = SqlDbType.Int;
                    //El parametro es de tipo entero 
                    Parametros.Value = Producto.CategoriaId;                                  
                    SqlCmd.Parameters.Add(Parametros);
                    //agrego el parametro

                    SqlParameter ParNombre = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParNombre.ParameterName = "@Nombre";
                    //nombre del parametro
                    ParNombre.SqlDbType = SqlDbType.VarChar;
                    ParNombre.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParNombre.Value = Producto.Nombre;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParNombre);

                    SqlParameter ParDescripcion = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParDescripcion.ParameterName = "@Descripcion";
                    //nombre del parametro
                    ParDescripcion.SqlDbType = SqlDbType.VarChar;
                    ParDescripcion.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParDescripcion.Value = Producto.Descripcion;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParDescripcion);

                    SqlParameter ParStock = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParStock.ParameterName = "@Stock";
                    //nombre del parametro
                    ParStock.SqlDbType = SqlDbType.Decimal;

                    //tamaño de caracteres que soporta la variable
                    ParStock.Value = Producto.Stock;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParStock);

                    SqlParameter ParPrecioCompra = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParPrecioCompra.ParameterName = "@PrecioCompra";
                    //nombre del parametro
                    ParPrecioCompra.SqlDbType = SqlDbType.Decimal;

                    //tamaño de caracteres que soporta la variable
                    ParPrecioCompra.Value = Producto.PrecioCompra;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParPrecioCompra);

                    SqlParameter ParPrecioVenta = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParPrecioVenta.ParameterName = "@PrecioVenta";
                    //nombre del parametro
                    ParPrecioVenta.SqlDbType = SqlDbType.Decimal;

                    //tamaño de caracteres que soporta la variable
                    ParPrecioVenta.Value = Producto.PrecioVenta;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParPrecioVenta);

                    SqlParameter ParFechaVencimiento = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParFechaVencimiento.ParameterName = "@FechaVencimiento";
                    //nombre del parametro
                    ParFechaVencimiento.SqlDbType = SqlDbType.Date;

                    //tamaño de caracteres que soporta la variable
                    ParFechaVencimiento.Value = Producto.FechaVencimiento;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParFechaVencimiento);

                    SqlParameter ParFechaCreado = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParFechaCreado.ParameterName = "@FechaCreado";
                    //nombre del parametro
                    ParFechaCreado.SqlDbType = SqlDbType.DateTime;

                    //tamaño de caracteres que soporta la variable
                    ParFechaCreado.Value = Producto.FechaCreado;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParFechaCreado);

                    SqlParameter Parimagen = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    Parimagen.ParameterName = "@imagen";
                    //nombre del parametro
                    Parimagen.SqlDbType = SqlDbType.Image;

                    //tamaño de caracteres que soporta la variable
                    Parimagen.Value = Producto.imagen;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(Parimagen);


                    rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";
                }

            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {
                //limpia todos los recursos que se asignan al bloque
                //siempre ejecuta finally
                conexion.Desconectado();
            }
            return rpta;


        }


    }
}
