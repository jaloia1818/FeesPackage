﻿using System;
using System.Data;

namespace FeesPackage.Models
{
	public class Needles_Case
	{
		public int casenum { get; set; }
		public string alt_case_num { get; set; }
		public string date_of_incident { get; set; }
		public string date_opened { get; set; }
		public string lim_date { get; set; }
		public char lim_stat { get; set; }
		public string matcode { get; set; }
		public string staff_1 { get; set; }
		public string staff_2 { get; set; }
		public string staff_3 { get; set; }
		public string staff_4 { get; set; }
		public string close_date { get; set; }
		public string case_date_1 { get; set; }
		public string case_date_2 { get; set; }
		public string case_date_3 { get; set; }
		public Nullable<DateTime> case_date_5 { get; set; }
		public Nullable<DateTime> case_date_4 { get; set; }
		public int referred_link { get; set; }
		public int referred_link_location { get; set; }
		public string _class { get; set; }
		public int group_id { get; set; }
		public int group_id_location { get; set; }
		public string synopsis { get; set; }
		public string ressign_date { get; set; }
		public string case_date_6 { get; set; }
		public string litigation_title { get; set; }
		public int court_link { get; set; }
		public int court_link_location { get; set; }
		public int judge_link { get; set; }
		public int judge_link_location { get; set; }
		public string docket { get; set; }
		public char dormant { get; set; }
		public string special_note { get; set; }
		public string staff_5 { get; set; }
		public string staff_6 { get; set; }
		public string staff_7 { get; set; }
		public string staff_8 { get; set; }
		public string staff_9 { get; set; }
		public string staff_10 { get; set; }
		public string case_date_7 { get; set; }
		public string case_date_8 { get; set; }
		public string case_date_9 { get; set; }
		public char open_status { get; set; }
		public string case_title { get; set; }
		public string alt_case_num_2 { get; set; }
		public int referred_to_id { get; set; }
		public int referred_to_loc { get; set; }
		public string intake_date { get; set; }
		public TimeSpan intake_time { get; set; }
		public string intake_staff { get; set; }
		public string import_date { get; set; }
		public string doc_default_path { get; set; }
		public int bill_to_id { get; set; }
		public int bill_to_location { get; set; }
		public string last_modified { get; set; }
		public string date_created { get; set; }
		public string staff_created { get; set; }
		public string date_modified { get; set; }
		public string staff_modified { get; set; }

		public Needles_Case() { }

