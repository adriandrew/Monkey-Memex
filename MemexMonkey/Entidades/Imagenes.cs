using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Imagenes
    {

        #region Campos

        private int idImagen;

        private int idCategoria;

        private Guid userId;

        private int esAprobado;

        private string titulo;

        private string directorioRelativo;

        private string rutaRelativa;

        private string enlaceExterno;

        private string etiquetasBasicas;

        private string etiquetasOpcionales;

        private DateTime fechaSubida;

        private DateTime fechaPublicacion;
                        
        #endregion

        #region Propiedades

        public int IdImagen
        {
            get { return idImagen; }
            set { idImagen = value; }
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

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string DirectorioRelativo
        {
            get { return directorioRelativo; }
            set { directorioRelativo = value; }
        }

        public string RutaRelativa        {
            get { return rutaRelativa; }
            set { rutaRelativa = value; }
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


        public DateTime FechaPublicacion
        {
            get { return fechaPublicacion; }
            set { fechaPublicacion = value; }
        }

        #endregion

        #region Metodos Publicos

        public void Guardar()
        {

            try
            {

                string sql = "INSERT INTO Imagenes ( IdCategoria, UserId, EsAprobado, Titulo, DirectorioRelativo, RutaRelativa, EnlaceExterno, EtiquetasBasicas, EtiquetasOpcionales, FechaSubida, FechaPublicacion ) VALUES ( @idCategoria, @userId, @esAprobado, @titulo, @directorioRelativo, @rutaRelativa, @enlaceExterno, @etiquetasBasicas, @etiquetasOpcionales, @fechaSubida, @FechaPublicacion )";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@idCategoria", this.IdCategoria );

                comando.Parameters.AddWithValue ( "@userId", this.UserId );

                comando.Parameters.AddWithValue ( "@esAprobado", this.EsAprobado );

                comando.Parameters.AddWithValue ( "@titulo", this.Titulo );

                comando.Parameters.AddWithValue ( "@directorioRelativo", this.DirectorioRelativo );

                comando.Parameters.AddWithValue ( "@rutaRelativa", this.RutaRelativa );

                comando.Parameters.AddWithValue ( "@enlaceExterno", this.EnlaceExterno );

                comando.Parameters.AddWithValue ( "@etiquetasBasicas", this.EtiquetasBasicas );

                comando.Parameters.AddWithValue ( "@etiquetasOpcionales", this.EtiquetasOpcionales );

                comando.Parameters.AddWithValue ( "@fechaSubida", this.FechaSubida );

                comando.Parameters.AddWithValue ( "@fechaPublicacion", this.FechaPublicacion );

                BaseDatos.conexion.Open();

                comando.ExecuteNonQuery();

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

        }

        public void Actualizar()
        {

            try
            {

                string sql = "UPDATE Imagenes SET EsAprobado = @esAprobado, FechaPublicacion = @fechaPublicacion WHERE IdImagen = @idImagen";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@idImagen", this.IdImagen );

                comando.Parameters.AddWithValue ( "@esAprobado", this.EsAprobado );

                comando.Parameters.AddWithValue ( "@fechaPublicacion", this.FechaPublicacion );

                BaseDatos.conexion.Open();

                comando.ExecuteNonQuery();

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

        }

        public void Eliminar()
        {

            try
            {

                string sql = "DELETE FROM Imagenes WHERE IdImagen = @IdImagen";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue("@IdImagen", this.IdImagen);

                BaseDatos.conexion.Open();

                comando.ExecuteNonQuery();

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

        }

        public List < Imagenes > ObtenerPorIdImagen ( int idImagen )
        { 
            List < Imagenes > lista = new List < Imagenes > ();

            try
            {

                string sql = "SELECT * FROM Imagenes WHERE IdImagen = @idImagen";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@idImagen", idImagen );

                BaseDatos.conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();

                Imagenes imagenes;

                while ( reader.Read() )
                {

                    imagenes = new Imagenes();

                    imagenes.IdImagen = Convert.ToInt32 ( reader [ "IdImagen" ] );

                    imagenes.IdCategoria = Convert.ToInt32 ( reader [ "IdCategoria" ] );

                    imagenes.UserId = new Guid ( reader [ "UserId" ].ToString() );

                    imagenes.EsAprobado = Convert.ToInt32 ( reader [ "EsAprobado" ] );

                    imagenes.Titulo = reader [ "Titulo" ].ToString();

                    imagenes.DirectorioRelativo = reader [ "DirectorioRelativo" ].ToString();

                    imagenes.RutaRelativa = reader [ "RutaRelativa" ].ToString();

                    imagenes.EnlaceExterno = reader [ "EnlaceExterno" ].ToString();

                    imagenes.EtiquetasBasicas = reader [ "EtiquetasBasicas" ].ToString();

                    imagenes.EtiquetasOpcionales = reader [ "EtiquetasOpcionales" ].ToString();

                    imagenes.FechaSubida = Convert.ToDateTime ( reader [ "FechaSubida" ] );

                    imagenes.FechaPublicacion = Convert.ToDateTime ( reader [ "FechaPublicacion" ] );

                    lista.Add ( imagenes );

                }

                BaseDatos.conexion.Close();

            }
            catch (Exception)
            {

                throw;

            }
            finally
            {

                BaseDatos.conexion.Close();
            
            }
            
            return lista;

        }

        public List < Imagenes > ObtenerListadoAprobados()
        {

            List < Imagenes > lista = new List < Imagenes > ();

            try
            {

                string sql = "SELECT * FROM Imagenes WHERE EsAprobado = 1 ORDER BY FechaSubida DESC";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                BaseDatos.conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();

                Imagenes imagenes;

                while ( reader.Read() )
                {

                    imagenes = new Imagenes();

                    imagenes.IdImagen = Convert.ToInt32 ( reader [ "IdImagen" ] );

                    imagenes.IdCategoria = Convert.ToInt32 ( reader [ "IdCategoria" ] .ToString() );

                    imagenes.UserId = new Guid ( reader [ "UserId" ] .ToString() );

                    imagenes.EsAprobado = Convert.ToInt32 ( reader [ "EsAprobado" ] .ToString() );

                    imagenes.Titulo = reader [ "Titulo" ] .ToString();

                    imagenes.DirectorioRelativo = reader [ "DirectorioRelativo" ] .ToString();

                    imagenes.RutaRelativa = reader [ "RutaRelativa" ] .ToString();

                    imagenes.EnlaceExterno = reader [ "EnlaceExterno" ] .ToString();

                    imagenes.EtiquetasBasicas = reader [ "EtiquetasBasicas" ] .ToString();

                    imagenes.EtiquetasOpcionales = reader [ "EtiquetasOpcionales" ] .ToString();

                    imagenes.FechaSubida = Convert.ToDateTime ( reader [ "FechaSubida" ] .ToString() );

                    imagenes.FechaPublicacion = Convert.ToDateTime ( reader [ "FechaPublicacion" ].ToString() );

                    lista.Add ( imagenes );

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

        public List < Imagenes > ObtenerListadoPendientes() {

            List < Imagenes > lista = new List < Imagenes > ();

            try 
            {

                string sql = "SELECT TOP (5) * FROM Imagenes WHERE EsAprobado = 0 ORDER BY FechaSubida DESC";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                BaseDatos.conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                
                Imagenes imagenes;

                while ( reader.Read() )
                {
                    
                    imagenes = new Imagenes();

                    imagenes.IdImagen = Convert.ToInt32 ( reader [ "IdImagen" ] );

                    imagenes.IdCategoria = Convert.ToInt32 ( reader [ "IdCategoria" ] );

                    imagenes.UserId = new Guid ( reader [ "UserId" ].ToString() );

                    imagenes.EsAprobado = Convert.ToInt32(reader["EsAprobado"]);
                    
                    imagenes.Titulo = reader [ "Titulo" ] .ToString();

                    imagenes.DirectorioRelativo = reader [ "DirectorioRelativo" ].ToString();

                    imagenes.RutaRelativa = reader [ "RutaRelativa" ].ToString();

                    imagenes.EnlaceExterno = reader [ "EnlaceExterno" ].ToString();

                    imagenes.EtiquetasBasicas = reader [ "EtiquetasBasicas" ].ToString();

                    imagenes.EtiquetasOpcionales = reader [ "EtiquetasOpcionales" ].ToString();

                    imagenes.FechaSubida = Convert.ToDateTime ( reader [ "FechaSubida" ] );

                    imagenes.FechaPublicacion = Convert.ToDateTime ( reader [ "FechaPublicacion" ].ToString());

                    lista.Add ( imagenes );

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
