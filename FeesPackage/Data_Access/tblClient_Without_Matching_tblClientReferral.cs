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
    
    public partial class tblClient_Without_Matching_tblClientReferral
    {
        public int Reference_Number { get; set; }
        public string Client_Name { get; set; }
        public string Employer_Name { get; set; }
        public Nullable<double> Escrow__ { get; set; }
        public string Handling_Atty { get; set; }
        public Nullable<double> Handling__ { get; set; }
        public string Credit_Atty { get; set; }
        public Nullable<double> Credit__ { get; set; }
        public Nullable<bool> Is_County { get; set; }
        public string County { get; set; }
        public string Accident_Desc { get; set; }
        public Nullable<short> Selection_Control { get; set; }
    }
}
