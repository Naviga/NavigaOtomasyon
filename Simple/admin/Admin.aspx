<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true"
    CodeBehind="Admin.aspx.cs" Inherits="Ws.admin.Admin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="/usc/uscUyari.ascx" TagName="uscUyari" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function SifreGoster(dv) {
            if ($("#" + dv).hasClass("hide")) {
                $("#" + dv).removeClass("hide");
            } else {
                $("#" + dv).addClass("hide");
            }
        }
    </script>
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
                    <li>Web sitenizi yönetebilecek kişileri burdan tanımlayabilirsiniz.</li>
                    <li><b>Yeni Kullanıcı / Admin</b> tanımlamak için aşağıdaki formda <b>Kullanıcı adı ve şifre</b> yazdıktan sonra <b>Kaydet</b> butonuna tıklayın.</li>
                </ul>
            </div>
        </div>
    </div>
    <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab Text="Kullanıcılar"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="RadPageView1">
            <div class="panel">
                <div class="row">
                    <div class="large-4">
                        <div class="row">
                            <div class="large-3 columns">
                                <h5>Kullanıcı adı</h5>
                            </div>
                            <div class="large-3 columns">
                                <asp:TextBox ID="txtKullaniciAdi" runat="server" MaxLength="60" CssClass="textBoxUzun" />
                            </div>
                            <div class="large-1 columns">
                                <h5>Şifre</h5>
                            </div>
                            <div class="large-3 columns">
                                <asp:TextBox ID="txtSifre" runat="server" MaxLength="60" CssClass="textBoxUzun" TextMode="Password" />
                            </div>
                            <div class="large-2 columns">
                                <asp:Button ID="btnKaydet" Text="Kaydet" runat="server" OnClick="btnKaydet_Click"
                                    CssClass="tiny success button" />
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="large-12">
                        <telerik:RadGrid ID="grvKullanicilar" runat="server" AutoGenerateColumns="false" Skin="MetroTouch" ClientSettings-Selecting-AllowRowSelect="true" AllowPaging="True" PageSize="10" AllowSorting="True" OnNeedDataSource="grvKullanicilar_OnNeedDataSource">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="No." ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Kullanıcı Adı" DataField="KullaniciAdi" SortExpression="KullaniciAdi" />
                                    <telerik:GridTemplateColumn HeaderText="Şifre">
                                        <ItemTemplate>
                                            <%# Eval("Finex").xToBooleanDefault()?"":"<a href='#!' onclick='SifreGoster(\"dvSifre"+Eval("Id")+"\")'>Şifreyi göster</a>"  %>
                                            <div id='dvSifre<%#Eval("Id") %>' data-alert class="alert-box secondary tiny hide large-2">
                                                <%# Eval("Sifre") %>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Aktif/Pasif" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkStatuDegistir" runat="server" OnClick="lnkStatuDegistir_Click"
                                                CommandArgument='<%# Eval("Id") %>' Visible='<%# !Eval("Finex").xToBooleanDefault() %>'>
                                                <%# Eval("Statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Kullanıcıyı aktif yapmak için tıklayın.' />" : "<img src='/admin/css/img/dolu.png' title='Kullanıcıyı pasif yapmak için tıklayın.' />"%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Düzenle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDuzenle" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkDuzenle_OnClick" Visible='<%# Eval("Finex").xToBooleanDefault() == false %>'>
                                                <img src='css/img/duzenle.png' title='Bloğu düzenlemek için tıklayın.' width="16px" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_Click" CommandArgument='<%# Eval("Id") %>'
                                                Visible='<%# Eval("Finex").xToBooleanDefault() == false %>'>
                                            <img src="css/img/sil.png" />
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                                ConfirmText="Bu kullanıcıyı silmek istediğinizden emin misiniz ?">
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
