using FeesPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class DailyPaymentsController : BaseController
    {
        // GET: DailyPayments
        public ActionResult Index()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Payments = db.tblPayments.Where(x => x.Posted_Indicator == false)
                                         .OrderByDescending(x => x.Payment_Date).ToList(),

                Attys = db.tblAttorneys
                .ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials.ToString(),
                    Name = c.Atty_Name.ToString()
                })
                .ToList()
            };

            return View(model);
        }
    }
}