<%@ Page Language="C#" Culture="uk-UA" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="QWA.Pages.Home" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card-list">
        <asp:Repeater ID="AnnouncementsRepeater" runat="server">
            <ItemTemplate>
                <div class="card">
                    <a href='<%# GetRouteUrl("PostDetailsRoute", new { id = Eval("PostID") }) %>'>
                        <img src='<%# Eval("ImageURL") %>' class="card-img-top" alt="<%# Eval("Title") %>">
                        <div class="card-body">
                            <p class="card-text title"><%# Eval("Title") %></p>
                            <p class="card-text price"><%# Eval("Price", "{0:N0}") %> грн.</p>
                            <p class="card-text date"><%# Eval("CreatedDate", "{0:dd MMM yyyy HH:mm}") %></p>
                        </div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="pagination-controls">
        <asp:HyperLink ID="lnkPrevious" runat="server" Text="Previous" CssClass="text-link" />
        <asp:Label ID="lblPageInfo" runat="server" />
        <asp:HyperLink ID="lnkNext" runat="server" Text="Next" CssClass="text-link" />
    </div>


</asp:Content>
