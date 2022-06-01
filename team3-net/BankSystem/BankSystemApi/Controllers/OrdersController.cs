using BankSystemApi.Contracts;
using BankSystemApi.Filters;
using BankSystemApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemApi.Controllers
{
    [MyExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _orderService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult MakeOrder([FromBody] Order order)
        {
            _orderService.MakeOrder(order);
            return Ok(new { StatusCode = 201, message = "Purchase was successful" });
        }
    }
}
