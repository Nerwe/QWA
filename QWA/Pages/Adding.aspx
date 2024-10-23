<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adding.aspx.cs" Inherits="QWA.Pages.Adding" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Добавить объявление</h2>
    <asp:Label ID="MessageLabel" runat="server" ForeColor="Red"></asp:Label>
    <asp:TextBox ID="tbTitle" runat="server" placeholder="Название объявления"></asp:TextBox>
    <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" Rows="5" placeholder="Описание"></asp:TextBox>
    <asp:TextBox ID="tbPrice" runat="server" placeholder="Цена"></asp:TextBox>
    <asp:TextBox ID="tbImageURL" runat="server" placeholder="URL изображения"></asp:TextBox>

    <asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList>

    <asp:Button ID="AddPostButton" runat="server" Text="Добавить" OnClick="AddPostButton_Click" />
</asp:Content>
