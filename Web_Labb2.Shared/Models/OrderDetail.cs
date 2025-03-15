using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_Labb2.Shared.Models;

public class OrderDetail
{
    [Key]
    public int OrderDetailID { get; set; }

    public int OrderID { get; set; }

    public string ProductID { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public OrderInfo OrderInfo { get; set; }

    public ProductEntity Product { get; set; }
}
