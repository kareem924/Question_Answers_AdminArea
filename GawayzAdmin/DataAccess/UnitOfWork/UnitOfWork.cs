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

        private IGenericRepository<ProductsSurveyQuestions> _productsSurveyQuestions;
        public IGenericRepository<ProductsSurveyQuestions> ProductsSurveyQuestions
        {
            get
            {
                if (_productsSurveyQuestions == null)
                {
                    return new EfGenericRepository<ProductsSurveyQuestions>(_context);
                }
                return _productsSurveyQuestions;
            }
        }
        private IGenericRepository<Users> _users;
        public IGenericRepository<Users> Users
        {
            get
            {
                if (_users == null)
                {
                    return new EfGenericRepository<Users>(_context);
                }
                return _users;
            }
        }

        private IGenericRepository<BusinessRules> _businessRules;
        public IGenericRepository<BusinessRules> BusinessRules
        {
            get
            {
                if (_businessRules == null)
                {
                    return new EfGenericRepository<BusinessRules>(_context);
                }
                return _businessRules;
            }
        }
        private IGenericRepository<ProductsBusinessRules> _productsBusinessRules;
        public IGenericRepository<ProductsBusinessRules> ProductsBusinessRules
        {
            get
            {
                if (_productsBusinessRules == null)
                {
                    return new EfGenericRepository<ProductsBusinessRules>(_context);
                }
                return _productsBusinessRules;
            }
        }

        private IGenericRepository<ProductsCodes> _productsCodes;
        public IGenericRepository<ProductsCodes> ProductsCodes
        {
            get
            {
                if (_productsCodes == null)
                {
                    return new EfGenericRepository<ProductsCodes>(_context);
                }
                return _productsCodes;
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
