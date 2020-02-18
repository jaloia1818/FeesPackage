namespace FeesPackage.Models
{
    using System;
    
    public partial class DailyDetail
    {
        public string Claim_Number { get; set; }
        public string Client_Name { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public string Handling_Atty { get; set; }
        public string Credit_Atty { get; set; }
        public Decimal? Amount { get; set; }
        public Double? Escrow { get; set; }
        public string Status_Code { get; set; }
        public Nullable<System.DateTime> Input_Date { get; set; }
    }
}
