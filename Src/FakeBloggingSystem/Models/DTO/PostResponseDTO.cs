namespace FakeBloggingSystem.Models.DTO
{
    public class PostResponseDTO
    {
        public bool IsSuccess { get; set; }
        public PostDTO? Post { get; set; }
        public PostAuthorDTO? PostAuthor { get; set; }
    }
}
