using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PixelStudio.ViewModels;

namespace PixelStudio.Models
{
    public class DbConn
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PixelStudio;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        internal void XogGalin(HomeSet homeSet)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Seveces values(@SName, @PhotoFormat, @Description, @ColorType, @Price)", conn);
            //command.Parameters.AddWithValue("@SName", homeSet.Name);
            //command.Parameters.AddWithValue("@PhotoFormat", homeSet.PhotoFormat);
            //command.Parameters.AddWithValue("@Description", homeSet.Description);
            //command.Parameters.AddWithValue("@ColorType", homeSet.ColorType);
            //command.Parameters.AddWithValue("@Price", homeSet.Price);

        }
    }
}