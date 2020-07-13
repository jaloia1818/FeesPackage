using System;
using System.Data;

namespace FeesPackage.Models
{
    public class Insurer
    {
        public int? names_id { get; set; }
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

        public Insurer() { }

		public Insurer(DataRow row)
		{
            this.names_id = (int?)GetValue(row, "names_id1");
            this.first_name = GetValue(row, "first_name1")?.ToString();
            this.prefix = GetValue(row, "prefix1")?.ToString();
            this.last_long_name = GetValue(row, "last_long_name1")?.ToString();
            this.address = GetValue(row, "address")?.ToString();
            this.address_2 = GetValue(row, "address_2")?.ToString();
            this.city = GetValue(row, "city1")?.ToString();
            this.state = GetValue(row, "state1")?.ToString();
            this.zipcode = GetValue(row, "zipcode")?.ToString();
            this.work_phone = GetValue(row, "work_phone1")?.ToString();
            this.work_extension = GetValue(row, "work_extension1")?.ToString();
            this.fax_number = GetValue(row, "fax_number1")?.ToString();
        }

        protected object GetValue(DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) && !(row[column] is DBNull) ? row[column] : null;
        }
    }
}
