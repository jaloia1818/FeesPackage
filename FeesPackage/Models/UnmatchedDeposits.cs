namespace FeesPackage.Models
{
    using System;
    
    public partial class UnmatchedDeposits
    {
        public DateTime? Input_Date { get; set; }
        public string Claim_Number { get; set; }
        public string Client_Name { get; set; }
        public int Reference_Number { get; set; }
        public DateTime? Period_From { get; set; }
        public DateTime? Period_To { get; set; }
        public Decimal? Amount { get; set; }
        public string Payment_Frequency { get; set; }
        public Decimal? Payment_Amount { get; set; }
        public string Comment { get; set; }
    }
}
