using AutoMapper;
using FakeBloggingSystem.CustomExceptions;
using FakeBloggingSystem.DBConfiguration;
using FakeBloggingSystem.Models.DTO;
using FakeBloggingSystem.Repositories;
using FakeBloggingSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FakeBlogging.Test
{
    public class PostServiceTests
    {
        private readonly PostService _postService;
        private readonly Mock<IPostRepository> _postRepoMock=new Mock<IPostRepository>();
        private readonly AuthorData _authorData=new AuthorData();
      
        public PostServiceTests() {
            _postService = new PostService(_postRepoMock.Object);
        }

        [Fact]
        public async Task GetPost_ShouldReturnPostWithoutAuhtorDetails_WhenIdExists_AuhtorDetailsNotRequested()
        {
            var postId= Guid.NewGuid();

            PostDTO postData = new PostDTO
            {
                Id = postId,
                AuthorId = 1,
                Content = "Test Content",
                Description = "Description",
                Title = "Title"
            };
            PostResponseDTO postResponseDTO = new PostResponseDTO
            {
                IsSuccess = true,
                Post = postData
            };
            _postRepoMock.Setup(x => x.GetPost(postId,false)).ReturnsAsync(postResponseDTO);

            var post = await _postService.GetPost(postId, false);

            Assert.NotNull(post.Post);
        }
        [Fact]
        public async Task GetPost_ShouldReturnNothing_WhenIdDoesnotExists_AuhtorDetailsNotRequested()
        {
            PostResponseDTO postResponseDTO = new PostResponseDTO
            {
                IsSuccess = true,
                Post = null,
                PostAuthor=null
            };
            _postRepoMock.Setup(x => x.GetPost(It.IsAny<Guid>(), false)).ReturnsAsync(() => postResponseDTO);

            var post = await _postService.GetPost(Guid.NewGuid(), false);

            Assert.Null(post.Post);
        }
        [Fact]
        public async Task GetPost_ShouldReturnPostWithAuthorDetails_WhenIdExists_AuhtorDetailsRequested_AuthorExists()
        {
            var postId = Guid.NewGuid();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<AuthorDataModel, AuthorDTO>(It.IsAny<AuthorDataModel>()))
            .Returns((AuthorDataModel source) =>
            {
                // Define the mapping behavior here
                var destination = new AuthorDTO
                {
                    Name= source.Name,
                    SurName= source.SurName
                };
                return destination;
            });
            var _auhtorDataModel = _authorData.Authors.FirstOrDefault(i => i.Id == 2);
            AuthorDTO author = mapperMock.Object.Map<AuthorDataModel, AuthorDTO>(_auhtorDataModel);
            PostAuthorDTO postData = new PostAuthorDTO
            {
                AuthorId = 2,
                Content = "Test Content",
                Description = "Description",
                Title = "Title",
                Author=author,
            };
            PostResponseDTO postResponseDTO = new PostResponseDTO
            {
                IsSuccess = true,
                PostAuthor = postData
            };
            _postRepoMock.Setup(x => x.GetPost(postId,true)).ReturnsAsync(postResponseDTO);

            var post = await _postService.GetPost(postId,true);

            Assert.NotNull(post.PostAuthor.Author);
        }
        [Fact]
        public async Task GetPost_ShouldReturnPostWithAuthorDetails_WhenIdExists_AuhtorDetailsRequested_AuthorDoesNotExists()
        {
            var postId = Guid.NewGuid();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<AuthorDataModel, AuthorDTO>(It.IsAny<AuthorDataModel>()))
            .Returns((AuthorDataModel source) =>
            {
                // Define the mapping behavior here
                var destination = new AuthorDTO
                {
                    Name = source.Name,
                    SurName = source.SurName
                };
                return destination;
            });
            var _auhtorDataModel = _authorData.Authors.FirstOrDefault(i => i.Id == 4);
            AuthorDTO author=null;
            if (_auhtorDataModel != null)
                mapperMock.Object.Map<AuthorDataModel, AuthorDTO>(_auhtorDataModel);
            PostAuthorDTO postData = new PostAuthorDTO
            {
                AuthorId = 4,
                Content = "Test Content",
                Description = "Description",
                Title = "Title",
                Author = author,
            };
            PostResponseDTO postResponseDTO = new PostResponseDTO
            {
                IsSuccess = true,
                PostAuthor = postData
            };
            _postRepoMock.Setup(x => x.GetPost(postId, true)).ReturnsAsync(postResponseDTO);

            var post = await _postService.GetPost(postId, true);

            Assert.Null(post.PostAuthor.Author);
        }
        [Fact]
        public async Task AddPost_ShouldCreateANewPost_WhenValidAuthorExists()
        {
            var postId = Guid.NewGuid();
            AuthorDataModel author = new AuthorDataModel
            {
                Id=1,
                Name = "Test",
                SurName = "Test",
            };
            AddPostDTO postData = new AddPostDTO
            {
                AuthorId = 1,
                Content = "Test Content",
                Description = "Description",
                Title = "Title"
            };
            PostDTO postDTO = new PostDTO
            {
                AuthorId = postData.AuthorId,
                Content = postData.Content,
                Description = postData.Description,
                Title = postData.Title,
                Id = postId
            };
            _postRepoMock.Setup(x => x.AddPost(postData)).ReturnsAsync(postDTO);

            var post = await _postService.AddPost(postData);

            Assert.NotNull(post);
        }
    }
}