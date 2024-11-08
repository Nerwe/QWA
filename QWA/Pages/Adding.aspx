<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adding.aspx.cs" Inherits="QWA.Pages.Adding" MasterPageFile="~/MasterPage.Master" Title="Add post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="card bg-dark text-light mx-auto" style="max-width: 500px;">
            <div class="card-header text-center">
                <h2>Add New Post</h2>
            </div>
            <div class="card-body">
                <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" CssClass="mb-3 d-block text-center"></asp:Label>
                <asp:TextBox ID="tbTitle" runat="server" placeholder="Title" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" Rows="10" placeholder="Description" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:TextBox ID="tbPrice" runat="server" placeholder="Price" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:TextBox ID="tbImageURL" runat="server" placeholder="Image URL" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary"></asp:DropDownList>
                <asp:Button ID="AddPostButton" runat="server" Text="Add" OnClick="AddPostButton_Click" CssClass="btn btn-primary btn-block" />
            </div>
        </div>
    </div>
</asp:Content>
