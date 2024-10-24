<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QWA.Pages.Register" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
        <div class="form-header">
            <h2>Register</h2>
        </div>
        <div class="form-main">
            <asp:TextBox ID="tbUsername" runat="server" placeholder="Username" CssClass="form-input"></asp:TextBox>
            <asp:TextBox ID="tbEmail" runat="server" placeholder="Email" CssClass="form-input"></asp:TextBox>
            <div class="password-container">
                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-input"></asp:TextBox>
                <asp:TextBox ID="tbPasswordConfirm" runat="server" TextMode="Password" placeholder="Confirm Password" CssClass="form-input"></asp:TextBox>
            </div>
            <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick="RegisterButton_Click" CssClass="submit-button" />
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-footer">
            <span class="text">Already have an account? </span>
            <a href="/login" class="text-link">Log In
            </a>
        </div>
    </div>
</asp:Content>
