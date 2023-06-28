using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IConfiguration _config;
        public CommentsController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet("GetCommentsByLocationId")]
        public IEnumerable<CommentsModel> GetComments(int Id, int StateId)
        {
            List<CommentsModel> Comments = new List<CommentsModel>();
            string dbconnection = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllCommentsByLocationId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@StateId", StateId);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        CommentsModel Comment = new CommentsModel();
                        Comment.Id = Convert.ToInt32(rdr["Id"]);
                        Comment.CommentDescription = rdr["CommentDescription"].ToString();
                        Comment.StateId = Convert.ToInt32(rdr["StateId"]);
                        Comment.CityId = Convert.ToInt32(rdr["CityId"]);
                        Comment.Active = rdr["Active"].ToString();
                        Comment.UserId = Convert.ToInt32(rdr["UserId"]);
                        Comments.Add(Comment);
                    }
                }
            }
            return Comments;
        }

        [HttpPost("AddComment")]
        public IActionResult AddComment(CommentsModel Comment)
        {
            string dbconnection = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spinsertnewcomment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", Comment.CommentDescription);
                    cmd.Parameters.AddWithValue("@StateId", Comment.StateId);
                    cmd.Parameters.AddWithValue("@CityId", Comment.CityId);
                    cmd.Parameters.AddWithValue("@UserId", Comment.UserId);
                    cmd.Parameters.AddWithValue("@Active", Comment.Active);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok();
        }
    }
}
