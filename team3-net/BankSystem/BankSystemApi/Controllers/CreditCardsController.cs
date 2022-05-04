using BankSystemApi.Contracts;
using BankSystemApi.Helpers;
using BankSystemApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        /// <summary>
        /// Returns Credit Card with specified ID
        /// </summary>
        /// <response code="200">You got Credit Card</response>
        /// <response code="404">Credit Card not found</response>
        /// <response code="400">Something went wrong</response>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _creditCardService.GetById(id);

                if (data == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Not Found"
                    });
                }
                return Ok(data);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = "Not Found"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    StatusCode = 400
                });
            }
        }


        /// <summary>
        /// Deletes Credit Card with specified ID
        /// </summary>
        /// <response code="200">You deleted Credit Card</response>
        /// <response code="404">Credit Card not found</response>
        /// <response code="400">Something went wrong</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _creditCardService.Delete(id);
                return Ok(new { Message = "Object was deleted", StatusCode = 200 });
            }
            catch (ArgumentNullException)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = "Not Found"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    StatusCode = 400
                });
            }
        }

        /// <summary>
        /// Creates Credit Card
        /// </summary>
        /// <response code="200">You created Credit Card</response>
        /// <response code="400">Some validation errors</response>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] CreditCard creditCard)
        {
            try
            {
                _creditCardService.Create(creditCard);
                
                return Ok(new { StatusCode = 201, message = "Object was added" });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Edits Credit Card with specified ID
        /// </summary>
        /// <response code="200">You updated Credit Card</response>
        /// <response code="404">Credit Card not found</response>
        /// <response code="400">Some validation errors</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] CreditCard creditCard)
        {
            try
            {
                creditCard.Id = id;
                _creditCardService.Update(creditCard);
                return Ok(new
                {
                    StatusCode = 201,
                    message = "Object was updated",
                    creditCard
                });
            }
            catch (ArgumentNullException)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = "Not Found"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    StatusCode = 400
                });
            }
        }

        /// <summary>
        /// Returns list of Credit Cards.
        /// </summary>
        /// <response code="200">You got list of Credit Cards</response>
        /// <response code="404">Any Credit Card was found</response>
        /// <response code="400">Something went wrong</response>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            
            
            try
            {
                var Result = _creditCardService.GetAll();

                return Ok(new { CreditCards = Result, Count = Result.Count() });
            }
            catch(ArgumentNullException)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = "Not Found"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    StatusCode = 400
                });
            }
        }


        /// <summary>
        /// Returns balance of Credit Card.
        /// </summary>
        /// <response code="200">You got balance of Credit Card</response>
        /// <response code="404">Any Credit Card was found</response>
        /// <response code="400">Something went wrong</response>
        [Authorize]
        [HttpGet("/balance/{cardNumber}")]
        public IActionResult GetBalance(string cardNumber)
        {
            
            try
            {
                var balance = _creditCardService.getBalance(cardNumber);

                return Ok(new { Balance = balance });
            }
            catch (ArgumentNullException)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = "Not Found"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    StatusCode = 400
                });
            }
        }
    }

}
