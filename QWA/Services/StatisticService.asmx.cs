using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Services;

namespace QWA.Services
{
    public class StatisticService : System.Web.Services.WebService
    {
        [WebMethod]
        public int GetTotalPosts()
        {
            int totalPosts = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Posts";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                totalPosts = (int)cmd.ExecuteScalar();
            }

            return totalPosts;
        }

        [WebMethod]
        public int GetTotalPostsByUser(int userID)
        {
            int totalPosts = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Posts WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);
                conn.Open();
                totalPosts = (int)cmd.ExecuteScalar();
            }

            return totalPosts;
        }

        [WebMethod]
        public int GetTotalUsers()
        {
            int totalUsers = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["QWAdb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                totalUsers = (int)cmd.ExecuteScalar();
            }

            return totalUsers;
        }
    }
}
