using BankSystemApi.Contracts;
using BankSystemApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;

namespace BankSystemApi.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IRepository<CreditCard> _creditCardRepository;
        private readonly IDistributedCache _cache;

        public CreditCardService(IRepository<CreditCard> creditCardRepository, IDistributedCache cache)
        {
            _creditCardRepository = creditCardRepository;
            _cache = cache;
        }

        public decimal getBalance(string cardNumber)
        {
            var balance = _cache.GetString(cardNumber);
            if (string.IsNullOrEmpty(balance))
            {
                var card = _creditCardRepository.GetAll().FirstOrDefault(x => x.CardNumber == cardNumber);
                if (card == null)
                {
                    throw new ArgumentNullException();
                }
                _cache.SetString(cardNumber, card.Balance.ToString());
                return card.Balance;
            }
            return Convert.ToDecimal(balance);
            
        }

        public CreditCard GetById(int id)
        {
            var data = _creditCardRepository.GetById(id);
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            return data;
        }
    }
}
