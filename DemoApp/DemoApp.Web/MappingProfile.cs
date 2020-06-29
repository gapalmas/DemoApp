using App.Entities;
using AutoMapper;
using DemoApp.Web.DTOs;

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