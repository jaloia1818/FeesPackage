using System.Configuration;
using System.Web.Mvc;
using Sap.Data.SQLAnywhere;

namespace FeesPackage.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            SAConnection myConnection = new SAConnection(ConfigurationManager.ConnectionStrings["Needles"].ConnectionString);
            myConnection.Open();

            GetCheckListCount(myConnection);
            GetCheckList(myConnection);
            GetOpenCheckList(myConnection);

            myConnection.Close();

            return View();
        }

        private void GetCheckListCount(SAConnection myConnection)
        {
            SACommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT count(*) as cnt FROM case_checklist";
            SADataReader myDataReader = myCommand.ExecuteReader();

            while (myDataReader.Read())
            {
                System.Diagnostics.Debug.WriteLine("cnt: {0}", myDataReader["cnt"]);
                ViewBag.CNT = myDataReader["cnt"];
            }

            myDataReader.Close();
        }

        private void GetCheckList(SAConnection myConnection)
        {
            SACommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "select count(*) as cnt from case_checklist where staff_assigned = 'KALAI'";
            SADataReader myDataReader = myCommand.ExecuteReader();

            while (myDataReader.Read())
            {
                System.Diagnostics.Debug.WriteLine("cnt: {0}", myDataReader["cnt"]);
                ViewBag.CNT2 = myDataReader["cnt"];
            }

            myDataReader.Close();
        }

        private void GetOpenCheckList(SAConnection myConnection)
        {
            SACommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "select count(*) as cnt from case_checklist where staff_assigned = 'KALAI' and status = 'Open'";
            SADataReader myDataReader = myCommand.ExecuteReader();

            while (myDataReader.Read())
            {
                System.Diagnostics.Debug.WriteLine("cnt: {0}", myDataReader["cnt"]);
                ViewBag.CNT3 = myDataReader["cnt"];
            }

            myDataReader.Close();
        }
    }
}
