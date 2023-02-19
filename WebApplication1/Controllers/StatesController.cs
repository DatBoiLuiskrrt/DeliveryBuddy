using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IConfiguration _config;
        public StatesController(IConfiguration config)
        {
            _config = config;
        }
        
        [HttpGet("GetAllStates")]
        public IEnumerable<StatesModel> GetAllStates()
        {
            List<StatesModel> States = new List<StatesModel>();
            string dbconnection = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllStates", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        StatesModel State = new StatesModel();
                        State.Id = Convert.ToInt32(rdr["Id"]);
                        State.StateName = rdr["StateName"].ToString();
                        State.StateCode = rdr["StateCode"].ToString();
                        State.CountryId = Convert.ToInt32(rdr["CountryId"]);
                        States.Add(State);
                    }
                }
            }
            return States;
        }
    }
}
