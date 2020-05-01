using System;

namespace FeesPackage.Models
{
    public class DailyDeposits
    {
        public string Deposit_Breakdown { get; set; }
        public Decimal Total { get; set; }
        public Double Escrow { get; set; }
        public Double Philadelphia { get; set; }
        public Double County { get; set; }
    }
}
