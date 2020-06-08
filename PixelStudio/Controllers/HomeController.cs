using PixelStudio.Models;
using PixelStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace PixelStudio.Controllers
{
    public class HomeController : Controller
    {
        string mainconn = ConfigurationManager.ConnectionStrings["StudioConnection"].ConnectionString;

        Order orders = new Order();
        List<Order> ordersList = new List<Order>();

        PhotoService photoService = new PhotoService();
        List<PhotoService> serviceList = new List<PhotoService>();
        List<PhotoService> monoList = new List<PhotoService>();
        List<PhotoService> colorList = new List<PhotoService>();

        HomeSet homeset = new HomeSet();
        List<HomeSet> homesetList = new List<HomeSet>();
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult About_Company()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ShopCard()
        {
            
            ordersList = GetAllOrders();
                return View(ordersList);

        }

        public List<Order> GetAllOrders()
        {
            
            //SELECT NumbCopies, (SELECT SName FROM Seveces WHERE Orders.ServiceId = serviceId)as ServiceName,TotalPrice, Image, (SELECT Name FROM Statuses WHERE Orders.StatusId= Id)as CurrentStatus FROM [dbo].[Orders] WHERE UserId = 6014;

            ordersList = new List<Order>();
            
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string tmp = Convert.ToString(Session["Id"]);
                string sql = "SELECT OrderId, NumbCopies, (SELECT SName FROM Seveces WHERE Orders.ServiceId = serviceId)as ServiceName," +
                             "TotalPrice, Image, (SELECT Name FROM Statuses WHERE Orders.StatusId = Id)as CurrentStatus " +
                             "FROM [dbo].[Orders] WHERE UserId =" + tmp;
                using (var commant = new SqlCommand(sql, connection))
                {
                    using (var reader = commant.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();
                            order.OrderID = Convert.ToInt32(reader["OrderId"]);
                            order.NumbCopies = Convert.ToInt32(reader["NumbCopies"]);
                            order.ServiceDesc = reader["ServiceName"].ToString();
                            order.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                            order.StatusDesc = reader["CurrentStatus"].ToString();
                            order.Image = reader["Image"].ToString();
                            ordersList.Add(order);

                        }
                    }
                    connection.Close();
                }
            }
            return ordersList;
        }

        public ActionResult ShowMono()
        {
            monoList = ShowAllMonoServices();
            return View(monoList);
        }
        public ActionResult ShowColor()
        {
            colorList = ShowAllColorServices();
            return View(colorList);
        }
        public ActionResult All_Services()
        {
            serviceList = GetAllServices();
            return View(serviceList);
        }
        public ActionResult Service_Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photoService = GetServiceById(Id);
            if (photoService == null)
            {
                return HttpNotFound();
            }
            return View(photoService);
        }

        public List<PhotoService> GetAllServices()
        {
            serviceList = new List<PhotoService>();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT * FROM [dbo].[Seveces] ";
                using (var commant = new SqlCommand(sql, connection))
                {
                    using (var reader = commant.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Service = new PhotoService();
                            Service.ServiceId = Convert.ToInt32(reader["serviceId"]);
                            Service.Name = reader["SName"].ToString();
                            Service.PhotoFormat = reader["PhotoFormat"].ToString();
                            Service.Description = reader["Description"].ToString();
                            Service.ColorType = reader["ColorType"].ToString();
                            Service.Price = Convert.ToDecimal(reader["Price"]);
                            Service.Image = reader["Image"].ToString();
                            serviceList.Add(Service);

                        }
                    }
                    connection.Close();
                }
            }
            return serviceList;
        }
        public List<PhotoService> ShowAllMonoServices()
        {
            serviceList = new List<PhotoService>();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT * FROM [dbo].[Seveces] where ColorType = 'Mono'";
                using (var commant = new SqlCommand(sql, connection))
                {
                    using (var reader = commant.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Service = new PhotoService();
                            Service.ServiceId = Convert.ToInt32(reader["serviceId"]);
                            Service.Name = reader["SName"].ToString();
                            Service.PhotoFormat = reader["PhotoFormat"].ToString();
                            Service.Description = reader["Description"].ToString();
                            Service.ColorType = reader["ColorType"].ToString();
                            Service.Price = Convert.ToDecimal(reader["Price"]);
                            Service.Image = reader["Image"].ToString();
                            serviceList.Add(Service);

                        }
                    }
                    connection.Close();
                }
            }
            return serviceList;
        }
        public List<PhotoService> ShowAllColorServices()
        {
            serviceList = new List<PhotoService>();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT * FROM [dbo].[Seveces] where ColorType = 'Color'";
                using (var commant = new SqlCommand(sql, connection))
                {
                    using (var reader = commant.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Service = new PhotoService();
                            Service.ServiceId = Convert.ToInt32(reader["serviceId"]);
                            Service.Name = reader["SName"].ToString();
                            Service.PhotoFormat = reader["PhotoFormat"].ToString();
                            Service.Description = reader["Description"].ToString();
                            Service.ColorType = reader["ColorType"].ToString();
                            Service.Price = Convert.ToDecimal(reader["Price"]);
                            Service.Image = reader["Image"].ToString();
                            serviceList.Add(Service);

                        }
                    }
                    connection.Close();
                }
            }
            return serviceList;
        }

        

        public PhotoService GetServiceById(int? id)
        {
            PhotoService Service = new PhotoService();

            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT * FROM [dbo].[Seveces] WHERE serviceId = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Service.ServiceId = Convert.ToInt32(reader["serviceId"]);
                        Service.Name = reader["SName"].ToString();
                        Service.PhotoFormat = reader["PhotoFormat"].ToString();
                        Service.Description = reader["Description"].ToString();
                        Service.ColorType = reader["ColorType"].ToString();
                        Service.Price = Convert.ToDecimal(reader["Price"]);
                        Service.Image = reader["Image"].ToString();
                        serviceList.Add(Service);
                    }
                    connection.Close();
                }
            }

            return Service;
        }


        //GET SERVICE FOR ORDER
        public ActionResult MakeOrder(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["Id"] != null)
            {
                return RedirectToAction("MakeOrderForEnteredUser", new{ Id = Id});
            }

            if (photoService == null)
            {
                return HttpNotFound();
            }
            
            return View();
        }

        public ActionResult MakeOrderForEnteredUser(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (photoService == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (Session["name"] == null || Session["Id"] == null)
            {
                RedirectToAction("Home");
            }
            
            return View();
        }
        
        [HttpPost]
        public ActionResult MakeOrder(int? Id, HomeSet homeSet, HttpPostedFileBase file)
        {

            try
            {
                CreateProcedure(Id, homeSet, file);
                
                return RedirectToAction("All_Services");
            }
            catch
            {
                return View();

            }

        }
        [HttpPost]
        public ActionResult MakeOrderForEnteredUser(int? Id, HomeSet homeSet, HttpPostedFileBase file)
        {

            try
            {
                SessionParameter sessionParameter = new SessionParameter();
                sessionParameter.Name = Session["Id"].ToString();
                
                CreateProcedureForLogined(Id, homeSet, file, sessionParameter);
                
                return RedirectToAction("All_Services");
            }
            catch
            {
                return View();

            }
            
        }

        public HomeSet CreateProcedure(int? id, HomeSet homeSet, HttpPostedFileBase file)
        {
            Random random = new Random();
            string encriptValue = (random.Next(100000, 200000).ToString());
            var Enctipt = FormsAuthentication.HashPasswordForStoringInConfigFile(encriptValue, "SHA1");
            Enctipt = Enctipt.Substring(0, 12);
            homeSet.register.Password = Enctipt;

            using (SqlConnection sqlConnection = new SqlConnection(mainconn))
            {
                
                sqlConnection.Open();
                string command = "INSERT INTO [dbo].[Users] ([UserName],[UserSurname],[Phone],[Email],[Password]) VALUES (@UserName,@UserSurname,@Phone,@Email,@Password)";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@UserName", homeSet.register.UserName);
                sqlCommand.Parameters.AddWithValue("@UserSurname", homeSet.register.UserSurname);
                sqlCommand.Parameters.AddWithValue("@Phone", homeSet.register.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", homeSet.register.Email);
                sqlCommand.Parameters.AddWithValue("@Password", homeSet.register.Password);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(mainconn))
            {
                sqlConnection.Open();
                string cmd = "INSERT INTO [dbo].[Orders](UserId,NumbCopies,TotalPrice, Image) VALUES((SELECT TOP 1 UserId FROM [dbo].[Users] ORDER BY UserId DESC), @Copies, ((SELECT Price FROM [dbo].[Seveces] WHERE serviceId = @Id)*@Copies), @Image);";
                SqlCommand sqlCommand = new SqlCommand(cmd, sqlConnection);
                //string command = "UPDATE [dbo].[Orders] SET UserId = (SELECT UserId FROM [dbo].[Users] WHERE Email = @Email) WHERE OrderId = (SELECT TOP 1 OrderId FROM [dbo].[Orders] ORDER BY OrderId DESC)";
                //UPDATE [dbo].[Orders] SET UserId = (SELECT UserId FROM [dbo].[Users] WHERE Email = 'ruslan.shkurenko@nure.ua') WHERE OrderId = 8;
                //UPDATE [dbo].[Orders] SET UserId = (SELECT UserId FROM [dbo].[Users] WHERE Email = 'ruslan.shkurenko@nure.ua') WHERE OrderId = (SELECT TOP 1 OrderId FROM [dbo].[Orders] ORDER BY OrderId DESC)
                //UPDATE [dbo].[Orders] SET UserId = (SELECT UserId FROM [dbo].[Users] WHERE Email = @Email) WHERE OrderId = (SELECT TOP 1 * FROM [dbo].[Orders] ORDER BY OrderId DESC

                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.Parameters.AddWithValue("@TotalPrice", homeSet.Price);
                sqlCommand.Parameters.AddWithValue("@Copies", homeSet.copies);
                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string imgpath = Path.Combine(Server.MapPath("~/User-Images/"), filename);
                    file.SaveAs(imgpath);
                }
                sqlCommand.Parameters.AddWithValue("@Image", "~/User-Images/" + file.FileName);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            return homeset;
        }

        public HomeSet CreateProcedureForLogined(int? id, HomeSet homeSet, HttpPostedFileBase file, SessionParameter sessionParameter)
        {
            //sessionParameter.Name = Session["Na"]

            using (SqlConnection sqlConnection = new SqlConnection(mainconn))
            {

                sqlConnection.Open();
                string command = "INSERT INTO [dbo].[Orders](UserId,ServiceId,NumbCopies,TotalPrice, Image) VALUES(@UserId, @Id, @Copies, ((SELECT Price FROM [dbo].[Seveces] WHERE serviceId = @Id)*@Copies), @Image)";
                //INSERT INTO [dbo].[Users] ([UserName],[UserSurname],[Phone],[Email],[Password]) VALUES (@UserName,@UserSurname,@Phone,@Email,@Password)
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                //Random random = new Random();
                //string encriptValue = (random.Next(100, 400).ToString());
                //var Enctipt = FormsAuthentication.HashPasswordForStoringInConfigFile(encriptValue, "SHA1");
                //Enctipt = Enctipt.Substring(0, 12);

                
                sqlCommand.Parameters.AddWithValue("@UserId", int.Parse(sessionParameter.Name));
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.Parameters.AddWithValue("@TotalPrice", homeSet.Price);
                sqlCommand.Parameters.AddWithValue("@Copies", homeSet.copies);

                //sqlCommand.Parameters.AddWithValue("@UserName", homeSet.register.UserName);
                //sqlCommand.Parameters.AddWithValue("@UserSurname", homeSet.register.UserSurname);
                //sqlCommand.Parameters.AddWithValue("@Phone", homeSet.register.Phone);
                //sqlCommand.Parameters.AddWithValue("@Email", homeSet.register.Email);
                //sqlCommand.Parameters.AddWithValue("@Password", homeSet.register.Password);

                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string imgpath = Path.Combine(Server.MapPath("~/User-Images/"), filename);
                    file.SaveAs(imgpath);
                }
                sqlCommand.Parameters.AddWithValue("@Image", "~/User-Images/" + file.FileName);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
            return homeset;
        }


    }



}