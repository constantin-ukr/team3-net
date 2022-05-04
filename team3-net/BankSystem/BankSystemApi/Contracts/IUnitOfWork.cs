using BankSystemApi.Models;
using System;

namespace BankSystemApi.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<CreditCard> GetCreditCardRepository { get; }
        IRepository<User> GetUserRepository { get; }
        IRepository<Role> GetRoleRepository { get; }
        IRepository<Order> GetOrdersRepository { get; }
        void Save();
    }
}
