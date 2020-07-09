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
        public Insurance Insurance { get; set; }
        public Insurer Insurer { get; set; }
        public string Name { get; set; }
    }
}