using System;

namespace FeesPackage.Models
{
    public class Payments
    {
        public int id { get; set; }
        public string Claim_Number { get; set; }
        public string Payment_Date { get; set; }
        public string Period_From { get; set; }
        public string Period_To { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Input_Date { get; set; }
        public string Inputter_Name { get; set; }
        public Nullable<int> Deposit_Indicator { get; set; }
        public Nullable<bool> Posted_Indicator { get; set; }
        public string Comment { get; set; }
    }
}