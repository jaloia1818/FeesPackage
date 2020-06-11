using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FeesPackage
{
    public class AuthorizeAppRole : AuthorizeAttribute
    {
        public string roles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized = true;

            //List<string> roles = new List<string> { "ADMIN", "CREM_USER", "SA" };

            if (HttpContext.Current.Session["LoggedInUser"] != null)
            {
                //UserInfo usr = (UserInfo)HttpContext.Current.Session["LoggedInUser"];
                //isAuthorized = roles.Contains(usr.AppRoleId);
            }

            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["LoggedInUser"] == null)
                base.HandleUnauthorizedRequest(filterContext);
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Auth", action = "NotFound" }));
        }
    }
}
