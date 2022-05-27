using System;

namespace BankSystemApi.Models
{
    public class CreditCard : BaseClass
    {
        public string CardNumber { get; set; }
        public DateTime DateOfExpire { get; set; }
        public int Cvc { get; set; }
        public decimal Balance { get; set; }
    }
}
