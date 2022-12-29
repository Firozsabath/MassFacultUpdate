using MassFacultyUpdateNew.Contracts;
using MassFacultyUpdateNew.Data;
using MassFacultyUpdateNew.Models;
using MassFacultyUpdateNew.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CamsDbContext db;
        private readonly IRequired_dropdowns _required_Dropdowns;
        private readonly ICamsOpration _camsOpration;

        public HomeController(ILogger<HomeController> logger, CamsDbContext Db, IRequired_dropdowns required_Dropdowns, ICamsOpration camsOpration)
        {
            _logger = logger;
            db = Db;
           _required_Dropdowns = required_Dropdowns;
            _camsOpration = camsOpration;
        }

        public async Task<IActionResult> Index()
        {
            var UserName = HttpContext.Session.GetString("Username");
            var lst = await _required_Dropdowns.GetTerms(UserName);
            lst.Insert(0, new TermsDetails { TermCalendarID = 0, TextTerm = "--Select a Term--" });
            ViewBag.Terms = lst;
            var lst1 = await _required_Dropdowns.GetPrograms(UserName);
            //lst1.Insert(0, new Programs { programID = 0, programName = "--Select a Program--" });
            ViewBag.Programslst = lst1;
            var lst2  = await _required_Dropdowns.GetFaculties(UserName);
            //lst2.Insert(0, new Faculties { AdvisorID = 0, Emailaddr = "--Select a Advisor--" });
            ViewBag.Advisors = lst2;
            var lst3 = await _required_Dropdowns.GetEnrollmentStatus(UserName);
            lst3.Insert(0, new EnrollmentStatus { EnrollmentStatusID = 0, Status = "Select a status" });
            ViewBag.enrolstatus = lst3;
            var lst4 = await _required_Dropdowns.GetYearLevel(UserName);
            lst4.Insert(0, new YearLevel { UniqueID = 0, DisplayText = "Select a Year" });
            ViewBag.yearlevel = lst4;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentDetails(string searchTerm,string searchAdvisors,string searchPrograms,string requestType, string searchRegistered = null, string searchStatus = null,string yearLevel = null)
        {
            try
            {
                if(requestType == "Origin")
                {
                    ViewData["TableType"] = "stdlistorigintb";
                }
                else if(requestType == "destin")
                {
                    ViewData["TableType"] = "stdlistdestintb";
                }
                int SearchTerm = 0;
                List<V_StudentAdvisorUpdateApp> studList = new List<V_StudentAdvisorUpdateApp>();
                if (!String.IsNullOrEmpty(searchTerm))
                {
                    SearchTerm = Convert.ToInt16(searchTerm);
                }
                string[] programs = null;
                programs = searchPrograms.Split(',');
                string[] Advisorids = null;
                Advisorids = searchAdvisors.Split(',');
                if (searchAdvisors == "null" || searchAdvisors == "")
                {
                    Advisorids[0] = "0";
                }

                if ((programs[0] != "0") && (!String.IsNullOrEmpty(searchRegistered)) && (!String.IsNullOrEmpty(searchStatus)) && (!String.IsNullOrEmpty(yearLevel)))//////
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.StudentRegistered == searchRegistered && x.EnrollmentStatus == searchStatus && x.TermCalendarID == SearchTerm && x.YearLevel == yearLevel).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] != "0") && (String.IsNullOrEmpty(searchRegistered)) && (String.IsNullOrEmpty(searchStatus)) && (String.IsNullOrEmpty(yearLevel)))//////
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] != "0")&&(!String.IsNullOrEmpty(searchRegistered))&&(!String.IsNullOrEmpty(searchStatus)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.StudentRegistered == searchRegistered && x.EnrollmentStatus == searchStatus && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] != "0") && (!String.IsNullOrEmpty(searchRegistered)) && (!String.IsNullOrEmpty(yearLevel)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.StudentRegistered == searchRegistered && x.YearLevel == yearLevel && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] != "0") && (!String.IsNullOrEmpty(searchStatus)) && (!String.IsNullOrEmpty(yearLevel)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.EnrollmentStatus == searchStatus && x.YearLevel == yearLevel && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if((programs[0] != "0") && (!String.IsNullOrEmpty(searchRegistered)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.StudentRegistered == searchRegistered && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList();
                }
                else if ((programs[0] != "0") && (!String.IsNullOrEmpty(searchStatus)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.EnrollmentStatus == searchStatus && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList();
                }
                else if ((programs[0] != "0") && (!String.IsNullOrEmpty(yearLevel)))  ///////
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && programs.Contains(x.MajorMinorID.ToString()) && x.YearLevel == yearLevel && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList();
                }
                else if ((programs[0] == "0") && (!String.IsNullOrEmpty(searchRegistered)) && (!String.IsNullOrEmpty(searchStatus)) && (!String.IsNullOrEmpty(yearLevel)))//////
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && x.StudentRegistered == searchRegistered && x.EnrollmentStatus == searchStatus && x.TermCalendarID == SearchTerm && x.YearLevel == yearLevel).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] == "0") && (!String.IsNullOrEmpty(searchRegistered)) && (!String.IsNullOrEmpty(searchStatus)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && x.StudentRegistered == searchRegistered && x.EnrollmentStatus == searchStatus && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] == "0") && (!String.IsNullOrEmpty(searchRegistered)) && (!String.IsNullOrEmpty(yearLevel)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && x.StudentRegistered == searchRegistered && x.YearLevel == yearLevel && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] == "0") && (!String.IsNullOrEmpty(searchStatus)) && (!String.IsNullOrEmpty(yearLevel)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && x.EnrollmentStatus == searchStatus && x.YearLevel == yearLevel && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList(); //x => x.AdvisorID == 70 &&
                }
                else if ((programs[0] == "0") && (!String.IsNullOrEmpty(searchRegistered)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && x.StudentRegistered == searchRegistered && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList();
                }
                else if ((programs[0] == "0") && (!String.IsNullOrEmpty(searchStatus)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) &&  x.EnrollmentStatus == searchStatus && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList();
                }
                else if ((programs[0] == "0") && (!String.IsNullOrEmpty(yearLevel)))
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && x.YearLevel == yearLevel && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList();
                }
                else
                {
                    studList = db.V_StudentAdvisorUpdateApp.Where(x => Advisorids.Contains(x.AdvisorID.ToString()) && x.TermCalendarID == SearchTerm).OrderBy(x => x.MajorMinorName).ToList();
                }


                return PartialView("_studentListPartials", studList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> LatestStduentStatus(string searchText)
        {
            try
            {                
                ViewData["TableType"] = "singlestdlistorigintb";              
                List<V_StudentAdvisorUpdateApp> studList = new List<V_StudentAdvisorUpdateApp>();
                var studListed = db.V_StudentAdvisorUpdateApp.Where(x => x.StudentID == searchText).OrderByDescending(x => x.Term).FirstOrDefault();
                studList.Add(studListed);
                return PartialView("_studentListPartials", studList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //var StudentList = db.V_StudentAdvisorUpdateApp.Where(x => x.AdvisorID == 96 && x.TermCalendarID == 54).ToList();

        public async Task<IActionResult> StudentUpdate()
        {
            var UserName = HttpContext.Session.GetString("Username");
            var lst1 = await _required_Dropdowns.GetPrograms(UserName);
            //lst1.Insert(0, new Programs { programID = 0, programName = "--Select a Program--" });
            ViewBag.Programslst = lst1;
            var lst2 = await _required_Dropdowns.GetFaculties(UserName);
            lst2.Insert(0, new Faculties { AdvisorID = 0, Emailaddr = "--Select a Advisor--" });
            ViewBag.Advisors = lst2;

            return View();
        }

        [HttpPost]
        public ActionResult Updateadvisor(string studIDarr, string Searchterm, string advisorto)
        {
            try
            {
                var UserName = HttpContext.Session.GetString("Username");               
                string[] stdid = studIDarr.Split(',');
                UpdateViewModel data = new UpdateViewModel {
                    studentid = stdid,
                    destinAdvisor = advisorto,
                    selectedTerm = Searchterm,
                    sessionUser = UserName
                };
                _camsOpration.UpdateCams(data);                
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
