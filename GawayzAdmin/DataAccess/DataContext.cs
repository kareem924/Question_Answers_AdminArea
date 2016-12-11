using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Choices> Choices { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<ProductsSurveyQuestions> ProductsSurveyQuestions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ProductsBusinessRules> ProductsBusinessRules { get; set; }
        public DbSet<BusinessRules> BusinessRules { get; set; }
        public DbSet<ProductsCodes> ProductsCodes { get; set; }
        public DbSet<Ads> Ads { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Questions>().Ignore(t => t.ObjectState);
            modelBuilder.Entity<Choices>().Ignore(t => t.ObjectState);
           
            base.OnModelCreating(modelBuilder);
        }

    }
}
