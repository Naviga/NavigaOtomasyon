<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="SosyalMedya.aspx.cs" Inherits="Ws.admin.SosyalMedya" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="/usc/uscUyari.ascx" TagName="uscUyari" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grvKullanicilar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvKullanicilar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch">
    </telerik:RadAjaxLoadingPanel>
    <div class="row">
        <div class="large-12">
            <div data-alert class="alert-box">
                <ul>
                    <li>Web sitenizi farklı bölümlerinde kullanabileceğiniz sosyal medya hesaplarınızı tanımlayın.</li>
                    <li><b>Yeni Sosyal Medya Hesabı </b> tanımlamak için aşağıdaki formda <b>Sosyal Medya adı ve şifre</b> yazdıktan sonra <b>Kaydet</b> butonuna tıklayın.</li>
                </ul>
            </div>
        </div>
    </div>
    <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab Text="Hesaplar"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="RadPageView1">
            <div class="panel">
                <div class="row">
                    <div class="large-12">
                        <div class="row">
                            <div class="large-1 columns">
                                <h5>Hesap Adı</h5>
                            </div>
                            <div class="large-2 columns">
                                <asp:TextBox ID="txtSosyalAdi" runat="server" MaxLength="60" CssClass="textBoxUzun" />
                            </div>
                            <div class="large-1 columns">
                                <h5>Adresi (URL)</h5>
                            </div>
                            <div class="large-2 columns">
                                <asp:TextBox ID="txtSosyalAdres" runat="server" MaxLength="60" CssClass="textBoxUzun" />
                            </div>
                            <div class="large-1 columns">
                                <h5>İkon (Resim)</h5>
                            </div>
                            <div class="large-3 columns">
                                <div class="row">
                                    <div class="large-1 columns">
                                        <asp:Literal ID="ltrIkon" runat="server" />
                                    </div>
                                    <div class="large-11 columns">
                                        <telerik:RadAsyncUpload ID="rauSosyalMedya" runat="server" MaxFileInputsCount="1" Culture="tr-TR"
                                            TargetFolder="~/Yukleme/resim/Ikon/" AllowedFileExtensions="jpg,jpeg,JPG,JPEG,png,PNG,gif,GIF"
                                            Localization-Cancel="İptal" Localization-Remove="Kaldır" Localization-Select="Seç" Skin="MetroTouch" HideFileInput="True" Width="150px">
                                        </telerik:RadAsyncUpload>
                                    </div>
                                </div>
                            </div>
                            <div class="large-2 columns">
                                <asp:Button ID="btnSosyalMedya" Text="Kaydet" runat="server" OnClick="btnSosyalMedya_OnClick"
                                    CssClass="tiny success button" />
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="large-12">
                        <telerik:RadGrid ID="rgrvSosyalMedya" runat="server" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowRowSelect="true" AllowPaging="True" PageSize="10" AllowSorting="True" OnNeedDataSource="grvKullanicilar_OnNeedDataSource">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="No." ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ikon">
                                        <ItemTemplate>
                                            <img src='<%# Eval("sos_ikonu") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Hesap" DataField="sos_adi" SortExpression="sos_adi" />
                                    <telerik:GridBoundColumn HeaderText="Url" DataField="sos_url" SortExpression="sos_url" />
                                    <telerik:GridTemplateColumn HeaderText="M.G." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderTooltip="Menüde göster">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkMenudeGoster" runat="server" OnClick="lnkMenudeGoster_OnClick"
                                                CommandArgument='<%# Eval("sos_id") %>'>
                                                <%# Eval("sos_menu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sosyal Medyayı menüde göstermek için tıklayın.' />" : "<img src='/admin/css/img/dolu.png' title='Sosyal Medyayı menüden kaldırmak için tıklayın.' />"%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Y.M.G." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderTooltip="Yan Menüde göster">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkYanMenudeGoster" runat="server" OnClick="lnkYanMenudeGoster_OnClick"
                                                CommandArgument='<%# Eval("sos_id") %>'>
                                                <%# Eval("sos_yanMenu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sosyal Medyayı yan menüde göstermek için tıklayın.' />" : "<img src='/admin/css/img/dolu.png' title='Sosyal Medyayı yan menüden kaldırmak için tıklayın.' />"%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="F.G." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderTooltip="Footer(Sayfa altı)'da göster">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFooterdaGoster" runat="server" OnClick="lnkFooterdaGoster_OnClick"
                                                CommandArgument='<%# Eval("sos_id") %>'>
                                                <%# Eval("sos_statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sosyal Medyayı footer (sayfa altı) da göstermek için tıklayın.' />" : "<img src='/admin/css/img/dolu.png' title='Sosyal Medyayı footer (sayfa altı) dan kaldırmak için tıklayın.' />"%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Aktif/Pasif" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkStatuDegistir" runat="server" OnClick="lnkStatuDegistir_Click"
                                                CommandArgument='<%# Eval("sos_id") %>'>
                                                <%# Eval("sos_statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Sosyal Medyayı aktif yapmak için tıklayın.' />" : "<img src='/admin/css/img/dolu.png' title='Sosyal Medyayı pasif yapmak için tıklayın.' />"%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Düzenle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDuzenle" runat="server" CommandArgument='<%# Eval("sos_id") %>' OnClick="lnkDuzenle_OnClick">
                                                <img src='css/img/duzenle.png' title='Bloğu düzenlemek için tıklayın.' width="16px" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_Click" CommandArgument='<%# Eval("sos_id") %>'>
                                            <img src="css/img/sil.png" />
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                                ConfirmText="Bu Sosyal Medyayı silmek istediğinizden emin misiniz ?">
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

    </telerik:RadMultiPage>
    <uc1:uscUyari ID="uscUyari1" runat="server" />
</asp:Content>
