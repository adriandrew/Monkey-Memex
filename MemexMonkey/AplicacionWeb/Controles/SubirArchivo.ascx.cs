using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AplicacionWeb.Controles
{

    public partial class SubirArchivo : System.Web.UI.UserControl
    {

        #region Campos.

        private static string tituloImagen;

        private static string directorioRelativo;

        private static string rutaRelativa;

        #endregion

        #region Propiedades.

        public static string TituloImagen
        {
            get { return SubirArchivo.tituloImagen; }
            set { SubirArchivo.tituloImagen = value; }
        }

        public static string DirectorioRelativo
        {
            get { return SubirArchivo.directorioRelativo; }
            set { SubirArchivo.directorioRelativo = value; }
        }

        public static string RutaRelativa
        {
            get { return SubirArchivo.rutaRelativa; }
            set { SubirArchivo.rutaRelativa = value; }
        }

        /// <summary>
        /// Título a mostrar en el control.
        /// </summary>
        public string Titulo { get { return lblTitulo.Text; } set { lblTitulo.Text = value; } }

        /// <summary>
        /// Nota o comentario.
        /// </summary>
        public string Nota { get { return lblNota.Text; } set { lblNota.Text = value; } }

        /// <summary>
        /// Carpeta donde se guardarán los archivos en el servidor.
        /// </summary>
        public string FolderDestino { get { return ViewState[this.ID + "DESTINATIONFOLDER"].ToString(); } set { ViewState[this.ID + "DESTINATIONFOLDER"] = value; } }

        /// <summary>
        /// Extensiones de archivo permitidas, separadas por "|" (pipe). Si no se especifica (es Null), se permite subir archivos con cualquier extensión.
        /// </summary>
        public string extensionesArchivosPermitidas { get { return ViewState[this.ID + "FILEEXTENSIONSENABLED"].ToString(); } set { ViewState[this.ID + "FILEEXTENSIONSENABLED"] = value; } }

        /// <summary>
        /// Límite de subida del total de los archivos, en bytes.
        /// </summary>
        public int maximoTamanoSubida { get { return Convert.ToInt32(ViewState[this.ID + "MAXUPLOADSIZE"]); } set { ViewState[this.ID + "MAXUPLOADSIZE"] = value; } }
        
        #endregion

        #region Variables Globales.

        // La longitud maxima del archivo es de 10 mb y está escrita en kilobytes.
        
        private int tamanoMaximo = 10240;

        #endregion

        #region Eventos.

        protected void Page_Load(object sender, EventArgs e)
        {

            if (ViewState[this.ID + "MAXUPLOADSIZE"] == null)

                ViewState[this.ID + "MAXUPLOADSIZE"] = tamanoMaximo;

        }

        #endregion

        #region Metodos Publicos.

        /// <summary>
        /// Sube los archivos seleccionados en los controles. Retorna False si por algún motivo no se guardaron los archivos, y True si subieron bien.
        /// </summary>
        /// <param name="esDirectorioInexistente"></param>
        public bool SubirArchivos(bool esDirectorioInexistente)
        {

            bool esResultadoExitoso = false;

            string rutaDirectorio = Server.MapPath(ViewState[this.ID + "DESTINATIONFOLDER"].ToString());

            if (esDirectorioInexistente)
            {

                DirectoryInfo directorio = new DirectoryInfo(rutaDirectorio);
                
                if (!directorio.Exists)
            
                    directorio.Create();
            
            }

            HttpFileCollection archivosCargados = Request.Files;

            if (ValidarExtensiones(archivosCargados))
            {

                if (ValidarTamaño(archivosCargados))
                {

                    #region Guardar archivos

                    try
                    {

                        double tamanoArchivo = 0;

                        int cantidadArchivos = 0;

                        string extensionArchivo = string.Empty;

                        for (int i = 0; i < archivosCargados.Count; i++)
                        {

                            HttpPostedFile archivoPosteado = archivosCargados[i];

                            if (archivoPosteado.ContentLength > 0)
                            {

                                extensionArchivo = Path.GetExtension(archivoPosteado.FileName);

                                string archivoGuardar = string.Format ( "{0}\\{1}", rutaDirectorio, AplicacionWeb.Controles.SubirArchivo.TituloImagen + extensionArchivo );

                                // Esta ruta relativa es la que se va a guardar en la base de datos.

                                rutaRelativa = string.Format ( "{0}\\{1}", rutaRelativa, AplicacionWeb.Controles.SubirArchivo.TituloImagen + extensionArchivo );

                                archivoPosteado.SaveAs(archivoGuardar);

                                tamanoArchivo += archivoPosteado.ContentLength;

                                cantidadArchivos++;

                                // Aqui se envia el directorio relativo de cada una de las imagenes del miembro de Memex.

                                AplicacionWeb.Miembros.EnviarAporte.DirectorioRelativo = directorioRelativo;

                                // Aqui se envia la ruta relativa de cada una de las imagenes del miembro de Memex.

                                AplicacionWeb.Miembros.EnviarAporte.RutaRelativa = rutaRelativa;
                                
                            }

                        }

                        if (cantidadArchivos > 0)
                        {

                            if (tamanoArchivo < 1024)

                                lblInformacion.Text = string.Format("{0:0.00} KB subidos en {1} archivos.", tamanoArchivo / 1024, cantidadArchivos);
                            
                            else
                            
                                lblInformacion.Text = string.Format("{0} KB subidos en {1} archivos.", Math.Round(tamanoArchivo / 1024), cantidadArchivos);

                            lblInformacion.CssClass = "mensajeCorrecto";

                            esResultadoExitoso = true;

                        }
                        else
                        {

                            lblInformacion.Text = "No se subieron archivos. Seleccione un archivo para subir.";

                            lblInformacion.CssClass = "mensajeError";

                        }
                           
                    }
                    catch (Exception ex)
                    {

                        lblInformacion.Text = string.Format("Se produjo un error al subir los archivos: {0}", ex.Message);

                        lblInformacion.CssClass = "mensajeError";

                    }

                    #endregion

                }
                else
                {

                    lblInformacion.Text = string.Format("Los archivos a subir superan el tamaño permitido de {0} MB.", Convert.ToInt32(ViewState[this.ID + "MAXUPLOADSIZE"]) / 1024);
                    
                    lblInformacion.CssClass = "mensajeError";

                }

            }
            else
            {

                lblInformacion.Text = "Uno o más archivos contiene una extensión no permitida. Los archivos no fueron subidos.";

                lblInformacion.CssClass = "mensajeError";

            }

            return esResultadoExitoso;

        }

        #endregion

        #region Metodos Protegidos.

        /// <summary>
        /// Verifica las extensiones de archivo permitidas. Si no se especificó ninguna, se puede subir cualquier tipo de archivo.
        /// </summary>
        /// <param name="archivosCargados"></param>
        /// <returns></returns>
        protected bool ValidarExtensiones(HttpFileCollection archivosCargados)
        {
            
            if (ViewState[this.ID + "FILEEXTENSIONSENABLED"] == null)
                return true;

            bool esValida = true;

            // Establece la expresión regular de validación.

            string expresionRegular = string.Format("({0})$", ViewState[this.ID + "FILEEXTENSIONSENABLED"]); 

            for (int i = 0; i < archivosCargados.Count; i++)
            {
                RegexOptions opcionesExpresion = RegexOptions.IgnoreCase

                    | RegexOptions.Singleline

                    | RegexOptions.Compiled;

                Regex resultado = new Regex(expresionRegular, opcionesExpresion);

                // Si alguna extensión no coincide cancela la subida de todos los archivos.
                
                if (resultado.Match(archivosCargados[i].FileName).Length == 0) 
                {

                    esValida = false;

                    break;

                }

            }

            return esValida;

        }

        /// <summary>
        /// Controla el tamaño total de todos los archivos a subir. Si no se especifica, utiliza el máximo definido en tamanoMaximo.
        /// </summary>
        /// <param name="archivosCargados"></param>
        /// <returns></returns>
        protected bool ValidarTamaño(HttpFileCollection archivosCargados)
        {

            int tamanoTotal = 0;

            for (int i = 0; i < archivosCargados.Count; i++)

                tamanoTotal += archivosCargados[i].ContentLength;

            return tamanoTotal < Convert.ToInt32(ViewState[this.ID + "MAXUPLOADSIZE"]) * 1024;

        }

        #endregion

    }
}