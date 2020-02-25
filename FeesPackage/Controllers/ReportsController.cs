using FeesPackage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
				).GroupBy(x => x.Escrow_Account).ToList()
			};

			return model;
		}

		private ClientInfoModel GetReferralEscrowDetailModel2(DateTime fromDate, DateTime toDate)
        {
			var connection = db.Database.Connection as SqlConnection;
			connection.Open();

			SqlCommand command = new SqlCommand(String.Format(
				"select atty.Combo_Description as [Escrow_Account], " +
				"	   ref.Referral_Firm, " +
				"	   cltref.Client_Referral_Atty, " +
				"	   clt.Client_Name, " +
				"	   cast(pay.Period_From as Date) as [Period_From], " +
				"	   cast(pay.Period_To as Date) as [Period_To], " +
				"	   pay.Amount, " +
				"	   cltref.Client_Referral, " +
				"	   round(pay.Amount * cltref.Client_Referral, 2) as [Sub_Total] " +
				"from tblPayments pay " +
				"inner join tblClaim clm on pay.Claim_Number = clm.Claim_Number " +
				"inner join tblClient clt on clm.Reference_Number = clt.id " +
				"inner join tblAttyDesc atty on clm.Attorney_Breakdown = atty.Combo_Indicator " +
				"inner join tblClientReferral cltref on clt.id = cltref.Reference_Number " +
				"inner join tblReferral ref on cltref.Client_Referral_Atty = ref.Referral_Name " +
				"where pay.Input_Date >= '2/19/20' and pay.Input_Date <= '2/20/20' and pay.Posted_Indicator = 1 " +
				"group by atty.Combo_Description, ref.Referral_Firm, cltref.Client_Referral_Atty, clt.Client_Name, pay.Period_From, pay.Period_To, pay.Amount, cltref.Client_Referral, pay.Amount * cltref.Client_Referral", fromDate, toDate), connection);
			SqlDataReader reader = command.ExecuteReader();

			ClientInfoModel model = new ClientInfoModel
			{
				ReferralEscrowDetail = new List<IGrouping<string, ReferralEscrowDetail>>()
			};

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					System.Diagnostics.Debug.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
						reader.GetString(0),
						reader.GetString(1),
						reader.GetString(2),
						reader.GetString(3),
						reader.GetDateTime(4),
						reader.GetDateTime(5),
						reader.GetSqlMoney(6),
						reader.GetDouble(7),
						reader.GetDouble(8));

					ReferralEscrowDetail detail = new ReferralEscrowDetail()
					{
						Escrow_Account = reader.GetString(0),
						Referral_Firm = reader.GetString(1),
						Client_Referral_Atty = reader.GetString(2),
						Client_Name = reader.GetString(3),
						Period_From = reader.GetDateTime(4),
						Period_To = reader.GetDateTime(5),
						Amount = reader.GetDecimal(6),
						Client_Referral = reader.GetDouble(7),
						Sub_Total = reader.GetDouble(8)
					};

					//model.ReferralEscrowDetail.Add(detail);
				}
			}

			reader.Close();
            
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

		// GET: Reports
		public ActionResult Index()
        {
            return View();
        }
    }
}