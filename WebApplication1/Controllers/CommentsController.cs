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
        public IEnumerable<CommentsModel> GetComments(int Id)
        {
            List<CommentsModel> Comments = new List<CommentsModel>();
            string dbconnection = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllCommentsByLocationId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        CommentsModel Comment = new CommentsModel();
                        Comment.Id = Convert.ToInt32(rdr["Id"]);
                        Comment.CommentDescription = rdr["CommentDescription"].ToString();
                        Comment.UserId = Convert.ToInt32(rdr["UserId"]);
                        Comment.LocationId = Convert.ToInt32(rdr["LocationId"]);
                        Comments.Add(Comment);
                    }
                }
            }
            return Comments;
        }
    }
}
