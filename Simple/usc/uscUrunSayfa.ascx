<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscUrunSayfa.ascx.cs" Inherits="Ws.usc.uscUrunSayfa" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>

<% enSiteHaritasi sayfa = SayfaId == 0 ? bllSiteHaritasi.SayfaGetirFiziksel(SayfaYolu) : bllSiteHaritasi.SayfaGetir(SayfaId); %>
<% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(SayfaId, true); %>

<% if (!IsPostBack) SiteMapOlustur(sayfa); %>

<div class="page-header row">
    <div class="large-16 column">
        <h1 id='hPageTitle<%= sayfa.Id %>'>
            <%= sayfa.Adi %>
        </h1>

    </div>

</div>

<div class="page-breadcrumbs-share row">
    <div class="page-breadcrumbs large-12 column">
        <asp:Literal ID="lblSiteMap" runat="server" />
    </div>
    <% if (sayfa.PaylasimAlani)
       {%>
    <div id="dvShare" class="page-share large-4 column"></div>
    <%} %>
</div>

<div class="row">
    <div class="large-4 columns">

        <% for (int i = 0; i < resimler.Count; i++)
           {%>

        <%if (resimler[i].AnaResim)
          {%>
        <img src='<%= resimler[i].Buyuk %>' />
        <%} %>

        <%}%>
    </div>
    <div class="large-12 columns">

        <p><%= sayfa.Icerik %></p>

    </div>
</div>


