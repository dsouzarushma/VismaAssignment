using FakeBloggingSystem.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FakeBloggingSystem.Services
{
    public interface IPostService
    {
        public Task<PostResponseDTO> GetPost(Guid Id, bool authorFlag);
        public Task<PostDTO> AddPost(AddPostDTO postDTO);
    }
}
