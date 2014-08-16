<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AplicacionWeb.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

        <div id="imagen" class="error">
            <h2> Y la publicación, donde está la publicación? </h2>
            <h4> Aporte por: Memex </h4>
            <h6> Fecha publicacion: 07-08-2014 </h6>
            <img src="Images/XaviReclama.jpg" alt="Xavi reclama la publicación."/>
            <h6> #xavi #monkey | #xavi reclama la publicacion #memex fan </h6>
        </div> <!-- Termina imagen -->

        <!-- Falta checar esto del flappy bird para que se haga automaticamente -->
        <a class="iframe" href="http://flappybird.io/" onmouseover="InvocarFancybox()">         
            <h2>Click aqui para jugar flappybird</h2>
        </a>

</asp:Content>
