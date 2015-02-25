<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="CarouselDuzenle.aspx.cs" Inherits="Ws.admin.CarouselDuzenle" MaintainScrollPositionOnPostback="true" %>

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
                    <telerik:AjaxUpdatedControl ControlID="rgrvCarousel"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="1" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab>
                <TabTemplate>
                    <a href='<%= Request.Url.AbsoluteUri.Contains("?iframe") ? ("Carousel.aspx?iframe&id="+ Request.QueryString["PageId"]) : "Carousel.aspx"%>'><< Geri
                    </a>
                </TabTemplate>
            </telerik:RadTab>
            <telerik:RadTab Text="Carousel Resimleri"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="1">
        <telerik:RadPageView runat="server" ID="RadPageView2">
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView1">
            <div class="panel">
                <table border="0" cellpadding="4" cellspacing="0">

                    <tr>
                        <td class="formBaslik">Adı</td>
                        <td>
                            <asp:TextBox ID="txtAdi" runat="server" MaxLength="75" Skin="MetroTouch"></asp:TextBox>
                        </td>
                        <td class="formBaslik">Gösterilecek Resim Sayısı</td>
                        <td>
                            <asp:TextBox ID="txtTekrarSayisi" runat="server" MaxLength="2" Skin="MetroTouch"></asp:TextBox>
                        </td>
                        <td class="formBaslik">Gösterim Süresi(sn.)</td>
                        <td>
                            <asp:TextBox ID="txtGosterimSuresi" runat="server" MaxLength="2" Skin="MetroTouch"></asp:TextBox>
                        </td>
                        <td align="right">
                            <telerik:RadButton ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_OnClick" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                                <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td class="formBaslik">Yeni resim ekle ->
                        </td>
                        <td>
                            <telerik:RadAsyncUpload ID="rauResim" runat="server" Width="100px" MaxFileInputsCount="1"
                                MaxFileSize="5000000" MultipleFileSelection="Automatic" Skin="MetroTouch" HideFileInput="True">
                            </telerik:RadAsyncUpload>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnResimYukle" runat="server" Text="Yükle" OnClick="btnResimYukle_click" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White">
                                <Icon PrimaryIconUrl="css/img/save_icon_16_beyaz.png" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <hr />
                <telerik:RadGrid ID="rgrvCarousel" runat="server" Skin="MetroTouch">
                    <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" AutoGenerateColumns="false">
                        <PagerStyle Mode="NumericPages" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="No." ItemStyle-Width="15px">
                                <ItemTemplate>
                                    <%# Container.ItemIndex + 1 %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Resim" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <img src='<%# Eval("Orta") %>' width="80%" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Başlık" ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAciklama" runat="server" OnClick="lnkAciklama_Click" CommandArgument='<%#Eval("Id") %>'>
                                    <%# Eval("Baslik").ToString().xBosMu() == false ? Eval("Baslik").ToString() : "[+ Ekle]" %>    
                                    </asp:LinkButton>
                                    <table id="tblDuzenle" runat="server" visible="false" width="100%">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtAciklama" runat="server" Text='<%# Eval("Baslik") %>' EmptyMessage="Açıklama" Skin="MetroTouch" TextMode="MultiLine" MaxLength="250" Width="100%" Resize="Vertical" Height="100%"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadButton ID="btnAciklamaKaydet" OnClick="btnAciklamaKaydet_Click" runat="server" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White" CommandArgument='<%# Eval("Id") %>'>
                                                    <Icon PrimaryIconUrl="css/img/check_icon_16_beyaz.png" />
                                                </telerik:RadButton>
                                                <telerik:RadButton ID="btnAciklamaIptal" OnClick="btnAciklamaIptal_Click" runat="server" Skin="MetroTouch">
                                                    <Icon PrimaryIconUrl="css/img/sil_16.png" />
                                                </telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="URL" ItemStyle-Width="30%" Resizable="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUrl" runat="server" OnClick="lnkUrl_Click" CommandArgument='<%#Eval("Id") %>'>
                                    <%# Eval("NavUrl").ToString() == "#!" ? "[+ Ekle]" : Eval("NavUrl").ToString() %>    
                                    <%# Eval("NavUrl").ToString().xBosMu() == true ? "[+ Ekle]" : "" %>    
                                    </asp:LinkButton>
                                    <table id="tblUrlDuzenle" runat="server" visible="false" width="100%">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtURL" runat="server" Text='<%# Eval("NavURL") %>' EmptyMessage="ör: http://www.google.com" Skin="MetroTouch" MaxLength="250" Width="100%"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadButton ID="btnURLKaydet" OnClick="btnURLKaydet_Click" runat="server" Skin="MetroTouch" CssClass="backColorBlue" ForeColor="White" CommandArgument='<%# Eval("Id") %>'>
                                                    <Icon PrimaryIconUrl="css/img/check_icon_16_beyaz.png" />
                                                </telerik:RadButton>
                                                <telerik:RadButton ID="btnURLIptal" OnClick="btnURLIptal_Click" runat="server" Skin="MetroTouch">
                                                    <Icon PrimaryIconUrl="css/img/sil_16.png" />
                                                </telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Foto" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFotoLink" runat="server" AutoPostBack="True" OnCheckedChanged="chkFotoLink_OnCheckedChanged" ResId='<%# Eval("Id") %>' Checked='<%# Eval("FotoLink").xToBooleanDefault() %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Video" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkVideoLink" runat="server" AutoPostBack="True" OnCheckedChanged="chkVideoLink_OnCheckedChanged" ResId='<%# Eval("Id") %>' Checked='<%# Eval("VideoLink").xToBooleanDefault() %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Sira" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtSiraNo" runat="server" Text='<%# Eval("Sira") %>' MaxLength="3" AutoPostBack="true" OnTextChanged="txtSiraNo_TextChanged" dzID='<%# Eval("Id") %>'></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Aktif/Pasif" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkStatuDegistir" runat="server" OnClick="lnkStatuDegistir_Click"
                                        CommandArgument='<%# Eval("Id") %>'>
                                        <%# Eval("Statu").xToBooleanDefault() == false ? "<img src='/admin/css/img/bos.png' title='Resmi görünür yapmak için tıklayın.' />" : "<img src='/admin/css/img/dolu.png' title='Galeriyi görünmez yapmak için tıklayın.' />"%>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_Click" CommandArgument='<%# Eval("Id") %>'>
                                            <img src="css/img/sil.png" />
                                    </asp:LinkButton>
                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                        ConfirmText="Bu resmi silmek istediğinizden emin misiniz ?">
                                    </asp:ConfirmButtonExtender>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <uc1:uscUyari ID="uscUyari1" runat="server" />
</asp:Content>
