using MassFacultyUpdateNew.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Data
{
    public class CamsDbContext:DbContext
    {
        public DbSet<V_StudentAdvisorUpdateApp> V_StudentAdvisorUpdateApp { get; set; }
        public DbSet<Advisor> Advisor { get; set; }        
        public CamsDbContext(DbContextOptions<CamsDbContext> options):base(options)
        {

        }
    }
}
