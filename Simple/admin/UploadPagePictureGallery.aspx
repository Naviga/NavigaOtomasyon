<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPagePictureGallery.aspx.cs" Inherits="Ws.admin.UploadPagePictureGallery" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Entity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <link rel="stylesheet" href="foundation/css/normalize.css">
    <link rel="stylesheet" href="foundation/css/foundation.css">
    <link href="../font-awesome-4.2.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
    <script src="foundation/js/vendor/modernizr.js"></script>
</head>
<body>

    <form id="form1" runat="server">
        <% enSiteHaritasi thisPage = bllSiteHaritasi.SayfaGetir(Request.QueryString["id"].xToIntDefault()); %>
        <% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(thisPage.Id, true); %>

        <% Session["PageId"] = thisPage.Id; %>
        <div class="panel text-center">
            <a id="aUploadButton" href="#!" class="button small">Resim Yükle</a>
        </div>
        <div class="row">
            <% int i = 0; foreach (enIcerikResim resim in resimler)
               {%>
            <div class="large-2 column <%= i == resimler.Count-1 ? " end" : "" %>">
                <div class="row">
                    <div class="large-12 columns">
                        <div class="imgThumb th" style="background-image: url(<%= resim.Orta %>)">
                            <a class="picture-gallery" href='<%= resim.Buyuk %>'></a>
                        </div>
                    </div>
                    <div class="large-12 column text-right">
                        <a href="#!" class="button tiny alert" onclick="DeleteImage('<%= resim.Id %>')"><span class="fa fa-trash"></span>&nbsp;</a>
                    </div>
                </div>
            </div>
            <% i++;
               } %>
        </div>
        <% if (resimler.Count == 0)
           {%>
        <p>Henüz hiç resim yüklemediniz</p>
        <%} %>
    </form>
    <script src="foundation/js/vendor/jquery.js"></script>
    <script src="foundation/js/foundation.min.js"></script>
    <script src="foundation/js/foundation/foundation.equalizer.js"></script>
    <script src="/scripts/Extentions.js"></script>
    <script src="/scripts/AjaxMethods.js"></script>
    <script src="/js/ajaxupload.3.6.js"></script>
    <script>
        $(document).foundation();

        $(".imgThumb").css("height", $(".imgThumb").width() / 1.5);

        $(document).ready(function () {
            var uploaderButton = $("#aUploadButton");
            var interval;
            var isFileTypeOk = true;
            if (uploaderButton[0]) {

                new AjaxUpload(uploaderButton, {
                    action: "/services/general.asmx/uploadPagePictures",
                    name: "userPhoto",
                    multiple: true,
                    onSubmit: function (file, extension) {
                        //Sadece txt uzantılı dosyalar yüklenebilecek
                        if (extension == "jpg" || extension == "png" || extension == "gif") {
                            isFileTypeOk = true;
                            //Butonun metnini değiştiriyoruz
                            uploaderButton.text("Yükleniyor...");
                            $("#imgPhotoLoader").show();
                        }
                        else
                            isFileTypeOk = false;
                    },
                    beforeSend: function () {
                        //alert('ok');
                    },
                    onComplete: function (file, response) {
                        window.clearInterval(interval);

                        this.enable();

                        uploaderButton.text("Fotoğraf Seç");

                        if (response == "hata") {
                            alert("Resim yüklenemedi ! Lütfen tekrar deneyin.");
                        }
                        else {

                            location.href = location.href;
                        }
                    }
                });
            }
        });

        function DeleteImage(id) {

            var dataS = '{"imageId":"' + id + '"}';

            var req = AjaxPost("/services/general.asmx/DeletePageImage", dataS);

            req.success(function () {
                
                location.href = location.href.replace("#!","");

            });
        }
    </script>
</body>
</html>
