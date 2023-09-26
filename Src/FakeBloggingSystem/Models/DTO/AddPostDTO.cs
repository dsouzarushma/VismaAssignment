using System.ComponentModel.DataAnnotations;

namespace FakeBloggingSystem.Models.DTO
{
    public class AddPostDTO
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
