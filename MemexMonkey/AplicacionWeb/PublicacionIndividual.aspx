<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PublicacionIndividual.aspx.cs" Inherits="AplicacionWeb.PublicacionIndividual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div id="uiImagen">
        <h2 runat="server" id="uiTituloImagen"> Y la publicación, donde está la publicación? </h2>
        <h4 runat="server" id="uiUsuarioAportador"> Aporte por: </h4>
        <h6 runat="server" id="uiFechaPublicacion"> Fecha publicacion. </h6>
        <img runat="server" id="uiImagenIndividual" src="~/Images/XaviReclama.jpg" alt="Xavi reclama la publicación."/>
        <h6 runat="server" id="uiEtiquetas"> #xavi #monkey | #xavi reclama la publicacion #soy memex </h6>
    </div> <!-- Termina uiImagen -->
    
    <div id="uiComentarios">
        <a href="#uiPluginComentariosFacebook" name="uiPluginComentariosFacebook" onclick="MuestraOculta('ContentPlaceHolder_uiPluginComentariosFacebook')">Comentarios Facebook</a>
            <div runat="server" id="uiPluginComentariosFacebook"  class="fb-comments" data-href="http://monkey.somee.com/PublicacionIndividual/0" data-numposts="5" data-colorscheme="dark"></div>
        <a href="#uiComentariosMemex" name="uiComentariosMemex" onclick="MuestraOculta('ContentPlaceHolder_uiComentariosMemex')">Comentarios Memex</a>
            <div runat="server" id="uiComentariosMemex" >
                <input runat="server" type="text" id="uiComentar" value="" />
            </div> <!-- Termina uiContenidoComentariosMemex -->
    </div> <!-- Termina uiComentarios -->

    <%--Plugin de Facebook--%>
    <%--Ponerlo al final para optimizar la carga del website en el servidor.--%>

    <div id="fb-root"></div>
    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/es_LA/sdk.js#xfbml=1&version=v2.0";
    fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));</script>

    <%--Termina plugin de Facebook--%>

</asp:Content>
