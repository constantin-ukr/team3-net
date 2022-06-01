using BankSystemApi;
using BankSystemApi.Contracts;
using BankSystemApi.Models;
using BankSystemApi.Services;
using BankSystemTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace BankSystemTests
{
    public class CreditCardServiceTests
    {
        private Mock<IRepository<CreditCard>> _creditCardRepository;
        private CreditCardService _creditCardService;
        public CreditCardServiceTests()
        {
            _creditCardRepository = new Mock<IRepository<CreditCard>>();
        }

        CreditCard card = new CreditCard()
        { Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020, 12, 12) };


        List<CreditCard> cards = new List<CreditCard>() {
             new CreditCard()
            { Id = 1, Balance = 50, CardNumber = "1111 1111 1111 1111", Cvc = 101,
            DateOfExpire = new DateTime(2020,12,12) },
         new CreditCard()
            { Id = 2, Balance = 50,CardNumber = "1112 1111 1111 1111", Cvc = 101, DateOfExpire = new DateTime(2020,12,12) }
    };

        [Fact]
        public void GetByIdThrowsArgumentNullException()
        {
            _creditCardRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns((CreditCard)null);
            _creditCardService = new CreditCardService(_creditCardRepository.Object);
            Assert.Throws<ArgumentNullException>(() => { _creditCardService.GetById(1); });

        }

        [Fact]
        public void GetByIdReturnsCard()
        {
            _creditCardRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(card);
            _creditCardService = new CreditCardService(_creditCardRepository.Object);
            var actual = _creditCardService.GetById(1);
            Assert.NotNull(actual);
            Assert.Equal(card, actual);
        }

        [Fact]
        public void GetBalanceReturnsInt()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _creditCardService = new CreditCardService(_creditCardRepository.Object);

            var result = _creditCardService.getBalance("1111 1111 1111 1111");
            Assert.Equal(50, result);
        }

        [Fact]
        public async void GetBalanceReturnsIntIntegration()
        {
            var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
        });

            var client = application.CreateClient();
            var response = await client.GetAsync(client.BaseAddress + "balance/1111 1112 1113 1114");
            var result = JsonConvert.DeserializeObject<BalanceResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(10, result.Balance);
        }
    }
}