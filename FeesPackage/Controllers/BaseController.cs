using FeesPackage.Data_Access;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class BaseController : Controller
    {
        protected readonly FeesPackageEntities db = new FeesPackageEntities();
    }
}