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

            //if ( ! IsPostBack )
            //{

            //    int idImagen = 0;

            //    string id = Page.RouteData.Values [ "idImagen" ].ToString();

            //    if ( int.TryParse ( id, out idImagen ) )

            //    CargarCaracteristicas ( idImagen );

            //}

        }


        // TODO Pendiente rellenar con la informacion correspondiente.

        private void CargarCaracteristicas ( int idImagen )
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            List < Entidades.Imagenes > listaImagen = imagenes.ObtenerPorIdImagen ( idImagen );

            if ( listaImagen.Count == 1 )
            {

                // TODO Faltan poner los demas parametros.

                tituloImagen.InnerHtml = listaImagen [ 0 ].Titulo;

                imagenIndividual.Src = listaImagen[0].RutaRelativa;



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