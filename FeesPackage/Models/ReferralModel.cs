﻿using System.Collections.Generic;
using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class ReferralModel
    {
       public List<tblReferral> Referrals { get; set; }
       public List<ListClass> Attys { get; set; }
    }
}