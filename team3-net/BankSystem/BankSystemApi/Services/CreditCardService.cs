using BankSystemApi.Contracts;
using BankSystemApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystemApi.Services
{
    public class CreditCardService : ICreditCardService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreditCardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(CreditCard card)
        {
            //Validation!!
            _unitOfWork.GetCreditCardRepository.Insert(card);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var card = GetById(id);

            if (card == null) throw new ArgumentNullException("Not Found");
            _unitOfWork.GetCreditCardRepository.Delete(id);
            _unitOfWork.Save();
        }

        public List<CreditCard> GetAll()
        {
            var result = _unitOfWork.GetCreditCardRepository.GetAll().ToList();
            if(result.Count==0)
            {
                throw new ArgumentNullException();
            }
            return result;

        }

        public int getBalance(string cardNumber)
        {
            var card = _unitOfWork.GetCreditCardRepository.GetAll().FirstOrDefault(x => x.CardNumber == cardNumber);
            if(card == null)
            {
                throw new ArgumentNullException();
            }
            return card.Balance;
        }

        public CreditCard GetById(int id)
        {
            var data = _unitOfWork.GetCreditCardRepository.GetById(id);
            if(data == null)
            {
                throw new ArgumentNullException();
            }
            return data;
        }

        public void Update(CreditCard card)
        {
            //Validation

            _unitOfWork.GetCreditCardRepository.Update(card);
            _unitOfWork.Save();
        }
    }
}
