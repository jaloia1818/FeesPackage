using FeesPackage.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ClientInfoController : Controller
    {
        private FeesPackageEntities db = new FeesPackageEntities();

        // GET: ClientInfo
        public ActionResult Index()
        {
            List<tblClient> model = db.tblClients.ToList();

            return View(model);
        }
    }
}