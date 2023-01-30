using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeadowPaymentService.Constant;

namespace MeadowPaymentService.Models
{
    [Table("account")]
    public class Account
    {
        [Key]
        [Column("customer_id")]
        [Display(Name = "Customer ID")]
        public string CustomerId { get; set; }

        [Column("phone_number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Column("email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Column("balance")]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        
        [Column("created_date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        public DateTime CreatedDate { get; set; }
        
        [Column("updated_date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Updated at")]
        public DateTime UpdatedDate { get; set; }
        
        [Column("status")]
        [Display(Name = "Status")]
        public AccountStatus Status { get; set; }
    }
}