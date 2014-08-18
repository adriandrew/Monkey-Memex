<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubirArchivo.ascx.cs" Inherits="AplicacionWeb.Controles.SubirArchivo" %>
<fieldset class="fileUploadControl">
    <legend>
        <asp:Label ID="lblTitulo" Text="Archivos a subir" runat="server"></asp:Label>
    </legend>
    <asp:Label ID="lblNota" runat="server" />
    <asp:Panel ID="pnlSubida" runat="server">
        <asp:FileUpload ID="fuExaminar" runat="server" />
    </asp:Panel>
     <asp:Label ID="lblInformacion" CssClass="mensajeCorrecto" runat="server" />
</fieldset>

