namespace WebApplication1.Models
{
    public class LocationsModel
    {
        public int Id { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public int HouseNumber { get; set; } = 0;
        public int? AptNumber { get; set; }
        public string? Active { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }



    }
}
