<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="Carousel.aspx.cs" Inherits="Ws.admin.Carousel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="/usc/uscUyari.ascx" TagName="uscUyari" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgrvCarousel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvCarousel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lnkOnizleme">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ltrOnizleme" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab Text="Liste"></telerik:RadTab>
            <telerik:RadTab Text="+ Yeni"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="RadPageView1">
            <telerik:RadGrid ID="rgrvCarousel" runat="server" Skin="MetroTouch">
                <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" AutoGenerateColumns="false">
                    <PagerStyle Mode="NumericPages" />
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="No." ItemStyle-Width="15px">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Seç" ItemStyle-Width="25px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCarouselSec" runat="server" OnClick="lnkCarouselSec_OnClick" Visible='<%# Request.Url.AbsoluteUri.Contains("?iframe") %>' CommandArgument='<%# Eval("Id") %>'>
                                    <img src='css/img/<%# CarouselSeciliMi(Eval("Id").xToIntDefault()) ? "checked.png" : "notchecked.png" %>' />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Carousel Adı">
                            <ItemTemplate>
                                <%# Eval("Adi") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Tekrar Sayisi">
                            <ItemTemplate>
                                <%# Eval("TekrarSayisi") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Resim Sayisi">
                            <ItemTemplate>
                                <%# Eval("ResimSayisi") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Önizleme">
                            <ItemTemplate>
                                <a class="iframe" href='CarouselOnizleme.aspx?iframe&id=<%# Eval("Id") %>'>Göster</a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Aktif/Pasif" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkStatuDegistir" runat="server" OnClick="lnkStatuDegistir_Click"
                                    CommandArgument='<%# Eval("Id") %>'>
                                    <%# Eval("Statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Carouselı görünür yapmak için tıklayın.' />" : "<img src='/admin/css/img/dolu.png' title='Carouselı görünmez yapmak için tıklayın.' />"%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Düzenle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                            <ItemTemplate>
                                <a href='<%# Request.Url.AbsoluteUri.Contains("?iframe") ? "CarouselDuzenle.aspx?iframe&pageId="+Request.QueryString["id"]+"&id="+ Eval("Id") : "CarouselDuzenle.aspx?id=" + Eval("Id") %>'>
                                    <img src='css/img/duzenle.png' title='Sayfayı düzenlemek için tıklayın.' />
                                </a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_OnClick" CommandArgument='<%# Eval("Id") %>'>
                                            <img src="css/img/sil.png" />
                                </asp:LinkButton>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                    ConfirmText="Bu carouseli tüm resimleri ile birlikte silmek istediğinizden emin misiniz ?">
                                </asp:ConfirmButtonExtender>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView2">
            <div class="panel">
                <table border="0" cellpadding="4" cellspacing="0">
                    <tr>
                        <td class="formBaslik">Adı
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtYeniCarouselAdi" runat="server" MaxLength="120" Skin="MetroTouch"></telerik:RadTextBox>
                        </td>
                        <td class="formBaslik">Tekrar Sayısı
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTekrarSayisi" runat="server" MaxLength="2" Skin="MetroTouch"></telerik:RadTextBox>
                        </td>
                        <td class="formBaslik">Resim Gösterim Süresi(sn.)
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtGosterimSuresi" runat="server" MaxLength="2" Skin="MetroTouch"></telerik:RadTextBox>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnYeniCarousel" runat="server" Text="Kaydet" Skin="MetroTouch" OnClick="btnYeniCarousel_OnClick"></telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <div class="row">
        <div class="large-12 column">
            <asp:Literal ID="ltrOnizleme" runat="server" />
        </div>
    </div>
    <uc1:uscUyari ID="uscUyari1" runat="server" />
</asp:Content>
