using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeadowPaymentService.Constant;

namespace MeadowPaymentService.Models
{
    [Table("payment_detail_external")]
    public class PaymentDetailExternal
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("payment_detail_id")]
        public string PaymentDetailId { get; set; }
        
        [Column("cmd")]
        public string Cmd { get; set; }
        
        [Column("url")]
        public string Url { get; set; }
        
        [Column("request")]
        public string Request { get; set; }
        
        [Column("response")]
        public string Response { get; set; }
        
        [Column("created_date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        
        [Column("updated_date")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }
                
        [Column("status")]
        public PaymentDetailStatus Status { get; set; }
    }
}