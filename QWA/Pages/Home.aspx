<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="QWA.Pages.Home" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card-list">
        <asp:Repeater ID="AnnouncementsRepeater" runat="server">
            <ItemTemplate>
                <div class="card">
                    <img src='<%# Eval("ImageURL") %>' class="card-img-top" alt="Объявление">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("Title") %></h5>
                        <p class="card-text">Category: <%# Eval("CategoryName") %></p>
                        <p class="card-text">Price: <%# Eval("Price") %></p>
                        <a href='<%# GetRouteUrl("PostDetailsRoute", new { id = Eval("PostID") }) %>' class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
