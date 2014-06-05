using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public partial class PublicacionIndividual : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if ( ! IsPostBack )
            {
                
                // TODO Propiedad para definir boton por default en el formulario.

                //this.Form.DefaultButton = uiEnviarComentario.ID;

                // Esto es para validar el id que se pasa por el enrutamiento.

                string idImagenString = Page.RouteData.Values["idImagen"].ToString();

                int idImagenEntero = 0;

                if ( int.TryParse ( idImagenString, out idImagenEntero ) )

                    Session["idImagen"] = idImagenEntero;

                    CargarCaracteristicas ( idImagenEntero );

                    MostrarComentarios();

            }
                        
        }

        protected void uiEnviarComentario_Click(object sender, EventArgs e)
        {

            if ( IsPostBack )
            {

                try
                {

                    if ( User.Identity.IsAuthenticated )
                    {

                        GuardarComentario();

                        LimpiarComentarios();

                    }

                }
                catch (Exception)
                {

                    uiImagen.Visible = false;

                }

            }

        }

        private void LimpiarComentarios()
        {

            uiAreaComentario.Value = string.Empty;
        
        }

        private void GuardarComentario() 
        {

            Entidades.Comentarios comentarios = new Entidades.Comentarios();

            comentarios.UserId = ( Guid ) Membership.GetUser().ProviderUserKey;

            comentarios.IdImagen = Convert.ToInt32( Session["idImagen"] );

            comentarios.Comentario = uiAreaComentario.Value;

            comentarios.FechaPublicacion = DateTime.Now;

            comentarios.MeGusta = 0;

            comentarios.Guardar();

        }

        private void MostrarComentarios()
        {

            Entidades.Comentarios comentarios = new Entidades.Comentarios();
            
            List < Entidades.Comentarios> listaComentarios = new List < Entidades.Comentarios >();

            int idImagen = Convert.ToInt32 ( Session["idImagen"] ) ;

            listaComentarios = comentarios.ObtenerListadoPorIdImagen ( idImagen );

            foreach ( Entidades.Comentarios elementoComentarios in listaComentarios )
            {

                string idComentario = elementoComentarios.IdComentario.ToString();

                string userId = elementoComentarios.UserId.ToString();

                // idImagen ya tenemos su valor.

                string comentario = elementoComentarios.Comentario;

                string fechaPublicacion = elementoComentarios.FechaPublicacion.ToString();

                string meGusta = elementoComentarios.MeGusta.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl uiComentariosPorUsuariosMemex = CrearDivComentariosPorUsuariosMemex();

                uiComentariosMemex.Controls.Add ( uiComentariosPorUsuariosMemex );

                System.Web.UI.HtmlControls.HtmlAnchor uiNombreUsuarioComentador = CrearAnchorComentariosPorUsuariosMemex ( userId );

                uiComentariosPorUsuariosMemex.Controls.Add ( uiNombreUsuarioComentador );

                System.Web.UI.HtmlControls.HtmlGenericControl uiAreaComentariosPorUsuariosMemex = CrearParagraphComentariosPorUsuariosMemex(comentario);

                uiComentariosPorUsuariosMemex.Controls.Add ( uiAreaComentariosPorUsuariosMemex );
                
            }

        }
        
        // TODO Pendiente rellenar con la informacion correspondiente.

        private void CargarCaracteristicas ( int idImagen )
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
                
                uiTituloImagen.InnerText = titulo;

                uiUsuarioAportador.InnerText = userId;

                uiFechaPublicacion.InnerText = fechaPublicacion;

                uiImagen.Src = rutaRelativa;

                uiEtiquetas.InnerText = string.Format ( "{0} | {1}", etiquetasBasicas, etiquetasOpcionales );

                uiPluginComentariosFacebook.Attributes.Add ( "style", "display: none;" );

                uiPluginComentariosFacebook.Attributes.Add ( "data-href", string.Format ( "{0}{1}", "http://monkey.somee.com/PublicacionIndividual/", idImagen ) );

                uiComentariosMemex.Attributes.Add ( "style", "display: none;" );

                if ( User.Identity.IsAuthenticated )
                {

                    uiAreaComentario.Attributes.Add("style", "display: inline;");

                    uiEnviarComentario.Attributes.Add("style", "display: inline;");

                }
                else if ( ! User.Identity.IsAuthenticated )
                {

                    uiAreaComentario.Attributes.Add("style", "display: none;");

                    uiEnviarComentario.Attributes.Add("style", "display: none;");

                }

                //uiComentariosMemex.Attributes.Add ( "OnClick", "alert('prueba')" );

                //uiComentariosMemex.Attributes.Add("OnClick", "Response.Redirect('~/Default.aspx')");

            }
            else if ( listaImagen.Count != 1 ) 
            {
                
                string script = @"<script type='text/javascript'> alert('{0}'); </script>";

                script = string.Format ( script, "Esta publicacion no pudo ser cargada :(" );

                ScriptManager.RegisterStartupScript ( this, typeof ( Page ), "Alerta", script, false );

                Response.Redirect ( "~/Default.aspx" );

            }

        }

        private System.Web.UI.HtmlControls.HtmlGenericControl CrearDivComentariosPorUsuariosMemex()
        {

            System.Web.UI.HtmlControls.HtmlGenericControl divComentariosPorUsuarioMemex = new System.Web.UI.HtmlControls.HtmlGenericControl();

            divComentariosPorUsuarioMemex.InnerHtml = string.Format ( "{0}", "<div id='uiComentariosPorUsuario'><div>" );
                                               
            return divComentariosPorUsuarioMemex;

        }

        private System.Web.UI.HtmlControls.HtmlAnchor CrearAnchorComentariosPorUsuariosMemex ( string userId )
        {

            System.Web.UI.HtmlControls.HtmlAnchor aComentariosPorUsuarioMemex = new System.Web.UI.HtmlControls.HtmlAnchor();

            aComentariosPorUsuarioMemex.InnerText = userId;

            return aComentariosPorUsuarioMemex;

        }

        private System.Web.UI.HtmlControls.HtmlGenericControl CrearParagraphComentariosPorUsuariosMemex ( string comentario )
        {

            System.Web.UI.HtmlControls.HtmlGenericControl pComentariosPorUsuarioMemex = new System.Web.UI.HtmlControls.HtmlGenericControl();

            pComentariosPorUsuarioMemex.InnerHtml = string.Format ( "{0}{1}{2}", "<p>", comentario, "</p>" ) ;

            return pComentariosPorUsuarioMemex;

        }


    }
}