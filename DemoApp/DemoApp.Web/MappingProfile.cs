using AutoMapper;
using DemoApp.Web.DTOs;
using DemoApp.Web.Models.Entities;

namespace DemoApp.Web
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}