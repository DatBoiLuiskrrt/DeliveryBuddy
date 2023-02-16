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
        private readonly IConfiguration _config;
        public UsersController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPut("UpdateUser")]
        public IEnumerable<UserModel> UpdateUser(string username, string password, int id)
        {
            List<UserModel> users = new List<UserModel>();
            string dbconnection = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@userpassword", password);
                    cmd.Parameters.AddWithValue("@userid", id);
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

        [HttpPost("user")]
        public IEnumerable<UserModel> PostUser(string username, string password)
        {
            List<UserModel> users = new List<UserModel>();
            string dbconnection = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spInsertNewUser", con))
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
