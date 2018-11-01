using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkShop2.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        public string CustId { get; set; }
        public string InitialCode { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public short? CustType { get; set; }

        public Type CustTypeNavigation { get; set; }
        public Initial InitialCodeNavigation { get; set; }
    }
}
