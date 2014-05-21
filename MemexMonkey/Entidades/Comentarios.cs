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

        private int userId;

        private string comentario;

        private string fechaPublicacion;

        private bool esAplauso;

        #endregion

        #region Propiedades 

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
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

        public bool EsAplauso
        {
            get { return esAplauso; }
            set { esAplauso = value; }
        }

        #endregion  

        #region Metodos Publicos

        public void Guardar()
        {
            try
            {

                string sql = "INSERT INTO Comentarios ( UserId, Comentario, FechaPublicacion, EsAplauso ) VALUES ( @userId, @comentario, @fechaPublicacion, @esAplauso )";

                SqlCommand comando = new SqlCommand();

                comando.Connection = BaseDatos.conexion;

                comando.CommandText = sql;

                comando.Parameters.AddWithValue ( "@userId", this.UserId );

                comando.Parameters.AddWithValue ( "@comentario", this.Comentario );

                comando.Parameters.AddWithValue ( "@fechaPublicacion", this.FechaPublicacion );

                comando.Parameters.AddWithValue ( "@esAplauso", this.EsAplauso );

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
