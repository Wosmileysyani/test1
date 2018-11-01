using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkShop2.Models
{

    [Table("Product")]
    public partial class Product
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? UnitPerprice { get; set; }
        public int? Qty { get; set; }
        public short? Status { get; set; }
        public string UnitCode { get; set; }
        public string CatId { get; set; }

        public Category Cat { get; set; }
        public Unit UnitCodeNavigation { get; set; }
    }
}
