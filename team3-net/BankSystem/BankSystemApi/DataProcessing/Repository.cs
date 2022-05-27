using BankSystemApi.Contracts;
using BankSystemApi.Database;
using BankSystemApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _enteties.ToList();
        }

        public T GetById(int id)
        {
            return _enteties.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            _enteties.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
