<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Individual.aspx.cs" Inherits="AplicacionWeb.Individual" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script src="../Js/scripts.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="contenedorImagenes">
        </div>

        <div id="uiContenedorComentarios">
            <a href="#uiComentariosMemex" name="uiComentariosMemex" onclick="MuestraOculta('uiComentariosMemex')">Comentarios Memex</a>
            <div id="uiComentariosMemex" runat="server">
                <input id="uiAreaComentario" runat="server" type="text" value="" />
                <button id="uiEnviarComentario" runat="server" onserverclick="uiEnviarComentario_Click" type="submit">Comentar</button>
            </div><!-- Termina uiContenidoComentariosMemex -->
            <a href="#uiPluginComentariosFacebook" name="uiPluginComentariosFacebook" onclick="MuestraOculta('uiPluginComentariosFacebook')">Comentarios Facebook</a>
            <div id="uiPluginComentariosFacebook" runat="server" class="fb-comments" data-colorscheme="dark" data-href="http://monkey.somee.com/Individual/0" data-numposts="5">
            </div><!-- Termina uiPluginComentariosFacebook -->
        </div><!-- Termina uiComentarios -->

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
