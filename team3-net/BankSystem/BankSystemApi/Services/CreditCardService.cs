using BankSystemApi.Contracts;
using BankSystemApi.Models;
using System;
using System.Linq;

namespace BankSystemApi.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IRepository<CreditCard> _creditCardRepository;

        public CreditCardService(IRepository<CreditCard> creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public decimal getBalance(string cardNumber)
        {
            var card = _creditCardRepository.GetAll().FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card == null)
            {
                throw new ArgumentNullException();
            }
            return card.Balance;
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
