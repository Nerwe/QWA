<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="QWA.Pages.Profile" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile">
        <h2>User Profile</h2>
        <asp:Label ID="UsernameLabel" runat="server"></asp:Label>
        <asp:Label ID="EmailLabel" runat="server"></asp:Label>
        <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" CssClass="logout-button"/>
        <h2>My Posts</h2>
        <div class="card-list">
            <asp:Repeater ID="AnnouncementsRepeater" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <a href='<%# GetRouteUrl("PostDetailsRoute", new { id = Eval("PostID") }) %>'>
                            <img src='<%# Eval("ImageURL") %>' class="card-img-top" alt="Объявление">
                            <div class="card-body">
                                <p class="card-text title"><%# Eval("Title") %></p>
                                <p class="card-text price"><%# Eval("Price", "{0:N2}") %> грн.</p>
                                <p class="card-text date"><%# Eval("CreatedDate", "{0:dd MMM yyyy HH:MM}") %></p>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
