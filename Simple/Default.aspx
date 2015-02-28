<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ws.Default" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp"></script>
    <script>
        function initialize() {
            var myLatlng = new google.maps.LatLng(39.915551, 32.858543);
            var mapOptions = {
                zoom: 16,
                center: myLatlng
            }
            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: 'Naviga Otomasyon'
            });
        }

        google.maps.event.addDomListener(window, 'load', initialize);

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="main-slider" class="owl-carousel owl-theme">
        <div class="item">
            <img src="slayt/2.jpg" />
        </div>
        <div class="item">
            <img src="slayt/1.jpg" />
        </div>
    </div>

    <div class="row width-100">
        <div class="small-16 columns text-center">
            <h1 class="inset color-dark-gray"><span class="fa fa-globe"></span>HABERLER
            </h1>
        </div>
    </div>
    <hr class="fancy-line" />
    <div id="main-news" class="row width-100">
        <div id="main-news-text" class="small-16 medium-7 large-7 columns bg-color-light-gray">
            <div id="main-news-text-carousel" class="owl-carousel owl-theme">
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c1.jpg" /></a>
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c2.jpg" /></a>
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c1.jpg" /></a>
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c2.jpg" /></a>
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                </div>
            </div>
        </div>
        <div id="main-news-seperator" class="medium-3 large-2 columns hide-for-small-down"></div>
        <div id="main-news-image" class="small-16 medium-6 large-7 columns bg-color-dark-gray">
            <div id="main-news-image-carousel" class="owl-carousel owl-theme">
                <div class="item">
                    <a href="carousel/c1.jpg" class="left th">
                        <img src="carousel/c1.jpg" /></a>
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c2.jpg" /></a>
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c1.jpg" /></a>
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c2.jpg" /></a>
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c1.jpg" /></a>
                </div>
                <div class="item">
                    <a href="#!" class="left th">
                        <img src="carousel/c2.jpg" /></a>
                </div>
            </div>
        </div>
    </div>

    <hr class="fancy-line" />

    <div class="row">
        <div class="small-16 columns text-center">
            <h1 class="inset color-dark-gray"><span class="fa fa-list"></span>ÜRÜNLER
            </h1>
        </div>
    </div>

    <hr class="fancy-line" />

    <div class="row">

        <% List<enIcerikResim> urunler = bllIcerikResimleri.Top4ResimGetir(null); %>

        <%for (int i = 0; i < 2; i++)
          {%>

        <div class="small-16 medium-8 large-8 columns">
            <div class="row">
                <div class="small-7 medium-7 large-7 columns">
                    <a class="th [radius]" href="#">
                        <img src="<%=urunler.ElementAt(i).Orta %>" />
                    </a>
                </div>
                <div class="small-9 medium-9 large-9 columns">
                    <h2><%=urunler.ElementAt(i).Baslik %></h2>
                    <%=urunler.ElementAt(i).Aciklama %>
                </div>
            </div>
        </div>

        <% } %>

        <%--<div class="small-16 medium-8 large-8 columns">
            <div class="row">
                <div class="small-7 medium-7 large-7 columns">
                    <a class="th [radius]" href="#">
                        <img src="carousel/c1.jpg" />
                    </a>
                </div>
                <div class="small-9 medium-9 large-9 columns">
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                </div>
            </div>
        </div>
        <div class="small-16 medium-8 large-8 columns">
            <div class="row">
                <div class="small-7 medium-7 large-7 columns">
                    <a class="th [radius]" href="#">
                        <img src="carousel/c2.jpg" />
                    </a>
                </div>
                <div class="small-9 medium-9 large-9 columns">
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                </div>
            </div>
        </div>--%>
    </div>
    <% if (urunler.Count > 2)
       {%>
    <hr class="fancy-line" />
    <div class="row">

        <%for (int i = 2; i < 4; i++)
          {%>

        <div class="small-16 medium-8 large-8 columns">
            <div class="row">
                <div class="small-7 medium-7 large-7 columns">
                    <a class="th [radius]" href="#">
                        <img src="<%=urunler.ElementAt(i).Orta %>" />
                    </a>
                </div>
                <div class="small-9 medium-9 large-9 columns">
                    <h2><%=urunler.ElementAt(i).Baslik %></h2>
                    <%=urunler.ElementAt(i).Aciklama %>
                </div>
            </div>
        </div>

        <% } %>

        <%--<div class="small-16 medium-8 large-8 columns">
            <div class="row">
                <div class="small-7 medium-7 large-7 columns">
                    <a class="th [radius]" href="#">
                        <img src="carousel/c1.jpg" class="th round border-secondary-blue-darker" />
                    </a>
                </div>
                <div class="small-9 medium-9 large-9 columns">
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                </div>
            </div>
        </div>
        <div class="small-16 medium-8 large-8 columns">
            <div class="row">
                <div class="small-7 medium-7 large-7 columns">
                    <a class="th [radius]" href="#">
                        <img src="carousel/c2.jpg" class="th round border-secondary-blue-darker" />
                    </a>
                </div>
                <div class="small-9 medium-9 large-9 columns">
                    <h2>LOREM IPSUM</h2>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                </div>
            </div>
        </div>--%>
    </div>
    <%} %>
    <hr class="fancy-line" />
    <div class="row">
        <div class="large-9 large-centered column text-center">
            <h2><span class="fa fa-quote-left"></span>&nbsp;&nbsp;50 yılı aşkın iş deneyimi&nbsp;&nbsp;<span class="fa fa-quote-right"></span></h2>
        </div>
    </div>
    <hr class="fancy-line" />

    <div class="row">
        <div class="large-16">
            <div id="main-brands-carousel" class="owl-carousel owl-theme">

                <div class="item">
                    <a href="#!">
                        <img src="img/cDenizKuvvetleri.png" /></a>
                </div>
                <div class="item">
                    <a href="#!">
                        <img src="img/cHavaKuvvetleri.png" /></a>
                </div>
                <div class="item">
                    <a href="#!">
                        <img src="img/cKaraKuvvetleri.png" /></a>
                </div>
                <div class="item">
                    <a href="#!">
                        <img src="img/cMgk.png" /></a>
                </div>
                <div class="item">
                    <a href="#!">
                        <img src="img/cMilliSavunma.png" /></a>
                </div>
                <div class="item">
                    <a href="#!">
                        <img src="img/cTai.png" /></a>
                </div>
                <!-- <div class="item">

                            <a href="#!" class="center">Tamamı&nbsp;<span class="fa fa-angle-double-right"></span>
                            </a>

                        </div>-->
            </div>
        </div>
    </div>
    <hr class="fancy-line" />
    <div class="row">
        <div class="large-9 large-centered column text-center">
            <h1 class="inset color-dark-gray"><span class="fa fa-map-marker"></span>&nbsp;NEREDEYİZ ?</h1>
        </div>
    </div>

    <div id="map-canvas" class="width-100"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincntScript" runat="server">
</asp:Content>
