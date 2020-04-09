using FeesPackage.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ReportsController : BaseController
    {
        private ClientInfoModel GetModel()
        {
            ClientInfoModel model = new ClientInfoModel
            {
                Clients = db.tblClients
                            .Where(x => x.Client_Name != null)
                            .OrderBy(x => x.Client_Name).ToList(),

                Attys = db.tblClientReferrals//.GroupBy(x => x.Client_Referral_Atty)
                .Select(c => new ListClass
                {
                    Id = c.Client_Referral_Atty,
                    Name = c.Client_Referral_Atty
                }).Distinct()
                .OrderBy(x => x.Name)
                .ToList(),
 
                Hand_Attys = db.tblAttorneys
                .Select(c => new ListClass
                {
                    Id = c.Atty_Initials,
                    Name = c.Atty_Name
                }).Distinct()
                .OrderBy(x => x.Name)
                .ToList(),
            };

            return model;
        }

        private ClientInfoModel GetReferralFeesModel(string attorney, string client)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                MonthlyIncome =
                (from clt in db.tblClients
                 join cla in db.tblClaims on clt.id equals cla.Reference_Number
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number
                 join atty in db.tblAttyDescs on cla.Attorney_Breakdown equals (int)atty.Combo_Indicator
                 join cltref in db.tblClientReferrals on clt.id equals cltref.Reference_Number
                 orderby cltref.Client_Referral_Atty, clt.Client_Name, pay.Period_From, pay.Period_To
                 select new MonthlyIncome()
                 {
                     Client = clt,
                     Attorney = atty,
                     Payment = pay,
                     RefAtty = cltref
                 }
                ).Where(x => x.RefAtty.Client_Referral_Atty == attorney || x.Client.Client_Name == client)
                .GroupBy(x => x.RefAtty.Client_Referral_Atty).ToList()
            };

            return model;
        }

        private ClientInfoModel ClaimsDormantModel(string atty)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                ClaimsDormant =
                (from cla in db.qryClaimsDormant2
                 where ((string.IsNullOrEmpty(atty)) ? 1 == 1 : cla.Handling_Atty == atty)
                 orderby cla.Handling_Atty, cla.Credit_Atty, cla.Client_Name
                 select cla
                ).ToList()
            };

            return model;
        }

        private ClientInfoModel ClaimsWithNoPaymentsOver30DaysModel(string atty)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                ClaimsWithNoPaymentsOver30Days =
                (from cla in db.qryClaims_30Days2
                 where ((string.IsNullOrEmpty(atty)) ? 1 == 1 : cla.Handling_Atty == atty)
                 orderby cla.Handling_Atty, cla.Credit_Atty, cla.Client_Name
                 select cla
                ).ToList()
            };

            return model;
        }

        private ClientInfoModel ClaimsWithNoPaymentsModel(string atty)
        {
            ClientInfoModel model = new ClientInfoModel
            {
                ClaimsWithNoPayments =
                (from cla in db.tblClaims
                 join pay in db.tblPayments on cla.Claim_Number equals pay.Claim_Number into claPay
                 from cp in claPay.DefaultIfEmpty()
                 join clt in db.tblClients on cla.Reference_Number equals clt.id
                 where cp.Claim_Number == null && cla.Status_Code != "C" && ((string.IsNullOrEmpty(atty)) ? 1 == 1 : clt.Handling_Atty == atty)
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

        // GET: RefAttyFeesByFP
#if DEBUG
        // render to a new tab for debugging
        public ActionResult RefAttyFeesByFP(string atty, string client)
        {
            return PartialView(GetReferralFeesModel(atty, client));
        }
#else
        // render as PDF for download/print
        public void RefAttyFeesByFP(string atty, string client)
        {
            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Portrait
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/RefAttyFeesByFP.cshtml", GetReferralFeesModel(atty, client), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} RefAttyFeesByFP.pdf", DateTime.Now.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: PostedDeposits
#if DEBUG
        // render to a new tab for debugging
        public ActionResult PostedDeposits(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return PartialView(GetMonthlyIncomeModel(fromDate, toDate));
        }
#else
        // render as PDF for download/print
        public void PostedDeposits(DateTime fromDate, DateTime toDate)
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/PostedDeposits.cshtml", GetMonthlyIncomeModel(fromDate, toDate), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} PostedDeposits.pdf", fromDate.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: ClaimsDormant
#if DEBUG
        // render to a new tab for debugging
        public ActionResult ClaimsDormant(string atty)
        {
            return PartialView(ClaimsDormantModel(atty));
        }
#else
        // render as PDF for download/print
        public void ClaimsDormant(string atty)
        {
            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Landscape
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ClaimsDormant.cshtml", ClaimsDormantModel(atty), true);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = string.Empty;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0} ClaimsDormant.pdf", DateTime.Now.ToString("MM/dd/yy")));
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
        }
#endif

        // GET: ClaimsWithNoPaymentsOver30Days
#if DEBUG
        // render to a new tab for debugging
        public ActionResult ClaimsWithNoPaymentsOver30Days(string atty)
        {
            return PartialView(ClaimsWithNoPaymentsOver30DaysModel(atty));
        }
#else
        // render as PDF for download/print
        public void ClaimsWithNoPaymentsOver30Days(string atty)
        {
            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Landscape
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ClaimsWithNoPaymentsOver30Days.cshtml", ClaimsWithNoPaymentsOver30DaysModel(atty), true);
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
        public ActionResult ClaimsWithNoPayments(string atty)
        {
            return PartialView(ClaimsWithNoPaymentsModel(atty));
        }
#else
        // render as PDF for download/print
        public void ClaimsWithNoPayments(string atty)
        {
            var footerHtml = $@"<div style=""text-align:center"">page <span class=""page""></span> of <span class=""topage""></span></div>";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter
            {
                PageFooterHtml = footerHtml,
                Margins = new NReco.PdfGenerator.PageMargins { Bottom = 15, Top = 15, Left = 10, Right = 10 },
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = NReco.PdfGenerator.PageOrientation.Landscape
            };

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ClaimsWithNoPayments.cshtml", ClaimsWithNoPaymentsModel(atty), true);
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
            return View(GetModel());
        }
    }
}