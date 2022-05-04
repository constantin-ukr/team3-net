using BankSystemApi.Contracts;
using BankSystemApi.DTO;
using BankSystemApi.Extensions;
using BankSystemApi.Helpers;
using BankSystemApi.Mappers;
using BankSystemApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BankSystemApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public UserService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public void Register(User user)
        {
            var userExist = _unitOfWork.GetUserRepository.GetAll().FirstOrDefault(x => x.Login == user.Login);

            if (userExist == null)
            {
                user.Password = user.Password.HashPassword();
                user.RoleId = _unitOfWork.GetRoleRepository.GetAll().FirstOrDefault(x => x.RoleName == "User").Id;
                _unitOfWork.GetUserRepository.Insert(user);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("User with such login already exist.");
            }
        }

        public UserAuthenticate Authenticate(string username, string password)
        {
            var user = _unitOfWork.GetUserRepository.GetAll().FirstOrDefault(x => x.Login == username && x.Password == password);

            if (user == null)
                return null;

            var userRole = _unitOfWork.GetRoleRepository.GetAll().FirstOrDefault(x => x.Id == user.RoleId).RoleName;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userAuth = user.ToUserAuthenticate();
            userAuth.Token = tokenHandler.WriteToken(token);

            return userAuth;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.GetUserRepository.GetAll();
        }

        public User GetById(int id)
        {
            var user = _unitOfWork.GetUserRepository.GetById(id);
            user.Role = _unitOfWork.GetRoleRepository.GetAll().FirstOrDefault(x => x.Id == user.RoleId);
            return _unitOfWork.GetUserRepository.GetById(id);
        }
    }
}
