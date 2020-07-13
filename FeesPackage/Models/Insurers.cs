using System;
using System.Collections.Generic;
using System.Data;

namespace FeesPackage.Models
{
    public class Insurers : Insurer
    {
        public List<Insurer> InsurerList;
        
        public Insurers() { }

        public Insurers(DataRowCollection rows)
        {
            InsurerList = new List<Insurer>();

            foreach (DataRow row in rows)
            {
                InsurerList.Add(new Insurer(row));
            }
        }
    }
}
