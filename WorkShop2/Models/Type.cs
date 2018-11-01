using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkShop2.Models
{
    [Table("Type")]
    public partial class Type
    {
        public Type()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        public short CustType { get; set; }
        [StringLength(30)]
        public string Name { get; set; }

        [InverseProperty("CustTypeNavigation")]
        public ICollection<Customer> Customers { get; set; }
    }
}
