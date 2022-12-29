using MassFacultyUpdateNew.Contracts;
using MassFacultyUpdateNew.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Services
{
    public class CamsOpration : ICamsOpration
    {
        public CamsOpration(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        public async Task<bool> UpdateCams(UpdateViewModel updateViewModel)
        {
            bool updatestatus = false;
            try
            {
                int AdvisorID = 0;
                var studentIds = updateViewModel.studentid;
                if (updateViewModel.destinAdvisor != "")
                {
                    AdvisorID = Convert.ToInt16(updateViewModel.destinAdvisor);
                }
                DataSet ds = new DataSet();
                foreach (string docId in studentIds)
                {                  

                    SqlConnection con = new SqlConnection(Config.GetConnectionString("CamsDataConnection"));
                    string spName = "AdvisorDataUpdate";
                    SqlCommand cmd = new SqlCommand(spName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@studentID", SqlDbType.VarChar)).Value = docId;
                    cmd.Parameters.Add(new SqlParameter("@advisorto", SqlDbType.VarChar)).Value = updateViewModel.destinAdvisor;
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar)).Value = updateViewModel.sessionUser;
                    cmd.Parameters.Add(new SqlParameter("@term", SqlDbType.VarChar)).Value = updateViewModel.selectedTerm;
                    SqlDataAdapter sqldata1 = new SqlDataAdapter(cmd);
                    sqldata1.Fill(ds);

                }
                updatestatus = true;
                return updatestatus;
            }
            catch (Exception)
            {
                updatestatus = false;
                return updatestatus;
                throw;
            }
        }
    }
}
