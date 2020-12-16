using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using VRM.Common;
using VRM.DataAccesslayer;
using VRM.Models;

namespace VRM.Controllers
{
    public class UserMaintenanceController : Controller
    {

        public class HttpParamActionAttribute : ActionNameSelectorAttribute
        {
            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                    return true;

                //if (!actionName.Equals("Action", StringComparison.InvariantCultureIgnoreCase))
                //    return false;

                var request = controllerContext.RequestContext.HttpContext.Request;
                return request[methodInfo.Name] != null;
            }
        }

        // GET: UserMaintenance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            List<GetUserMainParameters> ss = Repository.GetAllUserDetails();
            List<TeamParameters> retVal = Repository.GetTeamDrpVlaueUser();
            List<RoleParameters> role = Repository.GetAllRolesForDrpUser();
            List<ReporteeParameters> drpReportee = Repository.GetCorrectReporteeDropUser();
            List<SelectListItem> drpList = new List<SelectListItem>();
            List<SelectListItem> drpListForRole = new List<SelectListItem>();
            List<SelectListItem> drpListForReportee = new List<SelectListItem>();
            drpList = (from rtl in retVal select rtl).Select(p => new SelectListItem()
            {
                Text = p.TeamName,
                Value = p.TeamId.ToStr()

            }).ToList();
            ViewBag.drpList = drpList;
            drpListForRole=(from rtl in role select rtl).Select(p => new SelectListItem()
            {
               Text=p.RoleName,
               Value=p.RoleID.ToStr()

            }).ToList();
            ViewBag.drpListForRole = drpListForRole;

            drpListForReportee = (from rtl in drpReportee select rtl).Select(p => new SelectListItem()
            {
                Text = p.ReportName,
                Value = p.ReportID.ToStr()
            }).ToList();
            ViewBag.drpListForReportee = drpListForReportee;
            return View();
        }

        [HttpParamAction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveUser(UserMainParameters custDtls)
        {
            string retValue = string.Empty;
            retValue = Repository.SaveUserValue(custDtls);

            return View("ViewUserDetails");
        }

        public ActionResult ViewUserDetails()
        {

            List<GetUserMainParameters> ss = Repository.GetAllUserDetails();
            return View(ss);
        }
    }
}