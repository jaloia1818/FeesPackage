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
    
    public partial class tblClaim
    {
        public Nullable<int> Reference_Number { get; set; }
        public string Claim_Number { get; set; }
        public string Insurance_Contact { get; set; }
        public Nullable<System.DateTime> Claim_Date { get; set; }
        public Nullable<int> Attorney_Breakdown { get; set; }
        public Nullable<decimal> Payment_Amount { get; set; }
        public string Payment_Frequency { get; set; }
        public string Status_Code { get; set; }
    }
}
