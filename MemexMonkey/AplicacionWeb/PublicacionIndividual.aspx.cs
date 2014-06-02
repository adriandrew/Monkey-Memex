using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public partial class PublicacionIndividual : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                // TODO Propiedad para definir boton por default en el formulario.

                //this.Form.DefaultButton = uiEnviarComentario.ID;

                // Esto es para validar el id que se pasa por el enrutamiento.

                int idImagen = 0;

                string id = Page.RouteData.Values["idImagen"].ToString();

                if (int.TryParse(id, out idImagen))

                    CargarCaracteristicas(idImagen);

            }
                        
        }

        protected void uiEnviarComentario_Click(object sender, EventArgs e)
        {

            if ( IsPostBack )
            {

                GuardarComentario();      

            }


        }

        private void GuardarComentario() 
        {

            uiImagen.Visible = false;

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

    }
}