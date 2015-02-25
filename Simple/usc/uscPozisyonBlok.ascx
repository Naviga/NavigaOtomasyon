<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscPozisyonBlok.ascx.cs" Inherits="Ws.usc.uscPozisyonBlok" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Entity" %>

<% List<enBlok> bloklar = bllSiteHaritasi_Blok.PozisyonaGoreGetir(Pozisyon.xToIntDefault(), true, SayfaMi.xToBooleanDefault() ? SayfaId : null);%>

<% foreach (enBlok blok in bloklar)
   {
       enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(blok.PozisyonId);
       
%>
<style>
    #dvBlok_<%= blok.Id + "-" + blok.PozisyonId %>{
     <%= blok.MetinRengi.xBosMu() ? "" : "color:" + blok.MetinRengi + " !important;" %>
    <%= blok.Height.xBosMu() ? "": "height:"+blok.Height+"px !important;" %>
    <%= blok.ArkaplanRengi.xBosMu()?"":" background-color:"+blok.ArkaplanRengi+" !important;" %>
    <%= blok.CerceveRengi.xBosMu()?"":" border-color:"+blok.CerceveRengi+" !important;" %>
     }
    #dvBlok_<%= blok.Id + "-" + blok.PozisyonId %> h4,#dvBlok_<%= blok.Id + "-" + blok.PozisyonId %> small,#dvBlok_<%= blok.Id + "-" + blok.PozisyonId %> hr{<%= blok.MetinRengi.xBosMu() ? "" : "color:" + blok.MetinRengi + "; border-color:"+ blok.MetinRengi+" !important;" %>}
</style>

<%
       
       if (pozisyon.Master)
       {%>

<div class="large-12 dvSBlokCont columns" blokid='<%= blok.Id %>' blokname='<%= blok.Adi %>' pozid='<%= blok.PozisyonId %>'>
    <div id='dvBlok_<%= blok.Id + "_" + blok.PozisyonId %>' class='blok panel <%= blok.CerceveKullanimi ? "" : "reset-panel" %>'>
        <%= blok.BaslikKullanimi ? "<h4>"+blok.Adi+"<br/><small>"+blok.Aciklama+"</small></h4>" : "" %>
        <%= blok.BaslikKullanimi ? "<hr/>" : "" %>
        <%= blok.Icerik.xBosMu() ? "" : blok.Icerik %>
        <div class="clear"></div>
    </div>
</div>

<%}
       else
       {%>
<div class="large-12 dvSBlokCont columns" blokid='<%= blok.Id %>' blokname='<%= blok.Adi %>' pozid='<%= blok.PozisyonId %>'>
    <div id='dvBlok_<%= blok.Id + "_" + blok.PozisyonId %>' class='blok panel <%= blok.CerceveKullanimi ? "" : "reset-panel" %>'>
        <%= blok.BaslikKullanimi ? "<h4>"+blok.Adi+"<br/><small>"+blok.Aciklama+"</small></h4>" : "" %>
        <%= blok.BaslikKullanimi ? "<hr/>" : "" %>
        <%= blok.Icerik.xBosMu() ? "" : blok.Icerik %>
        <div class="clear"></div>
    </div>
</div>
<%} %>
<%} %>