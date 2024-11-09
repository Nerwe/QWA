<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="QWA.Pages.Profile" MasterPageFile="~/MasterPage.Master" Title="Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="d-flex justify-content-center mb-4">
            <div class="card bg-dark text-light" style="max-width: 400px; width: 100%;">
                <div class="card-header text-center">
                    <h4 class="mb-0">Profile Information</h4>
                </div>
                <div class="card-body text-center">
                    <asp:Label ID="UsernameLabel" runat="server" CssClass="text-light display-4 d-block mb-2"></asp:Label>
                    <asp:Label ID="EmailLabel" runat="server" CssClass="text-light h5 d-block mb-3"></asp:Label>
                    <div class="text-light h5 mb-3">
                        <i class="fas fa-file-alt"></i><%= TotalPostsByUser %> My Posts
                   
                    </div>
                    <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" CssClass="btn btn-danger btn-block" />
                    <asp:Button ID="DeleteAllPostsButton" runat="server" Text="Delete All Posts" OnClick="DeleteAllPostsButton_Click" CssClass="btn btn-danger btn-block mb-3" Visible="false" />
                    <asp:Button ID="DeleteAllCommentsButton" runat="server" Text="Delete All Comments" OnClick="DeleteAllCommentsButton_Click" CssClass="btn btn-danger btn-block mb-3" Visible="false" />
                </div>
            </div>
        </div>

        <h2 class="text-light mt-4 text-center">My Posts</h2>
        <div class="row">
            <asp:Repeater ID="AnnouncementsRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100 bg-dark text-white">
                            <a href='<%# GetRouteUrl("PostDetailsRoute", new { id = Eval("PostID") }) %>' class="text-decoration-none">
                                <img src='<%# Eval("ImageURL") %>' class="card-img-top" alt="<%# Eval("Title") %>" style="height: 200px; object-fit: cover;">
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title flex-grow-1" style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis; font-size: 1.25rem;"><%# Eval("Title") %></h5>
                                    <p class="card-text price" style="font-size: 1.5rem; font-weight: bold; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;"><%# Eval("Price", "{0:N0}") %> грн.</p>
                                    <p class="card-text date" style="font-size: 0.875rem; color: #adb5bd; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;"><%# Eval("CreatedDate", "{0:dd MMM yyyy HH:mm}") %></p>
                                    <a href='<%# GetRouteUrl("EditingRoute", new { id = Eval("PostID") }) %>' class="btn btn-warning btn-sm mt-2">🖊️ Edit</a>

                                    <asp:LinkButton ID="DeleteButton" runat="server"
                                        CommandArgument='<%# Eval("PostID") %>'
                                        OnClick="DeleteButton_Click"
                                        CssClass="btn btn-danger btn-sm mt-2">❌ Delete</asp:LinkButton>

                                    <asp:Panel ID="ConfirmationPanel" runat="server" CssClass="mt-2" Visible="false">
                                        <asp:LinkButton ID="ConfirmDeleteButton" runat="server"
                                            CommandArgument='<%# Eval("PostID") %>'
                                            OnClick="ConfirmDeleteButton_Click"
                                            CssClass="btn btn-danger btn-sm btn-block">Yes</asp:LinkButton>
                                        <asp:LinkButton ID="CancelDeleteButton" runat="server"
                                            CommandArgument='<%# Eval("PostID") %>'
                                            OnClick="CancelDeleteButton_Click"
                                            CssClass="btn btn-secondary btn-sm btn-block">No</asp:LinkButton>
                                    </asp:Panel>
                                </div>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="d-flex justify-content-between align-items-center mt-4">
            <asp:HyperLink ID="lnkPrevious" runat="server" Text="Previous" CssClass="btn btn-secondary" />
            <asp:Label ID="lblPageInfo" runat="server" CssClass="text-light" />
            <asp:HyperLink ID="lnkNext" runat="server" Text="Next" CssClass="btn btn-secondary" />
        </div>
    </div>

    <style>
        .card {
            transition: transform 0.3s, box-shadow 0.3s;
        }

            .card:hover {
                transform: scale(1.05);
                box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
            }

        .card-body {
            background-color: #212529;
        }

        a {
            color: #ffffff;
        }

            a:hover,
            a:focus {
                color: #ffffff;
                text-decoration: none;
            }
    </style>
</asp:Content>
