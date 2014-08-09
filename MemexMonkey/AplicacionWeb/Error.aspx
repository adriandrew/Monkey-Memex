<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AplicacionWeb.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div id="uiContenedorImagen">
        <h2 runat="server" id="uiTituloImagen"> Y la publicación, donde está la publicación? </h2>
        <h4 runat="server" id="uiUsuarioAportador"> Aporte por: Memex </h4>
        <h6 runat="server" id="uiFechaPublicacion"> Fecha publicacion: 07-08-2014 </h6>
        <img runat="server" id="uiImagen" src="~/Images/XaviReclama.jpg" alt="Xavi reclama la publicación."/>
        <h6 runat="server" id="uiEtiquetas"> #xavi #monkey | #xavi reclama la publicacion #memex fan </h6>
    </div> <!-- Termina uiImagen -->

    <!-- Falta checar esto del flappy bird para que se haga automaticamente -->
    <a class="iframe" href="http://flappybird.io/" onmouseover="InvocarFancybox()">         
        <h2>Click aqui para jugar flappybird</h2>
    </a>

</asp:Content>
