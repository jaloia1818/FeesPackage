using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class MonthlyIncome
    {
        public tblClient Client { get; set; }
        public tblAttyDesc Attorney { get; set; }
        public tblPayment Payment { get; set; }
        public tblClientReferral RefAtty { get; set; }
    }
}
