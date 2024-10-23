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
                Response.Redirect("login");
            }

            int userId = (int)Session["UserID"];
            LoadUserProfile(userId);
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

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login");
        }
    }
}
