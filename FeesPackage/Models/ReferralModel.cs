using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class ReferralModel
    {
       public List<tblReferral> Referrals { get; set; }
       public List<ListClass> Attys { get; set; }
    }

    public class ListClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}