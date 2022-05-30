using BankSystemApi.Contracts;
using BankSystemApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemApi.Controllers
{
    [MyExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _creditCardService.GetById(id);
            return Ok(data);
        }

        [HttpGet("/balance/{cardNumber}")]
        public IActionResult GetBalance(string cardNumber)
        {
            var balance = _creditCardService.getBalance(cardNumber);
            return Ok(new { Balance = balance });
        }
    }

}
