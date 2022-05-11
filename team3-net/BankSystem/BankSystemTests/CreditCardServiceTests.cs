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
        private Mock<IUnitOfWork> _unitOfWork;
        private CreditCardService _creditCardService;
        public CreditCardServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        CreditCard card =  new CreditCard()
            { Id = 1, Balance = 50, Bank = "monobank", CardNumber = "1111 1111 1111 1111", Cvc = 101, OwnerName = "Rostyk Stakhiv", UserId = 1, DateOfExpire = new DateTime(2020, 12, 12) };
        

        List<CreditCard> cards =  new List<CreditCard>() {
             new CreditCard()
            { Id = 1, Balance = 50, Bank = "monobank", CardNumber = "1111 1111 1111 1111", Cvc = 101,
                 OwnerName = "Rostyk Stakhiv", UserId = 1, DateOfExpire = new DateTime(2020,12,12) },
         new CreditCard()
            { Id = 2, Balance = 50, Bank = "monobank", CardNumber = "1112 1111 1111 1111", Cvc = 101, OwnerName = "Rostyk Stakhiv", UserId = 1, DateOfExpire = new DateTime(2020,12,12) }
    };
        
        [Fact]
        public void GetByIdThrowsArgumentNullException()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetById(It.IsAny<int>())).Returns((CreditCard)null);
            _creditCardService = new CreditCardService(_unitOfWork.Object);
            try
            {
                var result = _creditCardService.GetById(1);
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentNullException),ex.GetType());
            }
        }

        [Fact]
        public void GetByIdReturnsCard()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetById(It.IsAny<int>())).Returns(card);
            _creditCardService = new CreditCardService(_unitOfWork.Object);
            var actual = _creditCardService.GetById(1);
            Assert.NotNull(actual);
            Assert.Equal(card,actual);
        }

        [Fact]
        public void GetBalanceReturnsInt()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetAll()).Returns(cards);
            _creditCardService = new CreditCardService(_unitOfWork.Object);

            var result = _creditCardService.getBalance("1111 1111 1111 1111");
            Assert.Equal(50, result);
        }

        [Fact]
        public void GetAllReturnsCards()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetAll()).Returns(cards);
            _creditCardService = new CreditCardService(_unitOfWork.Object);

            var result = _creditCardService.GetAll();
            Assert.Equal(cards, result);
        }

        [Fact]
        public async void GetBalanceReturnsIntIntegration()
        {
            var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
        });

            var client = application.CreateClient();
            var response = await client.GetAsync(client.BaseAddress+"balance/1111 1111 1111 1111");
            var result = JsonConvert.DeserializeObject<BalanceResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(5, result.Balance);
        }


        [Fact]
        public void CreateAddsCard()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.Insert(It.IsAny<CreditCard>())).Verifiable();
            _creditCardService = new CreditCardService(_unitOfWork.Object);
            _creditCardService.Create(card);
            try
            {
                _unitOfWork.Verify(x => x.GetCreditCardRepository.Insert(card));
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void DeleteCorrect()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.GetById(It.IsAny<int>())).Returns(card);
            _unitOfWork.Setup(s => s.GetCreditCardRepository.Delete(It.IsAny<int>())).Verifiable();
            _creditCardService = new CreditCardService(_unitOfWork.Object);
            _creditCardService.Delete(1);
            try
            {
                _unitOfWork.Verify(x => x.GetCreditCardRepository.Delete(1));
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void UpdateUpdatesCard()
        {
            _unitOfWork.Setup(s => s.GetCreditCardRepository.Update(It.IsAny<CreditCard>())).Verifiable();
            _creditCardService = new CreditCardService(_unitOfWork.Object);
            _creditCardService.Update(card);
            try
            {
                _unitOfWork.Verify(x => x.GetCreditCardRepository.Update(card));
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.True(false);
            }
        }
    }
}