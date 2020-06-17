using FeesPackage.Data_Access;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace FeesPackage
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        protected readonly FeesPackageEntities db = new FeesPackageEntities();
        public string roles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized;

            // Get loggedin user information
            if (httpContext.Session["LoggedInUser"] == null)
            {
                httpContext.Session["LoggedInUser"] = db.Users.SingleOrDefault(x => x.Username == httpContext.Request.LogonUserIdentity.Name && x.IsActive == true);
            }

            User usr = (User)HttpContext.Current.Session["LoggedInUser"];
            if (usr == null)
            {
                isAuthorized = false;
            }
            else
            {
                isAuthorized = roles.Contains(usr.RoleId);
            }

            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Auth", action = "NotFound" }));
        }
    }
}
