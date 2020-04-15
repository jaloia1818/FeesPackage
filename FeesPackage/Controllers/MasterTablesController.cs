using FeesPackage.Data_Access;
using FeesPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class MasterTablesController : BaseController
    {
        // GET: MasterTables
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Attorney(tblAttorney model)
        {
            try
            {
                if (model.id == 0)
                {   // insert
                    // Insert into table
                    db.tblAttorneys.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblAttorney record = db.tblAttorneys.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Atty_Initials = model.Atty_Initials;
                    record.Atty_Name = model.Atty_Name;

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
            return Content(model.id.ToString());
        }

        [HttpGet]
        public ActionResult Attorney()
        {
            List<tblAttorney> model = db.tblAttorneys.OrderByDescending(x => x.id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AttorneyCombo(tblAttorneyCombo model)
        {
            try
            {
                if (model.id == 0)
                {   // insert
                    // Insert into table
                    db.tblAttorneyCombos.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblAttorneyCombo record = db.tblAttorneyCombos.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Deposit_Options = model.Deposit_Options;
                    record.Attorney_Combinations = model.Attorney_Combinations;

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
            return Content(model.id.ToString());
        }

        [HttpGet]
        public ActionResult AttorneyCombo()
        {
            List<tblAttorneyCombo> model = db.tblAttorneyCombos.OrderByDescending(x => x.id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult DepositTitleDescription(tblAttyDesc model)
        {
            try
            {
                if (model.id == 0)
                {   // insert
                    // Insert into table
                    db.tblAttyDescs.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblAttyDesc record = db.tblAttyDescs.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Combo_Indicator = model.Combo_Indicator;
                    record.Combo_Description = model.Combo_Description;

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
            return Content(model.id.ToString());
        }

        [HttpGet]
        public ActionResult DepositTitleDescription()
        {
            List<tblAttyDesc> model = db.tblAttyDescs.OrderByDescending(x => x.Combo_Indicator).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult County(tblCounty model)
        {
            try
            {
                if (model.id == 0)
                {   // insert
                    // set new Guid for primary key
                    int id = db.tblCounties.Max(x => x.id);
                    model.id = ++id;

                    // Insert into table
                    db.tblCounties.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblCounty record = db.tblCounties.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.County = model.County;
                    record.County_Desc = model.County_Desc;
                    record.County_Value = model.County_Value;

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
            return Content(model.id.ToString());
        }

        [HttpGet]
        public ActionResult County()
        {
            List<tblCounty> model = db.tblCounties.OrderByDescending(x => x.id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Referral(tblReferral model)
        {
            try
            {
                if (model.id == 0)
                {   // insert
                    // set new Guid for primary key
                    int id = db.tblReferrals.Max(x => x.id);
                    model.id = ++id;

                    // Insert into table
                    db.tblReferrals.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblReferral record = db.tblReferrals.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Referral_Name = model.Referral_Name;
                    record.Referral_Firm = model.Referral_Firm;
                    record.Referral_Tax_ID = model.Referral_Tax_ID;
                    record.Referral_Credit_Atty = model.Referral_Credit_Atty;

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
            return Content(model.id.ToString());
        }

        [HttpGet]
        public ActionResult ReferralSrc()
        {
            ReferralModel model = new ReferralModel
            {
                Referrals = db.tblReferrals.OrderByDescending(x => x.id).ToList(),

                Attys = db.tblAttorneys
                .ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials.ToString(),
                    Name = c.Atty_Name.ToString()
                })
                .ToList()
            };

            return PartialView("~/Views/MasterTables/Referral.cshtml", model);
        }

        [HttpGet]
        public ActionResult Referral()
        {
            ReferralModel model = new ReferralModel
            {
                Referrals = db.tblReferrals.OrderByDescending(x => x.id).ToList(),

                Attys = db.tblAttorneys.ToArray()
                .OrderBy(x => x.Atty_Initials)
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials.ToString(),
                    Name = c.Atty_Name.ToString()
                })
                .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult InsuranceInfo(tblInsurance model)
        {
            try
            {
                if (model.id == 0)
                {   // insert
                    // set new Guid for primary key
                    int id = db.tblInsurances.Max(x => x.id);
                    model.id = ++id;

                    // Insert into table
                    db.tblInsurances.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
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
                return HandleException(ex);
            }

            // return Status OK
            return Content(model.id.ToString());
        }

        [HttpGet]
        public ActionResult InsuranceContact()
        {
            List<tblInsurance> model = db.tblInsurances.OrderByDescending(x => x.id).ToList();

            return PartialView("~/Views/MasterTables/InsuranceInfo.cshtml", model);
        }

        [HttpGet]
        public ActionResult InsuranceInfo()
        {
            List<tblInsurance> model = db.tblInsurances.OrderByDescending(x => x.id).ToList();

            return View(model);
        }
    }
}