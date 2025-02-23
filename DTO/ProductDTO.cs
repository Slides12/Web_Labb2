public class Product
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required string ProductCategory { get; set; }
    public bool Status { get; set; }
}