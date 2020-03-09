using FeesPackage.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ReportsController : BaseController
    {
        public ClientInfoModel ClaimsWithNoPaymentsOver30DaysModel()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                ClaimsWithNoPaymentsOver30Days =
                (from cla in db.qryClaims_30Days2
                 orderby cla.Handling_Atty, cla.Credit_Atty, cla.Client_Name
                 select cla
                ).ToList()
            };

            return model;
        }

        private ClientInfoModel ClaimsWithNoPaymentsModel()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                ClaimsWithNoPayments =
                (from cla in db.tblClaims
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number into claPay
                 from cp in claPay.DefaultIfEmpty()
                 join clt in db.tblClients on cla.Reference_Number equals clt.id
                 where cp.Claim_Number == null && cla.Status_Code != "C"
                 orderby clt.Handling_Atty, clt.Credit_Atty, clt.Client_Name
                 select new ClaimsWithNoPayments()
                 {
                     Client = clt,
                     Claim = cla
                 }
                ).ToList()
            };

            return model;
        }

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
                     Client = clt,
                     Attorney = atty,
                     Payment = pay
                 }
                ).GroupBy(x => x.Attorney.Combo_Description).ToList()
            };

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

        // GET: ClaimsWithNoPayments
#if DEBUG
        // render to a new tab for debugging
        public ActionResult ClaimsWithNoPaymentsOver30Days()
        {
            return PartialView(ClaimsWithNoPaymentsOver30DaysModel());
        }
#else
        // render as PDF for download/print
        public void ClaimsWithNoPaymentsOver30Days()
        {
            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Landscape
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ClaimsWithNoPayments.cshtml", ClaimsWithNoPaymentsOver30DaysModel(), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} ClaimsWithNoPaymentsOver30Days.pdf", DateTime.Now.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: ClaimsWithNoPayments
#if DEBUG
        // render to a new tab for debugging
        public ActionResult ClaimsWithNoPayments()
        {
            return PartialView(ClaimsWithNoPaymentsModel());
        }
#else
        // render as PDF for download/print
        public void ClaimsWithNoPayments()
        {
            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Landscape
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ClaimsWithNoPayments.cshtml", ClaimsWithNoPaymentsModel(), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} ClaimsWithNoPayments.pdf", DateTime.Now.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: MonthlyIncome
#if DEBUG
        // render to a new tab for debugging
        public ActionResult MonthlyIncomeDetail(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetMonthlyIncomeModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void MonthlyIncomeDetail(DateTime fromDate, DateTime toDate)
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/MonthlyIncomeDetail.cshtml", GetMonthlyIncomeModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} MonthlyIncomeDetail.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif


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
                Orientation = NReco.PdfGenerator.PageOrientation.Portrait
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