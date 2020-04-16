using FeesPackage.Data_Access;
using FeesPackage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects;

namespace FeesPackage.Controllers
{
    public class DailyPaymentsController : BaseController
    {
        [HttpPost]
        public JsonResult GetAttyBreakdown(String ClaimNumber)
        {
            var attyBreakdown =
            (from cla in db.tblClaims
             where cla.Claim_Number == ClaimNumber
             select new
             {
                 cla.Attorney_Breakdown
             }
            ).ToList()[0].Attorney_Breakdown;

            return Json(new { attyBreakdown });
        }

        private ClientInfoModel GetUnmatchedDepositsModel(DateTime fromDate, DateTime toDate)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                UnmatchedDeposits =
                (from clt in db.tblClients
                 join cla in db.tblClaims on clt.id equals cla.Reference_Number
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                 where pay.Input_Date >= fromDate && pay.Input_Date <= toDate && pay.Amount != cla.Payment_Amount
                 orderby cla.Claim_Number
                 select new UnmatchedDeposits()
                 {
                     Input_Date = pay.Input_Date,
                     Claim_Number = cla.Claim_Number,
                     Client_Name = clt.Client_Name,
                     Reference_Number = clt.id,
                     Period_From = pay.Period_From,
                     Period_To = pay.Period_To,
                     Amount = pay.Amount,
                     Payment_Frequency = cla.Payment_Frequency,
                     Payment_Amount = cla.Payment_Amount,
                     Comment = pay.Comment
                 }
                ).ToList()
            };

            return model;
        }

        private ClientInfoModel GetDepositModel(DateTime fromDate, DateTime toDate)
        {
            var connection = db.Database.Connection as SqlConnection;
            connection.Open();
            
            SqlCommand command = new SqlCommand(string.Format("SELECT atty.Combo_Description AS Deposit_Breakdown, ROUND(SUM(pay.Amount * 1), 2) AS Total, ROUND(SUM(pay.Amount * clt.Escrow), 2) AS Escrow, \n" +
                                                              "ROUND(SUM((pay.Amount - pay.Amount * clt.Escrow) * cnty.County_Value), 2) AS Philadelphia, ROUND(SUM((pay.Amount - pay.Amount * clt.Escrow)\n" +
                                                              "- (pay.Amount - pay.Amount * clt.Escrow) * cnty.County_Value), 2) AS County\n" +
                                                              "FROM dbo.tblPayments AS pay INNER JOIN\n" +
                                                              "dbo.tblClaim AS clm ON pay.Claim_Number = clm.Claim_Number INNER JOIN\n" +
                                                              "dbo.tblClient AS clt ON clm.Reference_Number = clt.id INNER JOIN\n" +
                                                              "dbo.tblCounty AS cnty ON clt.County = cnty.County INNER JOIN\n" +
                                                              "dbo.tblAttyDesc AS atty ON clm.Attorney_Breakdown = atty.Combo_Indicator\n" +
                                                              "WHERE(pay.Input_Date >= '{0}') AND(pay.Input_Date <= '{1}') AND(pay.Posted_Indicator = 0)\n" + 
                                                              "GROUP BY atty.Combo_Description order by deposit_breakdown", fromDate, toDate), connection);
            SqlDataReader reader = command.ExecuteReader();

            ClientInfoModel model = new ClientInfoModel
            {
                DailyDeposits = new List<DailyDeposits>()
            };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                        reader.GetString(0),
                        reader.GetSqlMoney(1),
                        reader.GetDouble(2),
                        reader.GetDouble(3),
                        reader.GetDouble(4));

                    DailyDeposits dep = new DailyDeposits()
                    {
                        Deposit_Breakdown = reader.GetString(0),
                        Total = (Decimal)reader.GetSqlMoney(1),
                        Escrow = reader.GetDouble(2),
                        Philadelphia = reader.GetDouble(3),
                        County = reader.GetDouble(4)
                    };

                    model.DailyDeposits.Add(dep);
                }
            }

            reader.Close();

            return model;
        }

        private ClientInfoModel GetDetailModel(DateTime fromDate, DateTime toDate)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                DailyDetails =
                (from clt in db.tblClients
                 join cla in db.tblClaims on clt.id equals cla.Reference_Number
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                 where pay.Input_Date >= fromDate && pay.Input_Date <= toDate && pay.Posted_Indicator == false
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

        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                // get original record for unedited fields
                tblPayment record = db.tblPayments.Where(x => x.id == Id).Single();

                // Delete record
                db.Entry(record).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

            return Json(new { isDeleted = true });
        }

        // POST: PostPayments
        [HttpPost]
        public JsonResult PostPayments(DateTime fromDate, DateTime toDate)
        {
            // output params
            ObjectParameter cnt = new ObjectParameter("cnt", 0);            // int
            ObjectParameter amt = new ObjectParameter("amt", 0.0);          // double
            ObjectParameter rowCount = new ObjectParameter("rowCount", 0);  // int
            
            // call sproc with 2 input and 3 output params
            db.sp_PostPayments(fromDate, toDate, cnt, amt, rowCount);
            
            // convert
            int Cnt = int.Parse(cnt.Value.ToString());
            double Amt = double.Parse(amt.Value.ToString());
            int RowCount = int.Parse(rowCount.Value.ToString());

            return Json(new { count = Cnt, amount = Amt.ToString("C"), row_count = RowCount });
        }

        // GET: UnmatchedDepositsPrint
#if DEBUG
        // render to a new tab for debugging
        public ActionResult UnmatchedDepositsPrint(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetUnmatchedDepositsModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void UnmatchedDepositsPrint(DateTime fromDate, DateTime toDate)
        {
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/DailyPayments/UnmatchedDepositsPrint.cshtml", GetUnmatchedDepositsModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} UnmatchedDepositsPrint.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: DailyDepositsPrint
#if DEBUG
        // render to a new tab for debugging
        public ActionResult DailyDepositsPrint(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetDepositModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void DailyDepositsPrint(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Portrait
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/DailyPayments/DailyDepositsPrint.cshtml", GetDepositModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} DailyDepositsPrint.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: DailyDetailPrint
#if DEBUG
        // render to a new tab for debugging
        public ActionResult DailyDetailPrint(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetDetailModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void DailyDetailPrint(DateTime fromDate, DateTime toDate)
        {
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/DailyPayments/DailyDetailPrint.cshtml", GetDetailModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} DailyDetailPrint.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        [HttpPost]
        public ActionResult Index(tblPayment model, FormCollection coll)
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
                    record.Comment = model.Comment;

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

        // GET: DailyPayments
        public ActionResult Index()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Payments = db.tblPayments.Where(x => x.Posted_Indicator == false)
                                         .OrderByDescending(x => x.id).ToList(),

                AttyCombos = db.tblAttorneyCombos.ToArray()
                .OrderBy(x => x.Attorney_Combinations)
                .Select(c => new ListClass
                {
                    Id = c.Deposit_Options.ToString(),
                    Name = c.Attorney_Combinations
                })
                .ToList(),

                ClaimNumbers = (from cla in db.tblClaims
                                join clt in db.tblClients on cla.Reference_Number equals clt.id
                                orderby clt.Client_Name
                                select new ListClass()
                                {
                                    Id = cla.Claim_Number,
                                    Name = clt.Client_Name
                                }
                ).ToList()
            };

            return View(model);
        }
    }
}