using MassFacultyUpdateNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Contracts
{
    public interface IRequired_dropdowns
    {
        Task<List<TermsDetails>> GetTerms(string Username);
        Task<List<Faculties>> GetFaculties(string Username);
        Task<List<Programs>> GetPrograms(string Username);
        Task<List<EnrollmentStatus>> GetEnrollmentStatus(string Username);
        Task<List<YearLevel>> GetYearLevel(string Username);
    }
}
