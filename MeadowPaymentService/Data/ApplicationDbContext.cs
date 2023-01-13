using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MeadowPaymentService.Models;

namespace MeadowPaymentService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        public DbSet<MoneySource> MoneySource { get; set; }
        public DbSet<MeadowPaymentService.Models.Account> Account { get; set; }
        public DbSet<MeadowPaymentService.Models.Payment> Payment { get; set; }
    }
}