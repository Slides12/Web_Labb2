using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Labb2.Shared.Models
{
    public class OrderInfo
    {
        [Key]
        public int OrderID { get; set; }
        public int CustomerID { get; set; } 
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public CustomerEntity Customer { get; set; } 
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
