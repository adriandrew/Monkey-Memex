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
                
                // TODO Propiedad para definir btnAprobarCopia por default en el formulario.

                //this.Form.DefaultButton = uiEnviarComentario.ID;

                // Esto es para validar el id que se pasa por el enrutamiento.

                string idImagenString = Page.RouteData.Values["idImagen"].ToString();

                int idImagenEntero = 0;

                if ( int.TryParse ( idImagenString, out idImagenEntero ) )

                    Session["idImagen"] = idImagenEntero;

                    MostrarImagen2 ( idImagenEntero );

                    MostrarComentarios2();

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

        private void MostrarComentarios2()
        {

            Entidades.ComentariosAspNet_Users comentarios = new Entidades.ComentariosAspNet_Users();

            List<Entidades.ComentariosAspNet_Users> listaComentarios = new List<Entidades.ComentariosAspNet_Users>();

            int idImagen = Convert.ToInt32(Session["idImagen"]);

            listaComentarios = comentarios.ObtenerListadoPorIdImagen(idImagen);

            foreach (Entidades.ComentariosAspNet_Users elementoComentarios in listaComentarios)
            {

                string idComentario = elementoComentarios.IdComentario.ToString();

                string userId = elementoComentarios.UserId.ToString();

                // idImagen ya tenemos su valor.

                string comentario = elementoComentarios.Comentario;

                string fechaPublicacion = elementoComentarios.FechaPublicacion.ToString();

                string meGusta = elementoComentarios.MeGusta.ToString();

                string applicationId = elementoComentarios.ApplicationId.ToString();

                string userName = elementoComentarios.UserName;

                string loweredUserName = elementoComentarios.LoweredUserName;

                string mobileAlias = elementoComentarios.MobileAlias;

                string isAnonymous = elementoComentarios.IsAnonymous.ToString();

                string lastActivityDate = elementoComentarios.LastActivityDate.ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl uiComentariosPorUsuariosMemex = CrearDivComentariosPorUsuariosMemex();

                uiComentariosMemex.Controls.Add(uiComentariosPorUsuariosMemex);

                System.Web.UI.HtmlControls.HtmlAnchor uiNombreUsuarioComentador = CrearAnchorComentariosPorUsuariosMemex(userName);

                uiComentariosPorUsuariosMemex.Controls.Add(uiNombreUsuarioComentador);

                System.Web.UI.HtmlControls.HtmlGenericControl uiAreaComentariosPorUsuariosMemex = CrearParagraphComentariosPorUsuariosMemex(comentario);

                uiComentariosPorUsuariosMemex.Controls.Add(uiAreaComentariosPorUsuariosMemex);

            }

        }

        // TODO Pendiente rellenar con la informacion correspondiente.

        private void MostrarImagen ( int idImagen )
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

                //uiComentariosMemex.Attributes.Add ( "OnClick", "alert('id')" );

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

        private void MostrarImagen2(int idImagen)
        {

            Entidades.ImagenesAspNet_Users imagenes = new Entidades.ImagenesAspNet_Users();

            List<Entidades.ImagenesAspNet_Users> listaImagen = imagenes.ObtenerPorIdImagen(idImagen);

            if (listaImagen.Count == 1)
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

                string applicationId = listaImagen[0].ApplicationId.ToString();

                string userName = listaImagen[0].UserName;

                string loweredUserName = listaImagen[0].LoweredUserName;

                string mobileAlias = listaImagen[0].MobileAlias;

                string isAnonymous = listaImagen[0].IsAnonymous.ToString();

                string lastActivityDate = listaImagen[0].LastActivityDate.ToString();

                uiTituloImagen.InnerText = titulo;

                uiUsuarioAportador.InnerText = string.Format ("{0}{1}", "Aporte por: ", userName );

                uiFechaPublicacion.InnerText = fechaPublicacion;

                uiImagen.Src = string.Format("{0}{1}", ".", rutaRelativa );

                uiImagen.Alt = "No se encuentra la imagen :(";

                uiEtiquetas.InnerText = string.Format("{0} | {1}", etiquetasBasicas, etiquetasOpcionales);

                uiPluginComentariosFacebook.Attributes.Add("style", "display: none;");

                uiPluginComentariosFacebook.Attributes.Add("data-href", string.Format("{0}{1}", "http://monkey.somee.com/PublicacionIndividual/", idImagen));

                uiComentariosMemex.Attributes.Add("style", "display: none;");

                if (User.Identity.IsAuthenticated)
                {

                    uiAreaComentario.Attributes.Add("style", "display: inline;");

                    uiEnviarComentario.Attributes.Add("style", "display: inline;");

                }
                else if (!User.Identity.IsAuthenticated)
                {

                    uiAreaComentario.Attributes.Add("style", "display: none;");

                    uiEnviarComentario.Attributes.Add("style", "display: none;");

                }

                //uiComentariosMemex.Attributes.Add ( "OnClick", "alert('id')" );

                //uiComentariosMemex.Attributes.Add("OnClick", "Response.Redirect('~/Default.aspx')");

            }
            else if (listaImagen.Count != 1)
            {

                string script = @"<script type='text/javascript'> alert('{0}'); </script>";

                script = string.Format(script, "Esta publicacion no pudo ser cargada :(");

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alerta", script, false);

                Response.Redirect("~/Default.aspx");

            }

        }

        private System.Web.UI.HtmlControls.HtmlGenericControl CrearDivComentariosPorUsuariosMemex()
        {

            System.Web.UI.HtmlControls.HtmlGenericControl divComentariosPorUsuarioMemex = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divComentariosPorUsuarioMemex.Attributes.Add ("id",  "uiComentariosPorUsuario");
                                   
            return divComentariosPorUsuarioMemex;

        }

        private System.Web.UI.HtmlControls.HtmlAnchor CrearAnchorComentariosPorUsuariosMemex ( string nombreUsuario )
        {

            System.Web.UI.HtmlControls.HtmlAnchor aComentariosPorUsuarioMemex = new System.Web.UI.HtmlControls.HtmlAnchor();

            aComentariosPorUsuarioMemex.HRef = "#";

            aComentariosPorUsuarioMemex.InnerText = nombreUsuario;

            return aComentariosPorUsuarioMemex;

        }

        private System.Web.UI.HtmlControls.HtmlGenericControl CrearParagraphComentariosPorUsuariosMemex ( string comentario )
        {

            System.Web.UI.HtmlControls.HtmlGenericControl pComentariosPorUsuarioMemex = new System.Web.UI.HtmlControls.HtmlGenericControl("p");

            pComentariosPorUsuarioMemex.InnerText = comentario;

            return pComentariosPorUsuarioMemex;

        }
        
    }
}