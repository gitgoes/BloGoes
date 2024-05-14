using AutoMapper;
using BloGoes.Domain.DTO;
using BloGoes.Domain.Entity;


namespace BloGoes.Services.Mappings
{
    public class BlogUserProfileMap : Profile
    {
        public BlogUserProfileMap()
        {
            CreateMap<BlogUserDTO, BlogUserEntity>().ReverseMap();
            CreateMap<BlogUserRequestDTO, BlogUserEntity>().ReverseMap();
            
        }
    }
}
