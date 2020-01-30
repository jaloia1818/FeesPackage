using FeesPackage.Data_Access;
using FeesPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ClientInfoController : BaseController
    {
        // GET: ClientInfo/Create
        public ActionResult Create()
        {
            tblClient client = new tblClient();
            return PartialView("~/Views/ClientInfo/_Edit.cshtml", client);
        }

        [HttpPost]
        public ActionResult Save(tblClient model)
        {
            try
            {
                if (model.id == 0)
                {   // insert
                    // Insert into table
                    db.tblClients.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblClient record = db.tblClients.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Client_Name = model.Client_Name;
                    record.Employer_Name = model.Employer_Name;
                    record.Escrow = model.Escrow;
                    record.Handling_Atty = model.Handling_Atty;
                    record.Handling = model.Handling;
                    record.Credit_Atty = model.Credit_Atty;
                    record.Credit = model.Credit;
                    record.Is_County = model.Is_County;
                    record.County = model.County;
                    record.Accident_Desc = model.Accident_Desc;
                    record.Selection_Control = model.Selection_Control;

                    // Update record
                    db.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            // refresh screen
            return Redirect(Request.UrlReferrer.ToString());
        }

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