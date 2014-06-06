using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb.Miembros
{
    public partial class EnviarAporte : System.Web.UI.Page
    {

        #region Campos Estaticos
        
        private static string directorioRelativo;

        private static string rutaRelativa;

        #endregion

        #region Propiedades Estaticas

        public static string DirectorioRelativo
        {
            get { return EnviarAporte.directorioRelativo; }
            set { EnviarAporte.directorioRelativo = value; }
        }

        public static string RutaRelativa
        {
            get { return EnviarAporte.rutaRelativa; }
            set { EnviarAporte.rutaRelativa = value; }
        }

        #endregion
        
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!IsPostBack)
            //    Validate();

            CargarCategorias();

            CargarCaracteristicasControlImagenes();

            AgregarOnClick();

            //AgregarOnChange();

        }
       
        protected void btnEnviarAporte_Click(object sender, EventArgs e)
        {

            AplicacionWeb.Controles.SubirArchivo.TituloImagen = txtTituloImagen.Text;

            SubirImagenes();

            SubirAporte();
            
        }

        protected void lnkMostrarImagenes_Click(object sender, EventArgs e)

        {

            MostrarImagenes();

        }

        //protected void btnSubirImagenes_Click(object sender, EventArgs e)

        //{

        //    SubirImagenes();

        //}

        #endregion

        #region Metodos Privados

        private void SubirAporte() 
        {

            if ( ! string.IsNullOrEmpty ( AplicacionWeb.Miembros.EnviarAporte.RutaRelativa ) )
            {

                // Para guardar los datos que van en la tabla imagenesAspnet_users.

                Entidades.Imagenes imagenes = new Entidades.Imagenes();

                imagenes.IdCategoria = Convert.ToInt32 ( ddlCategoria.SelectedValue );

                imagenes.UserId = ( Guid ) Membership.GetUser().ProviderUserKey;

                imagenes.EsAprobado = 1;

                imagenes.Titulo = txtTituloImagen.Text;

                imagenes.DirectorioRelativo = AplicacionWeb.Miembros.EnviarAporte.DirectorioRelativo;

                imagenes.RutaRelativa = AplicacionWeb.Miembros.EnviarAporte.RutaRelativa;

                imagenes.EnlaceExterno = txtEnlaceExterno.Text;

                imagenes.EtiquetasBasicas = txtPersonaje.Text + " " + txtEquipo.Text + " " + txtLiga.Text;

                imagenes.EtiquetasOpcionales = txtEtiquetasOpcionales.Text;

                imagenes.FechaSubida = DateTime.Now;

                // Va en ceros porque aun no se sabe si será publicada.

                imagenes.FechaPublicacion = new DateTime ( 2001, 01, 01 );

                imagenes.Guardar();

                ReiniciarValores();

                Redireccionar();
                
            }
            else if ( string.IsNullOrEmpty ( AplicacionWeb.Miembros.EnviarAporte.RutaRelativa ) )
            {

                // Valiendo madre, no se pueden guardar los datos de la imgAprobada.

                lblError.Visible = true;

                lblError.Text = "Error al guardar datos de aporte";

            }

        }

        private void MostrarImagenes()
        {

            System.IO.DirectoryInfo _dirInfo = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(SubirArchivo.DestinationFolder));

            if ( _dirInfo.Exists )
            {
                System.IO.FileInfo[] _filesInfo = _dirInfo.GetFiles();
                foreach (System.IO.FileInfo _f in _filesInfo)
                {
                    Image _img = new Image();
                    _img.ImageUrl = string.Format( "{0}/{1}", SubirArchivo.DestinationFolder, _f );
                    _img.Height = new Unit ( 50 );
                    pnlImagenes.Controls.Add ( _img );
                }
            }
            else
            {
                pnlImagenes.Controls.Add ( new Label { Text = "Aún no se han subido archivos." } );
            }

        }

        private void SubirImagenes() 
        {
        
             SubirArchivo.UploadFiles ( true );

        }

        private void CargarCaracteristicasControlImagenes() 
        { 
        
            if ( !this.Page.IsPostBack )
            {

                // Propiedades del control.
                
                SubirArchivo.Titulo = "Subir imágenes";
                
                SubirArchivo.Comment = "1 archivo .png, .gif ó .jpg (máx. 10 MB).";
                
                SubirArchivo.MaxFilesLimit = 5;
                
                string fechaHoy = DateTime.Today.ToShortDateString();     
                
                SubirArchivo.DestinationFolder = "~/Aportes/" + fechaHoy.Replace ( '/', '-' ); // única propiedad obligatoria.

                AplicacionWeb.Controles.SubirArchivo.DirectorioRelativo = '\\' + "Aportes" + '\\' + fechaHoy.Replace('/', '-'); 

                AplicacionWeb.Controles.SubirArchivo.RutaRelativa = '\\' + "Aportes" + '\\' + fechaHoy.Replace('/', '-'); 
                
                SubirArchivo.FileExtensionsEnabled = ".png|.jpg|.jpeg|.jpe|.gif";
                
            }

        }

        private void CargarCategorias() 
        {

            if ( ddlCategoria.Items.Count == 0 )
            {

                try
                {

                    ddlCategoria.DataSource = Entidades.Categorias.ObtenerListado();
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();

                }
                catch ( Exception )
                {

                    throw;

                }
                
            }

        }

        private void ReiniciarValores()
        {

            AplicacionWeb.Miembros.EnviarAporte.RutaRelativa = string.Empty;

        }
        
        private void ReiniciarFormulario() 
        {
            
            txtTituloImagen.Text = string.Empty;

            ddlCategoria.SelectedIndex = 0;

            txtEnlaceExterno.Text = string.Empty;

            txtPersonaje.Text = string.Empty;

            txtEquipo.Text = string.Empty;

            txtLiga.Text = string.Empty;

            txtEtiquetasOpcionales.Text = string.Empty;
            
        }

        private void AgregarOnClick()
        {

            txtPersonaje.Attributes.Add ( "OnClick", "this.value = '#'" );

            txtEtiquetasOpcionales.Attributes.Add ( "OnClick", "this.value = '#'" );

            txtEquipo.Attributes.Add ( "OnClick", "this.value = '#'" );
             
            txtLiga.Attributes.Add ( "OnClick", "this.value = '#'" );

        }

        private void AgregarOnChange()
        {

            txtPersonaje.Attributes.Add ( "OnChange", "this.value = '#'" );

            txtEtiquetasOpcionales.Attributes.Add ( "OnChange", "this.value = '#'" );

            txtEquipo.Attributes.Add ( "OnChange", "this.value = '#'" );

            txtLiga.Attributes.Add ( "OnChange", "this.value = '#'" );

        }

        private void Redireccionar() 
        {

            Response.Redirect ( "~/AporteEnviado.aspx" );

        }
        #endregion 

    }
}