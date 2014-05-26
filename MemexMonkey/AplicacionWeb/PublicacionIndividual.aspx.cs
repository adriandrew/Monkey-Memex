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

            int idImagen = 0;

            string id = Page.RouteData.Values["idImagen"].ToString();

            if ( int.TryParse ( id,  out idImagen ) )
 
            CargarCaracteristicas ( idImagen );

        }


        // TODO Pendiente rellenar con la informacion correspondiente.

        private void CargarCaracteristicas ( int idImagen )
        {

            Entidades.Imagenes imagenes = new Entidades.Imagenes();

            // TODO Metodo pendiente de crear, buscar la imagen por id.

            imagenes.ObtenerPorIdImagen ( idImagen );

            // TODO Pienso mejor obtenerlos en una lista ya que es mas facil de usar con objetos que una string con split.

            imgIndividual.ImageUrl = imagenes.RutaRelativa;

        }

    }
}