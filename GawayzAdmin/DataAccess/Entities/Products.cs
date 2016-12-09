using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("Products")]
    public class Products
    {
        public Products()
        {
            ProductsBusinessRules = new List<ProductsBusinessRules>();
            ProductsCodes=new List<ProductsCodes>();
        }
        [Key]
        public int ProductID { get; set; }
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string enProductDescription { get; set; }
        public string arProductDescription { get; set; }
        public int ProductQPerPage { get; set; }
        public int ProductSurveyQPerPage { get; set; }
        public int ProductOrder { get; set; }
        public DateTime EnterdDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public Companies Company { get; set; }
        public virtual List<ProductsBusinessRules> ProductsBusinessRules { get; set; }
        public virtual List<ProductsCodes> ProductsCodes { get; set; }

    }
}
