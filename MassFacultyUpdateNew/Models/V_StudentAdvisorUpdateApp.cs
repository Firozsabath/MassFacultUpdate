using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Models
{
    public class V_StudentAdvisorUpdateApp
    {
        [Key]
        [DisplayName("Student ID")]
        public string StudentID { get; set; }
        [DisplayName("Student Name")]
        public string studname { get; set; }
        public int TermCalendarID { get; set; }       
        public Nullable<int> AdvisorID { get; set; }
        [DisplayName("Program")]
        public string MajorMinorName { get; set; }
        [DisplayName("Student Registered")]
        public string StudentRegistered { get; set; }
        [DisplayName("Enrollment Status")]
        public string EnrollmentStatus { get; set; }
        [DisplayName("Advisor")]
        public string AdvisorName { get; set; }
        public int MajorMinorID { get; set; }
        public string Term { get; set; }
        [DisplayName("Term")]
        public string TextTerm { get; set; }
        public string YearLevel { get; set; }
        [DisplayName("Last Term Reg")]
        public string LastTermReg { get; set; }
        [DisplayName("Credits")]
        public Nullable<float> CreditsCompleted { get; set; }        
    }
}
