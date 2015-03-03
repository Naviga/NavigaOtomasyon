<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="Pages.aspx.cs" Inherits="Ws.admin.Pages" MaintainScrollPositionOnPostback="true" %>

<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register Src="/usc/uscUyari.ascx" TagName="uscUyari" TagPrefix="uc1" %>
<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>



        function youtubeDataCallback(data) {

            var description = data.entry.media$group.media$description.$t.replace(/\n/g, '<br/>');

            var s = '<hr/><table cellpadding="10"><tr><td>';
            s += '<a href="' + data.entry.media$group.media$player.url + '" class="fancybox-media"><img  hspace="10" src="' + data.entry.media$group.media$thumbnail[0].url + '" width="' + data.entry.media$group.media$thumbnail[0].width + '" height="' + data.entry.media$group.media$thumbnail[0].height + '" alt="' + data.entry.media$group.media$thumbnail[0].yt$name + '" align="left"/></a>';
            s += '<b>Title:</b> ' + data.entry.title.$t + '<br/>';
            //s += '<b>Author:</b> ' + data.entry.author[0].name.$t + '<br/>';
            //s += '<b>Published:</b> ' + new Date(data.entry.published.$t).toLocaleDateString() + '<br/>';
            //s += '<b>Duration:</b> ' + Math.floor(data.entry.media$group.yt$duration.seconds / 60) + ':' + (data.entry.media$group.yt$duration.seconds % 60) + ' (' + data.entry.media$group.yt$duration.seconds + ' seconds)<br/>';
            //if (data.entry.gd$rating) {
            //    s += '<b>Rating:</b> ' + data.entry.gd$rating.average.toFixed(1) + ' out of ' + data.entry.gd$rating.max + ' (' + data.entry.gd$rating.numRaters + ' ratings)<br/>';
            //}
            //s += '<b>Statistics:</b> ' + data.entry.yt$statistics.favoriteCount + ' favorite(s); ' + data.entry.yt$statistics.viewCount + ' view(s)<br/>';
            s += '<br/>' + data.entry.media$group.media$description.$t.replace(/\n/g, '<br/>') + '<br/>';
            s += '<br/></td><td><iframe width="560" height="315" src="//www.youtube.com/embed/' + $('#txtVidKaynak').val() + '" frameborder="0" allowfullscreen></iframe></td></tr></table>';//<a href="' + data.entry.media$group.media$player.url + '" target="_blank">Watch on YouTube</a>';

            $('#youtubeDataOutput').html(s);

            $("#txtVidBaslik").val(data.entry.title.$t);


            $("#txtVidAciklama").val($(description).text());


            $("#imgKapak").attr("src", data.entry.media$group.media$thumbnail[0].url);
            $("#hdnKapakUrl").val(data.entry.media$group.media$thumbnail[0].url);

            $("#txtVidKaynakVimeo").val('');

        }

        $(document).ready(function () {

            if ($("#txtYeniSayfaUrl").val().indexOf("://www.") != -1) {
                $('#chkDisLink').prop('checked', true);
            }

            $('.fancybox-media')
                .attr('rel', 'media-gallery')
                .fancybox({
                    openEffect: 'none',
                    closeEffect: 'none',
                    prevEffect: 'none',
                    nextEffect: 'none',

                    arrows: false,
                    helpers: {
                        media: {},
                        buttons: {}
                    }
                });

            //$("#txtYeniSayfaUrl").keyup(function (e) {
            //    $(this).val($(this).val().ToUrl());
            //});

            $("#txtYeniSayfaUrl").change(function (e) {

                if ($('#chkDisLink').is(':checked') == false && $("#txtYeniSayfaUrl").val().indexOf("://www.") == -1) {

                    var url = $(this).val().ToUrl();

                    $(this).val(url);

                    var dataS = '{"url":"' + url + '","dzID":"<%=VwID %>"}';
                    var req = AjaxPost("/services/general.asmx/UrlVarMi", dataS);

                    req.success(function (data) {

                        if (data.d) {
                            $("#txtYeniSayfaUrl").addClass("required");
                            alert("Url başka bir sayfa tarafından kullanımda lütfen başka bir url seçin.");
                        }

                    });
                }

            });

            $("#txtYeniSayfaAdi").change(function () {
                $("#txtYeniSayfaUrl").val($(this).val().ToUrl());
            });

            //$("#txtYeniSayfaAdi").keyup(function (e) {
            //    $("#txtYeniSayfaUrl").val($(this).val().ToUrl());
            //});

            $('#txtVidKaynak').change(function (e) {

                e.preventDefault();
                var videoid = $('#txtVidKaynak').val();
                var m;
                if (m = videoid.match(/^http:\/\/www\.youtube\.com\/.*[?&]v=([^&]+)/i) || videoid.match(/^http:\/\/youtu\.be\/([^?]+)/i)) {
                    videoid = m[1];
                }
                if (!videoid.match(/^[a-z0-9_-]{11}$/i)) {
                    alert('Unable to parse Video ID/URL.');
                    return;
                }
                $.getScript('http://gdata.youtube.com/feeds/api/videos/' + encodeURIComponent(videoid) + '?v=2&alt=json-in-script&callback=youtubeDataCallback');
            });

            $('#txtVidKaynakVimeo').change(function (e) {

                var vimeoVideoID = $(this).val();

                $.getJSON('http://www.vimeo.com/api/v2/video/' + vimeoVideoID + '.json?callback=?', { format: "json" }, function (data) {

                    //$(".thumbs").attr('src', data[0].thumbnail_small);


                    var description = data[0].description;

                    var s = '<hr/><table cellpadding="10"><tr><td>';
                    s += '<a href="' + data[0].url + '" class="fancybox-media"><img src="' + data[0].thumbnail_medium + '"  align="left" hspace="10" /></a>';
                    s += '<b>Title:</b> ' + data[0].title + '<br/>';
                    s += '<br/>' + description + '<br/>';
                    s += '<br/></td><td><iframe src="//player.vimeo.com/video/' + data[0].id + '" width="500" height="281" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></td></tr></table>';

                    $('#youtubeDataOutput').html(s);

                    $("#txtVidBaslik").val(data[0].title);


                    $("#txtVidAciklama").val(data[0].description.replace('<br />', ''));


                    $("#imgKapak").attr("src", data[0].thumbnail_small);
                    $("#hdnKapakUrl").val(data[0].thumbnail_small);

                    $("#txtVidKaynak").val('');
                });
            });

            $(".aDz").click(function () {
                $(this).hide();
                $(this).parent().append("<input type='text' value='" + $(this).text() + "' />");
            });

        });

        function Validate() {
            if (!$("#txtYeniSayfaUrl").val()) {
                alert("Lütfen bir url yazın");
                return false;
            }
        }

        function OnClientValueChanged(sender, args) {

            var txtUrl = $find('<%= txtYeniSayfaUrl.ClientID %>');
            txtUrl.set_value(args.get_newValue().ToUrl());

        }

        function UstSayfaSecimTemizle() {

            var tree = $find('trvSayfalar');

            tree.unselectAllNodes();

        }
    </script>

    <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=rgrvResimler.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="trlSiteHaritasi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="trlSiteHaritasi" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="treeSayfalar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="treeSayfalar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgrvSayfaBloklari" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgrvBloklar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadCodeBlock2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="trInfo" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="trArrow" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlSayfaBloklariPozisyonlar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgrvSayfaBloklari">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvSayfaBloklari" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgrvBloklar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvBloklar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="trInfo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="trInfo" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="trArrow">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="trArrow" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSayfaBloklariPozisyonlar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSayfaBloklariPozisyonlar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgrvSayfaBloklari" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgrvResimler">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvResimler" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="uscUyari1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="uscUyari1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadTabStrip runat="server" ID="rtsGenel" MultiPageID="rmpGenel" SelectedIndex="0" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab Text="Liste"></telerik:RadTab>
            <telerik:RadTab Text="(+) Yeni"></telerik:RadTab>
            <telerik:RadTab Text="Sayfa Düzeni / Blok Yerleşimleri" ImageUrl="~/admin/css/img/DragAndDropIcon.png" Visible="False"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage runat="server" ID="rmpGenel" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="rpvListe">
            <telerik:RadTreeList ID="trlSiteHaritasi" OnItemDataBound="trlSiteHaritasi_ItemDataBound" runat="server" DataKeyNames="Id" ClientDataKeyNames="Url" ParentDataKeyNames="Parent"
                AllowSorting="true" AllowPaging="true" PageSize="20" OnNeedDataSource="trlSiteHaritasi_NeedDataSource" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowItemSelection="true" ClientSettings-Selecting-AllowToggleSelection="True">
                <Columns>
                    <telerik:TreeListBoundColumn DataField="Adi" HeaderText="Adı" SortExpression="Adi" HeaderStyle-Width="25%"></telerik:TreeListBoundColumn>
                    <telerik:TreeListBoundColumn DataField="Url" HeaderText="Url" SortExpression="Url" HeaderStyle-Width="45%" UniqueName="Url"></telerik:TreeListBoundColumn>
                    <%--<telerik:TreeListTemplateColumn HeaderText="YS" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Facebook Yorum sayısı" AllowSorting="False">
                        <ItemTemplate>
                            <%# Eval("FaceComments").xToBooleanDefault() ? ("<fb:comments-count href='http://"+Request.Url.Host+Eval("Url")+"'></fb:comments-count>") :"" %>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>--%>

                    <telerik:TreeListTemplateColumn HeaderText="Sıra" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadTextBox ID="txtSiraNo" runat="server" Text='<%# Eval("Sira") %>' MaxLength="3" AutoPostBack="true" OnTextChanged="txtSiraNo_TextChanged" dzID='<%# Eval("Id") %>' Width="40px"></telerik:RadTextBox>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>
                    <%-- <telerik:TreeListTemplateColumn HeaderText="Özel" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Özel sayfa durumunu değiştirmek için tıklayın" AllowSorting="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCustomDurumDegistir" runat="server" OnClick="lnkCustomDurumDegistir_OnClick"
                                CommandArgument='<%# Eval("Id") %>' Visible='<%# Eval("AnaSayfa").xToBooleanDefault() == false %>'>
                                                <%# Eval("Custom").xToBooleanDefault() == false ? "<img src='/admin/css/img/customDegil.png' title='Özel sayfa yapmak için tıklayın.' width='24px' height='24px' />" : "<img src='/admin/css/img/custom.png' title='Standart sayfa yapmak için tıklayın.' width='24px' height='24px'  />"%>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>--%>
                    <telerik:TreeListTemplateColumn HeaderText="A.Menü" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Açılır menü olarak kullan" AllowSorting="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAcilirMenu" runat="server" OnClick="lnkAcilirMenu_Click"
                                CommandArgument='<%# Eval("Id") %>'>
                                                <%# Eval("AcilirMenu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Ana menüde alt sayfaları göster.' width='24px' height='24px' />" : "<img src='/admin/css/img/dolu.png' title='Ana menüde alt sayfaları gösterme.' width='24px' height='24px'  />"%>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>
                    <telerik:TreeListTemplateColumn HeaderText="M.G." HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Ana menü görünürlüğü" AllowSorting="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkMenuGorunurluk" runat="server" OnClick="lnkMenuGorunurluk_OnClick"
                                CommandArgument='<%# Eval("Id") %>'>
                                                <%# Eval("Menu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sayfayı ana menüde görünür yapmak için tıklayın.' width='24px' height='24px' />" : "<img src='/admin/css/img/dolu.png' title='Sayfayı ana menüde görünmez yapmak için tıklayın.' width='24px' height='24px'  />"%>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>
                    <%--<telerik:TreeListTemplateColumn HeaderText="Y.M.G." HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Yan menü görünürlüğü" AllowSorting="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkYanMenuGorunurluk" runat="server" OnClick="lnkYanMenuGorunurluk_OnClick"
                                CommandArgument='<%# Eval("Id") %>'>
                                                <%# Eval("YanMenu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sayfayı yan menüde görünür yapmak için tıklayın.' width='24px' height='24px' />" : "<img src='/admin/css/img/dolu.png' title='Sayfayı yan menüde görünmez yapmak için tıklayın.' width='24px' height='24px'  />"%>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>--%>
                    <%--<telerik:TreeListTemplateColumn HeaderText="F.G." HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Footer (sayfa altı) menü görünürlüğü" AllowSorting="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFooterGorunurluk" runat="server" OnClick="lnkFooterGorunurluk_OnClick"
                                CommandArgument='<%# Eval("Id") %>'>
                                                <%# Eval("Footer").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sayfayı footer (sayfa altı) menüde görünür yapmak için tıklayın.' width='24px' height='24px' />" : "<img src='/admin/css/img/dolu.png' title='Sayfayı footer (sayfa altı) menüde görünmez yapmak için tıklayın.' width='24px' height='24px'  />"%>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>--%>
                    <telerik:TreeListTemplateColumn HeaderText="Yayında" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Sayfanın yayın durumu" AllowSorting="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkStatuDegistir" runat="server" OnClick="lnkStatuDegistir_Click"
                                CommandArgument='<%# Eval("Id") %>'>
                                                <%# Eval("Statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sayfayı yayına almak için tıklayın.' width='24px' height='24px' />" : "<img src='/admin/css/img/dolu.png' title='Sayfayı yayından kaldırmak için tıklayın.' width='24px' height='24px'  />"%>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>
                    <telerik:TreeListTemplateColumn HeaderText="Vitrin" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Sayfanın yayın durumu" AllowSorting="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkVitrin" runat="server" OnClick="lnkVitrin_Click"
                                CommandArgument='<%# Eval("Id") %>'>
                                                <%# Eval("Vitrin").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Ürünü vitrinde göstermek için tıklayın.' width='24px' height='24px' />" : "<img src='/admin/css/img/dolu.png' title='Ürünü vitrinden kaldırmak için tıklayın.' width='24px' height='24px'  />"%>
                            </asp:LinkButton>
                            <asp:HiddenField ID="hdnUrunMu" Value='<%#Eval("UrunMu").xToBooleanDefault() %>' runat="server" />
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>
                    <telerik:TreeListTemplateColumn HeaderText="Düzenle" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <a href='<%# Request.Url.AbsoluteUri.Contains("?iframe") ? "Pages.aspx?iframe&dzid="+ Eval("Id") : "Pages.aspx?dzid=" + Eval("Id") %>'>
                                <img src='css/img/duzenle.png' title='Sayfayı düzenlemek için tıklayın.' />
                            </a>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>
                    <telerik:TreeListTemplateColumn HeaderText="Sil" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_Click" CommandArgument='<%# Eval("Id") %>' CommandName='<%# Eval("Parent") %>' Visible='<%# (SessionManager.Admin.Finex ? true : !Eval("DefaultSayfa").xToBooleanDefault()) %>'>
                                             <img src="/admin/css/img/sil.png" />
                            </asp:LinkButton>
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                ConfirmText="Sayfayı silmek istediğinizden emin misiniz ?">
                            </asp:ConfirmButtonExtender>
                        </ItemTemplate>
                    </telerik:TreeListTemplateColumn>

                </Columns>
                <ClientSettings>
                    <ClientEvents OnItemSelected="OnItemSelected"></ClientEvents>
                    <Selecting AllowItemSelection="true"></Selecting>
                </ClientSettings>
            </telerik:RadTreeList>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvYeni">
            <table cellpadding="10" cellspacing="0" width="100%">
                <tr>
                    <td align="right">
                        <telerik:RadButton ID="btnNewPageClear" Text="+ Yeni Sayfa" runat="server" OnClick="btnNewPageClear_Click"
                            Skin="MetroTouch" Visible="false">
                        </telerik:RadButton>
                        <telerik:RadButton ID="RadButton1" Text="Kaydet >>" runat="server" OnClick="btnYeniSayfa_Click"
                            Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                            <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadTabStrip runat="server" ID="rtsYeni" MultiPageID="rmpYeni" SelectedIndex="0">
                            <Tabs>
                                <telerik:RadTab Text="Genel"></telerik:RadTab>
                                <telerik:RadTab Text="İçerik" Enabled="false"></telerik:RadTab>
                                <telerik:RadTab Text="Resim" Enabled="false"></telerik:RadTab>
                                <%--<telerik:RadTab Text="Video" Enabled="false"></telerik:RadTab>--%>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage runat="server" ID="rmpYeni" SelectedIndex="0">
                            <telerik:RadPageView runat="server" ID="rpvGenel">
                                <hr />
                                <asp:HiddenField ID="hdnDefaultSayfa" runat="server" />
                                <asp:HiddenField ID="hdnFizikselUrl" runat="server" />
                                <asp:HiddenField ID="hdnDynamic" runat="server" />
                                <table border="0" cellpadding="4" cellspacing="0">
                                    <tr>
                                        <td>
                                            <p class="formBaslik">Üst Sayfa</p>
                                            <hr />
                                            <input type="button" value="[X] Seçimi Temizle" onclick="UstSayfaSecimTemizle()" />
                                            <hr />
                                            <telerik:RadTreeView ID="trvSayfalar" runat="server" Width="180px" Height="300px" ClientIDMode="Static" ClientSettings-Selecting-AllowToggleSelection="True"></telerik:RadTreeView>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td valign="top">
                                            <table align="left">
                                                <%--<tr>
                                                    <td class="formBaslik">İkon
                                                    </td>
                                                    <td colspan="2">
                                                        <telerik:RadAsyncUpload ID="rauSayfaIkon" runat="server" Width="300px" MaxFileInputsCount="1"
                                                            MaxFileSize="5000000" TargetFolder="~/yukleme" MultipleFileSelection="Disabled" Skin="MetroTouch">
                                                        </telerik:RadAsyncUpload>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="imgSayfaIkon" runat="server" Visible="false" />&nbsp;
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="formBaslik">Adı
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtYeniSayfaAdi" runat="server" MaxLength="125" Skin="MetroTouch" ClientEvents-OnValueChanged="OnClientValueChanged"></telerik:RadTextBox>
                                                    </td>
                                                    <td class="formBaslik">Url
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtYeniSayfaUrl" runat="server" MaxLength="250" Skin="MetroTouch" ClientIDMode="Static"></telerik:RadTextBox>
                                                        <asp:CheckBox ID="chkDisLink" Text="Dış link" runat="server" ToolTip="Siteniz dışında başka bir adrese yönlendirme yapmak için tıklayın." ClientIDMode="Static" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Başlık
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtYeniSayfaBaslik" runat="server" MaxLength="250" Skin="MetroTouch"></telerik:RadTextBox>
                                                    </td>
                                                    <td class="formBaslik">Açıklama
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtYeniSayfaDesc" runat="server" MaxLength="250" Skin="MetroTouch"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Etiketler
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtYeniSayfaKeywords" runat="server" MaxLength="250" Skin="MetroTouch"></telerik:RadTextBox>
                                                    </td>
                                                    <td class="formBaslik">Fotoğraf Alanı Başlığı
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtFotoBaslik" runat="server" MaxLength="250" Skin="MetroTouch"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Video Alanı Başlığı
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtVideoBaslik" runat="server" MaxLength="250" Skin="MetroTouch"></telerik:RadTextBox>
                                                    </td>
                                                    <td class="formBaslik"></td>
                                                    <td></td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table>
                                                <%--<tr>
                                                    <td class="formBaslik">Blog Sayfası
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkBlogList" runat="server" />
                                                    </td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td class="formBaslik">Özel Sayfa
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkCustom" runat="server" />
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="formBaslik">Sayfa İçi Menü
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkSayfaMenu" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Başlık Alanı
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkBaslikAlani" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Sayfa Yolu/Adresi (Path)
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkSayfaYolu" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Paylaş Alanı
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkPaylasimAlani" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Ürün Mü?</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkUrunMu" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Haber Mi?</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkHaberMi" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Sol Alt Menü</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkSolAtlMenu" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Sağ Alt Menü</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkSagAtlMenu" runat="server" />
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td class="formBaslik">Fotoğraf Galerisi
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkFotoGaleri" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="formBaslik">Facebook Yorumları
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkFaceComments" runat="server" />
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>

                            <telerik:RadPageView runat="server" ID="rpvIcerik">
                                <hr />
                                <telerik:RadEditor DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.aspx" ID="edtYeniSayfaIcerik" runat="server" Width="100%" Height="350"
                                    Skin="MetroTouch" AllowScripts="True">
                                    <Content>
                                    </Content>
                                    <ImageManager DeletePaths="~/yukleme/resim" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/resim"
                                        ViewPaths="~/yukleme/resim" />
                                    <DocumentManager DeletePaths="~/yukleme/" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/"
                                        ViewPaths="~/yukleme/" SearchPatterns="*.jpg,*.doc,*.docx,*.txt,*.png" />
                                    <FlashManager DeletePaths="~/yukleme/flash" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/flash"
                                        ViewPaths="~/yukleme/flash" />
                                    <MediaManager DeletePaths="~/yukleme/media" EnableAsyncUpload="True" UploadPaths="~/yukleme/media" ViewPaths="~/yukleme/media" MaxUploadFileSize="100000000" />

                                    <CssFiles>
                                        <telerik:EditorCssFile Value="~/admin/css/editor.css" />
                                    </CssFiles>
                                </telerik:RadEditor>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="rpvResim">
                                <hr />
                                <table border="0" cellpadding="4" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkAktifEkle" Text="Fotoğrafları aktif olarak ekle" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--<td class="formBaslik">Açıklama
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtYeniResimAcikl" runat="server" MaxLength="150" Skin="MetroTouch"></telerik:RadTextBox>
                                        </td>--%>
                                        <td>
                                            <telerik:RadAsyncUpload ID="rauResmi" runat="server" Width="300px" MaxFileInputsCount="5"
                                                MaxFileSize="5000000" TargetFolder="~/galeri/resim/temp" MultipleFileSelection="Automatic" Skin="MetroTouch" HideFileInput="True">
                                            </telerik:RadAsyncUpload>
                                        </td>
                                        <td align="right">
                                            <telerik:RadButton ID="btnYeniResimYukle" runat="server" Text="Ekle" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White" OnClick="btnYeniResimYukle_Click">
                                                <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="row">
                                    <div class="large-2 column">
                                        <h1>Resimler
                                        </h1>
                                    </div>
                                    <div class="large-10 column text-right">
                                        <asp:Button ID="btnTumResimleriSil" Text="Tümünü Sil" runat="server" CssClass="button alert small right" OnClick="btnTumResimleriSil_OnClick" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnTumResimleriSil"
                                            ConfirmText="Bu sayfaya ait tüm resimleri silmek istediğinizden emin misiniz ?">
                                        </asp:ConfirmButtonExtender>
                                    </div>
                                </div>
                                <telerik:RadGrid ID="rgrvResimler" runat="server" PageSize="15" Skin="MetroTouch" OnPreRender="rgrvResimler_PreRender" OnNeedDataSource="rgrvResimler_NeedDataSource" AllowPaging="True" AllowSorting="true">
                                    <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" AutoGenerateColumns="false">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Resim" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <div class="galeriResim">
                                                        <a class="picture-gallery" href='<%# ResolveClientUrl(Eval("Buyuk").ToString()) %>'
                                                            title='<%# Eval("Aciklama") %>'>
                                                            <div class="thumb" style='background-image: url(<%# ResolveClientUrl(Eval("Orta").ToString().Replace("~","")) %>); background-position: 25% 25%;'>
                                                            </div>
                                                        </a>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Başlık" ItemStyle-Width="250px">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:TextBox ID="txtResimBaslik" runat="server" resimId='<%# Eval("Id") %>' Text='<%# Eval("Baslik") %>' placeholder="Başlık" OnTextChanged="txtResimBaslik_OnTextChanged" AutoPostBack="True" TabIndex='<%# Convert.ToInt16((Container.ItemIndex+1)) %>' MaxLength="75" />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Açıklama" ItemStyle-Width="450px">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:TextBox ID="txtResimAciklama" runat="server" resimId='<%# Eval("Id") %>' Text='<%# Eval("Aciklama") %>' placeholder="Açıklama" OnTextChanged="txtResimAciklama_OnTextChanged" AutoPostBack="True" TabIndex='<%# Convert.ToInt16((Container.ItemIndex+1)*10) %>' MaxLength="250" />

                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="İşlem">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="4" cellspacing="0" align="right">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtSiraNo" runat="server" Text='<%# Eval("Sira") %>' MaxLength="3" AutoPostBack="true" OnTextChanged="txtResimSiraNo_TextChanged" dzID='<%# Eval("Id") %>' Width="40px"></telerik:RadTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkStatuDegistir" runat="server" OnClick="lnkResimStatuDegistir_Click"
                                                                    CommandArgument='<%# Eval("Id") %>'>
                                                                    <%# Eval("Statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Resmi görünür yapmak için tıklayın.' width='15px' height='15px' />" : "<img src='/admin/css/img/dolu.png' title='Resmi görünmez yapmak için tıklayın.' width='15px' height='15px'  />"%>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkResimSil_Click" CommandArgument='<%# Eval("Id") %>'>
                                                                    <img src="/admin/css/img/sil.png" width="15px" height="15px" />
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                                        ConfirmText="Bu resmi silmek istediğinizden emin misiniz ?">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Ana Resim Seç">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rbAnaresim" runat="server" Style="text-decoration: inherit;" onclick="RadioCheck(this);" AutoPostBack="true" OnCheckedChanged="rbAnaresim_CheckedChanged" />
                                                    <asp:HiddenField ID="hfResimId" runat="server"
                                                        Value='<%#Eval("Id")%>' />
                                                    <asp:HiddenField ID="hfAnaResim" runat="server"
                                                        Value='<%#Eval("AnaResim")%>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="rpvVideo">
                                <hr />
                                <table border="0" cellpadding="0" cellspacing="4">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkVidAktifEkle" Text="Videoyu aktif olarak ekle" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            <asp:HiddenField ID="hdnKapakUrl" runat="server" ClientIDMode="Static" />
                                            <asp:Image ID="imgKapak" runat="server" ClientIDMode="Static" />
                                        </td>
                                        <td class="formBaslik">Youtube Kodu
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtVidKaynak" runat="server" MaxLength="75" Skin="MetroTouch" ClientIDMode="Static"></telerik:RadTextBox>
                                        </td>
                                        <td class="formBaslik">Başlık
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtVidBaslik" runat="server" MaxLength="75" Skin="MetroTouch" ClientIDMode="Static"></telerik:RadTextBox>
                                        </td>
                                        <td class="formBaslik">Açıklama
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtVidAciklama" runat="server" MaxLength="250" TextMode="MultiLine" Skin="MetroTouch" ClientIDMode="Static" Width="200px" Height="75px"></telerik:RadTextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>veya
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="formBaslik">Vimeo ID
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtVidKaynakVimeo" runat="server" MaxLength="75" Skin="MetroTouch" ClientIDMode="Static"></telerik:RadTextBox>
                                        </td>
                                        <td align="right" colspan="4">
                                            <telerik:RadButton ID="btnVideoEkle" runat="server" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White" Text="Ekle" OnClick="btnVideoEkle_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                                <div id="youtubeDataOutput"></div>
                                <hr />
                                <h1>Videolar
                                </h1>
                                <asp:Repeater ID="rptIcerikVideolar" runat="server">
                                    <ItemTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" align="left" style="margin: 1em">
                                            <tr>
                                                <td align="right">
                                                    <div class="galeriResim">
                                                        <a class="galeri fancybox-media" data-fancybox-group="gallery" href='<%# Eval("Kaynak") %>'
                                                            title='<%# Eval("Aciklama") %>'>
                                                            <img src='http://i.ytimg.com/vi/<%# Eval("UrlKodu") %>/0.jpg' alt="" width="120px" height="90px" />
                                                        </a>
                                                    </div>
                                                    <table border="0" cellpadding="4" cellspacing="0" align="right">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtSiraNo" runat="server" Text='<%# Eval("Sira") %>' MaxLength="3" AutoPostBack="true" OnTextChanged="txtVideoSiraNo_TextChanged" dzID='<%# Eval("Id") %>' Width="40px"></telerik:RadTextBox>
                                                            </td>

                                                            <td>
                                                                <asp:LinkButton ID="lnkVideoStatuDegistir" runat="server" OnClick="lnkVideoStatuDegistir_Click"
                                                                    CommandArgument='<%# Eval("Id") %>'>
                                                                    <%# Eval("Statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Resmi görünür yapmak için tıklayın.' width='15px' height='15px' />" : "<img src='/admin/css/img/dolu.png' title='Resmi görünmez yapmak için tıklayın.' width='15px' height='15px'  />"%>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkVideoSil" runat="server" OnClick="lnkVideoSil_Click" CommandArgument='<%# Eval("Id") %>'>
                                                                    <img src="/admin/css/img/sil.png" width="15px" height="15px" />
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkVideoSil"
                                                        ConfirmText="Bu videoyu silmek istediğinizden emin misiniz ?">
                                                    </asp:ConfirmButtonExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <hr />
                        <telerik:RadButton ID="btnYeniSayfa" Text="Kaydet >>" runat="server" OnClick="btnYeniSayfa_Click"
                            Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White" OnClientClicked="Validate">
                            <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvBloklar">
            <table width="100%">
                <tr>
                    <td style="width: 200px" valign="top">
                        <h4>Sayfa Seçin</h4>

                        <fieldset>
                            <telerik:RadTreeView ID="treeSayfalar" runat="server" Width="180px" Height="300px" OnNodeClick="treeSayfalar_OnNodeClick"></telerik:RadTreeView>
                        </fieldset>
                    </td>
                    <td valign="top">
                        <table width="100%">
                            <tr id="trInfo" runat="server" visible="False">
                                <td colspan="2">
                                    <%--   <p>
                                        Özel sayfanızın bloklarını aşağıdaki listeden yerleştirebilir, sıralayabilirsiniz. Yada aşağıdaki linke tıklayarak sürükle bırak ile aynı işlemleri yapabilirsiniz.
                                    </p>--%>
                                    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                        <div class="info">
                                            Sürükle bırak ile blokları yerleştirmek için <strong><a target="_blank" href='/BlokDuzenle.aspx?s=<%= treeSayfalar.SelectedValue %>'>tıklayın.</a></strong>
                                            <ul>
                                                <li>En soldaki listeden bir sayfa seçin.</li>
                                                <li>Seçili sayfaya eklemek istediğiniz blokları, sol taraftaki <b>BLOKLAR</b> listesinden <b>"Ekle"</b> butonuna tıklayarak, sağ taraftaki <b>SAYFA BLOKLARI</b> listesine ekleyebilirsiniz.  </li>
                                            </ul>
                                        </div>

                                    </telerik:RadCodeBlock>
                                </td>
                            </tr>
                            <tr id="trArrow" runat="server" visible="False">
                                <td colspan="2" align="center">
                                    <img src="css/img/sagdansola.png" style="margin-left: 36%" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 45%">
                                    <telerik:RadGrid ID="rgrvBloklar" runat="server" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowRowSelect="true">
                                        <MasterTableView Caption="BLOKLAR">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Adi" HeaderText="Adı/Başlık" />
                                                <telerik:GridTemplateColumn HeaderText="Düzenle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <a href='Bloklar.aspx?id=<%# Eval("Id") %>'>
                                                            <img src='css/img/duzenle.png' title='Bloğu düzenlemek için tıklayın.' width="16px" /></a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ekle" ItemStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <a href="#!" onclick='SB_PozisyonDegistirAc("<%# Eval("Id") %>")' title="Bu Blok'u seçili sayfaya eklmek için tıklayın.">
                                                            <img src="css/img/plus.png" width="16px" />
                                                        </a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                                <td valign="top">

                                    <div class="row">
                                        <div class="large-1 columns text-right"><b>Pozisyon</b></div>
                                        <div class="large-11 columns">
                                            <asp:DropDownList ID="ddlSayfaBloklariPozisyonlar" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSayfaBloklariPozisyonlar_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <telerik:RadGrid ID="rgrvSayfaBloklari" runat="server" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowRowSelect="true">
                                        <MasterTableView Caption="SAYFA BLOKLARI">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Adi" HeaderText="Adı/Başlık" />
                                                <telerik:GridTemplateColumn HeaderText="AR" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Arkaplan Rengi">
                                                    <ItemTemplate>
                                                        <telerik:RadColorPicker runat="server" ID="rcpArkaplanRengi" ShowIcon="true" PaletteModes="all" AutoPostBack="True" SelectedColor='<%# ColorTranslator.FromHtml(Eval("ArkaplanRengi").ToString()) %>' Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpArkaplanRengi_OnColorChanged" BlokId='<%# Eval("Id") %>' PozisyonId='<%# Eval("PozisyonId") %>'>
                                                        </telerik:RadColorPicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="MR" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Metin/Yazı Rengi">
                                                    <ItemTemplate>
                                                        <telerik:RadColorPicker runat="server" ID="rcpMetinRengi" ShowIcon="true" PaletteModes="all" AutoPostBack="True" SelectedColor='<%# ColorTranslator.FromHtml(Eval("MetinRengi").ToString()) %>' Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpMetinRengi_OnColorChanged" BlokId='<%# Eval("Id") %>' PozisyonId='<%# Eval("PozisyonId") %>'>
                                                        </telerik:RadColorPicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Yükseklik(px)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txtYukseklik" runat="server" Text='<%# Eval("Height") %>' AutoPostBack="True" OnTextChanged="txtYukseklik_OnTextChanged" BlokId='<%# Eval("Id") %>' Width="75px" PozisyonId='<%# Eval("PozisyonId") %>'></telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Pozisyon" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" SortExpression="PozisyonId">
                                                    <ItemTemplate>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <a href="#!" onclick='SB_PozisyonDegistirAc("<%# Eval("Id") %>","<%# Eval("PozisyonId") %>")' title="Blok yerini değiştirmek için tıklayın.">
                                                                <%# BLL.bllBlokPozisyonlari.Getir(Eval("PozisyonId").xToIntDefault()).Adi %>
                                                            </a>
                                                        </telerik:RadCodeBlock>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Başlık" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkBlokBaslik" runat="server" OnCheckedChanged="chkBlokBaslik_OnCheckedChanged" AutoPostBack="true" BlokId='<%# Eval("Id") %>' PozisyonId='<%# Eval("PozisyonId") %>' Checked='<%# Eval("BaslikKullanimi") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Çerçeve" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkBlokCerceve" runat="server" OnCheckedChanged="chkBlokCerceve_OnCheckedChanged" AutoPostBack="true" BlokId='<%# Eval("Id") %>' PozisyonId='<%# Eval("PozisyonId") %>' Checked='<%# Eval("CerceveKullanimi") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="ÇR" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" HeaderTooltip="Çerçeve Rengi">
                                                    <ItemTemplate>
                                                        <telerik:RadColorPicker runat="server" ID="rcpCerceveRengi" ShowIcon="true" PaletteModes="all" AutoPostBack="True" SelectedColor='<%# ColorTranslator.FromHtml(Eval("CerceveRengi").ToString()) %>' Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpCerceveRengi_OnColorChanged" BlokId='<%# Eval("Id") %>' PozisyonId='<%# Eval("PozisyonId") %>'>
                                                        </telerik:RadColorPicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Sıra" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txtBlokSiraNo" runat="server" Text='<%# Eval("Sira") %>' MaxLength="3" AutoPostBack="true" OnTextChanged="txtBlokSiraNo_OnTextChanged" BlokId='<%# Eval("Id") %>' Width="40px"></telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Kaldır" ItemStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSayfaBlokKaldır" runat="server" OnClick="lnkSayfaBlokKaldır_OnClick" CommandArgument='<%# Eval("Id") %>' PozisyonId='<%# Eval("PozisyonId") %>'>
                                                                    <img src="css/img/sil.png" width="16px" />
                                                        </asp:LinkButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSayfaBlokKaldır"
                                                            ConfirmText="Seçili sayfadan bloğu kaldırmak istediğinizden emin misiniz ?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <%--<hr />
            <iframe id="frm" runat="server" border="0" margin-height="0" margin-width="0" width="100%" height="1200" wmode="transparent"></iframe>--%>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <asp:HiddenField ID="hdnBlokEkleme" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnBlokId" runat="server" ClientIDMode="Static" />
    <telerik:RadWindow ID="modalPopup" runat="server" Modal="true" Skin="MetroTouch" Width="450" Height="650">
        <ContentTemplate>
            <div style="padding: 10px; text-align: center">
                <a href="css/img/blok_pozisyonlari_mevcut.jpg" class="picture-gallery" title="SOL ve SAĞ pozisyonları, yalnızca 3 Kolonlu şablonlarda görünür. Web sitenizin genel şablonunu değiştirmek için <a href='Ayarlar.aspx'>Ayarlar</a> > Tasarım > Genel Şablon sekmesini inceleyiniz.">
                    <img src="css/img/blok_pozisyonlari_mevcut.jpg" width="400px" /></a>
                <br />
                <small>Resmi büyütmek için tıklayın.</small>
            </div>
            <hr />
            <table width="100%">
                <tr>
                    <td colspan="2" class="formBaslik" align="center">Blok'u yerleştirmek için bir pozisyon seçin
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlPozisyonSec" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <telerik:RadButton ID="btnPozisyonSec" runat="server" Text="Kaydet" OnClick="btnPozisyonSec_OnClick" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                            <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <uc1:uscUyari ID="uscUyari1" runat="server" />
    <telerik:RadCodeBlock ID="rcbScript" runat="server">
        <% if (Request.QueryString["ss"] != null)
           {%>
        <div class="row">
            <hr />
            <div class="large-12 column text-center">
                <a href='#!' class="button small" onclick="SelectAndClose()">Tamam</a>
            </div>
        </div>
        <%} %>

        <script>
            function SB_PozisyonDegistirAc(id, pozId) {

                var wnd = $find("<%= modalPopup.ClientID %>");

                wnd.show();

                $("#hdnBlokId").val(id);

                if (!pozId) {
                    $("#hdnBlokEkleme").val("1");
                }

            }
        </script>
    </telerik:RadCodeBlock>

    <script>
        var selectedUrl;

        function getUrlParam(paramName) {
            var reParam = new RegExp('(?:[\?&]|&amp;)' + paramName + '=([^&]+)', 'i');
            var match = window.location.search.match(reParam);

            return (match && match.length > 1) ? match[1] : '';
        }

        function OnItemSelected(sender, eventArgs) {

            selectedUrl = eventArgs.get_item().get_dataKeyValue("Url");
        }


        function SelectAndClose() {
            var funcNum = getUrlParam('CKEditorFuncNum');
            var fileUrl = selectedUrl;
            window.opener.CKEDITOR.tools.callFunction(funcNum, fileUrl);
            window.close();
        }
    </script>
</asp:Content>