		public Needles_Case(DataRow row)
		{
			if (!(row["casenum"] is DBNull))
			{
				this.casenum = (int)row["casenum"];
			}
			if (!(row["alt_case_num"] is DBNull))
			{
				this.alt_case_num = (string)row["alt_case_num"];
			}
			if (!(row["date_of_incident"] is DBNull))
			{
				this.date_of_incident = ((DateTime)row["date_of_incident"]).ToString("MM/dd/yyyy");
			}
			if (!(row["date_opened"] is DBNull))
			{
				this.date_opened = ((DateTime)row["date_opened"]).ToString("MM/dd/yyyy");
			}
			if (!(row["lim_date"] is DBNull))
			{
				this.lim_date = ((DateTime)row["lim_date"]).ToString("MM/dd/yyyy");
			}
			else
            {
				this.lim_date = "Not Set";
            }
			if (!(row["lim_stat"] is DBNull))
			{
				this.lim_stat = ((string)row["lim_stat"])[0];
			}
			if (!(row["matcode"] is DBNull))
			{
				this.matcode = (string)row["matcode"];
			}
			if (!(row["staff_1"] is DBNull))
			{
				this.staff_1 = (string)row["staff_1"];
			}
			if (!(row["staff_2"] is DBNull))
			{
				this.staff_2 = (string)row["staff_2"];
			}
			if (!(row["staff_3"] is DBNull))
			{
				this.staff_3 = (string)row["staff_3"];
			}
			if (!(row["staff_4"] is DBNull))
			{
				this.staff_4 = (string)row["staff_4"];
			}
			if (!(row["close_date"] is DBNull))
			{
				this.close_date = ((DateTime)row["close_date"]).ToString("MM/dd/yyyy");
			}
			if (!(row["case_date_1"] is DBNull))
			{
				this.case_date_1 = ((DateTime)row["case_date_1"]).ToString("MM/dd/yyyy");
			}
			if (!(row["case_date_2"] is DBNull))
			{
				this.case_date_2 = ((DateTime)row["case_date_2"]).ToString("MM/dd/yyyy");
			}
			if (!(row["case_date_3"] is DBNull))
			{
				this.case_date_3 = ((DateTime)row["case_date_3"]).ToString("MM/dd/yyyy");
			}
			if (!(row["case_date_5"] is DBNull))
			{
				this.case_date_5 = (DateTime)row["case_date_5"];
			}
			if (!(row["case_date_4"] is DBNull))
			{
				this.case_date_4 = (DateTime)row["case_date_4"];
			}
			if (!(row["referred_link"] is DBNull))
			{
				this.referred_link = (int)row["referred_link"];
			}
			if (!(row["referred_link_location"] is DBNull))
			{
				this.referred_link_location = (int)row["referred_link_location"];
			}
			if (!(row["class"] is DBNull))
			{
				this._class = (string)row["class"];
			}
			if (!(row["group_id"] is DBNull))
			{
				this.group_id = (int)row["group_id"];
			}
			if (!(row["group_id_location"] is DBNull))
			{
				this.group_id_location = (int)row["group_id_location"];
			}
			if (!(row["synopsis"] is DBNull))
			{
				this.synopsis = (string)row["synopsis"];
			}
			if (!(row["ressign_date"] is DBNull))
			{
				this.ressign_date = ((DateTime)row["ressign_date"]).ToString("MM/dd/yyyy");
			}
			if (!(row["case_date_6"] is DBNull))
			{
				this.case_date_6 = ((DateTime)row["case_date_6"]).ToString("MM/dd/yyyy");
			}
			if (!(row["litigation_title"] is DBNull))
			{
				this.litigation_title = (string)row["litigation_title"];
			}
			if (!(row["court_link"] is DBNull))
			{
				this.court_link = (int)row["court_link"];
			}
			if (!(row["court_link_location"] is DBNull))
			{
				this.court_link_location = (int)row["court_link_location"];
			}
			if (!(row["judge_link"] is DBNull))
			{
				this.judge_link = (int)row["judge_link"];
			}
			if (!(row["judge_link_location"] is DBNull))
			{
				this.judge_link_location = (int)row["judge_link_location"];
			}
			if (!(row["docket"] is DBNull))
			{
				this.docket = (string)row["docket"];
			}
			if (!(row["dormant"] is DBNull))
			{
				this.dormant = ((string)row["dormant"])[0];
			}
			if (!(row["special_note"] is DBNull))
			{
				this.special_note = (string)row["special_note"];
			}
			if (!(row["staff_5"] is DBNull))
			{
				this.staff_5 = (string)row["staff_5"];
			}
			if (!(row["staff_6"] is DBNull))
			{
				this.staff_6 = (string)row["staff_6"];
			}
			if (!(row["staff_7"] is DBNull))
			{
				this.staff_7 = (string)row["staff_7"];
			}
			if (!(row["staff_8"] is DBNull))
			{
				this.staff_8 = (string)row["staff_8"];
			}
			if (!(row["staff_9"] is DBNull))
			{
				this.staff_9 = (string)row["staff_9"];
			}
			if (!(row["staff_10"] is DBNull))
			{
				this.staff_10 = (string)row["staff_10"];
			}
			if (!(row["case_date_7"] is DBNull))
			{
				this.case_date_7 = ((DateTime)row["case_date_7"]).ToString("MM/dd/yyyy");
			}
			if (!(row["case_date_8"] is DBNull))
			{
				this.case_date_8 = ((DateTime)row["case_date_8"]).ToString("MM/dd/yyyy");
			}
			if (!(row["case_date_9"] is DBNull))
			{
				this.case_date_9 = ((DateTime)row["case_date_9"]).ToString("MM/dd/yyyy");
			}
			if (!(row["open_status"] is DBNull))
			{
				this.open_status = ((string)row["open_status"])[0];
			}
			if (!(row["case_title"] is DBNull))
			{
				this.case_title = (string)row["case_title"];
			}
			if (!(row["alt_case_num_2"] is DBNull))
			{
				this.alt_case_num_2 = (string)row["alt_case_num_2"];
			}
			if (!(row["referred_to_id"] is DBNull))
			{
				this.referred_to_id = (int)row["referred_to_id"];
			}
			if (!(row["referred_to_loc"] is DBNull))
			{
				this.referred_to_loc = (int)row["referred_to_loc"];
			}
			if (!(row["intake_date"] is DBNull))
			{
				this.intake_date = ((DateTime)row["intake_date"]).ToString("MM/dd/yyyy");
			}
			if (!(row["intake_time"] is DBNull))
			{
				this.intake_time = (TimeSpan)row["intake_time"];
			}
			if (!(row["intake_staff"] is DBNull))
			{
				this.intake_staff = (string)row["intake_staff"];
			}
			if (!(row["import_date"] is DBNull))
			{
				this.import_date = ((DateTime)row["import_date"]).ToString("MM/dd/yyyy");
			}
			if (!(row["doc_default_path"] is DBNull))
			{
				this.doc_default_path = (string)row["doc_default_path"];
			}
			if (!(row["bill_to_id"] is DBNull))
			{
				this.bill_to_id = (int)row["bill_to_id"];
			}
			if (!(row["bill_to_location"] is DBNull))
			{
				this.bill_to_location = (int)row["bill_to_location"];
			}
			if (!(row["last_modified"] is DBNull))
			{
				this.last_modified = ((DateTime)row["last_modified"]).ToString("MM/dd/yyyy");
			}
			if (!(row["date_created"] is DBNull))
			{
				this.date_created = ((DateTime)row["date_created"]).ToString("MM/dd/yyyy");
			}
			if (!(row["staff_created"] is DBNull))
			{
				this.staff_created = (string)row["staff_created"];
			}
			if (!(row["date_modified"] is DBNull))
			{
				this.date_modified = ((DateTime)row["date_modified"]).ToString("MM/dd/yyyy");
			}
			if (!(row["staff_modified"] is DBNull))
			{
				this.staff_modified = (string)row["staff_modified"];
			}
		}
	}
}
