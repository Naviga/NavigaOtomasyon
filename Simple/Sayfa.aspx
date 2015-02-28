<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Sayfa.aspx.cs" Inherits="Ws.Sayfa" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>
<%@ Register Src="/usc/uscStandartSayfa.ascx" TagPrefix="uc1" TagName="uscStandartSayfa" %>
<%@ Register Src="~/usc/uscUrunSayfa.ascx" TagPrefix="uc1" TagName="uscUrunSayfa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="exposure_slider/demo4.css" rel="stylesheet" />
    <%--<link href="exposure_slider/demo1.css" rel="stylesheet" />--%>
    <link href="foundation/css/normalize.css" rel="stylesheet" />
    <link href="foundation/css/foundation.css" rel="stylesheet" />
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/naviga.css" rel="stylesheet" />
    <%-- <link href="owl-carousel/owl.carousel.css" rel="stylesheet" />
    <link href="owl-carousel/owl.theme.css" rel="stylesheet" />--%>
    <link href="font-awesome-4.2.0/css/font-awesome.min.css" rel="stylesheet" />
    <script src="foundation/js/vendor/modernizr.js"></script>
    <link href="owl-carousel/carouselSync.css" rel="stylesheet" />

    <script src="/fancybox/lib/jquery-1.10.1.min.js"></script>
    <link href="/fancybox/source/jquery.fancybox.css" rel="stylesheet" />
    <script src="/fancybox/source/jquery.fancybox.pack.js"></script>

    <%-- <% enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(RouteData.Values["Id"].xToIntDefault());

       uscStandartSayfa.SayfaId = sayfa.Id;%>--%>

    <%-- <script>
        var sayfaId = '<%= sayfa.Id %>'; 
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(RouteData.Values["Id"].xToIntDefault());%>

    <% if (sayfa.UrunMu)
       {
           uscStandartSayfa.SayfaId = sayfa.Id;
    %>

    <%--<uc1:uscUrunSayfa runat="server" ID="uscUrunSayfa" />--%>

    <uc1:uscStandartSayfa runat="server" ID="uscStandartSayfa1" />

    <% }%>

    <%else
       {
           uscStandartSayfa.SayfaId = sayfa.Id;   %>

    <script>
        var sayfaId = '<%= sayfa.Id %>'; 
    </script>

    <uc1:uscStandartSayfa runat="server" ID="uscStandartSayfa" />

    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincntScript" runat="server">

    <%--<script type="text/javascript">
        $(document).ready(function () {

            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

        });
    </script>--%>

    <script>

        $(document).ready(function () {

            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            var sync1 = $("#sync1");
            var sync2 = $("#sync2");

            sync1.owlCarousel({
                singleItem: true,
                slideSpeed: 1000,
                lazyLoad: true,
                navigation: true,
                pagination: false,
                afterAction: syncPosition,
                responsiveRefreshRate: 200,
            });

            sync2.owlCarousel({
                items: 15,
                itemsDesktop: [1199, 10],
                itemsDesktopSmall: [979, 10],
                itemsTablet: [768, 8],
                itemsMobile: [479, 4],
                pagination: false,
                responsiveRefreshRate: 100,
                afterInit: function (el) {
                    el.find(".owl-item").eq(0).addClass("synced");
                }
            });

            function syncPosition(el) {
                var current = this.currentItem;
                $("#sync2")
                  .find(".owl-item")
                  .removeClass("synced")
                  .eq(current)
                  .addClass("synced")
                if ($("#sync2").data("owlCarousel") !== undefined) {
                    center(current)
                }

            }

            $("#sync2").on("click", ".owl-item", function (e) {
                e.preventDefault();
                var number = $(this).data("owlItem");
                sync1.trigger("owl.goTo", number);
            });

            function center(number) {
                var sync2visible = sync2.data("owlCarousel").owl.visibleItems;

                var num = number;
                var found = false;
                for (var i in sync2visible) {
                    if (num === sync2visible[i]) {
                        var found = true;
                    }
                }

                if (found === false) {
                    if (num > sync2visible[sync2visible.length - 1]) {
                        sync2.trigger("owl.goTo", num - sync2visible.length + 2)
                    } else {
                        if (num - 1 === -1) {
                            num = 0;
                        }
                        sync2.trigger("owl.goTo", num);
                    }
                } else if (num === sync2visible[sync2visible.length - 1]) {
                    sync2.trigger("owl.goTo", sync2visible[1])
                } else if (num === sync2visible[0]) {
                    sync2.trigger("owl.goTo", num - 1)
                }
            }

        });
    </script>

</asp:Content>
