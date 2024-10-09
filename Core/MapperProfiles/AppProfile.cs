﻿using AutoMapper;
using Core.Dtos;
using Data.Entities;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<EditProductDto, Product>();
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<RegisterDto, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
