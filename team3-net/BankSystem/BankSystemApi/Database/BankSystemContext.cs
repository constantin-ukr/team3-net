using BankSystemApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystemApi.Database
{
    public class BankSystemContext : DbContext
    {
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Order> Orders { get; set; }

        public BankSystemContext(DbContextOptions<BankSystemContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
