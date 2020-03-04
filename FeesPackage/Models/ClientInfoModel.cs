using System.Collections.Generic;
using System.Linq;
using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class ClientInfoModel
    {
        public List<ListClass> Counties { get; set; }
        public tblClient Client { get; set; }
        public List<tblClient> Clients { get; set; }
        public List<ListClass> Attys { get; set; }
        public List<ListClass> ReferralwithClients { get; set; }
        public List<ListClass> AttyCombos { get; set; }
        public List<ListClass> Adjusters { get; set; }
        public List<ListClass> Frequencys { get; set; }
        public List<ListClass> StatusCodes { get; set; }
        public List<tblPayment> Payments { get; set; }
        public tblClaim Claim { get; set; }
        public List<qryClaim> Claims { get; set; }
        public List<tblClientReferral> ClientReferrals { get; set; }
        public List<DailyDetail> DailyDetails { get; set; }
        public List<DailyDeposits> DailyDeposits { get; set; }
        public List<UnmatchedDeposits> UnmatchedDeposits { get; set; }
        public List<IGrouping<string, ReferralEscrowDetail>> ReferralEscrow { get; set; }
        public List<IGrouping<string, MonthlyIncome>> MonthlyIncome { get; set; }
    }
}