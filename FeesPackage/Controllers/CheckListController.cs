using System.Configuration;
using System.Data;
using System.Web.Mvc;
using FeesPackage.Models;
using Newtonsoft.Json;
using Sap.Data.SQLAnywhere;

namespace FeesPackage.Controllers
{
    [AuthorizeRole(roles = "OPS, ADMIN")]
    public class CheckListController : BaseController
    {
        readonly SAConnection myConnection = new SAConnection(ConfigurationManager.ConnectionStrings["Needles"].ConnectionString);
        
        public ActionResult Index()
        {
            myConnection.Open();

            ClientInfoModel model = GetOpenCheckList(myConnection);

            myConnection.Close();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string case_no)
        {
            myConnection.Open();
            SACommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText =
                @"SELECT * FROM cases c
                    inner join user_case_data ucd on c.casenum = ucd.casenum
                    where c.casenum = " + case_no;
            SADataReader myDataReader = myCommand.ExecuteReader();

            DataSet ds = new DataSet();
            ds.Tables.Add("Case");
            ds.Tables[0].Load(myDataReader);

            ClientInfoModel model = new ClientInfoModel
            {
                Case = new Needles_Case(ds.Tables[0].Rows[0]),
                Case_Data = new User_Case_Data(ds.Tables[0].Rows[0])
            };

            myDataReader.Close();
            myConnection.Close();

            return PartialView("~/Views/CheckList/_Edit.cshtml", model);
        }

        private ClientInfoModel GetOpenCheckList(SAConnection myConnection)
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
                        , cases.lim_stat
                    from case_checklist cl
                    inner join checklist_dir cl_d on cl_d.matcode = cl.matcode and cl_d.code = cl.code
                    inner join cases on cases.casenum = cl.case_id
                    inner join party on party.case_id = cases.casenum
                    inner join names on names.names_id = party.party_id and names.name_location = party.party_id_location and party.our_client = 'Y'
                    where cl.staff_assigned = 'KALAI' and cl.status = 'Open' //and cl.code = 'FEE'
                    order by cl.due_date asc";
            SADataReader myDataReader = myCommand.ExecuteReader();

            DataSet dsChecklist = new DataSet();
            dsChecklist.Tables.Add("Checklist");
            dsChecklist.Tables[0].Load(myDataReader);

            ClientInfoModel model = new ClientInfoModel
            {
                CheckListCount = dsChecklist.Tables[0].Rows.Count,
                CheckList = JsonConvert.SerializeObject(dsChecklist)
            };

            myDataReader.Close();

            return model;
        }
    }
}
