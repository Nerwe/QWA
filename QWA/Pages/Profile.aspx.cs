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
                LoadAnnouncements(userId);
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

        private void LoadAnnouncements(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PostID, Title, CategoryName, Price, ImageURL, CreatedDate FROM Posts " +
                               "INNER JOIN Categories ON Posts.CategoryID = Categories.CategoryID " +
                               "WHERE Posts.UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            AnnouncementsRepeater.DataSource = reader;
                            AnnouncementsRepeater.DataBind();
                        }
                    }
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
            LoadAnnouncements(userId);
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
