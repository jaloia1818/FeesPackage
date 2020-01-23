using FeesPackage.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class MasterTablesController : Controller
    {
        private readonly FeesPackageEntities db = new FeesPackageEntities();

        // GET: MasterTables
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Attorney()
        {
            List<tblAttorney> model = db.tblAttorneys.ToList();

            return View(model);
        }

        public ActionResult AttorneyCombo()
        {
            List<tblAttorneyCombo> model = db.tblAttorneyCombos.ToList();

            return View(model);
        }

        public ActionResult DepositTitleDescription()
        {
            List<tblAttyDesc> model = db.tblAttyDescs.ToList();

            return View(model);
        }

        public ActionResult County()
        {
            List<tblCounty> model = db.tblCounties.ToList();

            return View(model);
        }

        public ActionResult Referral()
        {
            List<tblReferral> model = db.tblReferrals.ToList();

            return View(model);
        }

        [HttpPost]
        public HttpStatusCodeResult InsuranceInfo(tblInsurance model)
        {
            try
            {
                if (model.id == 0) { // insert
                    // set new Guid for primary key
                    int id = db.tblInsurances.Max(x => x.id);
                    model.id = ++id;

                    // Insert into table
                    db.tblInsurances.Add(model);
                    db.SaveChanges();
                }
                else {               // update
                    // get original record for unedited fields
                    tblInsurance record = db.tblInsurances.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Ins_Co_Name = model.Ins_Co_Name;
                    record.Ins_Contact = model.Ins_Contact;

                    // Update record
                    db.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }

            // return Status OK
            return new HttpStatusCodeResult(HttpStatusCode.OK, model.id.ToString());
        }

        [HttpGet]
        public ActionResult InsuranceInfo()
        {
            List<tblInsurance> model = db.tblInsurances.OrderByDescending(x => x.id).ToList();

            return View(model);
        }
    }
}