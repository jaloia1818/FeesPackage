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
        public char XXX_OLD_PINS_FIELDS_XXX { get; set; }
        public string STDLTD { get; set; }
        public string Return_to_Work_date { get; set; }
        public char VOC { get; set; }
        public string Voc_Counselor { get; set; }
        public char IRE { get; set; }
        public string IRE_Date { get; set; }
        public string How_Long_Emp { get; set; }
        public string Client_Dollars_Start_Date { get; set; }
        public string SupersedEffectiveDate { get; set; }
        public string Carrier_Dollars_Start_Date { get; set; }
        public char CFA_Signed { get; set; }
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
        public char Uninsured { get; set; }
        public string Suppt_Order { get; set; }
        public string Referring_Attorney { get; set; }
        public string Dispute_No { get; set; }
        public string Referring__No_Fee { get; set; }
        public string Place_of_Accident { get; set; }
        public string Time { get; set; }
        public char Police { get; set; }
        public char ReptInsurance_Co { get; set; }
        public string Witnesses { get; set; }
        public string Weather_Conditions { get; set; }
        public string Type_of_Collision { get; set; }
        public string LimitedFull_Tort { get; set; }
        public string Property_Damage { get; set; }
        public char Declaration_Page { get; set; }
        public string Date_of_Call { get; set; }
        public string Call_Taken_By { get; set; }
        public string Statement_Made { get; set; }
        public char Reported_to_DefIns_Co { get; set; }
        public string Deformity_of_Premises { get; set; }
        public string Deformity_of_Product { get; set; }
        public char Reported_Deformity { get; set; }
        public string Reported_to_Whom { get; set; }
        public string Type_of_Hazard { get; set; }
        public string SlipTrip { get; set; }
        public string Injuries_NCP { get; set; }
        public string Injuries_Decision { get; set; }
        public char Third_Party_Case { get; set; }
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
            if (!(row["casenum"] is DBNull))
            {
                this.casenum = (int)row["casenum"];
            }
            if (!(row["Location"] is DBNull))
            {
                this.Location = (string)row["Location"];
            }
            if (!(row["City"] is DBNull))
            {
                this.City = (string)row["City"];
            }
            if (!(row["County"] is DBNull))
            {
                this.County = (string)row["County"];
            }
            if (!(row["State"] is DBNull))
            {
                this.State = (string)row["State"];
            }
            if (!(row["PRIOR_STATUS"] is DBNull))
            {
                this.PRIOR_STATUS = (string)row["PRIOR_STATUS"];
            }
            if (!(row["DATE_DIS"] is DBNull))
            {
                this.DATE_DIS = (string)row["DATE_DIS"];
            }
            if (!(row["ATTY_FEE"] is DBNull))
            {
                this.ATTY_FEE = (string)row["ATTY_FEE"];
            }
            if (!(row["AWW"] is DBNull))
            {
                this.AWW = (string)row["AWW"];
            }
            if (!(row["PETITION_TYP"] is DBNull))
            {
                this.PETITION_TYP = (string)row["PETITION_TYP"];
            }
            if (!(row["START_DATE"] is DBNull))
            {
                this.START_DATE = (string)row["START_DATE"];
            }
            if (!(row["TTD_RATE"] is DBNull))
            {
                this.TTD_RATE = (string)row["TTD_RATE"];
            }
            if (!(row["CR_ATTORNEY"] is DBNull))
            {
                this.CR_ATTORNEY = (string)row["CR_ATTORNEY"];
            }
            if (!(row["JUDGE"] is DBNull))
            {
                this.JUDGE = (string)row["JUDGE"];
            }
            if (!(row["IME_DOCTOR"] is DBNull))
            {
                this.IME_DOCTOR = (string)row["IME_DOCTOR"];
            }
            if (!(row["DATE_INJ"] is DBNull))
            {
                this.DATE_INJ = (string)row["DATE_INJ"];
            }
            if (!(row["AWWCOMP"] is DBNull))
            {
                this.AWWCOMP = (string)row["AWWCOMP"];
            }
            if (!(row["CLMT_DOCTOR"] is DBNull))
            {
                this.CLMT_DOCTOR = (string)row["CLMT_DOCTOR"];
            }
            if (!(row["DATE_OF_IME"] is DBNull))
            {
                this.DATE_OF_IME = (string)row["DATE_OF_IME"];
            }
            if (!(row["RECCOMP$"] is DBNull))
            {
                this.RECCOMP_Dollars = (string)row["RECCOMP$"];
            }
            if (!(row["CREDIT_ATTY"] is DBNull))
            {
                this.CREDIT_ATTY = (string)row["CREDIT_ATTY"];
            }
            if (!(row["LIT_COSTS"] is DBNull))
            {
                this.LIT_COSTS = (string)row["LIT_COSTS"];
            }
            if (!(row["ONSET_DATE"] is DBNull))
            {
                this.ONSET_DATE = (string)row["ONSET_DATE"];
            }
            if (!(row["DT_DISABLED"] is DBNull))
            {
                this.DT_DISABLED = (string)row["DT_DISABLED"];
            }
            if (!(row["WC"] is DBNull))
            {
                this.WC = (string)row["WC"];
            }
            if (!(row["DPA"] is DBNull))
            {
                this.DPA = (string)row["DPA"];
            }
            if (!(row["LTD"] is DBNull))
            {
                this.LTD = (string)row["LTD"];
            }
            if (!(row["PIA"] is DBNull))
            {
                this.PIA = (string)row["PIA"];
            }
            if (!(row["DEPENDENTS"] is DBNull))
            {
                this.DEPENDENTS = (string)row["DEPENDENTS"];
            }
            if (!(row["TTD_OR_TPD"] is DBNull))
            {
                this.TTD_OR_TPD = (string)row["TTD_OR_TPD"];
            }
            if (!(row["CURRENT_RATE"] is DBNull))
            {
                this.CURRENT_RATE = (string)row["CURRENT_RATE"];
            }
            if (!(row["CR_$$S"] is DBNull))
            {
                this.CR_DollarsS = (string)row["CR_$$S"];
            }
            if (!(row["DATE_DENIED"] is DBNull))
            {
                this.DATE_DENIED = (string)row["DATE_DENIED"];
            }
            if (!(row["MED_CR_$$"] is DBNull))
            {
                this.MED_CR_Dollars = (string)row["MED_CR_$$"];
            }
            if (!(row["SSD"] is DBNull))
            {
                this.SSD = (string)row["SSD"];
            }
            if (!(row["TREATING_DR"] is DBNull))
            {
                this.TREATING_DR = (string)row["TREATING_DR"];
            }
            if (!(row["SETTLEMENT_$"] is DBNull))
            {
                this.SETTLEMENT_Dollars = (string)row["SETTLEMENT_$"];
            }
            if (!(row["Injuries"] is DBNull))
            {
                this.Injuries = (string)row["Injuries"];
            }
            if (!(row["Petitions_Resolved"] is DBNull))
            {
                this.Petitions_Resolved = (string)row["Petitions_Resolved"];
            }
            if (!(row["Petitions_Pendings"] is DBNull))
            {
                this.Petitions_Pendings = (string)row["Petitions_Pendings"];
            }
            if (!(row["Client_$$$_Owed_to_us"] is DBNull))
            {
                this.Client_Dollars_Owed_to_us = (string)row["Client_$$$_Owed_to_us"];
            }
            if (!(row["Fee_Pkg_No"] is DBNull))
            {
                this.Fee_Pkg_No = (string)row["Fee_Pkg_No"];
            }
            if (!(row["TPD_Rate"] is DBNull))
            {
                this.TPD_Rate = (string)row["TPD_Rate"];
            }
            if (!(row["WC_Rate_at_Settlement"] is DBNull))
            {
                this.WC_Rate_at_Settlement = (string)row["WC_Rate_at_Settlement"];
            }
            if (!(row["Unemp_Comp_$"] is DBNull))
            {
                this.Unemp_Comp_Dollars = (string)row["Unemp_Comp_$"];
            }
            if (!(row["Supt_Order"] is DBNull))
            {
                this.Supt_Order = (string)row["Supt_Order"];
            }
            if (!(row["XXX_OLD_PINS_FIELDS_XXX"] is DBNull))
            {
			    this.XXX_OLD_PINS_FIELDS_XXX = ((string)row["XXX_OLD_PINS_FIELDS_XXX"])[0];
            }
            if (!(row["STDLTD"] is DBNull))
            {
                this.STDLTD = (string)row["STDLTD"];
            }
            if (!(row["Return_to_Work_date"] is DBNull))
            {
                this.Return_to_Work_date = ((DateTime)row["Return_to_Work_date"]).ToString("MM/dd/yyyy");
            }
            if (!(row["VOC"] is DBNull))
            {
                this.VOC = ((string)row["VOC"])[0];
            }
            if (!(row["Voc_Counselor"] is DBNull))
            {
                this.Voc_Counselor = (string)row["Voc_Counselor"];
            }
            if (!(row["IRE"] is DBNull))
            {
                this.IRE = ((string)row["IRE"])[0];
            }
            if (!(row["IRE_Date"] is DBNull))
            {
                this.IRE_Date = ((DateTime)row["IRE_Date"]).ToString("MM/dd/yyyy");
            }
            if (!(row["How_Long_Emp"] is DBNull))
            {
                this.How_Long_Emp = (string)row["How_Long_Emp"];
            }
            if (!(row["Client_$$_Start_Date"] is DBNull))
            {
                this.Client_Dollars_Start_Date = ((DateTime)row["Client_$$_Start_Date"]).ToString("MM/dd/yyyy");
            }
            if (!(row["SupersedEffectiveDate"] is DBNull))
            {
                this.SupersedEffectiveDate = ((DateTime)row["SupersedEffectiveDate"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Carrier_$$_Start_Date"] is DBNull))
            {
                this.Carrier_Dollars_Start_Date = ((DateTime)row["Carrier_$$_Start_Date"]).ToString("MM/dd/yyyy");
            }
            if (!(row["CFA_Signed"] is DBNull))
            {
                this.CFA_Signed = ((string)row["CFA_Signed"])[0];
            }
            if (!(row["Appeal_#"] is DBNull))
            {
                this.Appeal_Num = (string)row["Appeal_#"];
            }
            if (!(row["TREATING_DOC"] is DBNull))
            {
                this.TREATING_DOC = (string)row["TREATING_DOC"];
            }
            if (!(row["DECISION_winlossdraw"] is DBNull))
            {
                this.DECISION_winlossdraw = (string)row["DECISION_winlossdraw"];
            }
            if (!(row["modified_timestamp"] is DBNull))
            {
                this.modified_timestamp = ((DateTime)row["modified_timestamp"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Senate_#"] is DBNull))
            {
                this.Senate_Num = row["Senate_#"].ToString();
            }
            if (!(row["House_#"] is DBNull))
            {
                this.House_Num = row["House_#"].ToString();
            }
            if (!(row["Impairment"] is DBNull))
            {
                this.Impairment = (string)row["Impairment"];
            }
            if (!(row["Last_day_worked"] is DBNull))
            {
                this.Last_day_worked = ((DateTime)row["Last_day_worked"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Highest_level_education"] is DBNull))
            {
                this.Highest_level_education = (string)row["Highest_level_education"];
            }
            if (!(row["TYPE_AGR"] is DBNull))
            {
                this.TYPE_AGR = (string)row["TYPE_AGR"];
            }
            if (!(row["Claims_Representative"] is DBNull))
            {
                this.Claims_Representative = (string)row["Claims_Representative"];
            }
            if (!(row["testing"] is DBNull))
            {
                this.testing = (string)row["testing"];
            }
            if (!(row["DOCTORPT_REFERRED_TO"] is DBNull))
            {
                this.DOCTORPT_REFERRED_TO = (string)row["DOCTORPT_REFERRED_TO"];
            }
            if (!(row["Percentage_Impaired"] is DBNull))
            {
                this.Percentage_Impaired = (string)row["Percentage_Impaired"];
            }
            if (!(row["TTD_Start_Date_for_IRE"] is DBNull))
            {
                this.TTD_Start_Date_for_IRE = ((DateTime)row["TTD_Start_Date_for_IRE"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Start_date500_weeks_TPD"] is DBNull))
            {
                this.Start_date500_weeks_TPD = ((DateTime)row["Start_date500_weeks_TPD"]).ToString("MM/dd/yyyy");
            }
            if (!(row["IA_Level"] is DBNull))
            {
                this.IA_Level = (string)row["IA_Level"];
            }
            if (!(row["Hearing_Level"] is DBNull))
            {
                this.Hearing_Level = (string)row["Hearing_Level"];
            }
            if (!(row["Date_of_IA_Decision"] is DBNull))
            {
                this.Date_of_IA_Decision = ((DateTime)row["Date_of_IA_Decision"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Date_of_Hearing_Decision"] is DBNull))
            {
                this.Date_of_Hearing_Decision = ((DateTime)row["Date_of_Hearing_Decision"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Ivins"] is DBNull))
            {
                this.Ivins = (string)row["Ivins"];
            }
            if (!(row["WC_Transfer"] is DBNull))
            {
                this.WC_Transfer = (string)row["WC_Transfer"];
            }
            if (!(row["Injuries_Alleged"] is DBNull))
            {
                this.Injuries_Alleged = (string)row["Injuries_Alleged"];
            }
            if (!(row["Injuries_per_NCPDecision"] is DBNull))
            {
                this.Injuries_per_NCPDecision = (string)row["Injuries_per_NCPDecision"];
            }
            if (!(row["Injuries_per_Doctor"] is DBNull))
            {
                this.Injuries_per_Doctor = (string)row["Injuries_per_Doctor"];
            }
            if (!(row["Third_Party"] is DBNull))
            {
                this.Third_Party = (string)row["Third_Party"];
            }
            if (!(row["Old_Fields__Dont_Use"] is DBNull))
            {
                this.Old_Fields__Dont_Use = (string)row["Old_Fields__Dont_Use"];
            }
            if (!(row["Attorney_Fee_"] is DBNull))
            {
                this.Attorney_Fee_ = row["Attorney_Fee_"].ToString();
            }
            if (!(row["Uninsured"] is DBNull))
            {
                this.Uninsured = ((string)row["Uninsured"])[0];
            }
            if (!(row["Suppt_Order"] is DBNull))
            {
                this.Suppt_Order = (string)row["Suppt_Order"];
            }
            if (!(row["Referring_Attorney"] is DBNull))
            {
                this.Referring_Attorney = (string)row["Referring_Attorney"];
            }
            if (!(row["Dispute_No"] is DBNull))
            {
                this.Dispute_No = (string)row["Dispute_No"];
            }
            if (!(row["Referring__No_Fee"] is DBNull))
            {
                this.Referring__No_Fee = (string)row["Referring__No_Fee"];
            }
            if (!(row["Place_of_Accident"] is DBNull))
            {
                this.Place_of_Accident = (string)row["Place_of_Accident"];
            }
            if (!(row["Time"] is DBNull))
            {
                this.Time = ((DateTime)row["Time"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Police"] is DBNull))
            {
                this.Police = ((string)row["Police"])[0];
            }
            if (!(row["ReptInsurance_Co"] is DBNull))
            {
                this.ReptInsurance_Co = ((string)row["ReptInsurance_Co"])[0];
            }
            if (!(row["Witnesses"] is DBNull))
            {
                this.Witnesses = (string)row["Witnesses"];
            }
            if (!(row["Weather_Conditions"] is DBNull))
            {
                this.Weather_Conditions = (string)row["Weather_Conditions"];
            }
            if (!(row["Type_of_Collision"] is DBNull))
            {
                this.Type_of_Collision = (string)row["Type_of_Collision"];
            }
            if (!(row["LimitedFull_Tort"] is DBNull))
            {
                this.LimitedFull_Tort = (string)row["LimitedFull_Tort"];
            }
            if (!(row["Property_Damage"] is DBNull))
            {
                this.Property_Damage = (string)row["Property_Damage"];
            }
            if (!(row["Declaration_Page"] is DBNull))
            {
                this.Declaration_Page = ((string)row["Declaration_Page"])[0];
            }
            if (!(row["Date_of_Call"] is DBNull))
            {
                this.Date_of_Call = ((DateTime)row["Date_of_Call"]).ToString("MM/dd/yyyy");
            }
            if (!(row["Call_Taken_By"] is DBNull))
            {
                this.Call_Taken_By = (string)row["Call_Taken_By"];
            }
            if (!(row["Statement_Made"] is DBNull))
            {
                this.Statement_Made = (string)row["Statement_Made"];
            }
            if (!(row["Reported_to_DefIns_Co"] is DBNull))
            {
                this.Reported_to_DefIns_Co = ((string)row["Reported_to_DefIns_Co"])[0];
            }
            if (!(row["Deformity_of_Premises"] is DBNull))
            {
                this.Deformity_of_Premises = (string)row["Deformity_of_Premises"];
            }
            if (!(row["Deformity_of_Product"] is DBNull))
            {
                this.Deformity_of_Product = (string)row["Deformity_of_Product"];
            }
            if (!(row["Reported_Deformity"] is DBNull))
            {
                this.Reported_Deformity = ((string)row["Reported_Deformity"])[0];
            }
            if (!(row["Reported_to_Whom"] is DBNull))
            {
                this.Reported_to_Whom = (string)row["Reported_to_Whom"];
            }
            if (!(row["Type_of_Hazard"] is DBNull))
            {
                this.Type_of_Hazard = (string)row["Type_of_Hazard"];
            }
            if (!(row["SlipTrip"] is DBNull))
            {
                this.SlipTrip = (string)row["SlipTrip"];
            }
            if (!(row["Injuries_NCP"] is DBNull))
            {
                this.Injuries_NCP = (string)row["Injuries_NCP"];
            }
            if (!(row["Injuries_Decision"] is DBNull))
            {
                this.Injuries_Decision = (string)row["Injuries_Decision"];
            }
            if (!(row["Third_Party_Case"] is DBNull))
            {
                this.Third_Party_Case = ((string)row["Third_Party_Case"])[0];
            }
            if (!(row["Third_Party_Atty"] is DBNull))
            {
                this.Third_Party_Atty = (string)row["Third_Party_Atty"];
            }
            if (!(row["Concurrent_Employment"] is DBNull))
            {
                this.Concurrent_Employment = (string)row["Concurrent_Employment"];
            }
            if (!(row["Gross_Weekly_Pay_OT"] is DBNull))
            {
                this.Gross_Weekly_Pay_OT = (string)row["Gross_Weekly_Pay_OT"];
            }
            if (!(row["AWW_on_NCP"] is DBNull))
            {
                this.AWW_on_NCP = row["AWW_on_NCP"].ToString();
            }
            if (!(row["State_injury_occurred"] is DBNull))
            {
                this.State_injury_occurred = (string)row["State_injury_occurred"];
            }
            if (!(row["VA_File_Number"] is DBNull))
            {
                this.VA_File_Number = (string)row["VA_File_Number"];
            }
            if (!(row["ReplyRef"] is DBNull))
            {
                this.ReplyRef = (string)row["ReplyRef"];
            }
            if (!(row["Tax_ID"] is DBNull))
            {
                this.Tax_ID = (string)row["Tax_ID"];
            }
            if (!(row["Fee_Amount"] is DBNull))
            {
                this.Fee_Amount = (string)row["Fee_Amount"];
            }
        }
    }
}