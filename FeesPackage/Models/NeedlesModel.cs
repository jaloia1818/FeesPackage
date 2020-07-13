using System.Collections.Generic;
using System.Linq;
using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class NeedlesModel
    {
        public string CheckList { get; set; }
        public int CheckListCount { get; set; }
        public Needles_Case Case { get; set; }
        public User_Case_Data Case_Data { get; set; }
        public Insurances Insurances { get; set; }
        public Party Party { get; set; }
        public Partys Partys { get; set; }
        public Insurers Insurers { get; set; }
        public Adjusters Adjusters { get; set; }
        public string CaseNotes { get; set; }
    }
}