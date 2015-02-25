<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/tek.master" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="Ws.SiteMap" %>

<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">

        <h1>
            <%= bllSiteHaritasi.SayfaGetirFiziksel("~/SiteMap.aspx").Adi %>
        </h1>

    </div>
    <div class="page-content">
        <asp:Literal ID="ltrSiteMap" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntScript" runat="server">
</asp:Content>
