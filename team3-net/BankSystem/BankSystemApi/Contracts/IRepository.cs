using BankSystemApi.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
