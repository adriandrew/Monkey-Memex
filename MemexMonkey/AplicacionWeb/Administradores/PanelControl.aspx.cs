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

            Button btnAprobarCopia = ( Button ) sender;

            string[] split = btnAprobarCopia.ID.Split ( '|' );

            foreach ( var item in split )
            {

                int idImagen = 0;

                if ( int.TryParse ( item, out idImagen ) )
                {

                    AprobarPublicacion ( idImagen.ToString() );

                    break;

                }

            }

            // CambiarColorFondo ( (Button) sender );
            
        }

        protected void btnAprobar_Click2(object sender, EventArgs e, string idImagen)
        {

            //AprobarPublicacion ( idImagen );

            // CambiarColorFondo ( (Button) sender);

        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {

            Button btnRechazarCopia = ( Button ) sender;

            string[] split = btnRechazarCopia.ID.Split ( '|' );

            foreach ( var item in split )
            {

                int idImagen = 0;

                if ( int.TryParse ( item, out idImagen ) )
                {
                    
                    RechazarPublicacion ( idImagen.ToString() );

                    break;

                }

            }

        }

        #endregion

        #region Metodos Privados

        // TODO. Falta agregar hoja de estilo con estos estilos.

        private void RecargarPagina()
        {

            Response.Redirect ( Request.RawUrl );

        }

        private void AdministrarImagenes() 
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List < Entidades.Imagenes > listaImagenes = new List < Entidades.Imagenes > ();  

            listaImagenes = imagenes.ObtenerListadoPendientes();

            foreach ( Entidades.Imagenes elementoImagenes in listaImagenes )
            {

                if ( ! string.IsNullOrEmpty ( elementoImagenes.DirectorioRelativo ) && ! string.IsNullOrEmpty ( elementoImagenes.RutaRelativa ) )
                {

                    VerificarArchivoImagen ( elementoImagenes );

                }
                else if ( ! string.IsNullOrEmpty ( elementoImagenes.EnlaceExterno ) )
                {

                    VerificarEnlaceImagen ( elementoImagenes );

                }

            }

        }

        private void VerificarArchivoImagen( Entidades.Imagenes elementoImagenes )
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

            System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(directorioRelativo));

            if (directorio.Exists)
            {

                System.IO.FileInfo[] archivos = directorio.GetFiles();

                bool esArchivoEncontrado = false;

                foreach (System.IO.FileInfo archivo in archivos)
                {

                    string urlImagen = string.Format("{0}\\{1}", directorioRelativo, archivo);

                    if (rutaRelativa.Equals(urlImagen))
                    {

                        Panel pnlImagen = CrearPanelImagen(idImagen);

                        pnlImagenes.Controls.Add(pnlImagen);

                        Image imgPendiente = CrearImagePendiente(urlImagen, titulo);

                        pnlImagen.Controls.Add(imgPendiente);

                        Panel pnlCalificar = CrearPanelCalificar(idImagen);

                        pnlImagen.Controls.Add(pnlCalificar);

                        Button btnAprobar = CrearButtonAprobar(idImagen);

                        pnlCalificar.Controls.Add(btnAprobar);

                        Button btnRechazar = CrearButtonRechazar(idImagen);

                        pnlCalificar.Controls.Add(btnRechazar);

                        esArchivoEncontrado = true;

                        break;

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
            else if (!directorio.Exists)
            {

                Label lblSinArchivos = CrearLabelDirectorioNoExistente();

                pnlImagenes.Controls.Add(lblSinArchivos);

            }

        }

        private void VerificarEnlaceImagen ( Entidades.Imagenes elementoImagenes )
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

            Panel pnlImagen = CrearPanelImagen(idImagen);

            pnlImagenes.Controls.Add(pnlImagen);

            Image imgPendiente = CrearImagePendiente(enlaceExterno, titulo);

            pnlImagen.Controls.Add(imgPendiente);

            Panel pnlCalificar = CrearPanelCalificar(idImagen);

            pnlImagen.Controls.Add(pnlCalificar);

            Button btnAprobar = CrearButtonAprobar(idImagen);

            pnlCalificar.Controls.Add(btnAprobar);

            Button btnRechazar = CrearButtonRechazar ( idImagen );

            pnlCalificar.Controls.Add(btnRechazar);

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

            RecargarPagina();

        }

        private void RechazarPublicacion ( string idImagen )
        {

            EliminarImagen ( Convert.ToInt32 ( idImagen ) );
             
            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            imagenes.IdImagen = Convert.ToInt32 ( idImagen );

            imagenes.Eliminar();

            RecargarPagina();

        }

        private void EliminarImagen ( int idImagen )
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List < Entidades.Imagenes > listaImagen = imagenes.ObtenerPorIdImagen ( idImagen );

            if ( listaImagen.Count == 1 )
            {

                string idCategoria = listaImagen[0].IdCategoria.ToString(); ;

                string userId = listaImagen[0].UserId.ToString();

                string esAprobado = listaImagen[0].EsAprobado.ToString();

                string titulo = listaImagen[0].Titulo;

                string directorioRelativo = listaImagen[0].DirectorioRelativo.ToString();

                string rutaRelativa = listaImagen[0].RutaRelativa.ToString();

                string enlaceExterno = listaImagen[0].EnlaceExterno.ToString();

                string etiquetasBasicas = listaImagen[0].EtiquetasBasicas.ToString();

                string etiquetasOpcionales = listaImagen[0].EtiquetasOpcionales.ToString();

                string fechaSubida = listaImagen[0].FechaSubida.ToString();

                string fechaPublicacion = listaImagen[0].FechaPublicacion.ToString();

                // Si es archivo hay que buscarlo para eliminarlo.

                if ( ! string.IsNullOrEmpty ( directorioRelativo ) && ! string.IsNullOrEmpty ( rutaRelativa ) )
                {

                    EliminarArchivoImagen ( directorioRelativo, rutaRelativa );

                }
            
            }

        }

        private void EliminarArchivoImagen ( string directorioRelativo, string rutaRelativa )
        {

            System.IO.DirectoryInfo directorioInformacion = new System.IO.DirectoryInfo ( HttpContext.Current.Server.MapPath ( directorioRelativo ) );

            if ( directorioInformacion.Exists )
            {

                System.IO.FileInfo[] archivos = directorioInformacion.GetFiles();
                
                foreach ( System.IO.FileInfo archivo in archivos )
                {

                    string urlImagen = string.Format ( "{0}\\{1}", directorioRelativo, archivo );

                    if ( rutaRelativa.Equals ( urlImagen ) )
                    {

                        archivo.Delete();

                        break;

                    }

                }

            }

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

            btnAprobar.ID = string.Format("{0}{1}", "Aprobar | ", idImagen);

            btnAprobar.Click += new EventHandler ( this.btnAprobar_Click );

            btnAprobar.Width = 177;

            btnAprobar.Height = 132;

            btnAprobar.Attributes.Add("style", "float: left; border: none; background: url('../images/aprobado.jpg'); background-size: contain; background-position: center; background-repeat: no-repeat;");

            return btnAprobar;

        }

        private Button CrearButtonRechazar ( string idImagen )
        {

            Button btnRechazar = new Button();

            btnRechazar.ID = string.Format ( "{0}{1}", "Rechazar | ", idImagen );

            btnRechazar.Click += new EventHandler ( this.btnRechazar_Click );

            btnRechazar.Width = 177;

            btnRechazar.Height = 132;

            btnRechazar.Attributes.Add("style", "float: right; border: none; background: url('../images/rechazado.jpg'); background-size: contain; background-position: center; background-repeat: no-repeat;");

            return btnRechazar;

        }

        private Literal CrearLiteralImagenNoEncontrada ( string rutaRelativa ) 
        {

            Literal litImagenNoEncontrada = new Literal();

            litImagenNoEncontrada.Text = "<h2>Falta el archivo.. " + rutaRelativa + " </h2>";

            return litImagenNoEncontrada;

        }

        private Label CrearLabelDirectorioNoExistente() 
        {

            return new Label { Text = "El directorio no existe." };

        }

        #endregion
        
    }
}