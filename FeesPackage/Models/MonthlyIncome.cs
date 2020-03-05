using FeesPackage.Data_Access;

public class MonthlyIncome
{
    public tblClient Client { get; set; }
    public tblAttyDesc Attorney { get; set; }
    public tblPayment Payment { get; set; }
}
