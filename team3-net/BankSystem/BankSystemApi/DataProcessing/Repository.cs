using BankSystemApi.Contracts;
using BankSystemApi.Database;
using BankSystemApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BankSystemApi.DataProcessing
{
    public class Repository<T> : IRepository<T> where T : BaseClass
    {
        private readonly BankSystemContext _context;
        private DbSet<T> _enteties;
        public Repository(BankSystemContext context)
        {
            _context = context;
            _enteties = context.Set<T>();
        }

        public void Delete(int id)
        {
            var entity = _enteties.FirstOrDefault(x => x.Id == id);

            _enteties.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _enteties.ToList();
        }

        public T GetById(int id)
        {
            T entity = _enteties.SingleOrDefault(s => s.Id == id);

            return entity;
        }

        public void Insert(T entity)
        {
            _enteties.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }


    }
}
