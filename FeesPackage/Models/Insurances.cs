using System;
using System.Collections.Generic;
using System.Data;

namespace FeesPackage.Models
{
    public class Insurances : Insurance
    {
        public List<Insurance> InsuranceList;

		public Insurances() { }

		public Insurances(DataRowCollection rows)
		{
            InsuranceList = new List<Insurance>();

            foreach (DataRow row in rows)
            {
                InsuranceList.Add(new Insurance(row));
            }
        }
    }
}
