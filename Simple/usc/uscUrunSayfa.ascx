<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscUrunSayfa.ascx.cs" Inherits="Ws.usc.uscUrunSayfa" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>

<% enSiteHaritasi sayfa = SayfaId == 0 ? bllSiteHaritasi.SayfaGetirFiziksel(SayfaYolu) : bllSiteHaritasi.SayfaGetir(SayfaId); %>
<% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(SayfaId, true); %>

<% if (!IsPostBack) SiteMapOlustur(sayfa); %>

<% if (sayfa.BaslikAlani)
   {%>
<div class="page-header row">
    <div class="large-16 column">
        <h1 id='hPageTitle<%= sayfa.Id %>'>
            <%= sayfa.Adi %>
        </h1>

    </div>

</div>
<%} %>
<% if (sayfa.SayfaYolu || sayfa.PaylasimAlani)
   {%>


<div class="page-breadcrumbs-share row">
    <% if (sayfa.SayfaYolu)
       {%>
    <div class="page-breadcrumbs large-12 column">
        <asp:Literal ID="lblSiteMap" runat="server" />
    </div>
    <%} %>
    <% if (sayfa.PaylasimAlani)
       {%>
    <div id="dvShare" class="page-share large-4 column"></div>
    <%} %>
</div>

<% } %>
<div class="row">
    <div class="large-8 columns">

        <div id="main">
            <div id="exposure"></div>
            <div class="clear"></div>
        </div>
        <div id="top">
            <div class="panel1">
                <div id="left">
                    <a href="javascript:void(0);">
                        <img src="/exposure_slider/left.png" alt="Previous" /></a>
                </div>
                <ul id="images">

                    <% for (int i = 0; i < resimler.Count; i++)
                       {%>

                    <li><a href='<%=resimler.ElementAt(i).Buyuk %>'>
                        <img src='<%=resimler.ElementAt(i).Kucuk %>' title='<%=resimler.ElementAt(i).Aciklama %>' /></a></li>

                    <%}%>
                </ul>
                <div id="right">
                    <a href="javascript:void(0);">
                        <img src="/exposure_slider/right.png" alt="Next" /></a>
                </div>
                <div class="clear"></div>
            </div>
        </div>

    </div>
    <div class="large-8 columns">

        <%= sayfa.Icerik %>

        <%--<div class="page-content row">
            <div id='dvPageContent<%= Session["ürünler_sayfaId"].xToIntDefault() %>' class='large-12 column <%= SessionManager.Admin!= null ? " editable-full' op='page-content' contenteditable='true'":"'" %>'>
                <%= sayfa.Icerik %>
            </div>
        </div>--%>
    </div>
</div>


