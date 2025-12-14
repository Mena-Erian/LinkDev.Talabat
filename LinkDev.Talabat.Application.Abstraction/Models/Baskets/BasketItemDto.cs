using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Baskets
{
    public record BasketItemDto
    {
        //[Required]
        public int Id { get; set; }
        //[Required]
        public required string ProductName { get; set; }
        public string? PictureUrl { get; set; }
        //[Range(.0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1 Item")]
        public int Quantity { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }
}
