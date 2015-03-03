<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscHarita.ascx.cs" Inherits="Ws.usc.uscHarita" %>

<script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
 <style>
        #map-canvas {
            width: 100%;
            height: 300px;
        }
    </style>

<div class="clear"></div>
<div id="map-canvas" class="width-100">
</div>
<div class="clear"></div>
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
