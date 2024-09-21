using AutoMapper;

namespace Blog.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Mapping for User
            CreateMap<Models.DTO.UserDTO, Models.Domain.User>().ReverseMap();
            CreateMap<Models.Domain.User, Models.DTO.AddUserDTO>().ReverseMap();
            CreateMap<Models.Domain.User, Models.DTO.UpdateUserDTO>().ReverseMap();

            //Mapping for Post
            CreateMap<Models.Domain.Post, Models.DTO.PostDTO>().ReverseMap();
            CreateMap<Models.Domain.Post, Models.DTO.AddPostDTO>().ReverseMap();
            CreateMap<Models.Domain.Post, Models.DTO.UpdatePostDTO>().ReverseMap();
            CreateMap<Models.Domain.PostCategory, Models.DTO.PostCategoryDTO>().ReverseMap();

            //Mapping for Category
            CreateMap<Models.Domain.Category, Models.DTO.CategoryDTO>().ReverseMap();
            CreateMap<Models.Domain.Category, Models.DTO.AddCategoryDTO>().ReverseMap();
            CreateMap<Models.Domain.Category, Models.DTO.UpdateCategoryDTO>().ReverseMap();
        }
    }
}
