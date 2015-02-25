<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true"
    CodeBehind="Ayarlar.aspx.cs" Inherits="Ws.admin.Ayarlar" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="/usc/uscUyari.ascx" TagName="uscUyari" TagPrefix="uc1" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/scripts/AjaxMethods.js"></script>
    <script type="text/javascript">
        function InfoAc() {
            $('.InfoContainer').show('slow');
        }
        function InfoKapat() {
            $('.InfoContainer').hide('slow');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.fotoZoom').fancybox();
        });

        function OnClientValueChanging(sender, args) {

            $("#lblMenuYuksekligi").html(args.get_newValue() + "px");
            //TopBarYukseklik(args.get_newValue());
        }
    </script>
    <style>
        #map-canvas
        {
            height: 450px;
            border: 4px solid #25a0da;
        }
    </style>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
    <script>
        function TemplateSec(file) {

            var data = '{"id":"' +<%= enEnumaration.enmTasarimAyarlari.Template %> +'","deger":"' + file + '"}';

            var req = AjaxPost("/services/general.asmx/TasarimAyarGuncelle", data);

            req.success(function (JSON) {
                alert(JSON.d);
            });

        }
    </script>
    <% string strMetinFontu = bllTasarimAyarlari.TasarimAyariGetir(Entity.enEnumaration.enmTasarimAyarlari.MetinFontu).Degeri;
       string strBaslikFontu = bllTasarimAyarlari.TasarimAyariGetir(Entity.enEnumaration.enmTasarimAyarlari.BaslikFontu).Degeri; %>

    <%= strMetinFontu.xBosMu() ? "" : "<link id='lnk" + strMetinFontu + "' href='http://fonts.googleapis.com/css?family=" + strMetinFontu + "' rel='stylesheet' type='text/css'>" %>
    <%= strBaslikFontu.xBosMu() ? "" : "<link id='lnk" + strBaslikFontu + "' href='http://fonts.googleapis.com/css?family=" + strBaslikFontu + "' rel='stylesheet' type='text/css'>" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab Text="Genel"></telerik:RadTab>
            <telerik:RadTab Text="Tasarım"></telerik:RadTab>
            <telerik:RadTab Text="Harita"></telerik:RadTab>
            <telerik:RadTab Text="Lisans Bilgileri"></telerik:RadTab>
            <telerik:RadTab Text="DB"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="RadPageView1">
            <div class="panel">
                <%--<div class="row">
                    <div class="large-7">
                        <div class="row">
                            <div class="large-3 columns">
                                <h5>Site içi arama</h5>
                            </div>
                            <div class="large-2 columns">
                                <div class="switch">
                                    <input id="inpSiteIciArama" type="checkbox">
                                    <label for="inpSiteIciArama"></label>
                                    <asp:HiddenField ID="hdnSiteIciArama" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="large-7 columns">
                                <div data-alert class="alert-box"><span class="fa fa-info-circle fa-2x"></span>&nbsp;&nbsp;Web sitenizin arama çubuğunu kapatır veya açar.</div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <div class="large-7">
                        <div class="row">
                            <div class="large-3 columns">
                                <h5>Google Map Kullanımı</h5>
                            </div>
                            <div class="large-2 columns">
                                <div class="switch">
                                    <input id="inpGoogleMap" type="checkbox">
                                    <label for="inpGoogleMap"></label>
                                    <asp:HiddenField ID="hdnGoogleMap" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="large-7 columns">
                                <div data-alert class="alert-box"><span class="fa fa-info-circle fa-2x"></span>&nbsp;&nbsp;İletişim sayfanızdaki harita görüntüsü.</div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="large-7">
                        <div class="row">
                            <div class="large-3 columns">
                                <h5>İletişim Formu Kullanımı</h5>
                            </div>
                            <div class="large-2 columns">
                                <div class="switch">
                                    <input id="inpIletisimFormu" type="checkbox">
                                    <label for="inpIletisimFormu"></label>
                                    <asp:HiddenField ID="hdnIletisimFormu" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="large-7 columns">
                                <div data-alert class="alert-box">
                                    <span class="fa fa-info-circle fa-2x"></span>&nbsp;&nbsp;Müşterilerinizin iletişim sayfasından size mail gönderebilmeleri
                                        içindir.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="large-7">
                        <div class="row">
                            <div class="large-3 columns">
                                <h5>İletişim Formu E-Posta</h5>
                            </div>
                            <div class="large-2 columns">
                                <asp:TextBox ID="txtIletisimEposta" runat="server" MaxLength="75" />
                            </div>
                            <div class="large-7 columns">
                                <div data-alert class="alert-box">
                                    <span class="fa fa-info-circle fa-2x"></span>&nbsp;&nbsp;Müşterilerinizin iletişim sayfasından size mesaj gönderecekleri e-posta adresi.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="large-7">
                        <div class="row">
                            <div class="large-3 columns">
                                <h5>E-Posta Sunucu</h5>
                            </div>
                            <div class="large-2 columns">
                                <asp:TextBox ID="txtMailSunucu" runat="server" MaxLength="75" />
                            </div>
                            <div class="large-7 columns">
                                <div data-alert class="alert-box">
                                    <span class="fa fa-info-circle fa-2x"></span>&nbsp;&nbsp;Genellikle mail.siteadiniz.com veya webmail.siteadiniz.com olarak kullanılır.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="large-7">
                        <div class="row">
                            <div class="large-3 columns">
                                <h5>Google Analytics</h5>
                            </div>
                            <div class="large-2 columns">
                                <asp:TextBox ID="txtAnalytics" runat="server" MaxLength="75" />
                            </div>
                            <div class="large-7 columns">
                                <div data-alert class="alert-box">
                                    <span class="fa fa-info-circle fa-2x"></span>&nbsp;&nbsp;Ziyaretçi istatistikleri için Google Analytics <b>İzleme Kimliği</b>. Ör: UA-52108111-1
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="large-7">
                        <asp:Button ID="btnGenelAyarlariKaydet" Text="Kaydet" runat="server" CssClass="button right success" OnClick="btnGenelAyarlariKaydet_OnClick" />
                    </div>
                </div>
            </div>
            <uc1:uscUyari ID="uscUyari2" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView2">
            <hr />
            <telerik:RadTabStrip runat="server" ID="RadTabStrip2" MultiPageID="RadMultiPage2" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="Genel"></telerik:RadTab>
                    <telerik:RadTab Text="Genel Şablon" Visible="False"></telerik:RadTab>
                    <telerik:RadTab Text="Tasarım Şablonu"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>

            <telerik:RadMultiPage runat="server" ID="RadMultiPage2" SelectedIndex="0">
                <telerik:RadPageView runat="server" ID="RadPageView4">
                    <div class="panel">
                        <div class="row">
                            <div class="large-4">
                                <div class="row">
                                    <div class="large-4 columns">
                                        <h5>Logo <a id="aSolUstGoster" href='<%= BLL.bllTasarimAyarlari.TasarimAyariGetir(Entity.enEnumaration.enmTasarimAyarlari.Logo).Degeri %>' class="picture-gallery"><span class="fa fa-search-plus"></span></a></h5>
                                    </div>
                                    <div class="large-8 columns">
                                        <telerik:RadAsyncUpload ID="uplSolUst" runat="server" MaxFileInputsCount="1" Culture="tr-TR"
                                            TargetFolder="~/Yukleme/resim/Logo" AllowedFileExtensions="jpg,jpeg,JPG,JPEG,png,PNG,gif,GIF"
                                            Localization-Cancel="İptal" Localization-Remove="Kaldır" Localization-Select="Seç" Skin="MetroTouch" HideFileInput="True" Width="150px">
                                        </telerik:RadAsyncUpload>
                                    </div>
                                    <div class="large-3 columns">
                                        <asp:LinkButton ID="lnkYukluResimlerdenSec" Text="Yüklü Resimlerden Seç" runat="server"
                                            OnClick="lnkYukluResimlerdenSec_Click" />
                                    </div>
                                    <div class="large-9 columns">
                                        <ul class="button-group right">
                                            <li>
                                                <asp:Button ID="btnSolResimYukle" Text="Kaydet" runat="server" OnClick="btnSolResimYukle_Click" CssClass="tiny button success" />
                                            </li>
                                            <li>
                                                <asp:Button ID="btnSolResimTemizle" runat="server" Text="Sil" OnClick="btnSolResimTemizle_Click" CssClass="tiny button alert"></asp:Button>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <asp:Panel ID="pnlYukluLogolar" runat="server" Visible="false" GroupingText="Yüklü Sol Üst Resimler">
                            <div id="dvSolUstResimler" style="max-height: 250px; overflow: auto;">
                                <asp:Repeater ID="rptSolUstResimler" runat="server">
                                    <ItemTemplate>
                                        <table border="0" cellpadding="4" cellspacing="0" style="border-bottom: 1px dotted silver; width: 300px">
                                            <tr>
                                                <td>
                                                    <a class="fotoZoom" href='/yukleme/resim/SolUst/<%# Eval("Adi") %>'>
                                                        <img src='/yukleme/resim/SolUst/<%# Eval("Adi") %>' alt="" width="75px" />
                                                    </a>
                                                </td>
                                                <td>
                                                    <%# Eval("Adi") %>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkSolUstSec" Text="Seç" runat="server" OnClick="lnkSolUstSec_Click"
                                                        CommandArgument='<%# Eval("Adi") %>' />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkResimSil" Text="[x]" ToolTip="Sil" runat="server" OnClick="lnkResimSil_Click"
                                                        CommandArgument='<%# Eval("Adi") %>' CommandName="SolUst" ForeColor="Red" />
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkResimSil"
                                                        ConfirmText="Resimi silmek istediğinizden emin misiniz ?">
                                                    </asp:ConfirmButtonExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:Panel>
                        <hr />
                        <div class="row">
                            <div class="large-4">
                                <div class="row">
                                    <div class="large-4 columns">
                                        <h5>Arkaplan Resmi <a id="a1" href='<%= BLL.bllTasarimAyarlari.TasarimAyariGetir(Entity.enEnumaration.enmTasarimAyarlari.ArkaPlanResmi).Degeri %>' class="picture-gallery"><span class="fa fa-search-plus"></span></a></h5>
                                    </div>
                                    <div class="large-8 columns">
                                        <telerik:RadAsyncUpload ID="uplArkaPlan" runat="server" MaxFileInputsCount="1" TargetFolder="~/Yukleme/resim/Arkaplanlar"
                                            AllowedFileExtensions="jpg,jpeg,JPG,JPEG,png,PNG,gif,GIF" OnClientFileSelected=""
                                            Localization-Cancel="İptal" Localization-Remove="Kaldır" Localization-Select="Seç" Skin="MetroTouch" HideFileInput="True" Width="150px">
                                        </telerik:RadAsyncUpload>
                                    </div>
                                    <div class="large-3 columns">
                                        <asp:LinkButton ID="lnkYukluArkaPlanlariGoster" Text="Yüklü Resimlerden Seç" runat="server"
                                            OnClick="lnkYukluArkaPlanlariGoster_Click" PostBackUrl="#dvArkaPlanlar" />
                                    </div>
                                    <div class="large-9 columns">
                                        <ul class="button-group right">
                                            <li>
                                                <asp:Button ID="btnArkaPlanYukle" Text="Kaydet" runat="server" OnClick="btnArkaPlanYukle_Click" CssClass="tiny button success" />
                                            </li>
                                            <li>
                                                <asp:Button ID="btnArkaPlanTemizle" runat="server" Text="Sil" OnClick="btnArkaPlanTemizle_Click" CssClass="tiny button alert"></asp:Button>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <asp:Panel ID="pnlYukleArkaPlanlar" runat="server" Visible="false" GroupingText="Yüklü Arka Planlar">
                            <div id="dvArkaPlanlar" style="max-height: 250px; overflow: auto;">
                                <asp:Repeater ID="rptArkaPlanlar" runat="server">
                                    <ItemTemplate>
                                        <table border="0" cellpadding="4" cellspacing="0" style="border-bottom: 1px dotted silver; width: 300px">
                                            <tr>
                                                <td>
                                                    <a class="fotoZoom" href='/yukleme/resim/Arkaplanlar/<%# Eval("Adi") %>'>
                                                        <img src='/yukleme/resim/Arkaplanlar/<%# Eval("Adi") %>' alt="" width="150px" />
                                                    </a>
                                                </td>
                                                <td>
                                                    <%# Eval("Adi") %>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkArkaPlanSec" Text="Seç" runat="server" OnClick="lnkArkaPlanSec_Click"
                                                        CommandArgument='<%# Eval("Adi") %>' />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkResimSil" Text="[x]" ToolTip="Sil" runat="server" OnClick="lnkResimSil_Click"
                                                        CommandArgument='<%# Eval("Adi") %>' CommandName="Arkaplanlar" ForeColor="Red" />
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkResimSil"
                                                        ConfirmText="Resimi silmek istediğinizden emin misiniz ?">
                                                    </asp:ConfirmButtonExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:Panel>
                        <hr />
                        <div class="row">
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Arkaplan Rengi</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpArkaPlan" ShowIcon="true" PaletteModes="all" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpArkaPlan_OnColorChanged" ShowEmptyColor="True" Width="100%">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                            <%--<div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Blok Arka Plan Rengi</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpBlokArkaPlan" ShowIcon="true" PaletteModes="all" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpBlokArkaPlan_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Blok Çerçeve Rengi</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpBlokCerceve" ShowIcon="true" PaletteModes="all" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpBlokCerceve_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>--%>
                            <div class="large-9 columns">
                                <div class="row">
                                    <div class="large-3 columns">
                                        <h5>Footer(Sayfa altı) Rengi</h5>
                                    </div>
                                    <div class="large-9 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpFooter" ShowIcon="true" PaletteModes="all" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpFooter_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Metin/Yazı Rengi</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpYaziRengi" ShowIcon="true" PaletteModes="all" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpYaziRengi_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Başlık Rengi</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpBaslikRengi" ShowIcon="true" PaletteModes="All" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpBaslikRengi_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Bağlantı Rengi 1</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpBaglantiRengi1" ShowIcon="true" PaletteModes="All" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpBaglantiRengi1_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Bağlantı Rengi 2</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpBaglantiRengi2" ShowIcon="true" PaletteModes="All" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpBaglantiRengi2_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-6 columns">
                                        <h5>Menü Arkaplan Rengi</h5>
                                    </div>
                                    <div class="large-6 columns">
                                        <telerik:RadColorPicker runat="server" ID="rcpMenuArkaPlan" ShowIcon="true" PaletteModes="all" AutoPostBack="True" Skin="MetroTouch" EnableCustomColor="true" OnColorChanged="rcpMenuArkaPlan_OnColorChanged">
                                        </telerik:RadColorPicker>
                                    </div>

                                </div>
                            </div>
                            <div class="large-2 columns">
                                <h5>Menü Yüksekliği</h5>
                                &nbsp;(<asp:Label ID="lblMenuYuksekligi" runat="server" ClientIDMode="Static" />) -
                                <asp:LinkButton ID="lnkMenuYukseklikTemizle" Text="Temizle" runat="server" OnClick="lnkMenuYukseklikTemizle_OnClick" />

                            </div>
                            <div class="large-3 columns">
                                <telerik:RadSlider ID="rsMenuYuksekligi" runat="server" MinimumValue="25" MaximumValue="100"
                                    SmallChange="5" LargeChange="10" ItemType="tick" Height="70px" Width="350px"
                                    AnimationDuration="400" CssClass="TicksSlider" ThumbsInteractionMode="Free" Skin="MetroTouch" OnClientValueChanging="OnClientValueChanging">
                                </telerik:RadSlider>
                            </div>
                            <div class="large-4 columns">
                                <asp:Button ID="btnMenuYuksekligiKaydet" runat="server" Text="Kaydet" OnClick="btnMenuYuksekligiKaydet_OnClick" CssClass="tiny button left success"></asp:Button>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="large-4 columns">
                                <div class="row">
                                    <div class="large-4 columns">
                                        <h5>Metin/Yazı Tipi (Font)</h5>
                                    </div>
                                    <div class="large-8 columns">
                                        <asp:Label ID="lblMetinFontu" ClientIDMode="Static" runat="server" Text="Lorem İpsum (şçöğüı)" Font-Size="16px" /><br />
                                        <a href="#!" onclick="FontSecimiGoster('hdnMetinFontu','lblMetinFontu')" class="tiny button">Font Seç</a><asp:Button ID="btnMetinFontuTemizle" Text="Temizle" runat="server" CssClass="tiny secondary button" OnClick="btnMetinFontuTemizle_OnClick" />
                                        <asp:HiddenField ID="hdnMetinFontu" ClientIDMode="Static" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="large-5 columns">
                                <div class="row">
                                    <div class="large-4 columns">
                                        <h5>Başlık Tipi (Font)</h5>
                                    </div>
                                    <div class="large-8 columns">
                                        <h4>
                                            <asp:Label ID="lblBaslikFontu" ClientIDMode="Static" runat="server" Text="Lorem İpsum (şçöğüı)" /></h4>
                                        <br />
                                        <a href="#!" onclick="FontSecimiGoster('hdnBaslikFontu','lblBaslikFontu')" class="tiny button">Font Seç</a><asp:Button ID="btnBaslikFontuTemizle" Text="Temizle" runat="server" CssClass="tiny secondary button" OnClick="btnBaslikFontuTemizle_OnClick" />
                                        <asp:HiddenField ID="hdnBaslikFontu" ClientIDMode="Static" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="large-1 columns end">
                                <asp:Button ID="btnFontSec" Text="Font Seçimini Kaydet" runat="server" CssClass="tiny button success" OnClick="btnFontSec_OnClick" />
                            </div>
                            <div class="clear"></div>
                            <div id="pnlFontSecimi" class="panel hide">
                                <iframe id="ifrmFontSecimi" border="0" margin-height="0" margin-width="0" width="100%" height="450" wmode="transparent"></iframe>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="RadPageView6">
                    <asp:Repeater ID="rptSablonlar" runat="server" OnItemDataBound="rptSablonlar_ItemDataBound">
                        <ItemTemplate>
                            <div id="dvSablon" runat="server" style='float: left; width: auto; margin: 5px; padding: 5px;'>
                                <asp:LinkButton ID="lnkSablonSecCift" runat="server" OnClick="SablonSec_Click" CommandArgument='<%# Eval("Id") %>'
                                    PostBackUrl="#frm">
                                        <img src='<%# Eval("Ikon") %>' width="150px" />
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <hr />
                    <iframe id="frm" src="/Default.aspx" border="0" margin-height="0" margin-width="0" width="100%" height="650" wmode="transparent"></iframe>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="RadPageView5">
                    <hr />
                    <p><b>Kullanılan template :</b> <%= Settings.Tasarim.sTemplate %> - <asp:LinkButton ID="lnkTemplateTemizle" runat="server" Text="[Temizle]" OnClick="lnkTemplateTemizle_OnClick" /></p> 
                    <asp:Repeater ID="rptTemplates" runat="server">
                        <ItemTemplate>
                            <div class="galeriResim left margin-10px <%# Eval("FileName").ToString().Trim() == Settings.Tasarim.sTemplate ? " secili":""  %>">
                                <a class="galeri picture-gallery" href='<%# Eval("ImageBig") %>'>
                                    <div class="thumb width-200px" style='background-image: url(<%# Eval("ImageThumb") %>); background-position: left top;'>
                                    </div>
                                </a>
                                <b><%# Eval("DisplayName") %></b>
                                <br />
                                <%# Eval("FileName").ToString().Trim() == Settings.Tasarim.sTemplate ? "":("<a href='#!' class='button small' onclick=\"TemplateSec('"+Eval("FileName")+"')\">Seç</a>")  %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView3">
            <div class="panel">
                <div class="row">
                    <div class="large-11 columns">
                        <ul class="info">
                            <li>Google map, iletişim sayfanızdaki harita görüntüsüdür.</li>
                            <li>Haritada yerinizi belirlemek için işaretçiyi <b>(marker)</b> sürükleyebilirsiniz.</li>
                        </ul>
                    </div>
                    <div class="large-1 columns">
                        <telerik:RadButton ID="RadButton1" runat="server" Text="Kaydet" OnClick="btnGmapKaydet_Click" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                            <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="row">
                    <div id="map-canvas" class="large-12">
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="large-12">
                        <table border="0" cellpadding="4" cellspacing="0" align="left" width="48%">
                            <tr>
                                <td>Latitude
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLat" runat="server" MaxLength="15" Skin="MetroTouch" ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td>
                                    <p class="info mini">Lütfen "." (nokta) kullanınız.</p>
                                </td>
                                <td>Longitude
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLong" runat="server" MaxLength="15" Skin="MetroTouch" ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td>
                                    <p class="info mini">Lütfen "." (nokta) kullanınız.</p>
                                </td>
                            </tr>
                            <tr>
                                <td>Marker Metni
                                </td>
                                <td colspan="5">
                                    <telerik:RadEditor DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.aspx" ID="edtGmapMetin" runat="server" Width="100%" Skin="MetroTouch" ToolsFile="~/admin/toolsfile/Basic.xml" Height="250">
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="right">
                                    <telerik:RadButton ID="btnGmapKaydet" runat="server" Text="Kaydet" OnClick="btnGmapKaydet_Click" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                                        <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                        <div class="clear"></div>
                    </div>
                </div>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView7">
            <table border="0" cellpadding="4" cellspacing="0">
                <tr>
                    <td>Yayın Durumu
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkYayinDurumuDegistir" runat="server" OnClick="lnkYayinDurumuDegistir_OnClick" Enabled='<%# SessionManager.Admin.Finex %>'>
                            <asp:Image ID="imgYayinDurum" runat="server" />
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>Yayın Tarihi
                    </td>
                    <td>
                        <asp:TextBox ID="txtYayinTar" runat="server" Enabled='<%# SessionManager.Admin.Finex %>' />
                    </td>
                </tr>
                <tr>
                    <td>Yayın Süre
                    </td>
                    <td>
                        <asp:TextBox ID="txtYayinSure" runat="server" Enabled='<%# SessionManager.Admin.Finex %>' />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnLisansBilgiGuncelle" Text="Güncelle" runat="server" OnClick="btnLisansBilgiGuncelle_OnClick" Visible='<%# SessionManager.Admin.Finex %>' />
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView8">
            <asp:Panel ID="pnlDatabase" runat="server" GroupingText="Veritabanı Değiştir">
                <table cellpadding="6">
                    <tr>
                        <td>Database</td>
                        <td>
                            <asp:TextBox ID="txtDatabaseName" runat="server" /></td>
                        <td>
                            <asp:Button ID="btnDatabaseKaydet" Text="Kaydet" runat="server" OnClick="btnDatabaseKaydet_OnClick" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlAddTable" runat="server" GroupingText="Create / Drop Table">

                <table>
                    <tr>
                        <td>Table Name</td>
                        <td>
                            <asp:TextBox ID="txtTableName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Primary Key Name</td>
                        <td>
                            <asp:TextBox ID="txtTablePrimaryKey" runat="server" />
                        </td>
                    </tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnAddTable" Text="Create" runat="server" OnClick="btnAddTable_OnClick" />
                    </td>
                </table>
                <hr />
                <div style="max-height: 350px; overflow: auto">
                    <telerik:RadGrid ID="rgrvTables" runat="server" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowRowSelect="true">
                        <MasterTableView Caption="TABLES">
                            <Columns>
                                <telerik:GridBoundColumn DataField="TABLE_NAME" HeaderText="Name" />
                                <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSilTable" runat="server" OnClick="lnkSilTable_OnClick" CommandArgument='<%# Eval("TABLE_NAME") %>'>
                                    <img src="css/img/sil.png" width="16px" />
                                        </asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSilTable"
                                            ConfirmText="Are you sure to drop the table ?">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlAddColumn" runat="server" GroupingText="Add / Drop Column">
                <table>
                    <tr>
                        <td>Table Name</td>
                        <td>
                            <asp:DropDownList ID="ddlTables" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTables_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Columns Name</td>
                        <td>
                            <asp:TextBox ID="txtColumnName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Data Type</td>
                        <td>
                            <asp:DropDownList ID="ddlDataTypes" runat="server">
                                <asp:ListItem Text="Text" Value="VARCHAR" />
                                <asp:ListItem Text="Integer" Value="Integer" />
                                <asp:ListItem Text="Yes/No" Value="Bit" />
                                <asp:ListItem Text="Date" Value="Date" />
                                <asp:ListItem Text="LongText" Value="Memo" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnColumnEkle" Text="Add" runat="server" OnClick="btnColumnEkle_OnClick" />
                        </td>
                    </tr>
                </table>
                <hr />
                <div style="max-height: 350px; overflow: auto">
                    <telerik:RadGrid ID="rgrvColumns" runat="server" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowRowSelect="true">
                        <MasterTableView Caption="COLUMNS">
                            <Columns>
                                <telerik:GridBoundColumn DataField="COLUMN_NAME" HeaderText="Name" />
                                <telerik:GridTemplateColumn HeaderText="Type">
                                    <ItemTemplate>
                                        <%# GetDataType(Eval("DATA_TYPE").ToString()) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_OnClick" CommandArgument='<%# Eval("COLUMN_NAME") %>'>
                                    <img src="css/img/sil.png" width="16px" />
                                        </asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                            ConfirmText="Are you sure to drop the column ?">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    <uc1:uscUyari ID="uscUyari1" runat="server" />
    <script>
        
        var gMap = $("#hdnGoogleMap").val();
        
        if (gMap == "True") {
            $("#inpGoogleMap").attr("checked", "");
        } else {
            $("#inpGoogleMap").removeAttr("checked");
        }

        //var iForm = $("#hdnIletisimFormu").val();

        //if (iForm == "True") {
        //    $("#inpInpIletisimFormu").attr("checked", "");
        //} else {
        //    $("#inpInpIletisimFormu").removeAttr("checked");
        //}


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
                    draggable: true,
                    map: map,
                    title: 'title'
                });
                google.maps.event.addDomListener(marker, 'dragend', function () {
                    $("#txtLat").val(marker.position.lat());
                    $("#txtLong").val(marker.position.lng());
                });


            });
        }

        google.maps.event.addDomListener(window, 'load', initialize);

        var hdnFont;
        var lblFont;

        function FontSec(name) {

            var $headlink = $("head").find("link[id='lnk" + name + "']");

            var linkElement = "<link id='lnk" + name + "' href='http://fonts.googleapis.com/css?family=" + name + "' rel='stylesheet' type='text/css'>";

            if ($headlink.length) {

            }
            else {
                $("head").append(linkElement);
            }

            $("#" + hdnFont).val(name);
            $("#" + lblFont).css("font-family", name);
        }

        $(document).ready(function () {
            
            //var siteArama = $("#hdnSiteIciArama").val();

            //if (siteArama == "True") {
            //    $("#inpSiteIciArama").attr("checked", "");
            //} else {
            //    $("#inpSiteIciArama").removeAttr("checked");
            //}

            


        });

        function FontSecimiGoster(hdn, lbl) {
            $("#pnlFontSecimi").removeClass("hide");
            $("#ifrmFontSecimi").attr("src", "WebFonts.aspx");
            hdnFont = hdn;
            lblFont = lbl;
            location.href = "#pnlFontSecimi";
        }

    </script>
</asp:Content>
