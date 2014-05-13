<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnviarAporte.aspx.cs" Inherits="AplicacionWeb.Miembros.EnviarAporte" %>
<%@ Register src="../Controles/SubirArchivo.ascx" tagname="SubirArchivo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Label ID="lblTituloImagen" runat="server" Text="Título de Imagen"></asp:Label>
    <asp:TextBox ID="txtTituloImagen" runat="server" Width="296px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvTituloImagen" runat="server" ControlToValidate="txtTituloImagen" ErrorMessage="Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label ID="lblCategoria" runat="server" Text="Categoria"></asp:Label>
    <asp:DropDownList ID="ddlCategoria" runat="server" Height="29px" Width="201px">
    </asp:DropDownList>
    <br />
    <br />
    <uc1:SubirArchivo ID="SubirArchivo" runat="server" />
    <%--<asp:Button ID="btnSubirImagenes" runat="server" OnClick="btnSubirImagenes_Click" Text="Subir imágenes" />--%>
    <br />
    <div>
        <asp:LinkButton ID="lnkMostrarImagenes" runat="server" onclick="lnkMostrarImagenes_Click">Mostrar las imágenes subidas</asp:LinkButton>
        <asp:Panel runat="server" ID="pnlImagenes" />
    </div>

<%--    <asp:Label ID="lblArchivo" runat="server" Text="Archivo"></asp:Label>
    <asp:FileUpload ID="FileUpload" runat="server" Width="349px" />--%>
    <br />
    <asp:Label ID="lblEnlaceExterno" runat="server" Text="Enlace Externo"></asp:Label>
    <asp:TextBox ID="txtEnlaceExterno" runat="server" Width="296px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblEtiquetasBasicas" runat="server" Text="Etiquetas Basicas"></asp:Label>
    <asp:TextBox ID="txtPersonaje" runat="server" Width="237px" ForeColor="Gray">#Personaje1 #Personaje2</asp:TextBox>
    <asp:TextBox ID="txtEquipo" runat="server" Width="200px" ForeColor="Gray">#Equipo1 #Equipo2</asp:TextBox>
    <asp:TextBox ID="txtLiga" runat="server" Width="145px" ForeColor="Gray">#Liga</asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblEtiquetasOpcionales" runat="server" Text="Etiquetas Opcionales"></asp:Label>
    <asp:TextBox ID="txtEtiquetasOpcionales" runat="server" Width="296px" ForeColor="Gray">#VivaMexico #SoyMemex</asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnEnviarAporte" runat="server" BackColor="#00CC66" OnClick="btnEnviarAporte_Click" Text="Enviar Aporte" />
    <asp:Button ID="btnReiniciar" runat="server" BackColor="#00CC66" Text="Reiniciar" />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
    </asp:Content>
