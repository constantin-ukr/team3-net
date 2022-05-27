using BankSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystemApi.Database
{
    public class BankSystemContext : DbContext
    {
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BankSystemContext(DbContextOptions<BankSystemContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
