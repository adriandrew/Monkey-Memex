using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.IO;

namespace LogicaNegocio
{
    public class Autoverificacion
    {

        public string AutoverificarSolucion() 
        {
            string retornar = string.Empty;

            DirectoryInfo carpetaAplicacionWeb = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);

            DirectoryInfo carpetaRaiz = carpetaAplicacionWeb.Parent;

            string nombreCarpetaRaiz = carpetaRaiz.FullName;
            
            if ( carpetaRaiz.Exists )
            {

                DirectoryInfo[] informacionCarpetaRaiz = carpetaRaiz.GetDirectories();

                List<DirectoryInfo> listaCarpetasRaiz = informacionCarpetaRaiz.ToList();

                listaCarpetasRaiz.ForEach ( delegate ( DirectoryInfo elementoCarpetaRaiz )
                {

                    if ( elementoCarpetaRaiz.Name.Equals ( "AplicacionWeb" ) || elementoCarpetaRaiz.Name.Equals ( "Entidades" ) || elementoCarpetaRaiz.Name.Equals ( " LogicaNegocio" ) ) 
                    {



                        retornar = " Exitoso ";
                        
                    }
                    else if (! elementoCarpetaRaiz.Name.Equals("AplicacionWeb") || ! elementoCarpetaRaiz.Name.Equals("Entidades") || ! elementoCarpetaRaiz.Name.Equals(" LogicaNegocio"))
                    {

                        retornar = retornar + System.Environment.NewLine + " No existe carpeta Aplicacion Web, Entidades o LogicaNegocio ";

                    }

                } );                
                
            }
            else if ( ! carpetaRaiz.Exists )
            {

                retornar = retornar + System.Environment.NewLine + " No existe carpeta Raiz ";

            }

                return retornar;

        }

    }
}
