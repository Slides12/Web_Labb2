using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Shared.DTO_s
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public CustomerEntity Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
