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

        /*-----------------------------------------ORDER MANAGEMENT-------------------------------------*/

        public ActionResult All_Orders()
        {
            ordersList = GetOrders();
            return View(ordersList);
        } //DONE
        public ActionResult Order_Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orders = GetOrderInfoById(Id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        public Order GetOrderInfoById(int? Id)
        {
            string sqlExpression = "findUserData";
            Order getOrder = new Order();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@OrderId",
                    Value = Id
                };
                command.Parameters.Add(idParam);
                var result = command.ExecuteScalar();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            
                            getOrder.OrderID = Convert.ToInt32(reader["OrderId"]);
                            getOrder.StatusID = Convert.ToInt32(reader["Id"]);
                            getOrder.UserInfo = reader["UserData"].ToString();
                            getOrder.ServiceDesc = reader["SName"].ToString();
                            getOrder.StatusDesc = reader["Status"].ToString();
                            getOrder.TotalPrice = Convert.ToInt32(reader["Price"]);
                            getOrder.NumbCopies = Convert.ToInt32(reader["NumbCopies"]);
                            getOrder.Image = reader["Photo"].ToString();
                            ordersList.Add(getOrder);
                        }
                        connection.Close();
                    }
                    reader.Close();
                    
                }


                return getOrder;
            }
        }
        public List<Order> GetOrders()
        {
            ordersList = new List<Order>();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT Orders.OrderId," +
                             "(SELECT CONCAT(Users.UserName, ' ', Users.UserSurname) FROM Users WHERE UserId = Orders.UserId) as UserData, Photo," +
                             "NumbCopies, Seveces.SName,(SELECT Price FROM Seveces WHERE Seveces.serviceId = Order_Services.serviceID)*NumbCopies as Price, Statuses.Name as Status FROM Orders " +
                             "INNER JOIN Order_Services ON Order_Services.orderID = Orders.OrderId " +
                             "INNER JOIN Statuses ON Statuses.Id = Orders.StatusId " +
                             "INNER JOIN Seveces ON Seveces.serviceId = Order_Services.serviceID; ";


                //"SELECT Order_Services.Photo as User_Photo, Order_Services.NumbCopies as User_Copies FROM Orders INNER JOIN Order_Services ON Order_Services.orderID = Orders.OrderId; "

                using (var commant = new SqlCommand(sql, connection))
                {
                    using (var reader = commant.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var selectedOrder = new Order();

                            
                            selectedOrder.OrderID = Convert.ToInt32(reader["OrderId"]);
                            //selectedOrder.UserInfo = reader["UserData"].ToString();
                            //selectedOrder.Image = reader["Photo"].ToString();
                            selectedOrder.NumbCopies = Convert.ToInt32(reader["NumbCopies"]);
                            selectedOrder.ServiceDesc = reader["SName"].ToString();
                            selectedOrder.TotalPrice = Convert.ToInt32(reader["Price"]);
                            selectedOrder.StatusDesc = reader["Status"].ToString();
                            
                            ordersList.Add(selectedOrder);

                        }
                    }
                    connection.Close();
                }
            }
            return ordersList;
        }

        public ActionResult Edit_Order(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (orders == null)
            {
                return HttpNotFound();
            }
            orders = GetOrderInfoById(Id);
            return View(orders);
        }
        [HttpPost]
        public ActionResult Edit_Order(int? Id, Order order)
        {

            try
            {
                Update_Order(Id, order);
                return RedirectToAction("All_Orders");
            }
            catch
            {
                return View();

            }

        }

        public Order Update_Order(int? id, Order order)
        {

            using (SqlConnection sqlConnection = new SqlConnection(mainconn))
            {
                sqlConnection.Open();
                string command = "UPDATE [dbo].[Orders] SET StatusId = @status WHERE OrderId = @Id";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.Parameters.AddWithValue("@status", order.StatusID);
     
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

            }
            return orders;
        }


        /*-----------------------------------------USER MANAGEMENT-------------------------------------*/
        Users users = new Users();
        List<Users> usersList = new List<Users>();
        public ActionResult All_Users()
        {
            usersList = GetUsers();
            return View(usersList);
        } //DONE

        public List<Users> GetUsers()
        {
            usersList = new List<Users>();
            using (var connection = new SqlConnection(mainconn))
            {
                connection.Open();
                string sql = "SELECT UserId, UserName, UserSurname, (SELECT RoleName FROM [dbo].[Role] WHERE RoleId = Users.RoleID)as Roles, Phone, Email FROM Users;";

                using (var commant = new SqlCommand(sql, connection))
                {
                    using (var reader = commant.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new Users();


                            user.UserId = Convert.ToInt32(reader["UserId"]);
                            user.UserName = reader["UserName"].ToString();
                            user.UserSurname = reader["UserSurname"].ToString();
                            user.Role = reader["Roles"].ToString();
                            user.Phone = reader["Phone"].ToString();
                            user.Email = reader["Email"].ToString();
                            usersList.Add(user);

                        }
                    }
                    connection.Close();
                }
            }
            return usersList;
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
                        Service.Image = reader["Image"].ToString();
                        serviceList.Add(Service);
                    }
                    connection.Close();
                }
            }

                return Service;
        }
        //#Save changes
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
                string imgpath = Path.Combine(Server.MapPath("~/Site-Images/"), filename);
                file.SaveAs(imgpath);
            }
            sqlCommand.Parameters.AddWithValue("@Image", "~/Site-Images/" + file.FileName);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            ViewData["Message"] = "Service" + photoService.Name + "was created successfully";
       
            return photoService;
        }
        //#Delete service
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
        //#Update
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