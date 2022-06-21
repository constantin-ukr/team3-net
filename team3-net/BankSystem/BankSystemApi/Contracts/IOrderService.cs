using BankSystemApi.Models;

namespace BankSystemApi.Contracts
{
    public interface IOrderService
    {
        Order GetById(int id);
        void MakeOrder(Order order);
    }
}
