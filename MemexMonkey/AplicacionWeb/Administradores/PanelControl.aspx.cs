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

                Response.Redirect ( "Inicio" );

        }

        protected void btnAprobar_Click(object sender, EventArgs e)
        {

            //AprobarPublicacion ( idImagen );

            //Label ea = new Label();

            //ea.Text = "funciono";

            //pnlImagenes.Controls.Add (ea);

            CambiarColorFondo ( (Button) sender);
            
        }

        protected void btnAprobar_DoubleClick(object sender, EventArgs e)
        {

        //    AprobarPublicacion(idImagen);

        }

        #endregion

        #region Metodos Privados
        
        private void AdministrarImagenes() 
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List < Entidades.Imagenes > listaImagenes = new List < Entidades.Imagenes > ();  

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

        private void CambiarColorFondo ( Button btnAprobar )
        {

            btnAprobar.BackColor = System.Drawing.Color.Black;

        }

        private void AprobarPublicacion ( string idImagen) 
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            imagenes.IdImagen = Convert.ToInt32 ( idImagen );

            imagenes.EsAprobado = 1;

            imagenes.Actualizar();
        
        }

        private void RechazarPublicacion(string idImagen)
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

            pnlImagen.Attributes.Add ( "style", "margin: 30px; float: left;" );

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

            imgPendiente.BorderWidth = 20;

            imgPendiente.BorderColor = System.Drawing.Color.Black;

            imgPendiente.BorderStyle = BorderStyle.Solid;

            return imgPendiente;

        }

        private Button CrearButtonAprobar ( string idImagen )
        {

            Button btnAprobar = new Button();

            btnAprobar.Attributes.Add ( "id", idImagen ) ;

            // btnAprobar.OnClientClick = AprobarPublicacion ( idImagen );

            btnAprobar.Click += new EventHandler ( this.btnAprobar_Click );

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

        private Label CrearLabelSinArchivos() 
        {

            return new Label { Text = "Aún no se han subido archivos." };

        }
        
        #endregion
        
    }
}