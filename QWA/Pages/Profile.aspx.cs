using QWA.Services;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace QWA.Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        public int TotalPostsByUser { get; set; }
        private const int PageSize = 6;

        private bool IsUserAdmin()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT isAdmin FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                return result != null && (bool)result;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/login");
            }

            int userId = (int)Session["UserID"];
            StatisticService service = new StatisticService();
            TotalPostsByUser = service.GetTotalPostsByUser(userId);

            if (!IsPostBack)
            {
                LoadUserProfile(userId);

                int pageIndex = 1;
                if (Page.RouteData.Values["id"] == null || !int.TryParse(Page.RouteData.Values["id"].ToString(), out pageIndex) || pageIndex < 1)
                {
                    Response.Redirect("/profile/page/1");
                    return;
                }

                int totalPosts = service.GetTotalPostsByUser(userId);
                int totalPages = (int)Math.Ceiling((double)totalPosts / PageSize);

                if (totalPages == 0)
                {
                    return;
                }

                if (pageIndex > totalPages)
                {
                    Response.Redirect($"/profile/page/{totalPages}");
                    return;
                }

                LoadAnnouncements(userId, pageIndex);

                if (IsUserAdmin())
                {
                    DeleteAllPostsButton.Visible = true;
                    DeleteAllCommentsButton.Visible = true;
                }
            }
        }

        private void LoadUserProfile(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Username, Email FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UsernameLabel.Text = reader["Username"].ToString();
                            EmailLabel.Text = reader["Email"].ToString();
                        }
                        else
                        {
                            UsernameLabel.Text = "User not found.";
                            EmailLabel.Text = "";
                        }
                    }
                }
            }
        }

        private void LoadAnnouncements(int userId, int pageIndex)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    WITH PaginatedPosts AS (
                        SELECT PostID, Title, CategoryName, Price, ImageURL, CreatedDate,
                               ROW_NUMBER() OVER(ORDER BY CreatedDate DESC) AS RowNum
                        FROM Posts
                        INNER JOIN Categories ON Posts.CategoryID = Categories.CategoryID
                        WHERE Posts.UserID = @UserID
                    )
                    SELECT * FROM PaginatedPosts
                    WHERE RowNum BETWEEN @StartRow AND @EndRow";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@StartRow", (pageIndex - 1) * PageSize + 1);
                command.Parameters.AddWithValue("@EndRow", pageIndex * PageSize);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        AnnouncementsRepeater.DataSource = reader;
                        AnnouncementsRepeater.DataBind();
                    }
                    else
                    {
                        AnnouncementsRepeater.DataSource = null;
                        AnnouncementsRepeater.DataBind();
                    }
                }

                UpdatePaginationControls(userId, pageIndex);
            }
        }

        private void UpdatePaginationControls(int userId, int pageIndex)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string countQuery = "SELECT COUNT(*) FROM Posts WHERE UserID = @UserID";

                SqlCommand countCmd = new SqlCommand(countQuery, connection);
                countCmd.Parameters.AddWithValue("@UserID", userId);

                int totalPosts = (int)countCmd.ExecuteScalar();
                int totalPages = (int)Math.Ceiling((double)totalPosts / PageSize);

                lblPageInfo.Text = $"Page {pageIndex} of {totalPages}";

                lnkPrevious.Visible = pageIndex > 1;
                lnkNext.Visible = pageIndex < totalPages;

                if (pageIndex > 1)
                {
                    lnkPrevious.NavigateUrl = $"/profile/page/{pageIndex - 1}";
                }

                if (pageIndex < totalPages)
                {
                    lnkNext.NavigateUrl = $"/profile/page/{pageIndex + 1}";
                }
            }
        }

        private void DeleteAllPosts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Posts";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteAllComments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Comments";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeletePost(int postId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Posts WHERE PostID = @PostID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostID", postId);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void DeleteAllPostsButton_Click(object sender, EventArgs e)
        {
            DeleteAllPosts();
            int userId = (int)Session["UserID"];
            int pageIndex = 1;
            if (Page.RouteData.Values["id"] != null)
            {
                int.TryParse(Page.RouteData.Values["id"].ToString(), out pageIndex);
            }
            LoadAnnouncements(userId, pageIndex);
        }


        protected void DeleteAllCommentsButton_Click(object sender, EventArgs e)
        {
            DeleteAllComments();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            LinkButton deleteButton = (LinkButton)sender;
            int postId = Convert.ToInt32(deleteButton.CommandArgument);

            ViewState["PostIdToDelete"] = postId;

            RepeaterItem item = (RepeaterItem)deleteButton.NamingContainer;
            Panel confirmationPanel = (Panel)item.FindControl("ConfirmationPanel");
            confirmationPanel.Visible = true;
            deleteButton.Visible = false;
        }

        protected void ConfirmDeleteButton_Click(object sender, EventArgs e)
        {
            LinkButton confirmButton = (LinkButton)sender;
            int postId = Convert.ToInt32(confirmButton.CommandArgument);

            DeletePost(postId);

            int userId = (int)Session["UserID"];
            int pageIndex = 1;
            LoadAnnouncements(userId, pageIndex);
        }

        protected void CancelDeleteButton_Click(object sender, EventArgs e)
        {
            LinkButton cancelButton = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)cancelButton.NamingContainer;

            Panel confirmationPanel = (Panel)item.FindControl("ConfirmationPanel");
            LinkButton deleteButton = (LinkButton)item.FindControl("DeleteButton");
            confirmationPanel.Visible = false;
            deleteButton.Visible = true;
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/login");
        }
    }
}
