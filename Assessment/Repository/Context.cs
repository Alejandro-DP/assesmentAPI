using Assessment.Models;
using Microsoft.EntityFrameworkCore;
namespace Assessment.Repository
{
    public class Context : DbContext
    {

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Accounts = 123456789, Balance = 45450, Owner = 7633392 }
                );;
            modelBuilder.ApplyConfiguration(new TransactiomConfiguration());
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
    }
}
