using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public partial class Individual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                // Esto es para validar el id que se pasa por el enrutamiento.

                string idImagenString = Page.RouteData.Values["idImagen"].ToString();

                int idImagenEntero = 0;

                if (int.TryParse(idImagenString, out idImagenEntero))

                    Session["idImagen"] = idImagenEntero;

                MostrarImagen(idImagenEntero);

                MostrarComentarios();

            }

        }

        protected void uiEnviarComentario_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {

                try
                {

                    if (User.Identity.IsAuthenticated)
                    {

                        GuardarComentario();

                        LimpiarComentarios();

                    }

                }
                catch (Exception)
                {

                    Response.Redirect("Inicio");

                }

            }

        }

        private void MostrarImagen(int idImagen)
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

                string tituloImagen = string.Format("<h2>{0}</h2>", titulo);

                string nombreUsuario = string.Format("<h3>{0}{1}</h3>", "Aporte por: ", userName);

                string fechaPublicacionImagen = string.Format("<h4>{0}</h4>", fechaPublicacion);

                string archivoImagen = string.Format("<img src='{0}{1}' alt='{2}'>", "..", rutaRelativa, titulo);

                string etiquetas = string.Format("<h4>{0} | {1}</h4>", etiquetasBasicas, etiquetasOpcionales);

                string contenidoDivImagen = string.Format("{0}{1}{2}{3}{4}", tituloImagen, nombreUsuario, fechaPublicacionImagen, archivoImagen, etiquetas);

                string divImagen = string.Format("<div class={0}>{1}</div>", "divImagen", contenidoDivImagen);

                contenedorImagenes.InnerHtml = divImagen;


                uiPluginComentariosFacebook.Attributes.Add("style", "display: none;");

                uiPluginComentariosFacebook.Attributes.Add("data-href", string.Format("{0}{1}", "http://monkey.somee.com/Individual/", idImagen));

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

            }
            else if (listaImagen.Count != 1)
            {

                string script = @"<script type='text/javascript'> alert('{0}'); </script>";

                script = string.Format(script, "Esta publicacion no pudo ser cargada :(");

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alerta", script, false);

                System.Threading.Thread.Sleep(5000);

                Response.Redirect("~/Default.aspx");

            }

        }

        private void MostrarComentarios()
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

                string linkUsuario = string.Format("<a href='{0}'>{1}</a>", "#", userName);

                string comentarioUsuario = string.Format("<p>{0}</p>", comentario);

                string divComentariosMemex = string.Format("<div id={0}>{1}{2}</div>", "uiComentariosPorUsuario", linkUsuario, comentarioUsuario);

                uiComentariosMemex.InnerHtml = divComentariosMemex;

            }

        }

        private void LimpiarComentarios()
        {

            uiAreaComentario.Value = string.Empty;

        }

        private void GuardarComentario()
        {
  
            Entidades.Comentarios comentarios = new Entidades.Comentarios();

            comentarios.UserId = (Guid)Membership.GetUser().ProviderUserKey;

            comentarios.IdImagen = Convert.ToInt32(Session["idImagen"]);

            comentarios.Comentario = uiAreaComentario.Value;

            comentarios.FechaPublicacion = DateTime.Now;

            comentarios.MeGusta = 0;

            comentarios.Guardar();

        }

    }
}