using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Questions> Questions { get; }
        IGenericRepository<Choices> Choices { get; }
        IGenericRepository<Companies> Companies { get; }
        IGenericRepository<Products> Products { get; }
        IGenericRepository<ProductsSurveyQuestions> ProductsSurveyQuestions { get; }
        IGenericRepository<Users> Users { get; }
        IGenericRepository<BusinessRules> BusinessRules { get; }
        IGenericRepository<ProductsBusinessRules> ProductsBusinessRules { get; }
        IGenericRepository<ProductsCodes> ProductsCodes { get; }
        void Save(); //Commit

    }
}
