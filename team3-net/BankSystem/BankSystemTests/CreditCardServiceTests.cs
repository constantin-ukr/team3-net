using BankSystemApi.Contracts;
using BankSystemApi.Models;
using BankSystemApi.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BankSystemTests
{
    public class CreditCardServiceTests
    {
        private Mock<IRepository<CreditCard>> _creditCardRepository;
        private Mock<IDistributedCache> _cache;
        private CreditCardService _creditCardService;
        public CreditCardServiceTests()
        {
            _creditCardRepository = new Mock<IRepository<CreditCard>>();
            _cache = new Mock<IDistributedCache>();
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
            _creditCardService = new CreditCardService(_creditCardRepository.Object, _cache.Object);
            Assert.Throws<ArgumentNullException>(() => { _creditCardService.GetById(1); });
        }

        [Fact]
        public void GetByIdReturnsCard()
        {
            _creditCardRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(card);
            _creditCardService = new CreditCardService(_creditCardRepository.Object, _cache.Object);
            var actual = _creditCardService.GetById(1);
            Assert.NotNull(actual);
            Assert.Equal(card, actual);
        }

        [Fact]
        public void GetBalanceReturnsInt()
        {
            _creditCardRepository.Setup(s => s.GetAll()).Returns(cards);
            _creditCardService = new CreditCardService(_creditCardRepository.Object, _cache.Object);

            var result = _creditCardService.getBalance("1111 1111 1111 1111");
            Assert.Equal(50, result);
        }
    }
}