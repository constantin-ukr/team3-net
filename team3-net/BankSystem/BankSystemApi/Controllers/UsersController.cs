using BankSystemApi.Contracts;
using BankSystemApi.DTO;
using BankSystemApi.Extensions;
using BankSystemApi.Mappers;
using BankSystemApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserDTO model)
        {
            try
            {
                var data = _userService.Authenticate(model.Login, model.Password.HashPassword());
                return Ok(data);
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

        [AllowAnonymous]
        [HttpPost("registrate")]
        public IActionResult Register([FromBody] UserDTO model)
        {
            try
            {
                _userService.Register(model.ToUser());
                return Ok(new { Message = "User was registered", StatusCode = 200 });
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = (List<User>)_userService.GetAll();
            if (data == null || data.Count == 0) return NotFound(new
            {
                StatusCode = 404,
                message = "Not Found"
            });

            return Ok(new { Users = data, Count = data.Count });

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _userService.GetById(id);

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
    }
}
