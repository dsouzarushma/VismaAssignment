namespace FakeBloggingSystem.Models.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
