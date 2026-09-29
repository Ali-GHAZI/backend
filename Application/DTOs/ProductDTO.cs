using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Sku {  get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }

        public decimal? Weight { get; set; }
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }


        public bool isNew { get; set; } = true;

        public List<string>? ImageUrls { get; set; }

        public int? Discount { get; set; }

    }
}
