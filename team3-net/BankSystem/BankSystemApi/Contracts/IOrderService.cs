using BankSystemApi.Models;
using System.Collections.Generic;

namespace BankSystemApi.Contracts
{
    public interface IOrderService
    {
        Order GetById(int id);
        void Create(Order order);
        void Delete(int id);
        List<Order> GetAllForUser(int userId);
    }
}
