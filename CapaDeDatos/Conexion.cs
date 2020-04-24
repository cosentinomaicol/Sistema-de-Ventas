using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    class Conexion
    {
        //cadena de conexion a la base de datos dentro del constructor 
        public SqlConnection Cn;
        public int idusuario;

        public string Conectado()
        {
            //funcion creada para conectarme a la base de datos Sql Server
            string rpta = "";
            try
            {
                Cn =  new SqlConnection("Data Source = localhost;Initial Catalog = DBVenta;Integrated Security=true");
                //creo una conexion a la base de datos
                Cn.Open();
                rpta = "OK";
                return rpta;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return rpta;
            }

        }

        public string Desconectado()
        {
            //Funcion para desconectarme de la base de datos
            string rpta = "";
            try
            {
                if (this.Cn.State == ConnectionState.Open)
                {
                    //Si el estado de la conexion esta abierta , la cierro
                    Cn.Close();
                    rpta = "OK";
                    return rpta;
                }
                else
                {
                    //no estaba abierta
                    rpta = "CERRADA";
                    return rpta;
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return rpta;
            }
        }
    }
}
