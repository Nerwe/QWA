using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace QWA.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text.Trim();
            string email = tbEmail.Text.Trim();
            string password = tbPassword.Text.Trim();
            string passwordConfirm = tbPasswordConfirm.Text.Trim();

            string connectionString = WebConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            if (IsValidInput(username, email, password, passwordConfirm))
            {
                string passwordHash = HashPassword(password);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                        checkCommand.Parameters.AddWithValue("@Username", username);
                        checkCommand.Parameters.AddWithValue("@Email", email);

                        int existingUserCount = (int)checkCommand.ExecuteScalar();
                        if (existingUserCount > 0)
                        {
                            MessageLabel.Text = "User with this username or email already exists.";
                            return;
                        }

                        string query = "INSERT INTO Users (Username, Email, PasswordHash, CreatedDate) VALUES (@Username, @Email, @PasswordHash, @CreatedDate)";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        int rowAffected = command.ExecuteNonQuery();

                        if (rowAffected > 0) Response.Redirect("/login");
                        else MessageLabel.Text = "Error in registration process";
                    }
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = "Error: " + ex.Message;
                }
            }
            else if (string.IsNullOrEmpty(MessageLabel.Text))
            {
                MessageLabel.Text = "Incorrect user data";
            }
        }

        private bool IsValidInput(string username, string email, string password, string passwordConfirm)
        {
            if (string.IsNullOrEmpty(username) || username.Length < 3 || username.Length > 50)
            {
                MessageLabel.Text = "Username must be between 3 and 50 characters.";
                return false;
            }

            if (!Regex.IsMatch(username, "^[a-zA-Z0-9]+$"))
            {
                MessageLabel.Text = "Username must contain only letters and numbers.";
                return false;
            }

            if (!Regex.IsMatch(email, "^[a-z0-9]+@([a-z0-9]+\\.)+[a-z]{2,6}$", RegexOptions.IgnoreCase))
            {
                MessageLabel.Text = "Invalid email format.";
                return false;
            }

            if (password != passwordConfirm)
            {
                MessageLabel.Text = "Password doesn't match confirmation.";
                return false;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                MessageLabel.Text = "Password must be at least 8 characters.";
                return false;
            }

            if (!Regex.IsMatch(password, @"\d") || !Regex.IsMatch(password, @"[a-zA-Z]"))
            {
                MessageLabel.Text = "Password must contain letters and numbers.";
                return false;
            }

            return true;
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
