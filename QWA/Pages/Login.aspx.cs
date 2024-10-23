using System;
using System.Configuration;
using System.Data.SqlClient;

namespace QWA.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            string hashedPassword = HashPassword(password);
            int? userId = AuthenticateUser(username, hashedPassword);

            if (userId.HasValue)
            {
                Session["UserID"] = userId.Value;
                Response.Redirect("profile");
            }
            else
            {
                MessageLabel.Text = "Invalid username or password";
            }
        }

        private int? AuthenticateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserID FROM Users WHERE Username = @Username AND PasswordHash = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return null;
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
