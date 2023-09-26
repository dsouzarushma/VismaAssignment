using AutoMapper;
using FakeBloggingSystem.Models.DTO;

namespace FakeBloggingSystem.Helper
{
    public class AutoMapperHandler:Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<PostDataModel, PostDTO>();
            CreateMap<PostDataModel, PostAuthorDTO>();
            CreateMap<PostDTO,PostAuthorDTO>();
            CreateMap<AuthorDataModel, AuthorDTO>();
        }
    }
}
