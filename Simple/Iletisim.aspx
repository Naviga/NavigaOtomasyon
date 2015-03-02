<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Iletisim.aspx.cs" Inherits="Ws.Iletisim" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Entity" %>
<%@ Register Src="/usc/uscIletisimSayfasi.ascx" TagPrefix="uc1" TagName="uscIletisimSayfasi" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style>
        #map-canvas {
            width: 100%;
            height: 300px;
        }
    </style>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageWrapper" class="bg-color-light-gray">
        <uc1:uscIletisimSayfasi runat="server" ID="uscIletisimSayfasi" SayfaYolu="~/Iletisim.aspx" />
    </div>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="maincntScript">
    <script>
        var map;
        var markerMap = {};
        var windowMap = {};

        function initialize() {
            var req = AjaxPost("/services/general.asmx/MapGetir");

            req.success(function (JSON) {

                var data = $.parseJSON(JSON.d);

                var centerLatlng = new google.maps.LatLng(data.Latitude, data.Longitude);

                var mapOptions = {
                    center: centerLatlng,
                    zoom: 16,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                }

                map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

                var infoContent = " <ul class='vcard'>" +
                                            "<li class='street-address'>" + data.Metin + "</li>";

                var infowindow = new google.maps.InfoWindow({
                    content: infoContent
                });

                var myLatlng = new google.maps.LatLng(data.Latitude, data.Longitude);

                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: 'title'
                });
            });
        }

        google.maps.event.addDomListener(window, 'load', initialize);


    </script>
</asp:Content>
