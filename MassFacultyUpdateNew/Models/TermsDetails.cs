using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Models
{
    public class TermsDetails
    {
        public int TermCalendarID { get; set; }
        public string TextTerm { get; set; }
    }

    public class Programs
    {
        public int programID { get; set; }
        public string programName { get; set; }
    }

    public class Faculties
    {
        public int AdvisorID { get; set; }
        public string Emailaddr { get; set; }
    }

    public class EnrollmentStatus
    {
        public int EnrollmentStatusID { get; set; }
        public string Status { get; set; }
    }

    public class YearLevel
    {
        public int UniqueID { get; set; }
        public string DisplayText { get; set; }
    }

    public class UpdateViewModel
    {
        public string[] studentid { get; set; }

        public string selectedTerm { get; set; }

        public string destinAdvisor { get; set; }

        public string sessionUser { get; set; }
    }
}
