using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDeDatos
{
    public class DCliente
    {
        SqlCommand SqlCmd;
        Conexion conexion;
        public string TextoBuscar;
        private int _Id;
        private string _Nombre;
        private string _Apellido;
        private int _Dni;
        private string _Direccion;
        private string _Telefono;
        //Constructor Vacio
        public DCliente()
        {

        }
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

        public int Dni
        {
            get
            {
                return _Dni;
            }

            set
            {
                _Dni = value;
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
        public string Apellido
        {
            get
            {
                return _Apellido;
            }

            set
            {
                _Apellido = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _Direccion;
            }

            set
            {
                _Direccion = value;
            }
        }

        public string Telefono
        {
            get
            {
                return _Telefono;
            }

            set
            {
                _Telefono = value;
            }
        }


        public DataTable Mostrar()
        {
            string rpta;
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblCliente");
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
                    SqlCmd.CommandText = "spmostrar_clientes";
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
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public string Eliminar(DCliente Cliente)
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
                SqlCmd.CommandText = "speliminar_cliente";
                //Objeto al que voy hacer referencia en mi base de datos
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //El tipo de objeto al que quiero conectarme es un stored procedure
                SqlParameter Parametros = new SqlParameter();
                //creo los parametros para pasarle al stored procedure
                Parametros.ParameterName = "@idCliente";
                //nombre del parametro
                Parametros.SqlDbType = SqlDbType.Int;
                //El parametro es de tipo entero

                Parametros.Value = Cliente.Id;
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
        public string Editar(DCliente Cliente)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try {
                conexion = new Conexion();
                rpta = conexion.Conectado();
                if (rpta == "OK")
                {
                    SqlCommand SqlCmd = new SqlCommand();
                    //Creo un comando para comunicarme con SQLServer
                    SqlCmd.Connection = conexion.Cn;
                    SqlCmd.CommandText = "speditar_cliente";
                    //Objeto al que voy hacer referencia en mi base de datos
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter Parametros = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    Parametros.ParameterName = "@idCliente";
                    //nombre del parametro
                    Parametros.SqlDbType = SqlDbType.Int;
                    //El parametro es de tipo entero

                    Parametros.Value =  Cliente.Id;
                    SqlCmd.Parameters.Add(Parametros);

                    SqlParameter ParNombre = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParNombre.ParameterName = "@Nombre";
                    //nombre del parametro
                    ParNombre.SqlDbType = SqlDbType.VarChar;
                    ParNombre.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParNombre.Value = Cliente.Nombre;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParNombre);

                    SqlParameter ParApellido = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParApellido.ParameterName = "@Apellido";
                    //nombre del parametro
                    ParApellido.SqlDbType = SqlDbType.VarChar;
                    ParApellido.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParApellido.Value = Cliente.Apellido;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParApellido);

                    SqlParameter ParDni = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParDni.ParameterName = "@Dni";
                    //nombre del parametro
                    ParDni.SqlDbType = SqlDbType.Int;
                   
                    //tamaño de caracteres que soporta la variable
                    ParDni.Value = Cliente.Dni;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParDni);

                    SqlParameter ParDireccion = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParDireccion.ParameterName = "@Direccion";
                    //nombre del parametro
                    ParDireccion.SqlDbType = SqlDbType.VarChar;
                    ParDireccion.Size = 500;
                    //tamaño de caracteres que soporta la variable
                    ParDireccion.Value = Cliente.Direccion;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParDireccion);

                    SqlParameter ParTelefono = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParTelefono.ParameterName = "@Telefono";
                    //nombre del parametro
                    ParTelefono.SqlDbType = SqlDbType.VarChar;
                    ParTelefono.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParTelefono.Value = Cliente.Telefono;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParTelefono);


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

        public string Insertar(DCliente Cliente)
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
                    SqlCmd.CommandText = "spinsertar_cliente";
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter Parametros = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    Parametros.ParameterName = "@idCliente";
                    //nombre del parametro
                    Parametros.SqlDbType = SqlDbType.Int;
                    //El parametro es de tipo entero
                    Parametros.Direction = ParameterDirection.Output;
                    //el parametro es del tipo de salida
                    SqlCmd.Parameters.Add(Parametros);
                    //agrego el parametro

                    SqlParameter ParNombre = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParNombre.ParameterName = "@Nombre";
                    //nombre del parametro
                    ParNombre.SqlDbType = SqlDbType.VarChar;
                    ParNombre.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParNombre.Value = Cliente.Nombre;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParNombre);

                    SqlParameter ParApellido = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParApellido.ParameterName = "@Apellido";
                    //nombre del parametro
                    ParApellido.SqlDbType = SqlDbType.VarChar;
                    ParApellido.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParApellido.Value = Cliente.Apellido;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParApellido);

                    SqlParameter ParDni = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParDni.ParameterName = "@Dni";
                    //nombre del parametro
                    ParDni.SqlDbType = SqlDbType.VarChar;
                    ParDni.Size = 500;
                    //tamaño de caracteres que soporta la variable
                    ParDni.Value = Cliente.Dni;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParDni);

                    SqlParameter ParDireccion = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParDireccion.ParameterName = "@Direccion";
                    //nombre del parametro
                    ParDireccion.SqlDbType = SqlDbType.VarChar;
                    ParDireccion.Size = 500;
                    //tamaño de caracteres que soporta la variable
                    ParDireccion.Value = Cliente.Direccion;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParDireccion);

                    SqlParameter ParTelefono = new SqlParameter();
                    //creo los parametros para pasarle al stored procedure
                    ParTelefono.ParameterName = "@Telefono";
                    //nombre del parametro
                    ParTelefono.SqlDbType = SqlDbType.VarChar;
                    ParTelefono.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParTelefono.Value = Cliente.Telefono;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParTelefono);


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
        public DataTable BuscarDNI(DCliente Cliente)
        {


            string rpta;
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblCliente");
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
                    SqlCmd.CommandText = "spBuscarDNI_clientes";
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter ParTextoBuscar = new SqlParameter();
                    ParTextoBuscar.ParameterName = "@textobuscar";
                    ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                    ParTextoBuscar.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParTextoBuscar.Value = Cliente.TextoBuscar;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParTextoBuscar);
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


        public DataTable BuscarNombre(DCliente Cliente)
        {


            string rpta;
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblCliente");
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
                    SqlCmd.CommandText = "spBuscarNombre_clientes";
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter ParTextoBuscar = new SqlParameter();
                    ParTextoBuscar.ParameterName = "@textobuscar";
                    ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                    ParTextoBuscar.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParTextoBuscar.Value = Cliente.TextoBuscar;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParTextoBuscar);
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

        public DataTable BuscarTelefono(DCliente Cliente)
        {


            string rpta;
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblCliente");
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
                    SqlCmd.CommandText = "spBuscarTelefono_clientes";
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter ParTextoBuscar = new SqlParameter();
                    ParTextoBuscar.ParameterName = "@textobuscar";
                    ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                    ParTextoBuscar.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParTextoBuscar.Value = Cliente.TextoBuscar;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParTextoBuscar);
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
        public DataTable BuscarApellido(DCliente Cliente)
        {



            string rpta;
            //se muestran las filas en una tabla en la memoria ram y luego sera utilizado para cargar una datagridview
            DataTable DtResultado = new DataTable("tblCliente");
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
                    SqlCmd.CommandText = "spBuscarApellido_clientes";
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    //El tipo de objeto al que quiero conectarme es un stored procedure
                    SqlParameter ParTextoBuscar = new SqlParameter();
                    ParTextoBuscar.ParameterName = "@textobuscar";
                    ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                    ParTextoBuscar.Size = 50;
                    //tamaño de caracteres que soporta la variable
                    ParTextoBuscar.Value = Cliente.TextoBuscar;
                    //valor que le voy asignar
                    SqlCmd.Parameters.Add(ParTextoBuscar);
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
    }
}
