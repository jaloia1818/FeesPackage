using FeesPackage.Data_Access;
using FeesPackage.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ClientInfoController : BaseController
    {
        // GET: ClientInfo/Create
        public ActionResult Create()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Client = new tblClient(),

                Attys = db.tblAttorneys.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials,
                    Name = c.Atty_Initials + " - " + c.Atty_Name
                })
                .ToList(),

                Counties = db.tblCounties.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.County,
                    Name = c.County_Desc
                })
                .ToList()
            };

            return PartialView("~/Views/ClientInfo/_Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Save(ClientInfoModel mdl)
        {
            tblClient model = mdl.Client;
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
            ClientInfoModel model = new ClientInfoModel
            {
                Client = db.tblClients.SingleOrDefault(x => x.id == id),

                Attys = db.tblAttorneys.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials,
                    Name = c.Atty_Initials + " - " + c.Atty_Name
                })
                .ToList(),

                Counties = db.tblCounties.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.County,
                    Name = c.County_Desc
                })
                .ToList()
            };

            return PartialView("~/Views/ClientInfo/_Edit.cshtml", model);
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