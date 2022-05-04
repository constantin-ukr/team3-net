using BankSystemApi.DTO;
using BankSystemApi.Models;
using System.Collections.Generic;

namespace BankSystemApi.Contracts
{
    public interface IUserService
    {
        void Register(User user);
        UserAuthenticate Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
