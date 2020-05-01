using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class ClaimsWithNoPayments
    {
        public tblClient Client { get; set; }
        public tblClaim Claim { get; set; }
    }
}
