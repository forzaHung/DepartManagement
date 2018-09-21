using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public partial class Employee_PayrollEntity
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public string NameEmployee { get; set; }
        public string NamePayroll { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public double WageAgreement { get; set; }
        public double StandardWorkDays { get; set; }
        public double NumberDaysOfficialSalary { get; set; }
        public double DaysProbationarySalary { get; set; }
        public double TotalMonthlySalary { get; set; }
        public double PremiumSalary { get; set; }
        public double CompulsoryInsurance { get; set; }
        public double UnionFees { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
