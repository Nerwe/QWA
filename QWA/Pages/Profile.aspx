<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="QWA.Pages.Profile" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile">
        <h2>User Profile</h2>
        <asp:Label ID="UsernameLabel" runat="server"></asp:Label>
        <asp:Label ID="EmailLabel" runat="server"></asp:Label>
        <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" />
        <h2>My Advertisements</h2>
        <div class="card-list">
            <asp:Repeater ID="AnnouncementsRepeater" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <img src='<%# Eval("ImageURL") %>' class="card-img-top" alt="Объявление">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Title") %></h5>
                            <p class="card-text">Category: <%# Eval("CategoryName") %></p>
                            <p class="card-text">Price: <%# Eval("Price") %></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
