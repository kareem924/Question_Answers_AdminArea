using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace GawayzAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // look if any security information exists for this request
            if (HttpContext.Current.User == null) return;

            // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

            // see if the authentication is done using FormsAuthentication
            if (!(HttpContext.Current.User.Identity is FormsIdentity)) return;

            // Get the roles stored for this request from the ticket
            // get the identity of the user
            FormsIdentity fid = (FormsIdentity)HttpContext.Current.User.Identity;

            // get the forms authetication ticket of the user
            FormsAuthenticationTicket ticket = fid.Ticket;

            // get the roles stored as UserData into the ticket
            //Get the stored user-data, in this case, Page Client ID and UserName
            string userData = ticket.UserData;
            string[] roles = userData.Split(',');

            // create generic principal and assign it to the current request
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(fid, roles);
        }
    }
}
