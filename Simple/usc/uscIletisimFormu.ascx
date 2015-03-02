<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscIletisimFormu.ascx.cs" Inherits="Ws.usc.uscIletisimFormu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<form id="frmIletisimFormu" runat="server">
    <div class="panel tiny">
        <%= BLL.bllDiziler.DiziGetir("ContactForm.Header") %>
        <hr />
        <div class="row collapse">
            <div class="large-3 columns">
                <%= BLL.bllDiziler.DiziGetir("ContactForm.Label.Name") %>
            </div>
            <div class="large-4 columns">
                <asp:TextBox ID="txtAd" runat="server" MaxLength="60">
                </asp:TextBox>
            </div>
            <div class="large-1 columns">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtAd"></asp:RequiredFieldValidator>
            </div>
            <div class="large-3 columns">
                <%= BLL.bllDiziler.DiziGetir("ContactForm.Label.Lastname") %>
            </div>
            <div class="large-4 columns">
                <asp:TextBox ID="txtSoyad" runat="server" MaxLength="60">
                </asp:TextBox>
            </div>
            <div class="large-1 columns">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtSoyad"></asp:RequiredFieldValidator>
            </div>
            <div class="large-3 columns">
                <%= BLL.bllDiziler.DiziGetir("ContactForm.Label.Email") %>
            </div>
            <div class="large-4 columns">
                <asp:TextBox ID="txtEposta" runat="server" MaxLength="60">
                </asp:TextBox>
            </div>
            <div class="large-1 columns">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtEposta"></asp:RequiredFieldValidator>
            </div>
            <div class="large-3 columns">
                <%= BLL.bllDiziler.DiziGetir("ContactForm.Label.Subject") %>
            </div>
            <div class="large-4 columns">
                <asp:TextBox ID="txtKonu" runat="server" MaxLength="60">
                </asp:TextBox>
            </div>
            <div class="large-1 columns">
            </div>
            <div class="large-3 columns">
                <%= BLL.bllDiziler.DiziGetir("ContactForm.Label.Message") %>
            </div>
            <div class="large-13 columns">
                <asp:TextBox ID="txtMesaj" runat="server" Width="100%" MaxLength="60" TextMode="MultiLine"
                    Height="150px">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtMesaj"></asp:RequiredFieldValidator>
            </div>
            <div class="large-16 columns text-right">
                <asp:Button ID="btnGonder" runat="server" OnClick="btnGonder_Click" CssClass="button small"></asp:Button>
            </div>
            <div class="large-16 columns text-center">
                <asp:Label ID="lblUyari" runat="server" />
            </div>
        </div>
    </div>
</form>

