using BankSystemApi.Contracts;
using BankSystemApi.Models;
using BankSystemApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BankSystemTests
{
    public class OrderServiceTests
    {
        private Mock<IRepository<Order>> _orderRepository;
        private Mock<IRepository<CreditCard>> _creditCardRepository;
        private OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepository = new Mock<IRepository<Order>>();
            _creditCardRepository = new Mock<IRepository<CreditCard>>();
        }

        static CreditCard card = new CreditCard()
        { Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) };

        static List<CreditCard> cards = new List<CreditCard>() {
             new CreditCard()
            { Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101,
                 DateOfExpire = new DateTime(2020,12,12) },
         new CreditCard()
            { Id = 2, Balance = 50, CardNumber = "1112 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020,12,12) }
    };

        static Order IncorectDataOrder = new Order() { Price = 50, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 199, DateOfExpire = new DateTime(2020, 11, 11) } };
        static Order CorrectOrder = new Order() { Price = 30, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) } };
        static Order FailedOrder = new Order() { Price = 100, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) } };
        static Order order = new Order()
        {
            Price = 50,
            CreditCard = new CreditCard { Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) }
        };
        static Order order2 = new Order()
        {
            Price = 50,
            CreditCard = new CreditCard { Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) }
        };
        static List<Order> orders = new List<Order>() { order, order2 };

        [Fact]
        public void MakeOrderThrowsArgumentNullException()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            Assert.Throws<ArgumentNullException>(() => { _orderService.MakeOrder(IncorectDataOrder); });
        }

        [Fact]
        public void MakeOrderAddsSuccesOrder()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _orderRepository.Setup(s => s.Insert(It.IsAny<Order>())).Verifiable();
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            _orderService.MakeOrder(CorrectOrder);
            _orderRepository.Verify(x => x.Insert(CorrectOrder));
        }

        [Fact]
        public void MakeOrderAddsFailedOrder()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _orderRepository.Setup(s => s.Insert(It.IsAny<Order>())).Verifiable();
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            Assert.Throws<ArgumentException>(() => { _orderService.MakeOrder(FailedOrder); });
            _orderRepository.Verify(x => x.Insert(FailedOrder));
        }

        [Fact]
        public void GetByIdThrowsArgumentNullException()
        {
            _orderRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns((Order)null);
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            Assert.Throws<ArgumentNullException>(() => { _orderService.GetById(1); });
        }

        [Fact]
        public void GetByIdReturnsOrder()
        {
            _orderRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(order);
            _creditCardRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(card);
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            var actual = _orderService.GetById(1);
            Assert.NotNull(actual);
            Assert.Equal(order, actual);
        }
    }
}

