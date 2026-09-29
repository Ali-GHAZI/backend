using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }                 // PK
        public required string SKU { get; set; }    // Unique

        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool isNew { get; set; } = true;

        // Foreign Keys
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }

        // Product data
        public decimal? Weight { get; set; }
        public string? Color { get; set; }
        public string? ImageUrl { get; set; }
        public string? HoverImageUrl { get; set; }
        public decimal? Price { get; set; }
        public int? Discount { get; set; }
        public int? Stock { get; set; }

        // Tracking
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }

        // Navigation
        public SubCategory SubCategory { get; set; } = null!;
        public Brand Brand { get; set; } = null!;

        public ICollection<Size> Sizes { get; set; } = new List<Size>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();


    }
}
