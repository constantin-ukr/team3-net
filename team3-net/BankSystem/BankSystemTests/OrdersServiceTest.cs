using BankSystemApi;
using BankSystemApi.Contracts;
using BankSystemApi.Models;
using BankSystemApi.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
        static Order CorrectOrder = new Order() { Price = 50, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) } };
        static Order FailedOrder = new Order() { Price = 50, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) } };
        static Order order = new Order() { Price = 50, CreditCard = new CreditCard{ Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) }
    };
        static Order order2 = new Order()
        {
            Price = 50,
            CreditCard = new CreditCard { Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) }
        };
        static List<Order> orders = new List<Order>() { order,order2 };

        [Fact]
        public void CreateThrowsArgumentNullException()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _orderService = new OrderService(_creditCardRepository.Object,_orderRepository.Object);
            try
            {
                _orderService.Create(IncorectDataOrder);
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentNullException), ex.GetType());
            }
        }
        [Fact]
        public void CreateAddsSuccesOrder()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _orderRepository.Setup(s => s.Insert(It.IsAny<Order>())).Verifiable();
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            _orderService.Create(CorrectOrder);
            try
            {
                _orderRepository.Verify(x => x.Insert(CorrectOrder));
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.True(false);
            }
        }


        [Fact]
        public void CreateAddsFailedOrder()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _orderRepository.Setup(s => s.Insert(It.IsAny<Order>())).Verifiable();
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            
            try
            {
                _orderService.Create(FailedOrder);
                _orderRepository.Verify(x => x.Insert(FailedOrder));
            }
            catch(Exception ex)
            {
                Assert.Equal(typeof(ArgumentException), ex.GetType());
            }
        }

        [Fact]
        public async void CreateReturnsNotFoundIntegration()
        {
            var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
        });

            var client = application.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(IncorectDataOrder), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress + "orders",content);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void GetByIdThrowsArgumentNullException()
        {
            _orderRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns((Order)null);
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            try
            {
                var result = _orderService.GetById(1);
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentNullException), ex.GetType());
            }
        }

        [Fact]
        public void GetByIdReturnsOrder()
        {
            _orderRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(order);
            _creditCardRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(card);
            _orderService = new OrderService(_creditCardRepository.Object, _orderRepository.Object);
            var actual =_orderService.GetById(1);
            Assert.NotNull(actual);
            Assert.Equal(order, actual);
        }

        

        

       
    }
}

