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
        [Column("id")]
        public int Id { get; set; }
        
        [Column("code")]
        public string Code { get; set; }
        
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        
        [Column("email")]
        public string Email { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; }
        
        [Column("created_date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        
        [Column("updated_date")]
        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }
        
        [Column("status")]
        public AccountStatus Status { get; set; }
    }
}