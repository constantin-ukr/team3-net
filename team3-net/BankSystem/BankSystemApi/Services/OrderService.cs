using BankSystemApi.Contracts;
using BankSystemApi.Models;
using System;
using System.Linq;

namespace BankSystemApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<CreditCard> _creditCardRepository;
        private readonly IRepository<Order> _ordersRepository;

        public OrderService(IRepository<CreditCard> creditCardRepository, IRepository<Order> orderRepository)
        {
            _creditCardRepository = creditCardRepository;
            _ordersRepository = orderRepository;
        }

        public void Create(Order order)
        {
            var creditcard = _creditCardRepository.GetAll().FirstOrDefault(x => (x.Cvc == order.CreditCard.Cvc &&
            x.CardNumber == order.CreditCard.CardNumber && x.DateOfExpire == order.CreditCard.DateOfExpire));
            if (creditcard == null)
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
                    _ordersRepository.Insert(order);
                    throw new ArgumentException("You don't have enough money");
                }
                else
                {
                    creditcard.Balance -= order.Price;
                    order.Success = true;
                    order.Message = "Purchase was successful";
                    _creditCardRepository.Update(creditcard);
                    _ordersRepository.Insert(order);
                }
            }
        }

        public Order GetById(int id)
        {
            var order = _ordersRepository.GetById(id);
            if (order == null)
            {
                throw new ArgumentNullException();
            }
            order.CreditCard = _creditCardRepository.GetById(order.CardId);
            return order;
        }
    }
}
