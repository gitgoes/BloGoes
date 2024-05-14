using AutoMapper;
using BloGoes.Domain.DTO;
using BloGoes.Domain.Entity;


namespace BloGoes.Services.Mappings
{
    public class BlogProfileMap : Profile
    {
        public BlogProfileMap()
        {
            CreateMap<BlogDTO, BlogEntity>().ReverseMap();
        }
    }
}
