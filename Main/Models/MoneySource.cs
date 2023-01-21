using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeadowPaymentService.Constant;

namespace MeadowPaymentService.Models
{
    [Table("money_source")]
    public class MoneySource
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("code")]
        [StringLength(50)]
        public string Code { get; set; }
        
        [Column("desc")]
        [StringLength(200)]
        public string Desc { get; set; }
        
        [Column("type")]
        public SourceType Type { get; set; }
        
        [Column("status")]
        public ModelStatus Status { get; set; }
        
        [Column("created_date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}