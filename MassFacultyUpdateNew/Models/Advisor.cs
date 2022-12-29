using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Models
{
    public class Advisor
    {
        public int AdvisorID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Salutation { get; set; }
        public string FormalName { get; set; }
        public string OfficePhone { get; set; }
        public int DepartmentID { get; set; }
        public string EMailAddress { get; set; }
        public bool ActiveFlag { get; set; }
        public string OriginalConvertValue { get; set; }
    }
}
