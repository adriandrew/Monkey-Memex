using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ConexionBaseDatos
    {

        public bool VerificarConexionBaseDatos()
        {

            bool esConexionCorrecta = false;

            try
            {

                BaseDatos.conexion.Open();

                BaseDatos.conexion.Close();

                esConexionCorrecta = true;

            }
            catch ( Exception )
            {

                esConexionCorrecta = false;

            }
            finally
            {

                BaseDatos.conexion.Close();

            }

            return esConexionCorrecta;

        }

    }
}
