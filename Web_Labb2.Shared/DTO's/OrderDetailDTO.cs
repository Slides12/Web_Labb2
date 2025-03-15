using System.ComponentModel.DataAnnotations.Schema;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Shared.DTO_s
{
    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
