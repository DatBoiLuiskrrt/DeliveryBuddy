using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //get store procedure   
        [HttpPost]
        public static List<UserModel> InsertUser(string username, string password)
        {
            List<UserModel> users = new List<UserModel>();
            using (SqlConnection con = new SqlConnection("DefaultConnection"))
            {
                using (SqlCommand cmd = new SqlCommand("InsertNewUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@userpassword", password);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UserModel user = new UserModel();
                        user.Id = Convert.ToInt32(rdr["Id"]);
                        user.UserName = rdr["UserName"].ToString();
                        user.UserPassword = rdr["UserPassword"].ToString();
                        users.Add(user);
                    }
                }
            }
            return users;
        }
    }
}
