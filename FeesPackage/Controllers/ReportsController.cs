using FeesPackage.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ReportsController : BaseController
    {
        private ClientInfoModel GetMonthlyIncomeModel(DateTime fromDate, DateTime toDate)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                MonthlyIncome =
                (from clt in db.tblClients
                 join cla in db.tblClaims on clt.id equals cla.Reference_Number
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                 join atty in db.tblAttyDescs on cla.Attorney_Breakdown equals (int)atty.Combo_Indicator
                 join cty in db.tblCounties on clt.County equals cty.County
                 where pay.Input_Date >= fromDate && pay.Input_Date <= toDate && pay.Posted_Indicator == true
                 orderby atty.Combo_Description
                 select new MonthlyIncome()
                 {
                     Combo_Description = atty.Combo_Description,
                     Amt_AmtXEscrow = (double?)pay.Amount - ((double?)pay.Amount * clt.Escrow),
                     Combo_Name = clt.Credit_Atty + "/" + clt.Handling_Atty,
                     County = clt.County
                 }
                ).GroupBy(x => x.Combo_Description).ToList()
            };

            foreach(var grp in model.MonthlyIncome.OrderBy(x => x.Key))
            {
                System.Diagnostics.Debug.WriteLine(string.Format("===> {0} {1} {2} {3}", 
                    grp.Key,
                    grp.Where(x => x.County == "Phila").Sum(x => x.Amt_AmtXEscrow), 
                    grp.Where(x => x.County == "Cty").Sum(x => x.Amt_AmtXEscrow),
                    grp.Sum(x => x.Amt_AmtXEscrow)));

                foreach (var atty in grp.GroupBy(x => x.Combo_Name).OrderBy(x => x.Key))
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} {1} {2} {3}", 
                        atty.Key, 
                        atty.Where(x => x.County == "Phila").Sum(x => x.Amt_AmtXEscrow), 
                        atty.Where(x => x.County == "Cty").Sum(x => x.Amt_AmtXEscrow),
                        atty.Sum(x => x.Amt_AmtXEscrow)));
                }
            }

            System.Diagnostics.Debug.WriteLine(string.Format("===> {0} {1} {2} {3}",
                "Grand Total:",
                model.MonthlyIncome.SelectMany(n => n).Where(x => x.County == "Phila").Sum(x => x.Amt_AmtXEscrow),
                model.MonthlyIncome.SelectMany(n => n).Where(x => x.County == "Cty").Sum(x => x.Amt_AmtXEscrow),
                model.MonthlyIncome.SelectMany(n => n).Sum(x => x.Amt_AmtXEscrow)));

            return model;
        }

        private ClientInfoModel GetReferralEscrowModel(DateTime fromDate, DateTime toDate)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                ReferralEscrow =
                (from clt in db.tblClients
                 join cla in db.tblClaims on clt.id equals cla.Reference_Number
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                 join atty in db.tblAttyDescs on cla.Attorney_Breakdown equals (int)atty.Combo_Indicator
                 join cltref in db.tblClientReferrals on clt.id equals cltref.Reference_Number
                 join refer in db.tblReferrals on cltref.Client_Referral_Atty equals refer.Referral_Name
                 where pay.Input_Date >= fromDate && pay.Input_Date <= toDate && pay.Posted_Indicator == true
                 orderby atty.Combo_Description
                 select new ReferralEscrowDetail()
                 {
                     Escrow_Account = atty.Combo_Description,
                     Referral_Firm = refer.Referral_Firm,
                     Client_Referral_Atty = cltref.Client_Referral_Atty,
                     Client_Name = clt.Client_Name,
                     Period_From = pay.Period_From,
                     Period_To = pay.Period_To,
                     Amount = pay.Amount,
                     Client_Referral = cltref.Client_Referral,
                     Sub_Total = (Double?)pay.Amount * cltref.Client_Referral
                 }
                ).GroupBy(x => x.Escrow_Account).ToList()
            };

            return model;
        }

        // GET: MonthlyIncome
#if DEBUG
        // render to a new tab for debugging
        public ActionResult MonthlyIncome(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetMonthlyIncomeModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void MonthlyIncome(DateTime fromDate, DateTime toDate)
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/MonthlyIncome.cshtml", GetMonthlyIncomeModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} MonthlyIncome.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: ReferralStatement
#if DEBUG
        // render to a new tab for debugging
        public ActionResult ReferralStatement(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetReferralEscrowModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void ReferralStatement(DateTime fromDate, DateTime toDate)
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ReferralStatement.cshtml", GetReferralEscrowModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} ReferralStatement.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: ReferralEscrowDetail
#if DEBUG
        // render to a new tab for debugging
        public ActionResult ReferralEscrowSummary(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetReferralEscrowModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void ReferralEscrowSummary(DateTime fromDate, DateTime toDate)
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ReferralEscrowSummary.cshtml", GetReferralEscrowModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} ReferralEscrowSummary.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: ReferralEscrowDetail
#if DEBUG
        // render to a new tab for debugging
        public ActionResult ReferralEscrowDetail(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetReferralEscrowModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void ReferralEscrowDetail(DateTime fromDate, DateTime toDate)
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ReferralEscrowDetail.cshtml", GetReferralEscrowModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} ReferralEscrowDetail.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
    }
}