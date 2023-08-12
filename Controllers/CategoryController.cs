using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NorthwindEfCore.Models;

namespace NorthwindEfCore.Controllers
{
    public static class CategoryController
    {

        public static Category Update(Category category)
        {

            try
            {
                NorthwindContext context = new NorthwindContext();
                context.Categories.Update(category);
                context.SaveChanges(); //Her adımda degilde En son işin bittiğinde bunu kullanabilirsin


                //SqlConnection conn = DB.conn();
                //SqlCommand cmd = new SqlCommand("Update Categories Set CategoryName = @categoryname, Description=@description where CategoryID=@categoryId", conn);
                //cmd.Parameters.AddWithValue("@categoryname", category.CategoryName);
                //cmd.Parameters.AddWithValue("@description", category.Description);
                //cmd.Parameters.AddWithValue("@categoryId", category.CategoryID);
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //conn.Close();
                return category;
            }
            catch
            {
                return null;
            }

        }
    }
}
