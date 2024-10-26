using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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

            int? userId = AuthenticateUser(username, password);

            if (userId.HasValue)
            {
                Session["UserID"] = userId.Value;
                Response.Redirect("/profile");
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
                string query = "SELECT UserID, PasswordHash FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader["PasswordHash"].ToString();
                            if (VerifyPassword(password, storedPasswordHash))
                            {
                                return Convert.ToInt32(reader["UserID"]);
                            }
                        }
                    }
                }
            }

            return null;
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            string[] parts = storedPassword.Split(':');
            if (parts.Length != 2) return false;

            string salt = parts[0];
            string hash = parts[1];

            string saltedPassword = salt + enteredPassword;

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                string enteredHash = Convert.ToBase64String(hashBytes);

                return hash == enteredHash;
            }
        }

        private string HashPassword(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            string saltedPassword = salt + password;
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                string hash = Convert.ToBase64String(hashBytes);

                return $"{salt}:{hash}";
            }
        }
    }
}
