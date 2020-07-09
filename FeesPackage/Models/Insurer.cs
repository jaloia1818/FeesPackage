using System;
using System.Data;

namespace FeesPackage.Models
{
    public class Insurer
    {
        public int names_id { get; set; }
        public string last_long_name { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }

        public Insurer() { }

		public Insurer(DataRow row)
		{
            this.names_id = (int)GetValue(row, "names_id");
            this.last_long_name = GetValue(row, "last_long_name")?.ToString();
            this.address = GetValue(row, "address")?.ToString();
            this.address_2 = GetValue(row, "address_2")?.ToString();
            this.city = GetValue(row, "city1")?.ToString();
            this.state = GetValue(row, "state1")?.ToString();
            this.zipcode = GetValue(row, "zipcode")?.ToString();
        }

        protected object GetValue(DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) && !(row[column] is DBNull) ? row[column] : null;
        }
    }
}
