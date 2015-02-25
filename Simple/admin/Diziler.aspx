<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="Diziler.aspx.cs" Inherits="Ws.admin.Diziler" %>

<%@ Import Namespace="Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var yeniKayit = false;

        function DuzenlemeModuAc(a) {

            if ($(a).attr("rel") == "[new]") {
                yeniKayit = true;
            }
            else {
                yeniKayit = false;
            }

            $find("mpe").show();

            var editor = $find("<%=edtDiziDeger.ClientID%>");

            editor.set_html($(a).attr("content"));

            $("#btnDiziKaydet").click(function () {
                YeniDiziDegeriKaydet($(a).attr("diziid"), a);
            });
        }

        function YeniDiziDegeriKaydet(id, a) {

            var editor = $find("<%=edtDiziDeger.ClientID%>");

            var deger = editor.get_html();

            var data = '{"id":"' + id + '","deger":"' + deger + '"}';

            if (yeniKayit) {
                var req = AjaxPost("/services/general.asmx/YeniDiziKaydet", data);

                req.success(function (data) {

                    $find("mpe").hide();

                    a.innerHTML = data.d;
                });
            } else {
                var req = AjaxPost("/services/general.asmx/DiziGuncelle", data);

                req.success(function (data) {

                    $find("mpe").hide();

                    a.innerHTML = data.d;
                });
            }

            return false;
        }

        var aHr;
        var id;

        function VarsayilanDuzenlemeModuAc(e) {

            $find("mpe").show();

            var editor = $find("<%=edtDiziDeger.ClientID%>");

            editor.set_html($(e).attr("content"));

            aHr = e;
            id = $(e).attr("diziid");

            $("#hdnID").val(id);
        }

        function ValueChanged(sender, eventArgs) {
            var ajaxMgr = $find("<%= RadAjaxManager1.ClientID %>");

            ajaxMgr.ajaxRequest(sender.get_id());

        }
    </script>
    <style>
        .ajax__html_editor_extender_texteditor
        {
            background-color: white;
            padding: .2em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgrvDiziler">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvDiziler" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtArama">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvDiziler" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDiziKaydet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgrvDiziler" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="pnlDiziDuzenle" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <%--<telerik:AjaxUpdatedControl ControlID="txtArama" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="MetroTouch">
        <Tabs>
            <telerik:RadTab Text="Yazılar">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="RadPageView1">
            <div class="panel">
                <div class="row">
                    <div class="large-4 columns">
                        <div class="row">
                            <div class="large-2 columns">
                                <h5>Ara</h5>
                            </div>
                            <div class="large-10 columns">
                                <telerik:RadTextBox ID="txtArama" runat="server" AutoPostBack="true" OnTextChanged="txtArama_TextChanged" Skin="MetroTouch" ShowButton="true">
                                    <ClientEvents OnButtonClick="ValueChanged"></ClientEvents>
                                </telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="large-4 columns">
                        <div class="large-2 columns">
                            <h5>Ekle</h5>
                        </div>
                        <div class="large-7 columns">
                            <asp:TextBox ID="txtDiziKodu" runat="server" />
                        </div>
                        <div class="large-3 columns">
                            <asp:Button ID="btnDiziKoduEkle" Text="Ekle" runat="server" OnClick="btnDiziKoduEkle_OnClick"
                                CssClass="tiny success button" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">

                    <div class="large-12">
                        <telerik:RadGrid ID="rgrvDiziler" runat="server" Skin="MetroTouch" OnNeedDataSource="rgrvDiziler_NeedDataSource" AllowPaging="True" PageSize="10" AllowSorting="True">
                            <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" AutoGenerateColumns="false">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Kodu" HeaderText="Kod" DataType="System.String" ItemStyle-Width="250px" SortExpression="Kodu"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Değer" ItemStyle-Width="250px">
                                        <ItemTemplate>
                                            <a href='#!' onclick='VarsayilanDuzenlemeModuAc(this)' class='aVarsayilanDuzModAc' style="display: block" diziid='<%# Eval("Id") %>' content='<%# Eval("VarsayilanDegeri") %>'>
                                                <%# Eval("VarsayilanDegeri").ToString().xToRemoveHTMLTags().xLeft(50) %> <%# Eval("VarsayilanDegeri").ToString().Length>50 ? "[...]":"" %>
                                                <%# Eval("VarsayilanDegeri").ToString().xBosMu() == true ? "[Ekle]" : "" %> <%# Eval("VarsayilanDegeri").ToString().Length>50 ? "[...]":"" %>
                                            </a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Sil" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSil" runat="server" OnClick="lnkSil_OnClick" CommandArgument='<%# Eval("Id") %>'>
                                                                    <img src="/admin/css/img/sil.png" width="15px" height="15px" />
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkSil"
                                                ConfirmText="Dizi kodunu silmek istediğinizden emin misiniz ?">
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



    <asp:Panel ID="pnlDiziDuzenle" runat="server" CssClass="modal large-8" Style="display: none">
        <asp:HiddenField runat="server" ID="hdnID" ClientIDMode="Static" />
        <table style="width: 100%" cellpadding="2" cellspacing="0">
            <tr>
                <td class="modalHeader" valign="middle">
                    <asp:Panel ID="pnlDrag" runat="server">
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>Dizi Düzenle
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnClose" runat="server" Text="X" CssClass="button btnclose tiny"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="info">Alt satıra geçmek için <b>Shift + Enter</b> kullanınız.
                </td>
            </tr>
            <tr>
                <td align="center">

                    <telerik:RadEditor DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.aspx" ID="edtDiziDeger" runat="server" Width="100%" Height="350"
                        Skin="MetroTouch" AllowScripts="False" ToolsFile="~/admin/toolsfile/Basic.xml">
                        <Content>
                        </Content>
                        <ImageManager DeletePaths="~/yukleme/resim" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/resim"
                            ViewPaths="~/yukleme/resim" />
                        <DocumentManager DeletePaths="~/yukleme/" MaxUploadFileSize="100000000" UploadPaths="~/yukleme/"
                            ViewPaths="~/yukleme/" SearchPatterns="*.jpg,*.png" />
                        <CssFiles>
                            <telerik:EditorCssFile Value="~/admin/css/editor.css" />
                        </CssFiles>
                    </telerik:RadEditor>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button Text="Kaydet" runat="server" ID="btnDiziKaydet" OnClick="btnDiziKaydet_OnClick" CssClass="button" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnDumm" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="mpeDiziDuzenle" BehaviorID="mpe" runat="server" BackgroundCssClass="modalBack"
            PopupControlID="pnlDiziDuzenle" TargetControlID="btnDumm" CancelControlID="btnClose" PopupDragHandleControlID="pnlDrag">
        </asp:ModalPopupExtender>
    </asp:Panel>
</asp:Content>
