<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="QWA.Pages.Profile" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Profile</h2>
    <asp:Label ID="UsernameLabel" runat="server"></asp:Label>
    <asp:Label ID="EmailLabel" runat="server"></asp:Label>
    <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" />
</asp:Content>
