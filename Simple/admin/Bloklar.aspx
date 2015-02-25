<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true"
    CodeBehind="Bloklar.aspx.cs" Inherits="Ws.admin.Bloklar" %>

<%@ Import Namespace="Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgrvBloklar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvBloklar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab Text="Bloklar"></telerik:RadTab>
            <telerik:RadTab Text="+ Yeni Blok"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="RadPageView1">
            <div class="panel">
                <div class="row">
                    <div class="large-12">
                        <telerik:RadGrid ID="rgrvBloklar" runat="server" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowRowSelect="true" AllowPaging="True" PageSize="10" AllowSorting="True" OnNeedDataSource="rgrvBloklar_OnNeedDataSource">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Adi" HeaderText="Adı/Başlık" SortExpression="Adi" />
                                    <telerik:GridTemplateColumn HeaderText="Durum" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSolStatuDegistir" runat="server" OnClick="lnkStatuDegistir_Click"
                                                CommandArgument='<%# Eval("Id") %>'>
                                    <%# Eval("Statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Görünür yapmak için tıklayın.' width='16px' />" : "<img src='/admin/css/img/dolu.png' title='Görünmez yapmak için tıklayın.'  width='16px' />"%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Düzenle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <a href='Bloklar.aspx?id=<%# Eval("Id") %>'>
                                                <img src='css/img/duzenle.png' title='Bloğu düzenlemek için tıklayın.' width="16px" /></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_Click" CommandArgument='<%# Eval("Id") %>' Visible='<%# (SessionManager.Admin.Finex ? true : !Eval("Default").xToBooleanDefault()) %>'>
                                    <img src="css/img/sil.png" width="16px" />
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                                ConfirmText="Bu Blok'u silmek istediğinizden emin misiniz ?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView3">
            <div class="panel">
                <div class="row">
                    <div class="large-12">
                        <table cellpadding="4" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    <telerik:RadButton ID="btnNewBlokClear" Text="+ Yeni Blok" runat="server" OnClick="btnNewBlokClear_Click"
                                        Skin="MetroTouch" Visible="false">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="RadButton1" runat="server" Text="Kaydet" OnClick="btnYeniBlokKaydet_Click" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                                        <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr id="trSayfaListeleHr" runat="server">
                                <td>
                                    <hr />
                                </td>
                            </tr>
                            <tr id="trSayfaListeleSec" runat="server">
                                <td>
                                    <table>
                                        <tr>
                                            <td class="formBaslik">Sayfa Seçimi 
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkSayfaSec" Text="Seç" runat="server" OnClientClick="return SayfaSecimModalAc()" ClientIDMode="Static" />
                                                <%if (hdnSayfaID.Value.xBosMu() == false)
                                                  {%>
                                                <a href="#" onclick="SecimiTemizle(this)">[X] Seçimi Temizle</a>
                                                <%} %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formBaslik">Slider / Carousel Seçimi
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkCarouselSec" Text="Seç" runat="server" OnClientClick="return CarouselSecimModalAc()" ClientIDMode="Static" />
                                                <%if (hdnCarouselID.Value.xBosMu() == false)
                                                  {%>
                                                <a href="#" onclick="CarouselSecimiTemizle(this)">[X] Seçimi Temizle</a>
                                                <%} %>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="left">
                                        <tr>
                                            <td class="formBaslik">Adı / Başlık
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtYeniBlokAdi" runat="server" MaxLength="100" Skin="MetroTouch"></telerik:RadTextBox>
                                            </td>
                                            <td class="formBaslik">Açıklama
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtYeniBlokAciklama" runat="server" MaxLength="150" Skin="MetroTouch"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr />
                                    <p class="formBaslik">
                                        İçerik
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadEditor DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.aspx" ID="edtIcerik" runat="server" Skin="MetroTouch" Width="100%" Height="350px" IsSkinTouch="True" AllowScripts="true">
                                        <Content>
                                        </Content>
                                        <ImageManager DeletePaths="~/yukleme/resim" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/resim"
                                            ViewPaths="~/yukleme/resim" />
                                        <DocumentManager DeletePaths="~/yukleme/" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/"
                                            ViewPaths="~/yukleme/" SearchPatterns="*.jpg,*.doc,*.docx,*.txt,*.png" />
                                        <FlashManager DeletePaths="~/yukleme/flash" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/flash"
                                            ViewPaths="~/yukleme/flash" />
                                        <MediaManager DeletePaths="~/yukleme/media" EnableAsyncUpload="True" UploadPaths="~/yukleme/media" ViewPaths="~/yukleme/media" MaxUploadFileSize="100000000" />
                                        <ExportSettings>
                                            <Pdf>
                                                <PageHeader>
                                                    <LeftCell Text="" />
                                                    <MiddleCell Text="" />
                                                    <RightCell Text="" />
                                                </PageHeader>
                                                <PageFooter>
                                                    <LeftCell Text="" />
                                                    <MiddleCell Text="" />
                                                    <RightCell Text="" />
                                                </PageFooter>
                                            </Pdf>
                                        </ExportSettings>
                                        <CssFiles>
                                            <telerik:EditorCssFile Value="~/admin/css/editor.css" />
                                        </CssFiles>
                                        <TrackChangesSettings CanAcceptTrackChanges="False" />
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <telerik:RadButton ID="btnYeniBlokKaydet" runat="server" Text="Kaydet" OnClick="btnYeniBlokKaydet_Click" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                                        <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <asp:HiddenField ID="hdnID" runat="server" ClientIDMode="Static" />

    <asp:HiddenField ID="hdnSayfaID" runat="server" ClientIDMode="Static" />
    <telerik:RadWindow ID="modSayfaSec" runat="server" Modal="true" Skin="MetroTouch" Height="600" Width="500">
        <ContentTemplate>
            <table align="center">
                <tr>
                    <td align="center">
                        <span class="formBaslik">Sayfa Seç</span>
                        <telerik:RadTreeView ID="trvSayfalar" runat="server"></telerik:RadTreeView>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                        <telerik:RadButton ID="btnSayfaKaydet" runat="server" Text="Seç" OnClick="btnSayfaKaydet_Click" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                            <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>

    <asp:HiddenField ID="hdnCarouselID" runat="server" ClientIDMode="Static" />
    <telerik:RadWindow ID="modCarouselSec" runat="server" Modal="true" Skin="MetroTouch" Height="600" Width="500">
        <ContentTemplate>

            <telerik:RadGrid ID="rgrvCarousel" runat="server" Skin="MetroTouch" Width="100%">
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" AutoGenerateColumns="false">
                    <PagerStyle Mode="NumericPages" />
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="No." ItemStyle-Width="15px">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Adı">
                            <ItemTemplate>
                                <%# Eval("Adi") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

            <telerik:RadButton ID="btnCarouselSec" runat="server" Text="Seç" OnClick="btnCarouselSec_OnClick" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
            </telerik:RadButton>

        </ContentTemplate>
    </telerik:RadWindow>
    <script>

        function SayfaSecimModalAc() {

            var wnd = $find("<%=modSayfaSec.ClientID %>");

            wnd.show();

            return false;
        }

        function SecimiTemizle(a) {
            $("#hdnSayfaID").val('');
            $("#lnkSayfaSec").html("Seç");
            $(a).hide();
        }

        function CarouselSecimModalAc() {

            var wnd = $find("<%=modCarouselSec.ClientID %>");

            wnd.show();

            return false;
        }

        function CarouselSecimiTemizle(a) {
            $("#hdnCarouselID").val('');
            $("#lnkCarouselSec").html("Seç");
            $(a).hide();
        }

    </script>
</asp:Content>
