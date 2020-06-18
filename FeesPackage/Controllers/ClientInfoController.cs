using FeesPackage.Data_Access;
using FeesPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    [AuthorizeRole(roles = "R/O, OPS, ADMIN")]
    public class ClientInfoController : BaseController
    {
        private ClientInfoModel GetModel(int id)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Client = db.tblClients.SingleOrDefault(x => x.id == id),

                Claim = db.tblClaims.SingleOrDefault(x => x.Reference_Number == id),

                ClientReferrals = db.tblClientReferrals.Where(x => x.Reference_Number == id).ToList(),

                Payments =
                    (from clt in db.tblClients
                     join cla in db.tblClaims on clt.id equals cla.Reference_Number
                     join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                     where clt.id == id
                     orderby pay.Period_From
                     select pay
                    ).ToList(),
            };

            return model;
        }

        // GET: ClientInfo/Print
#if DEBUG
        // render to a new tab for debugging
        public ActionResult Print(int id)
        {
            //RenderToPDF(ControllerContext, "~/Views/ClientInfo/Print.cshtml");

            return PartialView(GetModel(id));
        }
#else
        // render as PDF for download/print
        public void Print(int id)
        {
            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/ClientInfo/Print.cshtml", GetModel(id), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", "attachment; filename=ClientPrintout.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: ClientInfo/Create
        public ActionResult Create()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Client = new tblClient() {
                    Handling = 0.67,
                    Credit = 0.33
                },

                ClientReferrals = new List<tblClientReferral>(),

                Claims = new List<tblClaim>(),

                Payments = new List<tblPayment>(),

                Attys = new List<ListClass>
                {
                    new ListClass() {
                        Id = null,
                        Name = "Select ..."
                    }
                }
                .Concat(db.tblAttorneys.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials,
                    Name = c.Atty_Initials + " - " + c.Atty_Name
                }))
                .ToList(),

                ReferralwithClients = db.ReferralwithClients.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Referral_Name,
                    Name = c.Referral_Name
                })
                .ToList(),

                Referrals = db.tblReferrals.OrderBy(x => x.Referral_Name).ToList()
                .Select(c => new ListClass
                {
                    Id = c.Referral_Name,
                    Name = c.Referral_Name
                })
                .ToList(),

                AttyCombos = db.tblAttorneyCombos.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Deposit_Options.ToString(),
                    Name = c.Attorney_Combinations
                })
                .OrderBy(x => x.Id).ThenBy(x => x.Name)
                .ToList(),

                Adjusters = db.tblInsurances.OrderBy(x => x.Ins_Co_Name).ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Ins_Contact,
                    Name = c.Ins_Co_Name
                })
                .ToList(),

                Counties = new List<ListClass>
                {
                    new ListClass() {
                        Id = null,
                        Name = "Select ..."
                    }
                }
                .Concat(db.tblCounties.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.County,
                    Name = c.County + " - " + c.County_Desc
                }))
                .ToList(),

                Frequencys = db.tblPaymentFrequencies.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Payment_Frequency,
                    Name = c.Payment_Frequency
                })
                .ToList(),

                StatusCodes = db.tblStatusCodes.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Status_Code,
                    Name = c.Status_Code
                })
                .ToList()
            };

            return PartialView("~/Views/ClientInfo/_Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult SaveClientReferral(tblClientReferral data, int ref_no)
        {
            var model = data;
            try
            {
                if (model.id == 0)
                {   // insert
                    // transfer form fields
                    model.Reference_Number = ref_no;

                    // Insert into table
                    db.tblClientReferrals.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblClientReferral record = db.tblClientReferrals.Where(x => x.id == model.id).Single();

                    // transfer form fields
                    record.Reference_Number = ref_no;
                    record.Client_Referral_Atty = model.Client_Referral_Atty;
                    record.Client_Referral = model.Client_Referral;

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
            return Content(model.id.ToString());
        }

        [HttpPost]
        public ActionResult SaveClaim(tblClaim data, int ref_no)
        {
            var model = data;
            try
            {
                if (model.Reference_Number == null)
                {   // insert
                    // transfer form fields
                    model.Reference_Number = ref_no;

                    // Insert into table
                    db.tblClaims.Add(model);
                    db.SaveChanges();
                }
                else
                {   // update
                    // get original record for unedited fields
                    tblClaim record = db.tblClaims.Where(x => x.Claim_Number == model.Claim_Number).Single();

                    // transfer form fields
                    record.Claim_Number = model.Claim_Number;
                    record.Insurance_Contact = model.Insurance_Contact;
                    record.Claim_Date = model.Claim_Date;
                    record.Attorney_Breakdown = model.Attorney_Breakdown;
                    record.Payment_Amount = model.Payment_Amount;
                    record.Payment_Frequency = model.Payment_Frequency;
                    record.Status_Code = model.Status_Code;

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
            return Content(model.Reference_Number.ToString());
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
            return Content(model.id.ToString());    //Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Client = db.tblClients.SingleOrDefault(x => x.id == id),

                Claims = db.tblClaims.SqlQuery("select * from tblClaim where reference_number = " + id).ToList(),

                ClientReferrals = db.tblClientReferrals.Where(x => x.Reference_Number == id).ToList(),

                Referrals = db.tblReferrals.OrderBy(x => x.Referral_Name).ToList()
                .Select(c => new ListClass
                {
                    Id = c.Referral_Name,
                    Name = c.Referral_Name
                })
                .ToList(),

                Payments =
                    (from clt in db.tblClients
                     join cla in db.tblClaims on clt.id equals cla.Reference_Number
                     join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                     where clt.id == id
                     select pay
                    ).ToList(),

                Attys = new List<ListClass>
                {
                    new ListClass() {
                        Id = null,
                        Name = "Select ..."
                    }
                }
                .Concat(db.tblAttorneys.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials,
                    Name = c.Atty_Initials + " - " + c.Atty_Name
                }))
                .ToList(),

                ReferralwithClients = db.ReferralwithClients.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Referral_Name,
                    Name = c.Referral_Name
                })
                .ToList(),

                AttyCombos = db.tblAttorneyCombos.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Deposit_Options.ToString(),
                    Name = c.Attorney_Combinations
                })
                .ToList(),

                Adjusters = db.tblInsurances.OrderBy(x => x.Ins_Co_Name).ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Ins_Contact,
                    Name = c.Ins_Co_Name
                })
                .ToList(),

                Counties = new List<ListClass>
                {
                    new ListClass() {
                        Id = null,
                        Name = "Select ..."
                    }
                }
                .Concat(db.tblCounties.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.County,
                    Name = c.County + " - " + c.County_Desc
                }))
                .ToList(),

                Frequencys = db.tblPaymentFrequencies.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Payment_Frequency,
                    Name = c.Payment_Frequency
                })
                .ToList(),

                StatusCodes = db.tblStatusCodes.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Status_Code,
                    Name = c.Status_Code
                })
                .ToList(),

                ClaimNumbers = (from cla in db.tblClaims
                                join clt in db.tblClients on cla.Reference_Number equals clt.id
                                orderby clt.Client_Name
                                select new ListClass()
                                {
                                    Id = cla.Claim_Number,
                                    Name = clt.Client_Name + " - " + cla.Claim_Number
                                }
                ).ToList()
            };

            return PartialView("~/Views/ClientInfo/_Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditRO(int id)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Client = db.tblClients.SingleOrDefault(x => x.id == id),

                Claims = db.tblClaims.SqlQuery("select * from tblClaim where reference_number = " + id).ToList(),

                ClientReferrals = db.tblClientReferrals.Where(x => x.Reference_Number == id).ToList(),

                Referrals = db.tblReferrals.OrderBy(x => x.Referral_Name).ToList()
                .Select(c => new ListClass
                {
                    Id = c.Referral_Name,
                    Name = c.Referral_Name
                })
                .ToList(),

                Payments =
                    (from clt in db.tblClients
                     join cla in db.tblClaims on clt.id equals cla.Reference_Number
                     join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                     where clt.id == id
                     select pay
                    ).ToList(),

                Attys = new List<ListClass>
                {
                    new ListClass() {
                        Id = null,
                        Name = "Select ..."
                    }
                }
                .Concat(db.tblAttorneys.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials,
                    Name = c.Atty_Initials + " - " + c.Atty_Name
                }))
                .ToList(),

                ReferralwithClients = db.ReferralwithClients.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Referral_Name,
                    Name = c.Referral_Name
                })
                .ToList(),

                AttyCombos = db.tblAttorneyCombos.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Deposit_Options.ToString(),
                    Name = c.Attorney_Combinations
                })
                .ToList(),

                Adjusters = db.tblInsurances.OrderBy(x => x.Ins_Co_Name).ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Ins_Contact,
                    Name = c.Ins_Co_Name
                })
                .ToList(),

                Counties = new List<ListClass>
                {
                    new ListClass() {
                        Id = null,
                        Name = "Select ..."
                    }
                }
                .Concat(db.tblCounties.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.County,
                    Name = c.County + " - " + c.County_Desc
                }))
                .ToList(),

                Frequencys = db.tblPaymentFrequencies.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Payment_Frequency,
                    Name = c.Payment_Frequency
                })
                .ToList(),

                StatusCodes = db.tblStatusCodes.ToArray()
                .Select(c => new ListClass
                {
                    Id = c.Status_Code,
                    Name = c.Status_Code
                })
                .ToList(),

                ClaimNumbers = (from cla in db.tblClaims
                                join clt in db.tblClients on cla.Reference_Number equals clt.id
                                orderby clt.Client_Name
                                select new ListClass()
                                {
                                    Id = cla.Claim_Number,
                                    Name = clt.Client_Name + " - " + cla.Claim_Number
                                }
                ).ToList()
            };

            return PartialView("~/Views/ClientInfo/_EditRO.cshtml", model);
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

            User usr = (User)Session["LoggedInUser"];
            if (usr.RoleId == "R/O")
                return View("IndexRO", model);
            else
                return View(model);
        }
    }
}
