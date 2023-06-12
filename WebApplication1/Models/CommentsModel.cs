namespace WebApplication1.Models
{
    public class CommentsModel
    {
        public int Id { get; set; }
        public string? CommentDescription { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
    }
}
