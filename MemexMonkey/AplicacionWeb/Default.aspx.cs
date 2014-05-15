using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{

    public partial class Default : System.Web.UI.Page
    {

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            // TODO Se crea el objeto de la clase y se invoca el metodo para autoverificar la solucion. PENDIENTE.

            //LogicaNegocio.Autoverificacion autoverificacion = new LogicaNegocio.Autoverificacion();

            //if (! autoverificacion.AutoverificarSolucion().Equals("Exitoso"))
            //{

            //    Response.Write("<script>window.alert('"+ autoverificacion.AutoverificarSolucion() +"');</script>"); 

            //}

            //InsertarDiv();


            //Label lblNumeroUsuariosOnline = MostrarNumeroUsuariosOnline();

            //pnlImagenes.Controls.Add ( lblNumeroUsuariosOnline );

            MostrarImagenes();

        }

        #endregion

        #region Metodos Publicos

        public void InsertarDiv()
        {
                    
            Panel panel = new Panel();

            panel.ID = "idPanel";

            pnlImagenes.Controls.Add(panel);

            Literal literal = new Literal();

            literal.Text = "<h2> Prueba </h2>";

            panel.Controls.Add(literal);
            
        }

        public void InsertarDivExternamente() 
        {

            


            idImagenes.InnerHtml = "<h2> prueba </h2>";


            Panel panel = new Panel();

            panel.ID = "idPanel";

            pnlImagenes.Controls.Add(panel);

            Literal literal = new Literal();

            literal.Text = "<h2> Prueba </h2>";

            panel.Controls.Add(literal);

        }

        #endregion

        #region Metodos Privados

        private Label MostrarNumeroUsuariosOnline()
        {

            Label lblNumeroUsuariosOnline = new Label();

            if ( ! Page.IsPostBack )
               lblNumeroUsuariosOnline.Text = string.Format ( "{0} {1}", "Numero de usuarios activos en este momento:", Membership.GetNumberOfUsersOnline().ToString() );

            lblNumeroUsuariosOnline.Text = string.IsNullOrEmpty ( lblNumeroUsuariosOnline.Text.ToString() ) ? "0" : lblNumeroUsuariosOnline.Text.ToString() ;

            return lblNumeroUsuariosOnline;

        }

        // Mierda, tengo que encontrar la manera mas optima para mostrar las putas imagenes, tengo que consultarlo con la almohada..
        private void MostrarImagenes()
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List<Entidades.Imagenes> listaImagenes = new List<Entidades.Imagenes>();

            listaImagenes = imagenes.ObtenerListadoAprobados();

            foreach ( Entidades.Imagenes elementoImagenes in listaImagenes )
            {

                string idImagen = elementoImagenes.IdImagen.ToString();

                string titulo = elementoImagenes.Titulo.ToString();

                string directorioRelativo = elementoImagenes.DirectorioRelativo.ToString();

                string rutaRelativa = elementoImagenes.RutaRelativa.ToString();

                string enlaceExterno = elementoImagenes.EnlaceExterno.ToString();

                string etiquetasBasicas = elementoImagenes.EtiquetasBasicas.ToString();

                string etiquetasOpcionales = elementoImagenes.EtiquetasOpcionales.ToString();

                string fechaSubida = elementoImagenes.FechaSubida.ToString();

                string idCategoria = elementoImagenes.IdCategoria.ToString();

                string userId = elementoImagenes.UserId.ToString();

                string esAprobado = elementoImagenes.EsAprobado.ToString();

                System.IO.DirectoryInfo directorioInfo = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(directorioRelativo));

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

                            Literal litTituloImagenAprobada = CrearLiteralTituloImagenAprobada ( titulo );

                            pnlImagen.Controls.Add ( litTituloImagenAprobada );

                            Literal litNombreUsuarioImagenAprobada = CrearLiteralNombreUsuarioImagenAprobada ( userId );

                            pnlImagen.Controls.Add ( litNombreUsuarioImagenAprobada );

                            Literal litFechaPublicacionImagenAprobada = CrearLiteralFechaPublicacionImagenAprobada ( fechaSubida );

                            pnlImagen.Controls.Add ( litFechaPublicacionImagenAprobada );

                            Image imgAprobada = CrearImageAprobada ( urlImagen, titulo );

                            pnlImagen.Controls.Add ( imgAprobada );

                            Panel pnlEtiquetasImagenAprobada = CrearPanelEtiquetasImagenAprobada();

                            pnlImagen.Controls.Add ( pnlEtiquetasImagenAprobada );

                            Literal litEtiquetasImagenAprobada = CrearLiteralEtiquetasImagenAprobada ( etiquetasBasicas, etiquetasOpcionales );

                            pnlEtiquetasImagenAprobada.Controls.Add ( litEtiquetasImagenAprobada );

                            Button btnComentarios = CrearButtonComentariosImagenAprobada ( userId, idImagen );

                            pnlImagen.Controls.Add ( btnComentarios );

                            Panel pnlComentariosImagenAprobada = CrearPanelComentariosImagenAprobada ( idImagen );

                            pnlImagen.Controls.Add ( pnlComentariosImagenAprobada );

                            Panel pnlComentarioUsuarioImagenAprobada = CrearPanelComentarioUsuarioImagenAprobada();

                            pnlComentariosImagenAprobada.Controls.Add ( pnlComentarioUsuarioImagenAprobada );

                            Literal litComentarioUsuarioImagenAprobada = CrearLiteralComentarioUsuarioImagenAprobada();

                            pnlComentarioUsuarioImagenAprobada.Controls.Add ( litComentarioUsuarioImagenAprobada );

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

        private Panel CrearPanelImagen ( string idImagen )
        {

            Panel pnlImagen = new Panel();

            pnlImagen.ID = idImagen;

            pnlImagen.CssClass = "divImagenAprobada";

            return pnlImagen;

        }

        private Panel CrearPanelEtiquetasImagenAprobada ()
        {

            Panel pnlEtiquetasImagenAprobada = new Panel();

            pnlEtiquetasImagenAprobada.CssClass = "divEtiquetasImagenAprobada";

            return pnlEtiquetasImagenAprobada;

        }

        private Panel CrearPanelComentariosImagenAprobada ( string idImagen )
        {

            Panel pnlComentariosImagenAprobada = new Panel();

            pnlComentariosImagenAprobada.ID = string.Format ( "{0}{1}", "divComentariosImagenAprobada", idImagen );

            pnlComentariosImagenAprobada.CssClass = "divComentariosImagenAprobada";
            
            return pnlComentariosImagenAprobada;

        }

        private Panel CrearPanelComentarioUsuarioImagenAprobada()
        {

            Panel pnlComentarioUsuarioImagenAprobada = new Panel();

            //pnlComentarioUsuarioImagenAprobada.ID = "divComentarioUsuarioImagenAprobada";

            //pnlComentarioUsuarioImagenAprobada.CssClass = "divComentarioUsuarioImagenAprobada";

            return pnlComentarioUsuarioImagenAprobada;

        }

        // Creo que deberia cambiarlo por un li o algo asi..
        private Button CrearButtonComentariosImagenAprobada ( string userId, string idImagen ) 
        {
            
            Button btnComentariosImagenAprobada = new Button();

            btnComentariosImagenAprobada.ID = userId + idImagen;

            //btnComentariosImagenAprobada.Attributes.Add ( "onClick", string.Format("{0}", "muestra_oculta('{1}{2}')", "ContentPlaceHolder_divComentariosImagenAprobada", idImagen ) );

            btnComentariosImagenAprobada.OnClientClick = string.Format ( "{0}", "muestra_oculta('{1}{2}')", "ContentPlaceHolder_divComentariosImagenAprobada", idImagen );
        
            btnComentariosImagenAprobada.Text = "Comentarios";

            return btnComentariosImagenAprobada;

        }

        private Literal CrearLiteralComentarioUsuarioImagenAprobada ( )
        {

            Literal litComentarioUsuarioImagenAprobada = new Literal();

            litComentarioUsuarioImagenAprobada.Text = string.Format("<h4> {0} </h4>", "prueba");

            return litComentarioUsuarioImagenAprobada;

        }

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

        private Image CrearImageAprobada ( string urlImagen, string titulo )
        {

            Image imgAprobada = new Image();

            imgAprobada.ImageUrl = urlImagen;

            imgAprobada.AlternateText = titulo;

            imgAprobada.CssClass = "imgImagenAprobada";

            return imgAprobada;

        }

        private Literal CrearLiteralEtiquetasImagenAprobada ( string etiquetasBasicas, string etiquetasOpcionales )
        {

            Literal litEtiquetasImagenAprobada = new Literal();

            litEtiquetasImagenAprobada.Text = string.Format ( "<h6> {0} {1} </h6>", etiquetasBasicas, etiquetasOpcionales );

            return litEtiquetasImagenAprobada;

        }

        private Literal CrearLiteralImagenNoEncontrada ( string rutaRelativa )
        {

            Literal litImagenNoEncontrada = new Literal();

            litImagenNoEncontrada.Text = string.Format ( "<h2>Falta el archivo.. {0} </h2>", rutaRelativa );

            return litImagenNoEncontrada;

        }

        private Label CrearLabelSinArchivos()
        {

            return new Label { Text = "Aún no se han subido archivos." };

        }

        #endregion
        
    }
}