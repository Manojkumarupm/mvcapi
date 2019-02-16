using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeModelMVC.Models
{
    public class EmployeeInformation
    {
        public IEnumerable<EmployeesLeaveTracking> employeeTracking { get; set; }
        public IEnumerable<EmployeeLeaveDeail> employeeDetails { get; set; }
    }
}