<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QWA.Pages.Login" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Login</h2>
    <asp:TextBox ID="tbUsername" runat="server" placeholder="Username"></asp:TextBox>
    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
    <asp:Button ID="btLoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
    <asp:Label ID="MessageLabel" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
