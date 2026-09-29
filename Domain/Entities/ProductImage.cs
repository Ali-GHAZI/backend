using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public required string Url { get; set; }

        public int ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
