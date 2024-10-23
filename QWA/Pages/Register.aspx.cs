using System;
using System.Data.SqlClient;
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
            string passwordHash = HashPassword(tbPassword.Text.Trim());

            string connectionString = WebConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            if (IsValidInput(username, email, passwordHash))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        int rowAffected = command.ExecuteNonQuery();

                        if (rowAffected > 0) Response.Write("Register completed");
                        else Response.Write("Error in register proccess");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
            else Response.Write("Incorrent user data");
        }

        private bool IsValidInput(string username, string email, string passwordHash)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(passwordHash);
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}