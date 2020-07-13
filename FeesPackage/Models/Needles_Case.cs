using System;
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
		public char? lim_stat { get; set; }
		public string matcode { get; set; }
		public string staff_1 { get; set; }
		public string staff_2 { get; set; }
		public string staff_3 { get; set; }
		public string staff_4 { get; set; }
		public string close_date { get; set; }
		public string case_date_1 { get; set; }
		public string case_date_2 { get; set; }
		public string case_date_3 { get; set; }
		public string case_date_5 { get; set; }
		public string case_date_4 { get; set; }
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
		public char? dormant { get; set; }
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
		public char? open_status { get; set; }
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
			this.casenum = (int)GetValue(row, "casenum");
			this.alt_case_num = GetValue(row, "alt_case_num")?.ToString();
			this.date_of_incident = ((DateTime?)GetValue(row, "date_of_incident"))?.ToString("MM/dd/yyyy");
			this.date_opened = ((DateTime?)GetValue(row, "date_opened"))?.ToString("MM/dd/yyyy");
			this.lim_date = ((DateTime?)GetValue(row, "lim_date"))?.ToString("MM/dd/yyyy");
			this.lim_stat = GetFirstChar(GetValue(row, "lim_stat")?.ToString());
			this.matcode = GetValue(row, "matcode")?.ToString();
			this.staff_1 = GetValue(row, "staff_1")?.ToString();
			this.staff_2 = GetValue(row, "staff_2")?.ToString();
			this.staff_3 = GetValue(row, "staff_3")?.ToString();
			this.staff_4 = GetValue(row, "staff_4")?.ToString();
			this.close_date = ((DateTime?)GetValue(row, "close_date"))?.ToString("MM/dd/yyyy");
			this.case_date_1 = ((DateTime?)GetValue(row, "case_date_1"))?.ToString("MM/dd/yyyy");
			this.case_date_2 = ((DateTime?)GetValue(row, "case_date_2"))?.ToString("MM/dd/yyyy");
			this.case_date_3 = ((DateTime?)GetValue(row, "case_date_3"))?.ToString("MM/dd/yyyy");
			this.case_date_5 = ((DateTime?)GetValue(row, "case_date_5"))?.ToString("MM/dd/yyyy");
			this.case_date_4 = ((DateTime?)GetValue(row, "case_date_4"))?.ToString("MM/dd/yyyy");
			this.referred_link = (int)GetValue(row, "referred_link");
			this.referred_link_location = (int)GetValue(row, "referred_link_location");
			this._class = GetValue(row, "class")?.ToString();
			this.group_id = (int)GetValue(row, "group_id");
			this.group_id_location = (int)GetValue(row, "group_id_location");
			this.synopsis = GetValue(row, "synopsis")?.ToString();
			this.ressign_date = ((DateTime?)GetValue(row, "ressign_date"))?.ToString("MM/dd/yyyy");
			this.case_date_6 = ((DateTime?)GetValue(row, "case_date_6"))?.ToString("MM/dd/yyyy");
			this.litigation_title = GetValue(row, "litigation_title")?.ToString();
			this.court_link = (int)GetValue(row, "court_link");
			this.court_link_location = (int)GetValue(row, "court_link_location");
			this.judge_link = (int)GetValue(row, "judge_link");
			this.judge_link_location = (int)GetValue(row, "judge_link_location");
			this.docket = GetValue(row, "docket")?.ToString();
			this.dormant = GetFirstChar(GetValue(row, "dormant")?.ToString());
			this.special_note = GetValue(row, "special_note")?.ToString();
			this.staff_5 = GetValue(row, "staff_5")?.ToString();
			this.staff_6 = GetValue(row, "staff_6")?.ToString();
			this.staff_7 = GetValue(row, "staff_7")?.ToString();
			this.staff_8 = GetValue(row, "staff_8")?.ToString();
			this.staff_9 = GetValue(row, "staff_9")?.ToString();
			this.staff_10 = GetValue(row, "staff_10")?.ToString();
			this.case_date_7 = ((DateTime?)GetValue(row, "case_date_7"))?.ToString("MM/dd/yyyy");
			this.case_date_8 = ((DateTime?)GetValue(row, "case_date_8"))?.ToString("MM/dd/yyyy");
			this.case_date_9 = ((DateTime?)GetValue(row, "case_date_9"))?.ToString("MM/dd/yyyy");
			this.open_status = GetFirstChar(GetValue(row, "open_status")?.ToString());
			this.case_title = GetValue(row, "case_title")?.ToString();
			this.alt_case_num_2 = GetValue(row, "alt_case_num_2")?.ToString();
			this.referred_to_id = (int)GetValue(row, "referred_to_id");
			this.referred_to_loc = (int)GetValue(row, "referred_to_loc");
			this.intake_date = ((DateTime?)GetValue(row, "intake_date"))?.ToString("MM/dd/yyyy");
			this.intake_time = (TimeSpan)GetValue(row, "intake_time");
			this.intake_staff = GetValue(row, "intake_staff")?.ToString();
			this.import_date = ((DateTime?)GetValue(row, "import_date"))?.ToString("MM/dd/yyyy");
			this.doc_default_path = GetValue(row, "doc_default_path")?.ToString();
			this.bill_to_id = (int)GetValue(row, "bill_to_id");
			this.bill_to_location = (int)GetValue(row, "bill_to_location");
			this.last_modified = ((DateTime?)GetValue(row, "last_modified"))?.ToString("MM/dd/yyyy");
			this.date_created = ((DateTime?)GetValue(row, "date_created"))?.ToString("MM/dd/yyyy");
			this.staff_created = GetValue(row, "staff_created")?.ToString();
			this.date_modified = ((DateTime?)GetValue(row, "date_modified"))?.ToString("MM/dd/yyyy");
			this.staff_modified = GetValue(row, "staff_modified")?.ToString();
		}

		protected object GetValue(DataRow row, string column)
		{
			return row.Table.Columns.Contains(column) && !(row[column] is DBNull) ? row[column] : null;
		}

		protected char GetFirstChar(string str)
		{
			if (str == null || str.Length == 0)
				return ' ';
			else
				return str[0];
		}
	}
}
