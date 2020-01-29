﻿using FeesPackage.Data_Access;
using FeesPackage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ClientInfoController : BaseController
    {
        [HttpPost]
        public ActionResult Edit(int id)
        {
            tblClient client = db.tblClients.SingleOrDefault(x => x.id == id);
            return PartialView("~/Views/ClientInfo/_Edit.cshtml", client);
        }

        // GET: ClientInfo
        public ActionResult Index()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Clients = db.tblClients.OrderByDescending(x => x.id).ToList(),

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