using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkShop2.Models
{
    [Table("Initial")]
    public partial class Initial
    {
        public Initial()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        [StringLength(10)]
        public string InitialCode { get; set; }
        [StringLength(30)]
        public string InitialName { get; set; }

        [InverseProperty("InitialCodeNavigation")]
        public ICollection<Customer> Customers { get; set; }
    }
}
