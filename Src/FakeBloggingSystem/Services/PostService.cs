using AutoMapper;
using FakeBloggingSystem.CustomExceptions;
using FakeBloggingSystem.DBContext;
using FakeBloggingSystem.Models.DTO;
using FakeBloggingSystem.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FakeBloggingSystem.Services
{
    public class PostService : IPostService
    {    
        private IPostRepository _postRepository;
        public PostService(IPostRepository postRepository) {
            _postRepository = postRepository;
        }
        public async Task<PostDTO>  AddPost(AddPostDTO addPostDTO)
        {
            try
            {
                return await _postRepository.AddPost(addPostDTO);
            }
            catch (InvalidInputException ex) {

                throw ex;
            }
        }

        public async Task<PostResponseDTO> GetPost(Guid Id,bool authorFlag)
        {
                return await _postRepository.GetPost(Id,authorFlag);
        }
    }
}
