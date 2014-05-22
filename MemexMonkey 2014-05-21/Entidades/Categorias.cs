using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Categorias
    {

        #region "Campos"

        private int idCategoria;

        private string nombre;

        private string descripcion;

        #endregion

        #region "Propiedades"

        public int IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        #endregion

        #region "Metodos Publicos"

        public static List<Categorias> ObtenerListado ()
        {
            List<Categorias> listaCategoria = new List<Categorias>();

            try
            {

                string sql = "SELECT * FROM Categorias";

                SqlCommand comando = new SqlCommand(sql, BaseDatos.conexion);

                BaseDatos.conexion.Open();

                SqlDataReader dataReader = comando.ExecuteReader();

                Categorias categoria;

                while (dataReader.Read())
                {
                    
                    categoria = new Categorias();

                    categoria.IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);

                    categoria.Nombre = dataReader["Nombre"].ToString();

                    categoria.Descripcion = dataReader["Descripcion"].ToString();

                    listaCategoria.Add(categoria);

                }

                BaseDatos.conexion.Close();

            return listaCategoria;

            }
            catch(Exception)
            {

                throw;

            }
            finally
            {

                BaseDatos.conexion.Close();

            }

        }

        #endregion

    }
}
