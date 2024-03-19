using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "ProductName is required")]
        public string ProductName { get; init; } = string.Empty;
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; init; }
        public string Summary { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; init; }
    }
}
