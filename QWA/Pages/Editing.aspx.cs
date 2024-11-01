using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace QWA.Pages
{
    public partial class Editing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/login");
            }

            if (!IsPostBack)
            {
                LoadCategories();

                if (Page.RouteData.Values["id"] != null && int.TryParse(Page.RouteData.Values["id"].ToString(), out int postId) && IsUserPostOwner(postId, (int)Session["UserID"]))
                {
                    LoadPostData(postId);
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }

        private void LoadCategories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CategoryID, CategoryName FROM Categories";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    ddlCategory.DataSource = reader;
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryID";
                    ddlCategory.DataBind();
                }
            }
        }

        private void LoadPostData(int postId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Title, Content, CategoryID, Price, ImageURL FROM Posts WHERE PostID = @PostID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostID", postId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbTitle.Text = reader["Title"].ToString();
                            tbContent.Text = reader["Content"].ToString();
                            ddlCategory.SelectedValue = reader["CategoryID"].ToString();
                            tbPrice.Text = Math.Round(Convert.ToDecimal(reader["Price"])).ToString("N0");
                            tbImage.Text = reader["ImageURL"].ToString();
                        }
                        else
                        {
                            lblMessage.Text = "Post not found.";
                        }
                    }
                }
            }
        }

        private bool IsUserPostOwner(int postId, int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserID FROM Posts WHERE PostID = @PostID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostID", postId);
                    object result = command.ExecuteScalar();
                    return result != null && (int)result == userId;
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string title = tbTitle.Text.Trim();
            string content = tbContent.Text.Trim();
            string priceStr = tbPrice.Text;
            string imageURL = tbImage.Text;

            if (!IsValidInput(title, content, priceStr, imageURL))
            {
                return;
            }

            int postId = int.Parse(Page.RouteData.Values["id"] as string);
            UpdatePostInDatabase(postId, title, content, priceStr, imageURL);
        }

        private bool IsValidInput(string title, string content, string price, string imageURL)
        {
            if (!Regex.IsMatch(title, @"^[a-zA-Zа-яА-Я0-9\s.,!?:;()'-]+$"))
            {
                lblMessage.Text = "Title can only contain letters, digits, and certain punctuation.";
                return false;
            }

            if (!Regex.IsMatch(content, @"^[a-zA-Zа-яА-Я0-9\s.,!?:;()'-]+$"))
            {
                lblMessage.Text = "Content can only contain letters, digits, and certain punctuation.";
                return false;
            }

            if (!decimal.TryParse(price, out _))
            {
                lblMessage.Text = "Incorrect price format.";
                return false;
            }

            if (!Uri.IsWellFormedUriString(imageURL, UriKind.Absolute))
            {
                lblMessage.Text = "Invalid image URL.";
                return false;
            }

            return true;
        }

        private void UpdatePostInDatabase(int postId, string title, string content, string priceStr, string imageURL)
        {
            decimal price = decimal.Parse(priceStr);
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE Posts SET Title = @Title, Content = @Content, CategoryID = @CategoryID, 
                                Price = @Price, ImageURL = @ImageURL WHERE PostID = @PostID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Content", content);
                    command.Parameters.AddWithValue("@CategoryID", ddlCategory.SelectedValue);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@ImageURL", imageURL);
                    command.Parameters.AddWithValue("@PostID", postId);

                    int rowsAffected = command.ExecuteNonQuery();
                    Response.Redirect("/profile");
                }
            }
        }
    }
}
