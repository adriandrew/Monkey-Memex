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
        protected void Page_Load(object sender, EventArgs e)
        {

            //InsertarDiv();

        }



        // Mierda, tengo que encontrar la manera mas optima para mostrar las putas imagenes, tengo que consultarlo con la almohada..
        private void MostrarImagenes(string ruta)
        {
           
            System.IO.DirectoryInfo directorioInfo = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(ruta));

            if (directorioInfo.Exists)
            {

                System.IO.FileInfo[] _filesInfo = directorioInfo.GetFiles();
                
                foreach (System.IO.FileInfo f in _filesInfo)
                {
                    Image imagen = new Image();
                    imagen.ImageUrl = string.Format("{0}/{1}", ruta, f);
                    imagen.Height = new Unit(50);
                    pnlImagenes.Controls.Add(imagen);
                }

            }
            else
            {

                pnlImagenes.Controls.Add(new Label { Text = "Aún no se han subido archivos." });
            
            }
                   
        }

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

            


            imagenes.InnerHtml = "<h2> prueba </h2>";


            Panel panel = new Panel();

            panel.ID = "idPanel";

            pnlImagenes.Controls.Add(panel);

            Literal literal = new Literal();

            literal.Text = "<h2> Prueba </h2>";

            panel.Controls.Add(literal);

        }

    }
}