using MassFacultyUpdateNew.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IConfiguration Config)
        {
            _config = Config;           
        }

        public IConfiguration _config { get; }
       
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            
            try
            {
                string URL = _config["APIEndpoints:LoginEndpoint"];                
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(URL, user))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            HttpContext.Session.SetString("Username", user.EmailID);
                            var permission = accessPersmission(user);
                            if (permission)
                            {
                                using (SqlConnection con1 = new SqlConnection(_config.GetConnectionString("CamsDataConnectionEx")))
                                {
                                    con1.Open();
                                    string sqlcommand = "insert into UserDetail_ForAdvisorupdate(UserName,LoginTime) values('" + user.EmailID + "','" + DateTime.Now + "')";
                                    var cmd = new SqlCommand(sqlcommand, con1);
                                    cmd.ExecuteNonQuery();
                                    con1.Close();
                                }
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Signed In user doesn't have the access. Please contact the admin!");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Incorrect User Name or Password!");
                        }
                        //Company = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                //List<ErroList> ErrorList = JsonConvert.DeserializeObject<List<ErroList>>(apiResponse);
                //foreach (var errors in ErrorList)
                //{
                //    ModelState.AddModelError("", errors.Description);
                //}
                throw;
            }

            return View();
        }

        public bool accessPersmission(User user)
        {
            DataSet ds = new DataSet();
            bool permission = false;
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("CamsDataConnectionEx")))
            {
                //Session["Username"] = acc.name;
                con.Open();
                SqlDataAdapter sqldata = new SqlDataAdapter("select * from tbl_user_foradvisorupdate where UserName = '" + user.EmailID + "'", con);
                sqldata.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                    permission = true;
                else
                    permission = false;

            }
            return permission;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            foreach (var cookieKey in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookieKey);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
