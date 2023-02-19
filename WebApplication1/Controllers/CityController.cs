using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly IConfiguration _config;
        public CityController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetAllCitiesByStateId")]
        public IEnumerable<CityModel> GetCities(int Id)
        {
            List<CityModel> Cities = new List<CityModel>();
            string dbconnection = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllCitiesByStateId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        while (rdr.Read())
                        {
                            CityModel City = new CityModel();
                            City.Id = Convert.ToInt32(rdr["Id"]);
                            City.CityName = rdr["CityName"].ToString();
                            City.StateId = Convert.ToInt32(rdr["StateId"]);
                            Cities.Add(City);
                        }
                    }
                }
            }
            return Cities;
        }
    }
}
