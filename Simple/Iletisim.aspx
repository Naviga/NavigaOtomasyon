<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Iletisim.aspx.cs" Inherits="Ws.Iletisim" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Entity" %>
<%@ Register Src="/usc/uscIletisimSayfasi.ascx" TagPrefix="uc1" TagName="uscIletisimSayfasi" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style>
        #map-canvas {
            width: 100%;
            height: 300px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageWrapper" class="bg-color-light-gray">
        <uc1:uscIletisimSayfasi runat="server" ID="uscIletisimSayfasi" SayfaYolu="~/Iletisim.aspx" />
    </div>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="maincntScript">
    
</asp:Content>
