﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace QWA.Pages
{
    public partial class Adding : System.Web.UI.Page
    {
        private bool IsUserAdmin()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT isAdmin FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                return result != null && (bool)result;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/login");
            }

            if (!IsPostBack)
            {
                LoadCategories();

                bool isAdmin = IsUserAdmin();
                tbPostCopies.Visible = isAdmin;
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
            string title = tbTitle.Text.Trim();
            string content = tbContent.Text.Trim();
            decimal price;
            string imageURL = tbImageURL.Text.Trim();
            int categoryID = Convert.ToInt32(ddlCategories.SelectedValue);
            int copyCount = 1;

            if (IsUserAdmin() && int.TryParse(tbPostCopies.Text, out int copies))
            {
                copyCount = copies > 0 ? copies : 1;
            }

            if (!IsValidInput(title, content, tbPrice.Text, imageURL))
            {
                return;
            }

            if (!decimal.TryParse(tbPrice.Text, out price))
            {
                MessageLabel.Text = "Incorrect price.";
                return;
            }

            for (int i = 0; i < copyCount; i++)
            {
                AddPostToDatabase(title, content, price, imageURL, categoryID);
            }

            Response.Redirect("/profile");
        }


        private bool IsValidInput(string title, string content, string price, string imageURL)
        {
            if (!Regex.IsMatch(title, @"^[a-zA-Zа-яА-Я0-9\s.,!?:;()'-]+$"))
            {
                MessageLabel.Text = "Title can only contain letters, digits, and certain punctuation.";
                return false;
            }

            if (!Regex.IsMatch(content, @"^[a-zA-Zа-яА-Я0-9\s.,!?:;()'-]+$"))
            {
                MessageLabel.Text = "Description can only contain letters, digits, and certain punctuation.";
                return false;
            }

            if (!decimal.TryParse(price, out _))
            {
                MessageLabel.Text = "Incorrect price format.";
                return false;
            }

            if (!Uri.IsWellFormedUriString(imageURL, UriKind.Absolute))
            {
                MessageLabel.Text = "Invalid image URL.";
                return false;
            }

            return true;
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
