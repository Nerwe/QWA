using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace QWA.Pages
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.RouteData.Values["id"] != null)
                {
                    int postId = int.Parse(Page.RouteData.Values["id"] as string);
                    LoadPostDetails(postId);
                    LoadComments(postId);
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }

        private void LoadPostDetails(int postId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT Posts.Title, Posts.Content AS Description, Posts.Price, Posts.ImageURL, Posts.CreatedDate, 
                                Categories.CategoryName, Users.Username, Users.Email AS UserEmail, Users.CreatedDate AS UserRegDate
                                FROM Posts
                                INNER JOIN Categories ON Posts.CategoryID = Categories.CategoryID
                                INNER JOIN Users ON Posts.UserID = Users.UserID
                                WHERE Posts.PostID = @PostID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostID", postId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TitleLabel.Text = reader["Title"].ToString();
                            DescriptionLabel.Text = reader["Description"].ToString();
                            PriceLabel.Text = $"{Math.Round(Convert.ToDecimal(reader["Price"])).ToString("N0")} грн.";
                            CategoryLabel.Text = reader["CategoryName"].ToString();
                            ImageControl.ImageUrl = reader["ImageURL"].ToString();
                            CreatedDateLabel.Text = Convert.ToDateTime(reader["CreatedDate"]).ToString("MMM dd, yyyy");

                            UsernameLabel.Text = reader["Username"].ToString();
                            UserEmailLabel.Text = reader["UserEmail"].ToString();
                            UserRegDateLabel.Text = Convert.ToDateTime(reader["UserRegDate"]).ToString("MMM dd, yyyy");
                        }
                    }
                }
            }
        }

        private void LoadComments(int postId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CommentText AS Text, Users.Username AS Author, Comments.CreatedDate AS Date " +
                               "FROM Comments " +
                               "INNER JOIN Users ON Comments.UserID = Users.UserID " +
                               "WHERE Comments.PostID = @PostID ORDER BY Comments.CreatedDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostID", postId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rptComments.DataSource = reader;
                        rptComments.DataBind();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] != null && Page.RouteData.Values["id"] != null)
            {
                int postId = int.Parse(Page.RouteData.Values["id"] as string);
                int userId = (int)Session["UserID"];
                string commentText = txtComment.Text;

                if (!string.IsNullOrWhiteSpace(commentText))
                {
                    AddComment(postId, userId, commentText);
                    txtComment.Text = string.Empty;
                    LoadComments(postId); 
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }

        private void AddComment(int postId, int userId, string commentText)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Comments (PostID, UserID, CommentText, CreatedDate) " +
                               "VALUES (@PostID, @UserID, @CommentText, GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostID", postId);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@CommentText", commentText);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
