using FeesPackage.Data_Access;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    [AuthorizeRole(roles = "R/O, OPS, ADMIN")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            User usr = (User)Session["LoggedInUser"];
            if (usr.RoleId == "R/O")
                return Redirect("/reports");
            else
                return Redirect("/checklist");
        }
    }
}
