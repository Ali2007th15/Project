using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string TrackingId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("User")]
        public int UserId {  get; set; }
        public User Users { get; set; }
        [ForeignKey("Products")]
        public string Product {  get; set; }
        public int ProductsCount { get; set; }
        public string Status { get; set; } = "Order Placed";
        public DateTime Created { get; set; }
    }
}
