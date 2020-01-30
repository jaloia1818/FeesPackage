using FeesPackage.Data_Access;
using System;
using System.Net;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class BaseController : Controller
    {
        protected readonly FeesPackageEntities db = new FeesPackageEntities();

        protected HttpStatusCodeResult HandleException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.InnerException.InnerException.Message.Replace("\r\n", " "));
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
        }
    }
}