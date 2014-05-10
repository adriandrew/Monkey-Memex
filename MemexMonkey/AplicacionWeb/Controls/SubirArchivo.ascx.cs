using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AplicacionWeb.Controls
{

    public partial class SubirArchivo : System.Web.UI.UserControl
    {

        private static string tituloImagen;

        private static string directorioRelativo;

        private static string rutaRelativa;
        
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


        private int __MaxSize = 10240; // La longitud maxima del archivo es de 10 mb y está escrita en kilobytes.

        #region Propiedades

        /// <summary>
        /// Título a mostrar en el control.
        /// </summary>
        public string Titulo { get { return lblUploadFilesTitle.Text; } set { lblUploadFilesTitle.Text = value; } }
        /// <summary>
        /// Nota o comentario.
        /// </summary>
        public string Comment { get { return lblNota.Text; } set { lblNota.Text = value; } }
        /// <summary>
        /// Cantidad máxima de archivos que pueden subirse en una sola operación.
        /// </summary>
        public int MaxFilesLimit { get { return Convert.ToInt32(ViewState[this.ID + "MAXFILESLIMIT"]); } set { ViewState[this.ID + "MAXFILESLIMIT"] = value; } }
        /// <summary>
        /// Carpeta donde se guardarán los archivos en el servidor.
        /// </summary>
        public string DestinationFolder { get { return ViewState[this.ID + "DESTINATIONFOLDER"].ToString(); } set { ViewState[this.ID + "DESTINATIONFOLDER"] = value; } }
        /// <summary>
        /// Determina si el botón para subir archivos incluido en el control está visible. De manera predeterminada está oculto.
        /// </summary>
        public bool UploadButtonIsVisible { get { return Convert.ToBoolean(ViewState[this.ID + "UPLOADBUTTONISVISIBLE"]); } set { ViewState[this.ID + "UPLOADBUTTONISVISIBLE"] = value; } }
        /// <summary>
        /// Extensiones de archivo permitidas, separadas por "|" (pipe). Si no se especifica (es Null), se permite subir archivos con cualquier extensión.
        /// </summary>
        public string FileExtensionsEnabled { get { return ViewState[this.ID + "FILEEXTENSIONSENABLED"].ToString(); } set { ViewState[this.ID + "FILEEXTENSIONSENABLED"] = value; } }
        /// <summary>
        /// Indica si se seleccionó el primero de los archivos. 
        /// </summary>
        public bool HasFile { get { return fuExaminar.HasFile; } }
        /// <summary>
        /// Límite de subida del total de los archivos, en bytes.
        /// </summary>
        public int MaxUploadSize { get { return Convert.ToInt32(ViewState[this.ID + "MAXUPLOADSIZE"]); } set { ViewState[this.ID + "MAXUPLOADSIZE"] = value; } }
        
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            string _checkMssg = "Por favor seleccione el primer archivo antes de agregar otros.";

            StringBuilder _js = new StringBuilder("<script type=\"text/javascript\">");
            _js.Append("function addFileUploadCtrl(){");
            _js.AppendFormat("if(document.getElementById(\"{0}\").value.length == 0)", fuExaminar.ClientID);
            _js.Append("{");
            _js.AppendFormat("alert(\"{0}\");return;", _checkMssg);
            _js.Append("}");
            _js.Append("if (!document.getElementById || !document.createElement) return false;");
            _js.AppendFormat("var uploadArea = document.getElementById(\"{0}\");", pnlUpload.ClientID);
            _js.Append("if (!uploadArea) return;");
            _js.Append("var _uploadControl = document.createElement(\"input\");");
            _js.Append("if (!addFileUploadCtrl.newID) addFileUploadCtrl.newID = 1;");
            _js.Append("_uploadControl.type = \"file\";");
            _js.Append("_uploadControl.setAttribute(\"id\", \"upldFile_\" + addFileUploadCtrl.newID);");
            _js.Append("_uploadControl.setAttribute(\"name\", \"upldFile_\" + addFileUploadCtrl.newID);");
            _js.Append("_uploadControl.setAttribute(\"class\", \"fileInput\");");
            _js.Append("uploadArea.appendChild(_uploadControl);");
            _js.Append("addFileUploadCtrl.newID++;");

            //if (ViewState[this.ID + "MAXFILESLIMIT"] != null) // límite de cantidad de archivos.
            //{
            //    _js.AppendFormat("if(addFileUploadCtrl.newID > {0} -1)", ViewState[this.ID + "MAXFILESLIMIT"]);
            //    _js.AppendFormat("document.getElementById(\"{0}\").style.display = 'none';", lnkAgregarArchivoParaSubir.ClientID);
            //}

            _js.AppendFormat("document.getElementById(\"{0}\").style.display='none';", lblInformacion.ClientID); // limpiar mensaje de información.
            _js.Append("}</script>");

            if (this.Page.Master != null)
                this.Page.Master.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), _js.ToString());
            else
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), _js.ToString());


            base.OnPreRender(e);

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // Limite de archivos.
            //if (ViewState[this.ID + "MAXFILESLIMIT"] != null)
            //    lnkAgregarArchivoParaSubir.Visible = Convert.ToInt32(ViewState[this.ID + "MAXFILESLIMIT"]) > 1;

            if (ViewState[this.ID + "MAXUPLOADSIZE"] == null)
                ViewState[this.ID + "MAXUPLOADSIZE"] = __MaxSize;

            btnSubirArchivos.Visible = Convert.ToBoolean(ViewState[this.ID + "UPLOADBUTTONISVISIBLE"]);

        }

        protected void btnSubirArchivos_Click(object sender, EventArgs e)
        {

            UploadFiles(true);

        }

        /// <summary>
        /// Sube los archivos seleccionados en los controles. Retorna False si por algún motivo no se guardaron los archivos, y True si subieron bien.
        /// </summary>
        /// <param name="CreateDir"></param>
        public bool UploadFiles(bool CreateDir)
        {
            bool _resultOK = false;

            string _dirPath = Server.MapPath(ViewState[this.ID + "DESTINATIONFOLDER"].ToString());


            if (CreateDir)
            {
                DirectoryInfo _d = new DirectoryInfo(_dirPath);
                if (!_d.Exists)
                    _d.Create();
            }

            HttpFileCollection _fcol = Request.Files;
            if (ValidarExtensiones(_fcol))
            {
                if (ValidarTamaño(_fcol))
                {
                    #region Guardar archivos

                    try
                    {
                        double _fSizes = 0;
                        int _cantFiles = 0;
                        string extensionArchivo = string.Empty;

                        for (int i = 0; i < _fcol.Count; i++)
                        {
                            HttpPostedFile _postedF = _fcol[i];
                            if (_postedF.ContentLength > 0)
                            {
                                // Aquí va el nombre de la imgPendiente que se va a guardar.
                                //string _f2save = string.Format("{0}\\{1}", _dirPath, Path.GetFileName(_postedF.FileName));

                                extensionArchivo = Path.GetExtension(_postedF.FileName);

                                string _f2save = string.Format ( "{0}\\{1}", _dirPath, AplicacionWeb.Controls.SubirArchivo.TituloImagen + extensionArchivo );

                                // Este directorioRelativo es el que se va a guardar en la base de datos.
                                directorioRelativo = string.Format ( "{0}\\{1}", directorioRelativo ); 

                                // Esta rutaRelativa es la que se va a guardar en la base de datos.
                                rutaRelativa = string.Format ( "{0}\\{1}", rutaRelativa, AplicacionWeb.Controls.SubirArchivo.TituloImagen + extensionArchivo );

                                _postedF.SaveAs(_f2save);
                                _fSizes += _postedF.ContentLength;
                                _cantFiles++;

                                // Aqui se envia el directorio relativo de cada una de las imagenes del miembro de Memex.
                                AplicacionWeb.Miembros.EnviarAporte.DirectorioRelativo = directorioRelativo;

                                // Aqui se envia la ruta relativa de cada una de las imagenes del miembro de Memex.
                                AplicacionWeb.Miembros.EnviarAporte.RutaRelativa = rutaRelativa;
                                
                            }
                        }

                        if (_cantFiles > 0)
                        {
                            if (_fSizes < 1024)
                                lblInformacion.Text = string.Format("{0:0.00} KB subidos en {1} archivos.", _fSizes / 1024, _cantFiles);
                            else
                                lblInformacion.Text = string.Format("{0} KB subidos en {1} archivos.", Math.Round(_fSizes / 1024), _cantFiles);

                            lblInformacion.CssClass = "mssgOK";

                            _resultOK = true;
                        }
                        else
                        {
                            lblInformacion.Text = "No se subieron archivos. Seleccione un archivo para subir.";
                            lblInformacion.CssClass = "mssgERROR";
                        }

                           
                    }
                    catch (Exception ex)
                    {
                        lblInformacion.Text = string.Format("Se produjo un error al subir los archivos: {0}", ex.Message);
                        lblInformacion.CssClass = "mssgERROR";
                    }

                    #endregion
                }
                else
                {
                    lblInformacion.Text = string.Format("Los archivos a subir superan el tamaño permitido de {0} MB.", Convert.ToInt32(ViewState[this.ID + "MAXUPLOADSIZE"]) / 1024);
                    lblInformacion.CssClass = "mssgERROR";
                }
            }
            else
            {
                lblInformacion.Text = "Uno o más archivos contiene una extensión no permitida. Los archivos no fueron subidos.";
                lblInformacion.CssClass = "mssgERROR";
            }

            return _resultOK;
        }

        /// <summary>
        /// Verifica las extensiones de archivo permitidas. Si no se especificó ninguna, se puede subir cualquier tipo de archivo.
        /// </summary>
        /// <param name="p_Fcol"></param>
        /// <returns></returns>
        protected bool ValidarExtensiones(HttpFileCollection p_Fcol)
        {
            if (ViewState[this.ID + "FILEEXTENSIONSENABLED"] == null)
                return true;

            bool _allValid = true;
            string _regX = string.Format("({0})$", ViewState[this.ID + "FILEEXTENSIONSENABLED"]); // Establece la expresión regular de validación.

            for (int i = 0; i < p_Fcol.Count; i++)
            {
                RegexOptions _rOptions = RegexOptions.IgnoreCase
                    | RegexOptions.Singleline
                    | RegexOptions.Compiled;

                Regex _rX = new Regex(_regX, _rOptions);

                if (_rX.Match(p_Fcol[i].FileName).Length == 0) // si alguna extensión no coincide cancela la subida de todos los archivos.
                {
                    _allValid = false;
                    break;
                }
            }

            return _allValid;
        }

        /// <summary>
        /// Controla el tamaño total de todos los archivos a subir. Si no se especifica, utiliza el máximo definido en __MaxSize.
        /// </summary>
        /// <param name="p_Fcol"></param>
        /// <returns></returns>
        protected bool ValidarTamaño(HttpFileCollection p_Fcol)
        {
            int _totSize = 0;
            for (int i = 0; i < p_Fcol.Count; i++)
                _totSize += p_Fcol[i].ContentLength;

            return _totSize < Convert.ToInt32(ViewState[this.ID + "MAXUPLOADSIZE"]) * 1024;
        }

        
    }
}