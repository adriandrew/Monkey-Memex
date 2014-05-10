using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb.Administradores
{
    public partial class PanelControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            AdministrarImagenes();

        }

        private void AdministrarImagenes() 
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List < Entidades.Imagenes > listaImagenes = new List < Entidades.Imagenes > ();  

            listaImagenes = imagenes.ObtenerListadoPendientes();

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

                System.IO.DirectoryInfo directorioInfo = new System.IO.DirectoryInfo ( HttpContext.Current.Server.MapPath ( directorioRelativo ) );

                if ( directorioInfo.Exists )
                {

                    System.IO.FileInfo[] informacionArchivo = directorioInfo.GetFiles();

                    bool esArchivoEncontrado = false;

                    foreach ( System.IO.FileInfo elementoInformacionArchivo in informacionArchivo )
                    {

                        Image imagen = new Image();

                        imagen.ImageUrl = string.Format ( "{0}\\{1}", directorioRelativo, elementoInformacionArchivo );

                        if (rutaRelativa.Equals ( imagen.ImageUrl ) )
                        {

                            Panel pnlImagen = new Panel();

                            pnlImagen.ID = idImagen;

                            pnlImagen.Attributes.Add ( "style", "margin: 30px; float: left;" );

                            pnlImagenes.Controls.Add ( pnlImagen );

                            imagen.AlternateText = titulo;

                            imagen.BorderWidth = 20;

                            imagen.BorderColor = System.Drawing.Color.Black;

                            imagen.BorderStyle = BorderStyle.Solid;

                            pnlImagen.Controls.Add ( imagen );

                            Button btnAprobar = new Button();

                            btnAprobar.Text = "Aprobar";

                            btnAprobar.BackColor = System.Drawing.Color.Green;

                            pnlImagen.Controls.Add ( btnAprobar ) ;
                            
                            esArchivoEncontrado = true;

                        }

                    }

                    if ( ! esArchivoEncontrado )
                    {

                        Panel panelImagen = new Panel();

                        panelImagen.ID = idImagen;

                        pnlImagenes.Controls.Add ( panelImagen );

                        Literal imagenNoEncontrada = new Literal();

                        imagenNoEncontrada.Text = "<h2>Falta el archivo.. " + rutaRelativa + " </h3>";

                        panelImagen.Controls.Add ( imagenNoEncontrada );

                    }

                }
                else
                {

                    pnlImagenes.Controls.Add ( new Label { Text = "Aún no se han subido archivos." } );

                }

            }

        }

    }
}