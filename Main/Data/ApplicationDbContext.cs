using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MeadowPaymentService.Constant;
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

        public DbSet<MoneySource> MoneySource { get; set; }
        public DbSet<Account> Account { get; set; }
        
        public DbSet<AccountCard> AccountCard { get; set; }
        public DbSet<Payment> Payment { get; set; }
        
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        
        public DbSet<PaymentDetailExternal> PaymentDetailExternal { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            Audit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            Audit();
            // to the actual saving of the data
            return base.SaveChanges();
        }

        private void Audit()
        {
            var currentDateTime = DateTime.UtcNow;

            // get all the entities in the change tracker - this could be optimized
            // to fetch only the entities with "State == added" if that's the only 
            // case you want to handle
            var entities = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified);

            // handle newly added entities
            foreach (var entityEntry in entities)
            {
                switch (entityEntry.Entity)
                {
                    // set the CreatedOn field to the current date&time
                    case Account account:
                    {
                        account.UpdatedDate = currentDateTime;
                        if (entityEntry.State != EntityState.Added) continue;
                        account.CreatedDate = currentDateTime;
                        account.Status = AccountStatus.Init;
                        break;
                    }
                    case MoneySource moneySource:
                    {
                        moneySource.UpdatedDate = currentDateTime;
                        if (entityEntry.State != EntityState.Added) continue;
                        moneySource.CreatedDate = currentDateTime;
                        moneySource.Status = ModelStatus.Active;
                        break;
                    }
                }
            }
        }
    }
}