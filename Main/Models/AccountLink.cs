using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeadowPaymentService.Constant;

namespace MeadowPaymentService.Models;

[Table("account_card")]
public class AccountLink
{
    [Key]
    [Column("id")]
    [Display(Name = "Link ID")]
    public string Id { get; set; }

    [Column("account_id")]
    [Display(Name = "Account ID")]
    public string AccountId { get; set; }

    [Column("money_source_code")]
    [Display(Name = "Money source code")]
    public string MoneySourceCode { get; set; }

    [Column("number")]
    [Display(Name = "Card number")]
    public string Number { get; set; }

    [Column("created_date")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_date")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Updated date")]
    public DateTime UpdatedDate { get; set; }

    [Column("status")]
    [Display(Name = "Status")]
    public AccountStatus Status { get; set; }
}