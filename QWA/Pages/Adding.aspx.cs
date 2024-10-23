using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace QWA.Pages
{
    public partial class Adding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCategories.DataSource = dt;
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataBind();
            }
        }

        protected void AddPostButton_Click(object sender, EventArgs e)
        {
            string title = tbTitle.Text;
            string content = tbContent.Text;
            decimal price;

            if (!decimal.TryParse(tbPrice.Text, out price))
            {
                MessageLabel.Text = "Введите корректную цену.";
                return;
            }

            string imageURL = tbImageURL.Text;
            int categoryID = Convert.ToInt32(ddlCategories.SelectedValue);

            AddPostToDatabase(title, content, price, imageURL, categoryID);
        }

        private void AddPostToDatabase(string title, string content, decimal price, string imageURL, int categoryId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Posts (Title, Content, Price, ImageURL, CategoryID, UserID, CreatedDate)
                    VALUES (@Title, @Content, @Price, @ImageURL, @CategoryID, @UserID, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Content", content);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@ImageURL", imageURL);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageLabel.Text = "Объявление добавлено!";
                ClearFields();
            }
        }

        private void ClearFields()
        {
            tbTitle.Text = string.Empty;
            tbContent.Text = string.Empty;
            tbPrice.Text = string.Empty;
            tbImageURL.Text = string.Empty;
            ddlCategories.SelectedIndex = 0;
        }

    }
}