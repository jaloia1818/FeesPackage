//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FeesPackage.Data_Access
{
    using System;
    using System.Collections.Generic;
    
    public partial class qryClaims_30Days2
    {
        public string Handling_Atty { get; set; }
        public string Credit_Atty { get; set; }
        public int Reference_Number { get; set; }
        public string Client_Name { get; set; }
        public string Claim_Number { get; set; }
        public string Payment_Frequency { get; set; }
        public Nullable<decimal> Payment_Amount { get; set; }
        public System.DateTime Period_From { get; set; }
        public System.DateTime Period_To { get; set; }
        public Nullable<System.DateTime> LastDepositDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
