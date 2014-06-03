using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Comentarios
    {

        #region Campos

        private int idComentario;

        private Guid userId;

        private int idImagen;

        private string comentario;

        private string fechaPublicacion;

        private int meGusta;

        #endregion

        #region Propiedades 

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

        public string FechaPublicacion
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

        #region Metodos Publicos

        public void Guardar()
        {
            try
            {

                string sql = "INSERT INTO Comentarios ( UserId, IdImagen, Comentario, FechaPublicacion, MeGusta ) VALUES ( @userId, @idImagen, @comentario, @fechaPublicacion, @meGusta )";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@userId", this.UserId );

                comando.Parameters.AddWithValue ( "@idImagen", this.IdImagen );

                comando.Parameters.AddWithValue ( "@comentario", this.Comentario );

                comando.Parameters.AddWithValue ( "@fechaPublicacion", this.FechaPublicacion );

                comando.Parameters.AddWithValue ( "@meGusta", this.MeGusta );

                BaseDatos.conexion.Open();

                comando.ExecuteNonQuery();

                BaseDatos.conexion.Close();

            }
            catch (Exception)
            {
                
                throw;

            }            

        }

        #endregion

    }
}
