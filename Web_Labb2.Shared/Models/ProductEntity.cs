using System.ComponentModel.DataAnnotations.Schema;

public class ProductEntity
{
    public int Id { get; set; }
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public string? Description { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public required string ProductCategory { get; set; }
    public bool Status { get; set; }
}