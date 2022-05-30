using BankSystemApi.Models;
using System;
using System.Linq;

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
                        CardNumber = "1111 1111 1111 1111",
                        Cvc = 123,
                        DateOfExpire = new DateTime(2022, 10, 1),
                        Balance = 500
                    },
                    new CreditCard
                    {
                        CardNumber = "1111 2222 3333 1111",
                        Cvc = 111,
                        DateOfExpire = new DateTime(2022, 10, 1),
                        Balance = 500
                    },
                    new CreditCard
                    {
                        CardNumber = "1111 1112 1113 1114",
                        Cvc = 222,
                        DateOfExpire = new DateTime(2010, 8, 8),
                        Balance = 10
                    });

                context.SaveChanges();
            }
        }
    }
}
