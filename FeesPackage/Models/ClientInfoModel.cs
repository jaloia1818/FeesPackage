using System.Collections.Generic;
using FeesPackage.Data_Access;

namespace FeesPackage.Models
{
    public class ClientInfoModel
    {
        public List<ListClass> Counties { get; set; }
        public tblClient Client { get; set; }
        public List<tblClient> Clients { get; set; }
        public List<ListClass> Attys { get; set; }
    }
}