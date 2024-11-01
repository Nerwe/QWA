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
                int totalPages = GetTotalPages();

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

                LoadAnnouncements(pageIndex);
            }
        }

        private int GetTotalPages()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string countQuery = "SELECT COUNT(*) FROM Posts";
                SqlCommand countCmd = new SqlCommand(countQuery, conn);
                conn.Open();

                int totalRecords = (int)countCmd.ExecuteScalar();
                return (int)Math.Ceiling((double)totalRecords / PageSize);
            }
        }

        private void LoadAnnouncements(int pageIndex)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = 
                    @"WITH PaginatedPosts AS (
                    SELECT p.PostID, p.Title, p.Price, p.ImageURL, p.CreatedDate, 
                           c.CategoryName, 
                           ROW_NUMBER() OVER(ORDER BY p.CreatedDate DESC) AS RowNum
                    FROM Posts p
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                )
                SELECT * FROM PaginatedPosts
                WHERE RowNum BETWEEN @StartRow AND @EndRow";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StartRow", (pageIndex - 1) * PageSize + 1);
                cmd.Parameters.AddWithValue("@EndRow", pageIndex * PageSize);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                AnnouncementsRepeater.DataSource = dt;
                AnnouncementsRepeater.DataBind();

                cmd.CommandText = "SELECT COUNT(*) FROM Posts";
                conn.Open();
                int totalAnnouncements = (int)cmd.ExecuteScalar();
                int totalPages = (int)Math.Ceiling((double)totalAnnouncements / PageSize);

                UpdatePaginationControls(pageIndex, totalPages);
            }
        }



        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            int currentPage = (int)ViewState["CurrentPage"];
            if (currentPage > 1)
            {
                currentPage--;
                ViewState["CurrentPage"] = currentPage;
                LoadAnnouncements(currentPage);
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
                LoadAnnouncements(currentPage);
            }
        }

        private void UpdatePaginationControls(int pageIndex, int totalPages)
        {
            lblPageInfo.Text = $"Page {pageIndex} of {totalPages}";

            lnkPrevious.Visible = pageIndex > 1;
            lnkPrevious.NavigateUrl = GetRouteUrl("HomePaged", new { page = pageIndex - 1 });

            lnkNext.Visible = pageIndex < totalPages;
            lnkNext.NavigateUrl = GetRouteUrl("HomePaged", new { page = pageIndex + 1 });
        }
    }
}