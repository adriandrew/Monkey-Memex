using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Imagenes
    {

        #region "Campos"

        private string titulo;

        private string ruta;

        private string enlaceExterno;

        private string etiquetasBasicas;

        private string etiquetasOpcionales;

        private DateTime fechaSubida;

        private int idCategoria;

        private Guid userId;
                
        #endregion

        #region "Propiedades"

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string Ruta
        {
            get { return ruta; }
            set { ruta = value; }
        }

        public string EnlaceExterno
        {
            get { return enlaceExterno; }
            set { enlaceExterno = value; }
        }

        public string EtiquetasBasicas
        {
            get { return etiquetasBasicas; }
            set { etiquetasBasicas = value; }
        }

        public string EtiquetasOpcionales
        {
            get { return etiquetasOpcionales; }
            set { etiquetasOpcionales = value; }
        }

        public DateTime FechaSubida
        {
            get { return fechaSubida; }
            set { fechaSubida = value; }
        }

        public int IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        public Guid UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        #endregion

        #region "Metodos"

        public void Guardar()
        {

            try
            {

                string sql = "INSERT INTO Imagenes ( Titulo, Ruta, EnlaceExterno, EtiquetasBasicas, EtiquetasOpcionales, FechaSubida, IdCategoria, UserId ) VALUES ( @titulo, @ruta, @enlaceExterno, @etiquetasBasicas, @etiquetasOpcionales, @fechaSubida, @idCategoria, @userId )";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@titulo", this.Titulo );

                comando.Parameters.AddWithValue ( "@ruta", this.Ruta );

                comando.Parameters.AddWithValue ( "@enlaceExterno", this.EnlaceExterno );

                comando.Parameters.AddWithValue ( "@etiquetasBasicas", this.EtiquetasBasicas );

                comando.Parameters.AddWithValue ( "@etiquetasOpcionales", this.EtiquetasOpcionales );

                comando.Parameters.AddWithValue ( "@fechaSubida", this.FechaSubida );

                comando.Parameters.AddWithValue ( "@idCategoria", this.IdCategoria );

                comando.Parameters.AddWithValue ( "@userId", this.UserId );

                BaseDatos.conexion.Open();

                comando.ExecuteNonQuery();
                
            }
            catch ( Exception )
            {

                throw;

            }
            finally
            {

                BaseDatos.conexion.Close();

            }

        }

        public void ObtenerRutasPorUsuario() { 
        


        }

        #endregion

    }
}
