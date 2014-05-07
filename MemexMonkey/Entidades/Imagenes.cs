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

        private int idImagen;

        private string titulo;

        private string ruta;

        private string enlaceExterno;

        private string etiquetasBasicas;

        private string etiquetasOpcionales;

        private DateTime fechaSubida;

        private int idCategoria;

        private Guid userId;

        private int esAprobado;
                
        #endregion

        #region "Propiedades"

        public int IdImagen
        {
            get { return idImagen; }
            set { idImagen = value; }
        }

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

        public int EsAprobado
        {
            get { return esAprobado; }
            set { esAprobado = value; }
        }        

        #endregion

        #region "Metodos"

        public void Guardar()
        {

            try
            {

                string sql = "INSERT INTO Imagenes ( Titulo, Ruta, EnlaceExterno, EtiquetasBasicas, EtiquetasOpcionales, FechaSubida, IdCategoria, UserId, Aprobado ) VALUES ( @titulo, @ruta, @enlaceExterno, @etiquetasBasicas, @etiquetasOpcionales, @fechaSubida, @idCategoria, @userId, @aprobado )";

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

                comando.Parameters.AddWithValue("@Aprobado", this.EsAprobado );

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

        public List<Imagenes> ObtenerListadoAprobados()
        {

            List<Imagenes> lista = new List<Imagenes>();

            try
            {

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = "SELECT * FROM Imagenes WHERE EsAprobado = 1 ORDER BY FechaSubida DESC";

                BaseDatos.conexion.Open();

                SqlDataReader Reader = comando.ExecuteReader();

                Imagenes Imagenes;

                while ( Reader.Read() )
                {

                    Imagenes = new Imagenes();

                    Imagenes.idImagen = Convert.ToInt32(Reader["IdImagen"]);

                    Imagenes.titulo = Reader["Titulo"].ToString();

                    Imagenes.ruta = Reader["Ruta"].ToString();

                    Imagenes.enlaceExterno = Reader["EnlaceExterno"].ToString();

                    Imagenes.etiquetasBasicas = Reader["EtiquetasBasicas"].ToString();

                    Imagenes.etiquetasOpcionales = Reader["EtiquetasOpcionales"].ToString();

                    Imagenes.fechaSubida = Convert.ToDateTime(Reader["FechaSubida"].ToString());

                    Imagenes.idCategoria = Convert.ToInt32(Reader["IdCategoria"].ToString());

                    Imagenes.userId = new Guid ( Reader["UserId"].ToString() );

                    Imagenes.esAprobado = Convert.ToInt32 ( Reader["EsAprobado"].ToString() );

                    lista.Add(Imagenes);

                }

                BaseDatos.conexion.Close();

            }
            catch ( Exception )
            {

                throw;

            }

            finally            
            {

                BaseDatos.conexion.Close();

            }

            return lista;

        }

        #endregion

    }
}
