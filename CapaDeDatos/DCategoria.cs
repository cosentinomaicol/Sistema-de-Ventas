using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class DCategoria
    {
        //En esta clase estaran todos los metodos para poder eliminar, insertar, buscar, actualizar ,mostrar datos de la tabla Categoria
        private int _Id;
        private string _Descripcion;
        private string _TextoBuscar;

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

        public string TextoBuscar
        {
            get
            {
                return _TextoBuscar;
            }

            set
            {
                _TextoBuscar = value;
            }
        }
        //Constructor Vacio
        public DCategoria() {

        }
        //Constructor con parametros
        public DCategoria(int idcategoria, string descripcion, string textobuscar) {
            this.Id = idcategoria;
            this.Descripcion = descripcion;
            this.TextoBuscar = textobuscar;

        }
        //Metodo Insertar
        public string Insertar(DCategoria Categoria) {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
          //      SqlCon.ConnectionString = Conexion.Cn;
          /////777
                //obtengo cadena de conexion , de mi clase Conexion
                SqlCon.Open();
                //abro conexion a la base de datos
                SqlCommand SqlCmd = new SqlCommand();
                //Creo un comando para comunicarme con SQLServer
                SqlCmd.Connection = SqlCon;
                //El comando le asigno la conexion establecida
                SqlCmd.CommandText = "spinsertar_categoria";
                //Objeto al que voy hacer referencia en mi base de datos
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //El tipo de objeto al que quiero conectarme es un stored procedure
                SqlParameter Parametros = new SqlParameter();
                //creo los parametros para pasarle al stored procedure
                Parametros.ParameterName = "@idcategoria";
                //nombre del parametro
                Parametros.SqlDbType = SqlDbType.Int;
                //El parametro es de tipo entero
                Parametros.Direction = ParameterDirection.Output;
                //el parametro es del tipo de salida
                SqlCmd.Parameters.Add(Parametros);
                //agrego el parametro

                SqlParameter ParDescripcion = new SqlParameter();
                //creo los parametros para pasarle al stored procedure
                ParDescripcion.ParameterName = "@descripcion";
                //nombre del parametro
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 500;
                //tamaño de caracteres que soporta la variable
                ParDescripcion.Value = Categoria.Descripcion;
                //valor que le voy asignar
                SqlCmd.Parameters.Add(ParDescripcion);

                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";

            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally {
                //limpia todos los recursos que se asignan al bloque
                //siempre ejecuta finally
                if (SqlCon.State == ConnectionState.Open) {
                    //Si el estado de la conexion esta abierta , la cierro
                    SqlCon.Close();
                }
            }
            return rpta;


        }
        //Metodo Editar
        public string Editar(DCategoria Categoria) {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
             //   SqlCon.ConnectionString = Conexion.Cn;
                //obtengo cadena de conexion , de mi clase Conexion
                SqlCon.Open();
                //abro conexion a la base de datos
                SqlCommand SqlCmd = new SqlCommand();
                //Creo un comando para comunicarme con SQLServer
                SqlCmd.Connection = SqlCon;
                //El comando le asigno la conexion establecida
                SqlCmd.CommandText = "speditar_categoria";
                //Objeto al que voy hacer referencia en mi base de datos
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //El tipo de objeto al que quiero conectarme es un stored procedure
                SqlParameter Parametros = new SqlParameter();
                //creo los parametros para pasarle al stored procedure
                Parametros.ParameterName = "@idCategoria";
                //nombre del parametro
                Parametros.SqlDbType = SqlDbType.Int;
                //El parametro es de tipo entero
             
                Parametros.Value = Categoria.Id;
                SqlCmd.Parameters.Add(Parametros);
                //agrego el parametro

                SqlParameter ParDescripcion = new SqlParameter();
                //creo los parametros para pasarle al stored procedure
                ParDescripcion.ParameterName = "@descripcion";
                //nombre del parametro
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 500;
                //tamaño de caracteres que soporta la variable
                ParDescripcion.Value = Categoria.Descripcion;
                //valor que le voy asignar
                SqlCmd.Parameters.Add(ParDescripcion);

                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se Actualizo el registro";

            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {
                //limpia todos los recursos que se asignan al bloque
                //siempre ejecuta finally
                if (SqlCon.State == ConnectionState.Open)
                {
                    //Si el estado de la conexion esta abierta , la cierro
                    SqlCon.Close();
                }
            }
            return rpta;

        }

        //Metodo Eliminar
        public string Eliminar(DCategoria Categoria) {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
            //    SqlCon.ConnectionString = Conexion.Cn;
                
                
                //obtengo cadena de conexion , de mi clase Conexion
                SqlCon.Open();
                //abro conexion a la base de datos
                SqlCommand SqlCmd = new SqlCommand();
                //Creo un comando para comunicarme con SQLServer
                SqlCmd.Connection = SqlCon;
                //El comando le asigno la conexion establecida
                SqlCmd.CommandText = "speliminar_categoria";
                //Objeto al que voy hacer referencia en mi base de datos
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //El tipo de objeto al que quiero conectarme es un stored procedure
                SqlParameter Parametros = new SqlParameter();
                //creo los parametros para pasarle al stored procedure
                Parametros.ParameterName = "@idCategoria";
                //nombre del parametro
                Parametros.SqlDbType = SqlDbType.Int;
                //El parametro es de tipo entero

                Parametros.Value = Categoria.Id;
                SqlCmd.Parameters.Add(Parametros);
                //agrego el parametro

               

                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se elimino el registro";

            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {
                //limpia todos los recursos que se asignan al bloque
                //siempre ejecuta finally
                if (SqlCon.State == ConnectionState.Open)
                {
                    //Si el estado de la conexion esta abierta , la cierro
                    SqlCon.Close();
                }
            }
            return rpta;
        }

        //Metodo Mostrar
        //Devuelve DataTable , porque asi muestra todas las filas de la tabla Categoria
        public DataTable Mostrar() {
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblCategoria");
            //Creo una variable del tipo resultado para llenar la tabla
            SqlConnection SqlCon = new SqlConnection();
            try
            {
              //  SqlCon.ConnectionString = Conexion.Cn;
                //obtengo cadena de conexion , de mi clase Conexion
                SqlCon.Open();
                //abro conexion a la base de datos
                SqlCommand SqlCmd = new SqlCommand();
                //Creo un comando para comunicarme con SQLServer
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //El tipo de objeto al que quiero conectarme es un stored procedure
                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                //creo un SqlAdapter para poder ejecutar el comando y llenar el DataTable
                SqlDat.Fill(DtResultado);
                //Lleno el Datatable
            }
            catch (Exception ex) {
                //Si existe algun error no obtengo resultado ninguno
                DtResultado = null;
            }
            return DtResultado;
        }

        //Metodo Buscar Registro de Categoria

        public DataTable BuscarElemento(DCategoria Categoria) {
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblCategoria");
            //Creo una variable del tipo resultado para llenar la tabla
            SqlConnection SqlCon = new SqlConnection();
            try
            {
             //   SqlCon.ConnectionString = Conexion.Cn;
                //obtengo cadena de conexion , de mi clase Conexion
                SqlCon.Open();
                //abro conexion a la base de datos
                SqlCommand SqlCmd = new SqlCommand();
                //Creo un comando para comunicarme con SQLServer
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //El tipo de objeto al que quiero conectarme es un stored procedure

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                //tamaño de caracteres que soporta la variable
                ParTextoBuscar.Value = Categoria.TextoBuscar;
                //valor que le voy asignar
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                //creo un SqlAdapter para poder ejecutar el comando y llenar el DataTable
                SqlDat.Fill(DtResultado);
                //Lleno el Datatable
            }
            catch (Exception ex)
            {
                //Si existe algun error no obtengo resultado ninguno
                DtResultado = null;
            }
            return DtResultado;
        }
    }
}
