using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class SpecialTasksController : BaseController
    {
        // GET: SpecialTasks
        public ActionResult Index()
        {
            return View();
        }
    }
}