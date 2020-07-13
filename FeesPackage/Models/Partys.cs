using System;
using System.Collections.Generic;
using System.Data;

namespace FeesPackage.Models
{
    public class Partys : Party
    {
        public List<Party> PartyList;

        public Partys() { }

		public Partys(DataRowCollection rows)
		{
            PartyList = new List<Party>();

            foreach (DataRow row in rows)
            {
                PartyList.Add(new Party(row));
            }
        }
    }
}
