using BankSystemApi;
using BankSystemApi.Models;
using BankSystemTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BankSystemIntegrationTests
{
    public class MakeOrderTest
    {
        static Order IncorectDataOrder = new Order() { Price = 50, CreditCard = new CreditCard() { CardNumber = "1111 1111 1111 1111", Cvc = 199, DateOfExpire = new DateTime(2020, 11, 11) } };

        [Fact]
        public async void MakeOrderReturnsNotFoundIntegration()
        {
            var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
        });

            var client = application.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(IncorectDataOrder), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress + "orders", content);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
