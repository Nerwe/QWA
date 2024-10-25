using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace QWA.Pages
{
    public partial class Editing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();

                if (Page.RouteData.Values["id"] != null)
                {
                    int postId = int.Parse(Page.RouteData.Values["id"] as string);
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

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                int postId = int.Parse(Page.RouteData.Values["id"] as string);
                string query = @"UPDATE Posts SET Title = @Title, Content = @Content, CategoryID = @CategoryID, 
                                Price = @Price, ImageURL = @ImageURL WHERE PostID = @PostID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", tbTitle.Text);
                    command.Parameters.AddWithValue("@Content", tbContent.Text);
                    command.Parameters.AddWithValue("@CategoryID", ddlCategory.SelectedValue);
                    command.Parameters.AddWithValue("@Price", tbPrice.Text);
                    command.Parameters.AddWithValue("@ImageURL", tbImage.Text);
                    command.Parameters.AddWithValue("@PostID", postId);

                    int rowsAffected = command.ExecuteNonQuery();
                    lblMessage.Text = rowsAffected > 0 ? "Post updated successfully." : "Update failed.";
                }
            }
        }
    }
}
