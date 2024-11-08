using QWA.Services;
using System;

namespace QWA
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public int TotalPosts { get; set; }
        public int TotalUsers { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            StatisticService service = new StatisticService();

            TotalPosts = service.GetTotalPosts();
            TotalUsers = service.GetTotalUsers();
        }
    }
}