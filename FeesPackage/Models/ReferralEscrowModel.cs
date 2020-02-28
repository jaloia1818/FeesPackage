using System;

namespace FeesPackage.Models
{
    public class ReferralEscrowDetail
    {
       public string Escrow_Account { get; set; }
       public string Referral_Firm { get; set; }
       public string Client_Referral_Atty { get; set; }
       public string Client_Name { get; set; }
       public DateTime? Period_From { get; set; }
       public DateTime? Period_To { get; set; }
       public Decimal? Amount { get; set; }
       public Double? Client_Referral { get; set; }
       public Double? Sub_Total { get; set; }
    }
}