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
        private Mock<IUnitOfWork> _unitOfWork;
        private OrderService _orderService;
        public OrderServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        CreditCard card = new CreditCard()
        { Id = 1, Balance = 50, Bank = "monobank", CardNumber = "1111 1111 1111 1111", Cvc = 101, OwnerName = "Rostyk Stakhiv", UserId = 1, DateOfExpire = new DateTime(2020, 12, 12) };
        User user = new User() { Id = 1, Login = "login", Password = "pass", RoleId = 1 };

        List<CreditCard> cards = new List<CreditCard>() {
             new CreditCard()
            { Id = 1, Balance = 50, Bank = "monobank", CardNumber = "1111 1111 1111 1111", Cvc = 101,
                 OwnerName = "Rostyk Stakhiv", UserId = 1, DateOfExpire = new DateTime(2020,12,12) },
         new CreditCard()
            { Id = 2, Balance = 50, Bank = "monobank", CardNumber = "1112 1111 1111 1111", Cvc = 101, OwnerName = "Rostyk Stakhiv", UserId = 1, DateOfExpire = new DateTime(2020,12,12) }
    };

        Order IncorectDataOrder = new Order() { Price = 50, UserId = 1, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 199, DateOfExpire = new DateTime(2020, 11, 11) } };
        Order CorrectOrder = new Order() { Price = 50, UserId = 1, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) } };
        Order FailedOrder = new Order() { Price = 50, UserId = 1, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) } };

        [Fact]
        public void CreateThrowsArgumentNullException()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetAll()).Returns(cards);
            _unitOfWork.Setup(s => s.GetUserRepository.GetById(It.IsAny<int>())).Returns(user);
            _orderService = new OrderService(_unitOfWork.Object);
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
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetAll()).Returns(cards);
            _unitOfWork.Setup(s => s.GetUserRepository.GetById(It.IsAny<int>())).Returns(user);
            _unitOfWork.Setup(s => s.GetOrdersRepository.Insert(It.IsAny<Order>())).Verifiable();
            _orderService = new OrderService(_unitOfWork.Object);
            _orderService.Create(CorrectOrder);
            try
            {
                _unitOfWork.Verify(x => x.GetOrdersRepository.Insert(CorrectOrder));
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
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetAll()).Returns(cards);
            _unitOfWork.Setup(s => s.GetUserRepository.GetById(It.IsAny<int>())).Returns(user);
            _unitOfWork.Setup(s => s.GetOrdersRepository.Insert(It.IsAny<Order>())).Verifiable();
            _orderService = new OrderService(_unitOfWork.Object);
            _orderService.Create(FailedOrder);
            try
            {
                _unitOfWork.Verify(x => x.GetOrdersRepository.Insert(FailedOrder));
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.True(false);
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
    }
}

