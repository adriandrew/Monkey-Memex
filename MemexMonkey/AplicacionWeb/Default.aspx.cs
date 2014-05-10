using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

                            Image imgAprobada = CrearImageAprobada ( urlImagen, titulo );

                            pnlImagen.Controls.Add ( imgAprobada );

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

            pnlImagen.Attributes.Add ( "style", "margin: 30px;" );

            return pnlImagen;

        }

        private Image CrearImageAprobada ( string urlImagen, string titulo )
        {

            Image imgAprobada = new Image();

            imgAprobada.ImageUrl = urlImagen;

            imgAprobada.AlternateText = titulo;

            imgAprobada.BorderWidth = 20;

            imgAprobada.BorderColor = System.Drawing.Color.Black;

            imgAprobada.BorderStyle = BorderStyle.Solid;

            return imgAprobada;

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