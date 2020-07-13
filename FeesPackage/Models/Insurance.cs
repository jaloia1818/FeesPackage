using System;
using System.Data;

namespace FeesPackage.Models
{
    public class Insurance
    {
        public int? insurance_id { get; set; }
        public int? insurance_id_location { get; set; }
        public int? party_id { get; set; }
        public int? party_id_location { get; set; }
        public int? case_num { get; set; }
        public int? insurer_id { get; set; }
        public int? insurer_id_location { get; set; }
        public int? adjuster_id { get; set; }
        public int? adjuster_id_location { get; set; }
        public string policy { get; set; }
        public string claim { get; set; }
        public string insured { get; set; }
        public string limits { get; set; }
        public char? accept { get; set; }
        public string agent { get; set; }
        public string policy_type { get; set; }
        public string comments { get; set; }
        public Decimal minimum_amount { get; set; }
        public Decimal maximum_amount { get; set; }
        public Decimal actual { get; set; }
        public string date_settled { get; set; }
        public string how_settled { get; set; }
        public char? case_status { get; set; }
        public char? case_status_attn { get; set; }
        public char? case_status_client { get; set; }

		public Insurance() { }

		public Insurance(DataRow row)
		{
            this.insurance_id = (int?)GetValue(row, "insurance_id");
            this.insurance_id_location = (int?)GetValue(row, "insurance_id_location");
            this.party_id = (int?)GetValue(row, "party_id");
            this.party_id_location = (int?)GetValue(row, "party_id_location");
            this.case_num = (int?)GetValue(row, "case_num");
            this.insurer_id = (int?)GetValue(row, "insurer_id");
            this.insurer_id_location = (int?)GetValue(row, "insurer_id_location");
            this.adjuster_id = (int?)GetValue(row, "adjuster_id");
            this.adjuster_id_location = (int?)GetValue(row, "adjuster_id_location");
            this.policy = GetValue(row, "policy")?.ToString();
            this.claim = GetValue(row, "claim")?.ToString();
            this.insured = GetValue(row, "insured")?.ToString();
            this.limits = GetValue(row, "limits")?.ToString();
            this.accept = GetValue(row, "accept")?.ToString()[0];
            this.agent = GetValue(row, "agent")?.ToString();
            this.policy_type = GetValue(row, "policy_type")?.ToString();
            this.comments = GetValue(row, "comments")?.ToString();
            this.minimum_amount = (Decimal)GetValue(row, "minimum_amount");
            this.maximum_amount = (Decimal)GetValue(row, "maximum_amount");
            this.actual = (Decimal)GetValue(row, "actual");
			this.date_settled = ((DateTime?)GetValue(row, "date_settled"))?.ToString("MM/dd/yyyy");
            this.how_settled = GetValue(row, "how_settled")?.ToString();
            this.case_status = GetValue(row, "case_status")?.ToString()[0];
            this.case_status_attn = GetValue(row, "case_status_attn")?.ToString()[0];
            this.case_status_client = GetValue(row, "case_status_client")?.ToString()[0];
        }

        protected object GetValue(DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) && !(row[column] is DBNull) ? row[column] : null;
        }
    }
}
