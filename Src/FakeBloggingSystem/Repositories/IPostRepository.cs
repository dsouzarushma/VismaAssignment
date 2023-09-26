using FakeBloggingSystem.Models.DTO;

namespace FakeBloggingSystem.Repositories
{
    public interface IPostRepository
    {
        public Task<PostResponseDTO> GetPost(Guid Id, bool authorFlag);
        public Task<PostDTO> AddPost(AddPostDTO postDTO);
    }
}
