<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QWA.Pages.Login" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
        <div class="form-header">
            <h2>Login</h2>
        </div>
        <div class="form-main">
            <asp:TextBox ID="tbUsername" runat="server" placeholder="Username" CssClass="form-input"></asp:TextBox>
            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-input"></asp:TextBox>
            <asp:Button ID="btLoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" CssClass="submit-button" />
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-footer">
            <span class="text">Don't have an account? </span>
            <a href="/register" class="text-link">
              Sign Up
            </a>
        </div>
    </div>
</asp:Content>
