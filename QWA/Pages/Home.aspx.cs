using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QWA.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private const int PageSize = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageIndex = 1;
                string query = Page.RouteData.Values["query"]?.ToString();
                int totalPages = GetTotalPages(query);

                if (Page.RouteData.Values["page"] != null && int.TryParse(Page.RouteData.Values["page"].ToString(), out int page))
                {
                    if (page > 0 && page <= totalPages)
                    {
                        pageIndex = page;
                    }
                    else
                    {
                        Response.Redirect(GetRouteUrl("HomePaged", new { page = 1 }));
                        return;
                    }
                }

                LoadAnnouncements(pageIndex, query);
            }
        }

        private int GetTotalPages(string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string countQuery = "SELECT COUNT(*) FROM Posts";

                if (!string.IsNullOrEmpty(query))
                {
                    countQuery += " WHERE Title LIKE @Query";
                }

                SqlCommand countCmd = new SqlCommand(countQuery, conn);

                if (!string.IsNullOrEmpty(query))
                {
                    countCmd.Parameters.AddWithValue("@Query", "%" + query + "%");
                }

                conn.Open();
                int totalRecords = (int)countCmd.ExecuteScalar();
                return (int)Math.Ceiling((double)totalRecords / PageSize);
            }
        }

        private void LoadAnnouncements(int pageIndex, string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string postQuery =
                    @"WITH PaginatedPosts AS (
                    SELECT p.PostID, p.Title, p.Price, p.ImageURL, p.CreatedDate, 
                           c.CategoryName, 
                           ROW_NUMBER() OVER(ORDER BY p.CreatedDate DESC) AS RowNum
                    FROM Posts p
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                    ";

                if (!string.IsNullOrEmpty(query))
                {
                    postQuery += " WHERE p.Title LIKE @Query ";
                }

                postQuery += @"
                )
                SELECT * FROM PaginatedPosts
                WHERE RowNum BETWEEN @StartRow AND @EndRow";

                SqlCommand cmd = new SqlCommand(postQuery, conn);
                cmd.Parameters.AddWithValue("@StartRow", (pageIndex - 1) * PageSize + 1);
                cmd.Parameters.AddWithValue("@EndRow", pageIndex * PageSize);

                if (!string.IsNullOrEmpty(query))
                {
                    cmd.Parameters.AddWithValue("@Query", "%" + query + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                AnnouncementsRepeater.DataSource = dt;
                AnnouncementsRepeater.DataBind();

                string countQuery = "SELECT COUNT(*) FROM Posts";
                if (!string.IsNullOrEmpty(query))
                {
                    countQuery += " WHERE Title LIKE @Query";
                }

                SqlCommand countCmd = new SqlCommand(countQuery, conn);
                if (!string.IsNullOrEmpty(query))
                {
                    countCmd.Parameters.AddWithValue("@Query", "%" + query + "%");
                }

                conn.Open();
                int totalAnnouncements = (int)countCmd.ExecuteScalar();
                int totalPages = (int)Math.Ceiling((double)totalAnnouncements / PageSize);

                UpdatePaginationControls(pageIndex, totalPages, query);
            }
        }

        private void UpdatePaginationControls(int pageIndex, int totalPages, string query)
        {
            lblPageInfo.Text = $"Page {pageIndex} of {totalPages}";

            if (!string.IsNullOrEmpty(query))
            {
                lnkPrevious.NavigateUrl = GetRouteUrl("SearchRoute", new { query, page = pageIndex - 1 });
                lnkNext.NavigateUrl = GetRouteUrl("SearchRoute", new { query, page = pageIndex + 1 });
            }
            else
            {
                lnkPrevious.NavigateUrl = GetRouteUrl("HomePaged", new { page = pageIndex - 1 });
                lnkNext.NavigateUrl = GetRouteUrl("HomePaged", new { page = pageIndex + 1 });
            }

            lnkPrevious.Visible = pageIndex > 1;
            lnkNext.Visible = pageIndex < totalPages;
        }


        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            int currentPage = (int)ViewState["CurrentPage"];
            if (currentPage > 1)
            {
                currentPage--;
                ViewState["CurrentPage"] = currentPage;
                LoadAnnouncements(currentPage, (string)ViewState["SearchQuery"]);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int currentPage = (int)ViewState["CurrentPage"];
            int totalPages = (int)ViewState["TotalPages"];
            if (currentPage < totalPages)
            {
                currentPage++;
                ViewState["CurrentPage"] = currentPage;
                LoadAnnouncements(currentPage, (string)ViewState["SearchQuery"]);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string query = SearchTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(query))
            {
                Response.Redirect(GetRouteUrl("SearchRoute", new { query, page = 1 }));
            }
            else
            {
                Response.Redirect(GetRouteUrl("HomePaged", new { page = 1 }));
            }
        }
    }
}
