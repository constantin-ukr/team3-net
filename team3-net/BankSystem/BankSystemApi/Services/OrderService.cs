using BankSystemApi.Contracts;
using BankSystemApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystemApi.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Order order)
        {
            var creditcard = _unitOfWork.GetCreditCardRepository.GetAll().FirstOrDefault(x=>(x.Cvc==order.CreditCard.Cvc&&
            x.CardNumber==order.CreditCard.CardNumber&&x.DateOfExpire==order.CreditCard.DateOfExpire));
            var user = _unitOfWork.GetCreditCardRepository.GetById(order.UserId);
            if (user == null || creditcard == null||creditcard.UserId!=user.Id)
            {
                throw new ArgumentNullException("Incorect Data");
            }
            else
            {
                order.CardId = creditcard.Id;
                if (order.Price > creditcard.Balance)
                {
                    
                    order.Success = false;
                    order.Message = "You don't have enough money";
                    _unitOfWork.GetOrdersRepository.Insert(order);
                    _unitOfWork.Save();
                    throw new ArgumentException("You don't have enough money");
                }
                else
                {
                    creditcard.Balance -= order.Price;
                    order.Success = true;
                    order.Message = "Purchase was successful";
                    _unitOfWork.GetCreditCardRepository.Update(creditcard);
                    _unitOfWork.Save();
                    _unitOfWork.GetOrdersRepository.Insert(order);
                    _unitOfWork.Save();
                    
                }
            }
        }

        public void Delete(int id)
        {
            var order = GetById(id);

            if (order == null) throw new ArgumentNullException("Not Found");
            _unitOfWork.GetOrdersRepository.Delete(id);
            _unitOfWork.Save();
        }

        public List<Order> GetAllForUser(int userId)
        {
            var orders = _unitOfWork.GetOrdersRepository.GetAll().Where(x => x.UserId == userId);
            foreach ( var order in orders)
            {
                order.CreditCard = _unitOfWork.GetCreditCardRepository.GetById(order.CardId);
            }
            return orders.ToList();
        }


        public Order GetById(int id)
        {
            var order = _unitOfWork.GetOrdersRepository.GetById(id);
            if (order == null)
            {
                throw new ArgumentNullException();
            }
            order.CreditCard = _unitOfWork.GetCreditCardRepository.GetById(order.CardId);
            return order;
        }
    }
}
