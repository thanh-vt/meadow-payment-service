using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeadowPaymentService.Constant;
using Newtonsoft.Json;

namespace MeadowPaymentService.Models
{
    [Table("payment")]
    public class Payment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("status")]
        public PaymentStatus Status { get; set; }

        [Column("created_date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        
        [Column("updated_date")]
        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Dictionary<string, decimal> DetailsMap { get; set; }
        
        [NotMapped]
        [ReadOnly(true)]
        public IList<PaymentDetail> Details { get; set; }

    }
}