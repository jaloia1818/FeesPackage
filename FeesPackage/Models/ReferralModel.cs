using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class ReferralModel
    {
       public List<tblReferral> referrals { get; set; }
       public List<listClass> attys { get; set; }
    }

    public class listClass
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}