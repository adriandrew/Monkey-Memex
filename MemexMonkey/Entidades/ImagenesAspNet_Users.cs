using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ImagenesAspNet_Users
    {

        #region Campos

        #region Campos Imagenes

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

        #region Campos AspNet_Users

        private Guid applicationId;

        // UserId ya se encuentra en tabla de comentariosAspnet_users.

        // private Guid userId;

        private string userName;

        private string loweredUserName;

        private string mobileAlias;

        private int isAnonymous;

        private DateTime lastActivityDate;

        #endregion

        #endregion

        #region Propiedades

        #region Propiedades Imagenes

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

        public string RutaRelativa
        {
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

        #region Propiedades AspNet_Users

        public Guid ApplicationId
        {
            get { return applicationId; }
            set { applicationId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string LoweredUserName
        {
            get { return loweredUserName; }
            set { loweredUserName = value; }
        }

        public string MobileAlias
        {
            get { return mobileAlias; }
            set { mobileAlias = value; }
        }

        public int IsAnonymous
        {
            get { return isAnonymous; }
            set { isAnonymous = value; }
        }

        public DateTime LastActivityDate
        {
            get { return lastActivityDate; }
            set { lastActivityDate = value; }
        }

        #endregion

        #endregion

        #region Metodos Publicos

        public List < ImagenesAspNet_Users > ObtenerPorIdImagen ( int idImagen )
        {

            List < ImagenesAspNet_Users > lista = new List < ImagenesAspNet_Users > ();

            try
            {

                string sql = "SELECT AspNet_Users.*, Imagenes.* FROM Imagenes INNER JOIN AspNet_Users ON Imagenes.UserId = aspnet_Users.UserId WHERE Imagenes.IdImagen = @idImagen";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@idImagen", idImagen );

                BaseDatos.conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();

                ImagenesAspNet_Users imagenesAspnet_users;

                while ( reader.Read() )
                {

                    imagenesAspnet_users = new ImagenesAspNet_Users();

                    imagenesAspnet_users.IdImagen = Convert.ToInt32(reader["IdImagen"]);

                    imagenesAspnet_users.IdCategoria = Convert.ToInt32(reader["IdCategoria"]);

                    imagenesAspnet_users.UserId = new Guid(reader["UserId"].ToString());

                    imagenesAspnet_users.EsAprobado = Convert.ToInt32(reader["EsAprobado"]);

                    imagenesAspnet_users.Titulo = reader["Titulo"].ToString();

                    imagenesAspnet_users.DirectorioRelativo = reader["DirectorioRelativo"].ToString();

                    imagenesAspnet_users.RutaRelativa = reader["RutaRelativa"].ToString();

                    imagenesAspnet_users.EnlaceExterno = reader["EnlaceExterno"].ToString();

                    imagenesAspnet_users.EtiquetasBasicas = reader["EtiquetasBasicas"].ToString();

                    imagenesAspnet_users.EtiquetasOpcionales = reader["EtiquetasOpcionales"].ToString();

                    imagenesAspnet_users.FechaSubida = Convert.ToDateTime(reader["FechaSubida"]);

                    imagenesAspnet_users.FechaPublicacion = Convert.ToDateTime(reader["FechaPublicacion"]);

                    imagenesAspnet_users.ApplicationId = new Guid(reader["ApplicationId"].ToString());

                    imagenesAspnet_users.UserName = reader["UserName"].ToString();

                    imagenesAspnet_users.LoweredUserName = reader["LoweredUserName"].ToString();

                    imagenesAspnet_users.MobileAlias = reader["MobileAlias"].ToString();

                    imagenesAspnet_users.IsAnonymous = Convert.ToInt32(reader["IsAnonymous"]);

                    imagenesAspnet_users.LastActivityDate = Convert.ToDateTime(reader["LastActivityDate"]);

                    lista.Add(imagenesAspnet_users);

                }

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

        public List<ImagenesAspNet_Users> ObtenerListadoAprobados()
        {

            List<ImagenesAspNet_Users> lista = new List<ImagenesAspNet_Users>();

            try
            {

                string sql = "SELECT AspNet_Users.*, Imagenes.* FROM Imagenes INNER JOIN AspNet_Users ON Imagenes.UserId = aspnet_Users.UserId WHERE EsAprobado = 1 ORDER BY FechaPublicacion DESC";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                BaseDatos.conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();

                ImagenesAspNet_Users imagenesAspnet_users;

                while (reader.Read())
                {

                    imagenesAspnet_users = new ImagenesAspNet_Users();

                    imagenesAspnet_users.IdImagen = Convert.ToInt32(reader["IdImagen"]);

                    imagenesAspnet_users.IdCategoria = Convert.ToInt32(reader["IdCategoria"].ToString());

                    imagenesAspnet_users.UserId = new Guid(reader["UserId"].ToString());

                    imagenesAspnet_users.EsAprobado = Convert.ToInt32(reader["EsAprobado"].ToString());

                    imagenesAspnet_users.Titulo = reader["Titulo"].ToString();

                    imagenesAspnet_users.DirectorioRelativo = reader["DirectorioRelativo"].ToString();

                    imagenesAspnet_users.RutaRelativa = reader["RutaRelativa"].ToString();

                    imagenesAspnet_users.EnlaceExterno = reader["EnlaceExterno"].ToString();

                    imagenesAspnet_users.EtiquetasBasicas = reader["EtiquetasBasicas"].ToString();

                    imagenesAspnet_users.EtiquetasOpcionales = reader["EtiquetasOpcionales"].ToString();

                    imagenesAspnet_users.FechaSubida = Convert.ToDateTime(reader["FechaSubida"].ToString());

                    imagenesAspnet_users.FechaPublicacion = Convert.ToDateTime(reader["FechaPublicacion"].ToString());

                    imagenesAspnet_users.ApplicationId = new Guid(reader["ApplicationId"].ToString());

                    imagenesAspnet_users.UserName = reader["UserName"].ToString();

                    imagenesAspnet_users.LoweredUserName = reader["LoweredUserName"].ToString();

                    imagenesAspnet_users.MobileAlias = reader["MobileAlias"].ToString();

                    imagenesAspnet_users.IsAnonymous = Convert.ToInt32(reader["IsAnonymous"]);

                    imagenesAspnet_users.LastActivityDate = Convert.ToDateTime(reader["LastActivityDate"]);

                    lista.Add(imagenesAspnet_users);

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

        #endregion

    }
}
