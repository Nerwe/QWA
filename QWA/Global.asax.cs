using System;
using System.Web.Routing;

namespace QWA
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "RegisterRoute",
                "register",
                "~/Pages/Register.aspx"
            );

            routes.MapPageRoute(
                "LoginRoute",
                "login",
                "~/Pages/Login.aspx"
            );

            routes.MapPageRoute(
                "ProfileRoute",
                "profile",
                "~/Pages/Profile.aspx"
            );

            routes.MapPageRoute(
                "AddingRoute",
                "adding",
                "~/Pages/Adding.aspx"
            );

            routes.MapPageRoute(
                "EditingRoute",
                "edit/{id}",
                "~/Pages/Editing.aspx"
            );

            routes.MapPageRoute(
                "PostDetailsRoute",
                "post/{id}",
                "~/Pages/Details.aspx"
            );

            routes.MapPageRoute(
                "HomePaged",
                "page/{page}",
                "~/Pages/Home.aspx"
            );

            routes.MapPageRoute(
                "SearchRoute",
                "search/{query}/page/{page}",
                "~/Pages/Home.aspx"
            );

            routes.MapPageRoute(
                "NotFoundRoute",
                "{*url}",
                "~/Pages/Home.aspx"
            );
        }
    }
}