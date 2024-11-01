<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QWA.Pages.Register" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="card bg-dark text-light mx-auto" style="max-width: 400px;">
            <div class="card-header text-center">
                <h2>Register</h2>
            </div>
            <div class="card-body">
                <asp:TextBox ID="tbUsername" runat="server" placeholder="Username" CssClass="form-control mb-3 bg-dark text-light border-secondary" />
                <asp:TextBox ID="tbEmail" runat="server" placeholder="Email" CssClass="form-control mb-3 bg-dark text-light border-secondary" />
                <div class="mb-3">
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control mb-3 bg-dark text-light border-secondary" />
                    <asp:TextBox ID="tbPasswordConfirm" runat="server" TextMode="Password" placeholder="Confirm Password" CssClass="form-control mb-3 bg-dark text-light border-secondary" />
                </div>
                <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick="RegisterButton_Click" CssClass="btn btn-primary btn-block" />
                <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" CssClass="mt-2 d-block text-center"></asp:Label>
            </div>
            <div class="card-footer text-center">
                <span class="text">Already have an account? </span>
                <a href="/login" class="text-light">Log In</a>
            </div>
        </div>
    </div>
</asp:Content>
