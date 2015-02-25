<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="CarouselOnizleme.aspx.cs" Inherits="Ws.admin.CarouselOnizleme" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Entity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% enCarousel _carousel = bllCarousel.Getir(Request.QueryString["id"].xToIntDefault()); %>

    <% List<enCarouselResim> resimler = bllCarouselResimleri.ResimleriGetir(_carousel.Id, null);
       string guid = Guid.NewGuid().ToString();
       string thumbStr = _carousel.TekrarSayisi > 1 ? "th " : "";%>

    <div id='<%= guid %>' class='owl-carousel'>
        <% foreach (enCarouselResim resim in resimler)
           {
               string fvLink = "";

               if (resim.VideoLink)
               {
                   fvLink = "fancybox-media' data-fancybox-group='gallery'";
               }

               if (resim.FotoLink)
               {
                   fvLink = "picture-gallery";
                   resim.NavUrl = resim.Buyuk;
               }%>

        <div class='item'>
            <a class='<%=thumbStr + " " + fvLink %>' href='<%= resim.NavUrl %>' <%= resim.NavUrl.Contains("#!") ? "" : " target='_blank'" %>>
                <img src='<%= resim.Orta %>' alt='' />
                <br />
                <%= resim.Baslik %>
            </a>
        </div>

        <%} %>
    </div>

    <script>$('#<%= guid %>').owlCarousel({
    autoPlay: '<%= _carousel.GosterimSuresi %>',
    items: '<%= _carousel.TekrarSayisi %>',
    itemsDesktop: [1199, <%= _carousel.TekrarSayisi %>],
    itemsDesktopSmall: [979, <%= _carousel.TekrarSayisi %>]
});</script>
</asp:Content>
