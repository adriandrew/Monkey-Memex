using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ComentariosAspNet_Users
    {

        #region Campos

        #region Campos Comentarios

        private int idComentario;

        private Guid userId;

        private int idImagen;

        private string comentario;

        private DateTime fechaPublicacion;

        private int meGusta;

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

        #region Propiedades Comentarios

        public int IdComentario
        {
            get { return idComentario; }
            set { idComentario = value; }
        }

        public Guid UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public int IdImagen
        {
            get { return idImagen; }
            set { idImagen = value; }
        }

        public string Comentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public DateTime FechaPublicacion
        {
            get { return fechaPublicacion; }
            set { fechaPublicacion = value; }
        }

        public int MeGusta
        {
            get { return meGusta; }
            set { meGusta = value; }
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

        public List < ComentariosAspNet_Users > ObtenerListadoPorIdImagen ( int idImagen )
        {

            List < ComentariosAspNet_Users > lista = new List < ComentariosAspNet_Users > ();

            try
            {

                string sql = "SELECT AspNet_Users.*, Comentarios.* FROM Comentarios INNER JOIN AspNet_Users ON Comentarios.UserId = aspnet_Users.UserId WHERE Comentarios.IdImagen = @idImagen";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@idImagen", idImagen );

                BaseDatos.conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();

                ComentariosAspNet_Users comentariosAspnet_users;

                while ( reader.Read() )
                {

                    comentariosAspnet_users = new ComentariosAspNet_Users();

                    comentariosAspnet_users.IdComentario = Convert.ToInt32 ( reader["IdComentario"] );

                    comentariosAspnet_users.UserId = new Guid ( reader["UserId"].ToString() );

                    comentariosAspnet_users.IdImagen = Convert.ToInt32 ( reader["IdImagen"] );

                    comentariosAspnet_users.Comentario = reader["Comentario"].ToString();

                    comentariosAspnet_users.FechaPublicacion = Convert.ToDateTime ( reader["FechaPublicacion"] );

                    comentariosAspnet_users.MeGusta = Convert.ToInt32 ( reader["MeGusta"] );

                    comentariosAspnet_users.ApplicationId = new Guid ( reader["ApplicationId"].ToString() );

                    comentariosAspnet_users.UserName = reader["UserName"].ToString();

                    comentariosAspnet_users.LoweredUserName = reader["LoweredUserName"].ToString();

                    comentariosAspnet_users.MobileAlias = reader["MobileAlias"].ToString();

                    comentariosAspnet_users.IsAnonymous = Convert.ToInt32 ( reader["IsAnonymous"] );

                    comentariosAspnet_users.LastActivityDate = Convert.ToDateTime ( reader["LastActivityDate"] );

                    lista.Add ( comentariosAspnet_users );

                }

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
