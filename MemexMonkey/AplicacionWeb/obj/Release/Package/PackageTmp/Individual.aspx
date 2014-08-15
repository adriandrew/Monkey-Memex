<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Individual.aspx.cs" Inherits="AplicacionWeb.Individual" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link href="~/Css/contenido.css" rel="stylesheet" type="text/css" />    

    <script src="../Js/scripts.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">

        <div runat="server" id="imagenes"></div>

        <div id="comentarios">
            <a href="#aComentariosMemex" id="aComentariosMemex" onclick="MuestraOculta('comentariosMemex')">Comentarios Memex</a>
            <div id="comentariosMemex" runat="server"></div><!-- Termina comentariosMemex -->
            <div id="comentar" runat="server" visible="False">
                <input id="areaComentario" runat="server" type="text" value="" />
                <button id="enviarComentario" runat="server" onserverclick="EnviarComentario_Click" type="submit">Comentar</button>
            </div><!-- Termina comentar -->
            <a href="#aComentariosFacebook" id="aComentariosFacebook" onclick="MuestraOculta('comentariosFacebook')">Comentarios Facebook</a>
            <div id="comentariosFacebook" runat="server" class="fb-comments" data-colorscheme="dark" data-href="http://monkey.somee.com/Individual/0" data-numposts="5">
            </div><!-- Termina pluginComentariosFacebook -->
        </div><!-- Termina comentarios -->

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

    </form>
</body>
</html>
