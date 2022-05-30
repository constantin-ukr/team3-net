using BankSystemApi.Models;
using System.Collections.Generic;

namespace BankSystemApi.Contracts
{
    public interface IRepository<T> where T: BaseClass
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);

    }
}
