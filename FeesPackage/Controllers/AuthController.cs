using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View("NotFound", "_LayoutMin");
        }
    }
}