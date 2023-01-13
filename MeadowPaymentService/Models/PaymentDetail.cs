using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeadowPaymentService.Constant;

namespace MeadowPaymentService.Models
{
    [Table("payment_detail")]
    public class PaymentDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("type")]
        public string Type { get; set; }
        
        [Column("amount")]
        public decimal Amount { get; set; }
        
        [Column("fee")]
        public decimal Fee { get; set; }
        
        [Column("created_date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        
        [Column("updated_date")]
        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }
                
        [Column("status")]
        public PaymentDetailStatus Status { get; set; }
    }
}