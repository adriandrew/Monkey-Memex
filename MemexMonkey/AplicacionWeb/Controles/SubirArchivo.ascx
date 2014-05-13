<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubirArchivo.ascx.cs" Inherits="AplicacionWeb.Controles.SubirArchivo" %>
<fieldset class="fUploadControl">
    <legend>
        <asp:Label ID="lblUploadFilesTitle" Text="Archivos a subir" runat="server"></asp:Label>
    </legend>
    <asp:Label ID="lblNota" CssClass="comment" runat="server" />
    <asp:Panel ID="pnlUpload" runat="server">
        <asp:FileUpload ID="fuExaminar" CssClass="fileInput" runat="server" />
    </asp:Panel>
    <%--<asp:HyperLink ID="lnkAgregarArchivoParaSubir" CssClass="AddNewButton" NavigateUrl="javascript:addFileUploadCtrl();" Text="Agregar" runat="server" />--%>
    <asp:Button ID="btnSubirArchivos" Text="Subir archivos" ToolTip="Upload files" runat="server" onclick="btnSubirArchivos_Click" />
    <asp:Label ID="lblInformacion" CssClass="mssgOK" runat="server" ForeColor="Red" />
</fieldset>

