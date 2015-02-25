<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="WebFonts.aspx.cs" Inherits="Ws.admin.WebFonts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #dvList p
        {
            border-bottom: 1px solid #efefef;
        }

        #dvTopBar
        {
            display: none;
        }

    </style>
    <script>



        var jFonts;

        var indexNum = 0;
        var limitNum = 20;

        var fSize = 30;

        var sampleText = "Lorem ipsum dior ne diyor - çşiğüö";

        $(document).ready(function () {
            FontlariCek();

            $("#ddlSiralama").change(function () {
                FontlariCek();
            });
        });

        function FontlariCek() {

            var sortKey = $("#ddlSiralama").val();

            $.ajax({
                url: "https://www.googleapis.com/webfonts/v1/webfonts?key=AIzaSyAkYJHeGY4sSS1YrbrCw25j4wiNKKs74bQ&sort=" + sortKey,
                success: function (json) {
                    jFonts = json;

                    ListFonts(0);

                },
                dataType: "json"
            });

        }

        function ListFonts(value) {



            if (indexNum == 0 && value < 0) {
                return false;
            }

            $("#dvList").html("");

            indexNum = indexNum + value;

            for (var i = indexNum; i < indexNum + limitNum; i++) {

                var familyName = jFonts.items[i].family;

                var $headlink = $("head").find("link[id='lnk" + familyName + "']");

                var linkElement = "<link id='lnk" + familyName + "' href='http://fonts.googleapis.com/css?family=" + familyName + "' rel='stylesheet' type='text/css'>";

                if ($headlink.length) {

                }
                else {
                    $("head").append(linkElement);
                }

                if ($("#txtDisplayText").val()) {
                    $("#dvList").append("<p><span class='spnText' style=\"font-family: '" + familyName + "', sans-serif;\">" + $("#txtDisplayText").val() + "</span><br/><b><small>" + familyName + "</small></b> - <small><a href='#!' class='tiny button warning' onclick='javascript:parent.FontSec(\"" + familyName + "\")'>Kullan</a></small></p>");
                } else {
                    $("#dvList").append("<p><span class='spnText' style=\"font-family: '" + familyName + "', sans-serif;\">" + sampleText + "</span><br/><b><small>" + familyName + "</small></b> - <small><a href='#!' class='tiny button warning' onclick='javascript:parent.FontSec(\"" + familyName + "\")'>Kullan</a></small></p>");
                }
            }

            $("#dvList p").css("font-size", fSize + "px");
        }

        function FontSize(value) {

            var valInt = parseInt(value);

            fSize = fSize + value;

            if (fSize >= 10) {
                $("#dvList p").css("font-size", fSize + "px");
            } else {
                fSize = 10;
            }
        }

        function TextChange() {

            if ($("#txtDisplayText").val()) {
                $(".spnText").html($("#txtDisplayText").val());
            } else {
                $(".spnText").html(sampleText);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-bottom: -50px">
        <div class="large-3 columns">
            <div class="row">
                <div class="large-6 columns">
                    <ul class="button-group left">
                        <li><a href="#" class="tiny button" onclick="ListFonts(-20)"><</a></li>
                        <li><a href="#" class="tiny button" onclick="ListFonts(20)">></a></li>
                        <li><a href="#" class="tiny button" onclick="FontSize(-5)">-</a></li>
                        <li><a href="#" class="tiny button" onclick="FontSize(5)">+</a></li>
                    </ul>

                </div>
                <div class="large-6 columns">
                    <input class="left" type="text" id="txtDisplayText" value="" onkeyup="TextChange()" placeholder="Bir şeyler yaz..." />
                </div>
            </div>
        </div>
        <div class="large-9 columns">
            <div class="row">
                <div class="large-1 columns">
                    <p class="right"><b>Sıra</b> </p>
                </div>
                <div class="large-11 columns">

                    <select id="ddlSiralama">
                        <option value="popularity">Popüler</option>
                        <option value="alpha">Alfabetik</option>
                        <option value="date">Tarih</option>
                        <option value="trending">Trend</option>
                    </select>
                </div>
            </div>
        </div>
    </div>
    <hr />
    <div class="row">
        <div id="dvList" class="large-12">
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="large-3 columns">
            <div class="row">
                <div class="large-6 columns">
                    <ul class="button-group left">
                        <li><a href="#" class="tiny button" onclick="ListFonts(-20)"><</a></li>
                        <li><a href="#" class="tiny button" onclick="ListFonts(20)">></a></li>
                        <li><a href="#" class="tiny button" onclick="FontSize(-5)">-</a></li>
                        <li><a href="#" class="tiny button" onclick="FontSize(5)">+</a></li>
                    </ul>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
