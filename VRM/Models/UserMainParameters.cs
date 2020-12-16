using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRM.Models
{
    public class UserMainParameters
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public int Roles { get; set; }
        public bool IsActEmp { get; set; }
        public int Reportee { get; set; }
        public int Team { get; set; }
        public int Shift { get; set; }
        public int PermissionLevel { get; set; }
    }
}