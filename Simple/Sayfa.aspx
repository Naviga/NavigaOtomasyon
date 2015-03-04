<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Sayfa.aspx.cs" Inherits="Ws.Sayfa" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>
<%@ Register Src="/usc/uscStandartSayfa.ascx" TagPrefix="uc1" TagName="uscStandartSayfa" %>
<%@ Register Src="~/usc/uscUrunSayfa.ascx" TagPrefix="uc1" TagName="uscUrunSayfa" %>
<%@ Register Src="~/usc/uscHaberSayfa.ascx" TagPrefix="uc1" TagName="uscHaberSayfa" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(RouteData.Values["Id"].xToIntDefault());%>

    <div id="dvPageWrapper" class="bg-color-light-gray">


        <% if (sayfa.UrunMu)
           {
               uscUrunSayfa.SayfaId = sayfa.Id;
               uscUrunSayfa.SayfaAdi = sayfa.Adi;
        %>

        <uc1:uscUrunSayfa runat="server" ID="uscUrunSayfa" />

        <% }
           else if (sayfa.HaberMi)
           {
               uscHaberSayfa.SayfaId = sayfa.Id;
        %>
        <uc1:uscHaberSayfa runat="server" ID="uscHaberSayfa" />
        <%} %>

        <%else
           {
               uscStandartSayfa.SayfaId = sayfa.Id;   %>

        <script>
            var sayfaId = '<%= sayfa.Id %>'; 
        </script>

        <uc1:uscStandartSayfa runat="server" ID="uscStandartSayfa" />

        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincntScript" runat="server">

<%--    <script>

        $(document).ready(function () {

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
    </script>--%>

</asp:Content>
