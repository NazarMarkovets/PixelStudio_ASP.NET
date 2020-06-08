using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PixelStudio.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;

namespace PixelStudio.Controllers
{
    public class UserLoginController : Controller
    {
        public static string message = "";
        // GET: UserLogin
        public ActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Enter(LoginSystem login)
        {
            ViewBag.Name = message;
            string mainconn = ConfigurationManager.ConnectionStrings["StudioConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(mainconn);
            string sqlquery = "SELECT UserId, Email, UserName, RoleID, Password from [dbo].[Users] where Email = @Email and Password = @Password";
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);

            sqlCommand.Parameters.AddWithValue("@Email", login.Email);
            sqlCommand.Parameters.AddWithValue("@Password", login.Password);
            
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                
                Session["email"] = login.Email.ToString();
                while (sqlDataReader.Read()) // построчно считываем данные
                {
                    login.Id = Convert.ToInt32(sqlDataReader["UserId"]);
                    string email = sqlDataReader.GetString(1);
                    string name = sqlDataReader.GetString(2);
                    object role = sqlDataReader.GetValue(3);
                    
                    Session["name"] = name;
                    Session["id"] = login.Id;
                    Session["email"] = email;

                    if (email == "pixelstudio.admin@gmail.com" || (int)role == 1 && (int)role == 3)
                        return Redirect("/Administration/All_Services");
                    break;
                }


                return RedirectToAction("Logined");

            }

            else
            {
                ViewBag.LoginFailed = "Login failed";
            }

            connection.Close();

            return View();
        }

        public ActionResult SignOut()
        {
            try
            {
                if(Session["email"]!=null && Session["name"]!=null && Session["id"] != null)
                {
                    Session["email"] = null;
                    Session["name"] = null;
                    Session["id"] = null;
                }
                return Redirect("/Home/Home");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            
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
                ViewBag.Name = "Registration successfull";
                message = ViewBag.Name;
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