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
        [Column("code")]
        [StringLength(10)]
        public string Code { get; set; }
        
        [Column("desc")]
        [StringLength(200)]
        public string Desc { get; set; }
        
        [Column("name")]
        [StringLength(500)]
        public string Name { get; set; }
        
        [Column("type")]
        public SourceType Type { get; set; }
        
        [Column("created_date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        
        [Column("updated_date")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }
        
        [Column("status")]
        public ModelStatus Status { get; set; }
        
    }
}