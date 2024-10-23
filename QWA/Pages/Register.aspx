<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QWA.Pages.Register" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Register</h2>
    <asp:TextBox ID="tbUsername" runat="server" placeholder="Username"></asp:TextBox>
    <asp:TextBox ID="tbEmail" runat="server" placeholder="Email"></asp:TextBox>
    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
    <asp:Button ID="btRegisterButton" runat="server" Text="Register" OnClick="RegisterButton_Click" />
    <asp:Label ID="lbMessageLabel" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
