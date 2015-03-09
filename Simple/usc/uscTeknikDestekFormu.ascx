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
                <div class="small-11 columns">
                    <asp:TextBox ID="txtAdSoyad" runat="server" MaxLength="100" />
                </div>
                <div class="small-1 columns">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAdSoyad" ValidationGroup="tdForm"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Company") %></label>
                </div>
                <div class="small-11 columns">
                    <asp:TextBox ID="txtFirma" runat="server" MaxLength="100" />
                </div>
                <div class="small-1 columns">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtFirma" ValidationGroup="tdForm"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Address") %></label>
                </div>
                <div class="small-11 columns">
                    <asp:TextBox ID="txtAdres" runat="server" MaxLength="250" TextMode="MultiLine" />
                </div>
                <div class="small-1 columns">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAdres" ValidationGroup="tdForm"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Phone") %></label>
                </div>
                <div class="small-11 columns">
                    <asp:TextBox ID="txtTel" runat="server" MaxLength="100" />
                </div>
                <div class="small-1 columns">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtTel" ValidationGroup="tdForm"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Fax") %></label>
                </div>
                <div class="small-11 columns">
                    <asp:TextBox ID="txtFax" runat="server" MaxLength="100" />
                </div>
                <div class="small-1 columns">
                    &nbsp;
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.EMail") %></label>
                </div>
                <div class="small-11 columns">
                    <asp:TextBox ID="txtEposta" runat="server" MaxLength="100" />
                </div>
                <div class="small-1 columns">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtEposta" ValidationGroup="tdForm"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <label for="right-label" class="right inline"><%= bllDiziler.DiziGetir("SupportForm.Label.Feedback") %></label>
                </div>
                <div class="small-11 columns">
                    <asp:TextBox ID="txtMesaj" runat="server" MaxLength="500" TextMode="MultiLine" />
                </div>
                <div class="small-1 columns">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMesaj" ValidationGroup="tdForm"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="small-15 columns">
                    <asp:Button ID="btnGonder" runat="server" OnClick="btnGonder_Click" CssClass="button small right" ValidationGroup="tdForm"></asp:Button>
                </div>
            </div>
            <div class="row">
                <div class="small-1 columns">
                    &nbsp;
                </div>
                <div class="small-14 columns">
                    <asp:Label ID="lblUyari" runat="server" Text="Lütfen tüm alanları doldurunuz." />
                </div>
            </div>
        </div>
    </div>
</form>
