﻿using System;
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
        public DateTime Return_to_Work_date { get; set; }
        public char VOC { get; set; }
        public string Voc_Counselor { get; set; }
        public char IRE { get; set; }
        public DateTime IRE_Date { get; set; }
        public string How_Long_Emp { get; set; }
        public DateTime Client_Dollars_Start_Date { get; set; }
        public DateTime SupersedEffectiveDate { get; set; }
        public DateTime Carrier_Dollars_Start_Date { get; set; }
        public char CFA_Signed { get; set; }
        public string Appeal_Num { get; set; }
        public string TREATING_DOC { get; set; }
        public string DECISION_winlossdraw { get; set; }
        public DateTime modified_timestamp { get; set; }
        public Decimal Senate_Num { get; set; }
        public Decimal House_Num { get; set; }
        public string Impairment { get; set; }
        public DateTime Last_day_worked { get; set; }
        public string Highest_level_education { get; set; }
        public string TYPE_AGR { get; set; }
        public string Claims_Representative { get; set; }
        public string testing { get; set; }
        public string DOCTORPT_REFERRED_TO { get; set; }
        public string Percentage_Impaired { get; set; }
        public DateTime TTD_Start_Date_for_IRE { get; set; }
        public DateTime Start_date500_weeks_TPD { get; set; }
        public string IA_Level { get; set; }
        public string Hearing_Level { get; set; }
        public DateTime Date_of_IA_Decision { get; set; }
        public DateTime Date_of_Hearing_Decision { get; set; }
        public string Ivins { get; set; }
        public string WC_Transfer { get; set; }
        public string Injuries_Alleged { get; set; }
        public string Injuries_per_NCPDecision { get; set; }
        public string Injuries_per_Doctor { get; set; }
        public string Third_Party { get; set; }
        public string Old_Fields__Dont_Use { get; set; }
        public Decimal Attorney_Fee_ { get; set; }
        public char Uninsured { get; set; }
        public string Suppt_Order { get; set; }
        public string Referring_Attorney { get; set; }
        public string Dispute_No { get; set; }
        public string Referring__No_Fee { get; set; }
        public string Place_of_Accident { get; set; }
        public DateTime Time { get; set; }
        public char Police { get; set; }
        public char ReptInsurance_Co { get; set; }
        public string Witnesses { get; set; }
        public string Weather_Conditions { get; set; }
        public string Type_of_Collision { get; set; }
        public string LimitedFull_Tort { get; set; }
        public string Property_Damage { get; set; }
        public char Declaration_Page { get; set; }
        public DateTime Date_of_Call { get; set; }
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
        public Decimal AWW_on_NCP { get; set; }
        public string State_injury_occurred { get; set; }
        public string VA_File_Number { get; set; }
        public string ReplyRef { get; set; }
        public string Tax_ID { get; set; }
        public string Fee_Amount { get; set; }

        public User_Case_Data() { }

        public User_Case_Data(DataRow row)
        {
            this.casenum = (int)row["casenum"];
            /*this.Name = (string)row["name"];
			if (!(row["modified"] is DBNull))
			{
				this.Modified = (DateTime)row["modified"];
			}*/
        }
    }
}