using System;
using System.Configuration;
using System.Data.SqlClient;

namespace QWA.Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/login");
            }

            int userId = (int)Session["UserID"];

            LoadUserProfile(userId);
            LoadAnnouncements(userId);
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

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/login");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
