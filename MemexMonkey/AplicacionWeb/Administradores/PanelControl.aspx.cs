using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AplicacionWeb.Administradores
{
    public partial class PanelControl : System.Web.UI.Page
    {

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            if ( User.IsInRole ( "Administradores" ) )

                AdministrarImagenes();

            else if ( ! User.IsInRole ( "Administradores" ) )

                Response.Redirect ( "../Inicio" );

        }

        protected void btnAprobar_Click(object sender, EventArgs e)
        {

            Button btnAprobarCopia = (Button)sender;

            string idImagen = btnAprobarCopia.ID;

            AprobarPublicacion ( idImagen );

            // CambiarColorFondo ( (Button) sender );
            
        }

        protected void btnAprobar_Click2(object sender, EventArgs e, string idImagen)
        {

            //AprobarPublicacion ( idImagen );

            // CambiarColorFondo ( (Button) sender);

        }

        #endregion

        #region Metodos Privados

        // TODO. Falta agregar hoja de estilo con estos estilos.

        private void AdministrarImagenes() 
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List < Entidades.Imagenes > listaImagenes = new List < Entidades.Imagenes > ();  

            // TODO. Falta limitar las publicaciones a 5 o 10 o hacerlo con el scroll como la pagina de Inicio.

            listaImagenes = imagenes.ObtenerListadoPendientes();

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

                            Image imgPendiente = CrearImagePendiente ( urlImagen, titulo );

                            pnlImagen.Controls.Add ( imgPendiente );

                            Panel pnlCalificar = CrearPanelCalificar ( idImagen );

                            pnlImagen.Controls.Add ( pnlCalificar );

                            Button btnAprobar = CrearButtonAprobar ( idImagen );

                            pnlCalificar.Controls.Add ( btnAprobar );

                            Button btnRechazar = CrearButtonRechazar();

                            pnlCalificar.Controls.Add ( btnRechazar );
                            
                            esArchivoEncontrado = true;

                            break;

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
                else if ( ! directorioInfo.Exists )
                {

                    Label lblSinArchivos = CrearLabelDirectorioNoExistente();

                    pnlImagenes.Controls.Add ( lblSinArchivos );

                }

            }

        }

        private void CambiarColorFondo ( Button btnAprobar )
        {

            btnAprobar.BackColor = System.Drawing.Color.Black;

        }

        private void AprobarPublicacion ( string idImagen) 
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            imagenes.IdImagen = Convert.ToInt32 ( idImagen );

            imagenes.EsAprobado = 1;

            imagenes.FechaPublicacion = DateTime.Now;

            imagenes.Actualizar();
        
        }

        private void RechazarPublicacion ( string idImagen )
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            imagenes.IdImagen = Convert.ToInt32(idImagen);

            imagenes.EsAprobado = 0;

            imagenes.Actualizar();

        }

        private Panel CrearPanelImagen ( string idImagen )
        {

            Panel pnlImagen = new Panel();

            pnlImagen.Attributes.Add ( "id", idImagen );

            pnlImagen.Attributes.Add ( "style", "margin: 30px; float: left; max-width: 540px;" );

            return pnlImagen;

        }

        private Panel CrearPanelCalificar ( string idImagen )
        {

            Panel pnlCalificar = new Panel();

            pnlCalificar.Attributes.Add ( "id", idImagen );

            pnlCalificar.Attributes.Add("style", "clear: both; display: inherit;");

            return pnlCalificar;

        }

        private Image CrearImagePendiente ( string urlImagen, string titulo ) 
        {

            Image imgPendiente = new Image();

            imgPendiente.ImageUrl = urlImagen;

            imgPendiente.AlternateText = titulo;

            imgPendiente.Attributes.Add( "style", "width: 540px; max-width: 540px;" );

            return imgPendiente;

        }

        private Button CrearButtonAprobar ( string idImagen )
        {

            Button btnAprobar = new Button();

            //btnAprobar.Attributes.Add ( "id", idImagen ) ;

            btnAprobar.ID = idImagen;

            // btnAprobar.OnClientClick = AprobarPublicacion ( idImagen );

            btnAprobar.Click += new EventHandler ( this.btnAprobar_Click );

            //btnAprobar.Click += delegate(object sender, EventArgs e) { this.btnAprobar_Click2( sender, e, idImagen); };

            btnAprobar.Width = 177;

            btnAprobar.Height = 132;

            btnAprobar.Attributes.Add("style", "float: left; border: none; background: url('../images/like.png'); background-repeat: no-repeat;");

            return btnAprobar;

        }

        private Button CrearButtonRechazar()
        {

            Button btnRechazar = new Button();

            btnRechazar.Attributes.Add("id", "0");

            btnRechazar.Width = 177;

            btnRechazar.Height = 132;

            btnRechazar.Attributes.Add("style", "float: right; border: none; background: url('../images/dislike.png'); background-repeat: no-repeat;");

            return btnRechazar;

        }

        private Literal CrearLiteralImagenNoEncontrada ( string rutaRelativa ) 
        {

            Literal litImagenNoEncontrada = new Literal();

            litImagenNoEncontrada.Text = "<h2>Falta el archivo.. " + rutaRelativa + " </h3>";

            return litImagenNoEncontrada;

        }

        private Label CrearLabelDirectorioNoExistente() 
        {

            return new Label { Text = "El directorio no existe." };

        }

        private void EliminarImagen(string directorioRelativo, string rutaRelativa)
        {

            System.IO.DirectoryInfo directorioInformacion = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(directorioRelativo));

            if (directorioInformacion.Exists)
            {

                System.IO.FileInfo[] archivos = directorioInformacion.GetFiles();
                
                foreach (System.IO.FileInfo archivo in archivos)
                {

                    string urlImagen = string.Format("{0}\\{1}", directorioRelativo, archivo);

                    if ( rutaRelativa.Equals(urlImagen) )
                    {

                        archivo.Delete();

                        break;

                    }

                }

            }

        }

        #endregion
        
    }
}