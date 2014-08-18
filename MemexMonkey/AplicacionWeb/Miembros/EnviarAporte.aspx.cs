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
        
        private static string directorioRelativo = string.Empty;

        private static string rutaRelativa = string.Empty;

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

        #region Variables Globales

        private static int opcionSeleccionada = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            CargarCategorias();

            CargarCaracteristicasControlImagenes();

            AgregarOnFocus();

        }

        protected void rblEscoger_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ( rblEscoger.SelectedIndex == 0 )
            {

                opcionSeleccionada = 0;

                ucSubirArchivo.Visible = true;

                lblEnlaceExterno.Visible = false;

                txtEnlaceExterno.Visible = false;

                rfvEnlaceExterno.Visible = false;

                revEnlaceExterno.Visible = false;

            }
            else if ( rblEscoger.SelectedIndex == 1 )
            {

                opcionSeleccionada = 1;

                ucSubirArchivo.Visible = false;

                lblEnlaceExterno.Visible = true;

                txtEnlaceExterno.Visible = true;

                rfvEnlaceExterno.Visible = true;

                revEnlaceExterno.Visible = true;

            }

        }

        protected void btnEnviarAporte_Click(object sender, EventArgs e)
        {

            if ( opcionSeleccionada == 0 )
            {

                SubirImagen();

                GuardarAporte();

            }
            else if ( opcionSeleccionada == 1)
            {

                GuardarAporte();

            }

        }

        protected void btnReiniciar_Click(object sender, EventArgs e)
        {

            ReiniciarFormulario();

        }

        #endregion

        #region Metodos Privados

        private void GuardarAporte() 
        {

            // La primera opcion verifica si es archivo de imagen y si se pudo subir exitosamente para guardar el registro, la segunda opcion guarda normal.

            if ( ( opcionSeleccionada == 0 && ! string.IsNullOrEmpty ( AplicacionWeb.Miembros.EnviarAporte.RutaRelativa ) )  || ( opcionSeleccionada == 1 ) )
            {

                // Para guardar los datos que van en la tabla Imagenes.

                Entidades.Imagenes imagenes = new Entidades.Imagenes();

                imagenes.IdCategoria = Convert.ToInt32 ( ddlCategoria.SelectedValue );

                imagenes.UserId = ( Guid ) Membership.GetUser().ProviderUserKey;

                imagenes.EsAprobado = 1;

                imagenes.Titulo = txtTituloImagen.Text;

                imagenes.DirectorioRelativo = AplicacionWeb.Miembros.EnviarAporte.DirectorioRelativo;

                imagenes.RutaRelativa = AplicacionWeb.Miembros.EnviarAporte.RutaRelativa;

                imagenes.EnlaceExterno = txtEnlaceExterno.Text;

                imagenes.EtiquetasBasicas = txtPersonaje.Text + " " + txtEquipo.Text + " " + txtCompeticion.Text;

                imagenes.EtiquetasOpcionales = txtEtiquetasOpcionales.Text;

                imagenes.FechaSubida = DateTime.Now;

                // Va en ceros porque aun no se sabe si será publicada.

                imagenes.FechaPublicacion = new DateTime ( 2001, 01, 01 );

                imagenes.Guardar();

                ReiniciarValores();

                Redireccionar();
                
            }
            else 
            {

                lblError.Visible = true;

                lblError.Text = "Error al guardar datos de aporte";

            }

        }

        private void SubirImagen() 
        {

            AplicacionWeb.Controles.SubirArchivo.TituloImagen = txtTituloImagen.Text;
        
            ucSubirArchivo.SubirArchivos ( true );

        }

        private void CargarCaracteristicasControlImagenes() 
        { 
        
            if ( ! this.Page.IsPostBack )
            {

                // Propiedades del control.
                
                ucSubirArchivo.Titulo = "Subir imágenes";
                
                ucSubirArchivo.Nota = "1 archivo .png, .jpg ó .gif  (máx. 10 MB).";
                
                string fechaHoy = DateTime.Today.ToShortDateString();     
                
                ucSubirArchivo.FolderDestino = "~/Aportes/" + fechaHoy.Replace ( '/', '-' ); // única propiedad obligatoria.

                AplicacionWeb.Controles.SubirArchivo.DirectorioRelativo = '\\' + "Aportes" + '\\' + fechaHoy.Replace('/', '-'); 

                AplicacionWeb.Controles.SubirArchivo.RutaRelativa = '\\' + "Aportes" + '\\' + fechaHoy.Replace('/', '-'); 
                
                ucSubirArchivo.extensionesArchivosPermitidas = ".png|.jpg|.jpeg|.jpe|.gif";
                
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

            txtCompeticion.Text = string.Empty;

            txtEtiquetasOpcionales.Text = string.Empty;
            
        }

        private void AgregarOnFocus()
        {

            txtPersonaje.Attributes.Add ( "OnFocusIn", "this.value = '#'" );

            txtEtiquetasOpcionales.Attributes.Add ( "OnFocusIn", "this.value = '#'" );

            txtEquipo.Attributes.Add("OnFocusIn", "this.value = '#'");

            txtCompeticion.Attributes.Add("OnFocusIn", "this.value = '#'");

        }

        private void Redireccionar() 
        {

            Response.Redirect ( "AporteEnviado" );

        }

        #endregion 

    }
}