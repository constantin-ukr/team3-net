using BankSystemApi.Models;

namespace BankSystemApi.Contracts
{
    public interface ICreditCardService
    {
        CreditCard GetById(int id);
        decimal getBalance(string cardNumber);

    }
}
