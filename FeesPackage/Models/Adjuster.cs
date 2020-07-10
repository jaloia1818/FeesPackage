using System;
using System.Data;

namespace FeesPackage.Models
{
    public class Adjuster
    {
        public int names_id { get; set; }
        public string first_name { get; set; }
        public string prefix { get; set; }
        public string last_long_name { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string work_phone { get; set; }
        public string work_extension { get; set; }
        public string fax_number { get; set; }

        public Adjuster() { }

		public Adjuster(DataRow row)
		{
            this.names_id = (int)GetValue(row, "names_id2");
            this.first_name = GetValue(row, "first_name2")?.ToString();
            this.prefix = GetValue(row, "prefix2")?.ToString();
            this.last_long_name = GetValue(row, "last_long_name2")?.ToString();
            this.work_phone = GetValue(row, "work_phone2")?.ToString();
            this.work_extension = GetValue(row, "work_extension2")?.ToString();
            this.fax_number = GetValue(row, "fax_number2")?.ToString();
        }

        protected object GetValue(DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) && !(row[column] is DBNull) ? row[column] : null;
        }
    }
}
