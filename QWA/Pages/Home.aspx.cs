using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QWA.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAnnouncements();
            }
        }

        private void LoadAnnouncements()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.PostID, p.Title, p.Price, p.ImageURL, p.Content, c.CategoryName
                    FROM Posts p
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                AnnouncementsRepeater.DataSource = dt;
                AnnouncementsRepeater.DataBind();
            }
        }
    }
}