using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace project3.Models
{
    public partial class TblCustomers
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerUname { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string CustomerPwd { get; set; }
        [Required]
        
        public string CustomerName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }
    }
}
