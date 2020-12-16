using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRM.Models
{
    public class GetUserMainParameters
    {
        public int UserID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string  Email { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public string ReporteeName   { get; set; }
        public string TeamName { get; set; }
        public string Shift { get; set; }

        public int Permissionlevel { get; set; }


       
    }
}