using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDeDatos
{
    class DCliente
    {
        SqlCommand SqlCmd;
        Conexion conexion;

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






    }
}
