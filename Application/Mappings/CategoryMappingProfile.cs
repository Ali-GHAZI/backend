using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            //CreateMap<Product, ProductDto>().ReverseMap(); // avant l'include des url dans objet Product 
            CreateMap<Category, CategoryDto>();
        }
    }
}
