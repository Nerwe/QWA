<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editing.aspx.cs" Inherits="QWA.Pages.Editing" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
        <div class="form-header">
            <h2>Edit post</h2>
        </div>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <asp:TextBox ID="tbTitle" runat="server" placeholder="Title" CssClass="form-input"></asp:TextBox>
        <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" Rows="10" placeholder="Content" CssClass="form-input multi"></asp:TextBox>
        <asp:TextBox ID="tbPrice" runat="server" placeholder="Price" CssClass="form-input"></asp:TextBox>
        <asp:TextBox ID="tbImage" runat="server" placeholder="URL image" CssClass="form-input"/>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select"></asp:DropDownList>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="SaveButton_Click" CssClass="submit-button" />
    </div>
</asp:Content>
