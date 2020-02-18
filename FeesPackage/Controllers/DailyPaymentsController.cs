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
        private ClientInfoModel GetModel(DateTime fromDate, DateTime toDate)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                DailyDetails =
                (from clt in db.tblClients
                 join cla in db.tblClaims on clt.id equals cla.Reference_Number
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                 where pay.Payment_Date >= fromDate && pay.Payment_Date <= toDate
                 select new DailyDetail()
                 {
                     Claim_Number = cla.Claim_Number,
                     Client_Name = clt.Client_Name,
                     Payment_Date = pay.Payment_Date,
                     Handling_Atty = clt.Handling_Atty,
                     Credit_Atty = clt.Credit_Atty,
                     Amount = pay.Amount,
                     Escrow = clt.Escrow,
                     Status_Code = cla.Status_Code,
                     Input_Date = pay.Input_Date
                 }
                ).ToList()
            };

            return model;
        }

        // GET: DailyDetailPrint
#if !DEBUG
        // render to a new tab for debugging
        public ActionResult DailyDetailPrint()
        {
            var fromDate = DateTime.Parse("1/1/20").Date;
            var toDate = DateTime.Parse("1/31/20").Date;
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void DailyDetailPrint(DateTime fromDate, DateTime toDate)
        {
            //var fromDate = DateTime.Parse("1/1/20").Date;
            //var toDate = DateTime.Parse("1/31/20").Date;
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Landscape
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/DailyPayments/DailyDetailPrint.cshtml", GetModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", "attachment; filename=DailyDetailPrint.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

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