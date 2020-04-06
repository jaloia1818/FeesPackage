using System.Configuration;
using System.Data;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sap.Data.SQLAnywhere;

namespace FeesPackage.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            SAConnection myConnection = new SAConnection(ConfigurationManager.ConnectionStrings["Needles"].ConnectionString);
            myConnection.Open();

            GetOpenCheckListKalaiOpen(myConnection);

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

        private void GetCheckListCountKalai(SAConnection myConnection)
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

        private void GetOpenCheckListCountKalaiOpen(SAConnection myConnection)
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

        private void GetOpenCheckListKalaiOpen(SAConnection myConnection)
        {
            SACommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText =
                @"select cl.case_id as 'case'
                        , names.last_long_name + ', ' + names.prefix + ' ' + names.first_name as 'party_name'
                        , cl.code
                        , cl.description
                        , cl.staff_assigned as assigned
                        , cl.due_date
                        , cl.status
                        , cl_d.repeat_period
                        , cl_d.lim
                    from case_checklist cl
                    inner join checklist_dir cl_d on cl_d.matcode = cl.matcode and cl_d.code = cl.code
                    inner join cases on cases.casenum = cl.case_id
                    inner join party on party.case_id = cases.casenum
                    inner join names on names.names_id = party.party_id and names.name_location = party.party_id_location and party.our_client = 'Y'
                    where cl.staff_assigned = 'KALAI' and cl.status = 'Open'
                    order by cl.due_date desc";
            SADataReader myDataReader = myCommand.ExecuteReader();

            DataSet dsChecklist = new DataSet();
            dsChecklist.Tables.Add("Checklist");
            dsChecklist.Tables[0].Load(myDataReader);

            ViewBag.checklist = JsonConvert.SerializeObject(dsChecklist);

            myDataReader.Close();
        }
    }
}
