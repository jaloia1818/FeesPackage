using FeesPackage.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class ReportsController : BaseController
    {
		private ClientInfoModel GetReferralEscrowDetailModel(DateTime fromDate, DateTime toDate)
		{
			ClientInfoModel model = new ClientInfoModel
			{
				ReferralEscrowDetail =
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
				).GroupBy(x => new Groupings { Escrow_Account = x.Escrow_Account, Referral_Firm = x.Referral_Firm, Client_Referral_Atty = x.Client_Referral_Atty }).ToList()
			};

			return model;
		}

		// GET: ReferralEscrowDetail
#if DEBUG
		// render to a new tab for debugging
		public ActionResult ReferralEscrowDetail(DateTime fromDate, DateTime toDate)
		{
			ViewBag.fromDate = fromDate;
			ViewBag.toDate = toDate;

			return PartialView(GetReferralEscrowDetailModel(fromDate, toDate));
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

            var htmlContent = RenderViewToString(ControllerContext, "~/Views/Reports/ReferralEscrowDetail.cshtml", GetReferralEscrowDetailModel(fromDate, toDate), true);
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