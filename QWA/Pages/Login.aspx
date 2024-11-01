<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QWA.Pages.Login" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="card bg-dark text-light mx-auto" style="max-width: 400px;">
            <div class="card-header text-center">
                <h2>Login</h2>
            </div>
            <div class="card-body">
                <asp:TextBox ID="tbUsername" runat="server" placeholder="Username" CssClass="form-control mb-3 bg-dark text-light border-secondary" />
                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control mb-3 bg-dark text-light border-secondary" />
                <asp:Button ID="btLoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" CssClass="btn btn-primary btn-block" />
                <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" CssClass="mt-2 d-block text-center"></asp:Label>
            </div>
            <div class="card-footer text-center">
                <span class="text">Don't have an account? </span>
                <a href="/register" class="text-light">Sign Up</a>
            </div>
        </div>
    </div>
</asp:Content>
