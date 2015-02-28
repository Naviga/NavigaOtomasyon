<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscUrunSayfa.ascx.cs" Inherits="Ws.usc.uscUrunSayfa" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>

<% enSiteHaritasi sayfa = SayfaId == 0 ? bllSiteHaritasi.SayfaGetirFiziksel(SayfaYolu) : bllSiteHaritasi.SayfaGetir(SayfaId); %>
<% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(SayfaId, true); %>

<div class="row" style="margin-top: 25px;">
    <div class="small-16 columns text-left">
        <h2 class="inset color-dark-gray"><span class="fa fa-list"></span><% Response.Write(SayfaAdi.ToString()); %>
        </h2>
    </div>
</div>
<hr class="fancy-line" />
<div class="row width-100">
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


