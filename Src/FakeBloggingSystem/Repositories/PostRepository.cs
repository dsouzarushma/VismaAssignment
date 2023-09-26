using AutoMapper;
using FakeBloggingSystem.CustomExceptions;
using FakeBloggingSystem.DBConfiguration;
using FakeBloggingSystem.DBContext;
using FakeBloggingSystem.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FakeBloggingSystem.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AuthorData _authordata;
        private readonly ILogger<PostRepository> _logger;
        public PostRepository(BlogDBContext dbContext, IMapper mapper, AuthorData authordata, ILogger<PostRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authordata = authordata;
            _logger = logger;
        }
        public async Task<PostDTO> AddPost(AddPostDTO addPostDTO)
        {
            PostResponseDTO postResponseDTO = new PostResponseDTO();

            var author = _authordata.Authors.Find(i => i.Id == addPostDTO.AuthorId);
            if (author == null)
            {
                _logger.LogError("Invalid Author Id");
                throw new InvalidInputException("Invalid Author");
            }
            else
            {
                try
                {
                    PostDataModel post = new PostDataModel();
                    Guid Id = Guid.NewGuid();
                    post.Id = Id;
                    post.AuthorId = addPostDTO.AuthorId;
                    post.Title = addPostDTO.Title;
                    post.Description = addPostDTO.Description;
                    post.Content = addPostDTO.Content;
                    await _dbContext.Post.AddAsync(post);
                    await _dbContext.SaveChangesAsync();

                    PostDTO postDTO = new PostDTO();
                    postDTO = _mapper.Map<PostDataModel, PostDTO>(post);
                    return postDTO;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception at AddPost.Details:" + ex.Message);
                    throw ex;
                }
            }

        }

        public async Task<PostResponseDTO> GetPost(Guid Id, bool authorFlag)
        {
            PostResponseDTO postResponseDTO = new PostResponseDTO();

            if (authorFlag)
            {
                PostAuthorDTO postData = new PostAuthorDTO();
                var data = await _dbContext.Post.FirstOrDefaultAsync(i => i.Id == Id);
                if (data != null)
                {
                    try
                    {
                        postData = _mapper.Map<PostDataModel, PostAuthorDTO>(data);
                        var authorData = _authordata.Authors.FirstOrDefault(a => a.Id == data.AuthorId);
                        if (authorData != null)
                        {
                            postData.Author = _mapper.Map<AuthorDataModel, AuthorDTO>(authorData);
                        }
                        else
                        {
                            _logger.LogError("Author details not found.");
                            postData.Author = null;
                        }
                        postResponseDTO.IsSuccess = true;
                        postResponseDTO.PostAuthor = postData;
                        return postResponseDTO;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception at GetPost.Details:" + ex.Message);
                        throw ex;
                    }
                }
                else
                {
                    throw new ResourceNotFoundException("Post not found");
                }
            }
            else
            {
                PostDTO postData = new PostDTO();

                var data = await _dbContext.Post.FirstOrDefaultAsync(i => i.Id == Id);
                if (data != null)
                {
                    try
                    {
                        postData = _mapper.Map<PostDataModel, PostDTO>(data);
                        postResponseDTO.IsSuccess = true;
                        postResponseDTO.Post = postData;
                        return postResponseDTO;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception at GetPost.Details:" + ex.Message);
                        throw ex;
                    }
                }
                else
                {
                    _logger.LogError("Post not found");
                    throw new ResourceNotFoundException("Post not found");
                }
            }
        }
    }
}