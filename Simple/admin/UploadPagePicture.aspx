<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPagePicture.aspx.cs" Inherits="Ws.admin.UploadPagePicture" %>

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
        <% Session["PageId"] = thisPage.Id; %>
        <table cellpadding="8">
            <tr>
                <td>
                    <img id="imgPicture" src='<%= thisPage.Resim %>' />
                </td>
            </tr>
            <tr>
                <td><hr/></td>
            </tr>
            <tr>
                <td align="center">
                    <a id="aUploadButton" href="#!" class="button small">Yeni Sayfa Resmi Seç</a>&nbsp; &nbsp;<asp:LinkButton ID="lnkSayfaResmiKaldir" runat="server" CssClass="button small secondary" OnClick="lnkSayfaResmiKaldir_OnClick">Sayfa Resmini Kaldır</asp:LinkButton>
                </td>
            </tr>
        </table>
    </form>
    <script src="foundation/js/vendor/jquery.js"></script>
    <script src="foundation/js/foundation.min.js"></script>
    <script src="/scripts/Extentions.js"></script>
    <script src="/scripts/AjaxMethods.js"></script>
    <script src="../js/ajaxupload.3.6.js"></script>
    <script>
        $(document).ready(function () {
            var uploaderButton = $("#aUploadButton");
            var interval;
            var isFileTypeOk = true;
            if (uploaderButton[0]) {

                new AjaxUpload(uploaderButton, {
                    action: "/services/general.asmx/uploadPagePhoto",
                    name: "userPhoto",
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

                            $("#imgPicture").attr("src", "/yukleme/icerik/" + file);
                        }
                    }
                });
            }
        });
    </script>
</body>
</html>
