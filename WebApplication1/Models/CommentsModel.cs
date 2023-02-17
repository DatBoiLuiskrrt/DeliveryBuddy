namespace WebApplication1.Models
{
    public class CommentsModel
    {
        public int Id { get; set; }
        public string? CommentDescription { get; set; }
        public int? UserId { get; set; }
        public int? LocationId { get; set; }
    }
}
