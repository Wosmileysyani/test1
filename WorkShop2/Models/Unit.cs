using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkShop2.Models
{
    [Table("Unit")]
    public partial class Unit
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [StringLength(2)]
        public string UnitCode { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("UnitCodeNavigation")]
        public ICollection<Product> Products { get; set; }
    }
}
