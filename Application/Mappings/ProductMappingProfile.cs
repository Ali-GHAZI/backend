using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            //CreateMap<Product, ProductDto>().ReverseMap(); // avant l'include des url dans objet Product 
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images
                                                         .Select(img => img.Url)));
        }
    }
}
