using BankSystemApi.Contracts;
using BankSystemApi.Database;
using BankSystemApi.Models;
using System;

namespace BankSystemApi.DataProcessing
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankSystemContext _context;
        private IRepository<CreditCard> _creditCardRepository;
        private IRepository<Order> _ordersRepository;
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;

        public UnitOfWork(BankSystemContext context)
        {
            _context = context;
        }

        public IRepository<CreditCard> GetCreditCardRepository
        {
            get
            {
                if (this._creditCardRepository == null)
                {
                    this._creditCardRepository = new Repository<CreditCard>(_context);
                }
                return _creditCardRepository;
            }
        }

        public IRepository<Order> GetOrdersRepository
        {
            get
            {
                if (this._ordersRepository == null)
                {
                    this._ordersRepository = new Repository<Order>(_context);
                }
                return _ordersRepository;
            }
        }
        public IRepository<User> GetUserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new Repository<User>(_context);
                }
                return _userRepository;
            }
        }

        public IRepository<Role> GetRoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                {
                    this._roleRepository = new Repository<Role>(_context);
                }
                return _roleRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
