using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VRM.Common;
using VRM.Models;

namespace VRM.DataAccesslayer
{
    public static class Repository
    {

        public static List<TeamParameters> GetTeamDrpVlaueUser()
        {
            List<TeamParameters> retVal = new List<TeamParameters>();
            using (VirtualResourceManagerEntities client = new VirtualResourceManagerEntities())
            {
                retVal = (from rslt in client.Teams select rslt).Select(p => new TeamParameters()
                {
                     TeamId=p.TeamID,
                      TeamName=p.TeamName
                }).ToList();
            }
            return retVal == null ? new List<TeamParameters>() : retVal;
        }

        public static List<RoleParameters> GetAllRolesForDrpUser()
        {
            List<RoleParameters> retVal = new List<RoleParameters>();
            using (VirtualResourceManagerEntities client = new VirtualResourceManagerEntities())
            {
                retVal = (from rslt in client.Roles select rslt).Select(p => new RoleParameters()
                {
                     RoleID=p.RoleID,
                      RoleName=p.RoleName
                }).ToList();
            }
            return retVal == null ? new List<RoleParameters>() : retVal;
        }

        public static List<ShiftParameter> GetAllShiftDtlForDrpUser()
        {
            List<ShiftParameter> retvalue = new List<ShiftParameter>();

            retvalue.Add(new ShiftParameter { ShiftID = 0, ShiftName = "Please Select Shift" });
            retvalue.Add(new ShiftParameter { ShiftID = 1, ShiftName = "Morning" });
            retvalue.Add(new ShiftParameter { ShiftID = 2, ShiftName = "Rotational" });

            return retvalue;
        }

        public static List<ReporteeParameters> GetCorrectReporteeDropUser()
        {
            List<ReporteeParameters> retVal = new List<ReporteeParameters>();
            List<RoleParameters> roles = new List<RoleParameters>();
            using (VirtualResourceManagerEntities client = new VirtualResourceManagerEntities())
            {
                //roles = (from rslt in client.Roles where rslt.RoleHeirarchy>1 select rslt).Select(p => new RoleParameters()
                //{
                //    RoleID = p.RoleID,
                //    RoleName = p.RoleName
                //}).ToList();

                //retVal = (from rslt1 in client.UserDetails select rslt1).Select(p => new ReporteeParameters()
                //{
                //    ReportID = p.RoleID,
                //    ReportName = p.EmpName
                //}).ToList();

                //retVal = retVal.Where(p => p.ReportID.Equals(roles.Select(t => t.RoleID))).ToList();
                var t = (from pd in client.UserDetails
                         join od in client.Roles on pd.RoleID equals od.RoleID
                         where od.RoleHeirarchy > 1
                         select new
                         {
                             pd.EmpName,
                             pd.EmpID
                         }
                         ).ToList();
                         
                retVal = (from rslt1 in t select rslt1).Select(p => new ReporteeParameters()
                {
                    ReportID = p.EmpID,
                    ReportName = p.EmpName
                }).ToList();


            }
            return retVal == null ? new List<ReporteeParameters>() : retVal;
        }

        public static string SaveUserValue(UserMainParameters userMainParameters)
        {
            string retValue = "";
            bool result;
            using (VirtualResourceManagerEntities client = new VirtualResourceManagerEntities())
            {
                UserDetail usrDtls = new UserDetail();
                usrDtls.EmpID = userMainParameters.EmpID;
                usrDtls.EmpName = userMainParameters.EmpName;
                usrDtls.Email = userMainParameters.Email;
                usrDtls.RoleID = userMainParameters.Roles;
                usrDtls.IsActEmp = userMainParameters.IsActEmp;
                usrDtls.Reportee = userMainParameters.Reportee;
                usrDtls.Team = userMainParameters.Team;
                usrDtls.Shift = 1;
                usrDtls.Permissionlevel = 1;
                client.UserDetails.Add(usrDtls);
                result = (client.SaveChanges() > 0);

                if(result)
                {
                    retValue = "Success";
                }
            }
                return retValue;
        }

        public static List<GetUserMainParameters> GetAllUserDetails()
        {
            List<GetUserMainParameters> retval = new List<GetUserMainParameters>();
            List<GetReporteeName> getReporteeName = new List<GetReporteeName>();
            using (VirtualResourceManagerEntities client = new VirtualResourceManagerEntities())
            {
                //var sd = from rptdtl in client.UserDetails
                //        select new {
                //            rptdtl.EmpID,
                //            rptdtl.EmpName
                //        };
                             
                var result = (from ud in client.UserDetails
                         join ro in client.Roles on ud.RoleID equals ro.RoleID
                         join tm in client.Teams on ud.Team equals tm.TeamID
                         select new
                         {
                             ud.UserID,
                             ud.EmpID,
                             ud.EmpName,
                             ud.Email,
                             ro.RoleName,
                             ud.IsActEmp,
                             ud.Reportee,
                             tm.TeamName,
                             ud.Shift,
                             ud.Permissionlevel
                         }).ToList();

                retval = (from rtl in result select rtl).Select(p => new GetUserMainParameters()
                {
                    UserID = p.UserID,
                    EmployeeID = p.EmpID,
                    EmployeeName = p.EmpName,
                    Email = p.Email,
                    RoleName = p.RoleName,
                    IsActive = p.IsActEmp,
                    TeamName = p.TeamName,
                    ReporteeName=p.Reportee.ToStr(),
                    Shift = p.Shift.ToStr()


                }).ToList();

                return retval;
            }
        }

    }
}