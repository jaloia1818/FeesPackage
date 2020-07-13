using System;
using System.Collections.Generic;
using System.Data;

namespace FeesPackage.Models
{
    public class Adjusters
    {
        public List<Adjuster> AdjusterList;

        public Adjusters() { }

		public Adjusters(DataRowCollection rows)
        {
            AdjusterList = new List<Adjuster>();

            foreach (DataRow row in rows)
            {
                AdjusterList.Add(new Adjuster(row));
            }
        }
    }
}
