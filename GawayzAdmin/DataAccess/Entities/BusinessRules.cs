using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
  public  class BusinessRules
    {
        public BusinessRules()
        {
            ProductsBusinessRules = new List<ProductsBusinessRules>();
        }
        [Key]
        public int BusinessRuleID { get; set; }
        public string BusinessRuleName { get; set; }
        public virtual List<ProductsBusinessRules> ProductsBusinessRules { get; set; }
    }
}
