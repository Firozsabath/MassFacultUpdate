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
    public class Required_dropdowns : IRequired_dropdowns
    {
        public Required_dropdowns(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        public async Task<List<EnrollmentStatus>> GetEnrollmentStatus(string Username)
        {
            List<EnrollmentStatus> Enroll_status = new List<EnrollmentStatus>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(Config.GetConnectionString("CamsDataConnection")))
                {
                    con.Open();
                    string spName = "DropdownDetailsAdvisorUpdate";
                    SqlCommand cmd = new SqlCommand(spName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar)).Value = "enrollment";
                    cmd.Parameters.Add(new SqlParameter("@Admin", SqlDbType.NVarChar)).Value = Username;
                    SqlDataAdapter sqldata = new SqlDataAdapter(cmd);
                    sqldata.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Enroll_status.Add(new EnrollmentStatus
                            {
                                EnrollmentStatusID = Convert.ToInt32(ds.Tables[0].Rows[i]["EnrollmentStatusID"].ToString()),
                                Status = ds.Tables[0].Rows[i]["Status"].ToString(),
                            });
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            return Enroll_status;
        }

        public async Task<List<Faculties>> GetFaculties(string Username)
        {
            List<Faculties> Facultieslist = new List<Faculties>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(Config.GetConnectionString("CamsDataConnection")))
                {
                    con.Open();
                    string spName = "DropdownDetailsAdvisorUpdate";
                    SqlCommand cmd = new SqlCommand(spName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar)).Value = "Advisor";
                    cmd.Parameters.Add(new SqlParameter("@Admin", SqlDbType.NVarChar)).Value = Username;
                    SqlDataAdapter sqldata = new SqlDataAdapter(cmd);
                    sqldata.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Facultieslist.Add(new Faculties
                            {
                                AdvisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["AdvisorID"].ToString()),
                                Emailaddr = ds.Tables[0].Rows[i]["EMailAddress"].ToString(),
                            });
                        }
                    }

                }
            }
            catch(Exception Ex)
            {
                throw;
            }
            return Facultieslist;
        }

        public async Task<List<Programs>> GetPrograms(string Username)
        {
            List<Programs> Programslist = new List<Programs>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(Config.GetConnectionString("CamsDataConnection")))
                {
                    con.Open();
                    string spName = "DropdownDetailsAdvisorUpdate";
                    SqlCommand cmd = new SqlCommand(spName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar)).Value = "Program";
                    cmd.Parameters.Add(new SqlParameter("@Admin", SqlDbType.NVarChar)).Value = Username;
                    SqlDataAdapter sqldata = new SqlDataAdapter(cmd);
                    sqldata.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Programslist.Add(new Programs
                            {
                                programID = Convert.ToInt32(ds.Tables[0].Rows[i]["ProgramsID"].ToString()),
                                programName = ds.Tables[0].Rows[i]["Programs"].ToString(),
                            });
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            return Programslist;
        }

        public async Task<List<TermsDetails>> GetTerms(string Username)
        {
            List<TermsDetails> Termslist = new List<TermsDetails>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(Config.GetConnectionString("CamsDataConnection")))
                {
                    con.Open();
                    string spName = "DropdownDetailsAdvisorUpdate";
                    SqlCommand cmd = new SqlCommand(spName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar)).Value = "Term";
                    cmd.Parameters.Add(new SqlParameter("@Admin", SqlDbType.NVarChar)).Value = Username;
                    SqlDataAdapter sqldata = new SqlDataAdapter(cmd);
                    sqldata.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Termslist.Add(new TermsDetails
                            {
                                TermCalendarID = Convert.ToInt32(ds.Tables[0].Rows[i]["TermCalendarID"].ToString()),
                                TextTerm = ds.Tables[0].Rows[i]["TextTerm"].ToString(),
                            });
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            return Termslist;
        }

        public async Task<List<YearLevel>> GetYearLevel(string Username)
        {
            List<YearLevel> yearLevel = new List<YearLevel>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(Config.GetConnectionString("CamsDataConnection")))
                {
                    con.Open();
                    string spName = "DropdownDetailsAdvisorUpdate";
                    SqlCommand cmd = new SqlCommand(spName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar)).Value = "yearlevel";
                    cmd.Parameters.Add(new SqlParameter("@Admin", SqlDbType.NVarChar)).Value = Username;
                    SqlDataAdapter sqldata = new SqlDataAdapter(cmd);
                    sqldata.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            yearLevel.Add(new YearLevel
                            {
                                UniqueID = Convert.ToInt32(ds.Tables[0].Rows[i]["UniqueID"].ToString()),
                                DisplayText = ds.Tables[0].Rows[i]["DisplayText"].ToString(),
                            });
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            return yearLevel;
        }
    }
}
