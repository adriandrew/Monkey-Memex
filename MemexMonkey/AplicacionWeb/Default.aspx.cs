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
            // Se verifica la conexion linkImagen la base de datos.

            if ( ! IsPostBack )
            {
 
            VerificarConexionBaseDatos();

            //VerificarArchivos();

            }

            // TODO Es para poder guardar comentariosAspnet_users con el enter.

            //txtComentarioUsuarioImagenAprobada_TextChanged(sender, e);

        }

        
        // TODO Es para poder guardar comentariosAspnet_users con el enter.

        private void txtComentarioUsuarioImagenAprobada_TextChanged ( Object sender, System.EventArgs e )
        {

            GuardarComentario();

        }

        #endregion

        #region Metodos Publicos

        public void VerificarConexionBaseDatos()
        {

            Entidades.ConexionBaseDatos conexionBaseDatos = new Entidades.ConexionBaseDatos();

            bool esConexionCorrecta = conexionBaseDatos.VerificarConexionBaseDatos();

            if ( ! esConexionCorrecta )
            { 
            
                string script = @"<script type='text/javascript'> alert('{0}');</script>";
                
                script = string.Format ( script, "Conexion a Base de Datos Incorrecta :(" );
                
                ScriptManager.RegisterStartupScript ( this, typeof ( Page ), "Alerta", script, false );

                //Response.Redirect ("~/Error.aspx");

            }
            else if ( esConexionCorrecta )
            {
                //  TODO: Es para la prueba con el scroll infinito.
                //MostrarImagenes2();

            // MostrarNumeroUsuariosOnline();

            }

        }

        public void VerificarArchivos()
        { 

            // TODO Se crea el objeto de la clase y se invoca el metodo para autoverificar la solucion. PENDIENTE (CAMBIAR SCRIPT DE ALERTA).

            //LogicaNegocio.Autoverificacion autoverificacion = new LogicaNegocio.Autoverificacion();

            //if (! autoverificacion.AutoverificarSolucion().Equals("Exitoso"))
            //{

            //    Response.Write("<script>window.alert('"+ autoverificacion.AutoverificarSolucion() +"');</script>"); 

            //}

        }

        [WebMethod]
        public static string MostrarImagenes3 ( int posicionImagenes, int cantidadImagenes )
        {

            var htmlImagenes = new StringBuilder();

            //System.Threading.Thread.Sleep(5000);

            Entidades.ImagenesAspNet_Users imagenesUsuarios = new Entidades.ImagenesAspNet_Users();

            List<Entidades.ImagenesAspNet_Users> listaTotalImagenes = new List<Entidades.ImagenesAspNet_Users>();

            listaTotalImagenes = imagenesUsuarios.ObtenerListadoAprobados();

            var listaParcialImagenes = ( from elemento in listaTotalImagenes select elemento ).Skip ( posicionImagenes ).Take ( cantidadImagenes );

            foreach (Entidades.ImagenesAspNet_Users elementoImagenes in listaParcialImagenes)
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

                System.IO.DirectoryInfo directorioInfo = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(directorioRelativo));

                if (directorioInfo.Exists)
                {

                    System.IO.FileInfo[] informacionArchivo = directorioInfo.GetFiles();

                    bool esArchivoEncontrado = false;

                    foreach (System.IO.FileInfo elementoInformacionArchivo in informacionArchivo)
                    {

                        string urlImagen = string.Format("{0}\\{1}", directorioRelativo, elementoInformacionArchivo);

                        if (rutaRelativa.Equals(urlImagen))
                        {

                            string tituloImagen = string.Format ( "<h2>{0}</h2>", titulo );

                            string nombreUsuario = string.Format ( "<h4>{0}{1}</h4>", "Aporte por: ", userName );

                            string fechaPublicacionImagen = string.Format ( "<h6>{0}</h6>", fechaPublicacion );

                            string archivoImagen = string.Format ( "<img src='{0}' alt='{1}' class='{2}'>", urlImagen, titulo, "imgImagenAprobada" );

                            string linkImagen = string.Format ( "<a href={0}{1}>{2}</a>", "PublicacionIndividual/", idImagen, archivoImagen );

                            string etiquetas = string.Format ( "<h6>{0} {1}</h6>", etiquetasBasicas, etiquetasOpcionales );

                            string divEtiquetas = string.Format ( "<div class={0}>{1}</div>", "divEtiquetasImagenAprobada", etiquetas );

                            string contenidoDivImagen = string.Format ( "{0}{1}{2}{3}{4}", tituloImagen, nombreUsuario, fechaPublicacionImagen, linkImagen, divEtiquetas );

                            string divImagen = string.Format ( "<div class={0}>{1}</div>", "divImagenAprobada", contenidoDivImagen );

                            htmlImagenes.AppendFormat ( divImagen );

                            esArchivoEncontrado = true;

                        }

                    }

                    if ( !esArchivoEncontrado )
                    {

                        string imagenNoEncontrada = string.Format("<h2>{0}</br>{1}</h2>", "No se encontro la imagen:", rutaRelativa); 

                        string divImagen = string.Format("<div class={0}>{1}</div>", "divImagenAprobada", imagenNoEncontrada);

                        htmlImagenes.AppendFormat( divImagen );

                    }

                }
                else
                {

                    htmlImagenes.Clear();

                    htmlImagenes.AppendFormat ( string.Format ( "<h2>{0}</h2>", "Aun no se han subido archivos!" ) );

                }

            }

            return htmlImagenes.ToString();

        }

        #endregion

        #region Metodos Privados

        private void MostrarNumeroUsuariosOnline()
        {

            Label lblNumeroUsuariosOnline = new Label();

            if ( ! Page.IsPostBack )
               lblNumeroUsuariosOnline.Text = string.Format ( "{0} {1}", "Numero de usuarios activos en este momento:", Membership.GetNumberOfUsersOnline().ToString() );

            lblNumeroUsuariosOnline.Text = string.IsNullOrEmpty ( lblNumeroUsuariosOnline.Text.ToString() ) ? "0" : lblNumeroUsuariosOnline.Text.ToString() ;

            pnlImagenes.Controls.Add ( lblNumeroUsuariosOnline );

        }

        // Mierda, tengo que encontrar la manera mas optima para mostrar las putas imagenes, tengo que consultarlo con la almohada..
        private void MostrarImagenes()
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List < Entidades.Imagenes > listaImagenes = new List < Entidades.Imagenes > ();

            listaImagenes = imagenes.ObtenerListadoAprobados();

            foreach ( Entidades.Imagenes elementoImagenes in listaImagenes )
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

                System.IO.DirectoryInfo directorioInfo = new System.IO.DirectoryInfo ( HttpContext.Current.Server.MapPath ( directorioRelativo ) );

                if ( directorioInfo.Exists )
                {

                    System.IO.FileInfo[] informacionArchivo = directorioInfo.GetFiles();

                    bool esArchivoEncontrado = false;

                    foreach ( System.IO.FileInfo elementoInformacionArchivo in informacionArchivo )
                    {

                        string urlImagen = string.Format ( "{0}\\{1}", directorioRelativo, elementoInformacionArchivo );

                        if ( rutaRelativa.Equals ( urlImagen ) )
                        {
                            
                            Panel pnlImagen = CrearPanelImagen ( idImagen );

                            pnlImagenes.Controls.Add ( pnlImagen );

                            #region Header de contenidoDivImagen.

                            Literal litTituloImagenAprobada = CrearLiteralTituloImagenAprobada ( titulo );

                            pnlImagen.Controls.Add ( litTituloImagenAprobada );

                            Literal litNombreUsuarioImagenAprobada = CrearLiteralNombreUsuarioImagenAprobada ( userId );

                            pnlImagen.Controls.Add ( litNombreUsuarioImagenAprobada );

                            Literal litFechaPublicacionImagenAprobada = CrearLiteralFechaPublicacionImagenAprobada ( fechaSubida );

                            pnlImagen.Controls.Add ( litFechaPublicacionImagenAprobada );

                            #endregion

                            System.Web.UI.HtmlControls.HtmlAnchor lnkPublicacionIndividual = CrearHtmlAnchorPublicacionIndividual ( idImagen );

                            pnlImagen.Controls.Add ( lnkPublicacionIndividual );

                            Image imgAprobada = CrearImageAprobada ( urlImagen, titulo );

                            lnkPublicacionIndividual.Controls.Add ( imgAprobada );

                            #region Footer de contenidoDivImagen.

                            Panel pnlEtiquetasImagenAprobada = CrearPanelEtiquetasImagenAprobada();

                            pnlImagen.Controls.Add ( pnlEtiquetasImagenAprobada );

                            Literal litEtiquetasImagenAprobada = CrearLiteralEtiquetasImagenAprobada ( etiquetasBasicas, etiquetasOpcionales );

                            pnlEtiquetasImagenAprobada.Controls.Add ( litEtiquetasImagenAprobada );

                            #endregion

                            System.Web.UI.HtmlControls.HtmlButton btnMostrarComentariosImagenAprobadas = CrearHtmlButtonComentariosImagenAprobada ( idImagen );

                            pnlImagen.Controls.Add ( btnMostrarComentariosImagenAprobadas );

                            #region Panel de comentariosAspnet_users de contenidoDivImagen.

                            Panel pnlComentariosImagenAprobada = CrearPanelComentariosImagenAprobada ( idImagen );

                            pnlImagen.Controls.Add ( pnlComentariosImagenAprobada );

                            #region Panel de comentariosAspnet_users de memex.

                            Panel pnlComentarioUsuarioImagenAprobada = CrearPanelComentarioUsuarioImagenAprobada();

                            pnlComentariosImagenAprobada.Controls.Add ( pnlComentarioUsuarioImagenAprobada );

                            TextBox txtComentarioUsuarioImagenAprobada = CrearTextBoxComentarioUsuarioImagenAprobada();

                            pnlComentariosImagenAprobada.Controls.Add ( txtComentarioUsuarioImagenAprobada );

                            #endregion

                            #region Panel de comentariosAspnet_users de facebook.

                            //Panel pnlComentariosUsuarioFacebookImagenAprobada = CrearComentariosUsuarioFacebookImagenAprobada(idImagen);

                            //aComentariosPorUsuarioMemex.Controls.Add ( pnlComentariosUsuarioFacebookImagenAprobada );

                            #endregion

                            #endregion

                            esArchivoEncontrado = true;

                        }

                    }

                    if ( ! esArchivoEncontrado )
                    {

                        Panel pnlImagen = CrearPanelImagen ( idImagen );

                        pnlImagenes.Controls.Add ( pnlImagen );

                        Literal litImagenNoEncontrada = CrearLiteralImagenNoEncontrada ( rutaRelativa );

                        pnlImagen.Controls.Add ( litImagenNoEncontrada );

                    }

                }
                else
                {

                    Label lblSinArchivos = CrearLabelSinArchivos();

                    pnlImagenes.Controls.Add ( lblSinArchivos );

                }

            }

        }

        private void MostrarImagenes2()
        {

            Entidades.ImagenesAspNet_Users imagenes = new Entidades.ImagenesAspNet_Users();

            List<Entidades.ImagenesAspNet_Users> listaImagenes = new List<Entidades.ImagenesAspNet_Users>();

            listaImagenes = imagenes.ObtenerListadoAprobados();

            foreach (Entidades.ImagenesAspNet_Users elementoImagenes in listaImagenes)
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

                System.IO.DirectoryInfo directorioInfo = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(directorioRelativo));

                if (directorioInfo.Exists)
                {

                    System.IO.FileInfo[] informacionArchivo = directorioInfo.GetFiles();

                    bool esArchivoEncontrado = false;

                    foreach (System.IO.FileInfo elementoInformacionArchivo in informacionArchivo)
                    {

                        string urlImagen = string.Format("{0}\\{1}", directorioRelativo, elementoInformacionArchivo);

                        if (rutaRelativa.Equals(urlImagen))
                        {

                            Panel pnlImagen = CrearPanelImagen(idImagen);

                            pnlImagenes.Controls.Add(pnlImagen);

                            #region Header de contenidoDivImagen.

                            Literal litTituloImagenAprobada = CrearLiteralTituloImagenAprobada(titulo);

                            pnlImagen.Controls.Add(litTituloImagenAprobada);

                            Literal litNombreUsuarioImagenAprobada = CrearLiteralNombreUsuarioImagenAprobada(userName);

                            pnlImagen.Controls.Add(litNombreUsuarioImagenAprobada);

                            Literal litFechaPublicacionImagenAprobada = CrearLiteralFechaPublicacionImagenAprobada(fechaSubida);

                            pnlImagen.Controls.Add(litFechaPublicacionImagenAprobada);

                            #endregion

                            System.Web.UI.HtmlControls.HtmlAnchor lnkPublicacionIndividual = CrearHtmlAnchorPublicacionIndividual(idImagen);

                            pnlImagen.Controls.Add(lnkPublicacionIndividual);

                            Image imgAprobada = CrearImageAprobada(urlImagen, titulo);

                            lnkPublicacionIndividual.Controls.Add(imgAprobada);

                            #region Footer de contenidoDivImagen.

                            Panel pnlEtiquetasImagenAprobada = CrearPanelEtiquetasImagenAprobada();

                            pnlImagen.Controls.Add(pnlEtiquetasImagenAprobada);

                            Literal litEtiquetasImagenAprobada = CrearLiteralEtiquetasImagenAprobada(etiquetasBasicas, etiquetasOpcionales);

                            pnlEtiquetasImagenAprobada.Controls.Add(litEtiquetasImagenAprobada);

                            #endregion

                            System.Web.UI.HtmlControls.HtmlButton btnMostrarComentariosImagenAprobadas = CrearHtmlButtonComentariosImagenAprobada(idImagen);

                            pnlImagen.Controls.Add(btnMostrarComentariosImagenAprobadas);

                            #region Panel de comentariosAspnet_users de contenidoDivImagen.

                            Panel pnlComentariosImagenAprobada = CrearPanelComentariosImagenAprobada(idImagen);

                            pnlImagen.Controls.Add(pnlComentariosImagenAprobada);

                            #region Panel de comentariosAspnet_users de memex.

                            Panel pnlComentarioUsuarioImagenAprobada = CrearPanelComentarioUsuarioImagenAprobada();

                            pnlComentariosImagenAprobada.Controls.Add(pnlComentarioUsuarioImagenAprobada);

                            TextBox txtComentarioUsuarioImagenAprobada = CrearTextBoxComentarioUsuarioImagenAprobada();

                            pnlComentariosImagenAprobada.Controls.Add(txtComentarioUsuarioImagenAprobada);

                            #endregion

                            #region Panel de comentariosAspnet_users de facebook.

                            //Panel pnlComentariosUsuarioFacebookImagenAprobada = CrearComentariosUsuarioFacebookImagenAprobada(idImagen);

                            //aComentariosPorUsuarioMemex.Controls.Add ( pnlComentariosUsuarioFacebookImagenAprobada );

                            #endregion

                            #endregion

                            esArchivoEncontrado = true;

                        }

                    }

                    if (!esArchivoEncontrado)
                    {

                        Panel pnlImagen = CrearPanelImagen(idImagen);

                        pnlImagenes.Controls.Add(pnlImagen);

                        Literal litImagenNoEncontrada = CrearLiteralImagenNoEncontrada(rutaRelativa);

                        pnlImagen.Controls.Add(litImagenNoEncontrada);

                    }

                }
                else
                {

                    Label lblSinArchivos = CrearLabelSinArchivos();

                    pnlImagenes.Controls.Add(lblSinArchivos);

                }

            }

        }

        private Panel CrearPanelImagen ( string idImagen )
        {

            Panel pnlImagen = new Panel();

            pnlImagen.ID = idImagen;

            pnlImagen.CssClass = "divImagenAprobada";

            return pnlImagen;

        }

        #region Header de Imagen.
        
        private Literal CrearLiteralTituloImagenAprobada ( string titulo )
        {

            Literal litTituloImagenAprobada = new Literal();

            litTituloImagenAprobada.Text = string.Format("<h2> {0} </h2>", titulo);

            return litTituloImagenAprobada;

        }

        private Literal CrearLiteralNombreUsuarioImagenAprobada ( string nombreUsuario )
        {

            Literal litNombreUsuarioImagenAprobada = new Literal();

            litNombreUsuarioImagenAprobada.Text = string.Format("<h4>{0} {1}</h4>", "Aporte por:", nombreUsuario);

            return litNombreUsuarioImagenAprobada;

        }

        private Literal CrearLiteralFechaPublicacionImagenAprobada ( string fechaPublicacion )
        {

            Literal litFechaPublicacionImagenAprobada = new Literal();

            litFechaPublicacionImagenAprobada.Text = string.Format ( "<h6> {0} </h6>", fechaPublicacion );

            return litFechaPublicacionImagenAprobada;

        }

        #endregion

        #region Imagen

        private System.Web.UI.HtmlControls.HtmlAnchor CrearHtmlAnchorPublicacionIndividual ( string idImagen )
        {
             
            System.Web.UI.HtmlControls.HtmlAnchor lnkPublicacionIndividual = new System.Web.UI.HtmlControls.HtmlAnchor();

            lnkPublicacionIndividual.HRef = string.Format ( "{0}{1}", "~/PublicacionIndividual/", idImagen );

            return lnkPublicacionIndividual;

        }

        private Image CrearImageAprobada ( string urlImagen, string titulo )
        {

            Image imgAprobada = new Image();

            imgAprobada.ImageUrl = urlImagen;

            imgAprobada.AlternateText = titulo;

            imgAprobada.CssClass = "imgImagenAprobada";

            return imgAprobada;

        }

        #endregion

        #region Footer de Imagen.

        private Panel CrearPanelEtiquetasImagenAprobada ()
        {

            Panel pnlEtiquetasImagenAprobada = new Panel();

            pnlEtiquetasImagenAprobada.CssClass = "divEtiquetasImagenAprobada";

            return pnlEtiquetasImagenAprobada;

        }

        private Literal CrearLiteralEtiquetasImagenAprobada ( string etiquetasBasicas, string etiquetasOpcionales )
        {

            Literal litEtiquetasImagenAprobada = new Literal();

            litEtiquetasImagenAprobada.Text = string.Format ( "<h6> {0} {1} </h6>", etiquetasBasicas, etiquetasOpcionales );

            return litEtiquetasImagenAprobada;

        }

        #endregion 

        private System.Web.UI.HtmlControls.HtmlButton CrearHtmlButtonComentariosImagenAprobada ( string idImagen )
        {

            string lnkComentariosImagenAprobada = string.Empty;     
            
            System.Web.UI.HtmlControls.HtmlButton btnMostrarComentariosImagenAprobada = new System.Web.UI.HtmlControls.HtmlButton();
            
            lnkComentariosImagenAprobada = string.Format ( "{0}", "<a href='#' id='lnkMostrarComentariosImagenAprobada' OnClick=MuestraOculta('ContentPlaceHolder_divComentariosImagenAprobada" + idImagen + "')> Comentarios </a>" );

            btnMostrarComentariosImagenAprobada.InnerHtml = lnkComentariosImagenAprobada;
            
            return btnMostrarComentariosImagenAprobada;

        }

        #region Panel de comentariosAspnet_users de Imagen.

        private Panel CrearPanelComentariosImagenAprobada ( string idImagen )
        {

            Panel pnlComentariosImagenAprobada = new Panel();

            pnlComentariosImagenAprobada.ID = string.Format ( "{0}{1}", "divComentariosImagenAprobada", idImagen );

            pnlComentariosImagenAprobada.CssClass = "divComentariosImagenAprobada";

            // Esta es la excepcion de aplicar estilos desde codigo, ya que asi lo requiero y es mas facil.
            
            pnlComentariosImagenAprobada.Attributes.Add ( "style", "display: none;" );
            
            return pnlComentariosImagenAprobada;

        }

        #region Panel de comentariosAspnet_users de memex.

        private Panel CrearPanelComentarioUsuarioImagenAprobada()
        {

            Panel pnlComentarioUsuarioImagenAprobada = new Panel();

            //pnlComentarioUsuarioImagenAprobada.ID = "divComentarioUsuarioImagenAprobada";

            //pnlComentarioUsuarioImagenAprobada.CssClass = "divComentarioUsuarioImagenAprobada";

            return pnlComentarioUsuarioImagenAprobada;

        }
        
        private TextBox CrearTextBoxComentarioUsuarioImagenAprobada()
        {

            TextBox txtComentarioUsuarioImagenAprobada = new TextBox();

            txtComentarioUsuarioImagenAprobada.Text = "Escribe tu comentario";
            
            txtComentarioUsuarioImagenAprobada.Attributes.Add ( "OnClick", "this.value = '" + string.Empty + "'" );

            txtComentarioUsuarioImagenAprobada.Attributes.Add ( "OnChange", "this.value = '" + string.Empty + "'" );

            txtComentarioUsuarioImagenAprobada.TextChanged += new System.EventHandler ( txtComentarioUsuarioImagenAprobada_TextChanged );

            return txtComentarioUsuarioImagenAprobada;

        }

        #endregion

        #region Panel de comentariosAspnet_users de facebook.

        private Panel CrearComentariosUsuarioFacebookImagenAprobada ( string idImagen )
        {

            Panel pnlComentariosUsuarioFacebookImagenAprobada = new Panel();

            pnlComentariosUsuarioFacebookImagenAprobada.CssClass = "fb-comments";

            pnlComentariosUsuarioFacebookImagenAprobada.Attributes.Add ( "data-href", "http://monkey.somee.com/" + idImagen );

            pnlComentariosUsuarioFacebookImagenAprobada.Attributes.Add ( "data-numposts", "5" );

            pnlComentariosUsuarioFacebookImagenAprobada.Attributes.Add ( "data-colorscheme", "dark" );

            return pnlComentariosUsuarioFacebookImagenAprobada;

        }

        #endregion

        #endregion

        private Literal CrearLiteralImagenNoEncontrada ( string rutaRelativa )
        {

            Literal litImagenNoEncontrada = new Literal();

            litImagenNoEncontrada.Text = string.Format ( "<h2> Falta el archivo.. {0} </h2>", rutaRelativa );

            return litImagenNoEncontrada;

        }

        private Label CrearLabelSinArchivos()
        {

            return new Label { Text = "Aún no se han subido archivos." };

        }

        private void GuardarComentario()
        {

            string prueba;

            //if ((int)e.KeyChar == (int)Keys.Enter)
            //{
            //    //aqui codigo
            //}

        }

        #endregion

    }
}