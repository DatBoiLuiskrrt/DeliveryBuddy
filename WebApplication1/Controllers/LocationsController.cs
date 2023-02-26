using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LocationsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetLocationsByCityId")]
        public IEnumerable<LocationsModel> UpdateUser(int Id)
        {
            List<LocationsModel> Locations = new List<LocationsModel>();
            string dbconnection = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(dbconnection))
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllLocationsByCityId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        LocationsModel Location = new LocationsModel();
                        Location.Id = Convert.ToInt32(rdr["Id"]);
                        Location.Active = rdr["Active"].ToString();
                        Location.AptNumber = rdr["AptNumber"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["AptNumber"]);
                        Location.StreetName = rdr["StreetName"].ToString();
                        Location.StateId = Convert.ToInt32(rdr["StateId"]);
                        Location.UserId = Convert.ToInt32(rdr["UserId"]);
                        Location.CityId = Convert.ToInt32(rdr["CityId"]);
                        Location.CountryId = Convert.ToInt32(rdr["CountryId"]);
                        Location.HouseNumber = Convert.ToInt32(rdr["HouseNumber"]);
                        Locations.Add(Location);
                    }
                }
            }
            return Locations;
        }

    }
}
