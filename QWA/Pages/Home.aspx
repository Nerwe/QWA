<%@ Page Language="C#" Culture="uk-UA" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="QWA.Pages.Home" MasterPageFile="~/MasterPage.Master" Title="QWA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row mb-4">
            <div class="col-md-8 mx-auto">
                <div class="input-group">
                    <asp:TextBox ID="SearchTextBox" runat="server" CssClass="form-control bg-dark text-white shadow-none border-secondary" Placeholder="Search posts..." />
                    <div class="input-group-append">
                        <asp:Button ID="SearchButton" runat="server" Text="🔍" CssClass="btn btn-secondary" OnClick="SearchButton_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <asp:Repeater ID="AnnouncementsRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100 bg-dark text-white">
                            <a href='<%# GetRouteUrl("PostDetailsRoute", new { id = Eval("PostID") }) %>' class="text-decoration-none">
                                <img src='<%# Eval("ImageURL") %>' class="card-img-top" alt="<%# Eval("Title") %>" style="height: 200px; object-fit: cover;">
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title flex-grow-1" style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis; font-size: 1.25rem;"><%# Eval("Title") %></h5>
                                    <p class="card-text price" style="font-size: 1.5rem; font-weight: bold;"><%# Eval("Price", "{0:N0}") %> грн.</p>
                                    <p class="card-text date" style="font-size: 0.875rem; color: #adb5bd;"><%# Eval("CreatedDate", "{0:dd MMM yyyy HH:mm}") %></p>
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

        .input-group .form-control.bg-dark {
            background-color: #212529;
            color: #ffffff;
        }
    </style>
</asp:Content>
