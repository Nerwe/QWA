<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="QWA.Pages.Details" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="details">
        <div class="container">
            <div class="image-container">
                <asp:Image ID="ImageControl" runat="server" CssClass="main-image" Alt="Объявление" />
            </div>
            <div class="post-base-info">
                <p class="post-text">Publish:
                    <asp:Label ID="CreatedDateLabel" runat="server"></asp:Label></p>
                <h5 class="post-title">
                    <asp:Label ID="TitleLabel" runat="server"></asp:Label></h5>
                <p class="post-text">
                    <asp:Label ID="PriceLabel" runat="server"></asp:Label></p>
            </div>
        </div>
        <div class="post-description">
            <p class="post-text">Category:
                <asp:Label ID="CategoryLabel" runat="server"></asp:Label></p>
            <h5>DESCRIPTION</h5>
            <p class="post-text">
                <asp:Label ID="DescriptionLabel" runat="server"></asp:Label></p>
        </div>
        <div class="user-info">
            <h5>USER</h5>
            <h5 class="user-name">
                <asp:Label ID="UsernameLabel" runat="server"></asp:Label></h5>
            <p class="user-reg-date">
                <asp:Label ID="UserRegDateLabel" runat="server"></asp:Label></p>
        </div>
        <div class="user-contact">
            <h5>CONTACT SELLER</h5>
            <p class="user-email">Email:
                <asp:Label ID="UserEmailLabel" runat="server"></asp:Label></p>
        </div>
        <div class="comments-section">
            <h5>COMMENTS</h5>
            <div class="comment-list">
                <asp:Repeater ID="rptComments" runat="server">
                    <ItemTemplate>
                        <div class="comment">
                            <div class="comment-author">
                                <strong><%# Eval("Author") %></strong>
                                <span class="comment-date"><%# Eval("Date", "{0:MMM dd, yyyy}") %></span>
                            </div>
                            <div class="comment-text">
                                <%# Eval("Text") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="comment-form">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="10" CssClass="comment-textarea" placeholder="Write your comment..."></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" Text="Post Comment" CssClass="submit-button" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
