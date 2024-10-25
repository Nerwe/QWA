<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adding.aspx.cs" Inherits="QWA.Pages.Adding" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
        <div class="form-header">
            <h2>Add new post</h2>
        </div>
        <div class="form-main">
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red"></asp:Label>
            <asp:TextBox ID="tbTitle" runat="server" placeholder="Title" CssClass="form-input"></asp:TextBox>
            <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" Rows="10" placeholder="Description" CssClass="form-input multi"></asp:TextBox>
            <asp:TextBox ID="tbPrice" runat="server" placeholder="Price" CssClass="form-input"></asp:TextBox>
            <asp:TextBox ID="tbImageURL" runat="server" placeholder="URL image" CssClass="form-input"></asp:TextBox>
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-select"></asp:DropDownList>
            <asp:Button ID="AddPostButton" runat="server" Text="Add" OnClick="AddPostButton_Click" CssClass="submit-button" />
        </div>
    </div>
</asp:Content>
