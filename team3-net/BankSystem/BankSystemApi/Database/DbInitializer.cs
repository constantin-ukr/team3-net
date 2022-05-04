using BankSystemApi.Extensions;
using BankSystemApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystemApi.Database
{
    public static class DbInitializer
    {
        public static void SeedData(BankSystemContext context)
        {
            if (!context.CreditCards.Any())
            {
                context.CreditCards.AddRange(
                    new CreditCard
                    {
                        Bank = "Alfabank",
                        CardNumber = "1111 1111 1111 1111",
                        Cvc = 123,
                        DateOfExpire = new DateTime(2010, 10, 10),
                        Balance = 5,
                        UserId = 1,
                        OwnerName = "Colin Morgan"
                    },
                    new CreditCard
                    {
                        Bank = "monobank",
                        CardNumber = "1111 2222 3333 1111",
                        Cvc = 111,
                        DateOfExpire = new DateTime(2017, 11, 11),
                        Balance = 5,
                        UserId = 1,
                        OwnerName = "Vito Scaletto"
                    },
                    new CreditCard
                    {
                        Bank = "alfabank",
                        CardNumber = "1111 1112 1113 1114",
                        Cvc = 222,
                        DateOfExpire = new DateTime(2010, 8, 8),
                        Balance = 1,
                        UserId=1,
                        OwnerName = "Tommy Angelo"
                    }) ;

                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role
                    {
                        RoleName = "Admin"
                    },
                    new Role
                    {
                        RoleName = "User"
                    });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(
                    new User
                    {
                        Login = "admin@gmail.com",
                        Password = "1111".HashPassword(),
                        RoleId = context.Roles.Where(x=>x.RoleName == "Admin").FirstOrDefault().Id
                    });
                context.SaveChanges();
            }
        }
    }
}
