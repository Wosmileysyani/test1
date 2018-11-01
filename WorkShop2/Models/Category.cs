using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkShop2.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("CatID")]
        public string CatId { get; set; }
        public string CatNeme { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
