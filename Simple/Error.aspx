<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/tek.master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Ws.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row sayfaBaslik collapse">
        <div class="large-12">
            <h1>Hata Sayfası
            </h1>
        </div>
    </div>
    <div class="row sayfaIcerik">
        <div class="large-12 columns">

            <% var error = Server.GetLastError();
               var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500; %>

            <% if (code != 404)
               {%>
            <p>Beklenmeyen bir hata oluştu !</p>
            <b><%= code %></b>
            <p><%= error != null ? (error.Message + "<br/>" + error.InnerException.Message):"" %></p>
            <%}
               else
               {%>

            <p>Sayfa bulunamadı, kaldırılmış yada yanlış bir istekte bulunmuş olabilirsiniz !</p>

            <asp:Literal ID="ltrSiteHaritasi" runat="server" />

            <%} %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntScript" runat="server">
</asp:Content>
