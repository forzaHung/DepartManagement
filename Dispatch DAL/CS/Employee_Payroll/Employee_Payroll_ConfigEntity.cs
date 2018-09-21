using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public partial class Employee_Payroll_ConfigEntity
    {
        public int Id { get; set; }
        public string NameEmployee { get; set; }
        public int IdEmployee { get; set; }
        public double WageAgreement { get; set; }
        public DateTime ProbationaryFromDate { get; set; }
        public DateTime ProbationaryToDate { get; set; }
    }
}
