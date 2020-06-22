using FeesPackage.Data_Access;
using System.Collections.Generic;

namespace FeesPackage.Models
{
    public class User_Roles
    {
        public List<User> Users { get; set; }
        public List<ListClass> Roles { get; set; }
    }
}