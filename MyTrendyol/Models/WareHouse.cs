using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Models
{
    public class WareHouse
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}
