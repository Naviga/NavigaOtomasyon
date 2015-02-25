<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true"
    CodeBehind="DosyaYonetimi.aspx.cs" Inherits="Ws.admin.DosyaYonetimi" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ruFakeInput
        {
            display: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadFileExplorer runat="server" ID="FileExplorer1" Width="100%"
        Height="520px" Language="tr-TR" Skin="MetroTouch" OnClientItemSelected="OnClientItemSelected" ExplorerMode="Thumbnails">
        <Configuration ViewPaths="~/yukleme" UploadPaths="~/yukleme"
            DeletePaths="~/yukleme" MaxUploadFileSize="100000000" />
    </telerik:RadFileExplorer>
    <hr />
    <a href="#!" class="button small right" onclick="SelectAndClose()">Dosyayı Seç</a>
    <script>

        var selectedFile;

        function getUrlParam(paramName) {
            var reParam = new RegExp('(?:[\?&]|&amp;)' + paramName + '=([^&]+)', 'i');
            var match = window.location.search.match(reParam);

            return (match && match.length > 1) ? match[1] : '';
        }

        function OnClientItemSelected(sender, args) {

            var file;

            if (args.get_item().get_type() == Telerik.Web.UI.FileExplorerItemType.File) {// if the item is a file        
                file = args.get_item().get_path();

                selectedFile = file;
            }
            else {// filder        
                //file = args.get_item().get_path();
                alert("The selected item is a directory:\n");
            }
        }

        function SelectAndClose() {
            var funcNum = getUrlParam('CKEditorFuncNum');
            var fileUrl = selectedFile;
            window.opener.CKEDITOR.tools.callFunction(funcNum, fileUrl);
            window.close();
        }

    </script>
</asp:Content>
