using System;
using System.Data;

namespace FeesPackage.Models
{
    public class User_Case_Data
    {
        public int casenum { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string PRIOR_STATUS { get; set; }
        public string DATE_DIS { get; set; }
        public string ATTY_FEE { get; set; }
        public string AWW { get; set; }
        public string PETITION_TYP { get; set; }
        public string START_DATE { get; set; }
        public string TTD_RATE { get; set; }
        public string CR_ATTORNEY { get; set; }
        public string JUDGE { get; set; }
        public string IME_DOCTOR { get; set; }
        public string DATE_INJ { get; set; }
        public string AWWCOMP { get; set; }
        public string CLMT_DOCTOR { get; set; }
        public string DATE_OF_IME { get; set; }
        public string RECCOMP_Dollars { get; set; }
        public string CREDIT_ATTY { get; set; }
        public string LIT_COSTS { get; set; }
        public string ONSET_DATE { get; set; }
        public string DT_DISABLED { get; set; }
        public string WC { get; set; }
        public string DPA { get; set; }
        public string LTD { get; set; }
        public string PIA { get; set; }
        public string DEPENDENTS { get; set; }
        public string TTD_OR_TPD { get; set; }
        public string CURRENT_RATE { get; set; }
        public string CR_DollarsS { get; set; }
        public string DATE_DENIED { get; set; }
        public string MED_CR_Dollars { get; set; }
        public string SSD { get; set; }
        public string TREATING_DR { get; set; }
        public string SETTLEMENT_Dollars { get; set; }
        public string Injuries { get; set; }
        public string Petitions_Resolved { get; set; }
        public string Petitions_Pendings { get; set; }
        public string Client_Dollars_Owed_to_us { get; set; }
        public string Fee_Pkg_No { get; set; }
        public string TPD_Rate { get; set; }
        public string WC_Rate_at_Settlement { get; set; }
        public string Unemp_Comp_Dollars { get; set; }
        public string Supt_Order { get; set; }
        public char? XXX_OLD_PINS_FIELDS_XXX { get; set; }
        public string STDLTD { get; set; }
        public string Return_to_Work_date { get; set; }
        public char? VOC { get; set; }
        public string Voc_Counselor { get; set; }
        public char? IRE { get; set; }
        public string IRE_Date { get; set; }
        public string How_Long_Emp { get; set; }
        public string Client_Dollars_Start_Date { get; set; }
        public string SupersedEffectiveDate { get; set; }
        public string Carrier_Dollars_Start_Date { get; set; }
        public char? CFA_Signed { get; set; }
        public string Appeal_Num { get; set; }
        public string TREATING_DOC { get; set; }
        public string DECISION_winlossdraw { get; set; }
        public string modified_timestamp { get; set; }
        public string Senate_Num { get; set; }
        public string House_Num { get; set; }
        public string Impairment { get; set; }
        public string Last_day_worked { get; set; }
        public string Highest_level_education { get; set; }
        public string TYPE_AGR { get; set; }
        public string Claims_Representative { get; set; }
        public string testing { get; set; }
        public string DOCTORPT_REFERRED_TO { get; set; }
        public string Percentage_Impaired { get; set; }
        public string TTD_Start_Date_for_IRE { get; set; }
        public string Start_date500_weeks_TPD { get; set; }
        public string IA_Level { get; set; }
        public string Hearing_Level { get; set; }
        public string Date_of_IA_Decision { get; set; }
        public string Date_of_Hearing_Decision { get; set; }
        public string Ivins { get; set; }
        public string WC_Transfer { get; set; }
        public string Injuries_Alleged { get; set; }
        public string Injuries_per_NCPDecision { get; set; }
        public string Injuries_per_Doctor { get; set; }
        public string Third_Party { get; set; }
        public string Old_Fields__Dont_Use { get; set; }
        public string Attorney_Fee_ { get; set; }
        public char? Uninsured { get; set; }
        public string Suppt_Order { get; set; }
        public string Referring_Attorney { get; set; }
        public string Dispute_No { get; set; }
        public string Referring__No_Fee { get; set; }
        public string Place_of_Accident { get; set; }
        public string Time { get; set; }
        public char? Police { get; set; }
        public char? ReptInsurance_Co { get; set; }
        public string Witnesses { get; set; }
        public string Weather_Conditions { get; set; }
        public string Type_of_Collision { get; set; }
        public string LimitedFull_Tort { get; set; }
        public string Property_Damage { get; set; }
        public char? Declaration_Page { get; set; }
        public string Date_of_Call { get; set; }
        public string Call_Taken_By { get; set; }
        public string Statement_Made { get; set; }
        public char? Reported_to_DefIns_Co { get; set; }
        public string Deformity_of_Premises { get; set; }
        public string Deformity_of_Product { get; set; }
        public char? Reported_Deformity { get; set; }
        public string Reported_to_Whom { get; set; }
        public string Type_of_Hazard { get; set; }
        public string SlipTrip { get; set; }
        public string Injuries_NCP { get; set; }
        public string Injuries_Decision { get; set; }
        public char? Third_Party_Case { get; set; }
        public string Third_Party_Atty { get; set; }
        public string Concurrent_Employment { get; set; }
        public string Gross_Weekly_Pay_OT { get; set; }
        public string AWW_on_NCP { get; set; }
        public string State_injury_occurred { get; set; }
        public string VA_File_Number { get; set; }
        public string ReplyRef { get; set; }
        public string Tax_ID { get; set; }
        public string Fee_Amount { get; set; }

        public User_Case_Data() { }

        public User_Case_Data(DataRow row)
        {
            this.casenum = (int)GetValue(row, "casenum");
            this.Location = GetValue(row, "Location")?.ToString();
            this.City = GetValue(row, "City")?.ToString();
            this.County = GetValue(row, "County")?.ToString();
            this.State = GetValue(row, "State")?.ToString();
            this.PRIOR_STATUS = GetValue(row, "PRIOR_STATUS")?.ToString();
            this.DATE_DIS = GetValue(row, "DATE_DIS")?.ToString();
            this.ATTY_FEE = GetValue(row, "ATTY_FEE")?.ToString();
            this.AWW = GetValue(row, "AWW")?.ToString();
            this.PETITION_TYP = GetValue(row, "PETITION_TYP")?.ToString();
            this.START_DATE = GetValue(row, "START_DATE")?.ToString();
            this.TTD_RATE = GetValue(row, "TTD_RATE")?.ToString();
            this.CR_ATTORNEY = GetValue(row, "CR_ATTORNEY")?.ToString();
            this.JUDGE = GetValue(row, "JUDGE")?.ToString();
            this.IME_DOCTOR = GetValue(row, "IME_DOCTOR")?.ToString();
            this.DATE_INJ = GetValue(row, "DATE_INJ")?.ToString();
            this.AWWCOMP = GetValue(row, "AWWCOMP")?.ToString();
            this.CLMT_DOCTOR = GetValue(row, "CLMT_DOCTOR")?.ToString();
            this.DATE_OF_IME = GetValue(row, "DATE_OF_IME")?.ToString();
            this.RECCOMP_Dollars = GetValue(row, "RECCOMP$")?.ToString();
            this.CREDIT_ATTY = GetValue(row, "CREDIT_ATTY")?.ToString();
            this.LIT_COSTS = GetValue(row, "LIT_COSTS")?.ToString();
            this.ONSET_DATE = GetValue(row, "ONSET_DATE")?.ToString();
            this.DT_DISABLED = GetValue(row, "DT_DISABLED")?.ToString();
            this.WC = GetValue(row, "WC")?.ToString();
            this.DPA = GetValue(row, "DPA")?.ToString();
            this.LTD = GetValue(row, "LTD")?.ToString();
            this.PIA = GetValue(row, "PIA")?.ToString();
            this.DEPENDENTS = GetValue(row, "DEPENDENTS")?.ToString();
            this.TTD_OR_TPD = GetValue(row, "TTD_OR_TPD")?.ToString();
            this.CURRENT_RATE = GetValue(row, "CURRENT_RATE")?.ToString();
            this.CR_DollarsS = GetValue(row, "CR_$$S")?.ToString();
            this.DATE_DENIED = GetValue(row, "DATE_DENIED")?.ToString();
            this.MED_CR_Dollars = GetValue(row, "MED_CR_$$")?.ToString();
            this.SSD = GetValue(row, "SSD")?.ToString();
            this.TREATING_DR = GetValue(row, "TREATING_DR")?.ToString();
            this.SETTLEMENT_Dollars = GetValue(row, "SETTLEMENT_$")?.ToString();
            this.Injuries = GetValue(row, "Injuries")?.ToString();
            this.Petitions_Resolved = GetValue(row, "Petitions_Resolved")?.ToString();
            this.Petitions_Pendings = GetValue(row, "Petitions_Pendings")?.ToString();
            this.Client_Dollars_Owed_to_us = GetValue(row, "Client_$$$_Owed_to_us")?.ToString();
            this.Fee_Pkg_No = GetValue(row, "Fee_Pkg_No")?.ToString();
            this.TPD_Rate = GetValue(row, "TPD_Rate")?.ToString();
            this.WC_Rate_at_Settlement = GetValue(row, "WC_Rate_at_Settlement")?.ToString();
            this.Unemp_Comp_Dollars = GetValue(row, "Unemp_Comp_$")?.ToString();
            this.Supt_Order = GetValue(row, "Supt_Order")?.ToString();
			this.XXX_OLD_PINS_FIELDS_XXX = GetValue(row, "XXX_OLD_PINS_FIELDS_XXX")?.ToString()?.ToString()[0];
            this.STDLTD = GetValue(row, "STDLTD")?.ToString();
            this.Return_to_Work_date = ((DateTime?)GetValue(row, "Return_to_Work_date"))?.ToString("MM/dd/yyyy");
            this.VOC = GetValue(row, "VOC")?.ToString()[0];
            this.Voc_Counselor = GetValue(row, "Voc_Counselor")?.ToString();
            this.IRE = GetValue(row, "IRE")?.ToString()[0];
            this.IRE_Date = ((DateTime?)GetValue(row, "IRE_Date"))?.ToString("MM/dd/yyyy");
            this.How_Long_Emp = GetValue(row, "How_Long_Emp")?.ToString();
            this.Client_Dollars_Start_Date = ((DateTime?)GetValue(row, "Client_$$_Start_Date"))?.ToString("MM/dd/yyyy");
            this.SupersedEffectiveDate = ((DateTime?)GetValue(row, "SupersedEffectiveDate"))?.ToString("MM/dd/yyyy");
            this.Carrier_Dollars_Start_Date = ((DateTime?)GetValue(row, "Carrier_$$_Start_Date"))?.ToString("MM/dd/yyyy");
            this.CFA_Signed = GetValue(row, "CFA_Signed")?.ToString()[0];
            this.Appeal_Num = GetValue(row, "Appeal_#")?.ToString();
            this.TREATING_DOC = GetValue(row, "TREATING_DOC")?.ToString();
            this.DECISION_winlossdraw = GetValue(row, "DECISION_winlossdraw")?.ToString();
            this.modified_timestamp = ((DateTime?)GetValue(row, "modified_timestamp"))?.ToString("MM/dd/yyyy");
            this.Senate_Num = GetValue(row, "Senate_#")?.ToString();
            this.House_Num = GetValue(row, "House_#")?.ToString();
            this.Impairment = GetValue(row, "Impairment")?.ToString();
            this.Last_day_worked = ((DateTime?)GetValue(row, "Last_day_worked"))?.ToString("MM/dd/yyyy");
            this.Highest_level_education = GetValue(row, "Highest_level_education")?.ToString();
            this.TYPE_AGR = GetValue(row, "TYPE_AGR")?.ToString();
            this.Claims_Representative = GetValue(row, "Claims_Representative")?.ToString();
            this.testing = GetValue(row, "testing")?.ToString();
            this.DOCTORPT_REFERRED_TO = GetValue(row, "DOCTORPT_REFERRED_TO")?.ToString();
            this.Percentage_Impaired = GetValue(row, "Percentage_Impaired")?.ToString();
            this.TTD_Start_Date_for_IRE = ((DateTime?)GetValue(row, "TTD_Start_Date_for_IRE"))?.ToString("MM/dd/yyyy");
            this.Start_date500_weeks_TPD = ((DateTime?)GetValue(row, "Start_date500_weeks_TPD"))?.ToString("MM/dd/yyyy");
            this.IA_Level = GetValue(row, "IA_Level")?.ToString();
            this.Hearing_Level = GetValue(row, "Hearing_Level")?.ToString();
            this.Date_of_IA_Decision = ((DateTime?)GetValue(row, "Date_of_IA_Decision"))?.ToString("MM/dd/yyyy");
            this.Date_of_Hearing_Decision = ((DateTime?)GetValue(row, "Date_of_Hearing_Decision"))?.ToString("MM/dd/yyyy");
            this.Ivins = GetValue(row, "Ivins")?.ToString();
            this.WC_Transfer = GetValue(row, "WC_Transfer")?.ToString();
            this.Injuries_Alleged = GetValue(row, "Injuries_Alleged")?.ToString();
            this.Injuries_per_NCPDecision = GetValue(row, "Injuries_per_NCPDecision")?.ToString();
            this.Injuries_per_Doctor = GetValue(row, "Injuries_per_Doctor")?.ToString();
            this.Third_Party = GetValue(row, "Third_Party")?.ToString();
            this.Old_Fields__Dont_Use = GetValue(row, "Old_Fields__Dont_Use")?.ToString();
            this.Attorney_Fee_ = GetValue(row, "Attorney_Fee_")?.ToString();
            this.Uninsured = GetValue(row, "Uninsured")?.ToString()[0];
            this.Suppt_Order = GetValue(row, "Suppt_Order")?.ToString();
            this.Referring_Attorney = GetValue(row, "Referring_Attorney")?.ToString();
            this.Dispute_No = GetValue(row, "Dispute_No")?.ToString();
            this.Referring__No_Fee = GetValue(row, "Referring__No_Fee")?.ToString();
            this.Place_of_Accident = GetValue(row, "Place_of_Accident")?.ToString();
            this.Time = ((DateTime?)GetValue(row, "Time"))?.ToString("MM/dd/yyyy");
            this.Police = GetValue(row, "Police")?.ToString()[0];
            this.ReptInsurance_Co = GetValue(row, "ReptInsurance_Co")?.ToString()[0];
            this.Witnesses = GetValue(row, "Witnesses")?.ToString();
            this.Weather_Conditions = GetValue(row, "Weather_Conditions")?.ToString();
            this.Type_of_Collision = GetValue(row, "Type_of_Collision")?.ToString();
            this.LimitedFull_Tort = GetValue(row, "LimitedFull_Tort")?.ToString();
            this.Property_Damage = GetValue(row, "Property_Damage")?.ToString();
            this.Declaration_Page = GetValue(row, "Declaration_Page")?.ToString()[0];
            this.Date_of_Call = ((DateTime?)GetValue(row, "Date_of_Call"))?.ToString("MM/dd/yyyy");
            this.Call_Taken_By = GetValue(row, "Call_Taken_By")?.ToString();
            this.Statement_Made = GetValue(row, "Statement_Made")?.ToString();
            this.Reported_to_DefIns_Co = GetValue(row, "Reported_to_DefIns_Co")?.ToString()[0];
            this.Deformity_of_Premises = GetValue(row, "Deformity_of_Premises")?.ToString();
            this.Deformity_of_Product = GetValue(row, "Deformity_of_Product")?.ToString();
            this.Reported_Deformity = GetValue(row, "Reported_Deformity")?.ToString()[0];
            this.Reported_to_Whom = GetValue(row, "Reported_to_Whom")?.ToString();
            this.Type_of_Hazard = GetValue(row, "Type_of_Hazard")?.ToString();
            this.SlipTrip = GetValue(row, "SlipTrip")?.ToString();
            this.Injuries_NCP = GetValue(row, "Injuries_NCP")?.ToString();
            this.Injuries_Decision = GetValue(row, "Injuries_Decision")?.ToString();
            this.Third_Party_Case = GetValue(row, "Third_Party_Case")?.ToString()[0];
            this.Third_Party_Atty = GetValue(row, "Third_Party_Atty")?.ToString();
            this.Concurrent_Employment = GetValue(row, "Concurrent_Employment")?.ToString();
            this.Gross_Weekly_Pay_OT = GetValue(row, "Gross_Weekly_Pay_OT")?.ToString();
            this.AWW_on_NCP = GetValue(row, "AWW_on_NCP")?.ToString()?.ToString();
            this.State_injury_occurred = GetValue(row, "State_injury_occurred")?.ToString();
            this.VA_File_Number = GetValue(row, "VA_File_Number")?.ToString();
            this.ReplyRef = GetValue(row, "ReplyRef")?.ToString();
            this.Tax_ID = GetValue(row, "Tax_ID")?.ToString();
            this.Fee_Amount = GetValue(row, "Fee_Amount")?.ToString();
        }

        protected object GetValue(DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) && !(row[column] is DBNull) ? row[column] : null;
        }
    }
}