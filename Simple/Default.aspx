<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ws.Default" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>

<%@ Register Src="~/usc/uscHarita.ascx" TagPrefix="uc1" TagName="uscHarita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetirFiziksel("~/Default.aspx"); %>
    <% List<enSiteHaritasi> haberler = bllSiteHaritasi.HaberleriGetir(); %>

    <div id="defaultContentWrapper">
        <div id="main-slider">
            <% if (sayfa.CarouselId != null)
               {%>
            <%= CarouselOlustur(sayfa.CarouselId) %>
            <%} %>
        </div>

        <div class="row width-100">
            <div class="small-16 columns text-center">
                <h1 class="inset color-dark-gray"><span class="fa fa-globe"></span><%= bllDiziler.DiziGetir("Home.Title.News") %>
                </h1>
            </div>
        </div>
        <hr class="fancy-line" />
        <div id="main-news" class="row width-100">
            <div id="main-news-text" class="small-16 medium-7 large-7 columns bg-color-light-gray">
                <div id="main-news-text-carousel" class="owl-carousel owl-theme">
                    <%for (int i = 0; i < haberler.Count; i++)
                      {%>

                    <div class="item">
                        <a href="<%=haberler.ElementAt(i).Url %>" class="left th">
                            <img style="width: 150px;" src="<%=haberler.ElementAt(i).FotoOrta %>" /></a>
                        <h2><a href='<%=haberler.ElementAt(i).Url%>'><%=haberler.ElementAt(i).Adi %></a></h2>
                        <%=haberler.ElementAt(i).Icerik.xToRemoveHTMLTags().xLeft(100) %>...
                    </div>
                    <%} %>
                </div>
            </div>
            <div id="main-news-seperator" class="medium-3 large-2 columns hide-for-small-down"></div>
            <div id="main-news-image" class="small-16 medium-6 large-7 columns bg-color-dark-gray">
                <div id="main-news-image-carousel" class="owl-carousel owl-theme">
                    <div class="item text-center">
                        <a href="carousel/c1.jpg" class="left th">
                            <img src="carousel/c1.jpg" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!" class="left th">
                            <img src="carousel/c2.jpg" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!" class="left th">
                            <img src="carousel/c1.jpg" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!" class="left th">
                            <img src="carousel/c2.jpg" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!" class="left th">
                            <img src="carousel/c1.jpg" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!" class="left th">
                            <img src="carousel/c2.jpg" /></a>
                    </div>
                </div>
            </div>
        </div>

        <hr class="fancy-line" />

        <div class="row">
            <div class="small-16 columns text-center">
                <h1 class="inset color-dark-gray"><span class="fa fa-list"></span><%= bllDiziler.DiziGetir("Home.Title.Products") %>
                </h1>
            </div>
        </div>

        <hr class="fancy-line" />

        <div class="row">

            <% List<enSiteHaritasi> urunler = bllSiteHaritasi.VitrinGetir(); %>

            <%for (int i = 0; i < urunler.Count; i++)
              {%>
            <div class="large-8 columns">
                <div class="row">
                    <div class="large-3 columns">
                        <a class="th [radius]" href="<%=urunler.ElementAt(i).Url %>">
                            <img src="<%=urunler.ElementAt(i).FotoBuyuk %>" />
                        </a>
                    </div>
                    <div class="large-13 columns">
                        <h2><%=urunler.ElementAt(i).Adi %></h2>
                        <%=urunler.ElementAt(i).Description %>
                    </div>
                </div>
            </div>

            <% } %>
        </div>
        <%--<hr class="fancy-line" />
        <div class="row">
            <div class="large-9 large-centered column text-center">
                <h2><span class="fa fa-quote-left"></span>&nbsp;&nbsp;<%= bllDiziler.DiziGetir("Home.Slogan.Text") %>&nbsp;&nbsp;<span class="fa fa-quote-right"></span></h2>
            </div>
        </div>--%>
        <hr class="fancy-line" />
        <div class="row">
            <div class="large-9 large-centered column text-center">
                <h1 class="inset color-dark-gray"><span class="fa fa-map-marker"></span>&nbsp;<%= bllDiziler.DiziGetir("Home.Title.Map") %></h1>
            </div>
        </div>

        <div class="row width-100">
            <uc1:uscHarita runat="server" ID="uscHarita" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincntScript" runat="server">
</asp:Content>
