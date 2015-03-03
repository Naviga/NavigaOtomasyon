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
        <div id="main-slider" class="owl-carousel owl-theme">
            <div class="item">
                <img src="/slayt/2.jpg" />
            </div>
            <div class="item">
                <img src="/slayt/1.jpg" />
            </div>
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
                        <h2><%=haberler.ElementAt(i).Adi %></h2>
                        <%=haberler.ElementAt(i).Icerik.xToRemoveHTMLTags().xLeft(50) %>...
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

            <% if (urunler.Count > 1)
               {%>

            <%for (int i = 0; i < 2; i++)
              {%>

            <div class="small-16 medium-8 large-8 columns">
                <div class="row">
                    <div class="small-7 medium-7 large-7 columns">
                        <a class="th [radius]" href="<%=urunler.ElementAt(i).Url %>">
                            <img src="<%=urunler.ElementAt(i).FotoOrta %>" />
                        </a>
                    </div>
                    <div class="small-9 medium-9 large-9 columns">
                        <h2><%=urunler.ElementAt(i).Adi %></h2>
                        <%=urunler.ElementAt(i).Description %>
                    </div>
                </div>
            </div>

            <%} %>

            <% } %>

            <% else if (urunler.Count == 1)
               {%>

            <div class="small-16 medium-8 large-8 columns">
                <div class="row">
                    <div class="small-7 medium-7 large-7 columns">
                        <a class="th [radius]" href="<%=urunler.ElementAt(0).Url %>">
                            <img src="<%=urunler.ElementAt(0).FotoOrta %>" />
                        </a>
                    </div>
                    <div class="small-9 medium-9 large-9 columns">
                        <h2><%=urunler.ElementAt(0).Adi %></h2>
                        <%=urunler.ElementAt(0).Description %>
                    </div>
                </div>
            </div>

            <%  } %>
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
                        <a class="th [radius]" href="<%=urunler.ElementAt(i).Url %>">
                            <img src="<%=urunler.ElementAt(i).FotoOrta %>" />
                        </a>
                    </div>
                    <div class="small-9 medium-9 large-9 columns">
                        <h2><%=urunler.ElementAt(i).Adi %></h2>
                        <%=urunler.ElementAt(i).Description %>
                    </div>
                </div>
            </div>

            <% } %>
        </div>
        <%} %>
        <hr class="fancy-line" />
        <div class="row">
            <div class="large-9 large-centered column text-center">
                <h2><span class="fa fa-quote-left"></span>&nbsp;&nbsp;<%= bllDiziler.DiziGetir("Home.Slogan.Text") %>&nbsp;&nbsp;<span class="fa fa-quote-right"></span></h2>
            </div>
        </div>
        <hr class="fancy-line" />

        <% if (true)
           {%>

        <div class="row">
            <div class="large-16">

                <%--<div id="main-brands-carousel" class="owl-carousel owl-theme">

                    <div class="item text-center">
                        <a href="#!">
                            <img src="img/cDenizKuvvetleri.png" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!">
                            <img src="img/cHavaKuvvetleri.png" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!">
                            <img src="img/cKaraKuvvetleri.png" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!">
                            <img src="img/cMgk.png" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!">
                            <img src="img/cMilliSavunma.png" /></a>
                    </div>
                    <div class="item text-center">
                        <a href="#!">
                            <img src="img/cTai.png" /></a>
                    </div>
                </div>--%>

                <%= CarouselOlustur(sayfa.CarouselId) %>

            </div>
        </div>

        <% } %>
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
