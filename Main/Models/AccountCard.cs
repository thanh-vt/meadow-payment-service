using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeadowPaymentService.Constant;

namespace MeadowPaymentService.Models
{
    [Table("account_card")]
    public class AccountCard
    {
        [Key]
        [Column("id")]
        [Display(Name = "Card ID")]
        public string Id { get; set; }

        [Column("account_id")]
        [Display(Name = "Account ID")]
        public string AccountId { get; set; }
        
        [Column("money_source_code")]
        [Display(Name = "Money source code")]
        public string MoneySourceCode { get; set; }
        
        [Column("name")]
        [Display(Name = "Card name")]
        public string Name { get; set; }

        [Column("type")]
        [Display(Name = "Card type")]
        public CardType Type { get; set; }
        
        [Column("number")]
        [Display(Name = "Card number")]
        public string Number { get; set; }
        
        [Column("cvc")]
        [Display(Name = "CVC")]
        public string Cvc { get; set; }
        
        [Column("expired_at")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Expired at")]
        public DateTime ExpiredDate { get; set; }
        
        [Column("address")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        
        [Column("status")]
        [Display(Name = "Status")]
        public AccountStatus Status { get; set; }
    }
}