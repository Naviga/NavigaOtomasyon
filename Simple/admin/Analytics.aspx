<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="Analytics.aspx.cs" Inherits="Ws.admin.Analytics" %>

<%@ Register Src="~/admin/usc/uscAnalytics.ascx" TagPrefix="uc1" TagName="uscAnalytics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uscAnalytics runat="server" ID="uscAnalytics" />
</asp:Content>
