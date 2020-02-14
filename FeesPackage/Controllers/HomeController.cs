using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sap.Data.SQLAnywhere;

namespace FeesPackage.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            SAConnection myConnection = new SAConnection("Data Source=Needles;UID=DBA;PWD=sql");
            myConnection.Open();

            SACommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT count(*) as cnt FROM case_checklist";
            SADataReader myDataReader = myCommand.ExecuteReader();
            
            while (myDataReader.Read())
            {
                System.Diagnostics.Debug.WriteLine("cnt: {0}", myDataReader["cnt"]);
            }

            myDataReader.Close();
            myConnection.Close();

            return View();
        }
    }
}