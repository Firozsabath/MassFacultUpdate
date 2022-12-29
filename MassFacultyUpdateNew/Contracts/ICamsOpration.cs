using MassFacultyUpdateNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Contracts
{
    public interface ICamsOpration
    {
        Task<bool> UpdateCams(UpdateViewModel updateViewModel);
    }
}
