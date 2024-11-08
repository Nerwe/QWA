<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editing.aspx.cs" Inherits="QWA.Pages.Editing" MasterPageFile="~/MasterPage.Master" Title="Edit post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="card bg-dark text-light mx-auto" style="max-width: 500px;">
            <div class="card-header text-center">
                <h2>Edit Post</h2>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="mb-3 d-block text-center"></asp:Label>
                <asp:TextBox ID="tbTitle" runat="server" placeholder="Title" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" Rows="10" placeholder="Content" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:TextBox ID="tbPrice" runat="server" placeholder="Price" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:TextBox ID="tbImage" runat="server" placeholder="Image URL" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary" />
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control mb-3 bg-dark text-light shadow-none border-secondary"></asp:DropDownList>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="SaveButton_Click" CssClass="btn btn-primary btn-block" />
            </div>
        </div>
    </div>
</asp:Content>
