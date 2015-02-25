<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscUyari.ascx.cs" Inherits="Ws.usc.uscUyari" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Panel ID="pnlUyari" runat="server" CssClass="modal" Style="display: none">
    <table style="width: 100%" cellpadding="2" cellspacing="0">
        <tr>
            <td class="modalHeader" valign="middle">
                <asp:Panel ID="pnlDrag" runat="server">
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="lblUyariBaslik" ClientIDMode="Static" runat="server" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnClose" runat="server" Text="X" CssClass="tiny button secondary"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblUyari" ClientIDMode="Static" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnTamam" Text="Tamam" runat="server" CssClass="tiny button" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnDumm" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeUyari" runat="server" BackgroundCssClass="modalBack"
        PopupControlID="pnlUyari" TargetControlID="btnDumm" CancelControlID="btnClose"
        OkControlID="btnTamam" PopupDragHandleControlID="pnlDrag">
    </asp:ModalPopupExtender>
</asp:Panel>
