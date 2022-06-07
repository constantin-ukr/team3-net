using BankSystemApi;
using BankSystemTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace BankSystemIntegrationTests
{
    public class GetBalanceTest
    {
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