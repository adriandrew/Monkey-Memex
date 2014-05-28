<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PublicacionIndividual.aspx.cs" Inherits="AplicacionWeb.PublicacionIndividual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <h2 runat="server" id="tituloImagen"> Y la publicación, donde está la publicación? </h2>
    <h4 runat="server" id="usuarioAportador"> Aporte por: </h4>
    <h6 runat="server" id="fechaPublicacion"> Fecha publicacion. </h6>
    <img runat="server" id="imagenIndividual" src="~/Images/XaviReclama.jpg" alt="Xavi reclama la publicación."/>
    <h6 runat="server" id="etiquetas"> #xavi reclama la publicacion #soy monkey #soy memex </h6>

    
</asp:Content>
