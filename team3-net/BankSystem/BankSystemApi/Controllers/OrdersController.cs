using BankSystemApi.Contracts;
using BankSystemApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet("getAll/{userId}")]
        public IActionResult GetAll(int userId)
        {
            var data = (List<Order>)_orderService.GetAllForUser(userId);
            if (data == null || data.Count == 0) 
                return NotFound(new
            {
                StatusCode = 404,
                message = "Not Found"
            });

            return Ok(new { Orders = data, Count = data.Count });

        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _orderService.GetById(id);
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

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            try
            {
                _orderService.Create(order);
                return Ok(new { StatusCode = 201, message = "Purchase was successful" });
            }
            catch(ArgumentNullException ex)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = ex.Message
                });
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
    }
}
