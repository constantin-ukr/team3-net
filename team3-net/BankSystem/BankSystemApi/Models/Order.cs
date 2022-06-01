using System;

namespace BankSystemApi.Models
{
    public class Order : BaseClass
    {
        public string UserId { get; set; }
        public int CardId { get; set; }
        public decimal Price { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public CreditCard CreditCard { get; set; }
        private DateTime date;
        public DateTime Date
        {
            set
            {
                date = DateTime.Now.Date;
            }
            get => date;
        }
    }
}
