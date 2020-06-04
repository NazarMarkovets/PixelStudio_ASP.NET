using PixelStudio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PixelStudio.Controllers
{
    public class AdministrationController : Controller
    {
        
        string mainconn = ConfigurationManager.ConnectionStrings["StudioConnection"].ConnectionString;
        
        Order orders = new Order();
        List<Order> ordersList = new List<Order>();

        PhotoService photoService = new PhotoService();
        List<PhotoService> serviceList = new List<PhotoService>();


        public ActionResult All_Orders()
        {
            ordersList = GetOrders();
            return View(ordersList);
        }


        public ActionResult Order_Details(int orderId)
        {
            return View();
        }

        public List<Order> GetOrders()
        {
            ordersList = new List<Order>();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT * FROM [dbo].[Orders]";
                //(SELECT UserId, NumbCopies, (SELECT SName FROM Seveces WHERE Orders.ServiceId = serviceId)as ServiceName,TotalPrice FROM Orders)  
                using (var commant = new SqlCommand(sql, connection))
                {
                    using (var reader = commant.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var selectedOrder = new Order();

                            
                            selectedOrder.OrderID = Convert.ToInt32(reader["OrderId"]);
                            selectedOrder.UserID = Convert.ToInt32(reader["UserId"]);
                            selectedOrder.ServiceID = Convert.ToInt32(reader["ServiceId"]);
                            selectedOrder.StatusID = Convert.ToInt32(reader["StatusId"]);
                            selectedOrder.NumbCopies = Convert.ToInt32(reader["NumbCopies"]);
                            selectedOrder.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                            ordersList.Add(selectedOrder);

                        }
                    }
                    connection.Close();
                }
            }
            return ordersList;
        }

        /*-----------------------------------------SERVICE MANAGEMENT-------------------------------------*/


        //#SELECT ALL
        public ActionResult All_Services()
        {
            serviceList = GetPhotoServices();
            return View(serviceList);
        }
        public ActionResult Create_new_Service()
        {
            return View();
        }
        public ActionResult Delete_Service(int? Id)
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
        public ActionResult Edit_Service(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (photoService == null)
            {
                return HttpNotFound();
            }
            photoService = GetServiceById(Id);
            return View(photoService);
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




        [HttpPost]
        public ActionResult Delete_Service(int?Id, PhotoService photoService)
        {
            try
            {
                Delete(Id);
                return RedirectToAction("All_Services");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create_new_Service(PhotoService photoService,HttpPostedFileBase file)
        {

            try
            {
                Save(photoService, file);
                return RedirectToAction("All_Services");
            }
            catch
            {
                return Redirect("~/Home/Error.cshtml");
            }
            
        }
        [HttpPost]
        public ActionResult Edit_Service(int? Id, PhotoService photoService)
        {

            try
            {
                Update_Service(Id, photoService);
                return RedirectToAction("All_Services");
            }
            catch
            {
                return View();

            }

        }



        //#GET ALL
        public List<PhotoService> GetPhotoServices()
        {
            serviceList = new List<PhotoService>();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT * FROM [dbo].[Seveces]";
                using(var commant = new SqlCommand(sql, connection))
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
        //#GET BY ID
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
                        serviceList.Add(Service);
                    }
                    connection.Close();
                }
            }

                return Service;
        }
        
        
        public PhotoService Save(PhotoService photoService, HttpPostedFileBase file)
        {

            string mainconnection = ConfigurationManager.ConnectionStrings["StudioConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconnection);
            string command = "INSERT INTO [dbo].[Seveces](SName,PhotoFormat, Description, ColorType, Price, Image ) VALUES(@SName, @PhotoFormat, @Description, @ColorType, @Price, @Image)";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlConnection.Open();

            sqlCommand.Parameters.AddWithValue("@SName", photoService.Name);
            sqlCommand.Parameters.AddWithValue("@PhotoFormat", photoService.PhotoFormat);
            sqlCommand.Parameters.AddWithValue("@Description", photoService.Description);
            sqlCommand.Parameters.AddWithValue("@ColorType", photoService.ColorType);
            sqlCommand.Parameters.AddWithValue("@Price", photoService.Price);
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/User-Images/"), filename);
                file.SaveAs(imgpath);
            }
            sqlCommand.Parameters.AddWithValue("@Image", "~/User-Images/" + file.FileName);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            ViewData["Message"] = "Service" + photoService.Name + "was created successfully";
       
            return photoService;
        }
        
   
        public PhotoService Delete(int? Id)
        {
            PhotoService Service = new PhotoService();

            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "DELETE [dbo].[Seveces] WHERE serviceId = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", Id);
                command.ExecuteNonQuery();
                
               connection.Close();
                
            }
            return Service;
        }

        public PhotoService Update_Service(int?id, PhotoService photoService)
        {

            using (SqlConnection sqlConnection = new SqlConnection(mainconn))
            {
                sqlConnection.Open();
                string command = "UPDATE [dbo].[Seveces] SET SName = @SName, PhotoFormat = @PhotoFormat, Description = @Description, ColorType = @ColorType, Price = @Price WHERE serviceId = @Id";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.Parameters.AddWithValue("@SName", photoService.Name);
                sqlCommand.Parameters.AddWithValue("@PhotoFormat", photoService.PhotoFormat);
                sqlCommand.Parameters.AddWithValue("@Description", photoService.Description);
                sqlCommand.Parameters.AddWithValue("@ColorType", photoService.ColorType);
                sqlCommand.Parameters.AddWithValue("@Price", photoService.Price);
                //sqlCommand.Parameters.AddWithValue("@Image", photoService.Image);
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
                return photoService;
        }
    }
}