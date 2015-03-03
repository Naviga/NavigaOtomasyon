<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscTeknikDestekFormu.ascx.cs" Inherits="Ws.usc.uscTeknikDestekFormu" %>
<%@ Import Namespace="BLL" %>

<form id="frmTeknikDestek" runat="server">

    <div class="row">
        <div class="small-16 columns">
            <div class="row">
                <div class="small-16 column">
                    <%= bllDiziler.DiziGetir("SupportForm.Header") %>
                </div>

            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.NameLastname") %></label>
                </div>
                <div class="small-12 columns">
                    <asp:TextBox ID="txtAdSoyad" runat="server" MaxLength="100" />
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Company") %></label>
                </div>
                <div class="small-12 columns">
                    <asp:TextBox ID="txtFirma" runat="server" MaxLength="100" />
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Address") %></label>
                </div>
                <div class="small-12 columns">
                    <asp:TextBox ID="txtAdres" runat="server" MaxLength="250" TextMode="MultiLine" />
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Phone") %></label>
                </div>
                <div class="small-12 columns">
                    <asp:TextBox ID="txtTel" runat="server" MaxLength="100" />
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Fax") %></label>
                </div>
                <div class="small-12 columns">
                    <asp:TextBox ID="txtFax" runat="server" MaxLength="100" />
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.EMail") %></label>
                </div>
                <div class="small-12 columns">
                    <asp:TextBox ID="txtEposta" runat="server" MaxLength="100" />
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Feedback") %></label>
                </div>
                <div class="small-12 columns">
                    <asp:TextBox ID="txtMesaj" runat="server" MaxLength="500" TextMode="MultiLine" />
                </div>
            </div>
            <div class="row">
                <div class="small-1 columns right">
                    <asp:Button ID="btnGonder" runat="server" OnClick="btnGonder_Click" CssClass="button small"></asp:Button>
                </div>
            </div>
            <div class="row">
                <div class="small-1 columns">
                    &nbsp;
                </div>
                <div class="small-1 columns">
                    <asp:Label ID="lblUyari" runat="server" />
                </div>
            </div>
        </div>
    </div>
</form>
