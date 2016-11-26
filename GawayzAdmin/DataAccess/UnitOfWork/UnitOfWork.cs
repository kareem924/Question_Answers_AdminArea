using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext _context;
        public UnitOfWork()
        {
            _context = new DataContext();
        }

        private IGenericRepository<Questions> _questions;
        public IGenericRepository<Questions> Questions
        {
            get
            {
                if (_questions == null)
                {
                    return new EfGenericRepository<Questions>(_context);
                }
                return _questions;
            }
        }
        private IGenericRepository<Choices> _choices;
        public IGenericRepository<Choices> Choices
        {
            get
            {
                if (_choices == null)
                {
                    return new EfGenericRepository<Choices>(_context);
                }
                return _choices;
            }
        }

        private IGenericRepository<Companies> _company;
        public IGenericRepository<Companies> Companies
        {
            get
            {
                if (_company == null)
                {
                    return new EfGenericRepository<Companies>(_context);
                }
                return _company;
            }
        }

        private IGenericRepository<Products> _products;
        public IGenericRepository<Products> Products
        {
            get
            {
                if (_products == null)
                {
                    return new EfGenericRepository<Products>(_context);
                }
                return _products;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
