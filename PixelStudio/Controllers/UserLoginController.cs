using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PixelStudio.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace PixelStudio.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Enter(LoginSystem login)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["StudioConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(mainconn);
            string sqlquery = "SELECT Email, UserName, Role, Password from [dbo].[Users] where Email = @Email and Password = @Password";
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
            sqlCommand.Parameters.AddWithValue("@Email", login.Email);
            sqlCommand.Parameters.AddWithValue("@Password", login.Password);
            
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                
                Session["username"] = login.Email.ToString();
                while (sqlDataReader.Read()) // построчно считываем данные
                {
                    string email = sqlDataReader.GetString(0);
                    
                    string role = sqlDataReader.GetString(2);
                    Session["username"] = email;

                    if (email == "pixelstudio.admin@gmail.com" || role == "admin" && role == "manager")
                        return Redirect("/Administration/All_Services");
                    break;
                }

                return RedirectToAction("Logined");

            }

            else
            {
                ViewData["Message"] = "Login failed";
            }

            connection.Close();

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterSystem register)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["StudioConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            string sqlquery = "INSERT INTO [dbo].[Users] ([UserName],[UserSurname],[Phone],[Email],[Password])" +
                " VALUES (@UserName,@UserSurname,@Phone,@Email,@Password)";

            SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection);
            sqlConnection.Open();
            sqlCommand.Parameters.AddWithValue("@UserName", register.UserName);
            sqlCommand.Parameters.AddWithValue("@UserSurname", register.UserSurname);
            sqlCommand.Parameters.AddWithValue("@Phone", register.Phone);
            sqlCommand.Parameters.AddWithValue("@Email", register.Email);
            sqlCommand.Parameters.AddWithValue("@Password", register.Password);
            int rows = sqlCommand.ExecuteNonQuery();
            if(rows > 0)
            {
                ViewBag.Name = "Adding was succes"; 
                //ViewData["Message"] = "User Record Inserted Succes";
            }
    
            sqlConnection.Close();
            

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logined()
        {
            return View();
        }
    }

}