<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grid.aspx.cs" Inherits="Grid" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalDialog
        {
            position: fixed;
            font-family: Arial, Helvetica, sans-serif;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: rgba(255,255,255,0.8);
            z-index: 99999;
            opacity: 1;
            pointer-events: none;
        }
        
        .modalDialog > div
        {
            width: 60px;
            position: relative;
            margin: 20% auto;
            padding: 5px 20px 13px 20px;
            border-radius: 10px;
        }
        
        li
        {
            padding-top: 10px;
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script src="Scripts/Scrolling.js" type="text/javascript"></script>
    <script src="Scripts/modalLoad.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    On scroll data loader
                </h1>
            </div>
        </div>
        <div class="main">
            <ul id="dataPlaceHolder" runat="server">
            </ul>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
    </div>
    </form>
</body>
</html>
