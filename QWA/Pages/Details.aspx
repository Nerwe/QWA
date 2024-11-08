<%@ Page Language="C#" Culture="uk-UA" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="QWA.Pages.Details" MasterPageFile="~/MasterPage.Master" Title="Post details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 text-light">
        <div class="row">
            <div class="col-md-6">
                <div class="image-container mb-4">
                    <asp:Image ID="ImageControl" runat="server" CssClass="img-fluid rounded" Alt="Объявление" Style="max-height: 400px; object-fit: cover;" />
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="user-contact bg-dark border rounded p-3">
                    <h5>Contact Seller</h5>
                    <p class="user-email">
                        <strong>Email:</strong>
                        <asp:Label ID="UserEmailLabel" runat="server"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

        <div class="post-base-info mb-4 p-3 bg-dark border rounded">
            <p class="post-text">
                <strong>Publish:</strong>
                <asp:Label ID="CreatedDateLabel" runat="server"></asp:Label>
            </p>
            <h5 class="post-title">
                <asp:Label ID="TitleLabel" runat="server"></asp:Label>
            </h5>
            <p class="post-text">
                <strong>Price:</strong>
                <asp:Label ID="PriceLabel" runat="server"></asp:Label>
            </p>
        </div>

        <div class="post-description mb-4 p-3 bg-dark border rounded">
            <p class="post-text">
                <strong>Category:</strong>
                <asp:Label ID="CategoryLabel" runat="server"></asp:Label>
            </p>
            <h5>Description</h5>
            <p class="post-text" style="overflow-wrap: break-word;">
                <asp:Label ID="DescriptionLabel" runat="server"></asp:Label>
            </p>
        </div>

        <div class="user-info mb-4 p-3 bg-dark border rounded">
            <h5>User</h5>
            <h5 class="user-name">
                <asp:Label ID="UsernameLabel" runat="server"></asp:Label>
            </h5>
            <p class="user-reg-date">
                <strong>Registration Date:</strong>
                <asp:Label ID="UserRegDateLabel" runat="server"></asp:Label>
            </p>
        </div>

        <div class="comments-section mb-4">
            <h5 class="text-light">Comments</h5>
            <div class="comment-list mb-3">
                <asp:Repeater ID="rptComments" runat="server">
                    <ItemTemplate>
                        <div class="comment mb-2 p-3 bg-dark border rounded">
                            <div class="comment-header d-flex justify-content-between">
                                <strong class="comment-author text-white"><%# Eval("Author") %></strong>
                                <span class="comment-date text-muted" style="font-size: 0.875rem;"><%# Eval("Date", "{0:MMM dd, yyyy}") %></span>
                            </div>
                            <div class="comment-body mt-2 text-light" style="overflow-wrap: break-word;">
                                <%# Eval("Text") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <asp:PlaceHolder ID="phCommentForm" runat="server" Visible="false">
            <div class="comment-form mb-3">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control bg-dark shadow-none text-light" placeholder="Write your comment..."></asp:TextBox>
                <asp:Label ID="lblError" runat="server" Visible="false" CssClass="text-danger"></asp:Label>
                <asp:Button ID="btnSubmit" runat="server" Text="Post Comment" CssClass="btn btn-primary mt-2" OnClick="btnSubmit_Click" />
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phLoginPrompt" runat="server" Visible="false">
            <p class="text-danger">You need to be logged in to post a comment. <a href="/login" class="text-link">Login here</a>.</p>
        </asp:PlaceHolder>
    </div>


    <style>
        .bg-dark {
            background-color: #212529 !important;
        }

        .text-light {
            color: #f8f9fa !important;
        }

        .comment-header {
            border-bottom: 1px solid #343a40;
            padding-bottom: 5px;
            margin-bottom: 5px;
        }

        .comment-body {
            color: #e9ecef;
        }

        .comment {
            background-color: #343a40;
        }

        .text-link {
            color: #0d6efd;
            text-decoration: underline;
        }

        .form-control.bg-dark {
            background-color: #343a40;
            border-color: #495057;
            color: #f8f9fa;
        }
    </style>

</asp:Content>
