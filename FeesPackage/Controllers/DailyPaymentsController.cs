using FeesPackage.Data_Access;
using FeesPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class DailyPaymentsController : BaseController
    {
        [HttpPost]
        public HttpStatusCodeResult Index(tblPayment model, FormCollection coll)
        {
            model.Payment_Date = ToDate(coll["Payment_Date"]);
            model.Period_From = ToDate(coll["Period_From"]);
            model.Period_To = ToDate(coll["Period_To"]);
            model.Input_Date = ToDate(coll["Input_Date"]);

            try
            {
                if (model.id == 0)
                {   // insert
                    // Insert into table
                    db.tblPayments.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblPayment record = db.tblPayments.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Claim_Number = model.Claim_Number;
                    record.Payment_Date = model.Payment_Date;
                    record.Period_From = model.Period_From;
                    record.Period_To = model.Period_To;
                    record.Amount = model.Amount;
                    record.Input_Date = model.Input_Date;
                    record.Inputter_Name = model.Inputter_Name;
                    record.Deposit_Indicator = model.Deposit_Indicator;
                    record.Posted_Indicator = model.Posted_Indicator;

                    // Update record
                    db.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            // return Status OK
            return new HttpStatusCodeResult(HttpStatusCode.OK, model.id.ToString());
        }

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