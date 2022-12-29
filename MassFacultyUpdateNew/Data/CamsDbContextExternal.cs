using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Data
{
    public class CamsDbContextExternal:DbContext
    {
        public CamsDbContextExternal(DbContextOptions<CamsDbContextExternal> options): base(options)
        {
            
        }
    }
}
