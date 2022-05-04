using BankSystemApi.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankSystemApi.Models
{
    public class CreditCard : BaseClass
    {

        public string Bank { get; set; }
        public string CardNumber { get; set; }
        public DateTime DateOfExpire { get; set; }
        public int Cvc { get; set; }
        public int Balance { get; set; }
        public string OwnerName { get; set; }
        public int UserId { get; set; }

    }
}
