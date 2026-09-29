using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Size
    {
        public int Id { get; set; }                 // PK
        public required string Label { get; set; }    // Unique        

        // Navigation        
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
