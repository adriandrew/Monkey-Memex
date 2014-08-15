using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{

    public partial class Default : System.Web.UI.Page
    {

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            // Se verifica la conexion a la base de datos.

            if ( ! IsPostBack )
            {
 
                VerificarConexionBaseDatos();

                //VerificarArchivos();

            }

        }

        #endregion

        #region Metodos Publicos

        public void VerificarConexionBaseDatos()
        {

            Entidades.ConexionBaseDatos conexionBaseDatos = new Entidades.ConexionBaseDatos();

            bool esConexionCorrecta = conexionBaseDatos.VerificarConexionBaseDatos();

            if ( ! esConexionCorrecta )
            { 

                Response.Redirect("Error");

            }
          
        }

        public void VerificarArchivos()
        { 

            // TODO Se crea el objeto de la clase y se invoca el metodo para autoverificar la solucion. PENDIENTE (CAMBIAR SCRIPT DE ALERTA).

            //LogicaNegocio.Autoverificacion autoverificacion = new LogicaNegocio.Autoverificacion();

            //if (! autoverificacion.AutoverificarSolucion().Equals("Exitoso"))
            //{

            //string script = @"<script type='text/javascript'> alert('{0}');</script>";

            //script = string.Format(script, autoverificacion.AutoverificarSolucion());

            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alerta", script, false);

            //}

        }

        // Mierda, tengo que encontrar la manera mas optima para mostrar las putas imagenes, tengo que consultarlo con la almohada..

        [WebMethod]
        public static string MostrarImagenes ( int posicionImagenes, int cantidadImagenes )
        {

            var htmlImagenes = new StringBuilder();

            Default defaultt = new Default();

            Entidades.ImagenesAspNet_Users imagenesUsuarios = new Entidades.ImagenesAspNet_Users();

            List<Entidades.ImagenesAspNet_Users> listaTotalImagenes = new List<Entidades.ImagenesAspNet_Users>();

            listaTotalImagenes = imagenesUsuarios.ObtenerListadoAprobados();

            var listaParcialImagenes = ( from elemento in listaTotalImagenes select elemento ).Skip ( posicionImagenes ).Take ( cantidadImagenes );

            foreach (Entidades.ImagenesAspNet_Users elementoImagenes in listaParcialImagenes)
            {

                // Verifica si es archivo o enlace, ya que cada uno tiene metodos diferentes.

                if ( ! string.IsNullOrEmpty ( elementoImagenes.DirectorioRelativo.ToString() ) && ! string.IsNullOrEmpty ( elementoImagenes.RutaRelativa.ToString() ) )
                {

                    defaultt.VerificarArchivoImagen(htmlImagenes, elementoImagenes);

                }
                else if ( ! string.IsNullOrEmpty ( elementoImagenes.EnlaceExterno.ToString() ) )
                {

                    defaultt.VerificarEnlaceImagen(htmlImagenes, elementoImagenes);

                }

            }

            return htmlImagenes.ToString();    

        }

        #endregion

        #region Metodos Privados

        private void VerificarArchivoImagen ( StringBuilder htmlImagenes, Entidades.ImagenesAspNet_Users elementoImagenes )
        {

                string idImagen = elementoImagenes.IdImagen.ToString();

                string idCategoria = elementoImagenes.IdCategoria.ToString();

                string userId = elementoImagenes.UserId.ToString();

                string esAprobado = elementoImagenes.EsAprobado.ToString();

                string titulo = elementoImagenes.Titulo.ToString();

                string directorioRelativo = elementoImagenes.DirectorioRelativo.ToString();

                string rutaRelativa = elementoImagenes.RutaRelativa.ToString();

                string enlaceExterno = elementoImagenes.EnlaceExterno.ToString();

                string etiquetasBasicas = elementoImagenes.EtiquetasBasicas.ToString();

                string etiquetasOpcionales = elementoImagenes.EtiquetasOpcionales.ToString();

                string fechaSubida = elementoImagenes.FechaSubida.ToString();

                string fechaPublicacion = elementoImagenes.FechaPublicacion.ToString();

                string applicationId = elementoImagenes.ApplicationId.ToString();

                string userName = elementoImagenes.UserName;

                string loweredUserName = elementoImagenes.LoweredUserName;

                string mobileAlias = elementoImagenes.MobileAlias;

                string isAnonymous = elementoImagenes.IsAnonymous.ToString();

                string lastActivityDate = elementoImagenes.LastActivityDate.ToString();

                System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo ( HttpContext.Current.Server.MapPath ( directorioRelativo ) );

                if ( directorio.Exists )
                {

                    System.IO.FileInfo[] archivos = directorio.GetFiles();

                    bool esArchivoEncontrado = false;

                    foreach ( System.IO.FileInfo archivo in archivos )
                    {

                        string urlImagen = string.Format ( "{0}\\{1}", directorioRelativo, archivo );

                        if ( rutaRelativa.Equals ( urlImagen ) )
                        {

                            string tituloImagen = string.Format ( "<h2>{0}</h2>", titulo );

                            string nombreUsuario = string.Format ( "<h3>{0}{1}</h3>", "Aporte por: ", userName );

                            string fechaPublicacionImagen = string.Format ( "<h4>{0}</h4>", fechaPublicacion );

                            string archivoImagen = string.Format ( "<img src='{0}' alt='{1}'>", urlImagen, titulo );

                            string linkImagen = string.Format ( "<a class={0} href={1}{2} onmouseover={3}>{4}</a>", "iframe", "Individual/", idImagen, "InvocarFancybox()", archivoImagen );

                            string etiquetas = string.Format ( "<h4>{0} | {1}</h4>", etiquetasBasicas, etiquetasOpcionales );

                            string contenidoDivImagen = string.Format ( "{0}{1}{2}{3}{4}", tituloImagen, nombreUsuario, fechaPublicacionImagen, linkImagen, etiquetas );

                            string imagen = string.Format ( "<div class={0}>{1}</div>", "imagen", contenidoDivImagen );

                            htmlImagenes.AppendFormat ( imagen );

                            esArchivoEncontrado = true;

                            break;

                        }

                    }

                    if ( ! esArchivoEncontrado )
                    {

                        string imagenNoEncontrada = string.Format ( "<h2>{0}</br>{1}</h2>", "No se encontro la imagen:", rutaRelativa ); 

                        string imagen = string.Format ( "<div class={0}>{1}</div>", "imagen", imagenNoEncontrada );

                        htmlImagenes.AppendFormat ( imagen );

                    }

                }
                else if ( ! directorio.Exists )
                {

                    htmlImagenes.Clear();

                    htmlImagenes.AppendFormat ( string.Format ( "<h2>{0}</h2>", "El directorio no existe." ) );

                }

        }

        private void VerificarEnlaceImagen ( StringBuilder htmlImagenes, Entidades.ImagenesAspNet_Users elementoImagenes )
        {

            string idImagen = elementoImagenes.IdImagen.ToString();

            string idCategoria = elementoImagenes.IdCategoria.ToString();

            string userId = elementoImagenes.UserId.ToString();

            string esAprobado = elementoImagenes.EsAprobado.ToString();

            string titulo = elementoImagenes.Titulo.ToString();

            string directorioRelativo = elementoImagenes.DirectorioRelativo.ToString();

            string rutaRelativa = elementoImagenes.RutaRelativa.ToString();

            string enlaceExterno = elementoImagenes.EnlaceExterno.ToString();

            string etiquetasBasicas = elementoImagenes.EtiquetasBasicas.ToString();

            string etiquetasOpcionales = elementoImagenes.EtiquetasOpcionales.ToString();

            string fechaSubida = elementoImagenes.FechaSubida.ToString();

            string fechaPublicacion = elementoImagenes.FechaPublicacion.ToString();

            string applicationId = elementoImagenes.ApplicationId.ToString();

            string userName = elementoImagenes.UserName;

            string loweredUserName = elementoImagenes.LoweredUserName;

            string mobileAlias = elementoImagenes.MobileAlias;

            string isAnonymous = elementoImagenes.IsAnonymous.ToString();

            string lastActivityDate = elementoImagenes.LastActivityDate.ToString();

            string tituloImagen = string.Format("<h2>{0}</h2>", titulo);

            string nombreUsuario = string.Format("<h3>{0}{1}</h3>", "Aporte por: ", userName);

            string fechaPublicacionImagen = string.Format("<h4>{0}</h4>", fechaPublicacion);

            string archivoImagen = string.Format("<img src='{0}' alt='{1}'>", enlaceExterno, titulo);

            string linkImagen = string.Format("<a class={0} href={1}{2} onmouseover={3}>{4}</a>", "iframe", "Individual/", idImagen, "InvocarFancybox()", archivoImagen);

            string etiquetas = string.Format("<h4>{0} | {1}</h4>", etiquetasBasicas, etiquetasOpcionales);

            string contenidoDivImagen = string.Format("{0}{1}{2}{3}{4}", tituloImagen, nombreUsuario, fechaPublicacionImagen, linkImagen, etiquetas);

            string imagen = string.Format("<div class={0}>{1}</div>", "imagen", contenidoDivImagen);

            htmlImagenes.AppendFormat(imagen);

        }

        #endregion

    }
}