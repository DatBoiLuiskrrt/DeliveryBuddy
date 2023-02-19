namespace DeliveryBuddyFE.Models
{
    public class StatesModel
    {
        public int Id { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
        public int CountryId { get; set; }
    }
}
