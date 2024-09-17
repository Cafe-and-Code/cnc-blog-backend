using AutoMapper;

namespace Blog.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Domain.User, Models.DTO.UserDTO>().ReverseMap();
            CreateMap<Models.Domain.Post, Models.DTO.PostDTO>().ReverseMap();
            CreateMap<Models.Domain.Category, Models.DTO.CategoryDTO>().ReverseMap();
            CreateMap<Models.Domain.PostCategory, Models.DTO.PostCategoryDTO>().ReverseMap();
            CreateMap<Models.Domain.User, Models.DTO.AddUserDTO>().ReverseMap();
        }
    }
}
