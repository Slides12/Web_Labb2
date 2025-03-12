using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Labb2.DTO_s
{
    public class ProductDTO
    {
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "ProductId must be exactly 8 characters.")]
        public required string ProductId { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Product name Is required.")]
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public required string ProductCategory { get; set; }
        public bool Status { get; set; }
        public string? ImagePath { get; set; } = string.Empty;

    }
}
