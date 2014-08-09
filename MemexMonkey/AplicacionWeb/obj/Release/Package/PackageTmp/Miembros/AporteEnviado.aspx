<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AporteEnviado.aspx.cs" Inherits="AplicacionWeb.Miembros.AporteEnviado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <asp:Label ID="lblGracias" runat="server" Text="Gracias por enviar tu aporte. Lo vamos a filtrar y te prometo que si es lo suficientemente bueno se publicará en breve.
        Quieres enviar otro aporte o volver a la pagina principal?
    "></asp:Label>
    
    <asp:HyperLink ID="hlEnviarAporte" runat="server" NavigateUrl="../EnviarAporte">Volver a Enviar Aporte</asp:HyperLink>
    <asp:HyperLink ID="hlPaginaPrincipal" runat="server" NavigateUrl="../Inicio">Volver a Pagina Principal</asp:HyperLink>
    
</asp:Content>
